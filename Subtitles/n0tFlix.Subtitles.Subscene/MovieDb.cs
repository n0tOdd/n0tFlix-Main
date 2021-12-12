using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using n0tFlix.Subtitles.Subscene.Models;
using MediaBrowser.Common;
using MediaBrowser.Common.Net;
using MediaBrowser.Model.Serialization;
using System.Net.Http;

namespace n0tFlix.Subtitles.Subscene
{
    public class MovieDb
    {
        private const string token = "d9d7bb04fb2c52c2b594c5e30065c23c";// Get https://www.themoviedb.org/ API token
        private readonly string _movieUrl = "https://api.themoviedb.org/3/movie/{0}?api_key={1}";
        private readonly string _tvUrl = "https://api.themoviedb.org/3/tv/{0}?api_key={1}";
        private readonly string _searchMovie = "https://api.themoviedb.org/3/find/{0}?api_key={1}&external_source={2}";

        private readonly IJsonSerializer _jsonSerializer;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IApplicationHost _appHost;
        public MovieDb(IJsonSerializer jsonSerializer, IHttpClientFactory httpClientFactory, IApplicationHost appHost)
        {
            _jsonSerializer = jsonSerializer;
            _httpClientFactory = httpClientFactory;
            _appHost = appHost;
        }

        public async Task<MovieInformation> GetMovieInfo(string id)
        {
            var opts = BaseRequestOptions(HttpMethod.Get, string.Format(_movieUrl, id, token));

            /*var searchResults = await Tools.RequestUrl<MovieInformation>(opts.Url, "", HttpMethod.Get);
            return searchResults;*/
            using (var response = await _httpClientFactory.CreateClient().SendAsync(opts)/*.ConfigureAwait(false)*/)
            {
                if (!response.IsSuccessStatusCode)
                    return null;

                var searchResults = _jsonSerializer.DeserializeFromStream<MovieInformation>(response.Content.ReadAsStream());

                return searchResults;
            }
        }

        public async Task<FindMovie> SearchMovie(string id)
        {
            var type = id.StartsWith("tt") ? MovieSourceType.imdb_id : MovieSourceType.tvdb_id;
            var opts = BaseRequestOptions(HttpMethod.Get, string.Format(_searchMovie, id, token, type.ToString()));

            using (var response = await _httpClientFactory.CreateClient().SendAsync(opts).ConfigureAwait(false))
            {
                if (!response.IsSuccessStatusCode)
                    return null;

                var searchResults = _jsonSerializer.DeserializeFromStream<FindMovie>(response.Content.ReadAsStream());

                return searchResults;
            }
        }

        public async Task<TvInformation> GetTvInfo(string id)
        {
            var movie = await SearchMovie(id);

            if (movie?.tv_episode_results == null || !movie.tv_episode_results.Any())
                return null;

            var opts = BaseRequestOptions(HttpMethod.Get, string.Format(_tvUrl, movie.tv_episode_results.First().show_id, token));

            using (var response = await _httpClientFactory.CreateClient().SendAsync(opts).ConfigureAwait(false))
            {
                if (!response.IsSuccessStatusCode)
                    return null;

                var searchResults = _jsonSerializer.DeserializeFromStream<TvInformation>(response.Content.ReadAsStream());

                return searchResults;
            }
        }


        private HttpRequestMessage BaseRequestOptions(HttpMethod httpMethod, string url)
        {
            var request = new HttpRequestMessage(httpMethod, url);
            request.Headers.UserAgent.Add(new System.Net.Http.Headers.ProductInfoHeaderValue($"Jellyfin/{_appHost?.ApplicationVersion}"));
            return request;
        }
    }
}

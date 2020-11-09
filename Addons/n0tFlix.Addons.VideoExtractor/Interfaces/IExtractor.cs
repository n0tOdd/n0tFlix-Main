using n0tFlix.Addons.VideoExtractor.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace n0tFlix.Addons.VideoExtractor.Interfaces
{
    public interface IExtractor
    {
        /// <summary>
        /// Name of the extractor
        /// </summary>
        string Name { get; }

        /// <summary>
        /// A description of the extractor class, nice to show supported sites and stuff here
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Man function used to extract the information from the page
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<List<DownloadInfo>> Extract(string url);

        /// <summary>
        /// Function used to login on selected page, if no login is needed just return true so the Extract function will run
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pw"></param>
        /// <returns></returns>
        Task<bool> Login(string id, string pw);

        /// <summary>
        /// Function used to check if the extractor supports the url we want information from
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        bool CheckURL(string url);
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace n0tFlix.Addons.VideoExtractor.Utilities
{
    public class Utils
    {
        public static string GenerateNewGuidString()
            => Guid.NewGuid().ToString();

        /// <summary>
        ///   Checks an argument to ensure it isn't null.
        /// </summary>
        /// <param name = "value">The argument value to check</param>
        /// <param name = "name">The name of the argument</param>
        public static void ArgumentNotNull(object value, string name)
        {
            if (value != null)
            {
                return;
            }

            throw new ArgumentNullException(name);
        }

        /// <summary>
        ///   Checks an argument to ensure it isn't null or an empty string
        /// </summary>
        /// <param name = "value">The argument value to check</param>
        /// <param name = "name">The name of the argument</param>
        public static void ArgumentNotNullOrEmptyString(string value, string name)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return;
            }

            throw new ArgumentException("String is empty or null", name);
        }

        public static void ArgumentNotNullOrEmptyList<T>(IEnumerable<T> value, string name)
        {
            if (value != null && value.Any())
            {
                return;
            }

            throw new ArgumentException("List is empty or null", name);
        }

        public static string MakeAbsolutePath(string folderPath, string filePath)
        {
            if (String.IsNullOrWhiteSpace(filePath)) return filePath;

            if (IsStream(filePath)) return filePath;
            if (IsAbsolutePath(filePath)) return filePath;

            if (filePath[0] == '/' || filePath[0] == '\\') //relative path and starts with / or \
            {
                filePath = filePath.Substring(1);
            }
            try
            {
                if (IsStream(folderPath))
                {
                    if (!folderPath.EndsWith("/"))
                    {
                        folderPath = folderPath + "/";
                    }
                    string path = Path.Combine(folderPath, filePath);
                    return path;
                }
                else
                {
                    string path = Path.Combine(folderPath, filePath);
                    path = Path.GetFullPath(path);
                    return path;
                }
            }
            catch (ArgumentException ex)
            {
                return filePath;
            }
            catch (PathTooLongException)
            {
                return filePath;
            }
            catch (NotSupportedException)
            {
                return filePath;
            }
        }

        public static String MakeRelativePath(string folderPath, string fileAbsolutePath)
        {
            if (String.IsNullOrEmpty(folderPath)) throw new ArgumentNullException("folderPath");
            if (String.IsNullOrEmpty(fileAbsolutePath)) throw new ArgumentNullException("filePath");

            if (!folderPath.EndsWith(Path.DirectorySeparatorChar.ToString()))
            {
                folderPath = folderPath + Path.DirectorySeparatorChar;
            }

            Uri folderUri = new Uri(folderPath);
            Uri fileAbsoluteUri = new Uri(fileAbsolutePath);

            if (folderUri.Scheme != fileAbsoluteUri.Scheme) { return fileAbsolutePath; } // path can't be made relative.

            Uri relativeUri = folderUri.MakeRelativeUri(fileAbsoluteUri);
            String relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            if (fileAbsoluteUri.Scheme.Equals("file", StringComparison.CurrentCultureIgnoreCase))
            {
                relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            }

            return relativePath;
        }

        public static bool IsAbsolutePath(string path)
        {
            if (path.Length > 3)
            {
                if (path[1] == ':' && (path[2] == '\\' || path[2] == '/')) return true;
            }
            return false;
        }

        public static bool IsRelativePath(string path)
        {
            if (path.StartsWith(@"/") ||
                path.StartsWith(@"./") ||
                path.StartsWith(@"../") ||
                path.StartsWith(@"\") ||
                path.StartsWith(@".\") ||
                path.StartsWith(@"..\")) return true;
            return false;
        }

        public static bool IsStream(string path)
        {
            return path.Contains(@"://");
        }

        public static string UnEscape(string content)
        {
            if (content == null) return content;
            return content.Replace("&amp;", "&").Replace("&apos;", "'").Replace("&quot;", "\"").Replace("&gt;", ">").Replace("&lt;", "<");
        }

        public static string Escape(string content)
        {
            if (content == null) return null;
            return content.Replace("&", "&amp;").Replace("'", "&apos;").Replace("\"", "&quot;").Replace(">", "&gt;").Replace("<", "&lt;");
        }
    }
}
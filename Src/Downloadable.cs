﻿namespace Koinonia
{
    public class Downloadable
    {
        public string Name { get; set; }
        public string CommitSha { get; set; }
        public string AuthorName { get; set; }
        public string RepositoryName { get; set; }
        public DownloadableType Type { get; set; }
        public ConfigData ConfigData { get; set; }

        public override string ToString()
        {
            return string.Format("{0}/{1}@{2} ({3})",AuthorName,RepositoryName,Name,Type);
        }
    }

    public enum DownloadableType
    {
        Branch, Tag
    }

    public static class DownloadableExtensions
    {
        public static IGithubApiRequestManager GithubApiManager
        {
            get { return KoinoniaApplication.Instance.GithubApiRequestManager; }
        }

        public static Downloadable FetchConfigData(this Downloadable downloadable, bool force = false)
        {
            if (downloadable.ConfigData != null && !force) return downloadable;

            var configString = GithubApiManager.GetConfigDataString(downloadable.AuthorName, downloadable.RepositoryName,
                downloadable.CommitSha);

            downloadable.ConfigData = ConfigData.FromJsonString(configString);

            return downloadable;
        }


    }
}
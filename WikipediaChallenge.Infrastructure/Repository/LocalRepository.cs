using System;
using System.IO;
using System.Net;
using WikipediaChallenge.Domain.Repository;

namespace WikipediaChallenge.Infrastructure.Repository
{
    public class LocalRepository : ILocalRepository
    {
        private readonly string localFolder;

        public LocalRepository(string localFolder)
        {
            this.localFolder = localFolder;
        }

        public Exception CreateFolder(string folderStructure)
        {
            bool existsFolder = System.IO.Directory.Exists(folderStructure);
            if (!existsFolder)
                try
                {
                    System.IO.Directory.CreateDirectory(folderStructure);
                }
                catch (Exception err)
                {
                    return err;
                }
            return null;
        }

        public string GetLocationFolder()
        {
            return this.localFolder;
        }

        public Boolean VerifyFile(string filePath)
        {
            return File.Exists(@filePath);
        }
    }
}

using System;
using System.IO;
using System.Net;
using WikipediaChallenge.Domain.Repository;

namespace WikipediaChallenge.Infrastructure.Repository
{
    public class LocalRepository : ILocalRepository
    {
        private readonly string uncompressedFolder;
        private readonly string compressedFolder;

        public LocalRepository(string compressedFolder, string uncompressedFolder)
        {
            this.compressedFolder = compressedFolder;
            this.uncompressedFolder = uncompressedFolder;
        }

        public Exception Initialize()
        {
            Exception err;
            err = CreateFolder(this.compressedFolder);
            if (err != null)
            {
                return err;
            }
            err = CreateFolder(this.uncompressedFolder);
            return err;
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
        public Boolean VerifyFile(string filePath)
        {
            return File.Exists(@filePath);
        }

        public string GetUncompressedLocationFolder()
        {
            return this.uncompressedFolder;
        }

        public string GetCompressedLocationFolder()
        {
            return this.compressedFolder;
        }
    }
}

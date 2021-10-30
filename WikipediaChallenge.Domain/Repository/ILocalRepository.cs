using System;

namespace WikipediaChallenge.Domain.Repository
{
    public interface ILocalRepository
    {
        public Boolean VerifyFile(String filePath);
        public Exception CreateFolder(String folderStructure);
        public string GetUncompressedLocationFolder();
        public string GetCompressedLocationFolder();
    }
}

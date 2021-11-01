using System;

namespace WikipediaChallenge.Domain.Repository
{
    public interface ILocalRepository
    {
        public Boolean VerifyFile(String filePath);
        public Exception CreateFolder(String folderStructure);
        public System.IO.StreamReader OpenStreamReader(String fileLocation);
        public string GetUncompressedLocationFolder();
        public string GetCompressedLocationFolder();
    }
}

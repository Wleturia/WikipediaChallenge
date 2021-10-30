using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

using System;
using Xunit;

namespace WikipediaChallenge.Test
{
    public class LocalRepository
    {
        static string localRepositoryCompressedFolder = "d:/tmp/compressed";
        static string localRepositoryUnompressedFolder = "d:/tmp/uncompressed";

        Infrastructure.Repository.LocalRepository localRepository = new(localRepositoryCompressedFolder, localRepositoryUnompressedFolder);

        [Fact]
        public void CreateFolderCompressed()
        {
            var resp = localRepository.CreateFolder(localRepositoryCompressedFolder + "/test");
            Assert.Null(resp);
        }

        [Fact]
        public void CreateFolderUncompressed()
        {
            var resp = localRepository.CreateFolder(localRepositoryUnompressedFolder + "/test");
            Assert.Null(resp);
        }

        [Fact]
        public void GetUncompressedLocationFolder()
        {
            var resp = localRepository.GetUncompressedLocationFolder();
            Assert.Equal(localRepositoryUnompressedFolder, resp);
        }

        [Fact]
        public void GetCompressedLocationFolder()
        {
            var resp = localRepository.GetCompressedLocationFolder();
            Assert.Equal(localRepositoryCompressedFolder, resp);
        }
    }
}

using Factory.Common.Contracts;
using Ionic.Zip;
using System.IO;

namespace Factory.Common
{
    public class UzipFiles
    {
        private readonly IUserMessageWriter messageWriter;

        public UzipFiles(IUserMessageWriter messageWriter)
        {
            this.messageWriter = messageWriter;
        }

        public void Unzip(string zipFilePath, string unzipedFilesPath)
        {
            if (!File.Exists(zipFilePath))
            {
                this.messageWriter.Show("File not found");
                return;
            }

            if (!Directory.Exists(unzipedFilesPath))
            {
                this.messageWriter.Show("Directory not found");
                return;
            }

            using (ZipFile zip = ZipFile.Read(zipFilePath))
            {
                foreach (ZipEntry file in zip)
                {
                    if (!File.Exists(unzipedFilesPath + file.FileName))
                    {
                        file.Extract(unzipedFilesPath);
                    }
                }
            }
        }
    }
}

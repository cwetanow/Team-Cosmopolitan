using Ionic.Zip;

namespace Factory.ExcelReports
{
    public class ExcelSalesReportsReader
    {
        public void UnzipFiles(string zipFilePath, string unzipedFilesPath)
        {
            using (ZipFile zip = ZipFile.Read(zipFilePath))
            {
                foreach (ZipEntry e in zip)
                {
                    e.Extract(unzipedFilesPath);
                }
            }
        }

        public void ReadZipFile(string filePath)
        {
            // Implement Loading
        }
    }
}

using System.IO.Compression;

namespace FileUpDownloadSample.Services
{
    public class FileService : IFileService
    {

        private Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostingEnvironment;

        public FileService(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }


        //Verzeichnis erstellen und alle Dateien in file-List hochladen
        public void UploadFile(List<IFormFile> files, string subDirectory)
        {
            subDirectory = subDirectory ?? string.Empty;

            //Erstelle den Absoluten Pfad
            string targetDirectory = Path.Combine(_hostingEnvironment.ContentRootPath, subDirectory);

            //Erstelle Unterverzeichnis
            if (!Directory.Exists(targetDirectory))
                Directory.CreateDirectory(targetDirectory);

            files.ForEach(async file =>
            {
                if (file.Length <= 0)
                    return;

                //absoluter ZielPfad der zu hochladenten Daten
                string filePath = Path.Combine(targetDirectory, file.FileName);

                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            });
        }

        public (string fileType, byte[] archiveData, string archiveName) DownloadFiles(string subDirectory)
        {
            //string test = $"Zugriff auf {subDirectory}";

            //Name der Zip-Datei
            var zipName = $"archive-{DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss")}.zip";

            //Kollektiere alle Files in dem subDirectory
            IList<string> files = Directory.GetFiles(Path.Combine(_hostingEnvironment.ContentRootPath, subDirectory)).ToList();

            byte[] compressBytes = GetZipArchive(files);


            return ("application/zip", compressBytes, zipName);
        }


        private byte[] GetZipArchive(IList<string> filePaths)
        {
            List<InMemeoryFile> files = new List<InMemeoryFile>();

            foreach (string file in filePaths)
                files.Add(LoadFromFile(file));

            byte[] archiveFile;

            using (MemoryStream archiveStream = new MemoryStream())
            {
                //ZipArchive -> repsräsentiert eine ZipFile
                using (ZipArchive archive = new ZipArchive(archiveStream, ZipArchiveMode.Create, true))
                {
                    foreach (InMemeoryFile currentInMemory in files)
                    {
                        ZipArchiveEntry zipArchiveEntry = archive.CreateEntry(currentInMemory.FileName, CompressionLevel.Optimal);
                        using Stream zipStream = zipArchiveEntry.Open();
                        zipStream.Write(currentInMemory.Content, 0, currentInMemory.Content.Length);
                    }
                }
                archiveFile = archiveStream.ToArray();
            }
            return archiveFile;



        }

        /// <summary>
        /// Lade Datei von absoluten Pfad
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private InMemeoryFile LoadFromFile(string path)
        {
            using var fs = File.OpenRead(path);

            using var memFile = new MemoryStream();

            fs.CopyTo(memFile);

            memFile.Seek(0, SeekOrigin.Begin);

            return new InMemeoryFile() { Content = memFile.ToArray(), FileName = Path.GetFileName(path) };
        }

        public string SizeConverter(long bytes)
        {
            var fileSize = new decimal(bytes);
            var kilobyte = new decimal(1024);
            var megabyte = new decimal(1024 * 1024);
            var gigabyte = new decimal(1024 * 1024 * 1024);

            switch (fileSize)
            {
                case var _ when fileSize < kilobyte:
                    return $"Less then 1KB";
                case var _ when fileSize < megabyte:
                    return $"{Math.Round(fileSize / kilobyte, 0, MidpointRounding.AwayFromZero):##,###.##}KB";
                case var _ when fileSize < gigabyte:
                    return $"{Math.Round(fileSize / megabyte, 2, MidpointRounding.AwayFromZero):##,###.##}MB";
                case var _ when fileSize >= gigabyte:
                    return $"{Math.Round(fileSize / gigabyte, 2, MidpointRounding.AwayFromZero):##,###.##}GB";
                default:
                    return "n/a";
            }
        }


    }

    class InMemeoryFile
    {
        public string FileName { get; set; }
        public byte[] Content { get; set; }
    }
}

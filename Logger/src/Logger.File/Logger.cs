using Logger.Abstractions;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Logger.File
{
    class Logger : ILogger
    {
        public string FileDestination { get; }
        public string FileName { get; }

        private readonly string fullFileDestination;
        private readonly long fileSizeForRotate;
        private long fileArchiveCounter;
        private string fileNameWithoutExtension;
        private string fileExtension;

        public Logger(string fileDestination, string fileName, long fileSizeForRotate)
        {
            this.FileDestination = fileDestination;
            this.FileName = fileName;
            this.fileSizeForRotate = fileSizeForRotate;
            this.fullFileDestination = Path.Combine(FileDestination, FileName);
            this.fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
            this.fileExtension = Path.GetExtension(fileName);
            this.fileArchiveCounter = GetActualArchiveCount();
        }

        public void Log(LogLevel logLevel, string message)
        {
            using (StreamWriter file = new StreamWriter(fullFileDestination, append: true))
            {
                file.WriteLine(this.LogMessageFormatter(logLevel, message));
            }

            long length = new FileInfo(this.fullFileDestination).Length;
            if (length >= this.fileSizeForRotate)
            {
                try
                {
                    // Will not overwrite if the destination file already exists.
                    System.IO.File.Copy(
                        this.fullFileDestination,
                        Path.Combine(this.FileDestination, this.LogFileFormatter())
                    );

                    System.IO.File.Delete(this.fullFileDestination);
                }
                catch (IOException copyError)
                {
                    // Catch exception if the file was already copied.
                    Console.WriteLine(copyError.Message);
                }
                catch (Exception generalException)
                {
                    // Catch exception if the file was already copied.
                    Console.WriteLine(generalException.Message);
                }
            }
        }
        public async Task LogAsync(LogLevel logLevel, string message)
        {
            using (StreamWriter file = new StreamWriter(fullFileDestination, append: true))
            {
                await file.WriteLineAsync(LogMessageFormatter(logLevel, message));
            }

            FileInfo fileInfo = new FileInfo(this.fullFileDestination);
            if (fileInfo.Length >= this.fileSizeForRotate)
            {
                try
                {
                    using (FileStream SourceStream = System.IO.File.Open(this.fullFileDestination, FileMode.Open))
                    {
                        using (FileStream DestinationStream = System.IO.File.Create(Path.Combine(this.FileDestination, LogFileFormatter())))
                        {
                            await SourceStream.CopyToAsync(DestinationStream);
                        }
                    }

                    await fileInfo.DeleteAsync();
                }
                catch (IOException copyError)
                {
                    // Catch exception if the file was already copied.
                    Console.WriteLine(copyError.Message);
                }
                catch (Exception generalException)
                {
                    // Catch exception if the file was already copied.
                    Console.WriteLine(generalException.Message);
                }
            }
        }

        private string LogMessageFormatter(LogLevel logLevel, string message)
        {
            return $"{DateTime.UtcNow} {logLevel} {message}";
        }
        private string LogFileFormatter()
        {
            this.fileArchiveCounter++;
            return $"{this.fileNameWithoutExtension}.{this.fileArchiveCounter}{this.fileExtension}";
        }
        private long GetActualArchiveCount()
        {
            // TODO Safer Solution
            try
            {
                string[] dirs = Directory.GetFiles(this.FileDestination, $"{this.fileNameWithoutExtension}.*.{this.fileExtension}");
                string actualFile = Path.GetFileName(dirs.OrderBy(x => x).Last());
                string[] elements = actualFile.Split(".");
                return long.Parse(elements[1]);
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}

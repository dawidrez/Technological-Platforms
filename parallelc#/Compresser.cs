using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace WpfApp1
{
    class Compresser
    {
        public void Compress(DirectoryInfo dir)
        {
            List<Task> filesCompress = new List<Task>();
            var files = dir.GetFiles();
            foreach(var file in files)
            {
                filesCompress.Add(Task.Factory.StartNew(() => CompressFile(file)));
                
            }
            Task.WaitAll(filesCompress.ToArray());
        }

        public void CompressFile(FileInfo fi)
        {
            using (FileStream original = fi.OpenRead())
            {
                if ((File.GetAttributes(fi.FullName) &
                   FileAttributes.Hidden) != FileAttributes.Hidden && fi.Extension != ".gz")
                {
                    using (FileStream compressedFi = File.Create(fi.FullName + ".gz"))
                    {
                        using (GZipStream compressionStream = new GZipStream(compressedFi, CompressionMode.Compress))
                        {
                            original.CopyTo(compressionStream);

                        }
                    }
                // FileInfo info = new FileInfo($"{fi.Directory.FullName}{Path.DirectorySeparatorChar}{fi.Name}.gz");
                }

            }
        }

        public void Decompress(DirectoryInfo dir)
        {
            List<Task> filesDecompress = new List<Task>();
            var files = dir.GetFiles();
            foreach (var file in files)
            {
                filesDecompress.Add(Task.Factory.StartNew(() => DecompressFile(file)));

            }
            Task.WaitAll(filesDecompress.ToArray());
        }

        public void DecompressFile(FileInfo fi)
        {
            using (FileStream original = fi.OpenRead())
            {
                string fullname = fi.FullName;
                string extension = fi.Extension;
                
                if (extension == ".gz")
                {
                    string newName = fullname.Remove(fullname.Length - extension.Length);
                    using (FileStream decompressedFileStream = File.Create(newName))
                    {
                        using (GZipStream decompressionStream = new GZipStream(original, CompressionMode.Decompress))
                        {
                            decompressionStream.CopyTo(decompressedFileStream);
                        }
                    }
                }

            }
        }
    }
}

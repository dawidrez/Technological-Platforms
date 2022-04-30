using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
namespace lab1
{
    static class Program
    {
        static void Main(String [] args)
        {
            string path = args[0];
            PrintFiles(path,0);
            DirectoryInfo dir = new DirectoryInfo(path);
            DateTime time=dir.GetOldestFile();
            Console.WriteLine("Oldest file was created: {0}",time.ToString());
            CreateCollection(path);
            PrintDictionary();
        }
        static void PrintDictionary()
        {
            SortedDictionary<string, int> collection = new SortedDictionary<string, int>(new Comparer());
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fs = new FileStream("temp.dat", FileMode.Open);
            try
            {
                collection= (SortedDictionary<string, int>)binaryFormatter.Deserialize(fs);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }
            fs.Close();
            foreach (KeyValuePair<string, int> kvp in collection)
            {
                Console.WriteLine("{0}-->{1}",kvp.Key,kvp.Value);
            }

        }
        static void CreateCollection(string path)
        {
            SortedDictionary<string,int> collection = new SortedDictionary<string,int>(new Comparer());
            string[] folders = Directory.GetDirectories(path);
            for (int i = 0; i < folders.Length; i++)
            {
                int size=Directory.GetDirectories(folders[i]).Length;
                size+=Directory.GetFiles(folders[i]).Length;
                collection.Add(Path.GetFileName(folders[i]),size);
            }
            string[] files = Directory.GetFiles(path);
            for (int i = 0; i < files.Length; i++)
            {
                FileInfo fi=new FileInfo(files[i]);
                long size =fi.Length;
                collection.Add(fi.Name, Convert.ToInt32(size));
            }
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            FileStream fs = new FileStream("temp.dat", FileMode.Create);
            try
            {
                binaryFormatter.Serialize(fs, collection);
            }
            catch (SerializationException e)
            {
                Console.WriteLine(e.Message);
            }
            fs.Close();
           

        }
        static void  PrintFiles(String path,int nCall)
        {
            String indentation = "";
            for (int i = 0; i < nCall; i++)
            {
                indentation += "    ";
            }
            string[] folders = Directory.GetDirectories(path);
            string[] files = Directory.GetFiles(path);
            FileInfo fsi = new FileInfo(path);
            String rahs = fsi.GetRahs();
            int size = folders.Length + files.Length;
            Console.WriteLine("{0}{1} ({2}) {3}",indentation,Path.GetFileName(path),Convert.ToString(size),rahs);
            for (int i = 0; i < folders.Length; i++)
            {
                PrintFiles(folders[i],nCall+1);

            }
            indentation += "    ";
            foreach (string file in files)
            {
                 fsi = new FileInfo(file);
                rahs = fsi.GetRahs();
               long  size2=fsi.Length;
                Console.WriteLine("{0}{1} {2} {3} bytes",indentation,Path.GetFileName(file),rahs,size2.ToString());
            }
            
        }
        public static DateTime GetOldestFile(this DirectoryInfo path)
        {
            DirectoryInfo[] folders = path.GetDirectories();
            DateTime oldest= DateTime.MaxValue;
            foreach(DirectoryInfo folder in folders){
                DateTime tmp=folder.GetOldestFile();
                if (tmp < oldest)
                {
                    oldest = tmp;
                }
            }
            FileInfo[] files=path.GetFiles();
            foreach(FileInfo file in files)
            {
                DateTime tmp = file.CreationTime;
                if(tmp < oldest)
                {
                     oldest=tmp;
                }
            }
            return oldest;

        }
        public static string GetRahs(this FileSystemInfo file)
        {
            string ret = "";
            if (file == null)
            {
                ret = "----";
                return ret;
            }
            FileAttributes attributes = file.Attributes;
            if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
            {
                ret += "r";
            }
            else
                ret += "-";
            if ((attributes & FileAttributes.Archive) == FileAttributes.Archive)
            {
                ret += "a";
            }
            else
                ret += "-";
            if ((attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
            {
                ret += "h";
            }
            else
                ret += "-";
            if ((attributes & FileAttributes.System) == FileAttributes.System)
            {
                ret += "s";
            }
            else
                ret += "-";
            return ret;

        }

    }
}



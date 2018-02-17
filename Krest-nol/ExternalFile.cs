using System;
using Newtonsoft.Json;
using System.IO;

namespace KrestNol
{
    public static class ExternalFile
    {
        public static void Save(string put, Game data)
        {
            FileStream fs = File.Create(put);
            string s = JsonConvert.SerializeObject(data);
            StreamWriter stream = new StreamWriter(fs);
            stream.Write(s);
            stream.Close();
            fs.Close();
        }

        public static void Load(string put, ref Game data)
        {
            if (data == null) throw new ArgumentNullException("data");
            using (FileStream fs = File.OpenRead(put))
            {
                StreamReader streamReader = new StreamReader(fs);
                string s = streamReader.ReadToEnd();
                data = JsonConvert.DeserializeObject<Game>(s);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace KrestNol
{
    public class ExternalFile
    {
        public static void Save(string put, Game data)
        {
            JsonSerializer ser = new JsonSerializer();
            FileStream fs = File.Create(put);
            {
                //fs.Close();
                //fs = File.OpenWrite(put);
                string s = JsonConvert.SerializeObject(data);
                StreamWriter stream = new StreamWriter(fs);
                stream.Write(s);
                stream.Close();
                //TextWriter tw = new StreamWriter(fs);
                //ser.Serialize(tw, data);
                //Console.ReadLine();
                fs.Close();
            }
        }

        public static void Load(string put, ref Game data)
        {
            JsonSerializer ser = new JsonSerializer();
            using (FileStream fs = File.OpenRead(put))
            {
                StreamReader streamReader = new StreamReader(fs);
                string s = streamReader.ReadToEnd();
                data = JsonConvert.DeserializeObject<Game>(s);
            }
        }
    }
}

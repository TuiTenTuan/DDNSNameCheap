using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DDNSNameCheap
{
    public class Config
    {
        private static Config instance;

        public static Config Instance
        {
            get { if (instance == null) instance = new Config(); return instance; }
            private set { instance = value; }
        }
        private Config() { }

        public void Serialize(string pathData, string dataFileName, List<Profile> profiles)
        {
            if (profiles.Count == 0)
            {
                File.Delete(Path.Combine(pathData, dataFileName));
            }
            else
            {
                StreamWriter sw = new StreamWriter(new FileStream(Path.Combine(pathData, dataFileName), FileMode.Create));

                XmlSerializer serializer = new XmlSerializer(typeof(List<Profile>));

                try
                {
                    serializer.Serialize(sw, profiles);
                }
                catch { }

                sw.Close();
            }
        }

        public async Task<List<Profile>> Deserialize(string pathData, string dataFileName)
        {
            string path = Path.Combine(pathData, dataFileName);

            if (File.Exists(path))
            {
                Task<List<Profile>> t = Task.Run(() =>
                {
                    List<Profile> profiles = new List<Profile>();

                    StreamReader sr = new StreamReader(new FileStream(path, FileMode.Open));

                    try
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(List<Profile>));

                        profiles = serializer.Deserialize(sr) as List<Profile>;

                        sr.Close();
                    }
                    catch
                    {
                        sr.Close();
                    }

                    return profiles;
                });

                return await t;
            }
            else
            {
                return new List<Profile>();
            }
        }

        public async Task<List<Profile>> Deserialize(string pathFile)
        {
            if (File.Exists(pathFile))
            {
                Task<List<Profile>> t = Task.Run(() =>
                {
                    List<Profile> profiles = new List<Profile>();

                    StreamReader sr = new StreamReader(new FileStream(pathFile, FileMode.Open));

                    try
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(List<Profile>));

                        profiles = serializer.Deserialize(sr) as List<Profile>;

                        sr.Close();
                    }
                    catch
                    {
                        sr.Close();
                    }

                    return profiles;
                });

                return await t;
            }
            else
            {
                return new List<Profile>();
            }
        }
    }
}

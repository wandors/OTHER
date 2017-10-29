using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Run ru = new Run();
            //ru.Start();
            ru.Stops();
            Console.ReadKey();
        }
    }
    class Run
    {
        string path = @"D://t.dat";
        BinaryFormatter formatter = new BinaryFormatter();
        Dictionary<string, string> name = new Dictionary<string, string>();
        Dictionary<string, Dictionary<string, string>> soname = new Dictionary<string, Dictionary<string, string>>();
        Dictionary<string, Dictionary<string, Dictionary<string, string>>> father = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
        public void Start()
        {
            name.Add("First", "Serg");
            soname.Add("Second", name);
            father.Add("Tree", soname);
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, father);

                Console.WriteLine("Объект сериализован");
            }

        }
        public void Stops()
        {
            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {
                var father = (Dictionary<string, Dictionary<string, Dictionary<string,string>>>)formatter.Deserialize(fs);

                Console.WriteLine("Объект десериализован");
                foreach(KeyValuePair<string, Dictionary<string,Dictionary<string,string>>> i in father)
                {
                    soname = i.Value;
                    Console.WriteLine("Key = {0} : Value = {1}",i.Key.ToString(), i.Value.ToString());
                }
                foreach(KeyValuePair<string,Dictionary<string, string>> id in soname)
                {
                    Console.WriteLine("Key = {0} : Value = {1}",id.Key.ToString(),id.Value.ToString());
                    name = id.Value;
                }
                foreach(KeyValuePair<string, string> ig in name)
                {
                    Console.WriteLine("Key = {0} : Value = {1}", ig.Key.ToString(),ig.Value.ToString());

                }
            }
        }
    }
}

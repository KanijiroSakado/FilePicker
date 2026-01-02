using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Serialization;


namespace sakado
{
    class Global
    {
        public static Random RND = new System.Random();
    }


    public class FilePicker
    {
        public string Name;
        public Ingredient[] Ingredients;
        public double EntireWeight;
        int CurrentPosition;
        List<string> FileHistory;

        public FilePicker()
        {
            Ingredients = new Ingredient[0];
            Name = "default_name";
            EntireWeight = 0;

            FileHistory = new List<string>();
            CurrentPosition = 0;


        }


        public void Add(string fullpath, double weight)
        {
            Array.Resize(ref Ingredients, Ingredients.Length + 1);
            Ingredients[Ingredients.Length - 1] = new Ingredient("dummy_name",fullpath,weight);

            EntireWeight += weight;
        }

        public void MakeSureNextExist()
        {
            while (CurrentPosition + 1 >= FileHistory.Count())
            {
                FileHistory.Add(GetAnotherFile());
            }
        }

        public string GetNextFile()
        {
            MakeSureNextExist();
            CurrentPosition++;

            return GetCurrentFile();
        }

        public string GetPreviousFile()
        {
            if (CurrentPosition > 0) CurrentPosition--;

            return GetCurrentFile();
        }

        public string ScoutNextFile()
        {
            MakeSureNextExist();
            return FileHistory[CurrentPosition + 1];
        }

        public string GetCurrentFile()
        {
            return FileHistory[CurrentPosition];
        }

        public string ScoutPreviousFile()
        {
            if (CurrentPosition <= 0) return FileHistory[0];
            return FileHistory[CurrentPosition - 1];
        }


        string GetAnotherFile()
        {
            double val = Global.RND.Next(0, 100);
            int chosen;
            double weight_counter = 0;

            val = val * EntireWeight / 100;


            for (chosen = 0; chosen < Ingredients.Length; chosen++)
            {
                weight_counter += Ingredients[chosen].Weight;
                if (val <= weight_counter) break;
            }

            return Ingredients[chosen].GetAnotherFile();
        }

        /*
        public static Blend Import(string filename)
        {
            Blend blend;
            FileStream stream;
            try
            {
                stream = new FileStream(filename, FileMode.Open);
            }
            catch
            {
                return null;
            }
            XmlSerializer serizlizer = new XmlSerializer(typeof(Blend));

            blend = (Blend)serizlizer.Deserialize(stream);

            stream.Close();

            return blend;
        }
        

        public void Export(string filename)
        {
            StreamWriter writer = new StreamWriter(filename);
            XmlSerializer serizlizer = new XmlSerializer(typeof(Blend));

            serizlizer.Serialize(writer, this);

            writer.Close();
        }
        */

    }

    public class Ingredient
    {
        public string Name;
        public string FullPath;
        public double Weight;
        string[] Files;

        public Ingredient(string name, string fullpath, double weight)
        {
            Name = name;
            FullPath = fullpath;
            Weight = weight;
            Files = Directory.GetFiles(FullPath, "*.dat", SearchOption.AllDirectories);
        }

        public string GetAnotherFile()
        {
            return Files[Global.RND.Next(0, Files.Length)];
        }
    }


}

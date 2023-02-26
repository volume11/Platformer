using System.IO;
using System.Text.Json;

namespace platformer
{
    static class PersistentData
    {
        static SaveData data = new SaveData();
        public static int Score {get {return data.Score;} set {data.Score = value;}}

        public static void Save()
        {
            string jsonString = JsonSerializer.Serialize<SaveData>(data);
            File.WriteAllText("assets/savedata/save.txt", jsonString);
        }

        #nullable enable
        public static void Load()
        {
            //If the save data does not exist create the file and save empty data to it
            if (!File.Exists("assets/savedata/save.txt"))
            {
                File.Create("assets/savedata/save.txt").Close();
                Save();
                return;
            }

            try
            {
                data = JsonSerializer.Deserialize<SaveData>(File.ReadAllText("assets/savedata/save.txt"));
            }
            catch
            {
                //If the save data file is corrupt in some way (cannot be deserialised to the save data class), wipe the save data file with the defaults
                Save();
            }
        }
    }

    class SaveData
    {
        public int Score {get; set;}
    }
}
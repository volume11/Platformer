using System.IO;
using System.Text.Json;

namespace platformer
{
    static class PersistentData
    {
        static SaveData data = new SaveData();

        public static string CurrentLevel { get => data.currentLevel; set => data.currentLevel = value; }

        public static void Save()
        {
            string jsonString = JsonSerializer.Serialize<SaveData>(data);
            File.WriteAllText("assets/savedata/save.txt", jsonString);
        }

        public static void Load()
        {
            //If the save data does not exist create the file and save empty data to it
            if (!File.Exists("assets/savedata/save.txt"))
            {
                File.Create("assets/savedata/save.txt").Close();
                ResetData();
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
                ResetData();
                Save();
            }
        }

        static void ResetData()
        {
            data = new SaveData();
            data.currentLevel = AssetManager.GetFirstLevel();
        }
    }

    class SaveData
    {
        public string currentLevel {get; set;}
    }
}
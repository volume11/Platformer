using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace platformer
{
    static class PersistentData
    {
        static SaveData data = new SaveData();

        public static string CurrentLevel { get => data.currentLevel; set => data.currentLevel = value; }

        public static LevelData GetLevelData(string LevelID)
        {
            return data.levelScores[LevelID];
        }

        public static void SetLevelData(string LevelID, LevelData levelData)
        {
            data.levelScores[LevelID] = levelData;
        }

        public static void Save()
        {
            JsonSerializerOptions options = new JsonSerializerOptions();
            options.WriteIndented = true;
            string jsonString = JsonSerializer.Serialize<SaveData>(data, options);
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

            data.levelScores = new Dictionary<string, LevelData>();
            foreach (string level in AssetManager.GetAllLevels())
            {
                LevelData levelData = new LevelData();
                levelData.levelName = level;
                data.levelScores.Add(level, levelData);
            }
        }
    }

    class SaveData
    {
        public string currentLevel {get; set;}
        public Dictionary<string, LevelData> levelScores {get; set;}
    }
}
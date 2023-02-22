using System.IO;
using System.Collections.Generic;

using Raylib_cs;

namespace platformer
{
    static class AssetManager
    {
        static Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();
        static Dictionary<string, Sound> sounds = new Dictionary<string, Sound>();
        static Dictionary<string, Music> music = new Dictionary<string, Music>();

        public static void Init()
        {
            textures.Clear();
            foreach (string path in Directory.GetFiles("assets/sprites"))
            {
                Texture2D texture = Raylib.LoadTexture(path);
                string key = Path.GetFileNameWithoutExtension(path);
                textures.Add(key, texture);
            }

            sounds.Clear();
            foreach (string path in Directory.GetFiles("assets/sounds"))
            {
                Sound sound = Raylib.LoadSound(path);
                string key = Path.GetFileNameWithoutExtension(path);
                sounds.Add(key, sound);
            }

            music.Clear();
            foreach (string path in Directory.GetFiles("assets/music"))
            {
                Music m = Raylib.LoadMusicStream(path);
                string key = Path.GetFileNameWithoutExtension(path);
                music.Add(key, m);
            }
        }

        public static Optional<Sound> getSound(string key)
        {
            if (sounds.ContainsKey(key))
            {
                return new Optional<Sound>(sounds[key]);
            }
            System.Console.Error.WriteLine($"AssetManager: Failed to find sound {key}");
            return new Optional<Sound>();
        }

        public static Optional<Texture2D> getTexture(string key)
        {
            if (textures.ContainsKey(key))
            {
                return new Optional<Texture2D>(textures[key]);
            }
            return new Optional<Texture2D>();
        }

        public static Optional<Music> GetMusic(string key)
        {
            Music m;
            if (music.TryGetValue(key, out m))
            {
                return new Optional<Music>(m);
            }
            return new Optional<Music>();
        }
    }
}
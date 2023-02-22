using Raylib_cs;

namespace platformer
{
    static class SoundManager
    {
        static float volume = 1.0f;

        public static Music currentTrack;

        public static void PlaySound(string key)
        {
            Optional<Sound> sound = AssetManager.getSound(key);
            if (sound.HasValue)
            {
                Raylib.SetSoundVolume(sound.Value, volume);
                Raylib.PlaySoundMulti(sound.Value);
            }
        }

        public static void PlayMusic(string key)
        {
            Optional<Music> music = AssetManager.GetMusic("bgm");
            if (music.HasValue)
            {
                Raylib.StopMusicStream(currentTrack);
                Raylib.SetMusicVolume(music.Value, volume);
                currentTrack = music.Value;
                Raylib.PlayMusicStream(currentTrack);   
            }
        }

        public static void SetVolume(float v)
        {
            volume = v;
            Raylib.SetMusicVolume(currentTrack, v);
        }
    }
}
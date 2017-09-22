using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using AdventureGame.Main.Screens;

namespace AdventureGame.Main.GameManagers
{
    public enum SoundTypes
    {
        Shot = 1,
        MachineGun = 2,
        Walk = 3,
        Jump = 4,
        Attack = 5,
        Explosion = 6,
    }

    public class SoundManager
    {
        private Game game;
        private Dictionary<SoundTypes, Sound> sounds;
        private Dictionary<ScreenType, Song> musics;

        public SoundManager(Game game)
        {
            this.game = game;
            this.sounds = new Dictionary<SoundTypes, Sound>();
            this.musics = new Dictionary<ScreenType, Song>();
        }
        
        public void LoadContent()
        {
            this.sounds.Add(SoundTypes.Shot, new Sound(this.game.Content.Load<SoundEffect>("sounds/shot")));
            this.sounds.Add(SoundTypes.MachineGun, new Sound(this.game.Content.Load<SoundEffect>("sounds/machinegun")));
            this.sounds.Add(SoundTypes.Attack, new Sound(this.game.Content.Load<SoundEffect>("sounds/explosion")));
            musics.Add(ScreenType.Start, this.game.Content.Load<Song>("bgm/ranking"));
            musics.Add(ScreenType.Action, this.game.Content.Load<Song>("bgm/stage1"));
            musics.Add(ScreenType.About, this.game.Content.Load<Song>("bgm/about"));
            musics.Add(ScreenType.HowToPlay, this.game.Content.Load<Song>("bgm/howToPlay"));
            musics.Add(ScreenType.Help, this.game.Content.Load<Song>("bgm/help"));
            musics.Add(ScreenType.Rangking, this.game.Content.Load<Song>("bgm/ranking"));
            musics.Add(ScreenType.GameOver, this.game.Content.Load<Song>("bgm/gameOver"));
            musics.Add(ScreenType.Credit, this.game.Content.Load<Song>("bgm/credit"));
            musics.Add(ScreenType.StageClear, this.game.Content.Load<Song>("bgm/stageClear"));
            musics.Add(ScreenType.FinalBoss, this.game.Content.Load<Song>("bgm/final"));
            MediaPlayer.IsRepeating = true;   
        }

        public void Play(SoundTypes soundType)
        {
            this.sounds[soundType].Play();
        }

        public void Stop(SoundTypes soundType)
        {
            this.sounds[soundType].Stop();
        }

        public void StopAll()
        {
            foreach(KeyValuePair<SoundTypes, Sound> sound in sounds)
            {
                sound.Value.Stop();
            }
            MediaPlayer.Stop();
        }

        public void PlayBackgroudnMusic(ScreenType screen, bool repeat)
        {
            MediaPlayer.IsRepeating = repeat;
            MediaPlayer.Play(musics[screen]);
        }
        public void StopBackgroundMusic()
        {
            MediaPlayer.Stop();
        }
    }
}
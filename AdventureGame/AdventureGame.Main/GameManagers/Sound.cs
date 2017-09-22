using Microsoft.Xna.Framework.Audio;

namespace AdventureGame.Main.GameManagers
{
    public class Sound
    {
        private bool isPlaying;
        private SoundEffect effect;
        private SoundEffectInstance instance;

        public Sound(SoundEffect effect)
        {
            this.effect = effect;
            this.instance = effect.CreateInstance();
            this.instance.Volume = 1.0f;
            this.isPlaying = false;
        }

        public void Play()
        {
            effect.Play();
        }

        public void Stop()
        {
            if (isPlaying)
            {
                this.instance.Stop();
                this.isPlaying = false;
            }
        }
    }
}

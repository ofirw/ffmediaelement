using System;
using System.Drawing;
using System.Threading.Tasks;
using Unosquare.FFME.Platform;

namespace Unosquare.FFME.GDI
{
    public class MediaPlayer
    {
        private readonly MediaEngine mediaEngine;

        public event Action<Bitmap> OnFrame;

        public MediaPlayer()
        {
            MediaEngine.Initialize(WindowsPlatform.Instance);
            this.mediaEngine = new MediaEngine(this, null);
        }

        internal void DrawFrame(Bitmap frame)
        {
            this.OnFrame?.Invoke(frame);
        }

        public async Task Open(Uri uri)
        {
            await this.mediaEngine.Open(uri).ConfigureAwait(false);
        }

        public async Task Play()
        {
            await this.mediaEngine.Play().ConfigureAwait(false);
        }

        public async Task Stop()
        {
            await this.mediaEngine.Stop().ConfigureAwait(false);
        }

        public async Task Close()
        {
                await this.mediaEngine.Close().ConfigureAwait(false);
        }
    }
}

using System;
using System.Drawing;
using Unosquare.FFME.Primitives;
using Unosquare.FFME.Shared;

namespace Unosquare.FFME.GDI
{
    internal class VideoRenderer : IMediaRenderer
    {
        private readonly AtomicBoolean isRenderingInProgress = new AtomicBoolean(false);

        public VideoRenderer(MediaEngine mediaCore)
        {
            this.MediaCore = mediaCore;
        }

        public MediaEngine MediaCore { get; }

        public void Close()
        {
        }

        public void Pause()
        {
        }

        public void Play()
        {
        }

        public void Render(MediaBlock mediaBlock, TimeSpan clockPosition)
        {
            if (!(mediaBlock is VideoBlock)) return;
            if (this.isRenderingInProgress.Value) return;

            var block = (VideoBlock)mediaBlock;

            // Flag the start of a rendering cycle
            this.isRenderingInProgress.Value = true;

            if (block.IsDisposed)
            {
                this.isRenderingInProgress.Value = false;
                return;
            }

            try
            {
                var result = new Bitmap(
                block.PixelWidth,
                block.PixelHeight,
                block.PictureBufferStride,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb,
                block.Buffer);

                // Set the DPI, otherwise the pixel coordinates won't match
                // See issue #250
                result.SetResolution(
                    Convert.ToSingle(96),
                    Convert.ToSingle(96));

                var player = this.MediaCore?.Parent as MediaPlayer;
                player?.DrawFrame(result);
            }
            catch (Exception ex)
            {
                //this.LogError(Aspects.VideoRenderer, $"{nameof(VideoRenderer)}.{nameof(Render)} bitmap failed.", ex);
            }
            finally
            {
                // Always reset the rendering state
                this.isRenderingInProgress.Value = false;
            }

        }

        public void Seek()
        {
        }

        public void Stop()
        {
        }

        public void Update(TimeSpan clockPosition)
        {
        }

        public void WaitForReadyState()
        {
        }
    }
}
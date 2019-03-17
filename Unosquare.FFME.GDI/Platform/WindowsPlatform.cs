namespace Unosquare.FFME.Platform
{
    using Shared;
    using System;
    using System.Diagnostics;
    using Unosquare.FFME.GDI;

    /// <summary>
    /// Root for platform-specific implementations
    /// </summary>
    /// <seealso cref="IPlatform" />
    internal class WindowsPlatform : IPlatform
    {
        /// <summary>
        /// Initializes static members of the <see cref="WindowsPlatform"/> class.
        /// </summary>
        static WindowsPlatform()
        {
            Instance = new WindowsPlatform();
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="WindowsPlatform"/> class from being created.
        /// </summary>
        /// <exception cref="InvalidOperationException">Unable to get a valid GUI context.</exception>
        private WindowsPlatform()
        {
            NativeMethods = WindowsNativeMethods.Instance;
            IsInDesignTime = false;
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static WindowsPlatform Instance { get; }

        /// <inheritdoc />
        public INativeMethods NativeMethods { get; }

        /// <inheritdoc />
        public bool IsInDebugMode { get; } = Debugger.IsAttached;

        /// <inheritdoc />
        public bool IsInDesignTime { get; }

        /// <inheritdoc />
        public IMediaRenderer CreateRenderer(MediaType mediaType, MediaEngine mediaCore)
        {
            switch (mediaType)
            {
                case MediaType.Video:
                    return new VideoRenderer(mediaCore);
                default:
                    return new NullRenderer();
            }
        }

        /// <inheritdoc />
        public void HandleFFmpegLogMessage(MediaLogMessage message)
        {

        }
    }

    internal class NullRenderer : IMediaRenderer
    {
        public MediaEngine MediaCore => null;

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

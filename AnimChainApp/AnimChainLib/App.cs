namespace AnimChainLib
{
    public class AnimationChainApp
    {
        /// <summary>
        /// Instance variable represents the configuration settings for an animation.
        /// </summary>
        private AppConfigs configs;

        /// <summary>
        /// Instance variable represents the <see cref="AnimationChain"/> that will be used to render am animation.
        /// </summary>
        private AnimationChain chain;

        /// <summary>
        /// Constructor initializes a new instance of the <see cref="AnimChainApp"/> class with the specified AnimationChan and configuration settings.
        /// </summary>
        /// <param name="chain"></param>
        /// <param name="configs"></param>
        public AnimationChainApp(AnimationChain chain, AppConfigs configs)
        {
            this.chain = chain;
            this.configs = configs;
        }
    }

    /// <summary>
    /// Contains the configuration settings for the exported animation.
    /// </summary>
    public class AppConfigs
    {
        /// <summary>
        /// Instance variable represents the width and height of the exported animation.
        /// </summary>
        public readonly (int Width, int Height) Resolution;

        /// <summary>
        /// Instance variable represents the delay in milliseconds between frames of the exported animation.
        /// </summary>
        public readonly int FrameDelay;

        /// <summary>
        /// Instance variable represents the video frame rate of the exported animation.
        /// </summary>
        public readonly double FrameRate;

        /// <summary>
        /// Instance variable represents the file format of the exported animation.
        /// </summary>
        public readonly FileFormat Format;

        /// <summary>
        /// Constructor initializes a new instance of the <see cref="Configs"/> class from the specified resolution, delay, and format.
        /// </summary>
        /// <param name="resolution">The width and height of the exported animation.</param>
        /// <param name="frameDelay">The delay in milliseconds between frames of the exported animation.</param>
        /// <param name="format">The file format of the exported animation.</param>
        public AppConfigs((int Width, int Height) resolution, int frameDelay, FileFormat format)
        {
            Resolution = resolution;
            FrameDelay = frameDelay;
            Format = format;
        }

        /// <summary>
        /// Constructor initializes a new instance of the <see cref="Configs"/> class from the specified resolution, frame rate, and format.
        /// </summary>
        /// <param name="resolution">The width and height of the exported animation.</param>
        /// <param name="frameRate">The video frame rate of the exported animation.</param>
        /// <param name="format">The file format of the exported animation.</param>
        public AppConfigs((int Width, int Height) resolution, double frameRate, FileFormat format)
        {
            Resolution = resolution;
            FrameRate = frameRate;
            Format = format;
        }
    }

    /// <summary>
    /// Specifies a file type.
    /// </summary
    public enum FileFormat
    {
        GIF,
        MP4
    }
}

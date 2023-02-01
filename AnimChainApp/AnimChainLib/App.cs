using SkiaSharp;

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
        public AnimationChain Chain;

        /// <summary>
        /// Constructor initializes a new instance of the <see cref="AnimChainApp"/> class with the specified AnimationChan and configuration settings.
        /// </summary>
        /// <param name="chain">The AnimationChain that will be used to render the animation.</param>
        /// <param name="configs">The configuration settings that will be used to export the animation.</param>
        public AnimationChainApp(AnimationChain chain, AppConfigs configs)
        {
            Chain = chain;
            this.configs = configs;
        }

        public AnimationChainApp(SKBitmap image)
        {
            Chain = new AnimationChain(image);
            
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

        public AppConfigs() { } // HEY THIS IS WEIRD

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

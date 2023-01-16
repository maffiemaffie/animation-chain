namespace AnimChain
{
    public class AnimChainApp
    {
        private Configs configs;
        private AnimChain chain;

        private class Configs
        {
            public readonly (int Width, int Height) Resolution;
            public readonly int FrameDelay;
            public readonly double FrameRate;
            public readonly FileFormat Format;
        }

        private enum FileFormat
        {
            GIF,
            MP4
        }
    }

}

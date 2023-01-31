namespace AnimChainLib.Interpolators
{
    public class Constant : Interpolator
    {
        public Constant(int delay, int duration) : base(delay, duration)
        {
        }

        public override double Interpolate(int frame)
        {
            return frame < start ? 0 : 1;
        }
    }
}
namespace AnimChainLib.Transformers
{
    public class FlyIn : ITransformer<Point>
    {
        private readonly double direction;

        public Point Transform(Point value, double factor)
        {
            double _x = value.X;
            double _y = value.Y;

            _x += 2 * factor * Math.Cos(direction);

            return new Point(_x, _y);
        }
    }
}
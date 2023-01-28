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

    public class SplitIn : ITransformer<Point>
    {
        private readonly Direction direction;

        public Point Transform(Point value, double factor)
        {
            double _x = value.X;
            double _y = value.Y;

            if (direction == Direction.Horizontal)
            {
                if (value.X < 0) _x -= 1 - factor;
                else _x += 1 - factor;
            }
            else
            {
                if (value.Y < 0) _y -= 1 - factor;
                else _y += 1 - factor;
            }

            return new Point(_x, _y);
        }

        private enum Direction
        {
            Horizontal,
            Vertical
        }
    }

    public class Spin : ITransformer<Point>
    {
        private readonly double rotation;

        public Point Transform(Point value, double factor)
        {
            double _x = value.X;
            double _y = value.Y;

            double _rotation = rotation * factor;

            _x = _x * Math.Cos(_rotation) - _y * Math.Sin(_rotation);
            _y = _x * Math.Sin(_rotation) + _y * Math.Cos(_rotation);

            return new Point(_x, _y);
        }
    }

    public class Swivel : ITransformer<Point>
    {
        private readonly double rotation;
        private readonly double direction;
        public Point Transform(Point value, double factor)
        {
            double _x = value.X;
            double _y = value.Y;

            double _rotation = rotation * factor;

            double _ux = Math.Cos(direction);
            double _uy = Math.Sin(direction);

            double cosTheta = Math.Cos(_rotation);

            _x = _x * (cosTheta + _ux * _ux * (1 - cosTheta)) + _y * (_ux * _uy * (1 - cosTheta));
            _y = _x * (_uy * _ux * (1 - cosTheta)) + _y * (cosTheta + _uy * _uy * (1 - cosTheta));

            return new Point(_x, _y);
        }
    }

    public class Zoom : ITransformer<Point>
    {
        private readonly double from;
        private readonly double to;

        public Point Transform(Point value, double factor)
        {
            double zoom = from + factor * (to - from);

            return value * zoom;
        }
    }
}
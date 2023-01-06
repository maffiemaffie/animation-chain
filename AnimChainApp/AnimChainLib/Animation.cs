using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SkiaSharp;

namespace AnimChainLib
{
    public delegate T Animation<T>(T value, int frame);
    public interface IAnimatable<T>
    {
        void Animate(int frame, IAnimator<T> animator);
        void Animate(int frame, Animation<T> animation);
    }

    public interface IAnimator<T>
    {
        T Animate(T value, int frame);
    }

    public abstract class Animator<T> : IAnimator<T>
    {
        public abstract T Animate(T value, int frame);
    }

    public abstract class Animator : Animator<ImageMesh>
    {
        private ITransformer<Point> transformer;
        private Interpolator interpolator;

        public override ImageMesh Animate(ImageMesh value, int frame)
        {
            double factor = interpolator.Interpolate(frame);
            foreach (Point point in value.Points)
            {
                point.Transform(factor, transformer);
            }
            return value;
        }
    }
}

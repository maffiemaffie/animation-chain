using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SkiaSharp;

namespace AnimChainLib
{
    public delegate T Transformation<T>(T value, double factor);
    public interface ITransformable<T>
    {
        void Transform(double factor, ITransformer<T> transformer);
        void Transform(double factor, Transformation<T> transformation);
    }

    public interface ITransformer<T>
    {
        T Transform(T value, double factor);
    }
}

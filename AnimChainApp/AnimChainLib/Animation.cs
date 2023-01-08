using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SkiaSharp;

namespace AnimChainLib
{
    /// <summary>
    /// Represents the method that animates an <see cref="IAnimatable{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of object to animate.</typeparam>
    /// <param name="value">The object to be animated.</param>
    /// <param name="frame">The frame at which to apply the animation.</param>
    /// <returns>The animated object.</returns>
    public delegate T Animation<T>(T value, int frame);

    /// <summary>
    /// Defines a generalized animation method that a class implements to create a type-specific animation method for animating instances.
    /// </summary>
    /// <typeparam name="T">The type of the object to animate.</typeparam>
    public interface IAnimatable<T>
    {
        /// <summary>
        /// Method animates the current instance using the specified animation.
        /// </summary>
        /// <param name="frame">The frame at which to apply the animation.</param>
        /// <param name="animator">The <see cref="IAnimator{T}"/> implementation to use when animating objects.</param>
        void Animate(int frame, IAnimator<T> animator);

        /// <summary>
        /// Method animates the current instance using the specified animation.
        /// </summary>
        /// <param name="frame">The frame at which to apply the animation.</param>
        /// <param name="animation">The <see cref="Animation{T}"/> implementation to use when animating objects.</param>
        void Animate(int frame, Animation<T> animation);
    }

    /// <summary>
    /// Defines a method that a type implements to animate an object.
    /// </summary>
    /// <typeparam name="T">The type of the object to animate.</typeparam>
    public interface IAnimator<T>
    {
        /// <summary>
        /// Method animates a <see cref="IAnimatable{T}"/>.
        /// </summary>
        /// <param name="value">The object to be animated.</param>
        /// <param name="frame">The frame at which to apply the animation.</param>
        /// <returns>The animated object.</returns>
        T Animate(T value, int frame);
    }

    /// <summary>
    /// Defines a method that a type implements to animate a <see cref="ImageMesh"/>.
    /// </summary>
    /// <seealso cref="ImageMesh"/>
    /// <seealso cref="ITransformer{Point}"/>
    /// <seealso cref="Interpolator"/>
    public abstract class ImageMeshAnimator : IAnimator<ImageMesh>
    {
        /// <summary>
        /// Instance variable represents the <see cref="ITransformer{Point}"/> implementation to use when transforming<see cref="ImageMesh"/> objects.
        /// </summary>
        private readonly ITransformer<Point> transformer;

        /// <summary>
        /// Instance variable represents the <see cref="Interpolator"/> implementation to use when transforming <see cref="ImageMesh"/> objects.
        /// </summary>
        private readonly Interpolator interpolator;

        /// <summary>
        /// Constructor initializes a new instance of the <see cref="ImageMeshAnimator"/> class with the specified transformer and interpolator.
        /// </summary>
        /// <param name="transformer">The <see cref="ITransformer{Point}"/> implementation to use when transforming <see cref="ImageMesh"/> objects.</param>
        /// <param name="interpolator">The <see cref="Interpolator"/> implementation to use when transforming <see cref="ImageMesh"/> objects.</param>
        public ImageMeshAnimator(ITransformer<Point> transformer, Interpolator interpolator)
        {
            this.transformer = transformer;
            this.interpolator = interpolator;
        }

        /// <summary>
        /// Method animates an <see cref="ImageMesh"/>.
        /// </summary>
        /// <param name="value">The ImageMesh to be animated.</param>
        /// <param name="frame">The frame at which to apply the animation.</param>
        /// <returns>The animated ImageMesh.</returns>
        public ImageMesh Animate(ImageMesh value, int frame)
        {
            double factor = interpolator.Interpolate(frame); // convert frame to factor
            foreach (Point point in value.Points)
            {
                point.Transform(factor, transformer); // transform each point
            }
            return value;
        }
    }
}

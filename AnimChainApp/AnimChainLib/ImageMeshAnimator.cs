using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AnimChainLib
{
    /// <summary>
    /// Defines a method that a type implements to animate a <see cref="ImageMesh"/>.
    /// </summary>
    /// <seealso cref="ImageMesh"/>
    /// <seealso cref="ITransformer{Point}"/>
    /// <seealso cref="Interpolator"/>
    public class ImageMeshAnimator : IAnimator<ImageMesh>
    {
        /// <summary>
        /// Instance variable represents the <see cref="ITransformer{Point}"/> implementation to use when transforming<see cref="ImageMesh"/> objects.
        /// </summary>
        private readonly ITransformer<Point> transformer;

        /// <summary>
        /// Instance variable represents the <see cref="Interpolator"/> implementation to use when transforming <see cref="ImageMesh"/> objects.
        /// </summary>
        private readonly Interpolator interpolator;

        /// <value>
        /// Property represents the full duration to the end of this animation including delay time.
        /// </value>
        public int Duration { get { return interpolator.FullDuration; } }

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

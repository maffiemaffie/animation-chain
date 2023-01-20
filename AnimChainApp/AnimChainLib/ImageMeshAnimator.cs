using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

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
        /// Method returns a new instance of the <see cref="ImageMeshAnimator"/> class with the specified parameters. 
        /// </summary>
        /// <param name="transformerType">The <see cref="ITransformer{Point}"/> implementation that will be used by the new ImageMeshAnimator.</param>
        /// <param name="interpolatorType">The <see cref="Interpolator"/> implementation that will be used by the new ImageMeshAnimator.</param>
        /// <param name="configs">The JSON configs that will be used by the new ImageMeshAnimator.</param>
        /// <returns>The new ImageMeshAnimator.</returns>
        public static ImageMeshAnimator FromType(PointTransformerType transformerType, InterpolatorType interpolatorType, string configs)
        {
            return ImageMeshAnimatorFactory.GetAnimator(transformerType, interpolatorType, configs);
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

    /// <summary>
    /// Initializes a new instance of the <see cref="ImageMeshAnimator"/> class with the specified transform type, interpolator type, and configs.
    /// </summary>
    public class ImageMeshAnimatorFactory
    {
        /// <summary>
        /// Method initializes a new instance of the <see cref="ImageMeshAnimator"/> class with the specified transform type, interpolator type, and configs.
        /// </summary>
        /// <param name="transformerType">The <see cref="ITransformer{Point}"/> implementation that will be used by the new ImageMeshAnimator.</param>
        /// <param name="interpolatorType">The <see cref="Interpolator"/> implementation that will be used by the new ImageMeshAnimator.</param>
        /// <param name="configs">The JSON configs that will be used by the new ImageMeshAnimator.</param>
        /// <returns></returns>
        public static ImageMeshAnimator GetAnimator(PointTransformerType transformerType, InterpolatorType interpolatorType, string configs)
        {
            ITransformer<Point> transformer;
            Interpolator interpolator;

            JObject jConfigs = JObject.Parse(configs);

            string transformerConfigs = (string)jConfigs["animation"]["configs"];
            string interpolatorConfigs = (string)jConfigs["timing"]["configs"];

            JObject json = JObject.Parse(configs);
            transformerConfigs = (string)json[""]

            transformer = PointTransformerFactory.GetTransformer(transformerType, transformerConfigs);
            interpolator = InterpolatorFactory.GetInterpolator(interpolatorType, interpolatorConfigs);

            return new ImageMeshAnimator(transformer, interpolator);
        }
    }

    /// <summary>
    /// Initializes a new instance of the specified <see cref="ITransformer{Point}"/> implementation with the specified configs.
    /// </summary>
    public class PointTransformerFactory
    {
        /// <summary>
        /// Method initializes a new instance of the specified <see cref="ITransformer{Point}"/> implementation with the specified configs.
        /// </summary>
        /// <param name="type">The <see cref="ITransformer{Point}"/> implementation of the new transformer..</param>
        /// <param name="configs">The JSON configs that will be used by the new transformer.</param>
        /// <returns></returns>
        public static ITransformer<Point> GetTransformer(PointTransformerType type, string configs)
        {
            ITransformer<Point> transformer = null;

            switch (type)
            {
                case PointTransformerType./*TODO*/:
                    transformer = JsonConvert.DeserializeObject</*TODO*/>(configs);
                    break;
                    // etc
            }

            return transformer;
        }
    }

    /// <summary>
    /// Initializes a new instance of the specified <see cref="Interpolator"/> implementation with the specified configs.
    /// </summary>
    public static class InterpolatorFactory
    {
        /// <summary>
        /// Method initializes a new instance of the specified <see cref="Interpolator"/> implementation with the specified configs.
        /// </summary>
        /// <param name="type">The <see cref="Interpolator"/> implementation of the new interpolator.</param>
        /// <param name="configs">The JSON configs that will be used by the new interpolator.</param>
        /// <returns></returns>
        public static Interpolator GetInterpolator(InterpolatorType type, string configs)
        {
            Interpolator interpolator = null;

            switch (type)
            {
                case Interpolator./*TODO*/:
                    interpolator = JsonConvert.DeserializeObject</*TODO*/>(configs);
                    break;
                    // etc
            }

            return interpolator;
        }
    }

    /// <summary>
    /// Specifies a <see cref="ITransformer{Point}"/> implementation.
    /// </summary>
    public enum PointTransformerType
    {
        
    }

    /// <summary>
    /// Specifies a <see cref="Interpolator"/> implementation.
    /// </summary>
    public enum InterpolatorType
    {
        
    }
}

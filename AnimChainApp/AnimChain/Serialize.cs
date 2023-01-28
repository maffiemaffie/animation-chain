using AnimChainLib;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AnimChain
{
    /// <summary>
    /// Contains methods that take a JSON object and construct the appropriate animator.
    /// </summary>
    /// <seealso cref="ImageMeshAnimator"/>
    /// <seealso cref="ImageMeshAnimatorFactory"/>
    public class AnimationConverter : JsonConverter
    {
        /// <inheritdoc cref="JsonConverter.CanConvert(Type)"/>
        public override bool CanConvert(Type objectType)
        {
        return (objectType == typeof(ImageMeshAnimator));
        }

        /// <inheritdoc cref="JsonConverter.ReadJson(JsonReader, Type, object?, JsonSerializer)"/>
        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            JObject animation = JObject.Load(reader);
            string transformerType = (string)animation["animation"];
            string interpolatorType = (string)animation["timing"];
            JObject configs = (JObject)animation["configs"];

            return ImageMeshAnimatorFactory.GetAnimator(transformerType, interpolatorType, configs);
        }

        public override bool CanWrite { get { return false; } }

        /// <inheritdoc cref="JsonConverter.WriteJson(JsonWriter, object?, JsonSerializer)"/>
        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
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
        public static ImageMeshAnimator GetAnimator(PointTransformerType transformerType, InterpolatorType interpolatorType, JObject configs)
        {
            ITransformer<Point> transformer;
            Interpolator interpolator;

            string transformerConfigs = (string)configs["animation"]["configs"];
            string interpolatorConfigs = (string)configs["timing"]["configs"];

            transformer = PointTransformerFactory.GetTransformer(transformerType, transformerConfigs);
            interpolator = InterpolatorFactory.GetInterpolator(interpolatorType, interpolatorConfigs);

            return new ImageMeshAnimator(transformer, interpolator);
        }

        /// <inheritdoc cref="GetAnimator(PointTransformerType, InterpolatorType, JObject)"/>
        public static ImageMeshAnimator GetAnimator(string transformerType, string interpolatorType, JObject configs)
        {
            PointTransformerType _transformerType;
            InterpolatorType _interpolatorType;

            PointTransformerType.TryParse(transformerType, out _transformerType);
            InterpolatorType.TryParse(interpolatorType, out _interpolatorType);

            return GetAnimator(_transformerType, _interpolatorType, configs);
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

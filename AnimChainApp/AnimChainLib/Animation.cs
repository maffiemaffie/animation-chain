using Newtonsoft.Json;

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
}

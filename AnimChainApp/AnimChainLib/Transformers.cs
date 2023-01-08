using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SkiaSharp;

namespace AnimChainLib
{
    /// <summary>
    /// Represents the method that transforms a <see cref="ITransformable{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the object to transform.</typeparam>
    /// <param name="value">The object to be transformed.</param>
    /// <param name="factor">The amount by which to apply the transformation. 
    /// Expected to be in range [0, 1]; 0 applies no transformation, 1 applies the full transformation.
    /// Values outside the expected range are allowed, but may produce unexpected results.</param>
    /// <returns>The transformed object.</returns>
    public delegate T Transformation<T>(T value, double factor);

    /// <summary>
    /// Defines a generalized transformation method that a class implements to create a type-specific transformation method for transforming instances.
    /// </summary>
    /// <typeparam name="T">The type of the object to transform.</typeparam>
    public interface ITransformable<T>
    {
        /// <summary>
        /// Method transforms the current instance using the specified transformation.
        /// </summary>
        /// <param name="factor">The amount by which to apply the transformation. 
        /// Expected to be in range [0, 1]; 0 applies no transformation, 1 applies the full transformation.
        /// Values outside the expected range are allowed, but may produce unexpected results.</param>
        /// <param name="transformer">The <see cref="ITransformer{T}"/> implementation to use when transforming objects.</param>
        void Transform(double factor, ITransformer<T> transformer);

        /// <summary>
        /// Method transforms the current instance using the specified transformation.
        /// </summary>
        /// <param name="factor">The amount by which to apply the transformation.
        /// Expected to be in range [0, 1]; 0 applies no transformation, 1 applies the full transformation.
        /// Values outside the expected range are allowed, but may produce unexpected results.</param>
        /// <param name="transformation">The <see cref="Transformation{T}"/> implementation to use when transforming objects.</param>
        void Transform(double factor, Transformation<T> transformation);
    }

    /// <summary>
    /// Defines a method that a type implements to transform an object.
    /// </summary>
    /// <typeparam name="T">The type of the object to transform.</typeparam>
    public interface ITransformer<T>
    {
        /// <summary>
        /// Method transforms a <see cref="ITransformable{T}"/>.
        /// </summary>
        /// <param name="value">The object to be transformed.</param>
        /// <param name="factor">The amount by which to apply the transformation. 
        /// Expected to be in range [0, 1]; 0 applies no transformation, 1 applies the full transformation.
        /// Values outside the expected range are allowed, but may produce unexpected results.</param>
        /// <returns>The transformed object.</returns>
        T Transform(T value, double factor);
    }
}

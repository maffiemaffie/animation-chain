using System;
namespace AnimChainLib
{
	/// <summary>
	/// Rescales time to a factor accepted by the <see cref="ITransformer{T}"/>.
	/// <seealso cref="ImageMesh"/>
	/// <seealso cref="IAnimator{ImageMesh}"/>
	/// </summary>
	public abstract class Interpolator
	{
		/// <summary>
		/// Instance variable represents the lower bound.
		/// </summary>
		private int start;

		/// <summary>
		/// Instance variable represents the upper bound.
		/// </summary>
		private int end;

		/// <value>
		/// Property gets the duration between the start and end.
		/// </value>
		public int Duration { get { return end - start; } }

		/// <summary>
		/// Method rescales a specified value to an expected range of [0, 1].
		/// Values outside the expected range are allowed, but may produce unexpected results.
		/// </summary>
		/// <param name="frame">The frame value to be rescaled.</param>
		/// <returns>The scaled value.</returns>
		/// <seealso cref="ITransformer{T}"/>
		public abstract double Interpolate(int frame);

		/// <summary>
		/// Constructor initializes a new instance of the <see cref="Interpolator"/> class with the specified start and duration.
		/// </summary>
		/// <param name="start">The frame at which the new Interpolator will start from.</param>
		/// <param name="duration">The duration between start and end of the new Interpolator.</param>
		public Interpolator(int start, int duration)
        {
			this.start = start;
			this.end = start + duration;
        }

		/// <summary>
		/// Constructor initializes a new instance of the <see cref="Interpolator"/> class with the specified delay and duration.
		/// </summary>
		/// <param name="queueStart">The start frame of the <see cref="AnimationQueue"/> the new Interpolator is contained within.</param>
		/// <param name="delay">The delay to the beginning of the new Interpolator.</param>
		/// <param name="duration">The duration between start and end of the new Interpolator.</param>
		/// <seealso cref="AnimationQueue"/>
		public Interpolator(int queueStart, int delay, int duration) : this(queueStart + delay, duration) { }
	}
}


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
		/// Property represents the duration between the start and end.
		/// </value>
		public int Duration { get { return end - start; } }

		/// <value>
		/// Property represents the full duration to the end of this interpolator including delay time.
		/// </value>
		public int FullDuration { get { return end; } }

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
		/// <param name="delay">The frame at which the new Interpolator will start from, relative to its parent <see cref="AnimationQueue"/>.</param>
		/// <param name="duration">The duration between start and end of the new Interpolator.</param>
		/// <seealso cref="AnimationQueue"/>
		public Interpolator(int delay, int duration)
        {
			this.start = delay;
			this.end = start + duration;
        }
	}
}
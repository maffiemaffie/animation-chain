using System;
namespace AnimChainLib
{
	/// <summary>
	/// Abstract class <c>Interpolator</c> rescales time to a factor accepted
	/// by the transformer.
	/// </summary>
	public abstract class Interpolator
	{
		private int start;
		private int end;

		public int Duration { get { return end - start; } }

		public abstract double Interpolate(int frame);

		public Interpolator(int start, int duration)
        {
			this.start = start;
			this.end = start + duration;
        }
		public Interpolator(int queueStart, int delay, int duration) : this(queueStart + delay, duration);
	}
}


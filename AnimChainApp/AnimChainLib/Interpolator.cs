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

		public Interpolator(int start, int end)
		{
			this.start = start;
			this.end = end;
		}

		public Interpolator(int start, int duration): this()
	}
}


using SkiaSharp;

namespace AnimChainLib
{
	public class AnimationChain
	{
		/// <summary>
		/// Instance variable represents the <see cref="AnimationQueue"/> objects that will be used to
		/// perform a series of consecutive animations.
		/// </summary>
		private List<AnimationQueue> queues;

        /// <summary>
        /// Instance variable represents the image to be animated.
        /// </summary>
        private SKBitmap image;

        /// <summary>
        /// Instance variable represents the rendered and rasterized output of each frame in the full animation.
        /// </summary>
        private List<SKBitmap> rendered;

		/// <summary>
		/// Constructor initializes a new instance of the <see cref="AnimationChain"/> class from the specified image.
		/// </summary>
		/// <param name="image">The image to be animated by this AnimationChain.</param>
		public AnimationChain(SKBitmap image)
        {
			this.image = image;
			queues = new List<AnimationQueue>();
			rendered = new List<SKBitmap>();
        }

		/// <summary>
		/// Constructor initializes a new instance of the <see cref="AnimationChain"/> class from the specified image
		/// and animation queues. 
		/// </summary>
		/// <param name="image">The image to be animated by this AnimationChain.</param>
		/// <param name="queues">The animation queues to perform on <paramref name="image"/>.</param>
		public AnimationChain(SKBitmap image, List<AnimationQueue> queues) : this(image)
		{
			this.queues = queues;
		}

		public int Count { get { return queues.Count; } }

		/// <summary>
		/// Method adds a queue to the end of the chain. 
		/// </summary>
		/// <param name="queue">The queue that will be added to the chain.</param>
		/// <seealso cref="List{T}.Add(T)"/>
		public void Add(AnimationQueue queue)
        {
			queues.Add(queue);
        }

		/// <summary>
		/// Method renders and rasterizes the full animation to images.
		/// </summary>
		/// <returns>The rendered images.</returns>
		public List<SKBitmap> Render()
		{
			for (int i = 0; i < queues.Count; i++) // for each queue
			{
				for (int frame = GetQueueStart(i); frame < GetQueueEnd(i); frame++) // for each frame within the queue
				{
					SKBitmap renderedFrame = (SKBitmap)queues[i].Render(image, frame);
					rendered.Add(renderedFrame);
				}
			}
			return rendered;
		}

		/// <summary>
		/// Method returns the first global frame the specified <see cref="AnimationQueue"/> will be active on.
		/// </summary>
		/// <param name="queueIndex">The index of the specified AnimationQueue.</param>
		/// <returns>The first global frame the specified AnimationQueue will be active on.</returns>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="queueIndex"/> corresponds to a queue that doesn't exist.</exception>
		private int GetQueueStart(int queueIndex)
        {
			if (queueIndex < 0) throw new ArgumentOutOfRangeException(nameof(queueIndex), "Index must be greater than or equal to 0.");
			if (queueIndex >= queues.Count) throw new ArgumentOutOfRangeException(nameof(queueIndex), $"Index out of range (there are only {queues.Count} queues.");

			int start = 0;
			for (int i = 0; i < queueIndex; i++)
				start += queues[i].Duration;
			return start;
        }

		/// <summary>
		/// Method returns the last global frame the specified <see cref="AnimationQueue"/> will be active on.
		/// </summary>
		/// <param name="queueIndex">The index of the specified AnimationQueue.</param>
		/// <returns>The first global frame the specified AnimationQueue will be active on.</returns>
		/// <exception cref="ArgumentOutOfRangeException"><paramref name="queueIndex"/> corresponds to a queue that doesn't exist.</exception>
		private int GetQueueEnd(int queueIndex)
        {
			if (queueIndex < 0) throw new ArgumentOutOfRangeException(nameof(queueIndex), "Index must be greater than or equal to 0.");
			if (queueIndex >= queues.Count) throw new ArgumentOutOfRangeException(nameof(queueIndex), $"Index out of range (there are only {queues.Count} queues.");

			int end = 0;
			for (int i = 0; i <= queueIndex; i++)
				end += queues[i].Duration;
			return end;
		}
	}

	public class AnimationQueue
	{
		/// <summary>
		/// Instance varialbe contains the <see cref="ImageMeshAnimator"/> implementations that
		/// will be used to perform the animation.
		/// </summary>
		private List<ImageMeshAnimator> animations;

		/// <value>
		/// Property represents the duration in frames until the last animation is complete.
		/// </value>
		public int Duration
        {
			get
            {
				int end = 0;
				for (int i = 0; i < animations.Count; i++)
					if (animations[i].Duration > end) end = animations[i].Duration;
				return end;
            }
        }

		/// <summary>
		/// Constructor initializes a new instance of the <see cref="AnimationQueue"/> class.
		/// </summary>
		public AnimationQueue()
        {
			animations = new List<ImageMeshAnimator>();
        }

        /// <summary>
        /// Constructor initializes a new instance of the <see cref="AnimationQueue"/> class
        /// with the specified animations.
        /// </summary>
        /// <param name="animations">The animations to perform on an <see cref="ImageMesh"/>.</param>
		/// <seealso cref="IAnimator{T}"/>
		/// <seealso cref="IAnimatable{T}"/>
        public AnimationQueue(List<ImageMeshAnimator> animations)
		{
			this.animations = animations;
		}

		/// <summary>
		/// Method adds an animation to the end of the queue.
		/// </summary>
		/// <param name="animation">The animation that will be added to the end of the queue.</param>
		/// <seealso cref="List{T}.Add(T)"/>
		public void Add(ImageMeshAnimator animation)
        {
			animations.Add(animation);
        }

		/// <summary>
		/// Animates an <see cref="ImageMesh"/> through the local queue.
		/// </summary>
		/// <param name="mesh">The mesh to be animated by this AnimationQueue.</param>
		/// <param name="frame">The frame at which to perform the animation.</param>
		/// <returns></returns>
		public ImageMesh Render(ImageMesh mesh, int frame)
		{
			ImageMesh rendered = mesh;
			foreach (ImageMeshAnimator animation in animations)
			{
				rendered = animation.Animate(rendered, frame);
			}

			return rendered;
		}

		/// <summary>
		/// Animates an image through the local queue.
		/// </summary>
		/// <param name="image">The image to be animated by this AnimationQueue.</param>
		/// <param name="frame">The frame at which to perform the animation.</param>
		/// <returns></returns>
		public ImageMesh Render(SKBitmap image, int frame)
		{
			return Render(new ImageMesh(image), frame);
		}
	}
}


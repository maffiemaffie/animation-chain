using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		/// Constructor initializes a new instance of the <see cref="AnimationChain"/> class from the specified image
		/// and animation queues. 
		/// </summary>
		/// <param name="image">The image to be animated by this AnimationQueue.</param>
		/// <param name="queues">The animation queues to perform on <paramref name="image"/>.</param>
		public AnimationChain(SKBitmap image, List<AnimationQueue> queues)
		{
			this.image = image;
			this.queues = queues;
			this.rendered = new List<SKBitmap>();
		}

		public List<SKBitmap> Render()
		{
			int frame = 0;

			foreach (AnimationQueue queue in queues)
			{
				List<SKBitmap> rasterizedFrames = queue.Render(image, frame++).Cast<SKBitmap>()

                this.rendered.AddRange()
			}

			return rendered;
		}
	}

	public class AnimationQueue
	{
		/// <summary>
		/// Instance varialbe contains the <see cref="ImageMeshAnimator"/> implementations that
		/// will be used to perform the animation.
		/// </summary>
		private List<ImageMeshAnimator> animations;

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


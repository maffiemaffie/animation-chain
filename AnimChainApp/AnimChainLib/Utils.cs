using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SkiaSharp;

namespace AnimChainLib
{
    public static class Util
    {
        /// <summary>
        /// Method decodes a base-64 encoded image to a <see cref="SKBitmap"/> object.
        /// </summary>
        /// <param name="base64EncodedData">The base-64 string representation of the image to be decoded.</param>
        /// <returns>The decoded image.</returns>
        /// <exception cref="MalformedImageException"></exception>
        public static SKBitmap Base64ToImage(string base64EncodedData)
        {
            if (base64EncodedData == null) throw new MalformedImageException("Image is null");
            MemoryStream imageStream = new MemoryStream(Encoding.UTF8.GetBytes(base64EncodedData));

            SKBitmap image = SKBitmap.Decode(imageStream); // create image from data

            return image;
        }
    }
}

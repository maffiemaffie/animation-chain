using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SkiaSharp;

namespace AnimChainLib
{
    /// <summary>
    /// Exception returned upon receiving a malformed image. 
    /// </summary>
    public class MalformedImageException : Exception
    {
        public MalformedImageException() : base() { }
        public MalformedImageException(string message) : base(message) { }
    }

    //public class Image : IEnumerable
    //{
    //    private int width;
    //    private int height;
    //    private int[,] data;

    //    public int this[int i]
    //    {
    //        get { return data[i % width, i / width]; }
    //        set { data[i % width, i / width] = value; }
    //    }
    //    public int this[int x, int y]
    //    {
    //        get { return data[x, y]; }
    //        set { data[x, y] = value; }
    //    }

    //    public IEnumerator GetEnumerator()
    //    {
    //        return data.GetEnumerator();
    //    }
    //    public static bool operator ==(Image i1, Image i2)
    //    {
    //        if (ReferenceEquals(null, i1)) return false; // is null
    //        if (ReferenceEquals(null, i2)) return false; // is null
    //        if ((i1.width != i2.width) || (i1.height != i2.height)) return false; // wrong size
    //        for (int i = 0; i < i1.width * i1.height; i++)
    //        {
    //            if (i2[i] != i2[i]) return false; // mismatch
    //        }
    //        return true;
    //    }
    //    public static bool operator !=(Image i1, Image i2)
    //    {
    //        if (ReferenceEquals(null, i1)) return true; // is null
    //        if (ReferenceEquals(null, i2)) return true; // is null
    //        if ((i1.width != i2.width) || (i1.height != i2.height)) return true; // wrong size
    //        for (int i = 0; i < i1.width * i1.height; i++)
    //        {
    //            if (i2[i] != i2[i]) return true; // mismatch
    //        }
    //        return false;
    //    }
    //}

    //public struct ColorPoint
    //{
    //    private Point point;
    //    public Point Point 
    //    { 
    //        get { return point; } 
    //        set { point = value; } 
    //    }
    //    public double X 
    //    { 
    //        get { return point.X; } 
    //        set { point.X = value; }
    //    }
    //    public double Y 
    //    { 
    //        get { return point.Y; }
    //        set { point.Y = value; }
    //    }
    //    public SKColor Color;

    //    public byte A { 
    //        get { return Color.Alpha; }
    //        set { Color = new SKColor(value, R, G, B); }
    //    }
    //    public byte R
    //    {
    //        get { return Color.Red; }
    //        set { Color = new SKColor(A, value, G, B); }
    //    }
    //    public byte G
    //    {
    //        get { return Color.Green; }
    //        set { Color = new SKColor(A, R, value, B); }
    //    }
    //    public byte B
    //    {
    //        get { return Color.Blue; }
    //        set { Color = new SKColor(A, R, G, value); }
    //    }
    //    public byte Alpha
    //    {
    //        get { return A; }
    //        set { A = value; }
    //    }
    //    public byte Red
    //    {
    //        get { return R; }
    //        set { R = value; }
    //    }
    //    public byte Green
    //    {
    //        get { return G; }
    //        set { G = value; }
    //    }
    //    public byte Blue
    //    {
    //        get { return B; }
    //        set { B = value; }
    //    }

    //    public ColorPoint(Point point, SKColor color)
    //    {
    //        this.point = point;
    //        this.Color = color;
    //    }
    //    public ColorPoint(ColorPoint point) : this(point.point, point.Color) { }
    //    public ColorPoint(Point point, (byte r, byte g, byte b) color) : this(point, new SKColor(color.r, color.g, color.b)) { }
    //    public ColorPoint(Point point, (byte a, byte r, byte g, byte b) color) : this(point, new SKColor(color.a, color.r, color.g, color.b)) { }

    //    public static ColorPoint operator +(ColorPoint p1, Point p2)
    //    {
    //        return new ColorPoint(p1.Point + p2, p1.Color);
    //    }
    //    public static ColorPoint operator +(ColorPoint p1, (double x, double y) p2) {
    //        return new ColorPoint(p1.Point + p2, p1.Color);
    //    }
    //    public static ColorPoint operator -(ColorPoint p1, Point p2)
    //    {
    //        return new ColorPoint(p1.Point - p2, p1.Color);
    //    }
    //    public static ColorPoint operator -(ColorPoint p1, (double x, double y) p2)
    //    {
    //        return new ColorPoint(p1.Point - p2, p1.Color);
    //    }
    //    public static ColorPoint operator *(ColorPoint p1, Point p2)
    //    {
    //        return new ColorPoint(p1.point * p2, p1.Color);
    //    }
    //    public static ColorPoint operator *(ColorPoint p1, double scale)
    //    {
    //        return new ColorPoint(p1.point * scale, p1.Color);
    //    }
    //    public static ColorPoint operator /(ColorPoint p1, Point p2)
    //    {
    //        return new ColorPoint(p1.point / p2, p1.Color);
    //    }
    //    public static ColorPoint operator /(ColorPoint p1, double scale)
    //    {
    //        return new ColorPoint(p1.point / scale, p1.Color);
    //    }
    //}
    /// <summary>
    /// Represents a two-dimensional point. Provides methods to add and scale Points using vector addition and scalar multiplication.
    /// </summary>
    public struct Point : ITransformable<Point>
    {
        /// <summary>
        /// Instance variable represents the point's x-coordinate.
        /// </summary>
        public double X;

        /// <summary>
        /// Instance variable represents the point's y-coordinate.
        /// </summary>
        public double Y;

        /// <summary>
        /// Operator implicitly casts a <c>Tuple</c> of the form <c>(Double X, Double Y)</c> to a <c>Point</c>.
        /// </summary>
        /// <param name="point">The point to be casted.</param>
        public static implicit operator Point((double x, double y) point) => new(point);
        
        /// <summary>
        /// Operator implicitly converts a <c>Point</c> to a <c>Tuple</c> of the form <c>(Double X, Double Y).</c>
        /// </summary>
        /// <param name="point">The point to be casted.</param>
        public static implicit operator (double, double)(Point point) => (point.X, point.Y);
        
        /// <summary>
        /// Constructor initializes a new instance of the <see cref="Point"/> class that contains coordinates 
        /// (<paramref name="x"/>, <paramref name="y"/>).
        /// </summary>
        /// <param name="x">The new Point's x-coordinate.</param>
        /// <param name="y">The new Point's y-coordinate.</param>
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// Constructor initializes a new instance of the <see cref="Point"/> class that contains coordinates represented by the specified <see cref="Tuple{Double, Double}"/>.
        /// </summary>
        /// <param name="point">The x and y coordinates of the new Point.</param>
        public Point((double x, double y) point) : this(point.x, point.y) { }

        /// <summary>
        /// Constructor initializes a new instance of the <see cref="Point"/>class that contains coordinates copied from the specified <see cref="Point"/>.
        /// </summary>
        /// <param name="point">The Point whose coordinates are copied to the new Point.</param>
        public Point(Point point) : this(point.X, point.Y) { }

        /// <summary>
        /// Operator adds two Points using vector addition.
        /// </summary>
        /// <param name="p1">The first Point to be added.</param>
        /// <param name="p2">The second Point to be added.</param>
        /// <returns>The sum of the two Points.</returns>
        /// <seealso cref="operator+"/>
        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }
        
        /// <summary>
        /// Operator subtracts one Point from another using vector addition.
        /// </summary>
        /// <param name="p1">The minuend.</param>
        /// <param name="p2">The subtrahend.</param>
        /// <returns>The difference between the two Points.</returns>
        /// <seealso cref="operator-"/>
        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }

        /// <summary>
        /// Operator multiplies two Points by multiplying corresponding terms.
        /// </summary>
        /// <param name="p1">The first Point to be multiplied.</param>
        /// <param name="p2">The second Point to be multiplied.</param>
        /// <returns>The product of the two Points.</returns>
        /// <seealso cref="operator*"/>
        public static Point operator *(Point p1, Point p2)
        {
            return new Point(p1.X * p2.X, p1.Y * p2.Y);
        }

        /// <summary>
        /// Operator scales <paramref name="p1"/> by <paramref name="scale"/> using scalar multiplication.
        /// </summary>
        /// <param name="p1">The Point to be scaled.</param>
        /// <param name="scale">The scalar for <paramref name="p1"/> to be multiplied by.</param>
        /// <returns>The scaled Point.</returns>
        /// <seealso cref="operator*"/>
        public static Point operator *(Point p1, double scale)
        {
            return new Point(p1.X * scale, p1.Y * scale);
        }
        
        /// <summary>
        /// Operator divides one Point from another by dividing corresponding terms.
        /// </summary>
        /// <param name="p1">The dividend.</param>
        /// <param name="p2">The divisor.</param>
        /// <returns>The quotient of the two Points.</returns>
        /// <seealso cref="operator/"/>
        public static Point operator /(Point p1, Point p2)
        {
            return new Point(p1.X / p2.X, p1.Y / p2.Y);
        }

        /// <summary>
        /// Operator scales <paramref name="p1"/> by the multiplicative inverse of <paramref name="scale"/> using scalar multiplication.
        /// </summary>
        /// <param name="p1">The Point to be scaled.</param>
        /// <param name="scale">The scalar for <paramref name="p1"/> to be divided by.</param>
        /// <returns>The scaled Point.</returns>
        /// <seealso cref="operator/"/>
        public static Point operator /(Point p1, double scale)
        {
            return new Point(p1.X / scale, p1.Y / scale);
        }

        /// <summary>
        /// Method transforms the Point's coodinates using the specified transformer.
        /// </summary>
        /// <param name="factor">The amount by which to apply the transformation. 
        /// Expected to be in range [0, 1]; 0 applies no transformation, 1 applies the full transformation.
        /// Values outside the expected range are allowed, but may produce unexpected results.</param>
        /// <param name="transformer">The <see cref="ITransformer{Point}"/> implementation to use when transforming Points.</param>
        /// <seealso cref="ITransformable{T}"/>
        public void Transform(double factor, ITransformer<Point> transformer)
        {
            Point transformed = transformer.Transform(this, factor);
            X = transformed.X;
            Y = transformed.Y;
        }

        /// <summary>
        /// Method transforms the Point's coordinates using the specified transformation.
        /// </summary>
        /// <param name="factor">The amount by which to apply the transformation.
        /// Expected to be in range [0, 1]; 0 applies no transformation, 1 applies the full transformation.
        /// Values outside the expected range are allowed, but may produce unexpected results.</param>
        /// <param name="transformation">The <see cref="Transformation{Point}"/> implementation to use when transforming Points.</param>
        /// <seealso cref="ITransformable{T}"/>
        public void Transform(double factor, Transformation<Point> transformation)
        {
            Point transformed = transformation(this, factor);
            X = transformed.X;
            Y = transformed.Y;
        }
    }

    /// <summary>
    /// Represents a two-dimensional grid of <see cref="Point"/> elements.
    /// </summary>
    public class PointCollection : IEnumerable, ICollection, ICloneable
    {
        /// <summary>
        /// Instance variable contains the <see cref="Point"/> elements represented by this instance.
        /// </summary>
        private Point[,] data;

        /// <value>
        /// Property gets the <see cref="Point"/> elements represented by this instance.
        /// </value>
        public Point[,] Data
        {
            get { return data; }
        }

        /// <value>
        /// Property gets and sets the <see cref="Point"/> at index <paramref name="i"/>.
        /// </value>
        /// <param name="i">The one-dimensional index of the specified <see cref="Point"/>.</param>
        /// <returns>The specified <see cref="Point"/>.</returns>
        public Point this[int i]
        {
            get { return data[i % Width, i / Width]; }
            set { data[i % Width, i / Width] = value; }
        }
        /// <value>
        /// Property gets and sets the <see cref="Point"/> at index <paramref name="i"/>.
        /// </value>
        /// <param name="x">The x grid coordinate of the specified <see cref="Point"/>.</param>
        /// <param name="y">The y grid coordinate of the specified <see cref="Point"/>.</param>
        /// <returns>The specified <see cref="Point"/>.</returns>
        public Point this[int x, int y]
        {
            get { return data[x, y]; }
            set { data[x, y] = value; }
        }

        /// <value>
        /// Property gets the total number of <see cref="Point"/> elements represented by this instance.
        /// </value>
        /// <seealso cref="ICollection.Count"/>
        public int Count { get { return data.Length; } }

        /// <summary>
        /// Instance variable represents the width of the grid.
        /// </summary>
        private int width;

        /// <summary>
        /// Instance variable represents the height of the grid.
        /// </summary>
        private int height;

        /// <value>
        /// Property gets the number of rows in the grid.
        /// </value>
        public int Width { get { return width; } }

        /// <value>
        /// Property gets the number of columns in the grid.
        /// </value>
        public int Height { get { return height; } }

        /// <inheritdoc cref="ICollection.IsSynchronized"/>
        public bool IsSynchronized => data.IsSynchronized;

        /// <inheritdoc cref="ICollection.SyncRoot"/>
        public object SyncRoot => data.SyncRoot;

        /// <summary>
        /// Constructor initializes a new instance of the <see cref="PointCollection"/> class that is empty and has the specified capacity.
        /// </summary>
        /// <param name="width">The number of columns in the new PointCollection.</param>
        /// <param name="height">The number of rows in the new PointCollection.</param>
        public PointCollection(int width, int height)
        {
            this.width = width;
            this.height = height;

            data = new Point[Width, Height];
        }

        /// <summary>
        /// Constructor initializes a new instance of the <see cref="PointCollection"/> class 
        /// that contains elements copied from the specified collection and has the same width and height. 
        /// </summary>
        /// <param name="data">The collection whose Points are copied to the new PointCollection.</param>
        public PointCollection(Point[,] data)
        {
            width = data.GetLength(0);
            height = data.GetLength(1);

            this.data = new Point[Width, Height];
            for (int i = 0; i < data.Length; i++)
            {
                this[i] = new Point(data[i % Width, i / Width]);
            }
        }

        /// <summary>
        /// Constructor initializes a new instance of the <see cref="PointCollection"/> class
        /// that contains elements copied from the specified <see cref="PointCollection"/> and has the same width and height.
        /// </summary>
        /// <param name="pointCollection">The PointCollection whose Points are copied to the new PointCollection.</param>
        public PointCollection(PointCollection pointCollection) : this(pointCollection.Data) { }
        
        public IEnumerator GetEnumerator()
        {
            return data.GetEnumerator();
        }

        /// <inheritdoc cref="ICollection.CopyTo(Array, int)"/>
        public void CopyTo(Array array, int index)
        {
            data.CopyTo(array, index);
        }

        /// <inheritdoc cref="ICloneable.Clone"/>
        public PointCollection Clone()
        {
            return new PointCollection(this);
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
    }

    public struct Pixel
    {
        /// <summary>
        /// Instance variable contains the <see cref="Point"/> at each of the four corners of the Pixel.
        /// </summary>
        public Point[] Corners;
        /// <summary>
        /// Instance variable represents the <see cref="SKColor">color</see> of the Pixel.
        /// </summary>
        public SKColor Color;

        /// <summary>
        /// Constructor initializes a new instance of the <see cref="Pixel"/> class that contains the specified points and color.
        /// </summary>
        /// <param name="corners">An array containing the four corners.</param>
        /// <param name="color">The color of the pixel.</param>
        public Pixel(Point[] corners, SKColor color)
        {
            Corners = corners;
            Color = color;
        }

        /// <summary>
        /// Constructor initializes a new instance of the <see cref="Pixel"/> class that contains the points and color copied from the specified <see cref="Pixel"/>.
        /// </summary>
        /// <param name="pixel">The Pixel whose data is copied to the new Pixel.</param>
        public Pixel(Pixel pixel) : this(pixel.Corners, pixel.Color) { }
    }

    /// <summary>
    /// Represents an image using a mesh of quad pixels that can be distorted using an <see cref="ImageMeshAnimator{T}"/>
    /// <seealso cref="IAnimatable{T}"/>
    /// <seealso cref="ImageMeshAnimator"/>
    /// </summary>
    /// <remarks>
    /// Mesh points are scaled to [-1, 1] on both axes.
    /// </remarks>
    public class ImageMesh : IEnumerable,ICloneable,IAnimatable<ImageMesh>
    {
        /// <summary>
        /// Instance variable represents the <see cref="Point"/> elements of the gridlines that surround the pixels represented by this instance.
        /// </summary>
        public PointCollection Points;
        
        /// <summary>
        /// Instance variable represents the <see cref="SKColor"/> of each of the pixels in the image represented by this instance.
        /// <seealso cref="SkiaSharp"/>
        /// </summary>
        public SKColor[] Pixels;

        /// <summary>
        /// Instance variable represents the horizontal resolution of the image represented by this instance.
        /// </summary>
        private int width;

        /// <summary>
        /// Instance variable represents the vertical resolution of the image represented by this instance.
        /// </summary>
        private int height;

        /// <value>
        /// Property represents the horizontal resolution of the image represented by this instance.
        /// </value>
        public int Width { get { return width; } }

        /// <value>
        /// Property represents the vertical resolution of the image represented by this instance.
        /// </value>
        public int Height { get { return height; } }

        /// <value>
        /// Property gets the total number of pixels represented by this instance.
        /// <seealso cref="ICollection.Count"/>
        /// </value>
        public int Count { get { return Pixels.Length; } }

        /// <value>
        /// Property gets the <see cref="Pixel"/> at the specified index.
        /// </value>
        /// <param name="i">The one-dimensional index of the specified pixel.</param>
        /// <returns>A <see cref="Pixel"/> representing the specified pixel.</returns>
        /// <seealso cref="PointCollection"/>
        /// <seealso cref="SkiaSharp"/>
        public Pixel this[int i]
        {
            get
            {
                return this[i % Width, i / Width];
            }
        }

        /// <value>
        /// Property gets the <see cref="Pixel"/> at the specified coordinates.
        /// </value>
        /// <param name="x">The column of the specified pixel.</param>
        /// <param name="y">The row coordinate of the specified pixel.</param>
        /// <returns>A <see cref="Pixel"/> representing the specified pixel.</returns>
        /// <seealso cref="PointCollection"/>
        /// <seealso cref="SkiaSharp"/>
        public Pixel this[int x, int y]
        {
            get
            {
                Point[] corners = new Point[4];
                corners[0] = Points[x    , y    ];
                corners[1] = Points[x + 1, y    ];
                corners[2] = Points[x    , y + 1];
                corners[3] = Points[x + 1, y + 1];
                return new Pixel(corners, GetPixel(x, y));
            }
        }

        /// <summary>
        /// Method returns the color of the specified pixel.
        /// </summary>
        /// <param name="i">The one-dimensional index of the specified pixel.</param>
        /// <returns>The color of the specified pixel.</returns>
        /// <seealso cref="SkiaSharp"/>
        public SKColor GetPixel(int i)
        {
            return Pixels[i];
        }

        /// <summary>
        /// Method returns the color of the specified pixel.
        /// </summary>
        /// <param name="x">The column of the specified pixel.</param>
        /// <param name="y">The row of the specified pixel.</param>
        /// <returns>The color of the specified pixel.</returns>
        /// <seealso cref="SkiaSharp"/>
        public SKColor GetPixel(int x, int y)
        {
            return Pixels[x + Width * y];
        }

        /// <summary>
        /// Constructor initializes a new instance of the <see cref="ImageMesh"/> class constructed from an image.
        /// </summary>
        /// <param name="image">The image from which the new ImageMesh will be constructed from.</param>
        /// <seealso cref="SKBitmap"/>
        public ImageMesh(SKBitmap image)
        {
            SKColor[] pixels = image.Pixels;

            width = image.Width;
            height = image.Height;

            Points = new PointCollection(Width + 1, Height + 1);

            Pixels = new SKColor[pixels.Length];
            for (int i = 0; i < pixels.Length; i++)
            {
                Pixels[i] = pixels[i]; // copy over pixels
            }

            for (int y = 0; y <= Height; y++)
            {
                for (int x = 0; x <= Width; x++)
                {
                    double scaledX = 2.0 * x / Width - 1;
                    double scaledY = 2.0 * y / Height - 1;
                    Points[x, y] = new Point(scaledX, scaledY); // initialize mesh points
                }
            }
        }

        /// <summary>
        /// Constructor initializes a new instance of the <see cref="ImageMesh"/> class constructed from image data.
        /// </summary>
        /// <param name="base64EncodedData">A base-64 string on image data.</param>
        /// <exception cref="MalformedImageException"><paramref name="base64EncodedData"/> is <see langword="null"/>.</exception>
        public ImageMesh(string base64EncodedData) : this(base64ToImage(base64EncodedData)) { }

        private static SKBitmap base64ToImage(string base64EncodedData)
        {
            if (base64EncodedData == null) throw new MalformedImageException("Image is null");
            MemoryStream imageStream = new MemoryStream(Encoding.UTF8.GetBytes(base64EncodedData));

            SKBitmap image = SKBitmap.Decode(imageStream); // create image from data

            return image;
        }

        /// <summary>
        /// Constructor intializes a new instance of the <see cref="ImageMesh"/> class with the specified points and pixels.
        /// </summary>
        /// <param name="points">The points of the new ImageMesh.</param>
        /// <param name="pixels">The pixels of the new ImageMesh.</param>
        /// <remarks>This method is used primarily as part of the <see cref=Clone"/> method.</remarks>
        /// <seealso cref="Clone"/>
        private ImageMesh(PointCollection points, SKColor[] pixels)
        {
            Points = points;
            Pixels = pixels;
            this.width = points.Width - 1;
            this.height = points.Height - 1;
        }

        /// <summary>
        /// Operator explicitly casts ImageMesh to an <see cref="SKBitmap"/> object through rasterization.
        /// </summary>
        /// <param name="m">The ImageMesh to be cast</param>
        /// <seealso cref="SKBitmap"/>
        public static explicit operator SKBitmap(ImageMesh m) => m.ToRaster();

        /// <summary>
        /// Method renders the current MeshImage back into a raster image.
        /// </summary>
        /// <returns>A raster image representing the current MeshImage, bounded to it's original dimensions.</returns>
        /// <seealso cref="SkiaSharp"/>
        public SKBitmap ToRaster()
        {
            SKSurface surface = SKSurface.Create(new SKImageInfo(Width, Height));
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            foreach (Pixel quad in this)
            {
                SKPoint[] vertices = quad.Corners.Cast<SKPoint>().ToArray();
                canvas.DrawVertices(SKVertexMode.Triangles, vertices, null, new SKPaint { Color = quad.Color }); // draw pixels to the canvas
            }
            
            SKImage snap = surface.Snapshot(new SKRectI(-1, -1, 1, 1)); // save a bounded raster to the canvas
            return SKBitmap.FromImage(snap);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) GetEnumerator();
        }

        /// <inheritdoc cref="IEnumerable.GetEnumerator"/>
        public ImageMeshEnum GetEnumerator()
        {
            return new ImageMeshEnum(this);
        }

        /// <inheritdoc cref="Animate(int, IAnimator{ImageMesh})"/>
        public void Animate(int frame, IAnimator<ImageMesh> animator)
        {
            ImageMesh animated = animator.Animate(Clone(), frame);
            Points = animated.Points;
            Pixels = animated.Pixels;
        }

        /// <inheritdoc cref="Animate(int, Animation{ImageMesh})"/>
        public void Animate(int frame, Animation<ImageMesh> animation)
        {
            ImageMesh animated = animation(Clone(), frame);
            Points = animated.Points;
            Pixels = animated.Pixels;
        }

        /// <inheritdoc cref="ICloneable.Clone"/>
        public ImageMesh Clone()
        {
            PointCollection _points = Points.Clone();
            SKColor[] _pixels = (SKColor[])Pixels.Clone();

            return new ImageMesh(_points, _pixels);
        }

        object ICloneable.Clone()
        {
            return Clone();
        }
    }

    /// <inheritdoc cref="IEnumerator"/>
    /// <seealso cref="ImageMesh"/>
    public class ImageMeshEnum : IEnumerator
    {

        public ImageMesh _mesh;
        int position = -1;

        public ImageMeshEnum(ImageMesh mesh)
        {
            _mesh = mesh;
        }

        object IEnumerator.Current { get { return Current; } }

        /// <inheritdoc cref="IEnumerator.Current"/>
        public Pixel Current
        {
            get
            {
                try
                {
                    return _mesh[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }

        /// <inheritdoc cref="IEnumerator.MoveNext"/>
        public bool MoveNext()
        {
            position++;
            return (position < _mesh.Count);
        }

        /// <inheritdoc cref="IEnumerator.Reset"/>
        public void Reset()
        {
            position = -1;
        }
    }
}
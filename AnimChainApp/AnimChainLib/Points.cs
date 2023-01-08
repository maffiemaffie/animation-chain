using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SkiaSharp;

namespace AnimChainLib
{
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

    public struct Point : ITransformable<Point>
    {
        public double X;
        public double Y;

        public static implicit operator Point((double x, double y) point) => new Point(point);
        public static implicit operator (double, double)(Point point) => (point.X, point.Y);
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
        public Point((double x, double y) point) : this(point.x, point.y) { }
        public Point(Point point) : this(point.X, point.Y) { }

        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }
        public static Point operator +(Point p1, (double X, double Y) p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }
        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }
        public static Point operator -(Point p1, (double X, double Y) p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }
        public static Point operator *(Point p1, Point p2)
        {
            return new Point(p1.X * p2.X, p1.Y * p2.Y);
        }
        public static Point operator *(Point p1, double scale)
        {
            return new Point(p1.X * scale, p1.Y * scale);
        }
        public static Point operator /(Point p1, Point p2)
        {
            return new Point(p1.X / p2.X, p1.Y / p2.Y);
        }
        public static Point operator /(Point p1, double scale)
        {
            return new Point(p1.X / scale, p1.Y / scale);
        }

        public void Transform(double factor, ITransformer<Point> transformer)
        {
            Point transformed = transformer.Transform(this, factor);
            X = transformed.X;
            Y = transformed.Y;
        }
        public void Transform(double factor, Transformation<Point> transformation)
        {
            Point transformed = transformation(this, factor);
            X = transformed.X;
            Y = transformed.Y;
        }
    }

    public class PointCollection : IEnumerable, ICollection, ICloneable
    {
        private Point[,] data;
        public Point[,] Data
        {
            get { return data; }
        }
        public Point this[int i]
        {
            get { return data[i % Width, i / Width]; }
            set { data[i % Width, i / Width] = value; }
        }
        public Point this[int x, int y]
        {
            get { return data[x, y]; }
            set { data[x, y] = value; }
        }
        public int Count { get { return data.Length; } }

        private int width;
        private int height;

        public int Width { get { return width; } }
        public int Height { get { return height; } }

        public bool IsSynchronized => data.IsSynchronized;
        public object SyncRoot => data.SyncRoot;

        public PointCollection(int width, int height)
        {
            this.width = width;
            this.height = height;

            data = new Point[Width, Height];
        }
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
        
        public IEnumerator GetEnumerator()
        {
            return data.GetEnumerator();
        }

        public void CopyTo(Array array, int index)
        {
            data.CopyTo(array, index);
        }

        public PointCollection Clone()
        {
            return new PointCollection(data);
        }
        object ICloneable.Clone()
        {
            throw new NotImplementedException();
        }
    }

    public class ImageMesh : IEnumerable,ICloneable,IAnimatable<ImageMesh>
    {
        public PointCollection Points;
        public SKColor[] Pixels;

        private int width;
        private int height;

        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public int Count { get { return Pixels.Length; } }

        public (Point[] Corners, SKColor Color) this[int i]
        {
            get
            {
                return this[i % Width, i / Width];
            }
        }
        public (Point[] Corners, SKColor Color) this[int x, int y]
        {
            get
            {
                Point[] corners = new Point[4];
                corners[0] = Points[x    , y    ];
                corners[1] = Points[x + 1, y    ];
                corners[2] = Points[x    , y + 1];
                corners[3] = Points[x + 1, y + 1];
                return (corners, GetPixel(x, y));
            }
        }

        public SKColor GetPixel(int i)
        {
            return Pixels[i];
        }
        public SKColor GetPixel(int x, int y)
        {
            return Pixels[x + Width * y];
        }

        public ImageMesh(string base64EncodedData)
        {
            if (base64EncodedData == null) throw new MalformedImageException("Image is null");
            MemoryStream imageStream = new MemoryStream(Encoding.UTF8.GetBytes(base64EncodedData));

            SKBitmap image = SKBitmap.Decode(imageStream);
            width = image.Width;
            height = image.Height;

            SKColor[] pixels = image.Pixels;
            Points = new PointCollection(Width + 1, Height + 1);

            Pixels = new SKColor[pixels.Length];
            for (int i = 0; i < pixels.Length; i++)
            {
                Pixels[i] = pixels[i];
            }

            for (int y = 0; y <= Height; y++)
            {
                for (int x = 0; x <= Width; x++)
                {
                    double scaledX = 2.0 * x / Width - 1;
                    double scaledY = 2.0 * y / Height - 1;
                    Points[x, y] = new Point(scaledX, scaledY);
                }
            }
        }
        private ImageMesh(PointCollection points, SKColor[] pixels, int width, int height)
        {
            Points = points;
            Pixels = pixels;
            this.width = width;
            this.height = height;
        }

        public SKBitmap ToRaster()
        {
            SKSurface surface = SKSurface.Create(new SKImageInfo(Width, Height));
            SKCanvas canvas = surface.Canvas;

            canvas.Clear();

            foreach ((Point[] corners, SKColor color) quad in this)
            {
                SKPoint[] vertices = quad.corners.Cast<SKPoint>().ToArray();
                canvas.DrawVertices(SKVertexMode.Triangles, vertices, null, new SKPaint { Color = quad.color });
            }
            
            SKImage snap = surface.Snapshot(new SKRectI(-1, -1, 1, 1));
            return SKBitmap.FromImage(snap);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) GetEnumerator();
        }
        public ImageMeshEnum GetEnumerator()
        {
            return new ImageMeshEnum(this);
        }

        public void Animate(int frame, IAnimator<ImageMesh> animator)
        {
            ImageMesh animated = animator.Animate(Clone(), frame);
            Points = animated.Points;
            Pixels = animated.Pixels;
        }
        public void Animate(int frame, Animation<ImageMesh> animation)
        {
            ImageMesh animated = animation(Clone(), frame);
            Points = animated.Points;
            Pixels = animated.Pixels;
        }

        public ImageMesh Clone()
        {
            PointCollection _points = Points.Clone();
            SKColor[] _pixels = (SKColor[])Pixels.Clone();
            int _width = Width;
            int _height = Height;

            return new ImageMesh(_points, _pixels, _width, _height);
        }
        object ICloneable.Clone()
        {
            return Clone();
        }
    }

    public class ImageMeshEnum : IEnumerator
    {
        public ImageMesh _mesh;
        int position = -1;

        public ImageMeshEnum(ImageMesh mesh)
        {
            _mesh = mesh;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public (Point[] Corners, SKColor Color) Current
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

        public bool MoveNext()
        {
            position++;
            return (position < _mesh.Count);
        }

        public void Reset()
        {
            position = -1;
        }
    }
}
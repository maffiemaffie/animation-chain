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

    public struct ColorPoint
    {
        private DoublePoint point;
        public DoublePoint Point 
        { 
            get { return point; } 
            set { point = value; } 
        }
        public double X 
        { 
            get { return point.X; } 
            set { point.X = value; }
        }
        public double Y 
        { 
            get { return point.Y; }
            set { point.Y = value; }
        }
        public SKColor Color;

        public byte A { 
            get { return Color.Alpha; }
            set { Color = new SKColor(value, R, G, B); }
        }
        public byte R
        {
            get { return Color.Red; }
            set { Color = new SKColor(A, value, G, B); }
        }
        public byte G
        {
            get { return Color.Green; }
            set { Color = new SKColor(A, R, value, B); }
        }
        public byte B
        {
            get { return Color.Blue; }
            set { Color = new SKColor(A, R, G, value); }
        }
        public byte Alpha
        {
            get { return A; }
            set { A = value; }
        }
        public byte Red
        {
            get { return R; }
            set { R = value; }
        }
        public byte Green
        {
            get { return G; }
            set { G = value; }
        }
        public byte Blue
        {
            get { return B; }
            set { B = value; }
        }

        public ColorPoint(DoublePoint point, SKColor color)
        {
            this.point = point;
            this.Color = color;
        }
        public ColorPoint(ColorPoint point) : this(point.point, point.Color) { }
        public ColorPoint(DoublePoint point, (byte r, byte g, byte b) color) : this(point, new SKColor(color.r, color.g, color.b)) { }
        public ColorPoint(DoublePoint point, (byte a, byte r, byte g, byte b) color) : this(point, new SKColor(color.a, color.r, color.g, color.b)) { }

        public static ColorPoint operator +(ColorPoint p1, DoublePoint p2)
        {
            return new ColorPoint(p1.Point + p2, p1.Color);
        }
        public static ColorPoint operator +(ColorPoint p1, (double x, double y) p2) {
            return new ColorPoint(p1.Point + p2, p1.Color);
        }
        public static ColorPoint operator -(ColorPoint p1, DoublePoint p2)
        {
            return new ColorPoint(p1.Point - p2, p1.Color);
        }
        public static ColorPoint operator -(ColorPoint p1, (double x, double y) p2)
        {
            return new ColorPoint(p1.Point - p2, p1.Color);
        }
        public static ColorPoint operator *(ColorPoint p1, DoublePoint p2)
        {
            return new ColorPoint(p1.point * p2, p1.Color);
        }
        public static ColorPoint operator *(ColorPoint p1, double scale)
        {
            return new ColorPoint(p1.point * scale, p1.Color);
        }
        public static ColorPoint operator /(ColorPoint p1, DoublePoint p2)
        {
            return new ColorPoint(p1.point / p2, p1.Color);
        }
        public static ColorPoint operator /(ColorPoint p1, double scale)
        {
            return new ColorPoint(p1.point / scale, p1.Color);
        }
    }

    public struct DoublePoint
    {
        public double X;
        public double Y;

        public static implicit operator DoublePoint((double x, double y) point) => new DoublePoint(point);
        public static implicit operator (double, double)(DoublePoint point) => (point.X, point.Y);
        public DoublePoint(double x, double y)
        {
            this.X = x; 
            this.Y = y;
        }
        public DoublePoint((double x, double y) point) : this(point.x, point.y) { }

        public static DoublePoint operator +(DoublePoint p1, DoublePoint p2)
        {
            return new DoublePoint(p1.X + p2.X, p1.Y + p2.Y);
        }
        public static DoublePoint operator +(DoublePoint p1, (double X, double Y) p2)
        {
            return new DoublePoint(p1.X + p2.X, p1.Y + p2.Y);
        }
        public static DoublePoint operator -(DoublePoint p1, DoublePoint p2)
        {
            return new DoublePoint(p1.X - p2.X, p1.Y - p2.Y);
        }
        public static DoublePoint operator -(DoublePoint p1, (double X, double Y) p2)
        {
            return new DoublePoint(p1.X - p2.X, p1.Y - p2.Y);
        }
        public static DoublePoint operator *(DoublePoint p1, DoublePoint p2)
        {
            return new DoublePoint(p1.X * p2.X, p1.Y * p2.Y);
        }
        public static DoublePoint operator *(DoublePoint p1, double scale)
        {
            return new DoublePoint(p1.X * scale, p1.Y * scale);
        }
        public static DoublePoint operator /(DoublePoint p1, DoublePoint p2)
        {
            return new DoublePoint(p1.X / p2.X, p1.Y / p2.Y);
        }
        public static DoublePoint operator /(DoublePoint p1, double scale)
        {
            return new DoublePoint(p1.X / scale, p1.Y / scale);
        }
    }

    public class PointCollection : IEnumerable, ICollection
    {
        private ColorPoint[] data;
        public ColorPoint[] Data
        {
            get { return data; }
        }
        public ColorPoint this[int i]
        {
            get { return data[i]; }
            set { data[i] = value; }
        }
        public int Count { get { return data.Length; } }

        public bool IsSynchronized => data.IsSynchronized;

        public object SyncRoot => data.SyncRoot;

        public PointCollection(ColorPoint[] data)
        {
            this.data = new ColorPoint[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                this[i] = new ColorPoint(data[i]);
            }
        }
        public PointCollection(PointCollection pc) : this(pc.data) { }
        public PointCollection(string base64EncodedData)
        {
            if (base64EncodedData == null) throw new MalformedImageException("Image is null");
            MemoryStream imageStream = new MemoryStream(Encoding.UTF8.GetBytes(base64EncodedData));

            SKBitmap image = SKBitmap.Decode(imageStream);

            SKColor[] pixels = image.Pixels;
            data = new ColorPoint[pixels.Length];
            for (int i = 0; i < pixels.Length; i++)
            {
                int x = i % image.Width;
                int y = i / image.Width;
                data[i] = new ColorPoint((x, y), pixels[i]);
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
    }
}

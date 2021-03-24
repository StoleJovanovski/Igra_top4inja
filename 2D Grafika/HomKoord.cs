using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace _2D_Grafika
{               // HomogeniKoordinati2D
    public class HomKoord2D
    {
     // float a;
        public float Xx, Yx, X;
        public float Xy, Yy, Y;

        public HomKoord2D()
        {
            X = Y = 0;
            Xy = Yx = 0;    // 1 0 0
            Xx = Yy = 1;    // 0 1 0
        }
        public HomKoord2D(float x, float y, float sx = 1, float sy = 1, double a = 0)
        {
            X = x;  x = (float)Math.Sin(a);
            Y = y;  y = (float)Math.Cos(a);
          
            Xx = sx * y;  Yx = - sy * x;
            Xy = sx * x;  Yy =   sy * y;
        }
        public HomKoord2D(RectangleF aa, double a = 0)
            : this(aa.X, aa.Y, aa.Width, aa.Height, a) { }

        private HomKoord2D(float Xx, float Yx, float X,
                           float Xy, float Yy, float Y) {
            this.Xx = Xx;  this.Yx = Yx;  this.X = X;
            this.Xy = Xy;  this.Yy = Yy;  this.Y = Y;
        }

        public void Dodeli(HomKoord2D XYA)
        {
            Xx = XYA.Xx;
            Xy = XYA.Xy;
            Yx = XYA.Yx;
            Yy = XYA.Yy;
            X = XYA.X;
            Y = XYA.Y;
        }

        public HomKoord2D Inverzna()
        {
            float k = 1/(Xx*Yy - Xy*Yx);
            return new HomKoord2D(+Yy*k, -Yx*k, (Y*Yx-X*Yy)*k,
                                  -Xy*k, +Xx*k, (X*Xy-Y*Xx)*k);
        }

        public static HomKoord2D operator *(HomKoord2D A, HomKoord2D B)
        {
            return new HomKoord2D(A.Xx*B.Xx + A.Yx*B.Xy,  A.Xx*B.Yx + A.Yx*B.Yy,  A.Xx*B.X + A.Yx*B.Y + A.X,
                                  A.Xy*B.Xx + A.Yy*B.Xy,  A.Xy*B.Yx + A.Yy*B.Yy,  A.Xy*B.X + A.Yy*B.Y + A.Y);
        }
        public static PointF operator *(HomKoord2D A, PointF P)
        {
            return new PointF(A.Xx*P.X + A.Yx*P.Y + A.X,
                              A.Xy*P.X + A.Yy*P.Y + A.Y);
        }

        public static HomKoord2D T(float dx, float dy)
        {
            return new HomKoord2D(1,0,dx,
                                  0,1,dy);
        }
        public static HomKoord2D R(double a)
        {
            float sin = (float)Math.Sin(a);
            float cos = (float)Math.Cos(a);
            return new HomKoord2D(cos, -sin, 0,
                                  sin, +cos, 0);
        }
        public static HomKoord2D S(float sx, float sy)
        {
            return new HomKoord2D(sx,  0, 0,
                                   0, sy, 0);
        }
        public static HomKoord2D S(float s)
        {
            return new HomKoord2D(s, 0, 0,
                                  0, s, 0);
        }

        public void Pozicioniraj(float x, float y)
        {
            X = x;
            Y = y;
        }
        public void Transliraj(float dx, float dy)
        {
            X += dx;
            Y += dy;
        }
        public void Rotiraj(double a)
        {
            this.Dodeli(HomKoord2D.R(a) * this);
        }
        public void Skaliraj(float Sx, float Sy)
        {
            Xx *= Sx;  Yx *= Sx;  X *= Sx;
            Xy *= Sy;  Yy *= Sy;  Y *= Sy;
        }
        public void Skaliraj(float s)
        {
            Xx *= s;  Yx *= s;  X *= s;
            Xy *= s;  Yy *= s;  Y *= s;
        }
        public void SkalDim(float Sx, float Sy)
        {
            Xx *= Sx;  Yx *= Sx;
            Xy *= Sy;  Yy *= Sy;
        }
        public void SkalDim(float s)
        {
            Xx *= s; Yx *= s;
            Xy *= s; Yy *= s;
        }
    }
}

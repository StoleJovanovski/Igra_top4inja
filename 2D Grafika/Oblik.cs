using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace _2D_Grafika.Oblik
{
    public abstract class Oblik
    {
        public abstract bool DaliSodrzi(PointF P);

        public static bool DaliSeDopiraat(Oblik ob1, Oblik ob2)
        {
            throw new NotImplementedException();
        }

        public static bool DaliSeSodrziVo(Oblik ob1, Oblik ob2)
        {
            throw new NotImplementedException();
        }
    }

    public class Pravoagolnik : Oblik
    {
        public RectangleF prav { get; set; }

        public Pravoagolnik(RectangleF prav)
        {
            this.prav = prav;
        }

        public override bool DaliSodrzi(PointF P)
        {
            return prav.Contains(P);
        }
    }

    public class Krug : Oblik
    {
        public PointF Centar { get; set; }
        public float Radius { get; set; }

        public Krug(PointF Centar, float Radius)
        {
            this.Centar = Centar;
            this.Radius = Radius;
        }
        public Krug(float X, float Y, float Radius)
        {
            this.Centar = new PointF(X, Y);
            this.Radius = Radius;
        }

        public override bool DaliSodrzi(PointF P)
        {
            return (Centar.X-P.X)*(Centar.X-P.X) + (Centar.Y-P.Y)*(Centar.Y-P.Y) < Radius*Radius;
        }
    }

    public class Elipsa : Oblik
    {
        public PointF Centar { get; set; }
        public PointF Radius { get; set; }

        public Elipsa(PointF Centar, PointF Radius)
        {
            this.Centar = Centar;
            this.Radius = Radius;
        }

        public override bool DaliSodrzi(PointF P)
        {
            return (Centar.X-P.X)*(Centar.X-P.X)/Radius.X + (Centar.Y - P.Y)*(Centar.Y-P.Y)/Radius.Y < 1.0;
        }
    }

    public class _4agolnik : Oblik
    {
        public HomKoord2D XYA { get; set; }

        public _4agolnik(HomKoord2D XYA)
        {
            this.XYA = XYA;
        }

        public override bool DaliSodrzi(PointF P)
        {
            return (XYA.X          - P.X) * XYA.Xy > XYA.Xx * (XYA.Y          - P.Y) &&
                   (XYA.X          - P.X) * XYA.Yy < XYA.Yx * (XYA.Y          - P.Y) &&
                   (XYA.X + XYA.Yx - P.X) * XYA.Xy < XYA.Xx * (XYA.Y + XYA.Yy - P.Y) &&
                   (XYA.X + XYA.Xx - P.X) * XYA.Yy > XYA.Yx * (XYA.Y + XYA.Xy - P.Y);
        }
    }
}

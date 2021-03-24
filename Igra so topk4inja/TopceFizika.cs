
using System;
namespace Igra_so_topk4inja
{
    public class TopceFizika
    {
        public float dT;
        public float X, X1, Y, Y1, Vx, Vy, R;
        public TopceVidlivo top;

        public TopceFizika(TopceVidlivo top, float Vx, float Vy)
        {
            this.top = top;
            this.Vx = Vx;
            this.Vy = Vy;

            Prezemi();
        }

        public TopceFizika(float X, float Y, float R, float Vx = 0, float Vy = 0)
        {
            this.R = R;
            this.X = X1 = X;
            this.Y = Y1 = Y;
            this.Vx = Vx;
            this.Vy = Vy;
        }

        public void Prezemi()
        {
            R = top.R;
            X1 = X = top.X;
            Y1 = Y = top.Y;
        }
        public void Postavi()
        {
            top.X = X;
            top.Y = Y;
        }

        public void Vreme(float dT)
        {
            this.dT = dT;
        }

        public void Pridvizi()
        {
            X1 = (X = X1) + Vx * dT;
            Y1 = (Y = Y1) + Vy * dT;
        }

        public void Gravitacija()
        {
            Vy += dT * 333; // 9.81F * 900 / 0.25F;
        }

        public bool Interakcija(_2D_Grafika.HomKoord2D XYA)
        {
            if (X1 - R <= XYA.X ||
                X1 + R >= XYA.X +
                          XYA.Xx)  X1 = X + (Vx = -Vx) * dT;
            //if (Y1 - R <= XYA.Y ||
            //    Y1 + R >= XYA.Y +
            //              XYA.Yy)  Y1 = Y + (Vy = -Vy) * dT;
            return false;
        }
        public bool Interakcija(TopceFizika topc)
        {
            if (OpsteRabote.Kvadrat(this.X1 - topc.X)+ OpsteRabote.Kvadrat(this.Y1 - topc.Y) < OpsteRabote.Kvadrat(this.R + topc.R)) {
                double V = Math.Sqrt(OpsteRabote.Kvadrat(this.Vx) + OpsteRabote.Kvadrat(this.Vy)) * 0.9;
                double A = Math.PI - Math.Atan2(this.Vy, this.Vx) +
                                 2 * Math.Atan2(Y - topc.Y, X - topc.X);
                X1 = X + (Vx = (float)(V * Math.Cos(A))) * dT;
                Y1 = Y + (Vy = (float)(V * Math.Sin(A))) * dT;
                return true;
            }
            return false;
        }
    }
}

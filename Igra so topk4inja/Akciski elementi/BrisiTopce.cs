using System.Drawing;
using _2D_Grafika.Elementi;
using System;

namespace Igra_so_topk4inja.AkciskiElementi
{
    public class BrisiTopce : ElementAkciski
    {
        public const float _TT = 0.15F;
        
        private TopceVidlivo top;
        private float R, k;

        private event Action Kraj;

        public BrisiTopce(Point p, Matrica mat, ElementAkciski roditel)
            : base(roditel)
        {
            Kraj += new Action(delegate() { mat.IzbrisiTopce(p.Y, p.X); });

            R = (top = mat.A[p.Y,p.X]).R;
            k = 1;
        }

        public override void PriKomplet()
        {
            base.PriKomplet();
            Kraj();
        }

        public override PodatocneStrukture.Liste.Lista<ElementAkciski> Akcija(_2D_Grafika.Prostor p, long dt, float dT)
        {
            k -= dT / _TT;

            if (k > 0) top.R = R * k;
            else  this.Kompletiraj();

            return null;
        }
    }
}


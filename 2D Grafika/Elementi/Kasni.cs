using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PodatocneStrukture.Liste;

namespace _2D_Grafika.Elementi
{
    public class Kasni : ElementAkciski
    {
        private ElementAkciski e;
        private float t;

        public Kasni(float vreme, ElementAkciski el)
            : base(el.Roditel)
        {
            t = vreme;
            e = el;
        }

        public override Lista<ElementAkciski> Akcija(Prostor p, long dt, float dT)
        {
            if ((t -= dT) > 0) return null;
            
            base.Kompletiraj();
            
            Lista<ElementAkciski> l = new ListaNiza<ElementAkciski>(1);
            l.Unesi(e);
            return l;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using PodatocneStrukture.Liste;

namespace _2D_Grafika.Elementi
{
    public abstract class ElementProst { }

    public interface PoseduvaVidlivost
    {
        void Crtaj(Pogled p, long segaVreme);
    }
    public interface PoseduvaNastan
    {
        void DodadiSpravuvac(Spravuvaci sprav);
        void IzbrisiSpravuvac(Spravuvaci sprav);
    }

    public abstract class ElementAkciski : ElementProst
    {
        public ElementAkciski Roditel { get; private set; }

        public ElementAkciski(ElementAkciski roditel)
        {
            Roditel = roditel;
        }
        [Obsolete("Овој елемент мора да се содржи во SegasenSvet или во некоје негова компонента !")]
        public ElementAkciski()
        {
            Roditel = this;
        }

        public enum Sostojba { NOV, PAUZIRAN, RABOTI , ISKLUCEN,
                               KOMPLETIRAN, NEUSPESEN, PREKINAT }

        public Sostojba sostojba { get; private set; }

        public virtual void PriInicijal() { sostojba = Sostojba.RABOTI;   }
        public virtual void PriKomplet()  { sostojba = Sostojba.ISKLUCEN; }
        public virtual void PriNeuspeh()  { sostojba = Sostojba.ISKLUCEN; }
        public virtual void PriPrekin()   { sostojba = Sostojba.ISKLUCEN; }

        public abstract Lista<ElementAkciski> Akcija(Prostor p, long dt, float dT);

        public virtual void Pauziraj()   { if (sostojba == Sostojba.RABOTI) sostojba = Sostojba.PAUZIRAN; }
        public virtual void OdPauziraj() { if (sostojba == Sostojba.PAUZIRAN) sostojba = Sostojba.RABOTI; }

        public virtual void Prekini() { sostojba = Sostojba.PREKINAT; }
        public virtual void Resetiraj() { sostojba = Sostojba.RABOTI; }
   //   public virtual void Isklici() { sostojba = Sostojba.ISKLUCEN; }

        protected virtual void Kompletiraj() { sostojba = Sostojba.KOMPLETIRAN; }
        protected virtual void NeuspZavrsi() { sostojba = Sostojba.NEUSPESEN;   }
    }

    public class ElementSlozen : ElementProst
    {
        public Lista<ElementProst> Elementi { get; set; }

        public ElementSlozen()
        {
            Elementi = new ListaNiza<ElementProst>();
        }
    }
}

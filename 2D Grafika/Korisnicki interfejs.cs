using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace _2D_Grafika.KorisnickiInterfejs
{
    public class Panel<TipIzgled, TipOblik> : Elementi.ElementProst, Elementi.PoseduvaVidlivost, Elementi.PoseduvaNastan
        where TipIzgled : Elementi.PoseduvaVidlivost
        where TipOblik  : Oblik.Oblik
    {
        public event EventHandler priGluvceKlik;
        public event EventHandler priGluvceVlez;
        public event EventHandler priGluvceIzlez;
        public event Spravuvac.SpravuvacGluvce.FunkcijaSpravuvacGluvce priGluvceDvizi;
        public event Spravuvac.SpravuvacGluvce.FunkcijaSpravuvacGluvce priGluvceDole;
        public event Spravuvac.SpravuvacGluvce.FunkcijaSpravuvacGluvce priGluvceGore;

        public event Action postaviSostojbaAktivno;
        public event Action postaviSostojbaNeaktivno;

        private bool aktivno;
        public bool Aktivno {
            get { return aktivno; }
            set {
                if (aktivno = value) {  if (postaviSostojbaAktivno != null) postaviSostojbaAktivno();  }
                else                 {  if (postaviSostojbaNeaktivno != null) postaviSostojbaNeaktivno();  pritisnato = vnatre = false;  }
            }
        }

        public TipIzgled izgled { get; set; }
        public TipOblik  oblik  { get; set; }

        public Panel(TipIzgled izgled, TipOblik oblik)
        {
            this.izgled = izgled;
            this.oblik = oblik;

            aktivno = true;
        }

        public virtual void Crtaj(Pogled p, long segaVreme)
        {
            izgled.Crtaj(p, segaVreme);
        }

        public virtual void DodadiSpravuvac(Spravuvaci sprav)
        {
            Spravuvac.SpravuvacGluvce gl = (Spravuvac.SpravuvacGluvce)sprav.VratiSpravuvac(typeof(Spravuvac.SpravuvacGluvce));
            gl.DodadiGluvceDvizi(glDvizi);
            gl.DodadiGluvceGore(glGore);
            gl.DodadiGluvceDole(glDole);
        }
        public virtual void IzbrisiSpravuvac(Spravuvaci sprav)
        {
            Spravuvac.SpravuvacGluvce gl = (Spravuvac.SpravuvacGluvce)sprav.VratiSpravuvac(typeof(Spravuvac.SpravuvacGluvce));
            gl.IzbrisiGluvceDvizi(glDvizi);
            gl.IzbrisiGluvceGore(glGore);
            gl.IzbrisiGluvceDole(glDole);
        }

        public bool pritisnato { get; private set; }
        public bool vnatre     { get; private set; }

        private bool glDvizi(object povikuvac, MouseEventArgs arg, PointF T)
        {
            if (aktivno && vnatre != oblik.DaliSodrzi(T))
                if (vnatre = !vnatre)  {  if (priGluvceVlez != null)  priGluvceVlez(this, arg);   }
                else                   {  if (priGluvceIzlez != null) priGluvceIzlez(this, arg);  }
            
            if (vnatre && priGluvceDvizi != null) return priGluvceDvizi(this, arg, T);
            return false;
        }
        private bool glGore(object povikuvac, MouseEventArgs arg, PointF T)
        {
            if (pritisnato && vnatre && priGluvceKlik != null) priGluvceKlik(this, arg);
            pritisnato = false;
            if (aktivno && priGluvceGore != null) return priGluvceGore(this, arg, T);
            return false;
        }
        private bool glDole(object povikuvac, MouseEventArgs arg, PointF T)
        {
            if (vnatre) {
                pritisnato = true;
                if (priGluvceDole != null) return priGluvceDole(this, arg, T);
                return true;
            }
            return false;
        }
    }

    public class Kopce<TipIzgled, TipOblik> : Panel<TipIzgled, TipOblik>
        where TipIzgled : Elementi.PoseduvaVidlivost
        where TipOblik : Oblik.Oblik
    {
        public event Action postaviSostojbaSlobodno;
        public event Action postaviSostojbaSpremno;
        public event Action postaviSostojbaPritisnato;
        public new event Action postaviSostojbaAktivno;

        public new event Spravuvac.SpravuvacGluvce.FunkcijaSpravuvacGluvce priGluvceDole;
        public new event Spravuvac.SpravuvacGluvce.FunkcijaSpravuvacGluvce priGluvceGore;

        public Kopce(TipIzgled izgled, TipOblik oblik) : base(izgled, oblik)
        {
            base.postaviSostojbaAktivno += new Action(Kopce_postaviSostojbaAktivno);

            base.priGluvceVlez  += new EventHandler(Kopce_priGluvceVlez);
            base.priGluvceIzlez += new EventHandler(Kopce_priGluvceIzlez);

            base.priGluvceDole += new Spravuvac.SpravuvacGluvce.FunkcijaSpravuvacGluvce(Kopce_priGluvceDole);
            base.priGluvceGore += new Spravuvac.SpravuvacGluvce.FunkcijaSpravuvacGluvce(Kopce_priGluvceGore); 
        }

        void Kopce_postaviSostojbaAktivno()
        {
            postaviSostojbaSlobodno();
            if (postaviSostojbaAktivno != null) postaviSostojbaAktivno();
        }

        private void Kopce_priGluvceVlez(object povikuvac, EventArgs arg)
        {
            if (!pritisnato) postaviSostojbaSpremno();
        }
        private void Kopce_priGluvceIzlez(object povikuvac, EventArgs arg)
        {
            if (!pritisnato) postaviSostojbaSlobodno();
        }

        private bool Kopce_priGluvceDole(object povikuvac, MouseEventArgs arg, PointF T)
        {
            postaviSostojbaPritisnato();

            if (priGluvceDole != null) return priGluvceDole(povikuvac, arg, T);

            return true;
        }
        private bool Kopce_priGluvceGore(object povikuvac, MouseEventArgs arg, PointF T)
        {
            if (vnatre) postaviSostojbaSpremno();
            else        postaviSostojbaSlobodno();

            if (priGluvceGore != null) return priGluvceGore(povikuvac, arg, T);
            return false;
        }
    }

    public class Panel4agol: Panel<_2D_Grafika.Elementi._4agolnik, _2D_Grafika.Oblik._4agolnik>
    {
        public HomKoord2D XYA;

        public Panel4agol(HomKoord2D XYA, Pen p = null, Brush b = null)
            : base(
                new Elementi._4agolnik(XYA, p!=null ? p : Pens.Blue, b!=null ? b : Brushes.Gray),
                new Oblik._4agolnik(XYA)
            )
        {
            this.XYA = XYA;
        }
    }

    public class Kopce4agol : Kopce<_2D_Grafika.Elementi._4agolnik, _2D_Grafika.Oblik._4agolnik>
    {
        public HomKoord2D XYA;

        public Brush PozdNeaktivno  { get; set; }
        public Brush PozdSlobodno   { get; set; }
        public Brush PozdSpremno    { get; set; }
        public Brush PozdPritisnato { get; set; }

        public Kopce4agol(HomKoord2D XYA)
            : base(
                new Elementi._4agolnik(XYA, Pens.Black, Brushes.Gray),
                new Oblik._4agolnik(XYA)
            )
        {
            this.XYA = XYA;

            PozdNeaktivno  = Brushes.Silver;
            PozdSlobodno   = Brushes.Gray;
            PozdSpremno    = Brushes.DarkGray;
            PozdPritisnato = Brushes.LightGray;

            postaviSostojbaAktivno    += new Action(delegate() {  izgled.pen = Pens.Black;      });
            postaviSostojbaNeaktivno  += new Action(delegate() {  izgled.brs = PozdNeaktivno;  izgled.pen = Pens.Gainsboro;  });
            postaviSostojbaSlobodno   += new Action(delegate() {  izgled.brs = PozdSlobodno;    });
            postaviSostojbaSpremno    += new Action(delegate() {  izgled.brs = PozdSpremno;     });
            postaviSostojbaPritisnato += new Action(delegate() {  izgled.brs = PozdPritisnato;  });
        }
    }

    public class KopceAnimacisko<TipOblik> : Kopce<Elementi.Animacija, TipOblik>
        where TipOblik : Oblik.Oblik
    {
        public Animacija animNeaktivno  { get; set; }
        public Animacija animSlobodno   { get; set; }
        public Animacija animSprmno     { get; set; }
        public Animacija animPritisnato { get; set; }

        public KopceAnimacisko(HomKoord2D XYA, TipOblik oblik,
            Animacija animNeaktivno,
            Animacija animSlobodno,
            Animacija animSprmno,
            Animacija animPritisnato, bool dimenziiSlika = true)
            : base(new Elementi.Animacija(XYA, animSlobodno, dimenziiSlika), oblik)
        {
            this.animNeaktivno  = animNeaktivno;
            this.animSlobodno   = animSlobodno;
            this.animSprmno     = animSprmno;
            this.animPritisnato = animPritisnato;

            postaviSostojbaNeaktivno  += new Action(delegate() {  izgled.Anim = animNeaktivno;   });
            postaviSostojbaSlobodno   += new Action(delegate() {  izgled.Anim = animSlobodno;    });
            postaviSostojbaSpremno    += new Action(delegate() {  izgled.Anim = animSprmno;      });
            postaviSostojbaPritisnato += new Action(delegate() {  izgled.Anim = animPritisnato;  });
        }

        public KopceAnimacisko(HomKoord2D XYA, TipOblik oblik, Animacija[] anim, bool dimenziiSlika = true)
            : this(XYA, oblik, anim[0], anim[1], anim[2], anim[3], dimenziiSlika)
        { }
    }
}

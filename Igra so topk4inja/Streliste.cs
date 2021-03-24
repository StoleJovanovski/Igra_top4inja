using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _2D_Grafika.Elementi;
using _2D_Grafika;
using System.Drawing;
using PodatocneStrukture.Liste;
using Igra_so_topk4inja.AkciskiElementi;

namespace Igra_so_topk4inja
{
    public class Streliste : ElementAkciski, PoseduvaVidlivost, PoseduvaNastan
    {
        private const float _Xsprem11 =   0;
        private const float _Ysprem11 = -27;
        private const float _Xceka11  =  22;
        private const float _Yceka11  =  17;
        private const float _Xpoeni11 = -34;
        private const float _Ypoeni11 =   5;

        public const float _XX = GlavnaIgra._XX + GlavnaIgra._SS * 0.5F;
        public const float _YY = GlavnaIgra._YY + GlavnaIgra._VV - 140F;
        public const float _RR = 50F;
        public const float _Xsprem = _XX + _Xsprem11;
        public const float _Ysprem = _YY + _Ysprem11;
        public const float _Xceka  = _XX + _Xceka11;
        public const float _Yceka  = _YY + _Yceka11;
        public const float _Xpoeni = _XX + _Xpoeni11;
        public const float _Ypoeni = _YY + _Ypoeni11;

        private GlavnaIgra glavIgr;

        private _2D_Grafika.KorisnickiInterfejs.KopceAnimacisko<_2D_Grafika.Oblik.Krug> kop;
        private TopceVidlivo topSpremno, topCeka, topNas, topProektil;
        private Tekst tekBrPreostanato;

        private int brPreostanato;
        public int BrPreostanato {
            get { return brPreostanato; }
            set { tekBrPreostanato.tekst = (brPreostanato = value).ToString(); }
        }

        private PointF nasoka;
        public PointF Nasoka {
            get { return nasoka; }
            set {
                float r1 = 1 / (float)Math.Sqrt(value.X*value.X + value.Y*value.Y);
                PointF nas = new PointF(value.X * r1, value.Y * r1);
                if (nas.Y < -0.4F) nasoka = nas;
            }
        }

        private bool bb, kraj, us;
        private float k, kk;
        
        private static WMPLib.WindowsMediaPlayer zvukStrelaj, zvukSmeni;
        static Streliste()
        {
            (zvukStrelaj = new WMPLib.WindowsMediaPlayer() { URL = "Strelanje.wav" }).controls.stop();
            (zvukSmeni = new WMPLib.WindowsMediaPlayer() { URL = "Smeni.wav" }).controls.stop();
        }

        #pragma warning disable
        public Streliste(GlavnaIgra glavIgr)
        {
            this.glavIgr = glavIgr;

            _2D_Grafika.Animacija a0 = new _2D_Grafika.Animacija(new System.Drawing.Image[] { new Bitmap("Streliste1.png") }, 60);
            _2D_Grafika.Animacija a1 = new _2D_Grafika.Animacija(new System.Drawing.Image[] { new Bitmap("Streliste2.png"), new Bitmap("Streliste3.png"), new Bitmap("Streliste4.png") }, 60);
            kop = new _2D_Grafika.KorisnickiInterfejs.KopceAnimacisko<_2D_Grafika.Oblik.Krug>(new HomKoord2D(_XX-_RR, _YY-_RR), new _2D_Grafika.Oblik.Krug(_XX, _YY, _RR), null, a0, a1, a1);
            kop.priGluvceKlik += new EventHandler(delegate(object sender, EventArgs e) {
                Smeni();
            });
            kop.priGluvceDvizi += new Spravuvac.SpravuvacGluvce.FunkcijaSpravuvacGluvce(delegate(object povikuvac, System.Windows.Forms.MouseEventArgs arg, PointF T) { return true; });
            topNas = new TopceVidlivo(0,0, 4, new Bitmap("Nasoka.png"));

            tekBrPreostanato = new Tekst(
                new HomKoord2D(_Xpoeni, _Ypoeni),
                new Font("Jokerman", 12, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                "", Brushes.Red);
        }

        public void Resetiraj(int i)
        {
            BrPreostanato = i;

            Nasoka = new PointF(0,-1);

            bb = kraj = false;
            k = kk = 0;

            topProektil = null;
            topSpremno = new TopceVidlivo(_Xsprem, _Ysprem);
            topCeka = new TopceVidlivo(_Xceka, _Yceka);
            
            base.Resetiraj();
        }
        public void OslobodiProektil()
        {
            topProektil = null;
        }

        public void Crtaj(_2D_Grafika.Pogled p, long segaVreme)
        {
            topNas.X = _Xsprem + 15*kk * nasoka.X;
            topNas.Y = _Ysprem + 15*kk * nasoka.Y;
                topNas.Crtaj(p, segaVreme);
            for (int i = 1; i < 14; ++i) {
                topNas.X += 15 * nasoka.X;
                topNas.Y += 15 * nasoka.Y;
                topNas.Crtaj(p, segaVreme);
            }

            kop.Crtaj(p, segaVreme);

            if (topCeka != null) topCeka.Crtaj(p, segaVreme);
            if (topSpremno != null) topSpremno.Crtaj(p, segaVreme);
            if (topProektil != null) topProektil.Crtaj(p, segaVreme);
            
            tekBrPreostanato.Crtaj(p, segaVreme);
        }

        public void DodadiSpravuvac(_2D_Grafika.Spravuvaci sprav)
        {
            ((Spravuvac.SpravuvacTastatura)sprav.VratiSpravuvac(typeof(Spravuvac.SpravuvacTastatura))).DodadiKopcePritisnato(tast);
            kop.DodadiSpravuvac(sprav);
        }
        public void IzbrisiSpravuvac(_2D_Grafika.Spravuvaci sprav)
        {
            ((Spravuvac.SpravuvacTastatura)sprav.VratiSpravuvac(typeof(Spravuvac.SpravuvacTastatura))).IzbrisiKopcePritisnato(tast);
            kop.IzbrisiSpravuvac(sprav);
        }
        private bool tast(object povikuvac, System.Windows.Forms.KeyEventArgs arg)
        {
            if (arg.KeyData == System.Windows.Forms.Keys.Enter) {
                Strelaj();
                return true;
            }
            if (arg.KeyData == System.Windows.Forms.Keys.Space) {
                Smeni();
                return true;
            }
            return false;
        }

        public override Lista<ElementAkciski> Akcija(_2D_Grafika.Prostor p, long dt, float dT)
        {
            if ((kk += 2 * dT) > 1) kk = 0;

            if (k > 0) {
                k -= 2*dT;
                if (topSpremno != null) {
                    topSpremno.X = _Xceka*k - k*_Xsprem + _Xsprem;
                    topSpremno.Y = _Yceka*k - k*_Ysprem + _Ysprem;
                }
                if (topCeka != null && !bb) {
                    topCeka.X = _Xsprem*k - k*_Xceka + _Xceka;
                    topCeka.Y = _Ysprem*k - k*_Yceka + _Yceka;
                }
                if (k <= 0) bb = false;
            } 
            else if (kraj && topProektil == null) 
                if (brPreostanato > 0) {
                    Strelaj(false);
                } else {
                    if ((k -= dT) < -1 && glavIgr.kazani.ll.Prazno) {
                        kraj = false;
                        ListaNiza<ElementAkciski> l = new ListaNiza<ElementAkciski>();
                        l.Unesi(new NeUspesnoZavrseno(glavIgr, us, this));
                        return l;
                    }
                }

            if (dvizProek != null) {
                ListaNiza<ElementAkciski> l = new ListaNiza<ElementAkciski>();
                l.Unesi(dvizProek);
                dvizProek = null;
                return l;
            }
            return null;
        }

        private ElementAkciski dvizProek;

        public void Strelaj(bool str = true)
        {
            if (k > 0 || topSpremno == null || topProektil != null) return;

            if (ProstorIgra.Zvuk) zvukStrelaj.controls.play();

            if (str) dvizProek = new DviziProektil(glavIgr, topProektil = topSpremno, nasoka, this);
            else     dvizProek = new SlobodnoTopce(glavIgr, topSpremno, this);

            if (brPreostanato > 0) {
                if (--BrPreostanato > 1) topSpremno = new TopceVidlivo(_Xceka, _Yceka);
                else                     topSpremno = null;
                if (BrPreostanato == 0 && !kraj) {
                    us = false;
                    kraj = true;
                }
            }
            bb = true;
            Smeni();
        }
        public void Smeni()
        {
            if (k <= 0 && topCeka != null) {
                k = 1;
                OpsteRabote.Razmeni(ref topSpremno, ref topCeka);
                if (ProstorIgra.Zvuk) zvukSmeni.controls.play();
            }
        }
        public void KrajUspesno()
        {
            kraj = us = true;
        }
    }
}

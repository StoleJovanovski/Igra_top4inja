using System;
using System.Drawing;
using _2D_Grafika;
using _2D_Grafika.Elementi;
using _2D_Grafika.KorisnickiInterfejs;

namespace Igra_so_topk4inja
{
    public class GlavnaIgra : ElementSlozen
    {
        public const float _SS = 444;
        public const float _VV = 590;
        public const float _XX = (900-_SS) * 0.5F;
        public const float _YY = 4;

        public Slika pozadina;
        public Streliste streliste;
        public Matrica matrica;
        public Kazani kazani;
        public Panel4agol rabotnaPovrsina;
        public Kopce4agol kopcNazad;
        public Tekst poenTekst, tekKopNaz;
        public _2D_Grafika.Elementi._4agolnik poenPozd;

        private int brPoeni;
        public int BrPoeni
        {
            get { return brPoeni; }
            set { poenTekst.tekst = (brPoeni = value).ToString(); }
        }

        public GlavnaIgra(ProstorIgra pr)
        {
            pozadina = new Slika(new HomKoord2D(), new Bitmap("PozadinaG.jpg"));
            streliste = new Streliste(this);
            matrica = new Matrica();
            kazani = new Kazani();

            rabotnaPovrsina = new Panel4agol(new HomKoord2D(_XX, _YY, _SS, _VV), b: new SolidBrush(Color.FromArgb(64, Color.Yellow)));
            rabotnaPovrsina.priGluvceKlik += new EventHandler(delegate(object sender, EventArgs e) {
                streliste.Strelaj();
            });
            rabotnaPovrsina.priGluvceDvizi += new Spravuvac.SpravuvacGluvce.FunkcijaSpravuvacGluvce(
                delegate(object povikuvac, System.Windows.Forms.MouseEventArgs arg, PointF T) {
                    streliste.Nasoka = new PointF(T.X-Streliste._Xsprem, T.Y-Streliste._Ysprem);
                    return true;
                }
            );

            poenPozd = new _2D_Grafika.Elementi._4agolnik(new HomKoord2D(0,0, 120,50), Pens.DarkGreen, Brushes.DarkViolet);
            poenTekst = new _2D_Grafika.Elementi.Tekst(
                new HomKoord2D(),
                new System.Drawing.Font("Bauhaus 93", 20.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0))),
                "", Brushes.Gold
            );

            Font ff = new System.Drawing.Font("Monotype Corsiva", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            long tt = DateTime.Now.Ticks;
            kopcNazad = new Kopce4agol(new HomKoord2D(50, 520, 120, 33));
            kopcNazad.priGluvceKlik+= new EventHandler(delegate(object sender, EventArgs e) {
                if (tt + 2000000 > (tt = DateTime.Now.Ticks))
                    pr.Mapa();
            });
            tekKopNaz = new Tekst(new HomKoord2D(75, 523), ff, "Назад", new SolidBrush(Color.Cyan));

            OpsteRabote.PostaviKopceZvuk(kopcNazad);

            Elementi.Unesi(pozadina);
            Elementi.Unesi(rabotnaPovrsina);
            
            Elementi.Unesi(streliste);
            Elementi.Unesi(matrica);
            Elementi.Unesi(kazani);
            
            Elementi.Unesi(poenPozd);
            Elementi.Unesi(poenTekst);

            Elementi.Unesi(kopcNazad);
            Elementi.Unesi(tekKopNaz);
        }

        public void Resetiraj(int i)
        {
            TopceVidlivo.nn = i + 1;

            streliste.Resetiraj(10*i);
            matrica.Resetiraj();
            kazani.Resetiraj();

            BrPoeni = 0;

            poenPozd.brs = Brushes.DarkViolet;
            poenPozd.XYA.X = 40;
            poenPozd.XYA.Y = 150;
            poenTekst.XYA.X = poenPozd.XYA.X + 11;
            poenTekst.XYA.Y = poenPozd.XYA.Y + 11;
        }
    }
}

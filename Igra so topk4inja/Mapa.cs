using System;
using System.Drawing;
using _2D_Grafika;
using _2D_Grafika.Elementi;
using _2D_Grafika.KorisnickiInterfejs;

namespace Igra_so_topk4inja
{
    public class Mapa : ElementSlozen
    {
        public const float _SS = 70;
        public const float _VV = 70;

        public Slika pozadina;
        public Kopce4agol kop1, kop2, kop3, kop4, kopcNazad;
        public Tekst tekKop1, tekKop2, tekKop3, tekKop4, tekKopNaz;

        public Mapa(ProstorIgra pr)
        {
            pozadina = new Slika(new HomKoord2D(), new Bitmap("PozadinaM.jpg"));

            Font ff = new System.Drawing.Font("Monotype Corsiva", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            
            kopcNazad = new Kopce4agol(new HomKoord2D(50, 520, 120, 33));
            kopcNazad.priGluvceKlik += new EventHandler(delegate(object sender, EventArgs e) {
                pr.PocetenEkran();
            });
            tekKopNaz = new Tekst(new HomKoord2D(75, 523), ff, "Назад", new SolidBrush(Color.Cyan));

            ff = new System.Drawing.Font("Ariel", 32, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            
            kop1 = new Kopce4agol(new HomKoord2D(300, 150, _SS, _VV));
            kop1.priGluvceKlik += new EventHandler(delegate(object sender, EventArgs e) {  pr.NovaIgra(1);  });
            tekKop1 = new Tekst(new HomKoord2D(kop1.XYA.X + 20, kop1.XYA.Y + 10), ff, "I", new SolidBrush(Color.Red));

            kop2 = new Kopce4agol(new HomKoord2D(530, 150, _SS, _VV));
            kop2.priGluvceKlik += new EventHandler(delegate(object sender, EventArgs e) {  pr.NovaIgra(2);  });
            tekKop2 = new Tekst(new HomKoord2D(kop2.XYA.X + 15, kop2.XYA.Y + 10), ff, "II", new SolidBrush(Color.Red));

            kop3 = new Kopce4agol(new HomKoord2D(300, 350, _SS, _VV));
            kop3.priGluvceKlik += new EventHandler(delegate(object sender, EventArgs e) {  pr.NovaIgra(3);  });
            tekKop3 = new Tekst(new HomKoord2D(kop3.XYA.X + 10, kop3.XYA.Y + 10), ff, "III", new SolidBrush(Color.Red));

            kop4 = new Kopce4agol(new HomKoord2D(530, 350, _SS, _VV));
            kop4.priGluvceKlik += new EventHandler(delegate(object sender, EventArgs e) {  pr.NovaIgra(4);  });
            tekKop4 = new Tekst(new HomKoord2D(kop4.XYA.X + 8, kop4.XYA.Y + 10), ff, "IV", new SolidBrush(Color.Red));

            OpsteRabote.PostaviKopceZvuk(kop1);
            OpsteRabote.PostaviKopceZvuk(kop2);
            OpsteRabote.PostaviKopceZvuk(kop3);
            OpsteRabote.PostaviKopceZvuk(kop4);
            OpsteRabote.PostaviKopceZvuk(kopcNazad);

            Elementi.Unesi(pozadina);

            Elementi.Unesi(kop1);
            Elementi.Unesi(kop2);
            Elementi.Unesi(kop3);
            Elementi.Unesi(kop4);

            Elementi.Unesi(tekKop1);
            Elementi.Unesi(tekKop2);
            Elementi.Unesi(tekKop3);
            Elementi.Unesi(tekKop4);

            Elementi.Unesi(kopcNazad);
            Elementi.Unesi(tekKopNaz);
        }
    }
}

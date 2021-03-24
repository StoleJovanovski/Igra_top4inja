using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Igra_so_topk4inja
{
    public static class OpsteRabote
    {
        public static Random rand = new Random();

        public static void Razmeni<T>(ref T x, ref T y)
        {
            T t = y;
            y = x;
            x = t;
        }

        public static float Kvadrat(float x)
        {
            return x * x;
        }

        public static Color Mesaj(Color b1, Color b2, float k)
        {
            return Color.FromArgb(
                (int)(b1.A + (b2.A - b1.A) * k),
                (int)(b1.R + (b2.R - b1.R) * k),
                (int)(b1.G + (b2.G - b1.G) * k),
                (int)(b1.B + (b2.B - b1.B) * k)
            );
        }

        private static System.Media.SoundPlayer zv1 = new System.Media.SoundPlayer("Smeni.wav");
        private static System.Media.SoundPlayer zv2 = new System.Media.SoundPlayer("Kopce1.wav");
        private static System.Media.SoundPlayer zv3 = new System.Media.SoundPlayer("Smeni.wav");
        public static void PostaviKopceZvuk(_2D_Grafika.KorisnickiInterfejs.Kopce4agol kop)
        {
            var aa = new EventHandler(delegate(object sender, EventArgs e) {
                if (ProstorIgra.Zvuk && !kop.pritisnato) zv1.Play();
            });
            kop.priGluvceVlez += aa;
            kop.priGluvceIzlez += aa;

            kop.priGluvceDole += new _2D_Grafika.Spravuvac.SpravuvacGluvce.FunkcijaSpravuvacGluvce(
                delegate(object povikuvac, System.Windows.Forms.MouseEventArgs arg, PointF T) {
                    if (ProstorIgra.Zvuk) zv2.Play();
                    return false;
                }
            );
            kop.priGluvceGore += new _2D_Grafika.Spravuvac.SpravuvacGluvce.FunkcijaSpravuvacGluvce(
                delegate(object povikuvac, System.Windows.Forms.MouseEventArgs arg, PointF T) {
                    if (ProstorIgra.Zvuk && kop.vnatre) zv3.Play();
                    return false;
                }
            );
        }
    }
}

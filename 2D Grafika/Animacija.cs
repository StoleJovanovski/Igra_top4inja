using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace _2D_Grafika
{
    public class Animacija
    {
        private long vreme;
        private int i;

        public Image[] Sliki { get; set; }
        public bool Kruzna { get; set; }
        public long VremePomegju { get; set; }

        public int Sirina { get; private set; }
        public int Visina { get; private set; }

        public event EventHandler KrajAnimacija;

        public Animacija(Image[] sliki, long vremePomegju = 32, bool kruzna = true)
        {
            Sliki = sliki;
            Kruzna = kruzna;
            VremePomegju = vremePomegju * 10000;

            foreach (Image im in sliki) {
                if (Sirina < im.Width ) Sirina = im.Width;
                if (Visina < im.Height) Visina = im.Height;
            }

            i = 0;
            vreme = DateTime.Now.Ticks;
        }

        public void Pocni(long vreme)
        {
            this.vreme = vreme;
            i = 0;
        }
        public void Zastani()
        {
            if (i >= 0) i = -1 - i;
        }

        public Image SlikaVoMoment(long vremenskiMoment)
        {
            if (i >= 0) {
                i = (int)((vremenskiMoment - vreme) / VremePomegju);
                if (Kruzna) i %= Sliki.Length;
                else if (i+1 != Sliki.Length) {
                    i = Sliki.Length - 1;
                    if (KrajAnimacija != null)
                        KrajAnimacija(this, null);
                }
                return Sliki[i];
            }
            return Sliki[1-i];
        }
    }
}

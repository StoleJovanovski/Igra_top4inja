using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PodatocneStrukture.Liste;
using System.Threading;

namespace _2D_Grafika
{
    public class Prostor : IDisposable
    {
        public Spravuvaci spravuvaci { get; private set; }
        public Pogled Pogled { get; set; }

        private Elementi.ElementSlozen  SegasenSvet { get; set; }
        private Lista<Elementi.PoseduvaVidlivost> vidliviElementi { get; set; }
        private Lista<Elementi.ElementAkciski>    akciskiElementi { get; set; }

        public int IntervalCrta   { get; set; } // 1/10 000 ms
        public int IntervalAkcija { get; set; }
        private Thread NitkaCrta, NitkaAkcija;
        public Mutex muxCrta, muxAkcija;

        public System.Drawing.Color BojaPozadina { get; set; }

        public Prostor()
        {
            spravuvaci = new Spravuvaci();
            spravuvaci.DodadiSpravuvac(new Spravuvac.SpravuvacGluvce());
            spravuvaci.DodadiSpravuvac(new Spravuvac.SpravuvacTastatura());
            spravuvaci.DodadiSpravuvac(new Spravuvac.SpravuvacZatvori());
            ((Spravuvac.SpravuvacZatvori)spravuvaci.VratiSpravuvac(typeof(Spravuvac.SpravuvacZatvori))).DodadiIzvrsiZatvori(
                delegate(object povikuvac, object arg) { Dispose(); });

            IntervalCrta = 32;
            IntervalAkcija = 10;

            vidliviElementi = new ListaNiza<Elementi.PoseduvaVidlivost>();
            akciskiElementi = new ListaNiza<Elementi.ElementAkciski>();

            (muxCrta = new Mutex()).WaitOne();
            (muxAkcija = new Mutex()).WaitOne();
            (NitkaCrta = new Thread(Crtaj)).Start();
            (NitkaAkcija = new Thread(Akcija)).Start();
            //NitkaCrta.Priority = ThreadPriority.AboveNormal;
            //NitkaAkcija.Priority = ThreadPriority.AboveNormal;

            BojaPozadina = System.Drawing.Color.White;

            SegasenSvet = new Elementi.ElementSlozen();
        }

        public void Dodadi(Elementi.ElementProst e)
        {
            SegasenSvet.Elementi.Unesi(e);
            if (e is Elementi.PoseduvaNastan) ((Elementi.PoseduvaNastan)e).DodadiSpravuvac(spravuvaci);
            if (e is Elementi.PoseduvaVidlivost) vidliviElementi.Unesi((Elementi.PoseduvaVidlivost)e);
            if (e is Elementi.ElementAkciski) akciskiElementi.Unesi((Elementi.ElementAkciski)e);
            if (e is Elementi.ElementSlozen)
                foreach (Elementi.ElementProst ee in ((Elementi.ElementSlozen)e).Elementi)
                    Dodadi(ee);
        }
        public void Izbrisi(Elementi.ElementProst e)
        {
            SegasenSvet.Elementi.Izbrisi(e);
            if (e is Elementi.PoseduvaNastan) ((Elementi.PoseduvaNastan)e).IzbrisiSpravuvac(spravuvaci);
            if (e is Elementi.PoseduvaVidlivost) vidliviElementi.Izbrisi((Elementi.PoseduvaVidlivost)e);
            if (e is Elementi.ElementAkciski)
                foreach (Elementi.ElementAkciski el in akciskiElementi)
                    if (el.Roditel == e)
                        el.Prekini();
            if (e is Elementi.ElementSlozen)
                foreach (Elementi.ElementProst ee in ((Elementi.ElementSlozen)e).Elementi)
                    Izbrisi(ee);
        }

        public void ObnoviPogled()
        {
            //muxCrta.ReleaseMutex();
        }

        public void Crtaj()
        {
            long ts, tt, t = DateTime.Now.Ticks;

            for(;;) {
                muxCrta.WaitOne();
                if ((tt = IntervalCrta + t - (t = DateTime.Now.Ticks)) > 0) Thread.Sleep((int)(tt / 10000L));

                ts = DateTime.Now.Ticks;

                Pogled.Kalibriraj();
                Pogled.Grafik.Clear(BojaPozadina);
                for (int i = 0; i < vidliviElementi.Kolicina; ++i)
                        vidliviElementi[i].Crtaj(Pogled, ts);
                Pogled.Prikazi();
            }
        }
        public void Akcija()
        {
            int i;
            Elementi.ElementAkciski e;
            Lista<Elementi.ElementAkciski> l;
            long dt, tt, t = DateTime.Now.Ticks;
            float dT;

            for(;;) {
                muxAkcija.WaitOne();
                dt = t;
                if ((tt = IntervalCrta + t - (t = DateTime.Now.Ticks)) > 0) Thread.Sleep((int)(tt / 10000L));

                dt = DateTime.Now.Ticks - dt;
                dT = dt * 0.0000001F;

                for (i = akciskiElementi.Kolicina - 1; i >= 0; --i)
                    switch ((e = akciskiElementi[i]).sostojba) {
                        case Elementi.ElementAkciski.Sostojba.NOV:           e.PriInicijal();  break;
                        case Elementi.ElementAkciski.Sostojba.KOMPLETIRAN:  e.PriKomplet();  break;
                        case Elementi.ElementAkciski.Sostojba.NEUSPESEN:  e.PriNeuspeh();  break;
                        case Elementi.ElementAkciski.Sostojba.PREKINAT:  e.PriPrekin();  break;
                        case Elementi.ElementAkciski.Sostojba.RABOTI:
                            l = e.Akcija(this, dt, dT);
                            if (l != null)  akciskiElementi.Unesi(l);
                            break;
                        case Elementi.ElementAkciski.Sostojba.ISKLUCEN: akciskiElementi.IzbrisiNa(i); break;
                    }
            }
        }

        public void Dispose()
        {
            NitkaCrta.Abort();
            NitkaAkcija.Abort();
        }
    }
}

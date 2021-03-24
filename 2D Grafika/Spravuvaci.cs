using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PodatocneStrukture.Liste;
using System.Drawing;

namespace _2D_Grafika
{
    /// <summary>
    /// Колекција од сите справувачи
    /// </summary>
    public class Spravuvaci
    {
        private Dictionary<Type, Spravuvac> dd;

        public Spravuvaci()
        {
            dd = new Dictionary<Type, Spravuvac>();
        }

        public Spravuvac VratiSpravuvac(Type tipSprav)
        {
            if (dd.ContainsKey(tipSprav)) return dd[tipSprav];
            return null;
        }

        public void DodadiSpravuvac(Spravuvac nastan)
        {
            dd.Add(nastan.GetType(), nastan);
        }
        public void IzbrisiSpravuvac(Spravuvac nastan)
        {
            dd.Remove(nastan.GetType());
        }
    }

    /// <summary>
    /// Класа која чува повеке настани со кои се СПРАВУВА (со кои ракува)
    /// кога ке се изврши нивниот родителски настан (настан при кој треба да се извршат и тие.
    /// </summary>
    public abstract class Spravuvac
    {

        public class SpravuvacGluvce : Spravuvac
        {
            public delegate bool FunkcijaSpravuvacGluvce(object povikuvac, MouseEventArgs arg, PointF T);

            private Lista<FunkcijaSpravuvacGluvce> gluvceDvizi;
            private Lista<FunkcijaSpravuvacGluvce> gluvceGore;
            private Lista<FunkcijaSpravuvacGluvce> gluvceDole;

            public SpravuvacGluvce()
            {
                gluvceDvizi = new ListaNiza<FunkcijaSpravuvacGluvce>();
                gluvceGore = new ListaNiza<FunkcijaSpravuvacGluvce>();
                gluvceDole = new ListaNiza<FunkcijaSpravuvacGluvce>();
            }

            public bool DodadiGluvceDvizi(FunkcijaSpravuvacGluvce eh)
            {
                if (gluvceDvizi.SodrziLi(eh)) return false;
                gluvceDvizi.Unesi(eh);
                return true;
            }
            public bool DodadiGluvceGore(FunkcijaSpravuvacGluvce eh)
            {
                if (gluvceGore.SodrziLi(eh)) return false;
                gluvceGore.Unesi(eh);
                return true;
            }
            public bool DodadiGluvceDole(FunkcijaSpravuvacGluvce eh)
            {
                if (gluvceDole.SodrziLi(eh)) return false;
                gluvceDole.Unesi(eh);
                return true;
            }

            public bool IzbrisiGluvceDvizi(FunkcijaSpravuvacGluvce eh)
            {
                return gluvceDvizi.Izbrisi(eh);
            }
            public bool IzbrisiGluvceGore(FunkcijaSpravuvacGluvce eh)
            {
                return gluvceGore.Izbrisi(eh);
            }
            public bool IzbrisiGluvceDole(FunkcijaSpravuvacGluvce eh)
            {
                return gluvceDole.Izbrisi(eh);
            }

            public void GluvceDvizi(object povikuvac, MouseEventArgs arg, PointF T)
            {
                for (int i = gluvceDvizi.Kolicina - 1; i >= 0; --i)
                    if (i < gluvceDvizi.Kolicina && gluvceDvizi[i](povikuvac, arg, T))
                        return;
            }
            public void GluvceGore(object povikuvac, MouseEventArgs arg, PointF T)
            {
                for (int i = gluvceGore.Kolicina - 1; i >= 0; --i)
                    if (i < gluvceGore.Kolicina && gluvceGore[i](povikuvac, arg, T))
                        return;
            }
            public void GluvceDole(object povikuvac, MouseEventArgs arg, PointF T)
            {
                for (int i = gluvceDole.Kolicina - 1; i >= 0; --i)
                    if (i < gluvceDole.Kolicina && gluvceDole[i](povikuvac, arg, T))
                        return;
            }
        }

        public class SpravuvacTastatura : Spravuvac
        {
            public delegate bool FunkcijaSpravuvacTastatura(object sender, KeyEventArgs e);

            private Lista<FunkcijaSpravuvacTastatura> kopcePritisnato;

            public SpravuvacTastatura()
            {
                kopcePritisnato = new ListaNiza<FunkcijaSpravuvacTastatura>();
            }

            public bool DodadiKopcePritisnato(FunkcijaSpravuvacTastatura eh)
            {
                if (kopcePritisnato.SodrziLi(eh)) return false;
                kopcePritisnato.Unesi(eh);
                return true;
            }

            public bool IzbrisiKopcePritisnato(FunkcijaSpravuvacTastatura eh)
            {
                return kopcePritisnato.Izbrisi(eh);
            }

            public void KopcePritisnato(object povikuvac, KeyEventArgs arg)
            {
                for (int i = kopcePritisnato.Kolicina - 1; i >= 0; --i)
                    if (i < kopcePritisnato.Kolicina && kopcePritisnato[i](povikuvac, arg))
                        return;
            }
        }

        public class SpravuvacZatvori : Spravuvac
        {
            public delegate bool FunkcijaSpravuvacObidZatvori(object povikuvac, object arg);
            public delegate void FunkcijaSpravuvacIzvrsiZatvori(object povikuvac, object arg);

            private Lista<FunkcijaSpravuvacObidZatvori> obidZatvori;
            private Lista<FunkcijaSpravuvacIzvrsiZatvori> izvrsiZatvori;

            public SpravuvacZatvori()
            {
                obidZatvori = new ListaNiza<FunkcijaSpravuvacObidZatvori>();
                izvrsiZatvori = new ListaNiza<FunkcijaSpravuvacIzvrsiZatvori>();
            }

            public bool DodadiObidZatvori(FunkcijaSpravuvacObidZatvori eh)
            {
                if (obidZatvori.SodrziLi(eh)) return false;
                obidZatvori.Unesi(eh);
                return true;
            }
            public bool DodadiIzvrsiZatvori(FunkcijaSpravuvacIzvrsiZatvori eh)
            {
                if (izvrsiZatvori.SodrziLi(eh)) return false;
                izvrsiZatvori.Unesi(eh);
                return true;
            }

            public bool IzbrisiObidZatvori(FunkcijaSpravuvacObidZatvori eh)
            {
                return obidZatvori.Izbrisi(eh);
            }
            public bool IzbrisiIzvrsiZatvori(FunkcijaSpravuvacIzvrsiZatvori eh)
            {
                return izvrsiZatvori.Izbrisi(eh);
            }

            public bool ObidZatvori(object povikuvac, object arg)
            {
                for (int i = obidZatvori.Kolicina - 1; i >= 0; --i)
                    if (i < obidZatvori.Kolicina && !obidZatvori[i](povikuvac, arg))
                        return false;

                #pragma warning disable
                IzvrsiZatvori(povikuvac, arg);
                return true;
            }
            [Obsolete("Нема да се извршат соодветните проверки претходно, ке се изврши ДИРЕКТНО затворање ! ! !")]
            public void IzvrsiZatvori(object povikuvac, object arg)
            {
                for (int i = izvrsiZatvori.Kolicina - 1; i >= 0; --i)
                    if (i < izvrsiZatvori.Kolicina)
                        izvrsiZatvori[i](povikuvac, arg);
            }
        }
    }

    /*
    public class Slusac<T> where T : System.Delegate
    {
        private Lista<T> ll;

        public Slusac()
        {
            ll = new ListaNiza<T>();
        }

        public bool DodadiNastan(T nn)
        {
            if (ll.SodrziLi(nn)) return false;
            ll.Unesi(nn);
            return true;
        }

        public bool IzbrisiNastan(T nn)
        {
            return ll.Izbrisi(nn);
        }

        public void Izvrsi(object[] parametri)
        {
            foreach (T nn in ll)
                nn.DynamicInvoke(parametri);
        }
    }  //*/
}


using System.Windows.Forms;
namespace Igra_so_topk4inja
{
    public class ProstorIgra : _2D_Grafika.Prostor
    {
        private static WMPLib.WindowsMediaPlayer zvukPozd;
        private static Timer tmr;

        private static bool zvuk = false;
        public static bool Zvuk
        {
            get { return ProstorIgra.zvuk; }
            set {
                if (ProstorIgra.zvuk = value) tmr.Start();
                else zvukPozd.controls.stop();
            }
        }

        private PocetenEkran pocEk;
        private GlavnaIgra glIg;
        private Mapa map;

        static ProstorIgra()
        {
            zvukPozd = new WMPLib.WindowsMediaPlayer() { URL = "Pozadina.wav" };
            zvukPozd.controls.stop();
            zvukPozd.settings.volume = 10;

            tmr = new Timer();
            tmr.Stop();

            tmr.Tick += new System.EventHandler(delegate(object sender, System.EventArgs e) {
                zvukPozd.controls.play();
                tmr.Stop();
            });

            zvukPozd.PlayStateChange += new WMPLib._WMPOCXEvents_PlayStateChangeEventHandler(delegate(int NewState) {
                if (NewState == (int)WMPLib.WMPPlayState.wmppsMediaEnded) tmr.Start();
            });
        }

        public ProstorIgra()
        {
            pocEk = new PocetenEkran(this);
            glIg = new GlavnaIgra(this);
            map = new Mapa(this);

            Dodadi(new ProveriZatvori(this));

            PocetenEkran();
        }

        public void PocetenEkran()
        {
            Dodadi(pocEk);
            Izbrisi(map);
        }
        public void Mapa()
        {
            Dodadi(map);
            Izbrisi(glIg);
            Izbrisi(pocEk);
        }
        public void NovaIgra(int i)
        {
            glIg.Resetiraj(i);
            Dodadi(glIg);
            Izbrisi(map);
        }

    }
}

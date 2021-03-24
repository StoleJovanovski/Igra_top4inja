using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _2D_Grafika
{
    public partial class Prozor
    {
        public Pogled pogled { get; set; }
        Control control;

        public Prozor(Control cont, Prostor prostor, bool glMan = true)
        {
            control = cont;
            pogled = prostor.Pogled = new Pogled(cont.CreateGraphics(), new HomKoord2D());

            prostor.muxCrta.ReleaseMutex();
            prostor.muxAkcija.ReleaseMutex();

            cont.Resize += new EventHandler(delegate(object sender, EventArgs e) {
                prostor.Pogled.Grf = ((Control)sender).CreateGraphics();
            });
            cont.Paint += new PaintEventHandler(delegate(object sender, PaintEventArgs e) {
                prostor.ObnoviPogled();
              //prostor.Pogled.Prikazi();
            });

            Spravuvac.SpravuvacGluvce gl = (Spravuvac.SpravuvacGluvce)prostor.spravuvaci.VratiSpravuvac(typeof(Spravuvac.SpravuvacGluvce));
            cont.MouseMove += new MouseEventHandler(delegate(object povikuvac, MouseEventArgs arg) {
                gl.GluvceDvizi(povikuvac, arg, prostor.Pogled.XYA.Inverzna() * arg.Location);
            });
            cont.MouseUp   += new MouseEventHandler(delegate(object povikuvac, MouseEventArgs arg) {
                gl.GluvceGore(povikuvac, arg, prostor.Pogled.XYA.Inverzna() * arg.Location);
            });
            cont.MouseDown += new MouseEventHandler(delegate(object povikuvac, MouseEventArgs arg) {
                gl.GluvceDole(povikuvac, arg, prostor.Pogled.XYA.Inverzna() * arg.Location);
            });;
            
            if (glMan) gl.DodadiGluvceDvizi(Control_MouseMove);

            cont.KeyDown += new KeyEventHandler(((Spravuvac.SpravuvacTastatura)prostor.spravuvaci.VratiSpravuvac(typeof(Spravuvac.SpravuvacTastatura))).KopcePritisnato);
            
            if (cont is Form) {
                bool bb = true;
                Form ff = (Form)cont;
                Spravuvac.SpravuvacZatvori ssz = (Spravuvac.SpravuvacZatvori)prostor.spravuvaci.VratiSpravuvac(typeof(Spravuvac.SpravuvacZatvori));
                ssz.DodadiObidZatvori(delegate(object povikuvac, object arg) {
                    //System.Media.SystemSounds.Asterisk.Play();
                    return true;
                });
                ff.FormClosing += new FormClosingEventHandler(delegate(object povikuvac, FormClosingEventArgs arg) {
                    if (bb) arg.Cancel = !ssz.ObidZatvori(povikuvac, arg);
                });
                ssz.DodadiIzvrsiZatvori(delegate(object povikuvac, object arg) {
                    bb = false;
                    ff.Close();
                    bb = true;
                });
            }
        }

        private Point P;
        public bool Control_MouseMove(object sender, MouseEventArgs e, PointF T)
        {
            if (e.Button != MouseButtons.None) {
                Control cont = (Control)sender;

                if (((int)e.Button & (int)MouseButtons.Left) != 0) {
                    if (((int)e.Button & (int)MouseButtons.Middle) != 0)
                            pogled.XYA =
                                HomKoord2D.T(+0.5F * cont.Width, +0.5F * cont.Height) *
                                HomKoord2D.S((float)Math.Exp(0.01F * (P.Y - e.Y))) *
                                HomKoord2D.T(-0.5F * cont.Width, -0.5F * cont.Height) *
                                pogled.XYA;
                    else    pogled.XYA.Transliraj(e.X - P.X, e.Y - P.Y);

                    P = e.Location;
                    return true;
                }

                if (((int)e.Button & (int)MouseButtons.Right) != 0)
                {
                    //*
                    pogled.XYA.Transliraj(-0.5F * cont.Width, -0.5F * cont.Height);
                    pogled.XYA = HomKoord2D.R(Math.Atan2(e.Y -0.5F*cont.Height, e.X -0.5F*cont.Width) - Math.Atan2(P.Y -0.5F*cont.Height, P.X -0.5F*cont.Width)) * pogled.XYA;
                    pogled.XYA.Transliraj(+0.5F * cont.Width, +0.5F * cont.Height);

                    //*/  prostor.Pogled.XYA.Rotiraj(Math.Atan2(e.Y -0.5F*control.Height, e.X -0.5F*control.Width) - Math.Atan2(P.Y -0.5F*control.Height, P.X -0.5F*control.Width));
                
                    P = e.Location;
                    return true;
                }
            }

            P = e.Location;
            return false;
        }
    }
}
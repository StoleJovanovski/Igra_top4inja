using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace _2D_Grafika.Ureducaci
{
    public partial class Slozen : Form
    {
        private Action Promena;
        private Elementi.ElementSlozen element;

        private PodatocneStrukture.Liste.Lista<Form> ureduvaci;

        public Slozen(Elementi.ElementSlozen element, Action Promena, string imeElement)
        {
            InitializeComponent();

            this.element = element;
            this.Promena = Promena;
            this.Text = imeElement;

            int n = element.Elementi.Kolicina;
            ureduvaci = new PodatocneStrukture.Liste.ListaNiza<Form>(n);
            for (int i = 0; i < n; ++i)
                ureduvaci.Unesi((Form)null);

            textBox1.Text = n.ToString();
            listBox1.Items.AddRange(element.Elementi.ToArray());
            listBox1.SelectedIndex = -1;
        }

        public void MojaPromena()
        {
            listBox1.DisplayMember = "D";
            listBox1.DisplayMember = "";
            Promena();
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1) {
                //((ProstElement)listBox1.SelectedItem).Ureduvac(MojaPromena, listBox1.SelectedIndex.ToString()).Show();
                KreirajUreduvac(listBox1.SelectedIndex);
                listBox1.SelectedIndex = -1;
            }
        }
        private void Dodadi_Click(object sender, EventArgs e)
        {
            KreiranjeNovEl Ke = new KreiranjeNovEl(null);
            Ke.ShowDialog();
            if (Ke.E == null) return;
            element.Elementi.Unesi(Ke.E);
            Promena();
            listBox1.Items.Add(Ke.E);
            ureduvaci.Unesi((Form)null);
            KreirajUreduvac(ureduvaci.Kolicina-1);
            //Ke.E.Ureduvac(MojaPromena, (listBox1.Items.Count - 1).ToString()).Show();
        }
        private void Izbrisi_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1 && MessageBox.Show("Дали сакаш да га избришеш елемент :\n" + listBox1.SelectedItem.ToString(), "Бришање елемент !", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) {
                element.Elementi.Izbrisi((Elementi.ElementProst)listBox1.SelectedItem);
                if (ureduvaci[listBox1.SelectedIndex] != null) ureduvaci[listBox1.SelectedIndex].Close();
                ureduvaci.IzbrisiNa(listBox1.SelectedIndex);

                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                Promena();
            }
        }

        public void KreirajUreduvac(int i)
        {
            Form ff = ureduvaci[i];
            if (ff == null) {
                #warning  da se trgnev ureduva4i
                //ff = element.Elementi[i].Ureduvac(MojaPromena, listBox1.SelectedIndex.ToString());
                ff.Tag = i;
                ff.FormClosed += new FormClosedEventHandler(delegate(object send, FormClosedEventArgs e1) {
                    ureduvaci[(int)((Form)send).Tag] = null;
                });
                (ureduvaci[i] = ff).Show();
            } else
                ff.Activate();
        }
    }
}

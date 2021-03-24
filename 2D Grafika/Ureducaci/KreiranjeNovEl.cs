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
    public partial class KreiranjeNovEl : Form
    {
        private Prostor prostor;

        public Elementi.ElementProst E { get; set; }

        public KreiranjeNovEl(Prostor prostor)
        {
            InitializeComponent();

            this.prostor = prostor;
            E = null;

            var instances =
                from t in System.Reflection.Assembly.GetExecutingAssembly().GetTypes()
                where t.IsClass && t.IsSubclassOf(typeof(Elementi.ElementProst))// && t.GetConstructor(Type.EmptyTypes) != null
                select t;//Activator.CreateInstance(t) as ProstElement3D;

            listBox1.Items.AddRange(instances.ToArray());

            //foreach (Type mytype in System.Reflection.Assembly.GetExecutingAssembly().GetTypes().Where(mytype => mytype.GetInterfaces().Contains(typeof(ProstElement3D))))
                //do stuff
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) return;
            E = (Elementi.ElementProst)((Type)listBox1.SelectedItem).GetMethod("Kreiraj").Invoke(null, new object[] { prostor });
            Close();
        }
    }
}

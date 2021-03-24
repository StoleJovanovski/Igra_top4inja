using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using _2D_Grafika;

namespace Igra_so_topk4inja
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            new Prozor(this, new ProstorIgra(), false);

            //new Prozor(panel1, prostor);
        }
    }
}
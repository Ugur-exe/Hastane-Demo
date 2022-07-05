using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hastane_Demo
{
    public partial class Hastane_Otomasyon_Sistemi : Form
    {
        public Hastane_Otomasyon_Sistemi()
        {
            InitializeComponent();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            Hasta_Kabul yeni = new Hasta_Kabul();
            yeni.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Hasta_Giriş a = new Hasta_Giriş();
            a.Show();
           
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            YoneticiGiris yonetici = new YoneticiGiris();
           
            yonetici.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DoktorGiris gir = new DoktorGiris();
            gir.Show();
        }
    }
}

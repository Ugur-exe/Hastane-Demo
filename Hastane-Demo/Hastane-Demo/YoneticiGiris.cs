using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Hastane_Demo
{
    public partial class YoneticiGiris : Form
    {
        string sifre,tc;
        
        public YoneticiGiris()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-U0HV2JK;Initial Catalog=Hastane;Integrated Security=True");

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text==""||textBox2.Text=="")
            {
                MessageBox.Show("Boş Alan Bırakılamaz");
            }
            else
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select *from Yonetici where Tc=@Tc", baglanti);
                komut.Parameters.AddWithValue("@Tc", Convert.ToInt64(textBox1.Text));
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    sifre = oku["Sifre"].ToString();
                    tc = oku["Tc"].ToString();
                }
                baglanti.Close();
                if (Convert.ToInt64(textBox1.Text)==Convert.ToInt64(tc)&&textBox2.Text==sifre)
                {
                    this.Close();
                    Yonetici yonetici = new Yonetici();
                    yonetici.Show();
                    textBox1.Clear(); textBox2.Clear();

                }
                else
                    MessageBox.Show("Tc kimlik numarası veya şifre yanlış!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
                
            }
            
            
        }
    }
}

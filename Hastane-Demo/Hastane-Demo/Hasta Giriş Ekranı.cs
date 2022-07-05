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
    
    public partial class Hasta_Giriş : Form
    {
        string sifre, tc;
        public static long id;
        SatirKontrol a=new SatirKontrol();
        public Hasta_Giriş()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-U0HV2JK;Initial Catalog=Hastane;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {
            Kayit_Ekrani yeni=new Kayit_Ekrani();
            yeni.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Sifremi_Unuttum sifre = new Sifremi_Unuttum();
            sifre.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            a.kontrol(textBox1.Text);
            if (a.check == false)
            {
                MessageBox.Show("Tc Kimlik Numarası 11 Haneli Olmalıdır", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show("Lütfen boş alanları doldurun!", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("Select *from Hasta_Kayit where Tc =@Tc ", baglanti);
                    komut.Parameters.AddWithValue("@Tc", textBox1.Text);
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        sifre = oku["Sifre"].ToString();
                        tc = oku["Tc"].ToString();

                    }
                    baglanti.Close();

                    
                    if (Convert.ToInt64(tc) == Convert.ToInt64(textBox1.Text) && textBox2.Text == sifre)
                    {
                        id = Convert.ToInt64(textBox1.Text);
                        Giris a = new Giris();
                        a.Show();

                    }
                    else
                        MessageBox.Show("Tc kimlik numarası veya şifre yanlış!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.Clear();textBox2.Clear();
                    this.Close();
                }
            }
        }
    }
}

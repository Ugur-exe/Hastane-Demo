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
    public partial class DoktorGiris : Form
    {
        string tc, sifre;
        public static long id;
        public DoktorGiris()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-U0HV2JK;Initial Catalog=Hastane;Integrated Security=True");
        private void button1_Click(object sender, EventArgs e)
        {

            if (textBox1.Text == "" || textBox2.Text == "")
            {
                MessageBox.Show("Boş Bir Alan Bırakılamaz");
            }
            else
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Select *From Doktorlar Where Tc=@Tc", baglanti);
                komut.Parameters.AddWithValue("@Tc", Convert.ToInt64(textBox1.Text));
                SqlDataReader oku = komut.ExecuteReader();
                while (oku.Read())
                {
                    tc = oku["Tc"].ToString();
                    sifre = oku["Sifre"].ToString();
                }
                baglanti.Close();
            }
            if (textBox1.Text==tc && textBox2.Text==sifre)
            {
                id = Convert.ToInt64(textBox1.Text);
                Doktor doktor = new Doktor();
                doktor.Show();
                textBox1.Clear();textBox2.Clear();
                this.Close();
            }else
                MessageBox.Show("Tc kimlik numarası veya şifre yanlış!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);


        }
    }
}

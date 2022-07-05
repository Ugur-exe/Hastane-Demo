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
    public partial class RandevuAl : Form
    {
        long id = Hasta_Giriş.id;
        public RandevuAl()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-U0HV2JK;Initial Catalog=Hastane;Integrated Security=True");

        private void RandevuAl_Load(object sender, EventArgs e)
        {
            textBox1.Text = id.ToString();
            textBox1.Enabled = false;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select DISTINCT Klinik From Doktorlar ", baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox1.Items.Add(oku["Klinik"].ToString());
            }
            baglanti.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select *From Doktorlar Where Klinik=@Klinik", baglanti);
            komut.Parameters.AddWithValue("@Klinik", comboBox1.Text);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                comboBox2.Items.Add(oku["Ad"].ToString() +" "+oku["Soyad"].ToString());
            }
            baglanti.Close();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text==""||comboBox2.Text==""||comboBox3.Text==""||textBox1.Text==""||maskedTextBox1.Text=="")
            {
                MessageBox.Show("Hiçbir Alan Boş Geçilemez","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            else
            {
                try
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("insert into Randevu (Tc,Klinik,Doktor,Tarih,Saat) values (@Tc,@Klinik,@Doktor,@Tarih,@Saat)", baglanti);
                    komut.Parameters.AddWithValue("@Tc", Convert.ToInt64(textBox1.Text));
                    komut.Parameters.AddWithValue("@Klinik", comboBox1.Text);
                    komut.Parameters.AddWithValue("@Doktor", comboBox2.Text);
                    komut.Parameters.AddWithValue("@Tarih", maskedTextBox1.Text);
                    komut.Parameters.AddWithValue("@Saat", comboBox3.Text);
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    MessageBox.Show("Randevu Başarı İle Oluşturuldu.", "Randevu", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Bu Saat için Randevu Doludur","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                }
                
            }    
        }
    }
}

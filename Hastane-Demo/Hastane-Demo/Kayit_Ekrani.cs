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
    public partial class Kayit_Ekrani : Form
    {
        public Kayit_Ekrani()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-U0HV2JK;Initial Catalog=Hastane;Integrated Security=True");

        SatirKontrol a = new SatirKontrol();
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            a.kontrol(textBox11.Text);
            if (a.check == false)
            {
                MessageBox.Show("Tc Kimlik Numarası 11 Haneli Olmalıdır", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string cinsiyet;
                if (radioButton1.Checked)
                {
                    cinsiyet = "Erkek";
                }
                else
                    cinsiyet = "Kadın";


                if (textBox1.Text == "" || textBox2.Text == "" || comboBox1.Text == "" || richTextBox1.Text == ""
                    || maskedTextBox1.Text == "" || textBox7.Text == "" || textBox8.Text == "" || comboBox2.Text == "" ||
                    textBox10.Text == "" || textBox11.Text == "" || textBox12.Text == "")
                {
                    MessageBox.Show("Hiçbir Alan Boş Geçilemez");
                }
                else
                {
                    try
                    {
                        baglanti.Open();
                        SqlCommand komut = new SqlCommand("insert into Hasta_Kayit ( Tc,Ad,Soyad,Cinsiyet,Kan_Grubu,DogumTarihi,DogumYeri,AnneAdi,BabaAdi,CepTelefon,EPosta,Adres,Sifre) values(@Tc,@Ad,@Soyad,@Cinsiyet,@Kan_Grubu,@DogumTarihi,@DogumYeri,@AnneAdi,@BabaAdi,@CepTelefon,@Eposta,@Adres,@Sifre)", baglanti);
                        komut.Parameters.AddWithValue("@Ad", textBox1.Text);
                        komut.Parameters.AddWithValue("@Soyad", textBox2.Text);
                        komut.Parameters.AddWithValue("@Cinsiyet", cinsiyet);
                        komut.Parameters.AddWithValue("@Kan_Grubu", comboBox1.SelectedItem);
                        komut.Parameters.AddWithValue("@DogumTarihi", dateTimePicker1.Value);
                        komut.Parameters.AddWithValue("@Dogumyeri", comboBox2.Text);
                        komut.Parameters.AddWithValue("Anneadi", textBox7.Text);
                        komut.Parameters.AddWithValue("@BabaAdi", textBox8.Text);
                        komut.Parameters.AddWithValue("@CepTelefon", maskedTextBox1.Text);
                        komut.Parameters.AddWithValue("@EPosta", textBox10.Text);
                        komut.Parameters.AddWithValue("@Adres", richTextBox1.Text);
                        komut.Parameters.AddWithValue("@Tc", Convert.ToInt64(textBox11.Text));
                        komut.Parameters.AddWithValue("@Sifre", textBox12.Text);
                        komut.ExecuteNonQuery();

                        baglanti.Close();
                        MessageBox.Show("Kayıt Başarı ile gerçekleşti");
                    }
                    catch (System.FormatException)
                    {
                        MessageBox.Show("Girişleri lütfen sadece Rakam veya harf olarak giriniz");
                        baglanti.Close();

                    }
                }


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            a.kontrol(textBox1.Text);
            if (a.check == false)
            {
                MessageBox.Show("Tc Kimlik Numarası 11 Haneli Olmalıdır", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select *from Hasta_Kayit where Tc like'" + textBox11.Text + "'", baglanti);
                SqlDataReader read = komut.ExecuteReader();
                while (read.Read())
                {
                    textBox1.Text = read["Ad"].ToString();
                    textBox2.Text = read["Soyad"].ToString();
                    //textBox3.Text = read[].ToString();
                    //textBox4.Text = read[].ToString();
                    dateTimePicker1.Text = read["DogumTarihi"].ToString();
                    comboBox2.Text = read["Dogumyeri"].ToString();
                    textBox7.Text = read["Anneadi"].ToString();
                    textBox8.Text = read["BabaAdi"].ToString();
                    maskedTextBox1.Text = read["CepTelefon"].ToString();
                    textBox10.Text = read["EPosta"].ToString();
                    richTextBox1.Text = read["Adres"].ToString();
                    comboBox1.Text = read["Kan_Grubu"].ToString();
                    string cinsiyet = read["Cinsiyet"].ToString();
                    bool a = cinsiyet.StartsWith("E");

                    if (a == true)
                    {
                        radioButton1.PerformClick();
                    }
                    else
                    {
                        radioButton2.PerformClick();
                    }
                }
                baglanti.Close();

            }
   
        }

        

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show(maskedTextBox1.Text);
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }
    }
}

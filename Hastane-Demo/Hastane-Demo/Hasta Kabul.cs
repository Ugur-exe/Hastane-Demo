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
    public partial class Hasta_Kabul : Form
    {
        public Hasta_Kabul()
        {
            InitializeComponent();
        }
        SqlConnection baglanti=new SqlConnection(@"Data Source=DESKTOP-U0HV2JK;Initial Catalog=Hastane;Integrated Security=True");
        string cinsiyet;
        SatirKontrol a = new SatirKontrol();
        
        private void button1_Click(object sender, EventArgs e)
        {
            a.kontrol(textBox1.Text);
            if (a.check==false)
            {
                MessageBox.Show("Tc Kimlik Numarası 11 Haneli Olmalıdır", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == ""
               || textBox6.Text == "" || comboBox1.Text == "" || comboBox2.Text == "" || maskedTextBox1.Text == "" || richTextBox1.Text == "")
                {
                    MessageBox.Show("HiçBir Alan Boş Bırakılamaz!");
                }
                else
                {
                    if (radioButton1.Checked)
                    {
                        cinsiyet = "Erkek";
                    }
                    else
                        cinsiyet = "Kadın";
                    try
                    {
                        baglanti.Open();
                        SqlCommand komut = new SqlCommand("insert into Hasta_Kabul (Tc,Ad,Soyad,Cinsiyet,Kan_Grubu,DogumTarihi,DogumYeri,AnneAdi,BabaAdi,CepTelefon,EPosta,Adres) values(@Tc,@Ad,@Soyad,@Cinsiyet,@Kan_Grubu,@DogumTarihi,@DogumYeri,@AnneAdi,@BabaAdi,@CepTelefon,@EPosta,@Adres)", baglanti);
                        komut.Parameters.AddWithValue("@Tc", Convert.ToInt64(textBox1.Text));
                        komut.Parameters.AddWithValue("@Ad", textBox2.Text);
                        komut.Parameters.AddWithValue("@Soyad", textBox3.Text);
                        komut.Parameters.AddWithValue("@Cinsiyet", cinsiyet);
                        komut.Parameters.AddWithValue("@Kan_Grubu", comboBox1.Text);
                        komut.Parameters.AddWithValue("@DogumTarihi", dateTimePicker1.Value);
                        komut.Parameters.AddWithValue("@Dogumyeri", comboBox2.Text);
                        komut.Parameters.AddWithValue("@AnneAdi", textBox4.Text);
                        komut.Parameters.AddWithValue("@BabaAdi", textBox5.Text);
                        komut.Parameters.AddWithValue("@CepTelefon", maskedTextBox1.Text);
                        komut.Parameters.AddWithValue("@EPosta", textBox6.Text);
                        komut.Parameters.AddWithValue("@Adres", richTextBox1.Text);
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Aynı Kimlik Numarası Zaten Kayıtlı");
                        
                    }
                    

                }
            }
            
        }
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear(); textBox5.Clear(); textBox6.Clear();
            maskedTextBox1.Clear();richTextBox1.Clear();comboBox1.Text = "";comboBox2.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
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

        private void Hasta_Kabul_Load(object sender, EventArgs e)
        {
            textBox1.MaxLength = 11;
        }
    }
}

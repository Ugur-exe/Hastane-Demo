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
    public partial class Yonetici : Form
    {
        public Yonetici()
        {
            InitializeComponent();
        }
        string tc;
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-U0HV2JK;Initial Catalog=Hastane;Integrated Security=True");
        void textboxtemizle()
        {
            textBox1.Clear(); textBox2.Clear(); textBox3.Clear();textBox4.Clear();textBox5.Clear();textBox6.Clear();
            textBox7.Clear() ; textBox8.Clear(); textBox9.Clear(); textBox10.Clear();textBox11.Clear();textBox12.Clear();textBox13.Clear();
            textBox14.Clear();textBox15.Clear();textBox15.Clear();textBox16.Clear();textBox17.Clear();
            comboBox1.Text="";comboBox2.Text="";richTextBox1.Clear();maskedTextBox1.Clear();
            
        }
        void bul()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select *From Hasta_Kayit Where Tc=@Tc", baglanti);
            komut.Parameters.AddWithValue("@Tc", Convert.ToInt64(textBox17.Text));
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                tc = oku["Tc"].ToString();
            }
            baglanti.Close();

        }
        void YoneticiVerileri()
        {
            baglanti.Open();
            SqlDataAdapter getir = new SqlDataAdapter("Select *From Yonetici",baglanti);
            DataTable tablo = new DataTable();
            getir.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }
        void DoktorVerileri()
        {
            baglanti.Open();
            SqlDataAdapter getir = new SqlDataAdapter("Select *From Doktorlar", baglanti);
            DataTable tablo = new DataTable();
            getir.Fill(tablo);
            dataGridView2.DataSource = tablo;
            baglanti.Close();
        }
        private void yöneticiKontrolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hastaToolStripMenuItem.Enabled= false;
            doktorToolStripMenuItem.Enabled = false;
            YoneticiVerileri();
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            groupBox1.Visible = true;
        }
        

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            textboxtemizle();
            yöneticiKontrolToolStripMenuItem.Enabled = true;
            doktorToolStripMenuItem.Enabled = true;
            hastaToolStripMenuItem.Enabled = true;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
                {
                    MessageBox.Show("Hiçbir alan Boş Bırakılamaz!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("insert into Yonetici(Tc,Ad,Soyad,Sifre) values(@Tc,@Ad,@Soyad,@Sifre)", baglanti);
                    komut.Parameters.AddWithValue("@Ad", textBox1.Text);
                    komut.Parameters.AddWithValue("@Soyad", textBox2.Text);
                    komut.Parameters.AddWithValue("@Tc", Convert.ToInt64(textBox3.Text));
                    komut.Parameters.AddWithValue("@Sifre", textBox4.Text);
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    YoneticiVerileri();
                    textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Aynı Tc Kimlik Numarı ile Kayıt Yapılamaz!");
                baglanti.Close();
            }  
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text==""|| textBox2.Text == ""|| textBox3.Text == ""|| textBox4.Text == "")
            {
                MessageBox.Show("Hiçbir alan Boş Bırakılamaz!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Update Yonetici Set Tc=@Tc,Ad=@Ad,Soyad=@Soyad,Sifre=@Sifre Where Tc=@Tc", baglanti);
                komut.Parameters.AddWithValue("@Ad", textBox1.Text);
                komut.Parameters.AddWithValue("@Soyad", textBox2.Text);
                komut.Parameters.AddWithValue("@Tc", Convert.ToInt64(textBox3.Text));
                komut.Parameters.AddWithValue("@Sifre", textBox4.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                YoneticiVerileri();
                textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear();
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string koru = "24604568308";
            if (Convert.ToInt64(textBox3.Text) == Convert.ToInt64(koru))
            {
                MessageBox.Show("Korumalı Yönetici!","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }
            else
            {
                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "")
                {
                    MessageBox.Show("Hiçbir alan Boş Bırakılamaz!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("Delete From Yonetici Where Tc=@Tc", baglanti);
                    komut.Parameters.AddWithValue("@Tc", Convert.ToInt64(textBox3.Text));
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    YoneticiVerileri();
                    textBox1.Clear(); textBox2.Clear(); textBox3.Clear(); textBox4.Clear();
                }
            }
                      
        }

        private void doktorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yöneticiKontrolToolStripMenuItem.Enabled = false;
            hastaToolStripMenuItem.Enabled = false;
            groupBox1.Visible = false;
            groupBox3.Visible = false;
            groupBox2.Visible = true;
            DoktorVerileri();
        }

        private void Yonetici_Load(object sender, EventArgs e)
        {
            
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textboxtemizle();
            yöneticiKontrolToolStripMenuItem.Enabled = true;
            doktorToolStripMenuItem.Enabled = true;
            hastaToolStripMenuItem.Enabled = true;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "" || textBox9.Text == "")
                {
                    MessageBox.Show("Hiçbir alan Boş Bırakılamaz!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("insert into Doktorlar (Tc,Ad,Soyad,Klinik,Sifre) values(@Tc,@Ad,@Soyad,@Klinik,@Sifre)", baglanti);
                    komut.Parameters.AddWithValue("@Ad", textBox5.Text);
                    komut.Parameters.AddWithValue("@Soyad", textBox6.Text);
                    komut.Parameters.AddWithValue("@Tc", Convert.ToInt64(textBox7.Text));
                    komut.Parameters.AddWithValue("@Sifre", textBox8.Text);
                    komut.Parameters.AddWithValue("@Klinik", textBox9.Text);
                    komut.ExecuteNonQuery();
                    baglanti.Close();
                    DoktorVerileri();
                    textBox5.Clear(); textBox6.Clear(); textBox7.Clear(); textBox8.Clear(); textBox9.Clear();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Aynı Tc Kimlik Numarı ile Kayıt Yapılamaz!");
                baglanti.Close();
            }
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "" || textBox9.Text == "")
            {
                MessageBox.Show("Hiçbir alan Boş Bırakılamaz!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Update Doktorlar  Set Tc=@Tc,Ad=@Ad,Soyad=@Soyad,Sifre=@Sifre,Klinik=@Klinik Where Tc=@Tc", baglanti);
                komut.Parameters.AddWithValue("@Ad", textBox5.Text);
                komut.Parameters.AddWithValue("@Soyad", textBox6.Text);
                komut.Parameters.AddWithValue("@Tc", Convert.ToInt64(textBox7.Text));
                komut.Parameters.AddWithValue("@Sifre", textBox8.Text);
                komut.Parameters.AddWithValue("@Klinik", textBox9.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                DoktorVerileri();
                textBox5.Clear(); textBox6.Clear(); textBox7.Clear(); textBox8.Clear(); textBox9.Clear();
            }
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox5.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            textBox6.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            textBox7.Text = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            textBox8.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            textBox9.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            if (textBox5.Text == "" || textBox6.Text == "" || textBox7.Text == "" || textBox8.Text == "" || textBox9.Text == "")
            {
                MessageBox.Show("Hiçbir alan Boş Bırakılamaz!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Delete From Doktorlar Where Tc=@Tc", baglanti);
                komut.Parameters.AddWithValue("@Tc", Convert.ToInt64(textBox7.Text));
                komut.ExecuteNonQuery();
                baglanti.Close();
                DoktorVerileri();
                textBox5.Clear(); textBox6.Clear(); textBox7.Clear(); textBox8.Clear(); textBox9.Clear();
            }
        }

        private void hastaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            yöneticiKontrolToolStripMenuItem.Enabled = false;
            hastaToolStripMenuItem.Enabled = false;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string cinsiyet;
            if (radioButton1.Checked)
            {
                cinsiyet = "Erkek";
            }
            else
                cinsiyet = "Kadın";
            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into Hasta_Kayit ( Tc,Ad,Soyad,Cinsiyet,Kan_Grubu,DogumTarihi,DogumYeri,AnneAdi,BabaAdi,CepTelefon,EPosta,Adres,Sifre) values(@Tc,@Ad,@Soyad,@Cinsiyet,@Kan_Grubu,@DogumTarihi,@DogumYeri,@AnneAdi,@BabaAdi,@CepTelefon,@Eposta,@Adres,@Sifre)", baglanti);
                komut.Parameters.AddWithValue("@Tc", Convert.ToInt64(textBox10.Text));
                komut.Parameters.AddWithValue("@Ad", textBox11.Text);
                komut.Parameters.AddWithValue("@Soyad", textBox12.Text);
                komut.Parameters.AddWithValue("@Cinsiyet", cinsiyet);
                komut.Parameters.AddWithValue("@Kan_Grubu", comboBox1.SelectedItem);
                komut.Parameters.AddWithValue("@DogumTarihi", dateTimePicker1.Value);
                komut.Parameters.AddWithValue("@Dogumyeri", comboBox2.Text);
                komut.Parameters.AddWithValue("Anneadi", textBox13.Text);
                komut.Parameters.AddWithValue("@BabaAdi", textBox14.Text);
                komut.Parameters.AddWithValue("@CepTelefon", maskedTextBox1.Text);
                komut.Parameters.AddWithValue("@EPosta", textBox15.Text);
                komut.Parameters.AddWithValue("@Adres", richTextBox1.Text);
                
                komut.Parameters.AddWithValue("@Sifre", textBox16.Text);
                komut.ExecuteNonQuery();

                baglanti.Close();
                MessageBox.Show("Kayıt Başarı ile gerçekleşti");
                textboxtemizle();
            }
            
            catch (System.FormatException)
            {
                MessageBox.Show("Girişleri lütfen sadece Rakam veya harf olarak giriniz");
                baglanti.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Aynı Tc Kimlik Numarı ile Kayıt Yapılamaz!");
                baglanti.Close(); 
            }
        }
        
        private void button13_Click(object sender, EventArgs e)
        {
            bul();
            if (textBox17.TextLength!=11)
            {
                MessageBox.Show("Tc Kimlik Numarası 11 Haneli Olmalıdır", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (Convert.ToInt64(textBox17.Text) == Convert.ToInt64(tc))
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("Select *From Hasta_Kayit Where Tc=@Tc", baglanti);
                    komut.Parameters.AddWithValue("@Tc", Convert.ToInt64(textBox17.Text));
                    SqlDataReader oku = komut.ExecuteReader();
                    while (oku.Read())
                    {
                        textBox10.Text = oku["Tc"].ToString();
                        textBox11.Text = oku["Ad"].ToString();
                        textBox12.Text = oku["Soyad"].ToString();
                        comboBox1.Text = oku["Kan_Grubu"].ToString();
                        comboBox2.Text = oku["Dogumyeri"].ToString();
                        dateTimePicker1.Text = oku["DogumTarihi"].ToString();
                        textBox13.Text = oku["AnneAdi"].ToString();
                        textBox14.Text = oku["BabaAdi"].ToString();
                        maskedTextBox1.Text = oku["CepTelefon"].ToString();
                        textBox15.Text = oku["EPosta"].ToString();
                        textBox16.Text = oku["Sifre"].ToString();
                        richTextBox1.Text = oku["Adres"].ToString();
                        string cinsiyet = oku["Cinsiyet"].ToString();
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
                else
                    MessageBox.Show("Kayıt Bulunamadı.");

            }
            
        }

        private void button10_Click(object sender, EventArgs e)
        {

            if (textBox10.Text == "" || textBox12.Text == "" || comboBox1.Text == "" || richTextBox1.Text == ""
                || maskedTextBox1.Text == "" || textBox14.Text == "" || textBox13.Text == "" || comboBox2.Text == "" ||
                textBox16.Text == "" || textBox15.Text == "" || textBox14.Text == "")
            {
                MessageBox.Show("Hiçbir Alan Boş Geçilemez");
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
                baglanti.Open();
                SqlCommand komut = new SqlCommand("UPDATE Hasta_Kayit SET Ad=@Ad,Soyad=@Soyad,Cinsiyet=@Cinsiyet,Kan_Grubu=@Kan_Grubu,DogumTarihi=@DogumTarihi,Dogumyeri=@Dogumyeri,AnneAdi=@AnneAdi,BabaAdi=@BabaAdi,Ceptelefon=@CepTelefon,EPosta=@Eposta,Adres=@Adres,Sifre=@Sifre WHERE Tc=@Tc", baglanti);
                komut.Parameters.AddWithValue("@Tc", Convert.ToInt64(textBox10.Text));
                komut.Parameters.AddWithValue("@Ad", textBox11.Text);
                komut.Parameters.AddWithValue("@Soyad", textBox12.Text);
                komut.Parameters.AddWithValue("@Cinsiyet", cinsiyet);
                komut.Parameters.AddWithValue("@Kan_Grubu", comboBox1.Text);
                komut.Parameters.AddWithValue("@DogumTarihi", dateTimePicker1.Value);
                komut.Parameters.AddWithValue("@Dogumyeri", comboBox2.Text);
                komut.Parameters.AddWithValue("@AnneAdi", textBox13.Text);
                komut.Parameters.AddWithValue("@BabaAdi", textBox14.Text);
                komut.Parameters.AddWithValue("@CepTelefon", maskedTextBox1.Text);
                komut.Parameters.AddWithValue("@EPosta", textBox15.Text);
                komut.Parameters.AddWithValue("@Sifre", textBox16.Text);
                komut.Parameters.AddWithValue("@Adres", richTextBox1.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                textboxtemizle();

            }
            
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DialogResult sonuc;
            sonuc = MessageBox.Show("Kaydınızı Silmek Sitediğinize Emin misiniz? Bu İşlem Geri Alınamaz", "UYARI!", MessageBoxButtons.OKCancel);
            if (sonuc == DialogResult.OK)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Delete From Hasta_Kayit Where Tc=@Tc", baglanti);
                komut.Parameters.AddWithValue("@Tc", Convert.ToInt64(textBox10.Text));
                komut.ExecuteNonQuery();
                baglanti.Close();
                textboxtemizle();
            }
            else
            {
                MessageBox.Show("İşlem İptal Edildi");
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            yöneticiKontrolToolStripMenuItem.Enabled = true;
            doktorToolStripMenuItem.Enabled = true;
            hastaToolStripMenuItem.Enabled = true;
            groupBox1.Visible = false;
            groupBox2.Visible = false;
            groupBox3.Visible = false;
            textboxtemizle();
            groupBox3.Visible = false;
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            textBox1.MaxLength = 11;   
        }

        private void textBox17_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsNumber(e.KeyChar) || e.KeyChar == 8)
            {
                e.Handled = false;
            }
            else
                e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
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

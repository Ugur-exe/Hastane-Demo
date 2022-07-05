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
    public partial class Giris : Form
    {
        long id = Hasta_Giriş.id;
        public Giris()
        {
            InitializeComponent();

        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-U0HV2JK;Initial Catalog=Hastane;Integrated Security=True");
        void Verilerigoruntule()
        {
            baglanti.Open();
            SqlDataAdapter getir = new SqlDataAdapter("Select *from Hasta_Kayit where Tc ='"+id+"' ", baglanti);
            DataTable tablo = new DataTable();
            getir.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }

        private void Giris_Load(object sender, EventArgs e)
        {
            
            
            textBox1.Enabled = false;
            Verilerigoruntule();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[8].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[10].Value.ToString();
            textBox7.Text = dataGridView1.CurrentRow.Cells[12].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            maskedTextBox1.Text = dataGridView1.CurrentRow.Cells[9].Value.ToString();

            richTextBox1.Text = dataGridView1.CurrentRow.Cells[11].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            string cinsiyet = dataGridView1.CurrentRow.Cells[3].Value.ToString();
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
        
        private void button1_Click(object sender, EventArgs e)
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
            komut.Parameters.AddWithValue("@Sifre", textBox7.Text);
            komut.Parameters.AddWithValue("@Adres", richTextBox1.Text);
            komut.ExecuteNonQuery();

            baglanti.Close();
            Verilerigoruntule();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult sonuc;
            sonuc = MessageBox.Show("Kaydınızı Silmek Sitediğinize Emin misiniz? Bu İşlem Geri Alınamaz", "UYARI!", MessageBoxButtons.OKCancel);
            if (sonuc == DialogResult.OK)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Delete From Hasta_Kayit Where Tc=@Tc", baglanti);
                komut.Parameters.AddWithValue("@Tc", Convert.ToInt64(textBox1.Text));
                komut.ExecuteNonQuery();
                baglanti.Close();
                Verilerigoruntule();
                this.Close();

            }
            else
            {
                MessageBox.Show("İşlem İptal Edildi");
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {
            RandevuAl randevu = new RandevuAl();
            randevu.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Randevularim a = new Randevularim();
            a.ShowDialog();
        }
    }
}

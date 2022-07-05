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
    public partial class Doktor : Form
    {
        long id = DoktorGiris.id;
        string isim;
        public Doktor()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-U0HV2JK;Initial Catalog=Hastane;Integrated Security=True");
        void VerileriGoruntule()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select *From Randevu Where Doktor=@Doktor", baglanti);
            komut.Parameters.AddWithValue("@Doktor", isim);
            SqlDataAdapter getir = new SqlDataAdapter(komut);
            DataTable tablo = new DataTable();
            getir.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }
        private void Doktor_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select *From Doktorlar Where Tc=@Tc", baglanti);
            komut.Parameters.AddWithValue("@Tc", id);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                isim = oku["Ad"].ToString() + " " + oku["Soyad"].ToString();
            }
            baglanti.Close();
            MessageBox.Show(isim);
            timer1.Interval = 1000;
            timer1.Start();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            VerileriGoruntule();
            groupBox1.Visible = true;
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Delete From Randevu Where Tc=@Tc", baglanti);
            komut.Parameters.AddWithValue("@Tc", Convert.ToInt64(textBox1.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            VerileriGoruntule();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Clear();textBox2.Clear();textBox3.Clear();
            groupBox1.Visible = false;
            BekleyenHastalar hasta = new BekleyenHastalar();
            hasta.ShowDialog();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongDateString();
            label2.Text = DateTime.Now.ToLongTimeString();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

    }
}

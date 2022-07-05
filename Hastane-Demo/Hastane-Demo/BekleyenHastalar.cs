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
using System.Net.Mail;
using System.Net;

namespace Hastane_Demo
{
    public partial class BekleyenHastalar : Form
    {
        long id = DoktorGiris.id;
        string isim;
        public static string eposta;
        public BekleyenHastalar()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-U0HV2JK;Initial Catalog=Hastane;Integrated Security=True");
        void VerileriGoruntule()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select *From Randevu Where Doktor=@Doktor And Tarih=@Tarih", baglanti);
            komut.Parameters.AddWithValue("@Doktor", isim);
            komut.Parameters.AddWithValue("@Tarih", DateTime.Now.ToShortDateString());
            SqlDataAdapter getir = new SqlDataAdapter(komut);
            DataTable tablo = new DataTable();
            getir.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }
        void HastaEposta()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select *From Hasta_Kayit Where Tc=@Tc", baglanti);
            komut.Parameters.AddWithValue("@Tc", Convert.ToInt64(textBox1.Text));
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                eposta = oku["EPosta"].ToString();
            }
            baglanti.Close();
        }
        private void BekleyenHastalar_Load(object sender, EventArgs e)
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
            VerileriGoruntule();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = DateTime.Now.ToLongTimeString();
            label2.Text = DateTime.Now.ToLongDateString();   
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HastaEposta();
            Teshis koy = new Teshis();
            koy.ShowDialog();

        }
    }
}

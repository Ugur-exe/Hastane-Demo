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
    public partial class Randevularim : Form
    { long id = Hasta_Giriş.id;
        public Randevularim()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-U0HV2JK;Initial Catalog=Hastane;Integrated Security=True");
        private void Randevularim_Load(object sender, EventArgs e)
        {
            textBox1.Enabled = false;
            veriler();
        }
        void veriler()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From Randevu Where Tc=@Tc", baglanti);
            komut.Parameters.AddWithValue("@Tc", id);
            SqlDataAdapter getir = new SqlDataAdapter(komut);
            DataTable tablo = new DataTable();
            getir.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text=="")
            {
                MessageBox.Show("Randevu Bulunamadı");
            }
            else
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Delete From Randevu Where Klinik=@Klinik", baglanti);
                komut.Parameters.AddWithValue("@Klinik", textBox1.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                veriler();
                textBox1.Clear();
            }           
        }

        
    }
}

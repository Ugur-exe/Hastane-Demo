using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.Data.SqlClient;

namespace Hastane_Demo
{
    public partial class Sifremi_Unuttum : Form
    {
        string check;
        int sayi;
        bool ax;
        public Sifremi_Unuttum()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-U0HV2JK;Initial Catalog=Hastane;Integrated Security=True");
        Random rastgele = new Random();
        
        void verilericek()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select EPosta from Hasta_Kayit where EPosta =@eposta ", baglanti);
            komut.Parameters.AddWithValue("@eposta",textBox1.Text);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                check = oku["EPosta"].ToString();
                
            }
            baglanti.Close();
            if (textBox1.Text == check)
            {
                ax = true;
            }
            else
            {
                ax = false;
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            verilericek();
            if (ax==true)
            {
                sayi = rastgele.Next(10000, 100000);
                SmtpClient sc = new SmtpClient();
                sc.Port = 587;
                sc.Host = "smtp.outlook.com";
                sc.EnableSsl = true;
                sc.Credentials = new NetworkCredential("denemedersi06@outlook.com", "Aa010203,");

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("denemedersi06@outlook.com", "Uğur Koçak");
                mail.To.Add(textBox1.Text);
                mail.Subject = "ŞİFRE DEĞİŞTİRME";
                mail.IsBodyHtml = true;
                mail.Body = "Doğrulama Kodunuz: " + "<b><b>" + sayi;
                sc.Send(mail);
                button1.Enabled = false;
                button2.Visible = true;
            }
            else
            {
                MessageBox.Show("Geçerli bir mail adresi girin!");
            }       
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == textBox4.Text)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("update Hasta_Kayit set Sifre=@Sifre where EPosta=@EPosta", baglanti);
                komut.Parameters.AddWithValue("@EPosta", textBox1.Text);
                komut.Parameters.AddWithValue("Sifre", textBox4.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Sifre Başarı İle Değiştirildi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.Close();
            }
            else
                MessageBox.Show("Girilen Şifre Aynı Olmalıdır!","UYARI",MessageBoxButtons.OK,MessageBoxIcon.Error);   
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                MessageBox.Show("Lütfen Doğrulama Kodunu Giriniz","UYARI",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            else
            {
                int a = Convert.ToInt32(textBox2.Text);
                if (a == sayi)
                {
                    groupBox1.Visible = true;
                    groupBox2.Visible = false;
                }
                else
                    MessageBox.Show("Doğrulama Kodu Yanlış","UAYRI",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void Sifremi_Unuttum_Load(object sender, EventArgs e)
        {
            
            
        }
    }
}

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

namespace Hastane_Demo
{
    public partial class Teshis : Form
    {
        public Teshis()
        {
            InitializeComponent();
        }
        string EkDosya = null;
        string eposta = BekleyenHastalar.eposta;
        private void button1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text=="")
            {
                MessageBox.Show("Lütfen Tanı Giriniz");
            }
            else
            {
                SmtpClient sc = new SmtpClient();
                sc.Port = 587;
                sc.Host = "smtp.outlook.com";
                sc.EnableSsl = true;
                sc.Credentials = new NetworkCredential("denemedersi06@outlook.com", "Aa010203,");

                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("denemedersi06@outlook.com", "Hacettepe Hastanesi");
                mail.To.Add(eposta);
                mail.Subject = "HACETTEPE HASTANESİ";
                mail.IsBodyHtml = true;
                mail.Body = "TEŞHİS: " + "<b><b>" + richTextBox1.Text;
                if (EkDosya!=null)
                {
                    mail.Attachments.Add(new Attachment(EkDosya));
                }
                sc.Send(mail);
                MessageBox.Show("Teşhis Başarı İle Gönderildi");
                this.Close();
            }
            
        }

        private void flowLayoutPanel1_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Gönderi İçin Ek Dosya Seçebilirsiniz.";
            ofd.ShowDialog();
            EkDosya = ofd.FileName;
        }
    }
}

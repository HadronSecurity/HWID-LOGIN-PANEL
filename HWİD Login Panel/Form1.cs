using System;
using System.Windows.Forms;
using Firebase.Database.Query;
using System.Management;
using Firebase.Database;

namespace HWİD_Login_Panel
{
    public partial class Form1 : Form
    {
        private FirebaseClient database;
        public Form1()
        {
            InitializeComponent();
            var firebaseUrl = "Fribase Realtime Database api yi giriniz ";
            database = new FirebaseClient(firebaseUrl);
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(guna2TextBox2.Text);
            MessageBox.Show("Kopyalandı!");
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void guna2Button1_Click(object sender, EventArgs e)
        {
            
            // Kullanıcının girdiği HWID adresini alıyoruz
            string hwid = guna2TextBox1.Text;

            if (string.IsNullOrEmpty(hwid))
            {
                // HWID boşsa hata mesajı göster
                MessageBox.Show("HWID girmediniz. Lütfen bir HWID girin.");
                return;
            }

            // HWID'yi veritabanında kontrol ediyoruz
            var hwidExist = await database.Child("hwids").Child(hwid).OnceSingleAsync<string>();

            // HWID veritabanında varsa
            if (hwidExist != null)
            {
                // Giriş işlemini gerçekleştir
                MessageBox.Show("Başarıyla giriş yaptınız!");
            }
            else
            {
                // HWID veritabanında yoksa
                MessageBox.Show("HWID bulunamadı. Giriş yapılamadı.");
            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystemProduct");
            foreach (ManagementObject obj in searcher.Get())
            {
                guna2TextBox2.Text = obj["UUID"].ToString();
            }
        }
    }
}

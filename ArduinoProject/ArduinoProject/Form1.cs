using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
namespace ArduinoProject
{
    public partial class Form1 : Form
    {
        public string data;
        int satir;
        int satirNo;
        public string message = " ";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            label3.Visible = false;
            string[] ports = SerialPort.GetPortNames();  //Seri portları diziye ekleme
            foreach (string port in ports)
            comboBox1.Items.Add(port);                   //Seri portları comboBox1'e ekleme
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived); //DataReceived eventini oluşturma
            
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
                             
            this.Invoke(new EventHandler(displayData_event));

        }

        private void displayData_event(object sender, EventArgs e)
        {
            data = serialPort1.ReadLine();  //Gelen veriyi okuma
            label3.Visible = true;
            progressBar1.Value = Convert.ToInt32(data);  //Gelen veriyi Int değikene döndürerek progressBar değerine eşitle
            label3.Text = data;  //Gelen veriyi label3'ye yaz
            
            DateTime yeni = DateTime.Now;
            satir = dataGridView1.Rows.Add();

            dataGridView1.Rows[satir].Cells[0].Value = satirNo;
            dataGridView1.Rows[satir].Cells[1].Value = data;

            dataGridView1.Rows[satir].Cells[2].Value = yeni.ToLongTimeString();
            dataGridView1.Rows[satir].Cells[3].Value = yeni.ToShortDateString();
            dataGridView1.Rows[satir].Cells[4].Value = message;
            satir++;
            satirNo++;

            if (Convert.ToInt32(data) < 10)
            {
                message = "Çarpma tehlikesi";
            }
            else if (Convert.ToInt32(data) < 20)
            {
                message = "Dikkatli ol";
            }
            else message = "Tehlike Yok";
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
                serialPort1.PortName = comboBox1.Text;       //Port ismini comboBox1'in text'i olarak belirle
                serialPort1.Open();                          //Seri portu aç
                timer1.Start();
                button2.Enabled = true;                          //"Kes" butonunu tıklanabilir yap
                button1.Enabled = false;                      //"Bağlan" butonunu tıklanamaz yap
                label2.Text = "Bağlantı sağlandı";
                label2.ForeColor = Color.Green;              //Label3 yazı rengini yeşil yap
                     
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
                serialPort1.Close();              //Seri portu kapa
                timer1.Start();
                button2.Enabled = false;              //"Kes" butonunu tıklanamaz yap
                button1.Enabled = true;            //"BağlanKes" butonunu tıklanabilir yap
                label2.Text = "Bağlantı kesildi";
                label2.ForeColor = Color.Red;     //Label3 yazı rengini kırmızı yap
           
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serialPort1.IsOpen) {
                
                serialPort1.Close();    //Seri port açıksa kapat
            } 
        }

        private void button3_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;
            label3.Text=" ";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
        }
    }
}

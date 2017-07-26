using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace vt_kod6
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbConnection bag = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=bilgisayar.accdb");
        OleDbDataAdapter kurye;
        DataSet ds = new DataSet();
        BindingSource bs = new BindingSource();
        void baglan()
        {
            ds.Clear();
            string sorgu;
            sorgu = "select*from bilesenler";
            kurye = new OleDbDataAdapter(sorgu, bag);
            kurye.Fill(ds);
            bs.DataSource = ds.Tables[0];
            dataGridView1.DataSource = bs;
        }
        void textbagla()
        {
            textBox1.DataBindings.Add("text", bs, "parcaadi");
            textBox2.DataBindings.Add("text", bs, "firmaadi");
            textBox3.DataBindings.Add("text", bs, "tarih");
            textBox4.DataBindings.Add("text", bs, "parcaadedi");
            textBox5.DataBindings.Add("text", bs, "bfiyat");
            textBox6.DataBindings.Add("text", bs, "tutar");
            textBox7.DataBindings.Add("text", bs, "resimyolu");
            textBox8.DataBindings.Add("text", bs, "stokkodu");
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            baglan();
            textbagla();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string dosya;
            openFileDialog1.ShowDialog();
            dosya = openFileDialog1.FileName;
            textBox7.Text = dosya;
            pictureBox1.ImageLocation = dosya;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bs.AddNew();
            pictureBox1.Image = null;
            MessageBox.Show("Yeni Kayıt Eklendi.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //kayit ekleme
            try
            {
                bag.Open();
                OleDbCommand kyt = new OleDbCommand("insert into bilesenler values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "'," + textBox4.Text + "," + textBox5.Text + "," + textBox6.Text + ",'" + textBox7.Text + "')", bag);
                kyt.ExecuteNonQuery();
                baglan();
                bag.Close();
                bs.MoveFirst();
                MessageBox.Show("Yeni Kayıt Eklendi");
            }
            catch (Exception)
            {


            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            bag.Open();
            OleDbCommand gncll = new OleDbCommand("update bilesenler set parcaadi='" + textBox1.Text + "',firmaadi='" + textBox2.Text + "',tarih='" + textBox3.Text + "',parcaadedi=" + textBox4.Text + ",bfiyat=" + textBox5.Text + ",tutar=" + textBox6.Text + ",resimyolu='" + textBox7.Text + "' where stokkodu=" + textBox8.Text + "", bag);
            gncll.ExecuteNonQuery();
            baglan();
            bag.Close();
            MessageBox.Show("Güncellendi");
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            bag.Open();
            OleDbCommand sil = new OleDbCommand("delete from bilesenler where stokkodu=" + textBox8.Text + "", bag);
            int a = sil.ExecuteNonQuery();
            baglan();
            bag.Close();
            MessageBox.Show(a + " Adet Silindi");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bs.MovePrevious();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            bs.MoveFirst();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            bs.MoveLast();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            
            bs.MoveNext();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            label9.Text = (bs.Position + 1).ToString() + " / " + bs.Count;
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            int a, b;
            a = int.Parse(textBox4.Text);
            b = int.Parse(textBox5.Text);
            int s = a * b;
            textBox6.Text = s.ToString();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = textBox7.Text;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ds.Clear();
            string filtre;
            filtre = "select*from bilesenler where stokkodu=" + textBox9.Text + "";
            kurye = new OleDbDataAdapter(filtre, bag);
            kurye.Fill(ds);
            dataGridView1.DataSource = bs;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace stok_takip
{
    public partial class frmmarka : Form
    {
        public frmmarka()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=Fcmglr\\SQLEXPRESS;Initial Catalog=stok_takip;Integrated Security=True");
        bool durum;
        private void markakontrol()
        {
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from markabilgileri", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                if (comboBox1.Text ==read["kategori"].ToString() && textBox1.Text == read["marka"].ToString() || comboBox1.Text == "" || textBox1.Text == "")
                {
                    durum = false;
                }
            }
            baglanti.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            markakontrol();
            if (durum==true)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into markabilgileri(kategori,marka) values ('" + comboBox1.Text + "','" + textBox1.Text + "')", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("MARKA EKLENDİ");
            }
            else
            {
                MessageBox.Show("BÖYLE BİR KATEGORİ VE MARKA BULUNMAKTADIR!","UYARI");
            }
           
            textBox1.Text = "";
            comboBox1.Text = "";
           
        }

        private void frmmarka_Load(object sender, EventArgs e)
        {
            kategorigetir();

        }

        private void kategorigetir()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from kategoribilgileri ", baglanti);
            SqlDataReader read = komut.ExecuteReader();

            while (read.Read())
            {
                comboBox1.Items.Add(read["kategori"].ToString());
            }
            baglanti.Close();
        }
    }
}

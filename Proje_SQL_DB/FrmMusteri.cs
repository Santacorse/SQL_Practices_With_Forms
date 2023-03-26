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
using System.Security.Cryptography;

namespace Proje_SQL_DB
{
    public partial class FrmMusteri : Form
    {
        SqlConnection baglanti = new SqlConnection(@"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = SatisVT; Integrated Security = True");
        public FrmMusteri()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Listele();
        }
        void Listele()
        {
            SqlCommand komut = new SqlCommand("SELECT * FROM TBLMUSTERI", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt); 
            dataGridView1.DataSource = dt;
        }
        private void FrmMusteri_Load(object sender, EventArgs e)
        {
            Listele();
            baglanti.Open();
            SqlCommand komut1 = new SqlCommand("SELECT * FROM TBLSEHIR",baglanti);
            SqlDataReader dr = komut1.ExecuteReader();
            while(dr.Read())
            {
                CmbSehir.Items.Add(dr["SEHIRAD"]);
            }
            baglanti.Close(); 
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            TxtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            CmbSehir.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            TxtBakiye.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();  

        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLMUSTERI (MUSTERIAD,MUSTERISOYAD,MUSTERISEHIR,MUSTERIBAKIYE) VALUES (@P1,@P2,@P3,@P4)", baglanti);
            komut.Parameters.AddWithValue("@P1",TxtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@P3", CmbSehir.Text);
            komut.Parameters.AddWithValue("@P4",decimal.Parse(TxtBakiye.Text));
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Müşteri Sisteme Kaydedildi");
            Listele();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("DELETE FROM TBLMUSTERI WHERE MUSTERIID=@P1", baglanti);
            komut.Parameters.AddWithValue("P1", TxtID.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Müşteri Başarıyla Silindi");
            Listele();
        }

        private void BtnAra_Click(object sender, EventArgs e)
        {

            SqlCommand komut = new SqlCommand("SELECT * FROM TBLMUSTERI WHERE MUSTERIAD=@P1", baglanti);
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dataGridView1.DataSource= dt;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update TBLMUSTERI set MUSTERIAD=@P1, MUSTERISOYAD=@P2, MUSTERISEHIR=@P3, MUSTERIBAKIYE=@P4 WHERE MUSTERIID=@P5", baglanti);
            komut.Parameters.AddWithValue("@P1", TxtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@P3", CmbSehir.Text);
            komut.Parameters.AddWithValue("@P4", decimal.Parse(TxtBakiye.Text));
            komut.Parameters.AddWithValue("@P5", TxtID.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            Listele();
        }
    }
}

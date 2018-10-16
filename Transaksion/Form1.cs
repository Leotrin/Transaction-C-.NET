using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Transactions;
using System.Data.SqlClient;

namespace Transaksion
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            String conn = @"Data Source=WIN3;Initial Catalog=Student;Integrated Security=True";
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * from tblStudent", con);

            DataTable dt = new DataTable();
            da.Fill(dt);

            this.comboBox1.DataSource = dt;
            this.comboBox1.DisplayMember = "Name";
            this.dataGridView2.DataSource = dt;
            con.Close();

            String cone = @"Data Source=WIN3;Initial Catalog=Bank;Integrated Security=True";
            SqlConnection conee = new SqlConnection(cone);
            conee.Open();
            SqlDataAdapter daa = new SqlDataAdapter("Select * from tblBank", conee);

            DataTable dtt = new DataTable();
            daa.Fill(dtt);
            

            this.comboBox2.DataSource = dtt;
            this.comboBox2.DisplayMember = "Institut";
            this.dataGridView1.DataSource = dtt;
            conee.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String conn = @"Data Source=WIN3;Initial Catalog=Student;Integrated Security=True";
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter("Select * from tblStudent", con);

            DataTable dt = new DataTable();
            da.Fill(dt);

            this.dataGridView2.DataSource = dt;
            con.Close();
            
            String cone = @"Data Source=WIN3;Initial Catalog=Bank;Integrated Security=True";
            SqlConnection conee = new SqlConnection(cone);
            conee.Open();
            SqlDataAdapter daa = new SqlDataAdapter("Select * from tblBank", conee);

            DataTable dtt = new DataTable();
            daa.Fill(dtt);
            this.dataGridView1.DataSource = dtt;
            conee.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String constring2 =@"Data Source=WIN3;Initial Catalog=Bank;Integrated Security=True";
            String constring = @"Data Source=WIN3;Initial Catalog=Student;Integrated Security=True";
            
            try
            {
                using (TransactionScope scop = new TransactionScope())
                {
                    using (SqlConnection con = new SqlConnection(constring))
                    {
                        con.Open();
                        String update = "Update tblStudent set Amount=Amount-" + this.textBox1.Text + "Where Name='" + this.comboBox1.Text + "'";
                        SqlCommand cmd = new SqlCommand(update, con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    using (SqlConnection con = new SqlConnection(constring2))
                    {
                        con.Open();
                        String update = "Update tblBank set Amount=Amount+" + this.textBox1.Text + "Where Institut='" + this.comboBox2.Text + "'";
                        SqlCommand cmd = new SqlCommand(update, con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    scop.Complete();
                    MessageBox.Show("Transaksioni u kyre me sukses !");
                }

            }
            catch(TransactionAbortedException)
            {
              
            }
            finally
            {
                
            }
        }
    }
}

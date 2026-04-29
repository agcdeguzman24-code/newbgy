using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Barangay_Management_System
{
    public partial class Service_Request : Form
    {
        public Service_Request()
        {
            InitializeComponent();
        }

               private void InsertRequest(string certificateType)
        {
            string connString = "server=localhost;user id=root;password=;database=barangay_management_system;";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    string query = "INSERT INTO certificate_requests (name, certificate, date_requested, status) " +
                                   "VALUES (@name, @certificate, @date, @status)";

                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    cmd.Parameters.AddWithValue("@name", "juan"); 
                    cmd.Parameters.AddWithValue("@certificate", certificateType);
                    cmd.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@status", "pending");

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Request Sent!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            DashboardResident f2 = new DashboardResident();
            f2.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            
        }

       

        private void button2_Click(object sender, EventArgs e)
        {
            InsertRequest("clearance");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            InsertRequest("indigency");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            InsertRequest("good moral");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InsertRequest("residency");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            InsertRequest("solo parent");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            InsertRequest("no income");
        }
    }
}


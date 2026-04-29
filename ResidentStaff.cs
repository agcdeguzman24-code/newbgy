using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Barangay_Management_System
{
    public partial class ResidentStaff : Form
    {
        string connectionString = "server=localhost;user id=root;password=;database=barangaymanagement";

        public ResidentStaff()
        {
            InitializeComponent();
        }

        private void ResidentStaff_Load(object sender, EventArgs e)
        {
            LoadResidents();
        }

        private void LoadResidents()
        {
            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT * FROM residents";

                    MySqlDataAdapter da = new MySqlDataAdapter(query, con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    dataGridView1.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            new DashboardStaff().Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new BlotterRecordsStaff().Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new AnnouncementStaff().Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new CertificatesStaff().Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            new FinancialReportsStaff().Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            new Health_ServicesStaff().Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Hide();
        }
    }
}
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
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Resident f3 = new Resident();
            f3.Show();
            this.Hide();
        }

         private void Dashboard_Load(object sender, EventArgs e)
 {
     LoadCounts(); 

     chart1.Series.Clear();
     chart1.ChartAreas.Clear();

     chart1.ChartAreas.Add(new ChartArea());

     Series gender = new Series("Gender");
     gender.ChartType = SeriesChartType.Column;

     gender.Points.AddXY("Male", 45);
     gender.Points.AddXY("Female", 55);

     chart1.Series.Add(gender);


 }

        private void button3_Click(object sender, EventArgs e)
        {
            Officials f3 = new Officials();
            f3.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BlotterRecords f3 = new BlotterRecords();
            f3.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Announcement f3 = new Announcement();
            f3.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FinancialReports f3 = new FinancialReports();
            f3.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Certificates f3 = new Certificates();
            f3.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Health_Services f3 = new Health_Services();
            f3.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form1 f3 = new Form1();
            f3.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dashboard f3 = new Dashboard();
            f3.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click_1(object sender, EventArgs e)
        {
            Form1 f3 = new Form1();
            f3.Show();
            this.Hide();
        }

                private void LoadCounts()
        {
            string connString = "server=localhost;user id=root;password=;database=barangay_management_system;";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                try
                {
                    conn.Open();

                    // TOTAL REQUESTS
                    string totalQuery = "SELECT COUNT(*) FROM certificate_requests";
                    MySqlCommand totalCmd = new MySqlCommand(totalQuery, conn);
                    int total = Convert.ToInt32(totalCmd.ExecuteScalar());

                    label30.Text = total.ToString(); // PALITAN mo based sa label name mo

                    // CLEARANCE
                    string clearanceQuery = "SELECT COUNT(*) FROM certificate_requests WHERE certificate='clearance'";
                    MySqlCommand cCmd = new MySqlCommand(clearanceQuery, conn);
                    label28.Text = cCmd.ExecuteScalar().ToString();

                    // INDIGENCY
                    string indigencyQuery = "SELECT COUNT(*) FROM certificate_requests WHERE certificate='indigency'";
                    MySqlCommand iCmd = new MySqlCommand(indigencyQuery, conn);
                    label26.Text = iCmd.ExecuteScalar().ToString();

                    // GOOD MORAL
                    string gmQuery = "SELECT COUNT(*) FROM certificate_requests WHERE certificate='good moral'";
                    MySqlCommand gmCmd = new MySqlCommand(gmQuery, conn);
                    label24.Text = gmCmd.ExecuteScalar().ToString();

                    // RESIDENCY
                    string resQuery = "SELECT COUNT(*) FROM certificate_requests WHERE certificate='residency'";
                    MySqlCommand rCmd = new MySqlCommand(resQuery, conn);
                    label22.Text = rCmd.ExecuteScalar().ToString();

                    // SOLO PARENT
                    string spQuery = "SELECT COUNT(*) FROM certificate_requests WHERE certificate='solo parent'";
                    MySqlCommand spCmd = new MySqlCommand(spQuery, conn);
                    label20.Text = spCmd.ExecuteScalar().ToString();

                    // NO INCOME
                    string niQuery = "SELECT COUNT(*) FROM certificate_requests WHERE certificate='no income'";
                    MySqlCommand niCmd = new MySqlCommand(niQuery, conn);
                    label19.Text = niCmd.ExecuteScalar().ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }



        private void label28_Click(object sender, EventArgs e)
        {
           

        }

        private void label26_Click(object sender, EventArgs e)
        {
          

        }

        private void label24_Click(object sender, EventArgs e)
        {
            

        }

        private void label22_Click(object sender, EventArgs e)
        {
          

        }

        private void label20_Click(object sender, EventArgs e)
        {
            

        }

        private void label19_Click(object sender, EventArgs e)
        {
          

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }
    }
}















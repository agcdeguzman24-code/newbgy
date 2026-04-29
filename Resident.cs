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
    public partial class Resident : Form
    {
        public Resident()
        {
            InitializeComponent();
        }

        private void Resident_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Resident f3 = new Resident();
            f3.Show();
            this.Hide();
        }

        private void label16_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dashboard f3 = new Dashboard();
            f3.Show();
            this.Hide();
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
    }
}

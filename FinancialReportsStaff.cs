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
    public partial class FinancialReportsStaff : Form
    {
        public FinancialReportsStaff()
        {
            InitializeComponent();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            DashboardStaff f3 = new DashboardStaff();
            f3.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            BlotterRecordsStaff f3 = new BlotterRecordsStaff();
            f3.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ResidentStaff f3 = new ResidentStaff();
            f3.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AnnouncementStaff f3 = new AnnouncementStaff();
            f3.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CertificatesStaff f3 = new CertificatesStaff();
            f3.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Health_ServicesStaff f3 = new Health_ServicesStaff();
            f3.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form1 f3 = new Form1();
            f3.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FinancialReportsStaff f3 = new FinancialReportsStaff();
            f3.Show();
            this.Hide();
        }
    }
}

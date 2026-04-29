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
    public partial class DashboardResident : Form
    {
        public DashboardResident()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form1 f3 = new Form1();
            f3.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FeedbackForm f2 = new FeedbackForm();
            f2.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            Service_Request f2 = new Service_Request();
            f2.Show();
            this.Hide();
        }

        private void DashboardResident_Load(object sender, EventArgs e)
        {

        }
            private void label11_Click(object sender, EventArgs e)
        {
            txtResidentID.Text = "2026-001";
        }

        private void label12_Click(object sender, EventArgs e)
        {
            label12.Text = "HH-1045";
        }

        private void label13_Click(object sender, EventArgs e)
        {
            label13.Text = "Juan Dela Cruz";
        }

        private void label14_Click(object sender, EventArgs e)
        {
            label14.Text = "09123456789";
        }

        private void label15_Click(object sender, EventArgs e)
        {
            label15.Text = "Balanga City,Bataan";
        }

        private void label31_Click(object sender, EventArgs e)
        {
            label31.Visible = false;
        }

        private void label32_Click(object sender, EventArgs e)
        {
            label32.Visible = false;
        }

        private void label33_Click(object sender, EventArgs e)
        {
            label33.Visible = false;
        }

        private void label34_Click(object sender, EventArgs e)
        {
            label34.Visible = false;
        }

        private void label37_Click(object sender, EventArgs e)
        {
            label37.Visible = false;
        }

        private void label38_Click(object sender, EventArgs e)
        {
            label38.Visible = false;
        }

        private void label36_Click(object sender, EventArgs e)
        {
            label15.Text = "Blotter Report #001\r\nDate: April 29, 2026\r\nComplainant: Maria Santos\r\nRespondent: Pedro Cruz\r\nIncident: Noise Complaint due to loud videoke at 11:30 PM\r\nStatus: Settled through barangay mediation.";

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

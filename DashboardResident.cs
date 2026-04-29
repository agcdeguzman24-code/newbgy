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
    }
}

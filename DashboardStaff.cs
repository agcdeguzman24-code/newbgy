using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Barangay_Management_System
{
    public partial class DashboardStaff : Form
    {
        // UPDATE THIS with your actual MySQL password
        private string connectionString = "Server=127.0.0.1;Database=barangaymanagement;Uid=root;Pwd=;";

        // Timer for real-time updates
        private Timer refreshTimer;

        public DashboardStaff()
        {
            InitializeComponent();
            InitializeTimer();
        }

        private void DashboardStaff_Load(object sender, EventArgs e)
        {
            LoadDashboardData(); // Load data when form opens
        }

        private void InitializeTimer()
        {
            refreshTimer = new Timer();
            refreshTimer.Interval = 5000; // Refresh every 5 seconds
            refreshTimer.Tick += RefreshTimer_Tick;
            refreshTimer.Start();
        }

        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            LoadDashboardData(); // Auto-refresh every 5 seconds
        }

        private void LoadDashboardData()
        {
            LoadTotalCertificateRequests();
            LoadDocumentTypeCounts();
            LoadBlotterUpdates();
        }

        // Get total certificate requests
        private void LoadTotalCertificateRequests()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM certificate";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        // Update label - change "lblTotalRequests" to your actual label name
                        if (lblTotalRequests != null)
                            lblTotalRequests.Text = count.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // Silently fail or show error
                Console.WriteLine("Error loading total requests: " + ex.Message);
            }
        }

        // Get count by document type
        private void LoadDocumentTypeCounts()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    // Barangay Clearance
                    UpdateCountLabel(conn, "BarangayClearance", lblBarangayClearance);
                    UpdateCountLabel(conn, "Barangay Clearance", lblBarangayClearance); // Try both formats

                    // Certificate of Indigency
                    UpdateCountLabel(conn, "Certificate of Indigency", lblIndigency);

                    // Certificate of Good Moral Character
                    UpdateCountLabel(conn, "Certificate of Good Moral Character", lblGoodMoral);

                    // Certificate of Residency
                    UpdateCountLabel(conn, "Certificate of Residency", lblResidency);

                    // Certificate of Solo Parent
                    UpdateCountLabel(conn, "Certificate of Solo Parent", lblSoloParent);

                    // Certificate of No Income
                    UpdateCountLabel(conn, "Certificate of No Income", lblNoIncome);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading document counts: " + ex.Message);
            }
        }

        private void UpdateCountLabel(MySqlConnection conn, string documentType, Label label)
        {
            if (label == null) return;

            try
            {
                string query = "SELECT COUNT(*) FROM certificate WHERE DocumentType = @docType";
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@docType", documentType);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    // Only update if we got a result (some types might be stored differently in DB)
                    if (count > 0 || !string.IsNullOrEmpty(label.Text))
                        label.Text = count.ToString();
                }
            }
            catch
            {
                // Ignore individual type errors
            }
        }

        // Get total blotter records
        private void LoadBlotterUpdates()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT COUNT(*) FROM blotter";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        if (lblBlotterUpdates != null)
                            lblBlotterUpdates.Text = count.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading blotter updates: " + ex.Message);
            }
        }

        // Stop timer when form closes to prevent memory leaks
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (refreshTimer != null)
            {
                refreshTimer.Stop();
                refreshTimer.Dispose();
            }
        }

        // Navigation buttons
        private void button1_Click(object sender, EventArgs e)
        {
            // Dashboard button - already on dashboard
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BlotterRecordsStaff f3 = new BlotterRecordsStaff();
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

        private void button9_Click(object sender, EventArgs e)
        {
            Form1 f3 = new Form1();
            f3.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ResidentStaff f3 = new ResidentStaff();
            f3.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FinancialReportsStaff f3 = new FinancialReportsStaff();
            f3.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Health_ServicesStaff f3 = new Health_ServicesStaff();
            f3.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }
    }
}
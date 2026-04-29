using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;
using MySql.Data.MySqlClient;


namespace Barangay_Management_System
{
    public partial class CertificatesStaff : Form
    {
        private BindingList<CertificateRequest> certificateRequests;
        private BindingSource bindingSource;
        private CertificateRequest selectedForExport;

        // UPDATE THIS with your actual MySQL password
        private string connectionString = "Server=127.0.0.1;Database=barangaymanagement;Uid=root;Pwd=;";

        public CertificatesStaff()
        {
            InitializeComponent();
            InitializeData();
            SetupDataGridView();
        }

        private void InitializeData()
        {
            certificateRequests = new BindingList<CertificateRequest>();
            LoadCertificateDataFromDatabase();  // Load from MySQL instead of hardcoded data

            bindingSource = new BindingSource();
            bindingSource.DataSource = certificateRequests;
        }

        private void LoadCertificateDataFromDatabase()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT id, RequestID, RequestorName, DocumentType, status, daterequested FROM certificate";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            certificateRequests.Add(new CertificateRequest
                            {
                                RequestID = reader["RequestID"].ToString(),
                                RequestorName = reader["RequestorName"].ToString(),
                                DocumentType = reader["DocumentType"].ToString(),
                                Status = reader["status"].ToString(),
                                RequestDate = Convert.ToDateTime(reader["daterequested"]),
                                Purpose = "N/A"  // Your DB doesn't have this column yet
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Database Error: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateCertificateStatusInDatabase(string requestId, string newStatus)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE certificate SET status = @status WHERE RequestID = @requestId";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@status", newStatus);
                        cmd.Parameters.AddWithValue("@requestId", requestId);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to update database: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupDataGridView()
        {
            dataGridView1.DataSource = bindingSource;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "RequestID",
                HeaderText = "Request ID",
                Width = 100
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "RequestorName",
                HeaderText = "Requestor Name",
                Width = 150
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DocumentType",
                HeaderText = "Document Type",
                Width = 150
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Status",
                HeaderText = "Status",
                Width = 100
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "RequestDate",
                HeaderText = "Request Date",
                Width = 120
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Purpose",
                HeaderText = "Purpose",
                Width = 200
            });

            dataGridView1.CellFormatting += DataGridView1_CellFormatting;
        }

        private void DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Status" && e.Value != null)
            {
                string status = e.Value.ToString();
                switch (status)
                {
                    case "Approved":
                    case "Accepted":
                        e.CellStyle.ForeColor = Color.Green;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        break;
                    case "Rejected":
                        e.CellStyle.ForeColor = Color.Red;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        break;
                    case "Pending":
                        e.CellStyle.ForeColor = Color.Orange;
                        break;
                }
            }
        }

        // APPROVE button
        private void btnApprove_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a certificate request to approve.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CertificateRequest selected = (CertificateRequest)dataGridView1.SelectedRows[0].DataBoundItem;

            if (selected.Status == "Approved" || selected.Status == "Accepted")
            {
                MessageBox.Show("This request is already approved.", "Already Approved",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (selected.Status == "Rejected")
            {
                MessageBox.Show("This request was already rejected.", "Already Rejected",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Approve certificate for {selected.RequestorName}?\n\nDocument: {selected.DocumentType}",
                "Confirm Approval",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                selected.Status = "Approved";
                selected.ApprovedDate = DateTime.Now;
                selected.ApprovedBy = "Staff";

                UpdateCertificateStatusInDatabase(selected.RequestID, "Accepted");

                bindingSource.ResetBindings(false);
                MessageBox.Show("Certificate approved successfully!", "Approved",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // REJECT button
        private void btnReject_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a certificate request to reject.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CertificateRequest selected = (CertificateRequest)dataGridView1.SelectedRows[0].DataBoundItem;

            if (selected.Status == "Rejected")
            {
                MessageBox.Show("This request is already rejected.", "Already Rejected",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (selected.Status == "Approved" || selected.Status == "Accepted")
            {
                MessageBox.Show("Cannot reject an already approved certificate.", "Already Approved",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string reason = ShowInputDialog("Enter reason for rejection:", "Rejection Reason");

            if (string.IsNullOrWhiteSpace(reason))
            {
                MessageBox.Show("Rejection cancelled. A reason is required.", "Cancelled",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Reject certificate for {selected.RequestorName}?\n\nReason: {reason}",
                "Confirm Rejection",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                selected.Status = "Rejected";
                selected.RejectionReason = reason;
                selected.RejectedDate = DateTime.Now;

                UpdateCertificateStatusInDatabase(selected.RequestID, "Rejected");

                bindingSource.ResetBindings(false);
                MessageBox.Show("Certificate request rejected.", "Rejected",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private string ShowInputDialog(string prompt, string title)
        {
            Form inputForm = new Form();
            inputForm.Text = title;
            inputForm.Size = new Size(400, 150);
            inputForm.StartPosition = FormStartPosition.CenterParent;
            inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputForm.MaximizeBox = false;
            inputForm.MinimizeBox = false;

            Label label = new Label();
            label.Text = prompt;
            label.Location = new Point(10, 10);
            label.Size = new Size(360, 20);
            inputForm.Controls.Add(label);

            TextBox textBox = new TextBox();
            textBox.Location = new Point(10, 35);
            textBox.Size = new Size(360, 20);
            inputForm.Controls.Add(textBox);

            Button okButton = new Button();
            okButton.Text = "OK";
            okButton.DialogResult = DialogResult.OK;
            okButton.Location = new Point(200, 70);
            inputForm.Controls.Add(okButton);

            Button cancelButton = new Button();
            cancelButton.Text = "Cancel";
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Location = new Point(290, 70);
            inputForm.Controls.Add(cancelButton);

            inputForm.AcceptButton = okButton;
            inputForm.CancelButton = cancelButton;

            if (inputForm.ShowDialog(this) == DialogResult.OK)
            {
                return textBox.Text;
            }
            return null;
        }

        // EXPORT TO PDF button
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a certificate to export.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CertificateRequest selected = (CertificateRequest)dataGridView1.SelectedRows[0].DataBoundItem;

            if (selected.Status != "Approved" && selected.Status != "Accepted")
            {
                MessageBox.Show("Only approved certificates can be exported.", "Not Approved",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            selectedForExport = selected;

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                FileName = $"Certificate_{selected.RequestID}_{selected.RequestorName.Replace(" ", "_")}.pdf",
                Title = "Save Certificate as PDF"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    PrintCertificateToPdf(saveFileDialog.FileName);
                    MessageBox.Show("Certificate exported successfully!", "Export Complete",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Export Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PrintCertificateToPdf(string filePath)
        {
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += PrintDoc_PrintPage;

            printDoc.PrinterSettings.PrinterName = "Microsoft Print to PDF";
            printDoc.PrinterSettings.PrintToFile = true;
            printDoc.PrinterSettings.PrintFileName = filePath;

            printDoc.Print();
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            Font titleFont = new Font("Arial", 18, FontStyle.Bold);
            Font headerFont = new Font("Arial", 14, FontStyle.Bold);
            Font bodyFont = new Font("Arial", 12);
            Font smallFont = new Font("Arial", 10);

            float yPos = 50;
            float centerX = e.PageBounds.Width / 2;

            g.DrawString("BARANGAY MANAGEMENT SYSTEM", titleFont, Brushes.Black,
                centerX - g.MeasureString("BARANGAY MANAGEMENT SYSTEM", titleFont).Width / 2, yPos);
            yPos += 30;

            g.DrawString("Official Certificate", headerFont, Brushes.Black,
                centerX - g.MeasureString("Official Certificate", headerFont).Width / 2, yPos);
            yPos += 50;

            string certTitle = $"CERTIFICATE OF {selectedForExport.DocumentType.ToUpper()}";
            g.DrawString(certTitle, headerFont, Brushes.Black,
                centerX - g.MeasureString(certTitle, headerFont).Width / 2, yPos);
            yPos += 50;

            string bodyText = $"This is to certify that {selectedForExport.RequestorName} has been granted " +
                $"a {selectedForExport.DocumentType} upon request for the purpose of: {selectedForExport.Purpose}.\n\n" +
                $"This certificate is issued on {selectedForExport.ApprovedDate?.ToString("MMMM dd, yyyy")} " +
                $"and approved by {selectedForExport.ApprovedBy}.\n\n" +
                $"Request ID: {selectedForExport.RequestID}\n" +
                $"Date Requested: {selectedForExport.RequestDate.ToString("MMMM dd, yyyy")}";

            RectangleF bodyRect = new RectangleF(50, yPos, e.PageBounds.Width - 100, 200);
            g.DrawString(bodyText, bodyFont, Brushes.Black, bodyRect);
            yPos += 220;

            g.DrawString("_________________________", bodyFont, Brushes.Black,
                centerX - g.MeasureString("_________________________", bodyFont).Width / 2, yPos);
            yPos += 20;
            g.DrawString("Barangay Captain/Authorized Signatory", smallFont, Brushes.Black,
                centerX - g.MeasureString("Barangay Captain/Authorized Signatory", smallFont).Width / 2, yPos);
            yPos += 15;
            g.DrawString("Bataan, Philippines", smallFont, Brushes.Black,
                centerX - g.MeasureString("Bataan, Philippines", smallFont).Width / 2, yPos);
            yPos += 40;

            g.DrawString($"This is a computer-generated certificate. Request ID: {selectedForExport.RequestID}",
                smallFont, Brushes.Gray,
                centerX - g.MeasureString($"This is a computer-generated certificate. Request ID: {selectedForExport.RequestID}", smallFont).Width / 2, yPos);
        }

        // Navigation buttons
        private void button1_Click(object sender, EventArgs e)
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

        private void button6_Click(object sender, EventArgs e)
        {
            AnnouncementStaff f3 = new AnnouncementStaff();
            f3.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form1 f3 = new Form1();
            f3.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            ResidentStaff f3 = new ResidentStaff();
            f3.Show();
            this.Hide();
        }

        private void button6_Click_1(object sender, EventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            BlotterRecordsStaff f3 = new BlotterRecordsStaff();
            f3.Show();
            this.Hide();
        }

        private void button13_Click(object sender, EventArgs e)
        {
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button13_Click_1(object sender, EventArgs e)
        {

        }
    }

    public class CertificateRequest
    {
        public string RequestID { get; set; }
        public string RequestorName { get; set; }
        public string DocumentType { get; set; }
        public string Status { get; set; }
        public DateTime RequestDate { get; set; }
        public string Purpose { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public string ApprovedBy { get; set; }
        public DateTime? RejectedDate { get; set; }
        public string RejectionReason { get; set; }
    }
}
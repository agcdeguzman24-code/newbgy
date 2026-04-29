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
    public partial class AnnouncementStaff : Form
    {
        private BindingList<Announcement> announcements;
        private BindingSource bindingSource;

        private string connectionString = "Server=127.0.0.1;Database=barangaymanagement;Uid=root;Pwd=;";

        // Store references to the buttons we find
        private Button btnExport;
        private Button btnAdd;
        private Button btnUpdate;
        private Button btnRemove;

        public AnnouncementStaff()
        {
            InitializeComponent();
            FindAndWireUpButtons();  // Find buttons by text and wire up events
            InitializeData();
            SetupDataGridView();
        }

        // Find buttons by their text and wire up events
        private void FindAndWireUpButtons()
        {
            // Search through all controls on the form
            foreach (Control ctrl in this.Controls)
            {
                FindButtonsInControl(ctrl);
            }

            // Wire up events if we found the buttons
            if (btnExport != null)
                btnExport.Click += btnExport_Click;
            else
                MessageBox.Show("Export button not found! Make sure a button has text 'EXPORT DATA'", "Warning");

            if (btnAdd != null)
                btnAdd.Click += btnAdd_Click;

            if (btnUpdate != null)
                btnUpdate.Click += btnUpdate_Click;

            if (btnRemove != null)
                btnRemove.Click += btnRemove_Click;
        }

        // Recursively search for buttons
        private void FindButtonsInControl(Control parent)
        {
            foreach (Control ctrl in parent.Controls)
            {
                if (ctrl is Button button)
                {
                    string text = button.Text.ToUpper().Trim();

                    if (text.Contains("EXPORT"))
                        btnExport = button;
                    else if (text.Contains("ADD") && text.Contains("ANNOUNCEMENT"))
                        btnAdd = button;
                    else if (text.Contains("UPDATE"))
                        btnUpdate = button;
                    else if (text.Contains("REMOVE"))
                        btnRemove = button;
                }

                // Search inside panels/groupboxes
                if (ctrl.HasChildren)
                    FindButtonsInControl(ctrl);
            }
        }

        private void InitializeData()
        {
            announcements = new BindingList<Announcement>();
            LoadAnnouncementsFromDatabase();

            bindingSource = new BindingSource();
            bindingSource.DataSource = announcements;
        }

        private void LoadAnnouncementsFromDatabase()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT id, Title, Author, DatePosted FROM announcement";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            announcements.Add(new Announcement
                            {
                                ID = Convert.ToInt32(reader["id"]),
                                Title = reader["Title"].ToString(),
                                Author = reader["Author"].ToString(),
                                DatePosted = Convert.ToDateTime(reader["DatePosted"])
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

        private void SetupDataGridView()
        {
            dataGridView2.DataSource = bindingSource;
            dataGridView2.AutoGenerateColumns = false;
            dataGridView2.Columns.Clear();

            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ID",
                HeaderText = "ID",
                Width = 50
            });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Title",
                HeaderText = "Title",
                Width = 200
            });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Author",
                HeaderText = "Author",
                Width = 150
            });
            dataGridView2.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DatePosted",
                HeaderText = "Date Posted",
                Width = 120
            });
        }

        // ==================== EXPORT TO PDF ====================
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (announcements == null || announcements.Count == 0)
            {
                MessageBox.Show("No data to export.", "No Data",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF files (*.pdf)|*.pdf",
                FileName = $"Announcements_{DateTime.Now:yyyyMMdd_HHmmss}.pdf",
                Title = "Save Announcements as PDF"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    ExportDataGridViewToPdf(saveFileDialog.FileName);
                    MessageBox.Show("Data exported successfully!", "Export Complete",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Export Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ExportDataGridViewToPdf(string filePath)
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
            Font titleFont = new Font("Arial", 16, FontStyle.Bold);
            Font headerFont = new Font("Arial", 10, FontStyle.Bold);
            Font cellFont = new Font("Arial", 9);
            Font footerFont = new Font("Arial", 8);

            float yPos = 40;
            float leftMargin = 50;
            float pageWidth = e.PageBounds.Width - 100;

            string title = "ANNOUNCEMENT REPORT";
            g.DrawString(title, titleFont, Brushes.Black,
                (e.PageBounds.Width - g.MeasureString(title, titleFont).Width) / 2, yPos);
            yPos += 35;

            string subtitle = $"Generated on: {DateTime.Now:MMMM dd, yyyy hh:mm tt}";
            g.DrawString(subtitle, footerFont, Brushes.Gray,
                (e.PageBounds.Width - g.MeasureString(subtitle, footerFont).Width) / 2, yPos);
            yPos += 30;

            float[] colWidths = { 50, 250, 150, 120 };
            string[] headers = { "ID", "Title", "Author", "Date Posted" };
            float xPos = leftMargin;

            g.FillRectangle(Brushes.LightGray, leftMargin, yPos, pageWidth, 25);

            for (int i = 0; i < headers.Length; i++)
            {
                g.DrawString(headers[i], headerFont, Brushes.Black, xPos, yPos + 5);
                xPos += colWidths[i];
            }
            yPos += 25;

            foreach (Announcement item in announcements)
            {
                xPos = leftMargin;

                if (yPos > e.PageBounds.Height - 100)
                {
                    e.HasMorePages = true;
                    return;
                }

                g.DrawString(item.ID.ToString(), cellFont, Brushes.Black, xPos, yPos + 3);
                xPos += colWidths[0];

                g.DrawString(item.Title, cellFont, Brushes.Black, xPos, yPos + 3);
                xPos += colWidths[1];

                g.DrawString(item.Author, cellFont, Brushes.Black, xPos, yPos + 3);
                xPos += colWidths[2];

                g.DrawString(item.DatePosted.ToString("MM/dd/yyyy"), cellFont, Brushes.Black, xPos, yPos + 3);

                yPos += 20;
                g.DrawLine(Pens.LightGray, leftMargin, yPos, leftMargin + pageWidth, yPos);
            }

            yPos += 15;
            string footer = $"Total Records: {announcements.Count}";
            g.DrawString(footer, footerFont, Brushes.Gray, leftMargin, yPos);

            e.HasMorePages = false;
        }

        // ==================== ADD NEW ANNOUNCEMENT ====================
        private void btnAdd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Add New Announcement feature - implement input form here.", "Add",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ==================== UPDATE ====================
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            announcements.Clear();
            LoadAnnouncementsFromDatabase();
            bindingSource.ResetBindings(false);
            MessageBox.Show("Data refreshed successfully!", "Update",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        // ==================== REMOVE SELECTED ====================
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an announcement to remove.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Announcement selected = (Announcement)dataGridView2.SelectedRows[0].DataBoundItem;

            DialogResult result = MessageBox.Show(
                $"Delete announcement '{selected.Title}'?",
                "Confirm Delete",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();
                        string query = "DELETE FROM announcement WHERE id = @id";

                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@id", selected.ID);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    announcements.Remove(selected);
                    bindingSource.ResetBindings(false);
                    MessageBox.Show("Announcement deleted successfully!", "Deleted",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error deleting: " + ex.Message, "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Navigation buttons
        private void button1_Click(object sender, EventArgs e)
        {
            DashboardStaff f3 = new DashboardStaff();
            f3.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CertificatesStaff f3 = new CertificatesStaff();
            f3.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            BlotterRecordsStaff f3 = new BlotterRecordsStaff();
            f3.Show();
            this.Hide();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form1 f3 = new Form1();
            f3.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ResidentStaff f3 = new ResidentStaff();
            f3.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
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

        private void button17_Click(object sender, EventArgs e)
        {
            DashboardStaff f3 = new DashboardStaff();
            f3.Show();
            this.Hide();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            BlotterRecordsStaff f3 = new BlotterRecordsStaff();
            f3.Show();
            this.Hide();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            AnnouncementStaff f3 = new AnnouncementStaff();
            f3.Show();
            this.Hide();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            CertificatesStaff f3 = new CertificatesStaff();
            f3.Show();
            this.Hide();
        }

        private void AnnouncementStaff_Load(object sender, EventArgs e)
        {
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
    }

    public partial class Announcement
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace Barangay_Management_System
{
    public partial class BlotterRecordsStaff : Form
    {
        private string connectionString = "server=127.0.0.1;user id=root;password=;database=barangaymanagement;";

        public BlotterRecordsStaff()
        {
            InitializeComponent();
            this.Load += BlotterRecordsStaff_Load;
        }

        private void BlotterRecordsStaff_Load(object sender, EventArgs e)
        {
            // DEBUG: Check if dataGridView1 exists
            if (dataGridView1 == null)
            {
                MessageBox.Show("ERROR: dataGridView1 is NULL!\n\nThe grid is not initialized in Designer.cs.\nCheck if your DataGridView has a different name.",
                    "Debug", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show($"dataGridView1 found!\nLocation: {dataGridView1.Location}\nSize: {dataGridView1.Size}\nVisible: {dataGridView1.Visible}",
                "Debug", MessageBoxButtons.OK, MessageBoxIcon.Information);

            SetupGridColumns();
            LoadBlotterData();
        }

        private void SetupGridColumns()
        {
            dataGridView1.Columns.Clear();
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;

            dataGridView1.Columns.Add("id", "ID");
            dataGridView1.Columns["id"].DataPropertyName = "id";

            dataGridView1.Columns.Add("blotter_id", "Blotter ID");
            dataGridView1.Columns["blotter_id"].DataPropertyName = "Blotter ID";

            dataGridView1.Columns.Add("date", "Date");
            dataGridView1.Columns["date"].DataPropertyName = "Date";

            dataGridView1.Columns.Add("complainant", "Complainant");
            dataGridView1.Columns["complainant"].DataPropertyName = "Complainant";

            dataGridView1.Columns.Add("incident_type", "Incident Type");
            dataGridView1.Columns["incident_type"].DataPropertyName = "IncidentType";

            dataGridView1.Columns.Add("location", "Location");
            dataGridView1.Columns["location"].DataPropertyName = "Location";

            dataGridView1.Columns.Add("status", "Status");
            dataGridView1.Columns["status"].DataPropertyName = "Status";

            MessageBox.Show($"Columns created: {dataGridView1.Columns.Count}", "Debug");
        }

        private void LoadBlotterData()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    MessageBox.Show("Database connected successfully!", "Debug");

                    string query = "SELECT * FROM blotter";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    MessageBox.Show($"Rows retrieved from database: {dt.Rows.Count}", "Debug");

                    if (dt.Rows.Count > 0)
                    {
                        MessageBox.Show($"First row: ID={dt.Rows[0]["id"]}, Complainant={dt.Rows[0]["Complainant"]}", "Debug");
                    }

                    dataGridView1.DataSource = dt;

                    MessageBox.Show($"Grid now shows: {dataGridView1.Rows.Count} rows", "Debug");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + "\n\n" + ex.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== ADD NEW RECORD ====================
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            AddNewRecord();
        }

        private void AddNewRecord()
        {
            Form inputForm = new Form();
            inputForm.Text = "Add New Blotter Record";
            inputForm.Size = new Size(400, 400);
            inputForm.StartPosition = FormStartPosition.CenterParent;
            inputForm.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputForm.MaximizeBox = false;
            inputForm.MinimizeBox = false;

            int yPos = 20;
            int labelX = 20;
            int textX = 120;
            int textWidth = 230;

            Label lblBlotterID = new Label() { Text = "Blotter ID:", Location = new Point(labelX, yPos), Width = 90 };
            TextBox txtBlotterID = new TextBox() { Location = new Point(textX, yPos), Width = textWidth };
            yPos += 35;

            Label lblDate = new Label() { Text = "Date:", Location = new Point(labelX, yPos), Width = 90 };
            DateTimePicker dtpDate = new DateTimePicker() { Location = new Point(textX, yPos), Width = textWidth, Format = DateTimePickerFormat.Short };
            yPos += 35;

            Label lblComplainant = new Label() { Text = "Complainant:", Location = new Point(labelX, yPos), Width = 90 };
            TextBox txtComplainant = new TextBox() { Location = new Point(textX, yPos), Width = textWidth };
            yPos += 35;

            Label lblIncidentType = new Label() { Text = "Incident Type:", Location = new Point(labelX, yPos), Width = 90 };
            TextBox txtIncidentType = new TextBox() { Location = new Point(textX, yPos), Width = textWidth };
            yPos += 35;

            Label lblLocation = new Label() { Text = "Location:", Location = new Point(labelX, yPos), Width = 90 };
            TextBox txtLocation = new TextBox() { Location = new Point(textX, yPos), Width = textWidth };
            yPos += 35;

            Label lblStatus = new Label() { Text = "Status:", Location = new Point(labelX, yPos), Width = 90 };
            ComboBox cmbStatus = new ComboBox() { Location = new Point(textX, yPos), Width = textWidth, DropDownStyle = ComboBoxStyle.DropDownList };
            cmbStatus.Items.AddRange(new string[] { "Pending", "Resolved", "Ongoing", "Closed" });
            cmbStatus.SelectedIndex = 0;
            yPos += 50;

            Button btnSave = new Button() { Text = "Save", Location = new Point(120, yPos), Width = 80, DialogResult = DialogResult.OK };
            Button btnCancel = new Button() { Text = "Cancel", Location = new Point(210, yPos), Width = 80, DialogResult = DialogResult.Cancel };

            inputForm.Controls.AddRange(new Control[] {
                lblBlotterID, txtBlotterID,
                lblDate, dtpDate,
                lblComplainant, txtComplainant,
                lblIncidentType, txtIncidentType,
                lblLocation, txtLocation,
                lblStatus, cmbStatus,
                btnSave, btnCancel
            });

            inputForm.AcceptButton = btnSave;
            inputForm.CancelButton = btnCancel;

            if (inputForm.ShowDialog(this) == DialogResult.OK)
            {
                if (string.IsNullOrWhiteSpace(txtBlotterID.Text) ||
                    string.IsNullOrWhiteSpace(txtComplainant.Text) ||
                    string.IsNullOrWhiteSpace(txtIncidentType.Text) ||
                    string.IsNullOrWhiteSpace(txtLocation.Text))
                {
                    MessageBox.Show("Please fill in all fields!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();

                        string query = @"INSERT INTO blotter 
                            (`Blotter ID`, `Date`, `Complainant`, `IncidentType`, `Location`, `Status`) 
                            VALUES 
                            (@BlotterID, @Date, @Complainant, @IncidentType, @Location, @Status)";

                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@BlotterID", txtBlotterID.Text.Trim());
                            cmd.Parameters.AddWithValue("@Date", dtpDate.Value.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@Complainant", txtComplainant.Text.Trim());
                            cmd.Parameters.AddWithValue("@IncidentType", txtIncidentType.Text.Trim());
                            cmd.Parameters.AddWithValue("@Location", txtLocation.Text.Trim());
                            cmd.Parameters.AddWithValue("@Status", cmbStatus.SelectedItem.ToString());

                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Record added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadBlotterData();
                            }
                            else
                            {
                                MessageBox.Show("Failed to add record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error adding record: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AnnouncementStaff f3 = new AnnouncementStaff();
            f3.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 f3 = new Form1();
            f3.Show();
            this.Hide();
        }

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

        private void button9_Click(object sender, EventArgs e)
        {
            Form1 f3 = new Form1();
            f3.Show();
            this.Hide();
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

        private void button3_Click(object sender, EventArgs e)
        {
            AnnouncementStaff f3 = new AnnouncementStaff();
            f3.Show();
            this.Hide();
        }
    }
}

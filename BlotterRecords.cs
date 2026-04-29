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

namespace BarangayManagement-System
{
    public partial class BlotterRecords : Form
    {
        private BindingList<BlotterRecord> blotterRecords;
        private BindingSource bindingSource;


        private string connectionString = "Server=127.0.0.1;Database=barangaymanagement;Uid=root;Pwd=;";

        public BlotterRecords()
        {
            InitializeComponent();
            InitializeData();
            SetupDataGridView();
        }

        private void InitializeData()
        {
            blotterRecords = new BindingList<BlotterRecord>();
            LoadBlotterRecordsFromDatabase();

            bindingSource = new BindingSource();
            bindingSource.DataSource = blotterRecords;
        }

        private void LoadBlotterRecordsFromDatabase()
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT id, Blotter Id, Date, Compliant, IncidentType, Location, SELECT * FROM blotter";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            blotterRecords.Add(new BlotterRecord
                            {
                                ID = Convert.ToInt32(reader["id"]),
                                BlotterId = reader["BlotterId"].ToString(),
                                Date = Convert.ToDateTime(reader["Date"]),
                                Compliant = reader["Compliant"].ToString(),
                                IncidentType = reader["IncidentType"].ToString(),
                                Location = reader["Location"].ToString(),
                                Status = reader["Status"].ToString()
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
            dataGridView1.DataSource = bindingSource;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ID",
                HeaderText = "ID",
                Width = 50
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "BlotterId",
                HeaderText = "Blotter ID",
                Width = 100
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Date",
                HeaderText = "Date",
                Width = 100
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Compliant",
                HeaderText = "Complainant",
                Width = 150
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "IncidentType",
                HeaderText = "Incident Type",
                Width = 150
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Location",
                HeaderText = "Location",
                Width = 150
            });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Status",
                HeaderText = "Status",
                Width = 100
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
                    case "Resolved":
                        e.CellStyle.ForeColor = Color.Green;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        break;
                    case "Pending":
                        e.CellStyle.ForeColor = Color.Orange;
                        break;
                    case "Rejected":
                        e.CellStyle.ForeColor = Color.Red;
                        e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                        break;
                }
            }
        }

        // Navigation buttons
        private void button1_Click(object sender, EventArgs e)
        {
            Dashboard f3 = new Dashboard();
            f3.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Resident f3 = new Resident();
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }

    // Blotter Record Model
    public class BlotterRecord
    {
        public int ID { get; set; }
        public string BlotterId { get; set; }
        public DateTime Date { get; set; }
        public string Compliant { get; set; }
        public string IncidentType { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
    }
}
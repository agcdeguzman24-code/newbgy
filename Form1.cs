using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Barangay_Management_System
{
    public partial class Form1 : Form
    {
        string connectionString = "server=localhost;user id=root;password=;database=barangaymanagement";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btn1_Click(sender, e);
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text.Trim();
            string password = textBox2.Text;

            if (string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Email is required.");
                return;
            }

            if (string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("Password is required.");
                return;
            }

            if (!IsValidEmail(email))
            {
                MessageBox.Show("Invalid email format.");
                return;
            }

            using (MySqlConnection con = new MySqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    string query = "SELECT email, password, role FROM users";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    MySqlDataReader reader = cmd.ExecuteReader();

                    bool isValid = false;
                    string role = "";

                    while (reader.Read())
                    {
                        string dbEmail = reader["email"].ToString();
                        string dbPassword = reader["password"].ToString();

                        if (dbEmail == email && dbPassword == password)
                        {
                            isValid = true;
                            role = reader["role"].ToString();
                            break;
                        }
                    }

                    reader.Close();

                    if (isValid)
                    {
                        MessageBox.Show("Login Successful!");

                        if (role == "admin")
                        {
                            new Dashboard().Show();
                        }
                        else
                        {
                            new DashboardStaff().Show();
                        }

                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Invalid email or password.");
                        textBox2.Clear();
                        textBox2.Focus();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
        }

        private bool IsValidEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
    }
}
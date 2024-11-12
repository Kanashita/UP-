using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp1
{
    public partial class LoginForm : Form
    {
        
        private string DB = "Data Source=AC_UAA.db;Version=3;";


        public LoginForm()
        {
            InitializeComponent();
        }
        
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox1.Checked)
            {
                textBox2.UseSystemPasswordChar = true;
            }

            else
            {
                textBox2.UseSystemPasswordChar = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Login_User = textBox1.Text;
            string Password_User = textBox2.Text;
            string query = @"SELECT * FROM Users WHERE Login_User=@Login_User AND Password_User=@Password_User";
            
                
            using (SQLiteConnection conn = new SQLiteConnection(DB))
            {
                SQLiteCommand command = new SQLiteCommand(query, conn);
                    command.Parameters.AddWithValue("@Login_User", Login_User);
                    command.Parameters.AddWithValue("@Password_User", Password_User);
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(command);
                    DataTable dataTable = new DataTable();

                    try
                    {
                        conn.Open();
                        adapter.Fill(dataTable);
                        dataGridView1.DataSource = dataTable;
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка:" + ex.Message + ex.StackTrace);
                    }
    
                
            }
            if (dataGridView1.RowCount > 1)
            {
                UserForm userForm = new UserForm();
                userForm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль");
            }
                
        }

        

            private void LoginForm_Load(object sender, EventArgs e)
            {
            textBox2.UseSystemPasswordChar = true;
            }

        
    }
}

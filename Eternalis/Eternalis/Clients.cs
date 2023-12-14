using Guna.UI2.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eternalis
{
    public partial class Clients : Form
    {
        private string currentName;
        private string currentSurname;
        private string currentPatronymic;
        private int currentEmployeeID;
        string connectionString = "Data Source=DESKTOP-HURB5A2\\SQLEXPRESS;Initial Catalog=Eternalis;Integrated Security=True";

        public Clients(int employeeID, string name, string surname, string patronymic)
        {
            InitializeComponent();
            // Сохраняем переданные данные
            currentEmployeeID = employeeID;
            currentName = name;
            currentSurname = surname;
            currentPatronymic = patronymic;
            // Загрузить данные из базы данных в materialListView1
            
            materialListView1.Columns.Add("Фамилия", 150); 
            materialListView1.Columns.Add("Имя", 150); 
            materialListView1.Columns.Add("Отчество", 150); 
            materialListView1.Columns.Add("Дата рождения", 150); 
            materialListView1.Columns.Add("Адрес", 200); 
            materialListView1.Columns.Add("Телефон", 150); 
            materialListView1.Columns.Add("Email", 200); 
            LoadClientData();


        }
        private void LoadClientData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT [Фамилия], [Имя], [Отчество], CONVERT(DATE, [Дата_рождения]) AS [Дата_рождения], [Адрес], [Телефон], [Email] FROM [Клиенты]";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(new[]
                            {
                        reader["Фамилия"].ToString(),
                        reader["Имя"].ToString(),
                        reader["Отчество"].ToString(),
                        DateTime.Parse(reader["Дата_рождения"].ToString()).ToShortDateString(), // Преобразуем в короткий формат даты без времени
                        reader["Адрес"].ToString(),
                        reader["Телефон"].ToString(),
                        reader["Email"].ToString()
                    });

                            materialListView1.Items.Add(item);
                        }
                    }
                }
            }
        }

        private void materialFloatingActionButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Clients_Load(object sender, EventArgs e)
        {
            materialLabel1.Text = $"Работает: {currentName} {currentSurname} {currentPatronymic}";
            
        }
        
        private void materialButton4_Click(object sender, EventArgs e)
        {
            Form1 newForm = new Form1();
            newForm.Show();
            this.Hide();
        }

        

        private void materialTextBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = materialTextBox1.Text.Trim().ToLower();

            // Очищаем список клиентов
            materialListView1.Items.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Модифицированный запрос, который ищет по всем полям (имя, фамилия, отчество) и по полному имени
                string query = "SELECT [Фамилия], [Имя], [Отчество], CONVERT(DATE, [Дата_рождения]) AS [Дата_рождения], [Адрес], [Телефон], [Email] FROM [Клиенты] " +
                               "WHERE LOWER([Фамилия]) LIKE @searchText OR LOWER([Имя]) LIKE @searchText OR LOWER([Отчество]) LIKE @searchText OR " +
                               "LOWER([Фамилия] + ' ' + [Имя]) LIKE @searchText OR " +
                               "LOWER([Фамилия] + ' ' + [Отчество]) LIKE @searchText OR " +
                               "LOWER([Имя] + ' ' + [Отчество]) LIKE @searchText OR " +
                               "LOWER([Фамилия] + ' ' + [Имя] + ' ' + [Отчество]) LIKE @searchText OR " +
                               "LOWER([Фамилия] + ' ' + [Отчество] + ' ' + [Имя]) LIKE @searchText OR " +
                               "LOWER([Имя] + ' ' + [Фамилия] + ' ' + [Отчество]) LIKE @searchText OR " +
                               "LOWER([Имя] + ' ' + [Отчество] + ' ' + [Фамилия]) LIKE @searchText";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@searchText", "%" + searchText + "%");

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            ListViewItem item = new ListViewItem(new[]
                            {
                        reader["Фамилия"].ToString(),
                        reader["Имя"].ToString(),
                        reader["Отчество"].ToString(),
                        DateTime.Parse(reader["Дата_рождения"].ToString()).ToShortDateString(),
                        reader["Адрес"].ToString(),
                        reader["Телефон"].ToString(),
                        reader["Email"].ToString()
                    });

                            materialListView1.Items.Add(item);
                        }
                    }
                }
            }
        }

        private void materialButton5_Click(object sender, EventArgs e)
        {
            NewClient addClient = new NewClient(currentEmployeeID, currentName, currentSurname, currentPatronymic);
            addClient.Show();
            this.Hide();
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {

        }

        private void materialFloatingActionButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            Deceased deceasedForm = new Deceased(currentEmployeeID, currentName, currentSurname, currentPatronymic);
            deceasedForm.Show();
            this.Hide();
        }

        private void materialTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void materialButton3_Click(object sender, EventArgs e)
        {
            viewOrders newOrder = new viewOrders(currentEmployeeID, currentName, currentSurname, currentPatronymic);
            newOrder.Show();
            this.Hide();
        }
    }
}

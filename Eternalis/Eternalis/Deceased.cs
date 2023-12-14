using Guna.UI2.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eternalis
{
    public partial class Deceased : Form
    {
        private string currentName;
        private string currentSurname;
        private string currentPatronymic;
        private int currentEmployeeID;
        string connectionString = "Data Source=DESKTOP-HURB5A2\\SQLEXPRESS;Initial Catalog=Eternalis;Integrated Security=True";
        public Deceased(int employeeID, string name, string surname, string patronymic)
        {
            InitializeComponent();
            // Сохраняем переданные данные
            currentEmployeeID = employeeID;
            currentName = name;
            currentSurname = surname;
            currentPatronymic = patronymic;
            materialListView1.Columns.Add("Фамилия", 150);
            materialListView1.Columns.Add("Имя", 150);
            materialListView1.Columns.Add("Отчество", 150);
            materialListView1.Columns.Add("Дата рождения", 150);
            materialListView1.Columns.Add("Дата смерти", 150);
            materialListView1.Columns.Add("Клиент", 220);
            LoadDeceasedData();
        }
        private void LoadDeceasedData()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = @"SELECT P.[Фамилия], P.[Имя], P.[Отчество], CONVERT(DATE, P.[Дата_рождения]) AS [Дата_рождения],
                        CONVERT(DATE, P.[Дата_смерти]) AS [Дата_смерти], 
                        CONCAT(C.[Фамилия], ' ', C.[Имя], ' ', C.[Отчество]) AS [ФИО клиента]
                        FROM [Покойные] P
                        LEFT JOIN [Клиенты] C ON P.[ID_клиента] = C.[ID_клиента]";

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
                        DateTime.Parse(reader["Дата_рождения"].ToString()).ToShortDateString(),
                        DateTime.Parse(reader["Дата_смерти"].ToString()).ToShortDateString(),
                        reader["ФИО клиента"].ToString()
                    });

                            materialListView1.Items.Add(item);
                        }
                    }
                }
            }
        }
        private void Deceased_Load(object sender, EventArgs e)
        {
            materialLabel1.Text = $"Работает: {currentName} {currentSurname} {currentPatronymic}";
        }

        private void materialFloatingActionButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void materialTextBox1_TextChanged(object sender, EventArgs e)
        {
            string searchText = materialTextBox1.Text.Trim().ToLower();

            // Очищаем список клиентов
            materialListView1.Items.Clear();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Модифицированный запрос с левым соединением для получения информации о клиенте
                string query = @"SELECT P.[Фамилия], P.[Имя], P.[Отчество], 
                        CONVERT(DATE, P.[Дата_рождения]) AS [Дата_рождения], 
                        CONVERT(DATE, P.[Дата_смерти]) AS [Дата_смерти],
                        CONCAT(C.[Фамилия], ' ', C.[Имя], ' ', C.[Отчество]) AS [ФИО клиента]
                        FROM [Покойные] P
                        LEFT JOIN [Клиенты] C ON P.[ID_клиента] = C.[ID_клиента]
                        WHERE (LOWER(P.[Фамилия]) LIKE @searchText OR LOWER(P.[Имя]) LIKE @searchText OR LOWER(P.[Отчество]) LIKE @searchText OR " +
                                "LOWER(P.[Фамилия] + ' ' + P.[Имя]) LIKE @searchText OR " +
                                "LOWER(P.[Фамилия] + ' ' + P.[Отчество]) LIKE @searchText OR " +
                                "LOWER(P.[Имя] + ' ' + P.[Отчество]) LIKE @searchText OR " +
                                "LOWER(P.[Фамилия] + ' ' + P.[Имя] + ' ' + P.[Отчество]) LIKE @searchText OR " +
                                "LOWER(P.[Фамилия] + ' ' + P.[Отчество] + ' ' + P.[Имя]) LIKE @searchText OR " +
                                "LOWER(P.[Имя] + ' ' + P.[Фамилия] + ' ' + P.[Отчество]) LIKE @searchText OR " +
                                "LOWER(P.[Имя] + ' ' + P.[Отчество] + ' ' + P.[Фамилия]) LIKE @searchText OR " +
                                "LOWER(P.[Отчество] + ' ' + P.[Фамилия] + ' ' + P.[Имя]) LIKE @searchText OR " +
                                "LOWER(P.[Отчество] + ' ' + P.[Имя] + ' ' + P.[Фамилия]) LIKE @searchText)";

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
                        DateTime.Parse(reader["Дата_смерти"].ToString()).ToShortDateString(),
                        reader["ФИО клиента"].ToString()
                    });

                            materialListView1.Items.Add(item);
                        }
                    }
                }
            }
        }

        private void materialButton5_Click(object sender, EventArgs e)
        {
            newDeceased addDeceased = new newDeceased(currentEmployeeID, currentName, currentSurname, currentPatronymic);
            addDeceased.Show();
            this.Hide();
        }

        private void materialFloatingActionButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            Clients clientForm = new Clients(currentEmployeeID, currentName, currentSurname, currentPatronymic);
            clientForm.Show();
            this.Hide();
        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
            Form1 newForm = new Form1();
            newForm.Show();
            this.Hide();
        }

        private void materialButton3_Click(object sender, EventArgs e)
        {
            viewOrders newOrder = new viewOrders(currentEmployeeID, currentName, currentSurname, currentPatronymic);
            newOrder.Show();
            this.Hide();
        }
    }
    }


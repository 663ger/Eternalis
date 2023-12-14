using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Core;

namespace Eternalis
{
    public partial class adminOrders : Form
    {
        private string currentName;
        private string currentSurname;
        private string currentPatronymic;
        private int currentEmployeeID;
        string connectionString = "Data Source=DESKTOP-HURB5A2\\SQLEXPRESS;Initial Catalog=Eternalis;Integrated Security=True";
        public adminOrders(int employeeID, string name, string surname, string patronymic)
        {
            InitializeComponent();
            // Сохраняем переданные данные
            currentEmployeeID = employeeID;
            currentName = name;
            currentSurname = surname;
            currentPatronymic = patronymic;
        }
        private void FillDataGridView()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Запрос для объединения данных из двух таблиц
                    string query = @"
                SELECT 
                    Заказы.[ID_заказа],
                    Клиенты.[Фамилия] + ' ' + Клиенты.[Имя] + ' ' + Клиенты.[Отчество] AS Клиент,
                    Покойные.[Фамилия] + ' ' + Покойные.[Имя] + ' ' + Покойные.[Отчество] AS Покойный,
                    Сотрудники.[Фамилия] + ' ' + Сотрудники.[Имя] + ' ' + Сотрудники.[Отчество] AS Сотрудник,
                    Виды_похорон.[Наименование] AS Вид_похорон,
                    Виды_похорон.[Адрес] AS Адрес_похорон,
                    Виды_похорон.[Описание] AS Описание_похорон,
                    STRING_AGG(Услуги.[Название], ', ') AS Наименование_услуги,
                    Заказы.[Итоговая_сумма] AS Итоговая_сумма,
                    Заказы.[Дата_похорон] AS Дата_похорон,
                    Заказы.[Дата_заказа] AS Дата_заказа
                FROM 
                    [Eternalis].[dbo].[Заказы]
                LEFT JOIN 
                    [Eternalis].[dbo].[Услуги_заказа] ON Заказы.[ID_заказа] = Услуги_заказа.[ID_заказа]
                LEFT JOIN 
                    [Eternalis].[dbo].[Клиенты] ON Заказы.[ID_клиента] = Клиенты.[ID_клиента]
                LEFT JOIN 
                    [Eternalis].[dbo].[Покойные] ON Заказы.[ID_покойного] = Покойные.[ID_покойного]
                LEFT JOIN 
                    [Eternalis].[dbo].[Сотрудники] ON Заказы.[ID_сотрудника] = Сотрудники.[ID_сотрудника]
                LEFT JOIN 
                    [Eternalis].[dbo].[Виды_похорон] ON Заказы.[ID_вида_похорон] = Виды_похорон.[ID_вида_похорон]
                LEFT JOIN 
                    [Eternalis].[dbo].[Услуги] ON Услуги_заказа.[ID_услуги] = Услуги.[ID_услуги]
                GROUP BY 
                    Заказы.[ID_заказа],
                    Клиенты.[Фамилия], Клиенты.[Имя], Клиенты.[Отчество],
                    Покойные.[Фамилия], Покойные.[Имя], Покойные.[Отчество],
                    Сотрудники.[Фамилия], Сотрудники.[Имя], Сотрудники.[Отчество],
                    Виды_похорон.[Наименование], Виды_похорон.[Адрес], Виды_похорон.[Описание],
                    Заказы.[Итоговая_сумма],
                    Заказы.[Дата_похорон],
                    Заказы.[Дата_заказа]";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Привязываем DataTable к DataGridView
                    dataGridView1.DataSource = dataTable;

                    // Скрываем столбцы Id_заказа и Дата_заказа
                    dataGridView1.Columns["ID_заказа"].Visible = false;

                    // Устанавливаем названия столбцов
                    dataGridView1.Columns["Клиент"].HeaderText = "Клиент";
                    dataGridView1.Columns["Покойный"].HeaderText = "Покойный";
                    dataGridView1.Columns["Сотрудник"].HeaderText = "Сотрудник";
                    dataGridView1.Columns["Вид_похорон"].HeaderText = "Вид похорон";
                    dataGridView1.Columns["Адрес_похорон"].HeaderText = "Адрес похорон";
                    dataGridView1.Columns["Описание_похорон"].HeaderText = "Описание похорон";
                    dataGridView1.Columns["Наименование_услуги"].HeaderText = "Наименование услуги";
                    dataGridView1.Columns["Итоговая_сумма"].HeaderText = "Итоговая сумма";
                    dataGridView1.Columns["Дата_похорон"].HeaderText = "Дата похорон";
                    dataGridView1.Columns["Дата_заказа"].HeaderText = "Дата заказа";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
            Admin newAdmin = new Admin(currentEmployeeID, currentName, currentSurname, currentPatronymic);
            newAdmin.Show();
            this.Hide();
        }

        private void adminOrders_Load(object sender, EventArgs e)
        {
            materialLabel1.Text = $"Работает: {currentName} {currentSurname} {currentPatronymic}";
            FillDataGridView();  
        }
        private void SearchOrderByDate(DateTime searchDate)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Запрос для объединения данных из двух таблиц и фильтрации по дате заказа
                    string query = @"
                SELECT 
                    Заказы.[ID_заказа],
                    Клиенты.[Фамилия] + ' ' + Клиенты.[Имя] + ' ' + Клиенты.[Отчество] AS Клиент,
                    Покойные.[Фамилия] + ' ' + Покойные.[Имя] + ' ' + Покойные.[Отчество] AS Покойный,
                    Сотрудники.[Фамилия] + ' ' + Сотрудники.[Имя] + ' ' + Сотрудники.[Отчество] AS Сотрудник,
                    Виды_похорон.[Наименование] AS Вид_похорон,
                    Виды_похорон.[Адрес] AS Адрес_похорон,
                    Виды_похорон.[Описание] AS Описание_похорон,
                    STRING_AGG(Услуги.[Название], ', ') AS Наименование_услуги,
                    Заказы.[Итоговая_сумма] AS Итоговая_сумма,
                    Заказы.[Дата_похорон] AS Дата_похорон,
                    Заказы.[Дата_заказа] AS Дата_заказа
                FROM 
                    [Eternalis].[dbo].[Заказы]
                LEFT JOIN 
                    [Eternalis].[dbo].[Услуги_заказа] ON Заказы.[ID_заказа] = Услуги_заказа.[ID_заказа]
                LEFT JOIN 
                    [Eternalis].[dbo].[Клиенты] ON Заказы.[ID_клиента] = Клиенты.[ID_клиента]
                LEFT JOIN 
                    [Eternalis].[dbo].[Покойные] ON Заказы.[ID_покойного] = Покойные.[ID_покойного]
                LEFT JOIN 
                    [Eternalis].[dbo].[Сотрудники] ON Заказы.[ID_сотрудника] = Сотрудники.[ID_сотрудника]
                LEFT JOIN 
                    [Eternalis].[dbo].[Виды_похорон] ON Заказы.[ID_вида_похорон] = Виды_похорон.[ID_вида_похорон]
                LEFT JOIN 
                    [Eternalis].[dbo].[Услуги] ON Услуги_заказа.[ID_услуги] = Услуги.[ID_услуги]
                WHERE 
                    Заказы.[Дата_заказа] = @searchDate
                GROUP BY 
                    Заказы.[ID_заказа],
                    Клиенты.[Фамилия], Клиенты.[Имя], Клиенты.[Отчество],
                    Покойные.[Фамилия], Покойные.[Имя], Покойные.[Отчество],
                    Сотрудники.[Фамилия], Сотрудники.[Имя], Сотрудники.[Отчество],
                    Виды_похорон.[Наименование], Виды_похорон.[Адрес], Виды_похорон.[Описание],
                    Заказы.[Итоговая_сумма],
                    Заказы.[Дата_похорон],
                    Заказы.[Дата_заказа]";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@searchDate", searchDate);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Привязываем DataTable к DataGridView
                    dataGridView1.DataSource = dataTable;

                    // Скрываем столбцы Id_заказа и Дата_заказа
                    dataGridView1.Columns["ID_заказа"].Visible = false;

                    // Устанавливаем названия столбцов
                    dataGridView1.Columns["Клиент"].HeaderText = "Клиент";
                    dataGridView1.Columns["Покойный"].HeaderText = "Покойный";
                    dataGridView1.Columns["Сотрудник"].HeaderText = "Сотрудник";
                    dataGridView1.Columns["Вид_похорон"].HeaderText = "Вид похорон";
                    dataGridView1.Columns["Адрес_похорон"].HeaderText = "Адрес похорон";
                    dataGridView1.Columns["Описание_похорон"].HeaderText = "Описание похорон";
                    dataGridView1.Columns["Наименование_услуги"].HeaderText = "Наименование услуги";
                    dataGridView1.Columns["Итоговая_сумма"].HeaderText = "Итоговая сумма";
                    dataGridView1.Columns["Дата_похорон"].HeaderText = "Дата похорон";
                    dataGridView1.Columns["Дата_заказа"].HeaderText = "Дата заказа";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void materialButton5_Click(object sender, EventArgs e)
        {
            SearchOrderByDate(guna2DateTimePicker1.Value);
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            FillDataGridView();
        }

        private void materialFloatingActionButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void materialFloatingActionButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Eternalis
{
    public partial class adminEmployees : Form
    {
        private string currentName;
        private string currentSurname;
        private string currentPatronymic;
        private int currentEmployeeID;
        string connectionString = "Data Source=DESKTOP-HURB5A2\\SQLEXPRESS;Initial Catalog=Eternalis;Integrated Security=True";
        public adminEmployees(int employeeID, string name, string surname, string patronymic)
        {
            InitializeComponent();
            // Сохраняем переданные данные
            currentEmployeeID = employeeID;
            currentName = name;
            currentSurname = surname;
            currentPatronymic = patronymic;
        }

        private void adminEmployees_Load(object sender, EventArgs e)
        {
            materialLabel1.Text = $"Работает: {currentName} {currentSurname} {currentPatronymic}";
            LoadDataIntoDataGridView();
        }
        private void LoadDataIntoDataGridView()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT TOP (1000) 
                    s.[ID_сотрудника],
                    s.[Фамилия],
                    s.[Имя],
                    s.[Отчество],
                    s.[Дата_рождения],
                    s.[Адрес],
                    s.[Телефон],
                    s.[Email],
                    s.[Логин],
                    s.[Пароль],
                    d.[Название] AS [Должность]
                FROM [Eternalis].[dbo].[Сотрудники] s
                INNER JOIN [Eternalis].[dbo].[Должности] d ON s.[ID_должности] = d.[ID_должности]";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Привязываем DataTable к DataGridView
                    dataGridView1.DataSource = dataTable;

                    // Скрываем столбец с ID_сотрудника
                    dataGridView1.Columns["ID_сотрудника"].Visible = false;

                    // Устанавливаем названия столбцов
                    dataGridView1.Columns["Фамилия"].HeaderText = "Фамилия";
                    dataGridView1.Columns["Имя"].HeaderText = "Имя";
                    dataGridView1.Columns["Отчество"].HeaderText = "Отчество";
                    dataGridView1.Columns["Дата_рождения"].HeaderText = "Дата рождения";
                    dataGridView1.Columns["Адрес"].HeaderText = "Адрес";
                    dataGridView1.Columns["Телефон"].HeaderText = "Телефон";
                    dataGridView1.Columns["Email"].HeaderText = "Email";
                    dataGridView1.Columns["Логин"].HeaderText = "Логин";
                    dataGridView1.Columns["Пароль"].HeaderText = "Пароль";
                    dataGridView1.Columns["Должность"].HeaderText = "Должность";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void materialFloatingActionButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void materialFloatingActionButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void materialButton3_Click(object sender, EventArgs e)
        {
            adminNewEmploye newAddEmp = new adminNewEmploye(currentEmployeeID, currentName, currentSurname, currentPatronymic);
            newAddEmp.Show();
            this.Hide();
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            // Проверка выбора строки в DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Получаем ID выбранного сотрудника
                int selectedEmployeeID = (int)dataGridView1.SelectedRows[0].Cells["ID_сотрудника"].Value;

                // Получаем имя и фамилию выбранного сотрудника для отображения в сообщении
                string selectedEmployeeName = dataGridView1.SelectedRows[0].Cells["Имя"].Value.ToString();
                string selectedEmployeeSurname = dataGridView1.SelectedRows[0].Cells["Фамилия"].Value.ToString();

                // Показываем сообщение с подтверждением удаления
                DialogResult result = MessageBox.Show($"Вы действительно хотите удалить сотрудника {selectedEmployeeSurname} {selectedEmployeeName}?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Удаление сотрудника
                    DeleteEmployee(selectedEmployeeID);

                    // Перезагрузка данных в DataGridView
                    LoadDataIntoDataGridView();
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для удаления.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void DeleteEmployee(int employeeID)
        {
            // Создание подключения к базе данных
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Создание команды SQL для удаления сотрудника
                    string sqlQuery = "DELETE FROM [Eternalis].[dbo].[Сотрудники] WHERE [ID_сотрудника] = @EmployeeID";
                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@EmployeeID", employeeID);

                    // Выполнение команды SQL
                    command.ExecuteNonQuery();

                    MessageBox.Show("Сотрудник успешно удален.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
            Admin adminForm = new Admin(currentEmployeeID, currentName, currentSurname, currentPatronymic);
            adminForm.Show();
            this.Hide();
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            // Проверка выбранной строки в DataGridView
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Получение ID выбранного сотрудника
                int selectedEmployeeID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_сотрудника"].Value);

                // Запрос на получение данных о выбранном сотруднике
                string query = @"
            SELECT 
                s.[Имя],
                s.[Фамилия],
                s.[Отчество],
                s.[Дата_рождения],
                s.[Адрес],
                s.[Телефон],
                s.[Email],
                s.[Логин],
                s.[Пароль],
                s.[ID_должности]
            FROM [Eternalis].[dbo].[Сотрудники] s
            WHERE s.[ID_сотрудника] = @EmployeeID";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@EmployeeID", selectedEmployeeID);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    // Передача данных на форму редактирования
                                    adminEditEmploye editForm = new adminEditEmploye(
                                        currentEmployeeID,
                                        currentName,
                                        currentSurname,
                                        currentPatronymic,
                                        selectedEmployeeID,
                                        reader.GetString(0),  // Имя
                                        reader.GetString(1),  // Фамилия
                                        reader.GetString(2),  // Отчество
                                        reader.GetDateTime(3), // Дата рождения
                                        reader.GetString(4),  // Адрес
                                        reader.GetString(5),  // Телефон
                                        reader.GetString(6),  // Email
                                        reader.GetString(7),  // Логин
                                        reader.GetString(8),  // Пароль
                                        reader.GetInt32(9)     // ID_должности
                                    );

                                    editForm.ShowDialog();
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для редактирования.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void materialTextBox1_TextChanged(object sender, EventArgs e)
        {
            SearchEmployee(materialTextBox1.Text.ToLower());
        }
        private void SearchEmployee(string searchText)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT TOP (1000) 
                    s.[ID_сотрудника],
                    s.[Фамилия],
                    s.[Имя],
                    s.[Отчество],
                    s.[Дата_рождения],
                    s.[Адрес],
                    s.[Телефон],
                    s.[Email],
                    s.[Логин],
                    s.[Пароль],
                    d.[Название] AS [Должность]
                FROM [Eternalis].[dbo].[Сотрудники] s
                INNER JOIN [Eternalis].[dbo].[Должности] d ON s.[ID_должности] = d.[ID_должности]
                WHERE LOWER(s.[Фамилия]) LIKE @searchText OR
                      LOWER(s.[Имя]) LIKE @searchText OR
                      LOWER(s.[Отчество]) LIKE @searchText OR
                      LOWER(s.[Фамилия] + ' ' + s.[Имя]) LIKE @searchText OR
                      LOWER(s.[Фамилия] + ' ' + s.[Отчество]) LIKE @searchText OR
                      LOWER(s.[Имя] + ' ' + s.[Отчество]) LIKE @searchText OR
                      LOWER(s.[Фамилия] + ' ' + s.[Имя] + ' ' + s.[Отчество]) LIKE @searchText OR
                      LOWER(s.[Фамилия] + ' ' + s.[Отчество] + ' ' + s.[Имя]) LIKE @searchText OR
                      LOWER(s.[Имя] + ' ' + s.[Фамилия] + ' ' + s.[Отчество]) LIKE @searchText OR
                      LOWER(s.[Имя] + ' ' + s.[Отчество] + ' ' + s.[Фамилия]) LIKE @searchText";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@searchText", "%" + searchText + "%");

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Привязываем DataTable к DataGridView
                    dataGridView1.DataSource = dataTable;

                    // Скрываем столбец с ID_сотрудника
                    dataGridView1.Columns["ID_сотрудника"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

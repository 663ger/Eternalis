using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using MaterialSkin.Controls;

namespace Eternalis
{
    public partial class adminClients : Form
    {
        private string currentName;
        private string currentSurname;
        private string currentPatronymic;
        private int currentEmployeeID;
        string connectionString = "Data Source=DESKTOP-HURB5A2\\SQLEXPRESS;Initial Catalog=Eternalis;Integrated Security=True";
        public adminClients(int employeeID, string name, string surname, string patronymic)
        {
            InitializeComponent();
            // Сохраняем переданные данные
            currentEmployeeID = employeeID;
            currentName = name;
            currentSurname = surname;
            currentPatronymic = patronymic;
        }

        private void adminClients_Load(object sender, EventArgs e)
        {
            materialLabel1.Text = $"Работает: {currentName} {currentSurname} {currentPatronymic}";
            FillDataGridView();
        }
        private void FillDataGridView()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT TOP (1000) [ID_клиента]
                              ,[Фамилия]
                              ,[Имя]
                              ,[Отчество]
                              ,[Дата_рождения]
                              ,[Адрес]
                              ,[Телефон]
                              ,[Email]
                          FROM [Eternalis].[dbo].[Клиенты]";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Привязываем DataTable к dataGridView1
                    dataGridView1.DataSource = dataTable;

                    // Скрываем столбец ID_клиента
                    dataGridView1.Columns["ID_клиента"].Visible = false;
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

        private void materialButton2_Click(object sender, EventArgs e)
        {
            // Проверяем, выбрана ли строка
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить клиента?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // Получаем ID_клиента выбранной строки
                        int clientID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_клиента"].Value);

                        // Проверяем, участвует ли клиент в заказе
                        if (IsClientInOrder(clientID))
                        {
                            MessageBox.Show("Нельзя удалить клиента, участвующего в заказе.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }

                        // Удаляем клиента из базы данных
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            string deleteQuery = "DELETE FROM [Eternalis].[dbo].[Клиенты] WHERE [ID_клиента] = @ClientID";
                            SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                            deleteCommand.Parameters.AddWithValue("@ClientID", clientID);
                            deleteCommand.ExecuteNonQuery();

                            // Обновляем dataGridView1 после удаления
                            FillDataGridView();

                            MessageBox.Show("Клиент успешно удален.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Выберите клиента для удаления.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private bool IsClientInOrder(int clientID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM [Eternalis].[dbo].[Заказы] WHERE [ID_клиента] = @ClientID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ClientID", clientID);

                int count = (int)command.ExecuteScalar();

                return count > 0;
            }
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            // Проверяем, выбрана ли строка
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Получаем данные выбранной строки
                int clientID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_клиента"].Value);
                string surname = dataGridView1.SelectedRows[0].Cells["Фамилия"].Value.ToString();
                string name = dataGridView1.SelectedRows[0].Cells["Имя"].Value.ToString();
                string patronymic = dataGridView1.SelectedRows[0].Cells["Отчество"].Value.ToString();
                DateTime birthDate = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells["Дата_рождения"].Value);
                string address = dataGridView1.SelectedRows[0].Cells["Адрес"].Value.ToString();
                string phone = dataGridView1.SelectedRows[0].Cells["Телефон"].Value.ToString();
                string email = dataGridView1.SelectedRows[0].Cells["Email"].Value.ToString();

                // Создаем форму редактирования и передаем данные
                adminEditClients editClients = new adminEditClients(clientID, surname, name, patronymic, birthDate, address, phone, email);

                // Обработчик события FormClosed для обновления данных после закрытия формы редактирования
                editClients.FormClosed += (s, args) => { FillDataGridView(); };

                editClients.Show();
            }
            else
            {
                MessageBox.Show("Выберите клиента для редактирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void materialTextBox1_TextChanged(object sender, EventArgs e)
        {
            SearchClients(materialTextBox1.Text.ToLower());
        }
        private void SearchClients(string searchText)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                SELECT [ID_клиента]
                      ,[Фамилия]
                      ,[Имя]
                      ,[Отчество]
                      ,[Дата_рождения]
                      ,[Адрес]
                      ,[Телефон]
                      ,[Email]
                FROM [Eternalis].[dbo].[Клиенты]
                WHERE LOWER([Фамилия]) LIKE @searchText OR
                      LOWER([Имя]) LIKE @searchText OR
                      LOWER([Отчество]) LIKE @searchText OR
                      LOWER([Фамилия] + ' ' + [Имя]) LIKE @searchText OR
                      LOWER([Фамилия] + ' ' + [Отчество]) LIKE @searchText OR
                      LOWER([Имя] + ' ' + [Отчество]) LIKE @searchText OR
                      LOWER([Фамилия] + ' ' + [Имя] + ' ' + [Отчество]) LIKE @searchText OR
                      LOWER([Фамилия] + ' ' + [Отчество] + ' ' + [Имя]) LIKE @searchText OR
                      LOWER([Имя] + ' ' + [Фамилия] + ' ' + [Отчество]) LIKE @searchText OR
                      LOWER([Имя] + ' ' + [Отчество] + ' ' + [Фамилия]) LIKE @searchText";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@searchText", "%" + searchText + "%");

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Привязываем DataTable к dataGridView1
                    dataGridView1.DataSource = dataTable;

                    // Скрываем столбец ID_клиента
                    dataGridView1.Columns["ID_клиента"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    }


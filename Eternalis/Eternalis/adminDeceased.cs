using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Wordprocessing;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace Eternalis
{
    public partial class adminDeceased : Form
    {
        private string currentName;
        private string currentSurname;
        private string currentPatronymic;
        private int currentEmployeeID;
        string connectionString = "Data Source=DESKTOP-HURB5A2\\SQLEXPRESS;Initial Catalog=Eternalis;Integrated Security=True";
        public adminDeceased(int employeeID, string name, string surname, string patronymic)
        {
            InitializeComponent();
            // Сохраняем переданные данные
            currentEmployeeID = employeeID;
            currentName = name;
            currentSurname = surname;
            currentPatronymic = patronymic;
        }

        private void adminDeceased_Load(object sender, EventArgs e)
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
                        SELECT TOP(1000)
                            [Eternalis].[dbo].[Покойные].[ID_покойного],
                            [Eternalis].[dbo].[Покойные].[Имя] AS [Имя_покойного],
                            [Eternalis].[dbo].[Покойные].[Фамилия] AS [Фамилия_покойного],
                            [Eternalis].[dbo].[Покойные].[Отчество] AS [Отчество_покойного],
                            [Eternalis].[dbo].[Покойные].[Дата_рождения],
                            [Eternalis].[dbo].[Покойные].[Дата_смерти],
                            [Eternalis].[dbo].[Покойные].[ID_клиента],
                            [Eternalis].[dbo].[Клиенты].[Фамилия] + ' ' + [Eternalis].[dbo].[Клиенты].[Имя] + ' ' + [Eternalis].[dbo].[Клиенты].[Отчество] AS [ФИО_клиента]
                        FROM
                            [Eternalis].[dbo].[Покойные]
                        LEFT JOIN
                            [Eternalis].[dbo].[Клиенты] ON [Eternalis].[dbo].[Покойные].[ID_клиента] = [Eternalis].[dbo].[Клиенты].[ID_клиента]";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Привязываем DataTable к dataGridView1
                    dataGridView1.DataSource = dataTable;

                    // Скрываем столбец ID_клиента и ФИО_клиента
                    dataGridView1.Columns["ID_покойного"].Visible = false;
                    dataGridView1.Columns["ID_клиента"].Visible = false;

                    // Устанавливаем названия столбцов
                    dataGridView1.Columns["Имя_покойного"].HeaderText = "Имя покойного";
                    dataGridView1.Columns["Фамилия_покойного"].HeaderText = "Фамилия покойного";
                    dataGridView1.Columns["Отчество_покойного"].HeaderText = "Отчество покойного";
                    dataGridView1.Columns["Дата_рождения"].HeaderText = "Дата рождения";
                    dataGridView1.Columns["Дата_смерти"].HeaderText = "Дата смерти";
                    dataGridView1.Columns["ФИО_клиента"].HeaderText = "ФИО клиента";
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

        private void materialButton2_Click(object sender, EventArgs e)
        {
            // Проверяем, выбрана ли строка
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить покойного?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // Получаем ID_покойного выбранной строки
                        int deceasedID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_покойного"].Value);

                        // Проверяем, есть ли покойный в заказах
                        if (IsDeceasedInOrders(deceasedID))
                        {
                            MessageBox.Show("Нельзя удалить покойного, так как он участвует в заказе.", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // Удаляем покойного из базы данных
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            string deleteQuery = "DELETE FROM [Eternalis].[dbo].[Покойные] WHERE [ID_покойного] = @DeceasedID";
                            SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection);
                            deleteCommand.Parameters.AddWithValue("@DeceasedID", deceasedID);
                            deleteCommand.ExecuteNonQuery();

                            // Обновляем dataGridView1 после удаления
                            FillDataGridView();

                            MessageBox.Show("Покойный успешно удален.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                MessageBox.Show("Выберите покойного для удаления.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool IsDeceasedInOrders(int deceasedID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT TOP (1) [ID_заказа] FROM [Eternalis].[dbo].[Заказы] WHERE [ID_покойного] = @DeceasedID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@DeceasedID", deceasedID);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    return reader.HasRows;
                }
            }
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            // Проверяем, выбрана ли строка
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Получаем данные выбранной строки
                int deceasedID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_покойного"].Value);
                string surname = dataGridView1.SelectedRows[0].Cells["Фамилия_покойного"].Value.ToString();
                string name = dataGridView1.SelectedRows[0].Cells["Имя_покойного"].Value.ToString();
                string patronymic = dataGridView1.SelectedRows[0].Cells["Отчество_покойного"].Value.ToString();
                DateTime birthDate = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells["Дата_рождения"].Value);
                DateTime deathDate = Convert.ToDateTime(dataGridView1.SelectedRows[0].Cells["Дата_смерти"].Value);

                // Проверяем, является ли значение DBNull перед преобразованием
                int? clientID = DBNull.Value.Equals(dataGridView1.SelectedRows[0].Cells["ID_клиента"].Value)
                    ? (int?)null
                    : Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_клиента"].Value);

                // Создаем форму редактирования покойного и передаем данные
                adminEditDeceased editDeceased = new adminEditDeceased(deceasedID, surname, name, patronymic, birthDate, deathDate, clientID ?? default(int));

                // После сохранения данных обновляем dataGridView1
                editDeceased.FormClosed += (s, args) => FillDataGridView();

                editDeceased.ShowDialog(); // Используйте ShowDialog вместо Show
            }
            else
            {
                MessageBox.Show("Выберите покойного для редактирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
            Admin newAdmin = new Admin(currentEmployeeID, currentName, currentSurname, currentPatronymic);
            newAdmin.Show();
            this.Hide();
        }

        private void materialFloatingActionButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void materialTextBox1_TextChanged(object sender, EventArgs e)
        {
            SearchDeceased(materialTextBox1.Text.ToLower());
        }
        private void SearchDeceased(string searchText)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
    SELECT TOP(1000)
        [Eternalis].[dbo].[Покойные].[ID_покойного],
        [Eternalis].[dbo].[Покойные].[Имя] AS [Имя_покойного],
        [Eternalis].[dbo].[Покойные].[Фамилия] AS [Фамилия_покойного],
        [Eternalis].[dbo].[Покойные].[Отчество] AS [Отчество_покойного],
        [Eternalis].[dbo].[Покойные].[Дата_рождения],
        [Eternalis].[dbo].[Покойные].[Дата_смерти],
        [Eternalis].[dbo].[Покойные].[ID_клиента],
        [Eternalis].[dbo].[Клиенты].[Фамилия] + ' ' + [Eternalis].[dbo].[Клиенты].[Имя] + ' ' + [Eternalis].[dbo].[Клиенты].[Отчество] AS [ФИО_клиента]
    FROM
        [Eternalis].[dbo].[Покойные]
    LEFT JOIN
        [Eternalis].[dbo].[Клиенты] ON [Eternalis].[dbo].[Покойные].[ID_клиента] = [Eternalis].[dbo].[Клиенты].[ID_клиента]
    WHERE LOWER([Eternalis].[dbo].[Покойные].[Имя]) LIKE @searchText OR
          LOWER([Eternalis].[dbo].[Покойные].[Фамилия]) LIKE @searchText OR
          LOWER([Eternalis].[dbo].[Покойные].[Отчество]) LIKE @searchText OR
          LOWER([Eternalis].[dbo].[Покойные].[Фамилия] + ' ' + [Eternalis].[dbo].[Покойные].[Имя]) LIKE @searchText OR
          LOWER([Eternalis].[dbo].[Покойные].[Фамилия] + ' ' + [Eternalis].[dbo].[Покойные].[Отчество]) LIKE @searchText OR
          LOWER([Eternalis].[dbo].[Покойные].[Имя] + ' ' + [Eternalis].[dbo].[Покойные].[Отчество]) LIKE @searchText OR
          LOWER([Eternalis].[dbo].[Покойные].[Фамилия] + ' ' + [Eternalis].[dbo].[Покойные].[Имя] + ' ' + [Eternalis].[dbo].[Покойные].[Отчество]) LIKE @searchText OR
          LOWER([Eternalis].[dbo].[Покойные].[Фамилия] + ' ' + [Eternalis].[dbo].[Покойные].[Отчество] + ' ' + [Eternalis].[dbo].[Покойные].[Имя]) LIKE @searchText OR
          LOWER([Eternalis].[dbo].[Покойные].[Имя] + ' ' + [Eternalis].[dbo].[Покойные].[Фамилия] + ' ' + [Eternalis].[dbo].[Покойные].[Отчество]) LIKE @searchText OR
          LOWER([Eternalis].[dbo].[Покойные].[Имя] + ' ' + [Eternalis].[dbo].[Покойные].[Отчество] + ' ' + [Eternalis].[dbo].[Покойные].[Фамилия]) LIKE @searchText";


                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@searchText", "%" + searchText + "%");

                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Привязываем DataTable к dataGridView1
                    dataGridView1.DataSource = dataTable;

                    // Скрываем столбец ID_покойного и ID_клиента
                    dataGridView1.Columns["ID_покойного"].Visible = false;
                    dataGridView1.Columns["ID_клиента"].Visible = false;

                    // Устанавливаем названия столбцов
                    dataGridView1.Columns["Имя_покойного"].HeaderText = "Имя покойного";
                    dataGridView1.Columns["Фамилия_покойного"].HeaderText = "Фамилия покойного";
                    dataGridView1.Columns["Отчество_покойного"].HeaderText = "Отчество покойного";
                    dataGridView1.Columns["Дата_рождения"].HeaderText = "Дата рождения";
                    dataGridView1.Columns["Дата_смерти"].HeaderText = "Дата смерти";
                    dataGridView1.Columns["ФИО_клиента"].HeaderText = "ФИО клиента";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

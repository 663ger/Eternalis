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
    public partial class adminServices : Form
    {
        private string currentName;
        private string currentSurname;
        private string currentPatronymic;
        private int currentEmployeeID;
        string connectionString = "Data Source=DESKTOP-HURB5A2\\SQLEXPRESS;Initial Catalog=Eternalis;Integrated Security=True";
        public adminServices(int employeeID, string name, string surname, string patronymic)
        {
            InitializeComponent();
            // Сохраняем переданные данные
            currentEmployeeID = employeeID;
            currentName = name;
            currentSurname = surname;
            currentPatronymic = patronymic;
        }

        private void adminServices_Load(object sender, EventArgs e)
        {
            materialLabel1.Text = $"Работает: {currentName} {currentSurname} {currentPatronymic}";
            LoadDataIntoDataGridView();
        }
        // Метод для загрузки данных в DataGridView
        private void LoadDataIntoDataGridView()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT TOP (1000) [ID_услуги], [Название], [Стоимость] FROM [Eternalis].[dbo].[Услуги]";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Привязываем DataTable к DataGridView
                    dataGridView1.DataSource = dataTable;

                    // Устанавливаем названия столбцов
                    dataGridView1.Columns["ID_услуги"].Visible = false;
                    dataGridView1.Columns["Название"].HeaderText = "Название услуги";
                    dataGridView1.Columns["Стоимость"].HeaderText = "Стоимость";
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

        private void materialButton3_Click(object sender, EventArgs e)
        {
            // Создаем диалоговое окно для добавления услуги
            Form inputBox = new Form();
            inputBox.Text = "Добавление новой услуги";
            inputBox.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputBox.Width = 350;
            inputBox.Height = 200;
            inputBox.StartPosition = FormStartPosition.CenterScreen;

            Label labelName = new Label();
            labelName.Text = "Наименование:";
            labelName.Left = 10;
            labelName.Top = 10;

            TextBox textBoxName = new TextBox();
            textBoxName.Left = 110;
            textBoxName.Top = 10;
            textBoxName.Width = 150;

            Label labelCost = new Label();
            labelCost.Text = "Стоимость:";
            labelCost.Left = 10;
            labelCost.Top = 40;

            TextBox textBoxCost = new TextBox();
            textBoxCost.Left = 110;
            textBoxCost.Top = 40;
            textBoxCost.Width = 150;

            Button buttonOK = new Button();
            buttonOK.Text = "OK";
            buttonOK.DialogResult = DialogResult.OK;
            buttonOK.Left = 100;
            buttonOK.Top = 80;

            Button buttonCancel = new Button();
            buttonCancel.Text = "Отмена";
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Left = 180;
            buttonCancel.Top = 80;

            inputBox.Controls.Add(labelName);
            inputBox.Controls.Add(textBoxName);
            inputBox.Controls.Add(labelCost);
            inputBox.Controls.Add(textBoxCost);
            inputBox.Controls.Add(buttonOK);
            inputBox.Controls.Add(buttonCancel);

            inputBox.AcceptButton = buttonOK;
            inputBox.CancelButton = buttonCancel;

            DialogResult result = inputBox.ShowDialog();

            if (result == DialogResult.OK)
            {
                // Получаем значения из текстовых полей
                string newName = textBoxName.Text.Trim();

                // Проверяем корректность ввода стоимости
                if (!decimal.TryParse(textBoxCost.Text.Trim(), out decimal newCost))
                {
                    MessageBox.Show("Некорректная стоимость. Пожалуйста, введите числовое значение.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Создаем подключение к базе данных
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        // Открываем подключение
                        connection.Open();

                        // Создаем команду SQL для добавления новой услуги
                        string sqlQuery = "INSERT INTO [Eternalis].[dbo].[Услуги] ([Название], [Стоимость]) VALUES (@Name, @Cost)";
                        SqlCommand command = new SqlCommand(sqlQuery, connection);
                        command.Parameters.AddWithValue("@Name", newName);
                        command.Parameters.AddWithValue("@Cost", newCost);

                        // Выполняем команду SQL
                        command.ExecuteNonQuery();

                        // Обновляем данные в DataGridView
                        LoadDataIntoDataGridView();

                        MessageBox.Show("Услуга успешно добавлена.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            // Проверяем, выбрана ли строка
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Получаем данные выбранной услуги
                int serviceID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_услуги"].Value);
                string serviceName = dataGridView1.SelectedRows[0].Cells["Название"].Value.ToString();
                decimal serviceCost = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells["Стоимость"].Value);

                // Создаем диалоговое окно для редактирования услуги
                Form inputBox = new Form();
                inputBox.Text = "Редактирование услуги";
                inputBox.FormBorderStyle = FormBorderStyle.FixedDialog;
                inputBox.Width = 350;
                inputBox.Height = 200;
                inputBox.StartPosition = FormStartPosition.CenterScreen;

                Label labelName = new Label();
                labelName.Text = "Наименование:";
                labelName.Left = 10;
                labelName.Top = 10;

                TextBox textBoxName = new TextBox();
                textBoxName.Text = serviceName;
                textBoxName.Left = 110;
                textBoxName.Top = 10;
                textBoxName.Width = 150;

                Label labelCost = new Label();
                labelCost.Text = "Стоимость:";
                labelCost.Left = 10;
                labelCost.Top = 40;

                TextBox textBoxCost = new TextBox();
                textBoxCost.Text = serviceCost.ToString();
                textBoxCost.Left = 110;
                textBoxCost.Top = 40;
                textBoxCost.Width = 150;

                Button buttonOK = new Button();
                buttonOK.Text = "OK";
                buttonOK.DialogResult = DialogResult.OK;
                buttonOK.Left = 100;
                buttonOK.Top = 80;

                Button buttonCancel = new Button();
                buttonCancel.Text = "Отмена";
                buttonCancel.DialogResult = DialogResult.Cancel;
                buttonCancel.Left = 180;
                buttonCancel.Top = 80;

                inputBox.Controls.Add(labelName);
                inputBox.Controls.Add(textBoxName);
                inputBox.Controls.Add(labelCost);
                inputBox.Controls.Add(textBoxCost);
                inputBox.Controls.Add(buttonOK);
                inputBox.Controls.Add(buttonCancel);

                inputBox.AcceptButton = buttonOK;
                inputBox.CancelButton = buttonCancel;

                DialogResult result = inputBox.ShowDialog();

                if (result == DialogResult.OK)
                {
                    // Получаем новые значения из текстовых полей
                    string newName = textBoxName.Text.Trim();

                    // Проверяем корректность ввода стоимости
                    if (!decimal.TryParse(textBoxCost.Text.Trim(), out decimal newCost))
                    {
                        MessageBox.Show("Некорректная стоимость. Пожалуйста, введите числовое значение.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    // Создаем подключение к базе данных
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            // Открываем подключение
                            connection.Open();

                            // Создаем команду SQL для обновления данных услуги
                            string sqlQuery = "UPDATE [Eternalis].[dbo].[Услуги] SET [Название] = @Name, [Стоимость] = @Cost WHERE [ID_услуги] = @ServiceID";
                            SqlCommand command = new SqlCommand(sqlQuery, connection);
                            command.Parameters.AddWithValue("@Name", newName);
                            command.Parameters.AddWithValue("@Cost", newCost);
                            command.Parameters.AddWithValue("@ServiceID", serviceID);

                            // Выполняем команду SQL
                            command.ExecuteNonQuery();

                            // Обновляем данные в DataGridView
                            LoadDataIntoDataGridView();

                            MessageBox.Show("Данные услуги успешно обновлены.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите услугу для редактирования.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            // Проверяем, выбрана ли строка
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Получаем ID выбранной услуги
                int serviceID = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_услуги"].Value);

                // Подтверждение удаления
                DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить эту услугу?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Создаем подключение к базе данных
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            // Открываем подключение
                            connection.Open();

                            // Создаем команду SQL для удаления услуги
                            string sqlQuery = "DELETE FROM [Eternalis].[dbo].[Услуги] WHERE [ID_услуги] = @ServiceID";
                            SqlCommand command = new SqlCommand(sqlQuery, connection);
                            command.Parameters.AddWithValue("@ServiceID", serviceID);

                            // Выполняем команду SQL
                            command.ExecuteNonQuery();

                            // Обновляем данные в DataGridView
                            LoadDataIntoDataGridView();

                            MessageBox.Show("Услуга успешно удалена.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка: " + ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите услугу для удаления.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

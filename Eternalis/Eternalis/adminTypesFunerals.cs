using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Eternalis
{
    public partial class adminTypesFunerals : Form
    {
        private string currentName;
        private string currentSurname;
        private string currentPatronymic;
        private int currentEmployeeID;
        string connectionString = "Data Source=DESKTOP-HURB5A2\\SQLEXPRESS;Initial Catalog=Eternalis;Integrated Security=True";

        public adminTypesFunerals(int employeeID, string name, string surname, string patronymic)
        {
            InitializeComponent();
            // Сохраняем переданные данные
            currentEmployeeID = employeeID;
            currentName = name;
            currentSurname = surname;
            currentPatronymic = patronymic;
        }

        private void adminTypesFunerals_Load(object sender, EventArgs e)
        {
            materialLabel1.Text = $"Работает: {currentName} {currentSurname} {currentPatronymic}";

            // Загрузка данных в DataGridView при загрузке формы
            LoadDataIntoDataGridView();
        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
            Admin newAdmin = new Admin(currentEmployeeID, currentName, currentSurname, currentPatronymic);
            newAdmin.Show();
            this.Hide();
        }

        private void LoadDataIntoDataGridView()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"
                        SELECT TOP (1000) [ID_вида_похорон]
                            ,[Наименование]
                            ,[Описание]
                            ,[Адрес]
                            ,[Стоимость]
                        FROM [Eternalis].[dbo].[Виды_похорон]";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Привязываем DataTable к DataGridView
                    dataGridView1.DataSource = dataTable;

                    // Опционально скрываем столбец ID_вида_похорон, если не нужен пользователю
                    dataGridView1.Columns["ID_вида_похорон"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void materialButton3_Click(object sender, EventArgs e)
        {
            // Создаем диалоговое окно для добавления нового вида похорон
            Form inputBox = new Form();
            inputBox.Text = "Добавление вида похорон";
            inputBox.FormBorderStyle = FormBorderStyle.FixedDialog;
            inputBox.Width = 350;
            inputBox.Height = 300;
            inputBox.StartPosition = FormStartPosition.CenterScreen;

            Label labelName = new Label();
            labelName.Text = "Наименование:";
            labelName.Left = 10;
            labelName.Top = 10;

            TextBox textBoxName = new TextBox();
            textBoxName.Left = 110;
            textBoxName.Top = 10;
            textBoxName.Width = 150;

            Label labelDescription = new Label();
            labelDescription.Text = "Описание:";
            labelDescription.Left = 10;
            labelDescription.Top = 40;

            TextBox textBoxDescription = new TextBox();
            textBoxDescription.Left = 110;
            textBoxDescription.Top = 40;
            textBoxDescription.Width = 150;

            Label labelAddress = new Label();
            labelAddress.Text = "Адрес:";
            labelAddress.Left = 10;
            labelAddress.Top = 70;

            TextBox textBoxAddress = new TextBox();
            textBoxAddress.Left = 110;
            textBoxAddress.Top = 70;
            textBoxAddress.Width = 150;

            Label labelCost = new Label();
            labelCost.Text = "Стоимость:";
            labelCost.Left = 10;
            labelCost.Top = 100;

            TextBox textBoxCost = new TextBox();
            textBoxCost.Left = 110;
            textBoxCost.Top = 100;
            textBoxCost.Width = 150;

            Button buttonOK = new Button();
            buttonOK.Text = "OK";
            buttonOK.DialogResult = DialogResult.OK;
            buttonOK.Left = 100;
            buttonOK.Top = 160;

            Button buttonCancel = new Button();
            buttonCancel.Text = "Отмена";
            buttonCancel.DialogResult = DialogResult.Cancel;
            buttonCancel.Left = 180;
            buttonCancel.Top = 160;

            inputBox.Controls.Add(labelName);
            inputBox.Controls.Add(textBoxName);
            inputBox.Controls.Add(labelDescription);
            inputBox.Controls.Add(textBoxDescription);
            inputBox.Controls.Add(labelAddress);
            inputBox.Controls.Add(textBoxAddress);
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
                string newName = textBoxName.Text;
                string newDescription = textBoxDescription.Text;
                string newAddress = textBoxAddress.Text;

                decimal newCost;
                if (!decimal.TryParse(textBoxCost.Text, out newCost))
                {
                    MessageBox.Show("Введите корректное значение для стоимости.");
                    return;
                }

                // Другие проверки по необходимости

                // Создаем подключение к базе данных
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        // Открываем подключение
                        connection.Open();

                        // Создаем команду SQL для вставки нового вида похорон
                        string sqlQuery = "INSERT INTO [Eternalis].[dbo].[Виды_похорон] ([Наименование], [Описание], [Адрес], [Стоимость]) VALUES (@Name, @Description, @Address, @Cost)";
                        SqlCommand command = new SqlCommand(sqlQuery, connection);
                        command.Parameters.AddWithValue("@Name", newName);
                        command.Parameters.AddWithValue("@Description", newDescription);
                        command.Parameters.AddWithValue("@Address", newAddress);
                        command.Parameters.AddWithValue("@Cost", newCost);

                        // Выполняем команду SQL
                        command.ExecuteNonQuery();

                        // Обновляем данные в DataGridView
                        LoadDataIntoDataGridView();

                        MessageBox.Show("Новый вид похорон успешно добавлен.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ошибка: " + ex.Message);
                    }
                }
            }
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            // Проверяем, выбрана ли строка
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Получаем данные выбранной строки
                int typeId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_вида_похорон"].Value);
                string currentName = dataGridView1.SelectedRows[0].Cells["Наименование"].Value.ToString();
                string currentDescription = dataGridView1.SelectedRows[0].Cells["Описание"].Value.ToString();
                string currentAddress = dataGridView1.SelectedRows[0].Cells["Адрес"].Value.ToString();
                decimal currentCost = Convert.ToDecimal(dataGridView1.SelectedRows[0].Cells["Стоимость"].Value);

                // Создаем диалоговое окно для редактирования вида похорон
                Form inputBox = new Form();
                inputBox.Text = "Редактирование вида похорон";
                inputBox.FormBorderStyle = FormBorderStyle.FixedDialog;
                inputBox.Width = 350;
                inputBox.Height = 300;
                inputBox.StartPosition = FormStartPosition.CenterScreen;

                Label labelName = new Label();
                labelName.Text = "Наименование:";
                labelName.Left = 10;
                labelName.Top = 10;

                TextBox textBoxName = new TextBox();
                textBoxName.Text = currentName;
                textBoxName.Left = 110;
                textBoxName.Top = 10;
                textBoxName.Width = 150;

                Label labelDescription = new Label();
                labelDescription.Text = "Описание:";
                labelDescription.Left = 10;
                labelDescription.Top = 40;

                TextBox textBoxDescription = new TextBox();
                textBoxDescription.Text = currentDescription;
                textBoxDescription.Left = 110;
                textBoxDescription.Top = 40;
                textBoxDescription.Width = 150;

                Label labelAddress = new Label();
                labelAddress.Text = "Адрес:";
                labelAddress.Left = 10;
                labelAddress.Top = 70;

                TextBox textBoxAddress = new TextBox();
                textBoxAddress.Text = currentAddress;
                textBoxAddress.Left = 110;
                textBoxAddress.Top = 70;
                textBoxAddress.Width = 150;

                Label labelCost = new Label();
                labelCost.Text = "Стоимость:";
                labelCost.Left = 10;
                labelCost.Top = 100;

                TextBox textBoxCost = new TextBox();
                textBoxCost.Text = currentCost.ToString();
                textBoxCost.Left = 110;
                textBoxCost.Top = 100;
                textBoxCost.Width = 150;

                Button buttonOK = new Button();
                buttonOK.Text = "OK";
                buttonOK.DialogResult = DialogResult.OK;
                buttonOK.Left = 100;
                buttonOK.Top = 160;

                Button buttonCancel = new Button();
                buttonCancel.Text = "Отмена";
                buttonCancel.DialogResult = DialogResult.Cancel;
                buttonCancel.Left = 180;
                buttonCancel.Top = 160;

                inputBox.Controls.Add(labelName);
                inputBox.Controls.Add(textBoxName);
                inputBox.Controls.Add(labelDescription);
                inputBox.Controls.Add(textBoxDescription);
                inputBox.Controls.Add(labelAddress);
                inputBox.Controls.Add(textBoxAddress);
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
                    string newName = textBoxName.Text;
                    string newDescription = textBoxDescription.Text;
                    string newAddress = textBoxAddress.Text;

                    decimal newCost;
                    if (!decimal.TryParse(textBoxCost.Text, out newCost))
                    {
                        MessageBox.Show("Введите корректное значение для стоимости.");
                        return;
                    }

                    // Другие проверки по необходимости

                    // Создаем подключение к базе данных
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            // Открываем подключение
                            connection.Open();

                            // Создаем команду SQL для обновления данных вида похорон
                            string sqlQuery = "UPDATE [Eternalis].[dbo].[Виды_похорон] SET [Наименование] = @Name, [Описание] = @Description, [Адрес] = @Address, [Стоимость] = @Cost WHERE [ID_вида_похорон] = @TypeID";
                            SqlCommand command = new SqlCommand(sqlQuery, connection);
                            command.Parameters.AddWithValue("@Name", newName);
                            command.Parameters.AddWithValue("@Description", newDescription);
                            command.Parameters.AddWithValue("@Address", newAddress);
                            command.Parameters.AddWithValue("@Cost", newCost);
                            command.Parameters.AddWithValue("@TypeID", typeId);

                            // Выполняем команду SQL
                            command.ExecuteNonQuery();

                            // Обновляем данные в DataGridView
                            LoadDataIntoDataGridView();

                            MessageBox.Show("Данные вида похорон успешно обновлены.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите вид похорон для редактирования.");
            }
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            // Проверяем, выбрана ли строка
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Получаем ID выбранной строки
                int typeId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID_вида_похорон"].Value);

                // Запрашиваем подтверждение на удаление
                DialogResult result = MessageBox.Show("Вы уверены, что хотите удалить выбранный вид похорон?", "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Создаем подключение к базе данных
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        try
                        {
                            // Открываем подключение
                            connection.Open();

                            // Создаем команду SQL для удаления записи
                            string sqlQuery = "DELETE FROM [Eternalis].[dbo].[Виды_похорон] WHERE [ID_вида_похорон] = @TypeID";
                            SqlCommand command = new SqlCommand(sqlQuery, connection);
                            command.Parameters.AddWithValue("@TypeID", typeId);

                            // Выполняем команду SQL
                            command.ExecuteNonQuery();

                            // Обновляем данные в DataGridView
                            LoadDataIntoDataGridView();

                            MessageBox.Show("Вид похорон успешно удален.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Ошибка: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите вид похорон для удаления.");
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

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

namespace Eternalis
{
    public partial class newDeceased : Form
    {
        private string currentName;
        private string currentSurname;
        private string currentPatronymic;
        private int currentEmployeeID;
        string connectionString = "Data Source=DESKTOP-HURB5A2\\SQLEXPRESS;Initial Catalog=Eternalis;Integrated Security=True";
        public newDeceased(int employeeID, string name, string surname, string patronymic)
        {
            InitializeComponent();
            // Сохраняем переданные данные
            currentEmployeeID = employeeID;
            currentName = name;
            currentSurname = surname;
            currentPatronymic = patronymic;
            LoadClients();
            SetupAutoComplete();
        }

        private void newDeceased_Load(object sender, EventArgs e)
        {
            materialLabel1.Text = $"Работает: {currentName} {currentSurname} {currentPatronymic}";
        }

        private void materialFloatingActionButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void materialButton5_Click(object sender, EventArgs e)
        {
            // Соберите данные из materialTextBox
            string firstName = materialTextBox1.Text.Trim();
            string lastName = materialTextBox2.Text.Trim();
            string patronymic = materialTextBox3.Text.Trim();
            string birthDate = guna2DateTimePicker1.Value.ToString("yyyy-MM-dd"); // Используйте формат даты, который соответствует вашей базе данных
            string deathDate = guna2DateTimePicker2.Value.ToString("yyyy-MM-dd"); // Используйте формат даты, который соответствует вашей базе данных
            string selectedFullName = materialComboBox1.SelectedItem?.ToString();

            // Проверки на заполняемость полей
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(patronymic) ||
                string.IsNullOrEmpty(birthDate) || string.IsNullOrEmpty(deathDate) || string.IsNullOrEmpty(selectedFullName))
            {
                MessageBox.Show("Все поля должны быть заполнены.");
                return;
            }

            // Получите ID клиента по его ФИО
            int clientID = GetClientIDByFullName(selectedFullName);

            if (clientID == -1)
            {
                MessageBox.Show("Ошибка при получении ID клиента.");
                return;
            }

            // Создайте SQL-запрос для вставки нового покойного
            string insertQuery = "INSERT INTO Покойные (Фамилия, Имя, Отчество, Дата_рождения, Дата_смерти, ID_клиента) " +
                                "VALUES (@lastName, @firstName, @patronymic, @birthDate, @deathDate, @clientID)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@lastName", lastName);
                    command.Parameters.AddWithValue("@firstName", firstName);
                    command.Parameters.AddWithValue("@patronymic", patronymic);
                    command.Parameters.AddWithValue("@birthDate", birthDate);
                    command.Parameters.AddWithValue("@deathDate", deathDate);
                    command.Parameters.AddWithValue("@clientID", clientID);

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        // Успешно добавлено
                        MessageBox.Show("Покойный успешно добавлен.");

                        // Очистка полей после успешного добавления
                        materialTextBox1.Clear();
                        materialTextBox2.Clear();
                        materialTextBox3.Clear();
                        guna2DateTimePicker1.Value = DateTime.Now;
                        guna2DateTimePicker2.Value = DateTime.Now;
                        materialComboBox1.SelectedIndex = -1;
                    }
                    else
                    {
                        // Что-то пошло не так
                        MessageBox.Show("Ошибка при добавлении клиента.");
                    }
                }
            }
        }

        private int GetClientIDByFullName(string fullName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT [ID_клиента] FROM Клиенты WHERE CONCAT([Имя], ' ', [Фамилия], ' ', [Отчество]) = @FullName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FullName", fullName);
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        return Convert.ToInt32(result);
                    }
                }
            }

            return -1;
        }


        private void materialButton2_Click(object sender, EventArgs e)
        {
            Deceased deceasedForm = new Deceased(currentEmployeeID, currentName, currentSurname, currentPatronymic);
            deceasedForm.Show();
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
        private void SetupAutoComplete()
        {
            // Измените DropDownStyle перед настройкой автозаполнения
            materialComboBox1.DropDownStyle = ComboBoxStyle.DropDown;

            materialComboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            materialComboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            AutoCompleteStringCollection autoCompleteCollection = new AutoCompleteStringCollection();

            // Заполняем коллекцию для автозаполнения
            foreach (object item in materialComboBox1.Items)
            {
                autoCompleteCollection.Add(item.ToString());
            }

            materialComboBox1.AutoCompleteCustomSource = autoCompleteCollection;
        }
        private void LoadClients()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT CONCAT(Имя, ' ', Фамилия, ' ', Отчество) AS ФИО FROM Клиенты";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            materialComboBox1.Items.Add(reader["ФИО"]);
                        }
                    }
                }
            }
        }

        private void materialComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFullName = materialComboBox1.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(selectedFullName))
            {

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT [ID_клиента], [Фамилия], [Имя], [Отчество], [Дата_рождения], [Адрес], [Телефон], [Email]
                     FROM [Клиенты]
                     WHERE CONCAT([Имя], ' ', [Фамилия], ' ', [Отчество]) = @FullName";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@FullName", selectedFullName);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string clientID = reader["ID_клиента"].ToString();
                        string lastName = reader["Фамилия"].ToString();
                        string firstName = reader["Имя"].ToString();
                        string patronymic = reader["Отчество"].ToString();
                        string birthdate = reader["Дата_рождения"].ToString();
                        string address = reader["Адрес"].ToString();
                        string phone = reader["Телефон"].ToString();
                        string email = reader["Email"].ToString();
                        {

                        }
                    }

                    reader.Close();
                }
            }
        }
    }
}

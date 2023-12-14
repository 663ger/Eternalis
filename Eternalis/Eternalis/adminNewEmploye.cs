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
    public partial class adminNewEmploye : Form
    {
        private string currentName;
        private string currentSurname;
        private string currentPatronymic;
        private int currentEmployeeID;
        string connectionString = "Data Source=DESKTOP-HURB5A2\\SQLEXPRESS;Initial Catalog=Eternalis;Integrated Security=True";
        public adminNewEmploye(int employeeID, string name, string surname, string patronymic)
        {
            InitializeComponent();
            // Сохраняем переданные данные
            currentEmployeeID = employeeID;
            currentName = name;
            currentSurname = surname;
            currentPatronymic = patronymic;
        }

        private void adminNewEmploye_Load(object sender, EventArgs e)
        {
            materialLabel1.Text = $"Работает: {currentName} {currentSurname} {currentPatronymic}";
            FillPositionComboBox();
        }

        private void materialFloatingActionButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void FillPositionComboBox()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT [ID_должности], [Название] FROM [Eternalis].[dbo].[Должности]";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Привязываем DataTable к ComboBox
                    comboBox1.DataSource = dataTable;
                    comboBox1.DisplayMember = "Название";
                    comboBox1.ValueMember = "ID_должности";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void materialButton5_Click(object sender, EventArgs e)
        {
            // Обработка добавления нового сотрудника
            string firstName = materialTextBox1.Text.Trim();
            string lastName = materialTextBox2.Text.Trim();
            string patronymic = materialTextBox3.Text.Trim();
            DateTime birthDate = guna2DateTimePicker1.Value;
            string address = materialTextBox5.Text.Trim();
            string phone = materialTextBox6.Text.Trim();
            string email = materialTextBox7.Text.Trim();
            string login = materialTextBox21.Text.Trim();
            string password = materialTextBox22.Text;

            // Проверка на пустые поля
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(patronymic)
                || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phone) || string.IsNullOrEmpty(email)
                || string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Заполните все обязательные поля.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Проверка сложности пароля
            if (!IsStrongPassword(password))
            {
                MessageBox.Show("Пароль слишком простой. Используйте более сложный пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Проверка уникальности логина
            if (IsLoginExists(login))
            {
                MessageBox.Show("Логин уже существует. Выберите другой логин.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Проверка наличия выбранной должности в ComboBox
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите должность.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int positionID = (int)comboBox1.SelectedValue;

            // Создание подключения к базе данных
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Хеширование пароля
                    string hashedPassword = HashPassword(password);

                    // Создание команды SQL для добавления нового сотрудника
                    string sqlQuery = @"
                INSERT INTO [Eternalis].[dbo].[Сотрудники] 
                ([Имя], [Фамилия], [Отчество], [Дата_рождения], [Адрес], [Телефон], [Email], [Логин], [Пароль], [ID_должности]) 
                VALUES 
                (@FirstName, @LastName, @Patronymic, @BirthDate, @Address, @Phone, @Email, @Login, @Password, @PositionID)";

                    SqlCommand command = new SqlCommand(sqlQuery, connection);
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@Patronymic", patronymic);
                    command.Parameters.AddWithValue("@BirthDate", birthDate);
                    command.Parameters.AddWithValue("@Address", address);
                    command.Parameters.AddWithValue("@Phone", phone);
                    command.Parameters.AddWithValue("@Email", email);
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Password", hashedPassword); // Используем хешированный пароль
                    command.Parameters.AddWithValue("@PositionID", positionID);

                    // Выполнение команды SQL
                    command.ExecuteNonQuery();

                    MessageBox.Show("Новый сотрудник успешно добавлен.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ClearFields();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
        // Метод для проверки сложности пароля
        private bool IsStrongPassword(string password)
        {
            return password.Length >= 8 && password.Any(char.IsLower) && password.Any(char.IsUpper) && password.Any(char.IsDigit);
        }

        // Метод для проверки уникальности логина
        private bool IsLoginExists(string login)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT COUNT(*) FROM [Eternalis].[dbo].[Сотрудники] WHERE [Логин] = @Login";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Login", login);

                int count = (int)command.ExecuteScalar();

                return count > 0;
            }
        }
        private void ClearFields()
        {
            materialTextBox1.Text = "";
            materialTextBox2.Text = "";
            materialTextBox3.Text = "";
            guna2DateTimePicker1.Value = DateTime.Now;
            materialTextBox5.Text = "";
            materialTextBox6.Text = "";
            materialTextBox7.Text = "";
            materialTextBox21.Text = "";
            materialTextBox22.Text = "";
            comboBox1.SelectedIndex = -1;
        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
            adminEmployees newEmp = new adminEmployees(currentEmployeeID, currentName, currentSurname, currentPatronymic);
            newEmp.Show();
            this.Hide();
        }

        private void materialTextBox6_TextChanged(object sender, EventArgs e)
        {
            string phoneNumber = materialTextBox6.Text.Replace("-", "").Replace("+7", "");
            string formattedPhoneNumber = "+7-";

            if (phoneNumber.Length > 0)
            {
                formattedPhoneNumber += phoneNumber.Substring(0, Math.Min(3, phoneNumber.Length));
            }

            if (phoneNumber.Length > 3)
            {
                formattedPhoneNumber += "-" + phoneNumber.Substring(3, Math.Min(3, phoneNumber.Length - 3));
            }

            if (phoneNumber.Length > 6)
            {
                formattedPhoneNumber += "-" + phoneNumber.Substring(6, Math.Min(2, phoneNumber.Length - 6));
            }

            if (phoneNumber.Length > 8)
            {
                formattedPhoneNumber += "-" + phoneNumber.Substring(8, Math.Min(2, phoneNumber.Length - 8));
            }

            materialTextBox6.TextChanged -= materialTextBox6_TextChanged;
            materialTextBox6.Text = formattedPhoneNumber;
            materialTextBox6.SelectionStart = materialTextBox6.Text.Length;
            materialTextBox6.TextChanged += materialTextBox6_TextChanged;
        }

        private void materialTextBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Блокируем символы, которые не являются цифрами
            }
        }

        private void materialTextBox7_Leave(object sender, EventArgs e)
        {
            string email = materialTextBox7.Text.Trim();
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Неверный формат email");
                materialTextBox7.Focus();
            }
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void materialFloatingActionButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}

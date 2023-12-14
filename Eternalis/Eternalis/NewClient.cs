using Guna.UI2.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Eternalis
{
    public partial class NewClient : Form
    {
        private string currentName;
        private string currentSurname;
        private string currentPatronymic;
        private int currentEmployeeID;
        string connectionString = "Data Source=DESKTOP-HURB5A2\\SQLEXPRESS;Initial Catalog=Eternalis;Integrated Security=True";
        public NewClient(int employeeID, string name, string surname, string patronymic)
        {
            InitializeComponent();
            
            // Сохраняем переданные данные
            currentEmployeeID = employeeID;
            currentName = name;
            currentSurname = surname;
            currentPatronymic = patronymic;
        }

        private void NewClient_Load(object sender, EventArgs e)
        {
            materialLabel1.Text = $"Работает: {currentName} {currentSurname} {currentPatronymic}";
        }

        private void materialFloatingActionButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
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


        private void materialButton5_Click(object sender, EventArgs e)
        {
            // Соберите данные из materialTextBox
    string firstName = materialTextBox1.Text.Trim();
    string lastName = materialTextBox2.Text.Trim();
    string patronymic = materialTextBox3.Text.Trim();
    string birthDate = guna2DateTimePicker1.Value.ToString("yyyy-MM-dd");
    string address = materialTextBox5.Text.Trim();
    string phoneNumber = materialTextBox6.Text.Trim();
    string email = materialTextBox7.Text.Trim();

    // Проверки на заполняемость полей
    if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(birthDate) ||
        string.IsNullOrEmpty(address) || string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(email))
    {
        MessageBox.Show("Все поля должны быть заполнены.");
        return;
    }

    // Проверка на валидность даты
    if (!DateTime.TryParse(birthDate, out _))
    {
        MessageBox.Show("Неверный формат даты рождения.");
        return;
    }

    // Проверка на валидность email
    if (!IsValidEmail(email))
    {
        MessageBox.Show("Неверный формат email.");
        return;
    }

    // Создайте SQL-запрос для вставки нового клиента
    string insertQuery = "INSERT INTO Клиенты (Фамилия, Имя, Отчество, Дата_рождения, Адрес, Телефон, Email) " +
                        "VALUES (@lastName, @firstName, @patronymic, @birthDate, @address, @phoneNumber, @email)";

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();

        using (SqlCommand command = new SqlCommand(insertQuery, connection))
        {
            command.Parameters.AddWithValue("@lastName", lastName);
            command.Parameters.AddWithValue("@firstName", firstName);
            command.Parameters.AddWithValue("@patronymic", patronymic);
            command.Parameters.AddWithValue("@birthDate", birthDate);
            command.Parameters.AddWithValue("@address", address);
            command.Parameters.AddWithValue("@phoneNumber", phoneNumber);
            command.Parameters.AddWithValue("@email", email);

            int rowsAffected = command.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                // Успешно добавлено
                MessageBox.Show("Новый клиент успешно добавлен.");

                // Очистка полей после успешного добавления
                materialTextBox1.Clear();
                materialTextBox2.Clear();
                materialTextBox3.Clear();
                guna2DateTimePicker1.Value = DateTime.Now;
                materialTextBox5.Clear();
                materialTextBox6.Clear();
                materialTextBox7.Clear();
            }
            else
            {
                // Что-то пошло не так
                MessageBox.Show("Ошибка при добавлении клиента.");
            }
        }
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

        private void materialButton4_Click(object sender, EventArgs e)
        {
            Form1 newForm = new Form1();
            newForm.Show();
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

        private void materialButton2_Click(object sender, EventArgs e)
        {
            Deceased deceasedForm = new Deceased(currentEmployeeID, currentName, currentSurname, currentPatronymic);
            deceasedForm.Show();
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

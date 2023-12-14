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


namespace Eternalis
{
    public partial class adminEditClients : Form
    {
        private int clientID;
        private string currentName;
        private string currentSurname;
        private string currentPatronymic;
        private DateTime currentBirthDate;
        private string currentAddress;
        private string currentPhone;
        private string currentEmail;
        string connectionString = "Data Source=DESKTOP-HURB5A2\\SQLEXPRESS;Initial Catalog=Eternalis;Integrated Security=True";
        public adminEditClients(int clientID, string surname, string name, string patronymic, DateTime birthDate, string address, string phone, string email)
        
        {
            InitializeComponent();
            // Сохраняем переданные данные
            this.clientID = clientID;
            currentName = name;
            currentSurname = surname;
            currentPatronymic = patronymic;
            currentBirthDate = birthDate;
            currentAddress = address;
            currentPhone = phone;
            currentEmail = email;
        }

        private void adminEditClients_Load(object sender, EventArgs e)
        {
            materialTextBox2.Text = currentSurname;
            materialTextBox1.Text = currentName;
            materialTextBox3.Text = currentPatronymic;
            guna2DateTimePicker1.Value = currentBirthDate;
            materialTextBox5.Text = currentAddress;
            materialTextBox6.Text = currentPhone;
            materialTextBox7.Text = currentEmail;
        }

        private void materialButton5_Click(object sender, EventArgs e)
        {
            // Обновляем данные клиента в базе данных
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string updateQuery = @"
                        UPDATE [Eternalis].[dbo].[Клиенты]
                        SET [Фамилия] = @Surname,
                            [Имя] = @Name,
                            [Отчество] = @Patronymic,
                            [Дата_рождения] = @BirthDate,
                            [Адрес] = @Address,
                            [Телефон] = @Phone,
                            [Email] = @Email
                        WHERE [ID_клиента] = @ClientID";

                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@Surname", materialTextBox2.Text);
                    updateCommand.Parameters.AddWithValue("@Name", materialTextBox1.Text);
                    updateCommand.Parameters.AddWithValue("@Patronymic", materialTextBox3.Text);
                    updateCommand.Parameters.AddWithValue("@BirthDate", guna2DateTimePicker1.Value);
                    updateCommand.Parameters.AddWithValue("@Address", materialTextBox5.Text);
                    updateCommand.Parameters.AddWithValue("@Phone", materialTextBox6.Text);
                    updateCommand.Parameters.AddWithValue("@Email", materialTextBox7.Text);
                    updateCommand.Parameters.AddWithValue("@ClientID", clientID);

                    updateCommand.ExecuteNonQuery();

                    MessageBox.Show("Данные клиента успешно обновлены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Close();
        }

        private void materialFloatingActionButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void materialFloatingActionButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void materialFloatingActionButton3_Click(object sender, EventArgs e)
        {
            this.Close();
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
            // Регулярное выражение для проверки формата email
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            // Проверка формата email
            return Regex.IsMatch(email, emailPattern);
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
    }

}

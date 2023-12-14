using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;
namespace Eternalis
{

    public partial class Form1 : Form
    {
        private string currentName;
        private string currentSurname;
        private string currentPatronymic;
        private int currentEmployeeID;
        string connectionString = "Data Source=DESKTOP-HURB5A2\\SQLEXPRESS;Initial Catalog=Eternalis;Integrated Security=True";
        public Form1()
        {
            InitializeComponent();

        }

        private void materialFloatingActionButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void materialSwitch1_CheckedChanged(object sender, EventArgs e)
        {
            // Проверяем, включен ли materialSwitch1
            if (materialSwitch1.Checked)
            {
                // Если включен, то показываем пароль
                materialTextBox2.PasswordChar = '\0'; // '\0' означает, что пароль не скрыт
            }
            else
            {
                // Если выключен, то скрываем пароль
                materialTextBox2.PasswordChar = '•'; // Можете использовать другой символ, если хотите
            }
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            string login = materialTextBox1.Text;
            string password = materialTextBox2.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Запрос для получения хеша пароля пользователя
                string query = "SELECT ID_сотрудника, Имя, Фамилия, Отчество, ID_должности, Пароль FROM Сотрудники WHERE Логин = @login";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@login", login);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            currentEmployeeID = reader.GetInt32(0);
                            currentName = reader.GetString(1);
                            currentSurname = reader.GetString(2);
                            currentPatronymic = reader.GetString(3);
                            int role = reader.GetInt32(4);
                            string storedHashedPassword = reader.GetString(5);

                            // Проверка хеша пароля
                            if (VerifyPassword(password, storedHashedPassword))
                            {
                                // Пароль верный
                                if (role == 1)
                                {
                                    // Открываем форму Agent и передаем данные сотрудника
                                    Agent agentForm = new Agent(currentEmployeeID, currentName, currentSurname, currentPatronymic);
                                    agentForm.Show();
                                    this.Hide();
                                }
                                else if (role == 2)
                                {
                                    // Открываем форму Admin и передаем данные сотрудника
                                    Admin adminForm = new Admin(currentEmployeeID, currentName, currentSurname, currentPatronymic);
                                    adminForm.Show();
                                    this.Hide();
                                }
                            }
                            else
                            {
                                // Ошибка авторизации - пароль неверен
                                MessageBox.Show("Неправильный пароль. Попробуйте снова.");
                            }
                        }
                        else
                        {
                            // Ошибка авторизации - логин не найден
                            MessageBox.Show("Неправильный логин. Попробуйте снова.");
                        }
                    }
                }
            }
        }
        private bool VerifyPassword(string enteredPassword, string storedHashedPassword)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                // Хеширование введенного пароля и сравнение с хешем из базы данных
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(enteredPassword));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower() == storedHashedPassword;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}

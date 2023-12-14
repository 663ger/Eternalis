using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using WordApp = Microsoft.Office.Interop.Word.Application;
using WordDoc = Microsoft.Office.Interop.Word.Document;
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
using System.Web.UI.WebControls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Eternalis
{
    public partial class Order : Form
    {
        private string currentName;
        private string currentSurname;
        private string currentPatronymic;
        private int currentEmployeeID;
        string connectionString = "Data Source=DESKTOP-HURB5A2\\SQLEXPRESS;Initial Catalog=Eternalis;Integrated Security=True";
        public Order(int employeeID, string name, string surname, string patronymic)
        {
            InitializeComponent();
            // Сохраняем переданные данные
            currentEmployeeID = employeeID;
            currentName = name;
            currentSurname = surname;
            currentPatronymic = patronymic;
            LoadClients();
            LoadDeceased();
            LoadFuneralTypes();
            LoadServices();
            SetupAutoComplete(); // Вызывается метод настройки автозавершения


        }
        private List<ServiceInfo> selectedServices = new List<ServiceInfo>();
        public class ServiceInfo
        {
            public string Name { get; set; }
            public decimal Cost { get; set; }

            public ServiceInfo(string name, decimal cost)
            {
                Name = name;
                Cost = cost;
            }
        }

        private void Order_Load(object sender, EventArgs e)
        {
            materialLabel1.Text = $"Работает: {currentName} {currentSurname} {currentPatronymic}";
            
        }

        private void materialFloatingActionButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
                            comboBox1.Items.Add(reader["ФИО"]);
                        }
                    }
                }
            }
        }
        private void LoadDeceased()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT CONCAT(Имя, ' ', Фамилия, ' ', Отчество) AS ФИО FROM Покойные";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox2.Items.Add(reader["ФИО"]);
                        }
                    }
                }
            }
        }
        // Метод для загрузки данных о видах похорон в ComboBox3
        private void LoadFuneralTypes()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT [Наименование] FROM Виды_похорон";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox3.Items.Add(reader["Наименование"]);
                        }
                    }
                }
            }
        }
        // Метод для загрузки данных о услугах в ComboBox4
        private void LoadServices()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT [Название] FROM Услуги";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            comboBox4.Items.Add(reader["Название"]);
                        }
                    }
                }
            }
        }
        private void SetupAutoComplete()
        {
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox4.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox4.AutoCompleteSource = AutoCompleteSource.ListItems;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFullName = comboBox1.SelectedItem?.ToString();

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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFullName = comboBox2.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(selectedFullName))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT [ID_покойного], [Фамилия], [Имя], [Отчество], [Дата_рождения], [Дата_смерти], [ID_клиента]
                             FROM [Покойные]
                             WHERE CONCAT([Имя], ' ', [Фамилия], ' ', [Отчество]) = @FullName";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@FullName", selectedFullName);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string deceasedID = reader["ID_покойного"].ToString();
                        string lastName = reader["Фамилия"].ToString();
                        string firstName = reader["Имя"].ToString();
                        string patronymic = reader["Отчество"].ToString();
                        string birthdate = reader["Дата_рождения"].ToString();
                        string deathDate = reader["Дата_смерти"].ToString();
                        string clientID = reader["ID_клиента"].ToString();

                        // Получите ФИО клиента
                        string clientFullName = GetClientFullNameByID(clientID);

                        // Заполните ComboBox1 данными о клиенте
                        comboBox1.SelectedItem = clientFullName;
                    }

                    reader.Close();
                }
            }
        }

        private string GetClientFullNameByID(string clientID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT CONCAT([Имя], ' ', [Фамилия], ' ', [Отчество]) AS [ФИО] FROM [Клиенты] WHERE [ID_клиента] = @ClientID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ClientID", clientID);
                    return command.ExecuteScalar()?.ToString();
                }
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedFuneralType = comboBox3.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(selectedFuneralType))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT [ID_вида_похорон], [Наименование], [Описание], [Адрес], [Стоимость]
                             FROM [Виды_похорон]
                             WHERE [Наименование] = @FuneralType";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@FuneralType", selectedFuneralType);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string funeralTypeID = reader["ID_вида_похорон"].ToString();
                        string description = reader["Описание"].ToString();
                        string address = reader["Адрес"].ToString();
                        string cost = reader["Стоимость"].ToString();

                        // Дальнейшая обработка данных о виде похорон...

                        
                    }

                    reader.Close();
                }
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем ввод только цифр, двоеточия и клавиши Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != ':' && e.KeyChar != '\b')
            {
                e.Handled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string input = new string(textBox1.Text.Where(char.IsDigit).ToArray());

            if (input.Length > 4)
            {
                input = input.Substring(0, 4);
            }

            if (input.Length >= 2)
            {
                input = input.Insert(2, ":");
            }

            textBox1.Text = input;
            textBox1.SelectionStart = textBox1.Text.Length;


        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            string timePattern = @"^(0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]$"; // Регулярное выражение для времени в формате "00:00"

            if (!Regex.IsMatch(textBox1.Text, timePattern))
            {
                MessageBox.Show("Неверный формат времени. Пожалуйста, введите время в формате 'часы:минуты' (например, '12:30').", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true; // Отменяет событие Validating и оставляет фокус на текстовом поле.
            }
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedService = comboBox4.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(selectedService))
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = @"SELECT [ID_услуги], [Название], [Стоимость]
                             FROM [Услуги]
                             WHERE [Название] = @ServiceName";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ServiceName", selectedService);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        string serviceName = reader["Название"].ToString();
                        decimal cost = (decimal)reader["Стоимость"];
                        ServiceInfo service = new ServiceInfo(serviceName, cost);

                        // Проверяем, не добавлена ли уже выбранная услуга
                        if (!selectedServices.Any(s => s.Name == service.Name))
                        {
                            // Добавляем выбранную услугу в список выбранных услуг
                            selectedServices.Add(service);

                            // Обновляем MaterialLabel8, отображая выбранные услуги
                            UpdateSelectedServicesLabel();
                        }
                    }

                    reader.Close();
                }
            }
        }

        private void UpdateSelectedServicesLabel()
        {
            // Создаем список строк для выбранных услуг
            List<string> selectedServiceStrings = new List<string>();

            foreach (var service in selectedServices)
            {
                selectedServiceStrings.Add($"{service.Name}: {service.Cost:C}");
            }

            // Обновляем MaterialLabel8, отображая выбранные услуги через запятую
            materialLabel8.Text = "Выбранные услуги: " + string.Join(", ", selectedServiceStrings);
        }

        private void materialButton6_Click(object sender, EventArgs e)
        {
            if (comboBox4.SelectedItem != null)
            {
                string selectedServiceName = comboBox4.SelectedItem.ToString();

                // Находим выбранную услугу в списке выбранных услуг
                var serviceToRemove = selectedServices.FirstOrDefault(service => service.Name == selectedServiceName);

                if (serviceToRemove != null)
                {
                    // Удаляем выбранную услугу из списка выбранных услуг
                    selectedServices.Remove(serviceToRemove);

                    // Обновляем MaterialLabel8
                    UpdateSelectedServicesLabel();
                }
            }
        }

        private void materialButton5_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null && comboBox2.SelectedItem != null && comboBox3.SelectedItem != null)
            {
                int clientID = GetClientID(comboBox1.SelectedItem.ToString());
                int deceasedID = GetDeceasedID(comboBox2.SelectedItem.ToString());
                int funeralTypeID = GetFuneralTypeID(comboBox3.SelectedItem.ToString());
                DateTime orderDate = DateTime.Today;

                // Совмещение даты из guna2DateTimePicker1 и времени из textBox1
                DateTime funeralDateTime = guna2DateTimePicker1.Value.Date;
                TimeSpan timeSpan;
                if (TimeSpan.TryParse(textBox1.Text, out timeSpan))
                {
                    funeralDateTime = funeralDateTime.Add(timeSpan);
                }

                decimal totalCost = CalculateTotalCost(funeralTypeID);

                // Проверка на конфликты
                if (HasOrderConflictForDeceased(deceasedID))
                {
                    MessageBox.Show("Покойный уже участвует в другом заказе. Пожалуйста, выберите другого покойного.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (HasOrderConflictForDateTime(funeralTypeID, funeralDateTime))
                {
                    MessageBox.Show("Конфликт с существующим заказом. Пожалуйста, выберите другой вид похорон или другую дату и время.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    string confirmationMessage = $"Итоговая сумма заказа: {totalCost:C}\n\nВы действительно хотите оформить заказ?";

                    DialogResult result = MessageBox.Show(confirmationMessage, "Подтверждение заказа", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        int orderID = InsertOrder(clientID, deceasedID, currentEmployeeID, funeralTypeID, orderDate, funeralDateTime, totalCost);

                        if (selectedServices.Count > 0)
                        {
                            InsertOrderServices(orderID);
                        }

                        MessageBox.Show("Заказ успешно оформлен.");

                        // Очистка всех полей после успешного оформления заказа
                        comboBox1.SelectedIndex = -1;
                        comboBox2.SelectedIndex = -1;
                        comboBox3.SelectedIndex = -1;
                        comboBox4.SelectedIndex = -1;
                        guna2DateTimePicker1.Value = DateTime.Now; // Сброс даты на текущую
                        textBox1.Text = string.Empty;
                        selectedServices.Clear();
                        materialLabel8.Text = "";
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, выберите клиента, покойного и вид похорон.");
            }
        }
        private bool HasOrderConflictForDateTime(int funeralTypeID, DateTime funeralDateTime)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Проверка наличия заказа для данного покойного на указанную дату и время
                    string query = @"
                SELECT COUNT(*)
                FROM [Eternalis].[dbo].[Заказы]
                WHERE [ID_вида_похорон] = @funeralTypeID
                  AND [Дата_похорон] = @funeralDateTime";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@funeralTypeID", funeralTypeID);
                    command.Parameters.AddWithValue("@funeralDateTime", funeralDateTime);

                    int orderCount = (int)command.ExecuteScalar();

                    return orderCount > 0; // Возвращает true, если есть конфликт
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при проверке конфликта заказов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // В случае ошибки возвращаем false, чтобы предотвратить оформление заказа
            }
        }

        private bool HasOrderConflictForDeceased(int deceasedID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Проверка наличия заказа для данного покойного
                    string query = @"
                SELECT COUNT(*)
                FROM [Eternalis].[dbo].[Заказы]
                WHERE [ID_покойного] = @deceasedID";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@deceasedID", deceasedID);

                    int orderCount = (int)command.ExecuteScalar();

                    return orderCount > 0; // Возвращает true, если есть конфликт
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при проверке конфликта заказов: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false; // В случае ошибки возвращаем false, чтобы предотвратить оформление заказа
            }
        }
        private int GetClientID(string clientFullName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT [ID_клиента] FROM [Клиенты] WHERE CONCAT([Имя], ' ', [Фамилия], ' ', [Отчество]) = @FullName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FullName", clientFullName);
                    return (int)command.ExecuteScalar();
                }
            }
        }

        private int GetDeceasedID(string deceasedFullName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT [ID_покойного] FROM [Покойные] WHERE CONCAT([Имя], ' ', [Фамилия], ' ', [Отчество]) = @FullName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FullName", deceasedFullName);
                    return (int)command.ExecuteScalar();
                }
            }
        }

        private int GetFuneralTypeID(string funeralType)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT [ID_вида_похорон] FROM [Виды_похорон] WHERE [Наименование] = @FuneralType";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FuneralType", funeralType);
                    return (int)command.ExecuteScalar();
                }
            }
        }

        private decimal CalculateTotalCost(int funeralTypeID)
        {
            decimal totalCost = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT [Стоимость] FROM [Виды_похорон] WHERE [ID_вида_похорон] = @FuneralTypeID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FuneralTypeID", funeralTypeID);
                    totalCost += (decimal)command.ExecuteScalar();
                }
            }

            foreach (var service in selectedServices)
            {
                totalCost += service.Cost;
            }

            return totalCost;
        }

        private int InsertOrder(int clientID, int deceasedID, int employeeID, int funeralTypeID, DateTime orderDate, DateTime funeralDate, decimal totalCost)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO [Заказы] ([ID_клиента], [ID_покойного], [ID_сотрудника], [ID_вида_похорон], [Дата_заказа], [Дата_похорон], [Итоговая_сумма]) " +
                               "VALUES (@ClientID, @DeceasedID, @EmployeeID, @FuneralTypeID, @OrderDate, @FuneralDate, @TotalCost); " +
                               "SELECT SCOPE_IDENTITY();";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ClientID", clientID);
                    command.Parameters.AddWithValue("@DeceasedID", deceasedID);
                    command.Parameters.AddWithValue("@EmployeeID", employeeID);
                    command.Parameters.AddWithValue("@FuneralTypeID", funeralTypeID);
                    command.Parameters.AddWithValue("@OrderDate", orderDate);
                    command.Parameters.AddWithValue("@FuneralDate", funeralDate);
                    command.Parameters.AddWithValue("@TotalCost", totalCost);

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        private void InsertOrderServices(int orderID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "INSERT INTO [Услуги_заказа] ([ID_заказа], [ID_услуги]) VALUES (@OrderID, @ServiceID)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    foreach (var service in selectedServices)
                    {
                        command.Parameters.Clear();
                        command.Parameters.AddWithValue("@OrderID", orderID);

                        int serviceID = GetServiceID(service.Name);
                        command.Parameters.AddWithValue("@ServiceID", serviceID);

                        command.ExecuteNonQuery();
                    }
                }
            }
        }

        private int GetServiceID(string serviceName)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT [ID_услуги] FROM [Услуги] WHERE [Название] = @ServiceName";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ServiceName", serviceName);
                    return (int)command.ExecuteScalar();
                }
            }
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            Clients newClients = new Clients(currentEmployeeID, currentName, currentSurname, currentPatronymic);
            newClients.Show();
            this.Hide();
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

        private void materialButton4_Click(object sender, EventArgs e)
        {
            Form1 newForm = new Form1();
            newForm.Show();
            this.Hide();
        }

        private void materialButton7_Click(object sender, EventArgs e)
        {
            // Очистка всех полей после успешного оформления заказа
            comboBox1.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;
            comboBox4.SelectedIndex = -1;
            guna2DateTimePicker1.Value = DateTime.Now; // Сброс даты на текущую
            textBox1.Text = string.Empty;
            selectedServices.Clear();
            materialLabel8.Text = "";
        }

        private void materialButton3_Click(object sender, EventArgs e)
        {
            viewOrders newOrder = new viewOrders(currentEmployeeID, currentName, currentSurname, currentPatronymic);
            newOrder.Show();
            this.Hide();
        }
    }
}

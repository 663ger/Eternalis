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
    public partial class adminEditDeceased : Form
    {
        private int deceasedID;
        private string currentName;
        private string currentSurname;
        private string currentPatronymic;
        private DateTime currentBirthDate;
        private DateTime currentDeathDate;
        private int currentClientID;

        string connectionString = "Data Source=DESKTOP-HURB5A2\\SQLEXPRESS;Initial Catalog=Eternalis;Integrated Security=True";

        public adminEditDeceased(int deceasedID, string surname, string name, string patronymic, DateTime birthDate, DateTime deathDate, int clientID)
        {
            InitializeComponent();
            // Сохраняем переданные данные
            this.deceasedID = deceasedID;
            currentName = name;
            currentSurname = surname;
            currentPatronymic = patronymic;
            currentBirthDate = birthDate;
            currentDeathDate = deathDate;
            currentClientID = clientID;
        }

        private void FillClientComboBox()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT [ID_клиента], [Фамилия] + ' ' + [Имя] + ' ' + [Отчество] AS [ФИО_клиента] FROM [Eternalis].[dbo].[Клиенты]";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Привязываем DataTable к ComboBox
                    comboBox1.DataSource = dataTable;
                    comboBox1.DisplayMember = "ФИО_клиента";
                    comboBox1.ValueMember = "ID_клиента";

                    // Настройка автоподстановки
                    comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void materialButton5_Click(object sender, EventArgs e)
        {
            // Обновляем данные покойного в базе данных
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string updateQuery = @"
                        UPDATE [Eternalis].[dbo].[Покойные]
                        SET [Фамилия] = @Surname,
                            [Имя] = @Name,
                            [Отчество] = @Patronymic,
                            [Дата_рождения] = @BirthDate,
                            [Дата_смерти] = @DeathDate,
                            [ID_клиента] = @ClientID
                        WHERE [ID_покойного] = @DeceasedID";

                    SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                    updateCommand.Parameters.AddWithValue("@Surname", materialTextBox2.Text);
                    updateCommand.Parameters.AddWithValue("@Name", materialTextBox1.Text);
                    updateCommand.Parameters.AddWithValue("@Patronymic", materialTextBox3.Text);
                    updateCommand.Parameters.AddWithValue("@BirthDate", guna2DateTimePicker1.Value);
                    updateCommand.Parameters.AddWithValue("@DeathDate", guna2DateTimePicker2.Value);
                    updateCommand.Parameters.AddWithValue("@ClientID", comboBox1.SelectedValue);
                    updateCommand.Parameters.AddWithValue("@DeceasedID", deceasedID);

                    updateCommand.ExecuteNonQuery();

                    MessageBox.Show("Данные покойного успешно обновлены.", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            this.Close();
        }

        private void materialFloatingActionButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void adminEditDeceased_Load_1(object sender, EventArgs e)
        {
            materialTextBox2.Text = currentSurname;
            materialTextBox1.Text = currentName;
            materialTextBox3.Text = currentPatronymic;
            guna2DateTimePicker1.Value = currentBirthDate;
            guna2DateTimePicker2.Value = currentDeathDate;

            // Заполняем ComboBox клиентами
            FillClientComboBox();

            // Устанавливаем выбранным клиента из базы данных
            comboBox1.SelectedValue = currentClientID;
        }

        private void materialFloatingActionButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void materialFloatingActionButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

using System;
using System.Data;
using System.Windows.Forms;
using SysData = System.Data;
using System.Data.SqlClient;
using Microsoft.Office.Interop.Word;
using Word = Microsoft.Office.Interop.Word;
using WinForms = System.Windows.Forms;
using System.IO;



namespace Eternalis
{
    public partial class viewOrders : WinForms.Form
    {
        private string currentName;
        private string currentSurname;
        private string currentPatronymic;
        private int currentEmployeeID;
        string connectionString = "Data Source=DESKTOP-HURB5A2\\SQLEXPRESS;Initial Catalog=Eternalis;Integrated Security=True";
        public viewOrders(int employeeID, string name, string surname, string patronymic)
        {
            InitializeComponent();
            // Сохраняем переданные данные
            currentEmployeeID = employeeID;
            currentName = name;
            currentSurname = surname;
            currentPatronymic = patronymic;
        }

        private void viewOrders_Load(object sender, EventArgs e)
        {
            FillDataGridView();
            materialLabel1.Text = $"Работает: {currentName} {currentSurname} {currentPatronymic}";
        }
        private void FillDataGridView()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Запрос для объединения данных из двух таблиц
                    string query = @"
                SELECT 
                    Заказы.[ID_заказа],
                    Клиенты.[Фамилия] + ' ' + Клиенты.[Имя] + ' ' + Клиенты.[Отчество] AS Клиент,
                    Покойные.[Фамилия] + ' ' + Покойные.[Имя] + ' ' + Покойные.[Отчество] AS Покойный,
                    Сотрудники.[Фамилия] + ' ' + Сотрудники.[Имя] + ' ' + Сотрудники.[Отчество] AS Сотрудник,
                    Виды_похорон.[Наименование] AS Вид_похорон,
                    Виды_похорон.[Адрес] AS Адрес_похорон,
                    Виды_похорон.[Описание] AS Описание_похорон,
                    STRING_AGG(Услуги.[Название], ', ') AS Наименование_услуги,
                    Заказы.[Итоговая_сумма] AS Итоговая_сумма,
                    Заказы.[Дата_похорон] AS Дата_похорон,
                    Заказы.[Дата_заказа] AS Дата_заказа
                FROM 
                    [Eternalis].[dbo].[Заказы]
                LEFT JOIN 
                    [Eternalis].[dbo].[Услуги_заказа] ON Заказы.[ID_заказа] = Услуги_заказа.[ID_заказа]
                LEFT JOIN 
                    [Eternalis].[dbo].[Клиенты] ON Заказы.[ID_клиента] = Клиенты.[ID_клиента]
                LEFT JOIN 
                    [Eternalis].[dbo].[Покойные] ON Заказы.[ID_покойного] = Покойные.[ID_покойного]
                LEFT JOIN 
                    [Eternalis].[dbo].[Сотрудники] ON Заказы.[ID_сотрудника] = Сотрудники.[ID_сотрудника]
                LEFT JOIN 
                    [Eternalis].[dbo].[Виды_похорон] ON Заказы.[ID_вида_похорон] = Виды_похорон.[ID_вида_похорон]
                LEFT JOIN 
                    [Eternalis].[dbo].[Услуги] ON Услуги_заказа.[ID_услуги] = Услуги.[ID_услуги]
                GROUP BY 
                    Заказы.[ID_заказа],
                    Клиенты.[Фамилия], Клиенты.[Имя], Клиенты.[Отчество],
                    Покойные.[Фамилия], Покойные.[Имя], Покойные.[Отчество],
                    Сотрудники.[Фамилия], Сотрудники.[Имя], Сотрудники.[Отчество],
                    Виды_похорон.[Наименование], Виды_похорон.[Адрес], Виды_похорон.[Описание],
                    Заказы.[Итоговая_сумма],
                    Заказы.[Дата_похорон],
                    Заказы.[Дата_заказа]";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    SysData.DataTable dataTable = new SysData.DataTable();
                    adapter.Fill(dataTable);

                    // Привязываем DataTable к DataGridView
                    dataGridView1.DataSource = dataTable;

                    // Скрываем столбцы Id_заказа и Дата_заказа
                    dataGridView1.Columns["ID_заказа"].Visible = false;

                    // Устанавливаем названия столбцов
                    dataGridView1.Columns["Клиент"].HeaderText = "Клиент";
                    dataGridView1.Columns["Покойный"].HeaderText = "Покойный";
                    dataGridView1.Columns["Сотрудник"].HeaderText = "Сотрудник";
                    dataGridView1.Columns["Вид_похорон"].HeaderText = "Вид похорон";
                    dataGridView1.Columns["Адрес_похорон"].HeaderText = "Адрес похорон";
                    dataGridView1.Columns["Описание_похорон"].HeaderText = "Описание похорон";
                    dataGridView1.Columns["Наименование_услуги"].HeaderText = "Наименование услуги";
                    dataGridView1.Columns["Итоговая_сумма"].HeaderText = "Итоговая сумма";
                    dataGridView1.Columns["Дата_похорон"].HeaderText = "Дата похорон";
                    dataGridView1.Columns["Дата_заказа"].HeaderText = "Дата заказа";

                }
            }
            catch (Exception ex)
            {
                WinForms.MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", WinForms.MessageBoxButtons.OK, WinForms.MessageBoxIcon.Error);
            }
        }

        private void materialFloatingActionButton1_Click(object sender, EventArgs e)
        {
            WinForms.Application.Exit();
        }

        private void materialButton5_Click(object sender, EventArgs e)
        {
            Order newOrder = new Order(currentEmployeeID, currentName, currentSurname, currentPatronymic);
            newOrder.Show();
            this.Hide();
        }

        private void materialButton6_Click(object sender, EventArgs e)
        {
            try
    {
        if (dataGridView1.SelectedRows.Count > 0)
        {
            // Получаем выбранную строку из DataGridView
            DataRowView selectedRow = (DataRowView)dataGridView1.SelectedRows[0].DataBoundItem;

            // Формируем имя файла акта
            string actFileName = $"C:\\Users\\User\\Desktop\\Eternalis\\Act_{selectedRow["ID_заказа"]}.docx";

            // Проверяем, существует ли файл
            if (File.Exists(actFileName))
            {
                MessageBox.Show("Акт для выбранного заказа уже оформлен.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Создаем новый Word документ
            Word.Application wordApp = new Word.Application();
            wordApp.Visible = true;

            // Путь к макету Word
            string templatePath = @"C:\Users\User\Desktop\Eternalis\Eternalis\ActDocument.doc";

            // Открываем макет Word
            Word.Document doc = wordApp.Documents.Add(templatePath);

                    // Заполняем макет данными из выбранной строки
                    ReplaceWordStub("{ID_заказа}", selectedRow["ID_заказа"].ToString(), doc);
                    ReplaceWordStub("{ДатаЗаказа}", selectedRow["Дата_заказа"].ToString(), doc);
                    ReplaceWordStub("{ФиоСотрудника}", selectedRow["Сотрудник"].ToString(), doc);
                    ReplaceWordStub("{ФиоКлиента}", selectedRow["Клиент"].ToString(), doc);
                    ReplaceWordStub("{ФиоПокойного}", selectedRow["Покойный"].ToString(), doc);
                    ReplaceWordStub("{НаименованиеПохорон}", selectedRow["Вид_похорон"].ToString(), doc);
                    ReplaceWordStub("{ОписаниеПохорон}", selectedRow["Описание_похорон"].ToString(), doc);
                    ReplaceWordStub("{АдресПохорон}", selectedRow["Адрес_похорон"].ToString(), doc);
                    ReplaceWordStub("{ДатаПохорон}", selectedRow["Дата_похорон"].ToString(), doc);
                    ReplaceWordStub("{Наименование услуг}", selectedRow["Наименование_услуги"].ToString(), doc);
                    ReplaceWordStub("{ИтоговаяСуммаЗаказа}", selectedRow["Итоговая_сумма"].ToString(), doc);

                    // Сохраняем созданный документ
                    string outputPath = $"C:\\Users\\User\\Desktop\\Eternalis\\Act_{selectedRow["ID_заказа"]}.docx";
                    doc.SaveAs2(outputPath);

                    MessageBox.Show($"Акт выполненных работ сохранен по пути: {outputPath}", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Выберите заказ для формирования акта выполненных работ.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        // Метод для замены меток в Word документе
        private void ReplaceWordStub(string stubToReplace, string text, Word.Document wordDocument)
        {
            var range = wordDocument.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text, Replace: Word.WdReplace.wdReplaceAll);
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
        private void SearchOrderByDate(DateTime searchDate)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Запрос для объединения данных из двух таблиц и фильтрации по дате заказа
                    string query = @"
                SELECT 
                    Заказы.[ID_заказа],
                    Клиенты.[Фамилия] + ' ' + Клиенты.[Имя] + ' ' + Клиенты.[Отчество] AS Клиент,
                    Покойные.[Фамилия] + ' ' + Покойные.[Имя] + ' ' + Покойные.[Отчество] AS Покойный,
                    Сотрудники.[Фамилия] + ' ' + Сотрудники.[Имя] + ' ' + Сотрудники.[Отчество] AS Сотрудник,
                    Виды_похорон.[Наименование] AS Вид_похорон,
                    Виды_похорон.[Адрес] AS Адрес_похорон,
                    Виды_похорон.[Описание] AS Описание_похорон,
                    STRING_AGG(Услуги.[Название], ', ') AS Наименование_услуги,
                    Заказы.[Итоговая_сумма] AS Итоговая_сумма,
                    Заказы.[Дата_похорон] AS Дата_похорон,
                    Заказы.[Дата_заказа] AS Дата_заказа
                FROM 
                    [Eternalis].[dbo].[Заказы]
                LEFT JOIN 
                    [Eternalis].[dbo].[Услуги_заказа] ON Заказы.[ID_заказа] = Услуги_заказа.[ID_заказа]
                LEFT JOIN 
                    [Eternalis].[dbo].[Клиенты] ON Заказы.[ID_клиента] = Клиенты.[ID_клиента]
                LEFT JOIN 
                    [Eternalis].[dbo].[Покойные] ON Заказы.[ID_покойного] = Покойные.[ID_покойного]
                LEFT JOIN 
                    [Eternalis].[dbo].[Сотрудники] ON Заказы.[ID_сотрудника] = Сотрудники.[ID_сотрудника]
                LEFT JOIN 
                    [Eternalis].[dbo].[Виды_похорон] ON Заказы.[ID_вида_похорон] = Виды_похорон.[ID_вида_похорон]
                LEFT JOIN 
                    [Eternalis].[dbo].[Услуги] ON Услуги_заказа.[ID_услуги] = Услуги.[ID_услуги]
                WHERE 
                    Заказы.[Дата_заказа] = @searchDate
                GROUP BY 
                    Заказы.[ID_заказа],
                    Клиенты.[Фамилия], Клиенты.[Имя], Клиенты.[Отчество],
                    Покойные.[Фамилия], Покойные.[Имя], Покойные.[Отчество],
                    Сотрудники.[Фамилия], Сотрудники.[Имя], Сотрудники.[Отчество],
                    Виды_похорон.[Наименование], Виды_похорон.[Адрес], Виды_похорон.[Описание],
                    Заказы.[Итоговая_сумма],
                    Заказы.[Дата_похорон],
                    Заказы.[Дата_заказа]";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@searchDate", searchDate);

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    SysData.DataTable dataTable = new SysData.DataTable();
                    adapter.Fill(dataTable);

                    // Привязываем DataTable к DataGridView
                    dataGridView1.DataSource = dataTable;

                    // Скрываем столбцы Id_заказа и Дата_заказа
                    dataGridView1.Columns["ID_заказа"].Visible = false;

                    // Устанавливаем названия столбцов
                    dataGridView1.Columns["Клиент"].HeaderText = "Клиент";
                    dataGridView1.Columns["Покойный"].HeaderText = "Покойный";
                    dataGridView1.Columns["Сотрудник"].HeaderText = "Сотрудник";
                    dataGridView1.Columns["Вид_похорон"].HeaderText = "Вид похорон";
                    dataGridView1.Columns["Адрес_похорон"].HeaderText = "Адрес похорон";
                    dataGridView1.Columns["Описание_похорон"].HeaderText = "Описание похорон";
                    dataGridView1.Columns["Наименование_услуги"].HeaderText = "Наименование услуги";
                    dataGridView1.Columns["Итоговая_сумма"].HeaderText = "Итоговая сумма";
                    dataGridView1.Columns["Дата_похорон"].HeaderText = "Дата похорон";
                    dataGridView1.Columns["Дата_заказа"].HeaderText = "Дата заказа";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void materialButton8_Click(object sender, EventArgs e)
        {
            SearchOrderByDate(guna2DateTimePicker1.Value);
        }

        private void materialButton7_Click(object sender, EventArgs e)
        {
            FillDataGridView();
        }
    }
}

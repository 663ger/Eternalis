using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Eternalis
{
    public partial class Agent : Form
    {
        private string currentName;
        private string currentSurname;
        private string currentPatronymic;
        private int currentEmployeeID;
        string connectionString = "Data Source=DESKTOP-HURB5A2\\SQLEXPRESS;Initial Catalog=Eternalis;Integrated Security=True";
        public Agent(int employeeID, string name, string surname, string patronymic)
        {
            InitializeComponent();
            // Сохраняем переданные данные
            currentEmployeeID = employeeID;
            currentName = name;
            currentSurname = surname;
            currentPatronymic = patronymic;

        }

        private void Agent_Load(object sender, EventArgs e)
        {
            materialLabel1.Text = $"Работает: {currentName} {currentSurname} {currentPatronymic}";
        }

        private void materialFloatingActionButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Eternalis
{
    public partial class Admin : Form
    {
        private string currentName;
        private string currentSurname;
        private string currentPatronymic;
        private int currentEmployeeID;
        string connectionString = "Data Source=DESKTOP-HURB5A2\\SQLEXPRESS;Initial Catalog=Eternalis;Integrated Security=True";
        public Admin(int employeeID, string name, string surname, string patronymic)
        {
            InitializeComponent();
            // Сохраняем переданные данные
            currentEmployeeID = employeeID;
            currentName = name;
            currentSurname = surname;
            currentPatronymic = patronymic;
        }

        private void Admin_Load(object sender, EventArgs e)
        {
            materialLabel1.Text = $"Работает: {currentName} {currentSurname} {currentPatronymic}";
        }

        private void materialFloatingActionButton1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            this.Hide();
        }

        private void materialFloatingActionButton2_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void materialButton1_Click(object sender, EventArgs e)
        {
            adminClients newClients = new adminClients(currentEmployeeID, currentName, currentSurname, currentPatronymic);
            newClients.Show();
            this.Hide();
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            adminDeceased newDeceased = new adminDeceased(currentEmployeeID, currentName, currentSurname, currentPatronymic);
            newDeceased.Show();
            this.Hide();
        }

        private void materialButton3_Click(object sender, EventArgs e)
        {
            adminOrders newOrd = new adminOrders(currentEmployeeID, currentName, currentSurname, currentPatronymic);
            newOrd.Show();
            this.Hide();
        }

        private void materialButton5_Click(object sender, EventArgs e)
        {
            adminTypesFunerals newType = new adminTypesFunerals(currentEmployeeID, currentName, currentSurname, currentPatronymic);
            newType.Show();
            this.Hide();
        }

        private void materialButton7_Click(object sender, EventArgs e)
        {
            adminServices newSer = new adminServices(currentEmployeeID, currentName, currentSurname, currentPatronymic);
            newSer.Show();
            this.Hide();
        }

        private void materialButton6_Click(object sender, EventArgs e)
        {
            adminEmployees newEmp = new adminEmployees(currentEmployeeID, currentName, currentSurname, currentPatronymic);
            newEmp.Show();
            this.Hide();
        }
    }
}

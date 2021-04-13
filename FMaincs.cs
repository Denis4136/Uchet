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

namespace WOPUY
{
    public partial class FMaincs : Form
    {
        public FMaincs()
        {
            InitializeComponent();
            ConnectionString = "Data Source=STUDENT-407-006;Initial Catalog=Volobuev_Uchet;Integrated Security=True";
            conn(ConnectionString, PCK, dataGridView1);
            conn(ConnectionString, VidMetod, dataGridView2);
            conn(ConnectionString, MeroprPrepod, dataGridView3);
            conn(ConnectionString, Predsedatelya, dataGridView4);

            conn2(ConnectionString, PCK, comboBox2, "Название ПЦК", "Код ПЦК");
            conn2(ConnectionString, VidMetod, comboBox3, "Название Вид Метод", "Код Вид Метод");
            conn2(ConnectionString, Predsedatelya, comboBox1, "ФИО", "Код председателя");
        }
        public string ConnectionString = "";

        string PCK = "SELECT kod_pck AS [Код ПЦК], namepck AS [Название ПЦК], predsedpck AS [Председатель ПЦК] FROM PCK";
        string VidMetod = "SELECT kod_vidmetod AS [Код Вид Метод], namevidmetod AS [Название Вид Метод] FROM VidMetod";
        string MeroprPrepod = "SELECT MeroprPrepod.kod_meroprprepod AS [Код Преподаватель], MeroprPrepod.namemeropr AS [Название Меропр], MeroprPrepod.kod_vidmetod AS [Вид мероприятия], MeroprPrepod.uroven AS Уровень, MeroprPrepod.result AS Результат, MeroprPrepod.dokum AS Документ, MeroprPrepod.mesto AS[Место проведения], MeroprPrepod.dataprov AS[Дата проведения], MeroprPrepod.kod_prepod AS[Код преподаватель] FROM MeroprPrepod INNER JOIN VidMetod ON MeroprPrepod.kod_vidmetod = VidMetod.kod_vidmetod INNER JOIN Predsedatelya ON MeroprPrepod.kod_prepod = Predsedatelya.kod_prepod";
        string Predsedatelya = "SELECT Predsedatelya.kod_prepod AS [Код председателя], Predsedatelya.fioprepod AS ФИО, Predsedatelya.dolgnost AS Должность," +
            " Predsedatelya.datapriem AS [Дата приема], Predsedatelya.datauvol AS [Дата увольнения], Predsedatelya.kod_pck AS[Код ПЦК], " +
            "Predsedatelya.dataobuch AS[Дата последнего обучения]FROM Predsedatelya INNER JOIN PCK ON Predsedatelya.kod_pck = PCK.kod_pck";

        private void FMaincs_Load(object sender, EventArgs e)
        {

        }
        public void conn(string CS, string cmdT, DataGridView dgv)
        {
            //создание экземпляра адаптера

            SqlDataAdapter Adapter = new SqlDataAdapter(cmdT, CS);
            // сздание обьекта  DataSet (набор данных)
            DataSet ds = new DataSet();
            // Заполнение таблицы набора данных DataSet
            Adapter.Fill(ds, "Table");
            // Связыаем источник данных компонета dataGridView на форме, с таблицей
            dgv.DataSource = ds.Tables["Table"].DefaultView;
        }


        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //Новое подключение
            SqlConnection connect = new SqlConnection();
            connect.ConnectionString = ConnectionString;
            //Теперь можно устанавливать соединение, вызыва метод Open обьекта
            connect.Open();
            //Создаем новый экземпляр SQLCommand
            SqlCommand cmd = connect.CreateCommand();
            //Определяем тип SQLCommand=StoredProcedure
            cmd.CommandType = CommandType.StoredProcedure;
            //определяем имя вызывемой процедуры
            cmd.CommandText = "[T1]";
            //Создаем параметр
            //аналогично для все остальных параметров
            cmd.Parameters.Add("@namepck", SqlDbType.Char, 100);
            cmd.Parameters["@namepck"].Value = textBox1.Text;

            cmd.Parameters.Add("@predsedpck", SqlDbType.Char, 100);
            cmd.Parameters["@predsedpck"].Value = textBox2.Text;

            //Выполнение хранимой процедуры-добавление записи
            cmd.ExecuteNonQuery();
            //вывод сообщения
            MessageBox.Show("Запись измена!");
            //обновление записей в таблице в daataGridview
            conn(ConnectionString, PCK, dataGridView1);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {
            //Новое подключение
            SqlConnection connect = new SqlConnection();
            connect.ConnectionString = ConnectionString;
            //Теперь можно устанавливать соединение, вызыва метод Open обьекта
            connect.Open();
            //Создаем новый экземпляр SQLCommand
            SqlCommand cmd = connect.CreateCommand();
            //Определяем тип SQLCommand=StoredProcedure
            cmd.CommandType = CommandType.StoredProcedure;
            //определяем имя вызывемой процедуры
            cmd.CommandText = "[T3]";
            //Создаем параметр
            //аналогично для все остальных параметров
            cmd.Parameters.Add("@namevidmetod", SqlDbType.Char, 100);
            cmd.Parameters["@namevidmetod"].Value = textBox3.Text;
            //Выполнение хранимой процедуры-добавление записи
            cmd.ExecuteNonQuery();
            //вывод сообщения
            MessageBox.Show("Запись измена!");
            //обновление записей в таблице в daataGridview
            conn(ConnectionString, VidMetod, dataGridView2);
        }

        public void conn2(string CS, string cmdT, ComboBox CB, string field1, string field2)
        {
            //создание экземпляра адаптера
            SqlDataAdapter adapter = new SqlDataAdapter(cmdT, CS);
            //создание обекта DataSet (набор данных)
            DataSet ds = new DataSet();
            adapter.Fill(ds, "Table");
            //привязка comboBox к таблице БД
            CB.DataSource = ds.Tables["Table"];

            CB.DisplayMember = field1; //установка отбражаемого в списке поля
             CB.ValueMember = field2; //установка клчевого поля
        }

        private void button12_Click(object sender, EventArgs e)
        {

            //Новое подключение
            SqlConnection connect = new SqlConnection();
            connect.ConnectionString = ConnectionString;
            //Теперь можно устанавливать соединение, вызыва метод Open обьекта
            connect.Open();
            //Создаем новый экземпляр SQLCommand
            SqlCommand cmd = connect.CreateCommand();
            //Определяем тип SQLCommand=StoredProcedure
            cmd.CommandType = CommandType.StoredProcedure;
            //определяем имя вызывемой процедуры
            cmd.CommandText = "[T4]";
            //Создаем параметр
            //аналогично для все остальных параметров
            cmd.Parameters.Add("@namemeropr", SqlDbType.Char, 100);
            cmd.Parameters["@namemeropr"].Value = textBox5.Text;

            cmd.Parameters.Add("@kod_vidmetod", SqlDbType.Int);
            cmd.Parameters["@kod_vidmetod"].Value = comboBox3.SelectedValue;

            cmd.Parameters.Add("@uroven", SqlDbType.Char, 50);
            cmd.Parameters["@uroven"].Value = textBox8.Text;

            cmd.Parameters.Add("@result", SqlDbType.Char, 50);
            cmd.Parameters["@result"].Value = textBox9.Text;

            cmd.Parameters.Add("@dokum", SqlDbType.Char, 50);
            cmd.Parameters["@dokum"].Value = textBox10.Text;

            cmd.Parameters.Add("@mesto", SqlDbType.Char, 100);
            cmd.Parameters["@mesto"].Value = textBox11.Text;

            cmd.Parameters.Add("@dataprov", SqlDbType.Date);
            cmd.Parameters["@dataprov"].Value = dateTimePicker1.Value;

            cmd.Parameters.Add("@kod_prepod", SqlDbType.Int);
            cmd.Parameters["@kod_prepod"].Value = comboBox1.SelectedValue;

            //Выполнение хранимой процедуры-добавление записи
            cmd.ExecuteNonQuery();
            //вывод сообщения
            MessageBox.Show("Запись изменеа!");
            //обновление записей в таблице в daataGridview
            conn(ConnectionString, MeroprPrepod, dataGridView3);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            //Новое подключение
            SqlConnection connect = new SqlConnection();
            connect.ConnectionString = ConnectionString;
            //Теперь можно устанавливать соединение, вызыва метод Open обьекта
            connect.Open();
            //Создаем новый экземпляр SQLCommand
            SqlCommand cmd = connect.CreateCommand();
            //Определяем тип SQLCommand=StoredProcedure
            cmd.CommandType = CommandType.StoredProcedure;
            //определяем имя вызывемой процедуры
            cmd.CommandText = "[T2]";
            //Создаем параметр
            //аналогично для все остальных параметров
            cmd.Parameters.Add("@fioprepod", SqlDbType.Char, 100);
            cmd.Parameters["@fioprepod"].Value = textBox6.Text;

            cmd.Parameters.Add("@dolgnost", SqlDbType.Char, 100);
            cmd.Parameters["@dolgnost"].Value = textBox7.Text;

            cmd.Parameters.Add("@datapriem", SqlDbType.Date);
            cmd.Parameters["@datapriem"].Value = dateTimePicker2.Value;

            cmd.Parameters.Add("@datauvol", SqlDbType.Date);
            cmd.Parameters["@datauvol"].Value = dateTimePicker3.Value;

            cmd.Parameters.Add("@kod_pck", SqlDbType.Int);
            cmd.Parameters["@kod_pck"].Value = comboBox2.SelectedValue;

            cmd.Parameters.Add("@dataobuch", SqlDbType.Date);
            cmd.Parameters["@dataobuch"].Value = dateTimePicker4.Value;

            //Выполнение хранимой процедуры-добавление записи
            cmd.ExecuteNonQuery();
            //вывод сообщения
            MessageBox.Show("Запись измена!");
            //обновление записей в таблице в daataGridview
            conn(ConnectionString, Predsedatelya, dataGridView4);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
    }
}

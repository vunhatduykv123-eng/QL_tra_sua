using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyQuanTraSua
{
    public partial class UserMenuForm : Form
    {
        private SqlConnection conn;
        private SqlDataAdapter adapter;
        private DataSet dSetExam;
        private string sqlstr;

        public UserMenuForm()
        {
            InitializeComponent();
        }

        private void UserMenuForm_Load(object sender, EventArgs e)
        {
            try
            {
                conn = new SqlConnection("Server=LAPTOP-I6EBBTME;Initial Catalog=QLQTraSua;User Id=sa;pwd=Duy200666.;");
                conn.Open();

                sqlstr = "SELECT TenMon AS [Tên Trà Sữa], Gia AS [Giá Tiền] FROM MonChinh";

                adapter = new SqlDataAdapter(sqlstr, conn);
                dSetExam = new DataSet("ExamResults");
                adapter.Fill(dSetExam, "ExamResults");

                dgvUserMenu.DataSource = dSetExam.Tables[0];
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
            }
            finally
            {
                if (conn != null) conn.Close();
            }
        }

        private void dgvUserMenu_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
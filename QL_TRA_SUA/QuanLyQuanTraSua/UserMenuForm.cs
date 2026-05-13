using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyQuanTraSua
{
    public partial class UserMenuForm : Form
    {
        // Duy dùng chung chuỗi kết nối này nhé
        string connStr = @"Data Source=LAPTOP-I6EBBTME;Initial Catalog=QuanLyTraSua;Integrated Security=True";

        public UserMenuForm()
        {
            InitializeComponent();
        }

        // Hàm này giúp khách hàng thấy danh sách món ngay khi mở app
        private void UserMenuForm_Load(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    // Chỉ lấy Tên và Giá cho khách xem thôi, không cần lấy Mã
                    string sql = "SELECT TenMon AS [Tên Trà Sữa], Gia AS [Giá Tiền] FROM MonChinh";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvUserMenu.DataSource = dt; // Đài nhớ đặt tên DataGridView là dgvUserMenu nhé
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hiển thị Menu: " + ex.Message);
                }
            }
        }
    }
}
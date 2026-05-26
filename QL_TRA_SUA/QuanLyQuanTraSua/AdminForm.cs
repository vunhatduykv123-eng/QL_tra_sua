using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyQuanTraSua
{
    public partial class AdminForm : Form
    {
        private SqlConnection conn;
        private SqlDataAdapter adapter;
        private DataSet dSetExam;
        private string sqlstr;

        // BIẾN THẦY DẠY: Tạo một biến toàn cục để lưu lại Mã món đang được chọn
        private int maDangChon = -1;

        public AdminForm()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            try
            {
                conn = new SqlConnection("Data Source=LAPTOP-I6EBBTME;Initial Catalog=QLQTraSua;User ID=sa;Password=Duy200666.");
                sqlstr = "SELECT MaMon AS [Mã], TenMon AS [Tên Món Trà Sữa], Gia AS [Giá Tiền] FROM MonChinh";
                adapter = new SqlDataAdapter(sqlstr, conn);
                dSetExam = new DataSet("TSResults");
                adapter.Fill(dSetExam, "TSResults");
                dgvMenu.DataSource = dSetExam.Tables[0];
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
            }
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        // Sự kiện click dòng trên bảng để lấy dữ liệu
        private void dgvMenu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvMenu.Rows[e.RowIndex];
                if (row.Cells["Mã"].Value != DBNull.Value)
                {
                    // Khóa chặt mã số món trà sữa vào biến toàn cục
                    maDangChon = Convert.ToInt32(row.Cells["Mã"].Value);

                    txtTenMon.Text = row.Cells["Tên Món Trà Sữa"].Value.ToString();
                    txtGia.Text = row.Cells["Giá Tiền"].Value.ToString();
                }
            }
        }

        // Nhấn nút Update - Sửa dữ liệu dựa theo Mã món (Chuẩn xác 100% không lo bị nhảy lại tên cũ)
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (maDangChon == -1)
            {
                MessageBox.Show("Vui lòng click chọn một món trà sữa trong bảng trước khi sửa!");
                return;
            }

            try
            {
                conn = new SqlConnection("Data Source=LAPTOP-I6EBBTME;Initial Catalog=QLQTraSua;User ID=sa;Password=Duy200666.");
                conn.Open();

                string tenMoi = txtTenMon.Text.Trim();
                string giaMoi = txtGia.Text.Trim();

                // CẤU TRÚC CHUẨN CỦA THẦY: Update dựa theo khóa chính MaMon
                string sql = "UPDATE MonChinh SET TenMon=N'" + tenMoi + "', Gia=" + giaMoi + " WHERE MaMon=" + maDangChon;

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.ExecuteNonQuery();

                MessageBox.Show("Updated");
                conn.Close();

                maDangChon = -1; // Reset lại biến chọn
                txtTenMon.Clear();
                txtGia.Clear();
                LoadData(); // Nạp lại bảng dữ liệu thật từ SQL Server
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message);
            }
        }

        // Nhấn nút Add - Thêm món mới 
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenMon.Text.Trim()) || string.IsNullOrEmpty(txtGia.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tên món và Giá tiền trước khi bấm Thêm!");
                return;
            }

            conn = new SqlConnection("Data Source=LAPTOP-I6EBBTME;Initial Catalog=QLQTraSua;User ID=sa;Password=Duy200666.");
            conn.Open();

            string name = txtTenMon.Text;
            string price = txtGia.Text;

            string sql = "INSERT into MonChinh values(N'" + name + "', " + price + ")";

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Added");
            conn.Close();
            LoadData();
        }

        // Nhấn nút Delete - Xóa món theo Mã món được chọn
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (maDangChon == -1)
            {
                MessageBox.Show("Vui lòng click chọn món cần xóa trên bảng dữ liệu!");
                return;
            }

            conn = new SqlConnection("Data Source=LAPTOP-I6EBBTME;Initial Catalog=QLQTraSua;User ID=sa;Password=Duy200666.");
            conn.Open();

            string sql = "DELETE from MonChinh WHERE MaMon=" + maDangChon;

            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();

            MessageBox.Show("Deleted");
            conn.Close();

            maDangChon = -1;
            txtTenMon.Clear();
            txtGia.Clear();
            LoadData();
        }

        // Nhấn nút Reload - Làm mới lại toàn bộ danh sách bản ghi
        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }
}
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyQuanTraSua
{
    /*Tạo ra một lớp giao diện công khai tên là Form1, lớp này được chia làm
    nhiều file cấu phần (partial) và được kế thừa (:)
    toàn bộ tính năng của một cái cửa sổ Windows tiêu chuẩn (Form)"*/
    public partial class Form1 : Form  
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            bool OK = false;
            string vaiTro = ""; // Biến dùng để hứng chữ 'admin' hoặc 'khach' từ SQL Server lên

            SqlConnection conn = new SqlConnection("Data Source=LAPTOP-I6EBBTME;Initial Catalog=QLQTraSua;User ID=sa;Password=Duy200666.");
            SqlDataReader rdr = null;

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from Nguoidung", conn);
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    // Kiểm tra trùng khớp Tài khoản và Mật khẩu
                    if ((txtUsername.Text.Trim() == rdr["Tendangnhap"].ToString().Trim()) &&
                        (txtPassword.Text.Trim() == rdr["Matkhau"].ToString().Trim()))
                    {
                        OK = true;
                        // Lấy giá trị của cột Quyen dưới SQL gán vào biến vaiTro (ép về chữ thường để tránh gõ hoa thường)
                        vaiTro = rdr["Quyen"].ToString().Trim().ToLower();
                        break;
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi kết nối CSDL!");
                return;
            }
            finally
            {
                if (rdr != null) rdr.Close();
                if (conn != null) conn.Close();
            }

            if (OK == false)
            {
                MessageBox.Show("Tên đăng nhập/Mật khẩu không hợp lệ!");
            }
            else
            {
                MessageBox.Show("Đăng nhập thành công!");

                // --- ĐOẠN PHÂN QUYỀN THEO CÁCH 2 CỦA THẦY ---
                if (vaiTro == "admin")
                {
                    // Nếu cột Quyen dưới CSDL ghi là 'admin' -> Vào AdminForm
                    AdminForm frmAdmin = new AdminForm();
                    this.Hide();
                    frmAdmin.ShowDialog();
                    this.Show();
                }
                else
                {
                    // Nếu cột Quyen dưới CSDL ghi là 'khach' (hoặc bất kỳ chữ nào khác) -> Vào UserMenuForm
                    UserMenuForm frmUser = new UserMenuForm();
                    this.Hide();
                    frmUser.ShowDialog();
                    this.Show();
                }
            }
        }

        private void btnGoToRegister_Click(object sender, EventArgs e)
        {
            FormRegister frmRegister = new FormRegister();
            this.Hide();
            frmRegister.ShowDialog();
            this.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
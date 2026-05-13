using System;
using System.Data.SqlClient; // Thư viện để kết nối SQL Server
using System.Windows.Forms;

namespace QuanLyQuanTraSua
{
    public partial class FormRegister : Form
    {
        // Chuỗi kết nối đến máy LAPTOP-I6EBBTME của Duy
        // Duy kiểm tra kỹ phần Initial Catalog phải là QuanLyTraSua nhé
        string connStr = @"Data Source=LAPTOP-I6EBBTME;Initial Catalog=QuanLyTraSua;Integrated Security=True";

        public FormRegister()
        {
            InitializeComponent();
        }

        // Sự kiện khi nhấn nút ĐĂNG KÝ
        private void btnRegister_Click(object sender, EventArgs e)
        {
            // 1. Lấy dữ liệu và xóa khoảng trắng dư thừa
            string user = txtUser.Text.Trim();
            string pass = txtPass.Text.Trim();
            string name = txtFullName.Text.Trim();

            // 2. Kiểm tra nhập liệu cơ bản
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Duy và Đài nhớ nhập đủ Tài khoản và Mật khẩu nhé!", "Thông báo");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();

                    // BƯỚC 3: KIỂM TRA TÀI KHOẢN ĐÃ TỒN TẠI CHƯA
                    string checkSql = "SELECT COUNT(*) FROM users WHERE username = @user";
                    SqlCommand checkCmd = new SqlCommand(checkSql, conn);
                    checkCmd.Parameters.AddWithValue("@user", user);
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Tài khoản '" + user + "' đã có người dùng rồi. Chọn tên khác nha!", "Lỗi");
                        return;
                    }

                    // BƯỚC 4: THỰC HIỆN ĐĂNG KÝ (INSERT)
                    // Lưu ý: role mặc định là 'user' cho khách hàng mới
                    string insertSql = "INSERT INTO users (username, password, full_name, role) " +
                                     "VALUES (@user, @pass, @name, 'user')";

                    SqlCommand cmd = new SqlCommand(insertSql, conn);
                    cmd.Parameters.AddWithValue("@user", user);
                    cmd.Parameters.AddWithValue("@pass", pass);
                    cmd.Parameters.AddWithValue("@name", name);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Đăng ký thành công!", "Chúc mừng");
                        this.Close(); // Đóng form đăng ký để quay lại đăng nhập
                    }
                }
                catch (Exception ex)
                {
                    // Hiện thông báo lỗi nếu có vấn đề về Database
                    MessageBox.Show("Lỗi hệ thống: " + ex.Message, "Lỗi kết nối");
                }
            }
        }

        // Sự kiện khi nhấn nút QUAY LẠI
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close(); // Đóng form này lại
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
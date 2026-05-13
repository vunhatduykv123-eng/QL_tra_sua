using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyQuanTraSua
{
    public partial class Form1 : Form
    {
        // Chuỗi kết nối đến Database QuanLyTraSua
        string connStr = @"Data Source=LAPTOP-I6EBBTME;Initial Catalog=QuanLyTraSua;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string user = txtUser.Text.Trim();
                    string pass = txtPass.Text.Trim();

                    // Lấy cột 'role' từ database dựa trên tài khoản/mật khẩu
                    string sql = "SELECT role FROM users WHERE username=@user AND password=@pass";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@user", user);
                    cmd.Parameters.AddWithValue("@pass", pass);

                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        string role = result.ToString().Trim().ToLower();
                        MessageBox.Show("Đăng nhập thành công!", "Thông báo");

                        this.Hide(); // Ẩn Form đăng nhập

                        // ĐIỀU HƯỚNG DỰA TRÊN QUYỀN
                        if (role == "admin")
                        {
                            // Nếu là Admin thì mở trang Quản lý của Duy và Đài
                            AdminForm fAdmin = new AdminForm();
                            fAdmin.ShowDialog();
                        }
                        else
                        {
                            // Nếu là User/Nhân viên thì mở trang Menu xem món vừa tạo
                            UserMenuForm fUser = new UserMenuForm();
                            fUser.ShowDialog();
                        }

                        this.Show(); // Hiện lại Form đăng nhập sau khi đóng các Form kia
                    }
                    else
                    {
                        MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối: " + ex.Message);
                }
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            FormRegister f = new FormRegister();
            f.ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
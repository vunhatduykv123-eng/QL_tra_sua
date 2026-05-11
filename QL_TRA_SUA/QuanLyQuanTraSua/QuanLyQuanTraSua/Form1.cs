using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyQuanTraSua
{
    public partial class Form1 : Form
    {
        // Chuỗi kết nối dùng chung cho cả Form
        string connStr = @"Data Source=LAPTOP-I6EBBTME;Initial Catalog=QL_TraSua;Integrated Security=True";

        public Form1()
        {
            InitializeComponent();
        }

        // NÚT ĐĂNG NHẬP (btnLogin hoặc button1)
        private void btnLogin_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string user = txtUser.Text.Trim();
                    string pass = txtPass.Text.Trim();

                    // Thay đổi: Lấy cột 'role' thay vì COUNT
                    string sql = "SELECT role FROM users WHERE username=@user AND password=@pass";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@user", user);
                    cmd.Parameters.AddWithValue("@pass", pass);

                    // ExecuteScalar lấy giá trị của cột role
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        string role = result.ToString().Trim().ToLower();
                        MessageBox.Show("Đăng nhập thành công!", "Thông báo");

                        this.Hide(); // Ẩn Form đăng nhập

                        // KIỂM TRA QUYỀN ĐỂ MỞ FORM TƯƠNG ỨNG
                        if (role == "admin")
                        {
                            AdminForm fAdmin = new AdminForm();
                            fAdmin.ShowDialog();
                        }
                        else
                        {
                            // Sau này Đài thiết kế xong Form bán hàng thì mở ở đây
                            // UserForm fUser = new UserForm();
                            // fUser.ShowDialog();
                            MessageBox.Show("Chào nhân viên/khách hàng! Form bán hàng đang được cập nhật.");
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

        // NÚT MỞ FORM ĐĂNG KÝ
        private void btnRegister_Click(object sender, EventArgs e)
        {
            FormRegister f = new FormRegister();
            f.ShowDialog();
        }
    }
}
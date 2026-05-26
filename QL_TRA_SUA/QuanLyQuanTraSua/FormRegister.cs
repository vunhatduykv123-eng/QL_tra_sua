using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyQuanTraSua
{
    public partial class FormRegister : Form
    {
        public FormRegister()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUser.Text.Trim()) || string.IsNullOrEmpty(txtPass.Text.Trim()) || string.IsNullOrEmpty(txtRePass.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin đăng ký!");
                return;
            }

            if (txtPass.Text.Trim() != txtRePass.Text.Trim())
            {
                MessageBox.Show("Mật khẩu nhập lại không trùng khớp!");
                return;
            }

            using (SqlConnection conn = new SqlConnection("Data Source=LAPTOP-I6EBBTME;Initial Catalog=QLQTraSua;User ID=sa;Password=Duy200666."))
            {
                try
                {
                    conn.Open();
                    string checkSql = "SELECT COUNT(*) FROM Nguoidung WHERE Tendangnhap = @user";
                    SqlCommand checkCmd = new SqlCommand(checkSql, conn);
                    checkCmd.Parameters.AddWithValue("@user", txtUser.Text.Trim());
                    int count = (int)checkCmd.ExecuteScalar();

                    if (count > 0)
                    {
                        MessageBox.Show("Tài khoản này đã tồn tại trên hệ thống quán!");
                        return;
                    }

                    // --- CẬP NHẬT THÊM CỘT QUYEN THEO CÁCH 2 ---
                    string insertSql = "INSERT INTO Nguoidung (Tendangnhap, Matkhau, Quyen) VALUES (@user, @pass, @quyen)";
                    SqlCommand insertCmd = new SqlCommand(insertSql, conn);
                    insertCmd.Parameters.AddWithValue("@user", txtUser.Text.Trim());
                    insertCmd.Parameters.AddWithValue("@pass", txtPass.Text.Trim());
                    insertCmd.Parameters.AddWithValue("@quyen", "khach"); // Tự động gán quyền 'khach' cho tài khoản mới

                    insertCmd.ExecuteNonQuery();
                    MessageBox.Show("Đăng ký tài khoản quán trà sữa thành công!");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi đăng ký tài khoản: " + ex.Message);
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormRegister_Load(object sender, EventArgs e)
        {

        }
    }
}
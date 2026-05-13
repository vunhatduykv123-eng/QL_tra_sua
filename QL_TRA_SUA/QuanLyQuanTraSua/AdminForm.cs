using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyQuanTraSua
{
    public partial class AdminForm : Form
    {
        int maChon = -1;
        string connStr = @"Data Source=LAPTOP-I6EBBTME;Initial Catalog=QuanLyTraSua;Integrated Security=True";

        public AdminForm()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    // Lưu ý: Tên cột hiển thị là [Mã], [Tên Món], [Giá Tiền]
                    string sql = "SELECT MaMon AS [Mã], TenMon AS [Tên Món], Gia AS [Giá Tiền] FROM MonChinh";
                    SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvMenu.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải danh sách: " + ex.Message);
                }
            }
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenMon.Text) || string.IsNullOrEmpty(txtGia.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên món và giá!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string sql = "INSERT INTO MonChinh (TenMon, Gia) VALUES (@ten, @gia)";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ten", txtTenMon.Text.Trim());
                    cmd.Parameters.AddWithValue("@gia", txtGia.Text.Trim());

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã thêm món mới thành công!");
                    LoadData();

                    txtTenMon.Clear();
                    txtGia.Clear();
                    txtTenMon.Focus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm: " + ex.Message);
                }
            }
        }

        // Cập nhật logic xử lý khi chọn dòng trong bảng
        private void dgvMenu_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && !dgvMenu.Rows[e.RowIndex].IsNewRow)
            {
                DataGridViewRow row = dgvMenu.Rows[e.RowIndex];

                if (row.Cells["Mã"].Value != DBNull.Value)
                {
                    maChon = Convert.ToInt32(row.Cells["Mã"].Value);
                    txtTenMon.Text = row.Cells["Tên Món"].Value.ToString();

                    // Sửa lỗi: đổi "Giá" thành "Giá Tiền" cho khớp với LoadData
                    txtGia.Text = row.Cells["Giá Tiền"].Value.ToString();
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (maChon == -1)
            {
                MessageBox.Show("Vui lòng chọn một món trong bảng trước khi bấm Sửa!");
                return;
            }

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string sql = "UPDATE MonChinh SET TenMon=@ten, Gia=@gia WHERE MaMon=@id";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@ten", txtTenMon.Text.Trim());
                    cmd.Parameters.AddWithValue("@gia", txtGia.Text.Trim());
                    cmd.Parameters.AddWithValue("@id", maChon);

                    // Đoạn code mới để kiểm tra xem có sửa được dòng nào không:
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Đã sửa thành công " + rowsAffected + " món!");
                        LoadData(); // Cập nhật lại bảng
                    }
                    else
                    {
                        MessageBox.Show("Mã món là: " + maChon + ". Cập nhật chạy xong nhưng không có món nào bị đổi!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi sửa món: " + ex.Message);
                }
            }
        }
        // Logic cho nút XÓA MÓN
        private void button2_Click(object sender, EventArgs e)
        {
            if (maChon == -1)
            {
                MessageBox.Show("Vui lòng chọn món cần xóa trong danh sách!");
                return;
            }

            DialogResult dr = MessageBox.Show("Bạn có chắc chắn muốn xóa món này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (dr == DialogResult.Yes)
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    try
                    {
                        conn.Open();
                        string sql = "DELETE FROM MonChinh WHERE MaMon=@id";
                        SqlCommand cmd = new SqlCommand(sql, conn);
                        cmd.Parameters.AddWithValue("@id", maChon);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Đã xóa món thành công!");

                        // Reset lại trạng thái sau khi xóa
                        maChon = -1;
                        txtTenMon.Clear();
                        txtGia.Clear();
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xóa: " + ex.Message);
                    }
                }
            }
        }
    }
}
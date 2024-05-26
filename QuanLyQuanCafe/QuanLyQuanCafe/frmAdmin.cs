using QuanLyQuanCafe.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class frmAdmin : Form
    {
        public frmAdmin(String user)
        {
            InitializeComponent();
            LoadFood();
            LoadCate();
            LoadDinnerTable();
            LoadAccount();
            this.txtUser_Save.Text = user;
        }
        #region Load
        void LoadFood()
        {
            String sql = "select * from food";
            cbbFoodCategory.Items.Clear();
            dgvFood.DataSource = TruyXuatCSDL.LayBang(sql);
            dgvFood.Columns[0].HeaderText = "ID";
            dgvFood.Columns[1].HeaderText = "Tên món ăn";
            dgvFood.Columns[2].HeaderText = "ID danh mục";
            dgvFood.Columns[3].HeaderText = "Giá";
            sql = "select cat_name from category";
            DataTable dataTable = TruyXuatCSDL.LayBang(sql);
            DataRow row = null;
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                row = dataTable.Rows[i];
                cbbFoodCategory.Items.Add(row[0].ToString());
            }

        }
        void LoadCate()
        {
            String sql = "select * from category";
            dgvCategory.DataSource = TruyXuatCSDL.LayBang(sql);
            dgvCategory.Columns[0].HeaderText = "ID";
            dgvCategory.Columns[1].HeaderText = "Tên danh mục";

        }
        void LoadDinnerTable()
        {
            cbbDinnerTableStatus.Items.Clear();
            String sql = "select * from dinner_table";
            dgvDinnerTable.DataSource = TruyXuatCSDL.LayBang(sql);
            dgvDinnerTable.Columns[0].HeaderText= "ID";
            dgvDinnerTable.Columns[1].HeaderText = "Tên bàn";
            dgvDinnerTable.Columns[2].HeaderText = "Trạng thái";
            cbbDinnerTableStatus.Items.Add("Trống");
            cbbDinnerTableStatus.Items.Add("Có người");

        }
        void LoadAccount()
        {
            cbbAccountType.Items.Clear();
            String sql = "select * from login";
            dgvAccount.DataSource = TruyXuatCSDL.LayBang(sql);
            dgvAccount.Columns[0].HeaderText = "Tài khoản";
            dgvAccount.Columns[1].HeaderText = "Mật khẩu";
            dgvAccount.Columns[2].HeaderText = "Họ tên";
            dgvAccount.Columns[3].HeaderText = "Quyền hạn";
            cbbAccountType.Items.Add("admin");
            cbbAccountType.Items.Add("staff");

        }
        void loadListBillByDate(DateTime checkin , DateTime checkout)
        {
           dgvBill.DataSource =  BillDAO.Instance.GetBillByDate(checkin, checkout);
        }
        #endregion
        
        #region Food
        private void dgvFood_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvFood.CurrentRow != null)
            {
                txtFoodID.Text = dgvFood.CurrentRow.Cells[0].Value.ToString();
                txtFood_name.Text = dgvFood.CurrentRow.Cells[1].Value.ToString();
                nudFoodPrice.Value = int.Parse(dgvFood.CurrentRow.Cells[3].Value.ToString());
                String cat_id = dgvFood.CurrentRow.Cells[2].Value.ToString();
                int id = int.Parse(cat_id);
                String sql = "select cat_name from category where id = '" + id + "'";
                DataTable data = TruyXuatCSDL.LayBang(sql);
                DataRow row = data.Rows[0];
                String Check_name = row[0].ToString();
                foreach (String name in cbbFoodCategory.Items)
                {
                    if (Check_name.Equals(name))
                    {
                        cbbFoodCategory.SelectedItem = name;
                    }
                }
            }
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            txtFoodID.Text = "";
            int cat_id;
            String cat_names = cbbFoodCategory.SelectedItem.ToString();
            // MessageBox.Show(cat_names);
            String sql = "select * from category where cat_name = N'" + cat_names + "'";
            DataTable data = TruyXuatCSDL.LayBang(sql);
            if (data.Rows.Count == 0)
            {
                MessageBox.Show("SQL lỗi");
            }
            else
            {
                cat_id = int.Parse(data.Rows[0]["id"].ToString());
                sql = "insert into food (food_name,category_id,price) values (N'" + txtFood_name.Text
                    + "','" + cat_id + "','" + float.Parse(nudFoodPrice.Value.ToString()) + "')";
                TruyXuatCSDL.ThemSuaXoa(sql);
                LoadFood();
            }

        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            String sql = "Delete from food where id = '" + int.Parse(txtFoodID.Text) + "'";
            TruyXuatCSDL.ThemSuaXoa(sql);
            LoadFood();
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            int cat_id;
            String cat_name = cbbFoodCategory.Text;
            String sql = "select id from category where cat_name = N'" + cat_name + "'";
            DataTable data = TruyXuatCSDL.LayBang(sql);
            DataRow row = data.Rows[0];
            cat_id = int.Parse(row[0].ToString());
            //MessageBox.Show(txtFood_name.Text + " " + cat_id + " " + float.Parse(nudFoodPrice.Value.ToString()) + " " + int.Parse(txtFoodID.Text));
            sql =
                "update food set food_name = N'" + txtFood_name.Text + "', category_id = '"
                + cat_id + "', price = '" + float.Parse(nudFoodPrice.Value.ToString()) + "'" +
                " where id = '" + int.Parse(txtFoodID.Text) + "'";
            TruyXuatCSDL.ThemSuaXoa(sql);
            LoadFood();
        }
        private void btnSearchFood_Click(object sender, EventArgs e)
        {
            if (txtSearchFood.Text.Length > 0)
            {
                String sql = "Select * from food where food_name LIKE '% " + txtSearchFood.Text + "%'";
                dgvFood.DataSource = TruyXuatCSDL.LayBang(sql);
                if (dgvFood.Rows.Count == 1)
                {
                    MessageBox.Show("Không có dữ liệu cần tìm kiếm!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtSearchFood.Focus();
                    txtSearchFood.SelectAll();
                }
            }
            else
            {
                MessageBox.Show("Vui lòng nhập thông tin vào ô tìm kiếm!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSearchFood.Focus();
                txtSearchFood.SelectAll();

            }
        }
        #endregion

        #region Category
        private void dgvCategory_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCategory.CurrentRow != null)
            {
                txtCategoryID.Text = dgvCategory.CurrentRow.Cells[0].Value.ToString();
                txtCategoryName.Text = dgvCategory.CurrentRow.Cells[1].Value.ToString();
            }
        }

        private void btnAddCategory_Click(object sender, EventArgs e)
        {
            txtCategoryID.Text = "";
            String sql = "insert into category (cat_name) values (N'" + txtCategoryName.Text + "')";
            TruyXuatCSDL.ThemSuaXoa(sql);
            LoadCate();
        }
        

        private void btnEditCategory_Click(object sender, EventArgs e)
        {
            String sql = "update category set cat_name = N'" + txtCategoryName.Text + "' where id = '"+ txtCategoryID.Text+"'";
            TruyXuatCSDL.ThemSuaXoa(sql);
            LoadCate();
        }

        private void btnDeleteCateogory_Click(object sender, EventArgs e)
        {
            String sql = "delete from category where id = '" + txtCategoryID.Text + "'";
            TruyXuatCSDL.ThemSuaXoa(sql); 
            LoadCate();
        }
        #endregion

        #region Table

       

        private void btnAddDinnerTable_Click(object sender, EventArgs e)
        {
            txtDinnerTableID.Text = "";
            String sql = 
                "insert into dinner_table (dinner_table_name , status) " +
                "values (N'" + txtDinnerTableName.Text + "',N'" + cbbDinnerTableStatus.Text + "') ";
            TruyXuatCSDL.ThemSuaXoa(sql); 
            LoadDinnerTable();

        }

        private void btnEditDinnerTable_Click(object sender, EventArgs e)
        {
            String status_table;
            status_table = cbbDinnerTableStatus.Text;
            int id = int.Parse(txtDinnerTableID.Text); 
            //MessageBox.Show(status_table);
            String sql = "update dinner_table set dinner_table_name = N'" + txtDinnerTableName.Text
                + "', status = N'" + status_table + " ' where id = '" + id + "'";
            TruyXuatCSDL.ThemSuaXoa(sql);
            LoadDinnerTable();
        }

        private void dgvDinnerTable_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtDinnerTableID.Text = dgvDinnerTable.CurrentRow.Cells[0].Value.ToString();
            txtDinnerTableName.Text = dgvDinnerTable.CurrentRow.Cells[1].Value.ToString();
            cbbDinnerTableStatus.Text = dgvDinnerTable.CurrentRow.Cells[2].Value.ToString();

        }

        private void btnDeleteDinnerTable_Click(object sender, EventArgs e)
        {
            String sql = "delete from dinner_table where id = '" + txtDinnerTableID.Text + "'";
            TruyXuatCSDL.ThemSuaXoa(sql); 
            LoadDinnerTable();
        }
        #endregion

        #region Account
        private void dgvAccount_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtUsername.Text = dgvAccount.CurrentRow.Cells[0].Value.ToString();
            txtFullname.Text = dgvAccount.CurrentRow.Cells[2].Value.ToString();
            cbbAccountType.Text = dgvAccount.CurrentRow.Cells[3].Value.ToString();
        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            String sql = "insert into login (username,password,name,role) " +
                "values ('" + txtUsername.Text + "','123',N'" + txtFullname.Text + "','" + cbbAccountType.Text + "')";
            TruyXuatCSDL.ThemSuaXoa(sql);
            LoadAccount();
        }

        private void btnEditAccount_Click(object sender, EventArgs e)
        {
            String sql = "update login set username = '" + txtUsername.Text + "', name = N'" + txtFullname.Text
                + "', role = '" + cbbAccountType.Text + "' where username = '" + txtUser_Save.Text + "'";
            TruyXuatCSDL.ThemSuaXoa(sql);
            LoadAccount();
        }

        private void btnDeleteAccount_Click(object sender, EventArgs e)
        {
            String sql = "delete from login where username = '" + txtUsername.Text + "'";
            TruyXuatCSDL.ThemSuaXoa(sql);
            LoadAccount();
        }

        private void btnResetPass_Click(object sender, EventArgs e)
        {
            String sql = "update login set password = '123' where username = '" + txtUser_Save.Text + "'";
            TruyXuatCSDL.ThemSuaXoa(sql);
            LoadAccount();
        }
        #endregion

        #region Bill
        private void btnViewBill_Click(object sender, EventArgs e)
        {
            loadListBillByDate(dtpDateStart.Value, dtpDateEnd.Value);
            dgvBill.Columns[0].HeaderText = "Tên bàn";
            dgvBill.Columns[1].HeaderText = "Tổng bill (.000đ)";
            dgvBill.Columns[2].HeaderText = "Ngày vào";
            dgvBill.Columns[3].HeaderText = "Ngày ra";
            dgvBill.Columns[4].HeaderText = "Giảm giá (%)";

        }
        #endregion

        private void frmAdmin_Load(object sender, EventArgs e)
        {

        }

        private void dtpDateStart_ValueChanged(object sender, EventArgs e)
        {

        }
    }

}

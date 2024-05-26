using QuanLyQuanCafe.DAO;
using QuanLyQuanCafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QuanLyQuanCafe
{
    public partial class frmManager : Form
    {
        public frmManager()
        {
            InitializeComponent();
        }
        public frmManager(String username)
        {
            InitializeComponent();
            this.txtSaveUserName.Text = username;
            LoadDinnerTable();
            LoadCategory();
        }
        #region Method

        //Hàm load ra Danh mục đồ ăn thức uống
        public void LoadCategory()
        {
            List<CategoryDTO> listCategory = CategoryDAO.Instance.GetListCategory();
            cbbCategory.DataSource = listCategory;
            cbbCategory.DisplayMember = "CatName";
        }

        //Hàm load ra Danh sách món ăn, đồ uống
        public void LoadFoodListByCategoryID(int id)
        {
            List<FoodDTO> listFood = FoodDAO.Instance.GetListFoodByCategoryID(id);
            cbbFood.DataSource = listFood;
            cbbFood.DisplayMember = "FoodName";
        }

        //Hàm load ra Danh sách bàn ăn
        public void LoadDinnerTable()
        {
            flpDinnerTable.Controls.Clear();
            List<DinnerTableDTO> dinnerTableList = DinnerTableDAO.Instance.LoadDinnerTableList(); //Lấy ra danh sách bàn ăn
            foreach (DinnerTableDTO item in dinnerTableList)
            {
                //Mỗi 1 bàn ăn trong danh sách bàn tạo ra 1 button
                Button btn = new Button() { Width = DinnerTableDAO.tableWidth, Height = DinnerTableDAO.tableHeight };
                btn.Text = item.DinnerTableName + "\n" + item.Status; //Hiển thị tên button là tên bàn
                btn.Click += BtnBanAn_Click;
                btn.Tag = item;
                //Đổi màu cho 2 loại bàn
                switch (item.Status)
                {
                    case "Trống":
                        btn.BackColor = Color.Aqua;
                        break;
                    case "Có người":
                        btn.BackColor = Color.LightPink;
                        break;
                    default:
                        btn.BackColor = Color.Aqua;
                        break; 
                }
                flpDinnerTable.Controls.Add(btn); //Hiển thị các button bàn ăn lên FlowLayoutPanel
            }
        }

        //Hàm hiển thị hoá đơn
        public void ShowBill(int id)
        {
            lvBill.Items.Clear(); //Xoá hết các item trong listview
            List<MenuDTO> listBillDetail = MenuDAO.Instance.GetListMenuByDinnerTable(id);
            float totalPrice = 0;
            foreach (MenuDTO item in listBillDetail)
            {
                ListViewItem lvItem = new ListViewItem(item.FoodName.ToString());
                lvItem.SubItems.Add(item.Quantity.ToString());
                lvItem.SubItems.Add(item.Price.ToString());
                lvItem.SubItems.Add(item.TotalPrice.ToString());
                totalPrice += item.TotalPrice;
                lvBill.Items.Add(lvItem);
            }
            CultureInfo culture = new CultureInfo("vi-VN");
            txtTotalPrice.Text = totalPrice.ToString("c", culture); //Định dạng tiền tệ sang tiếng Việt           
        }

        #endregion

        #region Events

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProfile frm = new frmProfile(txtSaveUserName.Text);
            frm.ShowDialog();
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            frmAdmin frm = new frmAdmin(txtSaveUserName.Text);
            frm.ShowDialog();
            LoadDinnerTable();
        }

        private void BtnBanAn_Click(object sender, EventArgs e)
        {
            int dinnerTableID = ((sender as Button).Tag as DinnerTableDTO).ID;
            lvBill.Tag = (sender as Button).Tag; //Mỗi khi button click vào bàn nào thì listview có tag của bàn đó
            ShowBill(dinnerTableID);
        }

        //Load danh sách món ăn khi chọn danh mục
        private void cbbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;
            ComboBox cbb = sender as ComboBox;
            if (cbb.SelectedItem == null)
            {
                return;
            }
            CategoryDTO selected = cbb.SelectedItem as CategoryDTO;
            id = selected.ID;
            LoadFoodListByCategoryID(id);
        }

        //Nút thêm món
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            DinnerTableDTO dinnerTable = lvBill.Tag as DinnerTableDTO; //Lấy bàn ăn hiện tại
            if (dinnerTable == null)
            {
                MessageBox.Show("Hãy chọn bàn");
                return;
            }
            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(dinnerTable.ID); //Lấy IDBill
            int idFood = (cbbFood.SelectedItem as FoodDTO).ID;
            int quantity = (int)nudFoodCount.Value;
            if (idBill == -1) //chưa có bill nào
            {
                BillDAO.Instance.InsertBill(dinnerTable.ID); //thêm mới hoá đơn (bill)
                BillDetailDAO.Instance.InsertBillDetail(BillDAO.Instance.GetMaxIDBill(), idFood, quantity); //thêm mới chi tiết hoá đơn (bill_detail)
            }
            else //bill đã tồn tại
            {
                BillDetailDAO.Instance.InsertBillDetail(idBill, idFood, quantity);
            }
            ShowBill(dinnerTable.ID);
            LoadDinnerTable();
        }

        //Nút thanh toán
        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            //Bill hiện tại xác định dựa vào bàn ăn hiện tại
            DinnerTableDTO dinnerTable = lvBill.Tag as DinnerTableDTO; //Lấy bàn ăn hiện tại
            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(dinnerTable.ID); //Lấy IDBill
            int discount = (int)nudDiscount.Value; //Lấy giá trị mã giảm giá trên form
            float totalPrice = float.Parse(txtTotalPrice.Text.Split(',')[0]);
            float finalTotalPrice = totalPrice - (totalPrice / 100) * discount;
            if (idBill != -1) //bàn đã có bill
            {
                if (MessageBox.Show(string.Format("Bạn có chắc muốn thanh toán hoá đơn cho bàn {0}\n\nTổng tiền\n= Tổng tiền - (Tổng tiền / 100) x Giảm giá\n= {1} - ({1} / 100) x {2}\n= {3} VNĐ", dinnerTable.DinnerTableName, totalPrice, discount, finalTotalPrice), "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(idBill, discount,finalTotalPrice); //Thanh toán bill cho bàn
                    ShowBill(dinnerTable.ID); //Load lại bill sau khi thanh toán
                    LoadDinnerTable();
                }
            }
        }

        #endregion

        private void frmManager_Load(object sender, EventArgs e)
        {
            String query = "select * from login where username = '" + txtSaveUserName.Text + "' and role = 'admin' ";
            DataTable role = DataProvider.Instance.ExecuteQuery(query);
            if (role.Rows.Count > 0)
            {
                adminToolStripMenuItem.Visible = true;
            }
            else
            {
                adminToolStripMenuItem.Visible = false;
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void frmManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}

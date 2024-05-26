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
    public partial class frmProfile : Form
    {
        public frmProfile()
        {
            InitializeComponent();
        }
        public frmProfile(String Username)
        {
            InitializeComponent();
            this.txtUsername.Text = Username;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            String sql = "Select password from login where username = '" + txtUsername.Text + "'";
            DataTable dt = TruyXuatCSDL.LayBang(sql); 
            DataRow row = dt.Rows[0];
            if (txtPass.Text.Equals(row[0].ToString()))
            {
                if (txtNewPass.Text.Equals(txtReNewPass.Text))
                {
                    sql = "update login set name = N'" 
                        + txtFullname.Text + "', password = '"+txtNewPass.Text + "' where username = '"+txtUsername.Text+"'";
                    TruyXuatCSDL.ThemSuaXoa(sql);
                    MessageBox.Show("Cập nhật thông tin thành công!",
                        "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close(); 
                }
                else
                {
                    MessageBox.Show("Hãy nhập hai mật khẩu giống nhau!",
                        "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtReNewPass.Focus();
                } 
                    
            }
            else
            {
                MessageBox.Show("Bạn đã nhập sai mật khẩu!",
                        "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPass.Focus();    
            }
        }

        private void frmProfile_Load(object sender, EventArgs e)
        {
            String sql = "Select name from login where username = '" + txtUsername.Text + "'";
            DataTable dt =  TruyXuatCSDL.LayBang(sql); 
            DataRow column = dt.Rows[0];
            txtFullname.Text = column[0].ToString(); 
        }
    }
}

using QuanLyQuanCafe.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyQuanCafe
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void chkAnHien_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAnHien.Checked == true)
            {
                txtPass.UseSystemPasswordChar = false;
            }
            else
            {
                txtPass.UseSystemPasswordChar = true;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có chắc chắc muốn thoát không?","Thông Báo",
                MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
            DialogResult dialogResult;
            if (rs == DialogResult.OK)
            {
                Application.Exit();
            }    
            
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string pass = txtPass.Text.Trim();
            if (username == "")
            {
                MessageBox.Show("Vui lòng nhập tài khoản", "Thông báo",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtUsername.Focus();
            }
            else if (pass == "")
            {
                MessageBox.Show("Vui lòng nhập mật khẩu", "Thông báo",
                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPass.Focus();
            }
            else
            {


                if (Login(username, pass))
                {

                    this.Hide();
                    frmManager frm = new frmManager(txtUsername.Text);
                    frm.ShowDialog();
                    this.Show();
                }
                else
                {
                    MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu! Mời bạn nhập lại!","Thông báo",
                        MessageBoxButtons.OK,MessageBoxIcon.Error);
                    txtUsername.Clear();
                    txtPass.Clear();
                    txtUsername.Focus();
                }
            }
        }

        bool Login(string username, string pass)
        {
            return AccountDAO.Instance.Login(username, pass);
        }
    }
}

using LFA.Forum.BLL;
using LFA.Forum.BLL.Model;
using LFA.Forum.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LFA.Forum.WinForm.UI
{
    public enum UserStatusEnum
    {
        EMAIL_NOT_VERIFIED,
        VERIFIED,
        BLOCKED,
    }

    public partial class UserForm : Form
    {
        private UsersBll _usersBll;

        public UserForm()
        {
            InitializeComponent();

            _usersBll = new UsersBll();

            cboUserStatus.Items.Add(UserStatusEnum.EMAIL_NOT_VERIFIED);
            cboUserStatus.Items.Add(UserStatusEnum.VERIFIED);
            cboUserStatus.Items.Add(UserStatusEnum.BLOCKED);

        }

        private void LoadUsersGrid()
        {
            dgvUsers.DataSource = _usersBll.GetAll();
        }

        private void LoadUsersComboBox()
        {
            var lstUsers = _usersBll.GetAll().Select(x => new { x.UserName, FullName = x.FirstName + " " + x.LastName }).Distinct().ToList();

            comboBox1.DisplayMember = "FullName";
            comboBox1.ValueMember = "UserName";
            comboBox1.DataSource = lstUsers.ToList();

        }

        private void Clear()
        {
            txtUserName.Clear();
            txtFirstName.Clear();
            txtLastName.Clear();
            txtEmail.Clear();
            txtPassword.Clear();
            chkModerator.Checked = false;
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            LoadUsersGrid();
            LoadUsersComboBox();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var SaveOrUpdate = (Button)sender;

            Users u = new Users();
            u.UserName = txtUserName.Text;
            u.FirstName = txtFirstName.Text;
            u.LastName = txtLastName.Text;
            u.Email = txtEmail.Text;
            u.HashPassword = txtPassword.Text;
            u.IsModerator = chkModerator.Checked;
            u.UserStatus = Convert.ToInt32(cboUserStatus.SelectedValue);

            if (SaveOrUpdate.Text == "&Save")
            {
                int sucessNo = _usersBll.Add(u);
                if (sucessNo > 0)
                {
                    LoadUsersComboBox();
                    Clear();
                    MessageBox.Show("Record Added", "User Registration", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                int sucessNo = _usersBll.Update(u);
                MessageBox.Show("Record Updated", "User Registration", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Search_Click(object sender, EventArgs e)
        {
            var objUser = _usersBll.GetAll().FirstOrDefault(x => x.UserName == comboBox1.SelectedValue.ToString());
            if (objUser != null)
            {
                txtUserName.Text = objUser.UserName;
                txtFirstName.Text = objUser.FirstName;
                txtLastName.Text = objUser.LastName;
                txtEmail.Text = objUser.Email;
                txtPassword.Text = objUser.HashPassword;
                chkModerator.Checked = objUser.IsModerator;
                cboUserStatus.SelectedValue = objUser.UserStatus;

                btnSave.Text = "&Update";
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Text = "&Save";
        }


    }
}

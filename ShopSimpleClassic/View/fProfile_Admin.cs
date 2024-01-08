using ShopSimpleClassic.Controller;
using ShopSimpleClassic.Library;
using ShopSimpleClassic.Model;
using System;
using System.Windows.Forms;

namespace ShopSimpleClassic.View
{
    public partial class fProfile_Admin : Form
    {
        /// Variable
        private Admin _data;

        private string pass;
        private bool isSave = false;
        private bool isViewOrEdit;

        /// Main
        public fProfile_Admin(bool isViewOrEdit)
        {
            InitializeComponent();
            this.isViewOrEdit = isViewOrEdit;
            load();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (isViewOrEdit)
                {
                    isViewOrEdit = false;
                }
                else
                {
                    save();
                    isViewOrEdit = true;
                }

                CURD__Control__EnableDisable();
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            try
            {
                if (isViewOrEdit)
                {
                    this.Close();
                }
                else
                {
                    isViewOrEdit = true;
                    binding();
                    CURD__Control__EnableDisable();
                }
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void fProfile_Admin_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = isSave ? DialogResult.Yes : DialogResult.OK;
            e.Cancel = false;
        }

        /// Function
        private void load()
        {
            binding();
            CURD__Control__EnableDisable();
        }

        private void save()
        {
            package();

            if (!checkInput()) return;

            var result = new bAdmin().Update(_data);

            if (!result)
            {
                ShowMess.Error__AddedOrUpdated($"{_data.Username}: {_data.Name}", false);
                return;
            }

            ShowMess.Success__AddedOrUpdated($"{_data.Username}: {_data.Name}", false);
            isSave = true;
        }

        private void binding()
        {
            var binding = new bAdmin().Detail(Temp.Username);
            tbCode.Text = binding.Username;
            tbName.Text = binding.Name;
            pass = binding.Password;
        }

        private void CURD__Control__EnableDisable()
        {
            tbCode.ReadOnly = true;
            tbName.ReadOnly = isViewOrEdit;

            // thay đổi nút
            btAdd.Text = isViewOrEdit ? "Sửa" : "Lưu";
            btAdd.Image = isViewOrEdit ? Properties.Resources.edit : Properties.Resources.save;
            btExit.Text = isViewOrEdit ? "Thoát" : "Huỷ";
        }

        private bool checkInput()
        {
            if (!Lib.CheckInputNotEmpty(pnContent, errorProvider1)) return false;

            if (!new bAdmin().IsExists(_data.Username))
            {
                ShowMess.Error__NotExists($"{_data.Username}: {_data.Name}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Đóng gói dữ liệu
        /// </summary>
        private void package()
        {
            _data = new Admin
            {
                Username = tbCode.Text.Trim(),
                Name = tbName.Text.Trim(),
                Password = pass
            };
        }
    }
}
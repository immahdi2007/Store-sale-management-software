using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KiyanBabyShopCSProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        bool FcodeSucc = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'kiyanDbDataSet.Customers' table. You can move, or remove it, as needed.
            this.customersTableAdapter.Fill(this.kiyanDbDataSet.Customers);
            // TODO: This line of code loads data into the 'kiyanDbDataSet.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter.Fill(this.kiyanDbDataSet.Products);
            toolTip1.SetToolTip(textBox7, "جستجو بر اساس کد، نام و نوع محصول میباشد.");
            toolTip1.SetToolTip(txtCusSearch, "جستجور بر اساس کد، نام و شماره تلفن مشتری میباشد.");
            btnRemove.Enabled = btnUpdate.Enabled = false;
            btnCdelete.Enabled = btnCupdate.Enabled = false;
            LoginForm Flogin = new LoginForm();
            //Flogin.ShowDialog();
            //if (!Flogin.succeeded)
            //{
            //    this.Close();
            //}
            btnShopCart.Enabled = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView1.CurrentCell.RowIndex;
            txtName.Text = dataGridView1.Rows[index].Cells[1].Value.ToString();
            txtStock.Text = dataGridView1.Rows[index].Cells[2].Value.ToString();
            txtPrice.Text = dataGridView1.Rows[index].Cells[3].Value.ToString();
            txtSize.Text = dataGridView1.Rows[index].Cells[4].Value.ToString();
            txtColor.Text = dataGridView1.Rows[index].Cells[5].Value.ToString();
            txtCtg.Text = dataGridView1.Rows[index].Cells[6].Value.ToString();
            string prdCode = dataGridView1.Rows[index].Cells[0].Value.ToString();
            btnRemove.Enabled = false;
            if (dataGridView1.Rows[index].Selected == true)
            {
                btnRemove.Enabled = btnUpdate.Enabled = true;
            }

        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int row_index = dataGridView1.CurrentCell.RowIndex;
            string prdCode = dataGridView1.Rows[row_index].Cells[0].Value.ToString();
            try {
                if (txtName.Text == "" || txtStock.Text == "" || txtPrice.Text == "" || txtSize.Text == "" || txtColor.Text == "" || txtCtg.Text == "")
                    {
                    throw new Exception("لطفا همه فیلد هارا کامل کنید");
                    }
                productsTableAdapter.UpdateQuery(
                    txtName.Text,
                    txtStock.Text,
                    txtPrice.Text,
                    txtSize.Text,
                    txtColor.Text,
                    txtCtg.Text,
                    int.Parse(prdCode)
                    );
                productsTableAdapter.Fill(kiyanDbDataSet.Products);
                dataGridView1.ClearSelection();
                btnRemove.Enabled = btnUpdate.Enabled = false;
                MessageBox.Show("ویرایش با موفقیت انجام گردید");
                txtName.Text =
                txtStock.Text =
                txtPrice.Text =
                txtSize.Text =
                txtColor.Text =
                txtCtg.Text = "";
                //dataGridView1.SelectedCells = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.Text == "" || txtStock.Text == "" || txtPrice.Text == "" || txtSize.Text == "" || txtColor.Text == "" || txtCtg.Text == "")
                {
                    throw new Exception("لطفا همه فیلد هارا کامل کنید");
                }
                productsTableAdapter.InsertQuery(
                txtName.Text,
                txtStock.Text,
                txtPrice.Text,
                txtSize.Text,
                txtColor.Text,
                txtCtg.Text);
                productsTableAdapter.Fill(kiyanDbDataSet.Products);
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show(
                "آیا از انجام این عملیات مطمعن هستید؟؟",
                "حذف محصول",
                MessageBoxButtons.YesNo  ,
                MessageBoxIcon.Question
                );
            if (res == DialogResult.Yes) {
                try
                {
                    int row_index = dataGridView1.CurrentCell.RowIndex;
                    string prdCode = dataGridView1.Rows[row_index].Cells[0].Value.ToString();
                    productsTableAdapter.DeleteQuery(int.Parse(prdCode));
                    productsTableAdapter.Fill(kiyanDbDataSet.Products);

                    if (row_index > 0)
                    {
                        dataGridView1.ClearSelection();
                        btnRemove.Enabled = btnUpdate.Enabled = false;
                    }
                    txtName.Text =
                    txtStock.Text =
                    txtPrice.Text =
                    txtSize.Text =
                    txtColor.Text =
                    txtCtg.Text = "";
                } catch { 
                        }
            }
            else
            {

            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            string srchTxt = textBox7.Text.Trim();
            productsTableAdapter.SearchByCode(kiyanDbDataSet.Products, srchTxt, srchTxt, srchTxt);
            dataGridView1.ClearSelection();
            srchResultLbl.Text = dataGridView1.Rows.Count.ToString();
            srchResultLbl.ForeColor = Color.Green;
            if (textBox7.Text.Length <= 0)
            {
                srchResultLbl.Text = "0";
                srchResultLbl.ForeColor = Color.Black;
            } else if (dataGridView1.Rows.Count <= 0)
            {
                srchResultLbl.ForeColor = Color.Black;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
                txtName.Text =
                txtStock.Text =
                txtPrice.Text =
                txtSize.Text =
                txtColor.Text =
                txtCtg.Text = "";
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(tabControl1.SelectedIndex == 1)
            {
                productsTableAdapter.Fill(kiyanDbDataSet.Products);
                dataGridView1.ClearSelection();
                btnRemove.Enabled = btnUpdate.Enabled = false;
                txtName.Text =
                txtStock.Text =
                txtPrice.Text =
                txtSize.Text =
                txtColor.Text =
                txtCtg.Text = "";
            }
            if(tabControl1.SelectedIndex == 2)
            {
                btnCdelete.Enabled = btnCupdate.Enabled = false;
                customersTableAdapter.Fill(kiyanDbDataSet.Customers);
                dataGridView1.ClearSelection();
                txtCname.Text = txtCLname.Text = txtCtel.Text = "";
            }
        }

        private void txtFCode_TextChanged(object sender, EventArgs e)
        {
            if(txtFCode.Text.Length > 0)
            {
                try
                {
                    int fSrchCode = int.Parse(txtFCode.Text.Trim());
                    productsTableAdapter.FillByPrdCODE(kiyanDbDataSet.Products, fSrchCode);
                    if (kiyanDbDataSet.Products.Rows.Count > 0)
                    {
                        txtFname.ForeColor = Color.SeaGreen;
                        txtFname.Text = kiyanDbDataSet.Products.Rows[0]["prdName"].ToString();
                        txtFPrice.Text = kiyanDbDataSet.Products.Rows[0]["prdPrice"].ToString();
                        txtFStock.Text = kiyanDbDataSet.Products.Rows[0]["prdStock"].ToString();
                        FcodeSucc = true;
                    }
                    else
                    {
                        txtFname.Text = "محصول مورد نظر وجود ندارد";
                        txtFname.ForeColor = Color.Brown;
                        txtFPrice.Text = txtFStock.Text = "";
                        FcodeSucc = false;
                    }
                } catch
                {
                    MessageBox.Show("!لطفا فقط عدد وارد کنید");
                }
            } else
            {
                txtFname.Text = txtFPrice.Text = txtFStock.Text = "";
                FcodeSucc = false;
            }
            if (FcodeSucc)
            {
                btnShopCart.Enabled = true;
            } else
            {
                btnShopCart.Enabled = false;
            }
        }

        private void label21_Click(object sender, EventArgs e)
        {
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            txtName.Focus();
        }

        private void btnShopCart_Click(object sender, EventArgs e)
        {
            //int index = dataGridView1.RowCount - 1;
            try
            {
                int Price_res = int.Parse(txtFPrice.Text) * int.Parse(txtFAmount.Text);
                dgvFators.Rows.Add(
                txtFCode.Text,
                txtFname.Text,
                txtFAmount.Text,
                txtFPrice.Text,
                Price_res
                );

            }
            catch
            {
                MessageBox.Show("لطفا فقط عدد وارد کنید");
            }

        }

        private void dgvFators_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvFators.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.RowIndex >= 0)
            {
                dgvFators.Rows.RemoveAt(e.RowIndex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            string prdCode = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
            txtFCode.Text = prdCode;
            txtFAmount.Focus();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dataGridView2.CurrentCell.RowIndex;
            txtCname.Text = dataGridView2.Rows[index].Cells[1].Value.ToString();
            txtCLname.Text = dataGridView2.Rows[index].Cells[2].Value.ToString();
            txtCtel.Text = dataGridView2.Rows[index].Cells[3].Value.ToString();
            string prdCode = dataGridView2.Rows[index].Cells[0].Value.ToString();
            if (dataGridView2.Rows[index].Selected == true)
            {
                btnCdelete.Enabled = btnCupdate.Enabled = true;
            }
        }

        private void btnCadd_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCname.Text == "" || txtCLname.Text == "" || txtCtel.Text == "")
                {
                    throw new Exception("لطفا همه فیلد هارا کامل کنید");
                }
                customersTableAdapter.InsertQuery(txtCname.Text, txtCLname.Text, txtCtel.Text);
                customersTableAdapter.Fill(kiyanDbDataSet.Customers);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            txtCname.Text = txtCLname.Text = txtCtel.Text = "";
        }

        private void btnCdelete_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show(
            "آیا از انجام این عملیات مطمعن هستید؟؟",
            "حذف مشتری",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
            );
            if (res == DialogResult.Yes)
            {
                try
                {
                    int row_index = dataGridView2.CurrentCell.RowIndex;
                    string CusCode = dataGridView2.Rows[row_index].Cells[0].Value.ToString();
                    customersTableAdapter.DeleteQuery(CusCode);
                    customersTableAdapter.Fill(kiyanDbDataSet.Customers);

                    if (row_index > 0)
                    {
                        dataGridView2.ClearSelection();
                        btnCdelete.Enabled = btnCupdate.Enabled = false;
                    }
                    txtCname.Text = txtCLname.Text = txtCtel.Text = "";
                }
                catch
                {
                }
            }
            else
            {

            }
        }

        private void btnCupdate_Click(object sender, EventArgs e)
        {
            int row_index = dataGridView2.CurrentCell.RowIndex;
            string CusCode = dataGridView2.Rows[row_index].Cells[0].Value.ToString();
            try
            {
                if (txtCname.Text == "" || txtCLname.Text == "" || txtCtel.Text == "")
                {
                    throw new Exception("لطفا همه فیلد هارا کامل کنید");
                }
                customersTableAdapter.UpdateQuery(txtCname.Text, txtCLname.Text, txtCtel.Text, CusCode);
                customersTableAdapter.Fill(kiyanDbDataSet.Customers);
                dataGridView2.ClearSelection();
                btnCdelete.Enabled = btnCupdate.Enabled = false;
                MessageBox.Show("ویرایش با موفقیت انجام گردید");
                txtCname.Text = txtCLname.Text = txtCtel.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtCusSearch_TextChanged(object sender, EventArgs e)
        {
            string srchTxt = txtCusSearch.Text.Trim();
            string param1 = srchTxt + "%";
            string param2 = "%" + srchTxt + "%";
            string param3 = "%" + srchTxt + "%";
            string param4 = srchTxt + "%";
            customersTableAdapter.SearchCustomerTab(kiyanDbDataSet.Customers, param1, param2, param3 , param4);
            dataGridView2.ClearSelection();
            srchResultLbl2.Text = dataGridView2.Rows.Count.ToString();
            srchResultLbl2.ForeColor = Color.Green;
            if (txtCusSearch.Text.Length <= 0)
            {
                srchResultLbl2.Text = "0";
                srchResultLbl2.ForeColor = Color.Black;
            }
            else if (dataGridView2.Rows.Count <= 0)
            {
                srchResultLbl2.ForeColor = Color.Black;
            }
        }

        private void CusToFac_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 0;
            string CusCode = dataGridView2.Rows[dataGridView2.CurrentCell.RowIndex].Cells[0].Value.ToString();
            txtCustomerCode.Text = CusCode;
            txtFAmount.Focus();
        }
    }
}

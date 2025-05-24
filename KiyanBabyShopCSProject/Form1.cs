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
        bool FcodeSucc_cus = false;
        bool isStockValid = false;

        int sumPrice = 0;
        int FactorCode = 0;
        string Fdate;

        private void CheckEnabaledBtnShopCart()
        {
            if(FcodeSucc && FcodeSucc_cus && isStockValid)
            {
                btnShopCart.Enabled = true;
            }
            else
            {
                btnShopCart.Enabled = false;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.kiyanDbDataSet.EnforceConstraints = false;
            // TODO: This line of code loads data into the 'kiyanDbDataSet.FactorItems' table. You can move, or remove it, as needed.
            this.factorItemsTableAdapter.Fill(this.kiyanDbDataSet.FactorItems);
            // TODO: This line of code loads data into the 'kiyanDbDataSet.Factors' table. You can move, or remove it, as needed.
            this.factorsTableAdapter.Fill(this.kiyanDbDataSet.Factors);
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
            SubmitFactor.Enabled = false;


            try
            {
                int FactorCode = (int)factorsTableAdapter.GetFactorCode() + 1;
                lblFactorId.Text = FactorCode.ToString();
            }
            catch
            {

            }

            System.Globalization.PersianCalendar p = new System.Globalization.PersianCalendar();
            Fdate = p.GetYear(DateTime.Now).ToString() + "/" + p.GetMonth(DateTime.Now).ToString("0#") + "/" + p.GetDayOfMonth(DateTime.Now).ToString("0#") + " " + p.GetHour(DateTime.Now).ToString("0#") + ":" + p.GetMinute(DateTime.Now).ToString("0#") + ":" + p.GetSecond(DateTime.Now).ToString("0#");
            lblFactorDate.Text = Fdate;
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
                MessageBox.Show("ویرایش با موفقیت انجام گردید", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                txtCname.Text = txtCLname.Text = txtCtel.Text = txtCLoc.Text = "";
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
                        txtFAmount.Text = "1";
                        FcodeSucc = true;
                    }
                    else
                    {
                        txtFname.Text = "محصول مورد نظر وجود ندارد";
                        txtFname.ForeColor = Color.Brown;
                        txtFPrice.Text = txtFStock.Text = txtFAmount.Text = "";
                        FcodeSucc = false;
                    }
                } catch
                {
                    MessageBox.Show("!لطفا تعداد محصول را صحیح وارد کنید");
                }
            } else
            {
                txtFname.Text = txtFPrice.Text = txtFStock.Text = txtFAmount.Text = "";
                FcodeSucc = false;
            }

            //if (FcodeSucc && FcodeSucc_cus)
            //{
            //    btnShopCart.Enabled = true;

            //}
            //else
            //{
            //    btnShopCart.Enabled = false;
            //}
            CheckEnabaledBtnShopCart();
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
                sumPrice += Price_res;
                Price_result.Text = sumPrice.ToString("N0") + " " + "تومان";
                bool foundPrd = false;
                for (int i = 0; i < dgvFators.Rows.Count; i++)
                {
                    if (int.Parse(dgvFators.Rows[i].Cells[0].Value.ToString()) == int.Parse(txtFCode.Text))
                    {
                        int dgvAmount = int.Parse(dgvFators.Rows[i].Cells[2].Value.ToString());
                        dgvAmount += int.Parse(txtFAmount.Text);
                        dgvFators.Rows[i].Cells[2].Value = dgvAmount;
                        foundPrd = true;
                        break;
                    }
                    else
                    {

                    }
                }
                if (!foundPrd)
                {
                    dgvFators.Rows.Add(
                    txtFCode.Text,
                    txtFname.Text,
                    txtFAmount.Text,
                    txtFPrice.Text,
                    Price_res
                    );
                }
            }
            catch
            {
                MessageBox.Show("لطفا مقادیر را صحیح وارد کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (dgvFators.Rows.Count > 0)
            {
                SubmitFactor.Enabled = true;
            }
            else
            {
                SubmitFactor.Enabled = false;
            }
        }

        private void dgvFators_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(dgvFators.Columns[e.ColumnIndex] is DataGridViewImageColumn && e.RowIndex >= 0)
            {
                if (dgvFators.Rows.Count > 0)
                {
                    SubmitFactor.Enabled = true;
                }
                else
                {
                    sumPrice = 0;
                    SubmitFactor.Enabled = false;
                }
                int minusPrice = int.Parse(dgvFators.Rows[dgvFators.CurrentCell.RowIndex].Cells[4].Value.ToString());
                sumPrice -= minusPrice;
                Price_result.Text = sumPrice.ToString("N0") + " " + "تومان";
                dgvFators.Rows.RemoveAt(e.RowIndex);
                txtFCode.Text = txtFAmount.Text = "";
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
            txtCLoc.Text = dataGridView2.Rows[index].Cells[4].Value.ToString();
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
                if (txtCname.Text == "" || txtCLname.Text == "" || txtCtel.Text == "" || txtCLoc.Text == "")
                {
                    throw new Exception("لطفا همه فیلد هارا کامل کنید");
                }
                customersTableAdapter.InsertQuery(txtCname.Text, txtCLname.Text, txtCtel.Text , txtCLoc.Text);
                customersTableAdapter.Fill(kiyanDbDataSet.Customers);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            txtCname.Text = txtCLname.Text = txtCtel.Text = txtCLoc.Text = "";
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
                    txtCname.Text = txtCLname.Text = txtCtel.Text = txtCLoc.Text= "";
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
                if (txtCname.Text == "" || txtCLname.Text == "" || txtCtel.Text == "" || txtCLoc.Text == "")
                {
                    throw new Exception("لطفا همه فیلد هارا کامل کنید");
                }
                customersTableAdapter.UpdateQuery(txtCname.Text, txtCLname.Text, txtCtel.Text, txtCLoc.Text, int.Parse(CusCode));
                customersTableAdapter.Fill(kiyanDbDataSet.Customers);
                dataGridView2.ClearSelection();
                btnCdelete.Enabled = btnCupdate.Enabled = false;
                MessageBox.Show("ویرایش با موفقیت انجام گردید", "" , MessageBoxButtons.OK , MessageBoxIcon.Information);
                txtCname.Text = txtCLname.Text = txtCtel.Text = txtCLoc.Text = "";
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

        private void txtCustomerCode_TextChanged(object sender, EventArgs e)
        {
            if (txtCustomerCode.Text.Length >= 0)
            {
                try
                {
                    string fSrchCode = txtCustomerCode.Text.Trim();
                    customersTableAdapter.FillByCusCode(kiyanDbDataSet.Customers, fSrchCode);
                    if (kiyanDbDataSet.Customers.Rows.Count > 0)
                    {
                        lblFCutumoerName.ForeColor = Color.SeaGreen;
                        lblFCutumoerName.Text = (kiyanDbDataSet.Customers.Rows[0]["CustomerFristName"].ToString()) + " " + (kiyanDbDataSet.Customers.Rows[0]["CustomerLastName"].ToString());
                        lblFCutumoerTel.Text = kiyanDbDataSet.Customers.Rows[0]["CustomerMobile"].ToString();
                        lblFCutumoerLoc.Text = kiyanDbDataSet.Customers.Rows[0]["CustomerLocation"].ToString();
                        FcodeSucc_cus = true;
                    }
                    else
                    {
                        lblFCutumoerName.Text = "لطفا کد مشتری را صحیح وارد کنید.";
                        lblFCutumoerName.ForeColor = Color.Brown;
                        lblFCutumoerTel.Text = lblFCutumoerLoc.Text = "";
                        FcodeSucc_cus = false;
                    }
                }
                catch
                {
                    MessageBox.Show("!لطفا فقط عدد وارد کنید", "" , MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                lblFCutumoerName.Text = lblFCutumoerName.Text = "";
                FcodeSucc_cus = false;
            }

            //if (FcodeSucc && FcodeSucc_cus)
            //{
            //    btnShopCart.Enabled = true;
            //}
            //else
            //{
            //    btnShopCart.Enabled = false;
            //}
            CheckEnabaledBtnShopCart();
        }

        private bool CustomerCheckValid(string code)
        {
            customersTableAdapter.FillByCusCode(kiyanDbDataSet.Customers, code);
            return kiyanDbDataSet.Customers.Rows.Count > 0;

        }
        private void button6_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 2;
            txtCname.Focus();
        }

        private void SubmitFactor_Click(object sender, EventArgs e)
        {
            if (!CustomerCheckValid(txtCustomerCode.Text.Trim()))
            {
                MessageBox.Show("مشتری دیگر در دیتا بیس وجود ندارد", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                FcodeSucc_cus = false;
                CheckEnabaledBtnShopCart();
                return;
            }
            
            string CustomerCode = txtCustomerCode.Text;
            try
            {
                factorsTableAdapter.InsertQuery(CustomerCode, Fdate);

                //for minus the number of prdStock
                for (int i = 0; i < dgvFators.Rows.Count; i++) {
                    int prdCode = int.Parse(dgvFators.Rows[i].Cells[0].Value.ToString());
                    int prdAmount = int.Parse(dgvFators.Rows[i].Cells[2].Value.ToString());

                    kiyanDbDataSet.Products.Clear();
                    productsTableAdapter.FillByPrdCODE(kiyanDbDataSet.Products, prdCode);

                    if(kiyanDbDataSet.Products.Rows.Count > 0)
                    {
                        int prdStock = int.Parse(kiyanDbDataSet.Products.Rows[0]["prdStock"].ToString());
                        int finalAmount = prdAmount;
                        
                        //minusStock = prdStock - prdAmount;
                        if (prdAmount > prdStock)
                        {
                            finalAmount = prdStock;
                            MessageBox.Show($"مقدار درخواستی برای محصول مورد نظر بیشتر از موجودی انبار بود\n" +
                                $"از تعداد {prdAmount} عدد درخواستی، فقط {prdStock} عدد تحویل داده شد",
                                "هشدار موجودی",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                                );
                        }


                        int minusStock = prdStock - finalAmount;
                        productsTableAdapter.UpdatePrdStock(minusStock.ToString() , prdCode);
                        productsTableAdapter.Fill(kiyanDbDataSet.Products);


                        //submit in factorItem
                        factorItemsTableAdapter.InsertQuery(((int)factorsTableAdapter.GetFactorCode()).ToString(), prdCode, finalAmount);
                    }
                }
                txtFCode.Text =
                txtFAmount.Text =
                txtCustomerCode.Text =
                lblFCutumoerLoc.Text =
                Price_result.Text = "";
                dgvFators.Rows.Clear();
                int FactorCode = (int)factorsTableAdapter.GetFactorCode() + 1;
                lblFactorId.Text = FactorCode.ToString();
                SubmitFactor.Enabled = false;


                MessageBox.Show("فاکتور با کد فاکتور" + " " + (FactorCode - 1).ToString() + " " + "در دیتا بیس ثبت شد" , "عملیات موفق", MessageBoxButtons.OK , MessageBoxIcon.Information);
            }
            catch(Exception ee)
            {
                MessageBox.Show("خطا در ثبت فاکتور" + ee.Message , "" , MessageBoxButtons.OK , MessageBoxIcon.Error);
            }
        }

        private void txtFAmount_TextChanged(object sender, EventArgs e)
        {
            int prdAmount = 1;
            int prdStock = 0;

            if (kiyanDbDataSet.Products.Rows.Count > 0 && txtFCode.TextLength > 0)
            {
                prdStock = int.Parse(kiyanDbDataSet.Products.Rows[0]["prdStock"].ToString());
                if (int.TryParse(txtFAmount.Text, out prdAmount))
                {
                    if(prdAmount > prdStock)
                    {
                        //btnShopCart.Enabled = false;
                        isStockValid = false;
                        MessageBox.Show("تعداد محصول مورد نظر موجود نمیباشد","خطا" , MessageBoxButtons.OK , MessageBoxIcon.Error);
                    } else
                    {
                        isStockValid = true;
                        //btnShopCart.Enabled = true;
                    }
                    CheckEnabaledBtnShopCart();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
                txtFCode.Text = 
                txtFAmount.Text =
                txtCustomerCode.Text = 
                lblFCutumoerLoc.Text = 
                Price_result.Text = "";
            SubmitFactor.Enabled = false;
            sumPrice = 0;
            dgvFators.Rows.Clear();
        }
    }
}

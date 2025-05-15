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


        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'kiyanDbDataSet.Products' table. You can move, or remove it, as needed.
            this.productsTableAdapter.Fill(this.kiyanDbDataSet.Products);
            toolTip1.SetToolTip(textBox7, "جستجو بر اساس کد، نام و نوع محصول میباشد.");
            btnRemove.Enabled = btnUpdate.Enabled = false;
            LoginForm Flogin = new LoginForm();
            //Flogin.ShowDialog();
            //if (!Flogin.succeeded)
            //{
            //    this.Close();
            //}

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
            int row_index = dataGridView1.CurrentCell.RowIndex;
            string prdCode = dataGridView1.Rows[row_index].Cells[0].Value.ToString();
            btnRemove.Enabled = false;
            if (dataGridView1.Rows[row_index].Selected == true)
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
                dataGridView1.Rows[row_index].Selected = true;
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
                        dataGridView1.Rows[row_index].Selected = true;
                    }
                    txtName.Text = dataGridView1.Rows[row_index].Cells[1].Value.ToString();
                    txtStock.Text = dataGridView1.Rows[row_index].Cells[2].Value.ToString();
                    txtPrice.Text = dataGridView1.Rows[row_index].Cells[3].Value.ToString();
                    txtSize.Text = dataGridView1.Rows[row_index].Cells[4].Value.ToString();
                    txtColor.Text = dataGridView1.Rows[row_index].Cells[5].Value.ToString();
                    txtCtg.Text = dataGridView1.Rows[row_index].Cells[6].Value.ToString();
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
                dataGridView1.ClearSelection();
                btnRemove.Enabled = btnUpdate.Enabled = false;

            }
        }
    }
}

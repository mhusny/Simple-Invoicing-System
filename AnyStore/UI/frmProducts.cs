using AnyStore.BLL;
using AnyStore.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace AnyStore.UI
{
    public partial class frmProducts : Form
    {
        public frmProducts()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            //Add code to hide this form
            this.Hide();
        }

        categoriesDAL cdal = new categoriesDAL();
        productsBLL p = new productsBLL();
        productsDAL pdal = new productsDAL();
        userDAL udal = new userDAL();
        transactionDAL tDAL = new transactionDAL();
        transactionDetailDAL tdDAL = new transactionDetailDAL();

        public DataTable transactionDT = new DataTable();

        private void frmProducts_Load(object sender, EventArgs e)
        {
            //Creating DAta Table to hold the categories from Database
            DataTable categoriesDT = cdal.Select();
            //Specify DataSource for Category ComboBox
            cmbCategory.DataSource = categoriesDT;
            //Specify Display Member and Value Member for Combobox
            cmbCategory.DisplayMember = "title";
            cmbCategory.ValueMember = "title";

            //Load all the Products in Data Grid View
            DataTable dt = pdal.Select();
            dgvProducts.DataSource = dt;

            //Specify Columns for our TransactionDataTable
            transactionDT.Columns.Add("Code");
            transactionDT.Columns.Add("Product Name");
            transactionDT.Columns.Add("Rate");
            transactionDT.Columns.Add("Quantity");
            transactionDT.Columns.Add("Discount");
            transactionDT.Columns.Add("Total");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Get All the Values from Product Form
            p.name = txtName.Text;
            p.category = cmbCategory.Text;
            p.description = txtDescription.Text;
            p.rate = decimal.Parse(txtRate.Text);
            p.qty = 0;
            p.added_date = DateTime.Now;
            //Getting username of logged in user
            String loggedUsr = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUsr);

            p.added_by = usr.id;

            //Create Boolean variable to check if the product is added successfully or not
            bool success = pdal.Insert(p);
            //if the product is added successfully then the value of success will be true else it will be false
            if(success==true)
            {
                //Product Inserted Successfully
                MessageBox.Show("Product Added Successfully");
                //Calling the Clear Method
                Clear();
                //Refreshing DAta Grid View
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
            else
            {
                //Failed to Add New product
                MessageBox.Show("Failed to Add New Product");
            }
        }
        public void Clear()
        {
            txtID.Text = "";
            txtName.Text = "";
            txtDescription.Text = "";
            txtRate.Text = "";
            txtSearch.Text = "";
        }

        private void dgvProducts_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Create integer variable to know which product was clicked
            int rowIndex = e.RowIndex;
            //Display the Value on Respective TextBoxes
            txtID.Text = dgvProducts.Rows[rowIndex].Cells[0].Value.ToString();
            txtName.Text = dgvProducts.Rows[rowIndex].Cells[1].Value.ToString();
            cmbCategory.Text = dgvProducts.Rows[rowIndex].Cells[2].Value.ToString();
            txtDescription.Text = dgvProducts.Rows[rowIndex].Cells[3].Value.ToString();
            txtRate.Text = dgvProducts.Rows[rowIndex].Cells[4].Value.ToString();
            txtQty.Text = dgvProducts.Rows[rowIndex].Cells[5].Value.ToString();
            txtNewQty.Text = dgvProducts.Rows[rowIndex].Cells[5].Value.ToString();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //Update Quantity----------------------------------------------------
            transactionsBLL transaction = new transactionsBLL();


            //Get the ID of Dealer or Customer Here
            //Lets get name of the dealer or customer first
            //string deaCustName = cmbCustomer.Text;
            //DeaCustBLL dc = dcDAL.GetDeaCustIDFromName(deaCustName);

            //transaction.invoice_no = decimal.Parse(txtInvoiceNo.Text);

            if (Math.Round(decimal.Parse("0" + txtQty.Text) - decimal.Parse("0" + txtNewQty.Text), 2) < 0)
            {
                transaction.type = "ADJIN";
            }
            else
            {
                transaction.type = "ADJOUT";
            }

            
            transaction.invoice_no = 0;
            transaction.dea_cust_id = 0;
            transaction.grandTotal = Math.Round(decimal.Parse("0" + txtQty.Text) - decimal.Parse("0" + txtNewQty.Text), 2) * decimal.Parse("0" + txtRate.Text);
            transaction.transaction_date = DateTime.Now;
            transaction.tax = 0;
            transaction.discount = 0;
            transaction.cash = 0;
            transaction.card = 0;
            transaction.cheque = 0;
            transaction.cheque_no = 0;

            //Get the Username of Logged in user
            string username = frmLogin.loggedIn;
            userBLL u = udal.GetIDFromUsername(username);

            transaction.added_by = u.id;
            transaction.transactionDetails = transactionDT;

            //Lets Create a Boolean Variable and set its value to false
            bool success = false;

            //Actual Code to Insert Transaction And Transaction Details
            using (TransactionScope scope = new TransactionScope())
            {
                int transactionID = -1;
                //Create aboolean value and insert transaction 
                bool w = tDAL.Insert_Transaction(transaction, out transactionID);

                //Use for loop to insert Transaction Details
                //for (int i = 0; i < transactionDT.Rows.Count; i++)
                //{
                    //Get all the details of the product
                    transactionDetailBLL transactionDetail = new transactionDetailBLL();
                    //Get the Product name and convert it to id
                    string ProductName = dgvProducts.SelectedRows[0].Cells[0].ToString();
                    productsBLL p = pdal.GetProductIDFromName(ProductName);

                    transactionDetail.transastion_id = transactionID;
                    transactionDetail.product_id = p.id;
                    transactionDetail.rate = decimal.Parse(txtRate.Text);
                    transactionDetail.qty = Math.Round(decimal.Parse("0" + txtQty.Text) - decimal.Parse("0" + txtNewQty.Text), 2);
                    transactionDetail.discount = 0;
                    transactionDetail.total = Math.Round(decimal.Parse("0" + txtQty.Text) - decimal.Parse("0" + txtNewQty.Text), 2) * decimal.Parse("0" + txtRate.Text);
                    transactionDetail.dea_cust_id = 0;
                    transactionDetail.added_date = DateTime.Now;
                    transactionDetail.added_by = u.id;

                    //Here Increase or Decrease Product Quantity based on Purchase or sales
                    string transactionType = lblTop.Text;

                    //Lets check whether we are on ADJIN or ADJOUT
                    bool x = false;
                    if (transactionType == "ADJOUT")
                    {
                        //Decrease the Product coz RTS
                        x = pdal.DecreaseProduct(transactionDetail.product_id, transactionDetail.qty);
                    }
                    else if (transactionType == "ADJIN")
                    {
                        //Increase the Product Quntiyt coz CRN
                        x = pdal.IncreaseProduct(transactionDetail.product_id, transactionDetail.qty);
                    }

                    //Insert Transaction Details inside the database
                    bool y = tdDAL.InsertTransactionDetail(transactionDetail);
                    success = w && x && y;

                //}

                tDAL.UpdateInvNo(decimal.Parse(tDAL.GetNextInvoiceNo(transaction.type).ToString()), transaction.type);
                //tDAL.UpdateInvNo(decimal.Parse(txtInvoiceNo.Text), transaction.type);
                //update grand total
                if (success == true)
                {
                    ////////////frmReport r = new frmReport();
                    ////////////r.DocId = 1;
                    ////////////r.TrID = transactionID;
                    ////////////r.ShowDialog();
                    //Transaction Complete
                    scope.Complete();

                    //Code to Print Bill

                    //to do print bills


                    //DGVPrinter printer = new DGVPrinter();

                    //printer.Title = "\r\n\r\n\r\n ANYSTORE PVT. LTD. \r\n\r\n";
                    //printer.SubTitle = "Kathmandu, Nepal \r\n Phone: 01-045XXXX \r\n\r\n";
                    //printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                    //printer.PageNumbers = true;
                    //printer.PageNumberInHeader = false;
                    //printer.PorportionalColumns = true;
                    //printer.HeaderCellAlignment = StringAlignment.Near;
                    //printer.Footer = "Discount: " + txtDiscount.Text + "% \r\n" + "VAT: " + txtVat.Text + "% \r\n" + "Grand Total: " + txtGrandTotal.Text + "\r\n\r\n" + "Thank you for doing business with us.";
                    //printer.FooterSpacing = 15;
                    //printer.PrintDataGridView(dgvAddedProducts);


                    //MessageBox.Show("Transaction Completed Sucessfully");
                    //Celar the Data Grid View and Clear all the TExtboxes
                    //dgvAddedProducts.DataSource = null;
                    //dgvAddedProducts.Rows.Clear();
                    transactionDT.Rows.Clear();



                    //txtSearchProduct.Text = "";
                    //cmbItemName.Text = "";
                    txtQty.Text = txtNewQty.Text;
                    txtNewQty.Text = "0";
                    //txtDiscount.Text = "0";
                    //txtVat.Text = "0";
                    //txtGrandTotal.Text = "0";
                    //txtCash.Text = "0";
                    //txtCard.Text = "0";
                    //txtCheque.Text = "0";
                    //txtChequeNo.Text = "0";
                    //txtReturnAmount.Text = "0";
                    //txtInvoiceNo.Text = (decimal.Parse(txtInvoiceNo.Text) + 1).ToString();

                    //Load all the Products in Data Grid View
                    DataTable dt = pdal.Select();
                    dgvProducts.DataSource = dt;

                }
                else
                {
                    //Transaction Failed
                    MessageBox.Show("Transaction Failed");
                }
            }
            //-------------------------------------------------------------------

            //Get the Values from UI or Product Form
            p.id = int.Parse(txtID.Text);
            p.name = txtName.Text;
            p.category = cmbCategory.Text;
            p.description = txtDescription.Text;
            p.rate = decimal.Parse(txtRate.Text);
            p.qty = decimal.Parse(txtNewQty.Text);
            p.added_date = DateTime.Now;
            //Getting Username of logged in user for added by
            String loggedUsr = frmLogin.loggedIn;
            userBLL usr = udal.GetIDFromUsername(loggedUsr);

            p.added_by = usr.id;

            //Create a boolean variable to check if the product is updated or not
            success = pdal.Update(p);
            //If the prouct is updated successfully then the value of success will be true else it will be false
            if(success==true)
            {
                //Product updated Successfully
                MessageBox.Show("Product Successfully Updated");
                Clear();
                //REfresh the Data Grid View
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
            else
            {
                //Failed to Update Product
                MessageBox.Show("Failed to Update Product");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //GEt the ID of the product to be deleted
            p.id = int.Parse(txtID.Text);

            //Create Bool VAriable to Check if the product is deleted or not
            bool success = pdal.Delete(p);

            //If prouct is deleted successfully then the value of success will true else it will be false
            if(success==true)
            {
                //Product Successfuly Deleted
                MessageBox.Show("Product successfully deleted.");
                Clear();
                //Refresh DAta Grid View
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
            else
            {
                //Failed to Delete Product
                MessageBox.Show("Failed to Delete Product.");
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            //Get the Keywordss from Form
            string keywords = txtSearch.Text;

            if(keywords!=null)
            {
                //Search the products
                DataTable dt = pdal.Search(keywords);
                dgvProducts.DataSource = dt;
            }
            else
            {
                //Display All the products
                DataTable dt = pdal.Select();
                dgvProducts.DataSource = dt;
            }
        }
    }
}

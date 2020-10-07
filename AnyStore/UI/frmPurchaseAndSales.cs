using AnyStore.BLL;
using AnyStore.DAL;
using DGVPrinterHelper;
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
    public partial class frmPurchaseAndSales : Form
    {
        public string Code;
        public string Description;
        public decimal Rate;
        public decimal Qty;

        public frmPurchaseAndSales()
        {
            InitializeComponent();
        }

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        DeaCustDAL dcDAL = new DeaCustDAL();
        productsDAL pDAL = new productsDAL();
        userDAL uDAL = new userDAL();
        transactionDAL tDAL = new transactionDAL();
        transactionDetailDAL tdDAL = new transactionDetailDAL();

        DataTable transactionDT = new DataTable();
        private void frmPurchaseAndSales_Load(object sender, EventArgs e)
        {
            //Get the transactionType value from frmUserDashboard
            string type = frmUserDashboard.transactionType;
            //Set the value on lblTop
            lblTop.Text = type;

            //Specify Columns for our TransactionDataTable
            transactionDT.Columns.Add("Code");
            transactionDT.Columns.Add("Product Name");
            transactionDT.Columns.Add("Rate");
            transactionDT.Columns.Add("Quantity");
            transactionDT.Columns.Add("Total");

            //Get the keyword fro the text box
            //string keyword = txtSearch.Text;

            //if (keyword == "")
            //{
            //    //Clear all the textboxes
            //    txtName.Text = "";
            //    txtEmail.Text = "";
            //    txtContact.Text = "";
            //    txtAddress.Text = "";
            //    return;
            //}

            //Write the code to get the details and set the value on text boxes
            //DeaCustBLL dc = dcDAL.GetDealerCustomerForTransaction();
            DataSet ds =  dcDAL.GetDealerCustomerForTransaction();

            //Now transfer or set the value from DeCustBLL to textboxes
            cmbCustomer.DataSource = ds.Tables[0];
            cmbCustomer.ValueMember = "name";
            cmbCustomer.DisplayMember = "name";
        }

        private void txtSearchProduct_TextChanged(object sender, EventArgs e)
        {
            ////Get the keyword from productsearch textbox
            //string keyword = txtSearchProduct.Text;
            //TxtQty.Text = "1";
            ////Check if we have value to txtSearchProduct or not
            //if (keyword=="")
            //{
            //    TxtQty.Text = "";
            //    return;
            //}

            ////Search the product and display on respective textboxes
            //var p = pDAL.GetProductsForTransaction(keyword);

            //productName = p[0].description.ToString();
            //Rate = decimal.Parse(p[0].rate.ToString());
            //Qty = decimal.Parse(p[0].qty.ToString());

            //decimal Total = Rate * Qty; //Total=RatexQty

            ////Display the Subtotal in textbox
            ////Get the subtotal value from textbox
            ////decimal subTotal = decimal.Parse(txtSubTotal.Text);
            ////subTotal = subTotal + Total;

            ////Check whether the product is selected or not
            ////if(productName=="")
            ////{
            ////    //Display error MEssage
            ////    MessageBox.Show("Select the product first. Try Again.");
            ////}
            ////else
            ////{
            ////Add product to the dAta Grid View
            //transactionDT.Rows.Add(productName, Rate, Qty, Total);

            ////Show in DAta Grid View
            //dgvAddedProducts.DataSource = transactionDT;
            ////Display the Subtotal in textbox
            ////txtSubTotal.Text = subTotal.ToString();

            ////Clear the Textboxes
            ////txtSearchProduct.Text = "";
            ////txtProductName.Text = "";
            ////txtInventory.Text = "0.00";
            ////txtRate.Text = "0.00";
            ////TxtQty.Text = "0.00";
            ////}

            ////Set the values on textboxes based on p object
            ////txtProductName.Text = p.description;
            ////txtInventory.Text = p.qty.ToString();
            ////txtRate.Text = p.rate.ToString();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //Get Product Name, Rate and Qty customer wants to buy
            //string productName = txtProductName.Text;
            //decimal Rate = decimal.Parse(txtRate.Text);
            //decimal Qty = decimal.Parse(TxtQty.Text);

            //decimal Total = Rate * Qty; //Total=RatexQty

            //Display the Subtotal in textbox
            //Get the subtotal value from textbox
            //decimal subTotal = decimal.Parse(txtSubTotal.Text);
            //subTotal = subTotal + Total;

            //Check whether the product is selected or not
            //if(productName=="")
            //{
            //    //Display error MEssage
            //    MessageBox.Show("Select the product first. Try Again.");
            //}
            //else
            //{
            //    //Add product to the dAta Grid View
            //    transactionDT.Rows.Add(productName,Rate,Qty,Total);

            //    //Show in DAta Grid View
            //    dgvAddedProducts.DataSource = transactionDT;
            //    //Display the Subtotal in textbox
            //    txtSubTotal.Text = subTotal.ToString();

            //    //Clear the Textboxes
            //    txtSearchProduct.Text = "";
            //    txtProductName.Text = "";
            //    txtInventory.Text = "0.00";
            //    txtRate.Text = "0.00";
            //    TxtQty.Text = "0.00";
            //}
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            //Get the value fro discount textbox
            string value = txtDiscount.Text;

            if(value=="")
            {
                //Display Error Message
                MessageBox.Show("Please Add Discount First");
            }
            else
            {
                //Get the discount in decimal value
                decimal subTotal = decimal.Parse(txtSubTotal.Text);
                decimal discount = decimal.Parse(txtDiscount.Text);

                //Calculate the grandtotal based on discount
                decimal grandTotal = ((100 - discount) / 100) * subTotal;

                //Display the GrandTotla in TextBox
                txtGrandTotal.Text = grandTotal.ToString();
            }
            
        }

        private void txtVat_TextChanged(object sender, EventArgs e)
        {
            //Check if the grandTotal has value or not if it has not value then calculate the discount first
            string check = txtGrandTotal.Text;
            if(check=="")
            {
                //Deisplay the error message to calcuate discount
                MessageBox.Show("Calculate the discount and set the Grand Total First.");
            }
            else
            {
                //Calculate VAT
                //Getting the VAT Percent first
                decimal previousGT = decimal.Parse(txtGrandTotal.Text);
                decimal vat = decimal.Parse(txtVat.Text);
                decimal grandTotalWithVAT=((100+vat)/100)*previousGT;

                //Displaying new grand total with vat
                txtGrandTotal.Text = grandTotalWithVAT.ToString();
            }
        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            //Get the paid amount and grand total
            decimal grandTotal = decimal.Parse(txtGrandTotal.Text);
            decimal paidAmount = decimal.Parse(txtPaidAmount.Text);

            decimal returnAmount = paidAmount - grandTotal;

            //Display the return amount as well
            txtReturnAmount.Text = returnAmount.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Get the Values from PurchaseSales Form First
            transactionsBLL transaction = new transactionsBLL();

            transaction.type = lblTop.Text;

            //Get the ID of Dealer or Customer Here
            //Lets get name of the dealer or customer first
            string deaCustName = cmbCustomer.Text;
            DeaCustBLL dc = dcDAL.GetDeaCustIDFromName(deaCustName);

            transaction.dea_cust_id = dc.id;
            transaction.grandTotal = Math.Round(decimal.Parse(txtGrandTotal.Text),2);
            transaction.transaction_date = DateTime.Now;
            transaction.tax = decimal.Parse(txtVat.Text);
            transaction.discount = decimal.Parse(txtDiscount.Text);

            //Get the Username of Logged in user
            string username = frmLogin.loggedIn;
            userBLL u = uDAL.GetIDFromUsername(username);

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
                for(int i=0;i<transactionDT.Rows.Count;i++)
                {
                    //Get all the details of the product
                    transactionDetailBLL transactionDetail = new transactionDetailBLL();
                    //Get the Product name and convert it to id
                    string ProductName = transactionDT.Rows[i][0].ToString();
                    productsBLL p = pDAL.GetProductIDFromName(ProductName);

                    transactionDetail.product_id = p.id;
                    transactionDetail.rate = decimal.Parse(transactionDT.Rows[i][1].ToString());
                    transactionDetail.qty = decimal.Parse(transactionDT.Rows[i][2].ToString());
                    transactionDetail.total = Math.Round(decimal.Parse(transactionDT.Rows[i][3].ToString()),2);
                    transactionDetail.dea_cust_id = dc.id;
                    transactionDetail.added_date = DateTime.Now;
                    transactionDetail.added_by = u.id;

                    //Here Increase or Decrease Product Quantity based on Purchase or sales
                    string transactionType = lblTop.Text;

                    //Lets check whether we are on Purchase or Sales
                    bool x=false;
                    if(transactionType=="Purchase")
                    {
                        //Increase the Product
                        x = pDAL.IncreaseProduct(transactionDetail.product_id, transactionDetail.qty);
                    }
                    else if(transactionType=="Sales")
                    {
                        //Decrease the Product Quntiyt
                        x = pDAL.DecreaseProduct(transactionDetail.product_id, transactionDetail.qty);
                    }

                    //Insert Transaction Details inside the database
                    bool y = tdDAL.InsertTransactionDetail(transactionDetail);
                    success = w && x && y;
                }
                
                if (success == true)
                {
                    //Transaction Complete
                    scope.Complete();

                    //Code to Print Bill
                    DGVPrinter printer = new DGVPrinter();

                    printer.Title = "\r\n\r\n\r\n ANYSTORE PVT. LTD. \r\n\r\n";
                    printer.SubTitle = "Kathmandu, Nepal \r\n Phone: 01-045XXXX \r\n\r\n";
                    printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;
                    printer.PageNumbers = true;
                    printer.PageNumberInHeader = false;
                    printer.PorportionalColumns = true;
                    printer.HeaderCellAlignment = StringAlignment.Near;
                    printer.Footer = "Discount: "+ txtDiscount.Text +"% \r\n" + "VAT: " + txtVat.Text + "% \r\n" + "Grand Total: "+ txtGrandTotal.Text + "\r\n\r\n" +"Thank you for doing business with us.";
                    printer.FooterSpacing = 15;
                    printer.PrintDataGridView(dgvAddedProducts);

                    MessageBox.Show("Transaction Completed Sucessfully");
                    //Celar the Data Grid View and Clear all the TExtboxes
                    dgvAddedProducts.DataSource = null;
                    dgvAddedProducts.Rows.Clear();

                    
                    txtSearchProduct.Text = "";
                    
                    TxtQty.Text = "0";
                    txtSubTotal.Text = "0";
                    txtDiscount.Text = "0";
                    txtVat.Text = "0";
                    txtGrandTotal.Text = "0";
                    txtPaidAmount.Text = "0";
                    txtReturnAmount.Text = "0";
                }
                else
                {
                    //Transaction Failed
                    MessageBox.Show("Transaction Failed");
                }
            }
        }

        private void txtSearchProduct_KeyDown(object sender, KeyEventArgs e)
        {
            //Get the keyword from productsearch textbox
            string keyword = txtSearchProduct.Text;
            TxtQty.Text = "1";
            //Check if we have value to txtSearchProduct or not
            if (keyword == "")
            {
                TxtQty.Text = "";
                return;
            }

            if (e.KeyCode == Keys.Enter)
            {
                //Search the product and display on respective textboxes
                var p = pDAL.GetProductsForTransaction(keyword);

                if (p.Count == 1)
                {
                    Code = p[0].name.ToString();
                    Description = p[0].description.ToString();
                    Rate = decimal.Parse(p[0].rate.ToString());
                    Qty = decimal.Parse(TxtQty.Text);

                    decimal Total = Rate * Qty; //Total=RatexQty
                    int added = -1;

                    for (int i = 0; i < dgvAddedProducts.Rows.Count - 1; i++)
                    {
                        if (Code == dgvAddedProducts.Rows[i].Cells["code"].Value.ToString())
                        added = i;
                    }

                    if (added >= 0)
                    {
                        dgvAddedProducts.Rows[added].Cells["Quantity"].Value = decimal.Parse(dgvAddedProducts.Rows[added].Cells["Quantity"].Value.ToString()) + 1;
                    }
                    else
                    {
                        //Add product to the dAta Grid View
                        transactionDT.Rows.Add(Code, Description, Rate, Qty, Total);
                        //Show in DAta Grid View
                        dgvAddedProducts.DataSource = transactionDT;
                    }

                    CalcTot();

                    ////Display the Subtotal in textbox
                    ////Get the subtotal value from textbox
                    //decimal subTotal = decimal.Parse(txtSubTotal.Text);
                    //subTotal = subTotal + Total;
                    
                    ////Display the Subtotal in textbox
                    //txtSubTotal.Text = subTotal.ToString();
                    
                    //Clear the Textboxes
                    txtSearchProduct.Text = "";
                    //txtProductName.Text = "";
                    //txtInventory.Text = "0.00";
                    //txtRate.Text = "0.00";
                    TxtQty.Text = "0.00";
                }
            }
        }


        private void dgvAddedProducts_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            CalcTot();
        }

        private void TxtQty_KeyDown(object sender, KeyEventArgs e)
        {
            //Get the keyword from productsearch textbox
            string keyword = txtSearchProduct.Text;

            if (e.KeyCode == Keys.Enter)
            {
                //Search the product and display on respective textboxes
                var p = pDAL.GetProductsForTransaction(keyword);

                if (p.Count == 1)
                {
                    Code = p[0].name.ToString();
                    Description = p[0].description.ToString();
                    Rate = decimal.Parse(p[0].rate.ToString());
                    Qty = decimal.Parse(TxtQty.Text);

                    decimal Total = Rate * Qty; //Total=RatexQty

                    //Display the Subtotal in textbox
                    //Get the subtotal value from textbox
                    decimal subTotal = decimal.Parse(txtSubTotal.Text);
                    subTotal = subTotal + Total;


                    //Add product to the dAta Grid View
                    transactionDT.Rows.Add(Code, Description, Rate, Qty, Total);
                    //Show in DAta Grid View
                    dgvAddedProducts.DataSource = transactionDT;
                    //Display the Subtotal in textbox
                    txtSubTotal.Text = subTotal.ToString();

                    //Clear the Textboxes
                    txtSearchProduct.Text = "";
                    //txtProductName.Text = "";
                    //txtInventory.Text = "0.00";
                    //txtRate.Text = "0.00";
                    TxtQty.Text = "0.00";
                }
            }
        }

        private void CalcTot()
        {
            if (dgvAddedProducts.Rows.Count > 0)
            {
                decimal Total = 0;
                for (int i = 0; i < dgvAddedProducts.Rows.Count - 1; i++)
                {
                    Total = Total + (decimal.Parse(dgvAddedProducts.Rows[i].Cells["Quantity"].Value.ToString()) * decimal.Parse(dgvAddedProducts.Rows[i].Cells["Rate"].Value.ToString()));
                }

                //Display the Subtotal in textbox
                txtSubTotal.Text = Total.ToString();
            }
            else
            {
                txtSubTotal.Text = "0.00";
            }

            txtSearchProduct.Focus();
        }
    }
}

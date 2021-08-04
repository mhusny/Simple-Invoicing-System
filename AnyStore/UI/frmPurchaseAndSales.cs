using AnyStore.BLL;
using AnyStore.DAL;
using DGVPrinterHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace AnyStore.UI
{
    public partial class frmPurchaseAndSales : Form
    {
        public int transactionID;
        public string Code;
        public string type;
        public string Description;
        public decimal Rate;
        public decimal Qty;
        public decimal Discount;

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

        public DataTable transactionDT = new DataTable();

        private void frmPurchaseAndSales_Load(object sender, EventArgs e)
        {
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(KeyEvent);

            //Get the transactionType value from frmUserDashboard
            //string type = frmUserDashboard.transactionType;
            //Set the value on lblTop
            lblTop.Text = type;

            //Specify Columns for our TransactionDataTable
            transactionDT.Columns.Add("Code");
            transactionDT.Columns.Add("Product Name");
            transactionDT.Columns.Add("Rate");
            transactionDT.Columns.Add("Quantity");
            transactionDT.Columns.Add("Discount");
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

            txtInvoiceNo.Text = tDAL.GetNextInvoiceNo(type).ToString();

            DataSet ds =  dcDAL.GetDealerCustomerForTransaction();
            //Now transfer or set the value from DeCustBLL to textboxes
            cmbCustomer.DataSource = ds.Tables[0];
            cmbCustomer.ValueMember = "name";
            cmbCustomer.DisplayMember = "name";
            this.ActiveControl = txtSearchProduct;

            //Get Item list
            DataTable items = pDAL.Search("");
            cmbItemName.DataSource = items;
            cmbItemName.ValueMember = "name";
            cmbItemName.DisplayMember = "description";


            //foreach (DataGridViewRow row in dgvAddedProducts.Rows)
            //{
            //    transactionDT.Rows.Add(row.Cells[0].Value, row.Cells[1].Value, row.Cells[2].Value, row.Cells[3].Value, row.Cells[4].Value, row.Cells[5].Value);
            //    //foreach (DataGridViewCell cell in row.Cells)
            //    //{
            //    //    transactionDT.Rows[transactionDT.Rows.Count - 1][cell.ColumnIndex] = cell.Value.ToString();
            //    //}

            //}
        }

        private void KeyEvent(object sender, KeyEventArgs e) //Keyup Event 
        {
            if (e.KeyCode == Keys.F5)
            {
                txtCash.Text = txtGrandTotal.Text;
                txtCard.Text = "";
                txtCheque.Text = "";
            }
            if (e.KeyCode == Keys.F6)
            {
                txtCard.Text = txtGrandTotal.Text;
                txtCheque.Text = "";
                txtCash.Text = "";
            }
            if (e.KeyCode == Keys.F7)
            {
                txtCheque.Text = txtGrandTotal.Text;
                txtChequeNo.Focus();
                txtCard.Text = "";
                txtCash.Text = "";
            }
            if (e.KeyCode == Keys.F9)
            {
                btnSave_Click(sender, e);
            }
            //else
            //MessageBox.Show("No Function");

        }

        public void txtSearchProduct_TextChanged(object sender, EventArgs e)
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
                decimal previousGT = decimal.Parse("0" + txtGrandTotal.Text);
                decimal vat = decimal.Parse("0" + txtVat.Text);
                decimal grandTotalWithVAT=((100+vat)/100)*previousGT;

                //Displaying new grand total with vat
                txtGrandTotal.Text = grandTotalWithVAT.ToString();
            }
        }

        private void txtPaidAmount_TextChanged(object sender, EventArgs e)
        {
            //Get the paid amount and grand total
            decimal grandTotal = decimal.Parse(txtGrandTotal.Text);
            decimal paidAmount = decimal.Parse("0" + txtCash.Text) + decimal.Parse("0" + txtCard.Text) + decimal.Parse("0" + txtCheque.Text);

            decimal returnAmount = paidAmount - grandTotal;

            //Display the return amount as well
            txtReturnAmount.Text = returnAmount.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //validation
            if (decimal.Parse("0" + txtCheque.Text) > 0 && txtChequeNo.Text.Length < 2)
            {
                MessageBox.Show("Please enter a cheque no.");
            }

            //Get the Values from PurchaseSales Form First
            transactionsBLL transaction = new transactionsBLL();

            transaction.type = lblTop.Text;

            //Get the ID of Dealer or Customer Here
            //Lets get name of the dealer or customer first
            string deaCustName = cmbCustomer.Text;
            DeaCustBLL dc = dcDAL.GetDeaCustIDFromName(deaCustName);

            transaction.invoice_no = decimal.Parse(txtInvoiceNo.Text);
            //transaction.invoice_no = decimal.Parse(tDAL.GetNextInvoiceNo(transaction.type).ToString());
            transaction.dea_cust_id = dc.id;
            transaction.grandTotal = Math.Round(decimal.Parse("0" + txtGrandTotal.Text), 2);
            transaction.transaction_date = dtpBillDate.Value;
            transaction.tax = decimal.Parse("0" + txtVat.Text);
            transaction.discount = decimal.Parse("0" + txtDiscount.Text);
            transaction.cash = decimal.Parse("0" + txtCash.Text);
            transaction.card = decimal.Parse("0" + txtCard.Text);
            transaction.cheque = decimal.Parse("0" + txtCheque.Text);
            transaction.cheque_no = decimal.Parse("0" + txtChequeNo.Text);

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
                for (int i = 0; i < transactionDT.Rows.Count; i++)
                {
                    //Get all the details of the product
                    transactionDetailBLL transactionDetail = new transactionDetailBLL();
                    //Get the Product name and convert it to id
                    string ProductName = transactionDT.Rows[i][0].ToString();
                    productsBLL p = pDAL.GetProductIDFromName(ProductName);

                    transactionDetail.transastion_id = transactionID;
                    transactionDetail.product_id = p.id;
                    transactionDetail.rate = decimal.Parse(transactionDT.Rows[i][2].ToString());
                    transactionDetail.qty = decimal.Parse(transactionDT.Rows[i][3].ToString());
                    transactionDetail.discount = decimal.Parse(transactionDT.Rows[i][4].ToString());
                    transactionDetail.total = Math.Round(decimal.Parse(transactionDT.Rows[i][5].ToString()), 2);
                    transactionDetail.dea_cust_id = dc.id;
                    transactionDetail.added_date = DateTime.Now;
                    transactionDetail.added_by = u.id;

                    //Here Increase or Decrease Product Quantity based on Purchase or sales
                    string transactionType = lblTop.Text;

                    //Lets check whether we are on Purchase or Sales
                    bool x = false;
                    if (transactionType == "Purchase")
                    {
                        //Increase the Product
                        x = pDAL.IncreaseProduct(transactionDetail.product_id, transactionDetail.qty);
                    }
                    else if (transactionType == "Sales")
                    {
                        //Decrease the Product Quntiyt
                        x = pDAL.DecreaseProduct(transactionDetail.product_id, transactionDetail.qty);
                    }

                    //Insert Transaction Details inside the database
                    bool y = tdDAL.InsertTransactionDetail(transactionDetail);
                    success = w && x && y;

                    //tDAL.IncrementInvNo(decimal.Parse(tDAL.GetNextInvoiceNo().ToString()));
                    tDAL.UpdateInvNo(decimal.Parse(txtInvoiceNo.Text), transactionType);
                }
                //update grand total
                if (success == true)
                {
                    frmReport r = new frmReport();
                    r.DocId = 1;
                    r.TrID = transactionID;
                    r.ShowDialog();
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
                    dgvAddedProducts.DataSource = null;
                    dgvAddedProducts.Rows.Clear();
                    transactionDT.Rows.Clear();



                    txtSearchProduct.Text = "";
                    cmbItemName.Text = "";
                    TxtQty.Text = "0";
                    txtSubTotal.Text = "0";
                    txtDiscount.Text = "0";
                    txtVat.Text = "0";
                    txtGrandTotal.Text = "0";
                    txtCash.Text = "0";
                    txtCard.Text = "0";
                    txtCheque.Text = "0";
                    txtChequeNo.Text = "0";
                    txtReturnAmount.Text = "0";
                    txtInvoiceNo.Text = (decimal.Parse(txtInvoiceNo.Text) + 1).ToString();
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
            string keyword = "";
            try
            {
                if (txtSearchProduct.Text.Length > 0)
                {
                    keyword = txtSearchProduct.Text;
                    //cmbItemName.Text = "";
                }
                else if (cmbItemName.Text.Length > 0)
                {
                    keyword = cmbItemName.SelectedValue.ToString();
                    //txtSearchProduct.Text = "";
                }
            }
            catch (Exception)
            {
               
            }

            TxtQty.Text = "1";
            //Check if we have value to txtSearchProduct or not
            if (keyword == "")
            {
                TxtQty.Text = "";
                return;
            }
            
            if (e.KeyCode == Keys.Enter)
            {
                addItemToGrid(keyword);
            }
        }

        public void addItemToGrid(string keyword)
        {
            //Search the product and display on respective textboxes
            var p = pDAL.GetProductsForTransaction(keyword);

            if (p.Count == 1)
            {
                Code = p[0].name.ToString();
                Description = p[0].description.ToString();
                Rate = decimal.Parse(p[0].rate.ToString());
                Qty = 1;


                decimal Total = Rate * Qty; //Total=RatexQty
                int added = -1;




                for (int i = 0; i < dgvAddedProducts.Rows.Count - 1; i++)
                {
                    if (Code == dgvAddedProducts.Rows[i].Cells["code"].Value.ToString())
                        added = i;
                }

                if (added >= 0) //if item is added increase the qty
                {
                    dgvAddedProducts.Rows[added].Cells["Quantity"].Value = decimal.Parse(dgvAddedProducts.Rows[added].Cells["Quantity"].Value.ToString()) + 1;
                }
                else
                {
                    //Add product to the dAta Grid View
                    transactionDT.Rows.Add(Code, Description, Rate, Qty, Discount, Total);
                    //Show in DAta Grid View
                    dgvAddedProducts.DataSource = transactionDT;
                }


                dgvAddedProducts.Columns["Code"].Width = 50;
                dgvAddedProducts.Columns["Product Name"].Width = 250;
                //dgvAddedProducts.Columns["Rate"].
                //dgvAddedProducts.Columns["Quantity"].Width
                //dgvAddedProducts.Columns["Discount"].Width
                //dgvAddedProducts.Columns["Total"].Width

                CalcTot();

                ////Display the Subtotal in textbox
                ////Get the subtotal value from textbox
                //decimal subTotal = decimal.Parse(txtSubTotal.Text);
                //subTotal = subTotal + Total;

                ////Display the Subtotal in textbox
                //txtSubTotal.Text = subTotal.ToString();

                //Clear the Textboxes
                txtSearchProduct.Text = "";
                cmbItemName.Text = "";
                //txtProductName.Text = "";
                //txtInventory.Text = "0.00";
                //txtRate.Text = "0.00";
                TxtQty.Text = "0.00";
            }
        }

        //todo to do adjust stock quantity

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

        public void CalcTot()
        {
            if (dgvAddedProducts.Rows.Count > 0)
            {
                decimal Total = 0;
                for (int i = 0; i < dgvAddedProducts.Rows.Count - 1; i++)
                {
                    Total = Total + (decimal.Floor(decimal.Parse("0" + dgvAddedProducts.Rows[i].Cells["Quantity"].Value.ToString()) * decimal.Parse("0" + dgvAddedProducts.Rows[i].Cells["Rate"].Value.ToString()) * (1 - decimal.Parse("0" + dgvAddedProducts.Rows[i].Cells["Discount"].Value.ToString()) / 100)));
                    dgvAddedProducts.Rows[i].Cells["Total"].Value = decimal.Floor(decimal.Parse("0" + dgvAddedProducts.Rows[i].Cells["Quantity"].Value.ToString()) * decimal.Parse("0" + dgvAddedProducts.Rows[i].Cells["Rate"].Value.ToString()) * (1 - decimal.Parse("0" + dgvAddedProducts.Rows[i].Cells["Discount"].Value.ToString()) / 100));
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

        private void dgvAddedProducts_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //toto not only edit quantity
            if (dgvAddedProducts.Columns[e.ColumnIndex].Name == "Quantity")
            { 
                var Rate = decimal.Parse(dgvAddedProducts.Rows[e.RowIndex].Cells["Rate"].Value.ToString());
                var Quantiy = decimal.Parse(dgvAddedProducts.Rows[e.RowIndex].Cells["Quantity"].Value.ToString());
                dgvAddedProducts.Rows[e.RowIndex].Cells["Total"].Value = Rate * Quantiy;

                CalcTot();
            }
        }

        private void cmbCustomer_SelectedValueChanged(object sender, EventArgs e)
        {
            string deaCustName = cmbCustomer.Text;
            DeaCustBLL dc = dcDAL.GetDeaCustIDFromName(deaCustName);


            if (dc.type != null)

            Discount = decimal.Parse(dc.type);

            {
                if (dgvAddedProducts.Rows.Count>0)
                {
                    for (int i = 0; i < dgvAddedProducts.Rows.Count - 1; i++)
                    {
                        dgvAddedProducts.Rows[i].Cells["Discount"].Value = dc.type;
                    }
                    CalcTot();
                }
            }
        }

        private void txtCard_TextChanged(object sender, EventArgs e)
        {
            //Get the paid amount and grand total
            decimal grandTotal = decimal.Parse(txtGrandTotal.Text);
            decimal paidAmount = decimal.Parse("0" + txtCash.Text) + decimal.Parse("0" + txtCard.Text) + decimal.Parse("0" + txtCheque.Text);

            decimal returnAmount = paidAmount - grandTotal;

            //Display the return amount as well
            txtReturnAmount.Text = returnAmount.ToString();
        }

        private void txtCheque_TextChanged(object sender, EventArgs e)
        {
            //Get the paid amount and grand total
            decimal grandTotal = decimal.Parse(txtGrandTotal.Text);
            decimal paidAmount = decimal.Parse("0" + txtCash.Text) + decimal.Parse("0" + txtCard.Text) + decimal.Parse("0" + txtCheque.Text);

            decimal returnAmount = paidAmount - grandTotal;

            //Display the return amount as well
            txtReturnAmount.Text = returnAmount.ToString();
        }

        private void txtSubTotal_TextChanged(object sender, EventArgs e)
        {
            txtGrandTotal.Text = txtSubTotal.Text;
        }

        private void txtGrandTotal_TextChanged(object sender, EventArgs e)
        {
            txtCash.Text = txtGrandTotal.Text;
        }

        private void cmbItemName_KeyDown(object sender, KeyEventArgs e)
        {
            txtSearchProduct_KeyDown(sender, e);
        }

        private void txtSearchProduct_Leave(object sender, EventArgs e)
        {
            txtSearchProduct.Text = "";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //validation
            if (decimal.Parse("0" + txtCheque.Text) > 0 && txtChequeNo.Text.Length < 2)
            {
                MessageBox.Show("Please enter a cheque no.");
            }

            //to do cancell bills

            //Get the Values from PurchaseSales Form First
            transactionsBLL transaction = new transactionsBLL();

            
            if (lblTop.Text == "Sales")
            {
                transaction.type = "CRN";
            }
            else if (lblTop.Text == "Purchase")
            {
                transaction.type = "RTS";
            }

            //Get the ID of Dealer or Customer Here
            //Lets get name of the dealer or customer first
            string deaCustName = cmbCustomer.Text;
            DeaCustBLL dc = dcDAL.GetDeaCustIDFromName(deaCustName);

            //transaction.invoice_no = decimal.Parse(txtInvoiceNo.Text);
            transaction.invoice_no = decimal.Parse(tDAL.GetNextInvoiceNo(transaction.type).ToString());
            transaction.dea_cust_id = dc.id;
            transaction.grandTotal = Math.Round(decimal.Parse("0" + txtGrandTotal.Text), 2);
            transaction.transaction_date = dtpBillDate.Value;
            transaction.tax = decimal.Parse("0" + txtVat.Text);
            transaction.discount = decimal.Parse("0" + txtDiscount.Text);
            transaction.cash = decimal.Parse("0" + txtCash.Text);
            transaction.card = decimal.Parse("0" + txtCard.Text);
            transaction.cheque = decimal.Parse("0" + txtCheque.Text);
            transaction.cheque_no = decimal.Parse("0" + txtChequeNo.Text);

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
                for (int i = 0; i < transactionDT.Rows.Count; i++)
                {
                    //Get all the details of the product
                    transactionDetailBLL transactionDetail = new transactionDetailBLL();
                    //Get the Product name and convert it to id
                    string ProductName = transactionDT.Rows[i][0].ToString();
                    productsBLL p = pDAL.GetProductIDFromName(ProductName);

                    transactionDetail.transastion_id = transactionID;
                    transactionDetail.product_id = p.id;
                    transactionDetail.rate = decimal.Parse(transactionDT.Rows[i][2].ToString());
                    transactionDetail.qty = decimal.Parse(transactionDT.Rows[i][3].ToString());
                    transactionDetail.discount = decimal.Parse(transactionDT.Rows[i][4].ToString());
                    transactionDetail.total = Math.Round(decimal.Parse(transactionDT.Rows[i][5].ToString()), 2);
                    transactionDetail.dea_cust_id = dc.id;
                    transactionDetail.added_date = DateTime.Now;
                    transactionDetail.added_by = u.id;

                    //Here Increase or Decrease Product Quantity based on Purchase or sales
                    string transactionType = lblTop.Text;

                    //Lets check whether we are on Purchase or Sales
                    bool x = false;
                    if (transactionType == "Purchase")
                    {
                        //Decrease the Product coz RTS
                        x = pDAL.DecreaseProduct(transactionDetail.product_id, transactionDetail.qty);
                    }
                    else if (transactionType == "Sales")
                    {
                        //Increase the Product Quntiyt coz CRN
                        x = pDAL.IncreaseProduct(transactionDetail.product_id, transactionDetail.qty);
                    }

                    //Insert Transaction Details inside the database
                    bool y = tdDAL.InsertTransactionDetail(transactionDetail);
                    success = w && x && y;

                }

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
                    dgvAddedProducts.DataSource = null;
                    dgvAddedProducts.Rows.Clear();
                    transactionDT.Rows.Clear();



                    txtSearchProduct.Text = "";
                    cmbItemName.Text = "";
                    TxtQty.Text = "0";
                    txtSubTotal.Text = "0";
                    txtDiscount.Text = "0";
                    txtVat.Text = "0";
                    txtGrandTotal.Text = "0";
                    txtCash.Text = "0";
                    txtCard.Text = "0";
                    txtCheque.Text = "0";
                    txtChequeNo.Text = "0";
                    txtReturnAmount.Text = "0";
                    txtInvoiceNo.Text = (decimal.Parse(txtInvoiceNo.Text) + 1).ToString();
                }
                else
                {
                    //Transaction Failed
                    MessageBox.Show("Transaction Failed");
                }
            }
        }
        
        private void btnReturn_Click(object sender, EventArgs e)
        {
            frmReport r = new frmReport();
            r.DocId = 1;
            r.TrID = transactionID;
            r.ShowDialog();
        }
    }
}
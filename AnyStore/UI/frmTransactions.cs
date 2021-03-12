using AnyStore.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnyStore.UI
{
    public partial class frmTransactions : Form
    {
        public frmTransactions()
        {
            InitializeComponent();
        }

        transactionDAL tdal = new transactionDAL();
        DeaCustDAL dcDAL = new DeaCustDAL();

        private void pictureBoxClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void frmTransactions_Load(object sender, EventArgs e)
        {

            //Dispay all the transactions
            DataTable dt = tdal.DisplayAllTransactions();
            dgvTransactions.DataSource = dt;

            DataTable dt1 = tdal.DisplayTransactions("invoice_no", "0", "999999999");
            cmbInvFrom.DataSource = dt1;
            cmbInvFrom.ValueMember = "invoice_no";

            DataTable dt2 = tdal.DisplayTransactions("invoice_no", "0", "999999999");
            cmbInvTo.DataSource = dt2;
            cmbInvTo.ValueMember = "invoice_no";

            DataSet ds1 = dcDAL.GetDealerCustomerForTransaction();
            cmbCusFrom.DataSource = ds1.Tables[0];
            cmbCusFrom.ValueMember = "name";

            DataSet ds2 = dcDAL.GetDealerCustomerForTransaction();
            cmbCusTo.DataSource = ds2.Tables[0];
            cmbCusTo.ValueMember = "name";
        }

        private void cmbTransactionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Get the Value from Combobox
            string type = cmbTransactionType.Text;

            DataTable dt = tdal.DisplayTransactionByType(type);
            dgvTransactions.DataSource = dt;
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            //Dispay all the transactions
            DataTable dt = tdal.DisplayAllTransactions();
            dgvTransactions.DataSource = dt;
        }

        private void dgvTransactions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataTable transactionDT = new DataTable();
            transactionDT.Columns.Add("Code");
            transactionDT.Columns.Add("Product Name");
            transactionDT.Columns.Add("Rate");
            transactionDT.Columns.Add("Quantity");
            transactionDT.Columns.Add("Discount");
            transactionDT.Columns.Add("Total");

            DataSet tr;
            int id = int.Parse(dgvTransactions.Rows[e.RowIndex].Cells[0].Value.ToString());
            tr = tdal.DisplayTransactionByID(id);

            frmPurchaseAndSales fsales = new frmPurchaseAndSales();
            fsales.Show();

            foreach (DataRow dr in tr.Tables[0].Rows)
            {

                //Add product to the dAta Grid View
                transactionDT.Rows.Add(dr[32], dr["description"], dr["rate"], dr["qty"], dr["discount1"], dr["total"]);
                //Show in DAta Grid View
                fsales.dgvAddedProducts.DataSource = transactionDT;
            }
            fsales.cmbCustomer.Text = tr.Tables[0].Rows[0]["name"].ToString();

            fsales.dtpBillDate.Value = DateTime.Parse(tr.Tables[0].Rows[0]["transaction_date"].ToString());
            fsales.txtInvoiceNo.Text = tr.Tables[0].Rows[0]["invoice_no"].ToString();

            if (double.Parse(tr.Tables[0].Rows[0]["card"].ToString()) > 0)
            {
                fsales.txtCard.Text = fsales.txtGrandTotal.Text;
                fsales.txtCash.Text = "0";
            }

            if (double.Parse(tr.Tables[0].Rows[0]["cheque"].ToString()) > 0)
            {
                fsales.txtCheque.Text = fsales.txtGrandTotal.Text;
                fsales.txtChequeNo.Text = tr.Tables[0].Rows[0]["cheque_no"].ToString();
                fsales.txtCash.Text = "0";
            }

            //CalcTot();
            //txtSearchProduct.Text = "";
            //TxtQty.Text = "0.00";
        }
   

        private void btnPrint_Click(object sender, EventArgs e)
        {
            frmReport r = new frmReport();
            r.DocId = 2;
            r.invfrom = cmbInvFrom.Text;
            r.invto = cmbInvTo.Text;
            r.ShowDialog();
        }
    }
}

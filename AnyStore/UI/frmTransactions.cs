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

            DataTable dt1 = tdal.DisplayTransactions("invoice_no");
            cmbInvFrom.DataSource = dt1;
            cmbInvFrom.ValueMember = "invoice_no";

            DataTable dt2 = tdal.DisplayTransactions("invoice_no");
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
            int id = int.Parse(dgvTransactions.Rows[e.RowIndex].Cells[0].Value.ToString());
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            frmReport r = new frmReport();
            r.DocId = 2;
            r.ShowDialog();
        }
    }
}

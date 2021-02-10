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
    public partial class frmReport : Form
    {
        //List<BLL.transactionDetailBLL> _list;
        //Orders _orders;
        //public frmReport(Orders orders, List<OrderDetail> list)
        //{
        //    InitializeComponent();
        //    _orders = orders;
        //    _list = list;
        //}
        public int TrID;

        public frmReport()
        {
            InitializeComponent();
        }

        DAL.transactionDAL dblayer = new DAL.transactionDAL();

        private void frmReport_Load(object sender, EventArgs e)
        {
            List<BLL.BillBLL> _List = new List<BLL.BillBLL>();

            DataSet ds1 = dblayer.DisplayTransactionByID(TrID);

            foreach (DataRow dr in ds1.Tables[0].Rows)
            {
                _List.Add(new BLL.BillBLL
                {
                    id = int.Parse(dr["id"].ToString()),
                    invoice_no = dr["invoice_no"].ToString(),
                    grandTotal =  decimal.Parse(dr["grandTotal"].ToString()),
                    transaction_date = DateTime.Parse(dr["transaction_date"].ToString()),
                    name = dr["name"].ToString(),
                    Pname = dr["Expr1"].ToString(),
                    description = dr["description"].ToString(),
                    discount =  decimal.Parse(dr["discount"].ToString()),
                    total = decimal.Parse(dr["total"].ToString()),
                    rate  = decimal.Parse(dr["rate"].ToString()),
                    qty  = decimal.Parse(dr["qty"].ToString()),
                });

            }

            Report.rptBill rptdoc = new Report.rptBill();
            rptdoc.SetDataSource(_List);
            crystalReportViewer1.ReportSource = rptdoc;
        }
    }
}

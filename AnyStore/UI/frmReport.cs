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
        public int DocId;

        public String itemfrom;
        public String itemto;

        public String cusfrom;
        public String custo;

        public String invfrom;
        public String invto;

        public DateTime datefrom;
        public DateTime dateto;

        public frmReport()
        {
            InitializeComponent();
        }

        DAL.transactionDAL dblayer = new DAL.transactionDAL();

        private void frmReport_Load(object sender, EventArgs e)
        {
            if (DocId == 1) // bill
            {
                List<BLL.BillBLL> _List = new List<BLL.BillBLL>();

                DataSet ds1 = dblayer.DisplayTransactionByID(TrID);

                foreach (DataRow dr in ds1.Tables[0].Rows)
                {
                    _List.Add(new BLL.BillBLL
                    {
                        id = int.Parse(dr["id"].ToString()),
                        invoice_no = dr["invoice_no"].ToString(),
                        grandTotal = decimal.Parse(dr["grandTotal"].ToString()),
                        transaction_date = DateTime.Parse(dr["transaction_date"].ToString()),
                        name = dr["name"].ToString(),
                        Pname = dr["name1"].ToString(),
                        description = dr["description"].ToString(),
                        discount = decimal.Parse(dr["discount"].ToString()),
                        total = decimal.Parse(dr["total"].ToString()),
                        rate = decimal.Parse(dr["rate"].ToString()),
                        qty = decimal.Parse(dr["qty"].ToString()),
                    });

                }

                Report.rptBill rptdoc = new Report.rptBill();
                rptdoc.SetDataSource(_List);
                crystalReportViewer1.ReportSource = rptdoc;
                crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            }

            if (DocId == 2) // sales report
            {
                List<BLL.transactionsBLL> _List = new List<BLL.transactionsBLL>();

                DataTable dt = dblayer.DisplayTransactions("*", invfrom, invto);

                foreach (DataRow dr in dt.Rows)
                {
                    _List.Add(new BLL.transactionsBLL
                    {
                        id = int.Parse(dr["id"].ToString()),
                        invoice_no = decimal.Parse(dr["invoice_no"].ToString()),
                        type = dr["type"].ToString(),
                        dea_cust_id = int.Parse(dr["dea_cust_id"].ToString()),
                        grandTotal = decimal.Parse(dr["grandTotal"].ToString()),
                        transaction_date = DateTime.Parse(dr["transaction_date"].ToString()),
                        tax = decimal.Parse(dr["tax"].ToString()),
                        discount = decimal.Parse(dr["discount"].ToString()),
                        added_by = int.Parse(dr["added_by"].ToString()),
                        cash = decimal.Parse(dr["cash"].ToString()),
                        card = decimal.Parse(dr["card"].ToString()),
                        cheque = decimal.Parse(dr["cheque"].ToString()),
                        cheque_no = decimal.Parse(dr["cheque_no"].ToString()),
                    });

                }

                Report.rptSales rptdoc = new Report.rptSales();
                rptdoc.SetDataSource(_List);
                crystalReportViewer1.ReportSource = rptdoc;
                crystalReportViewer1.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None;
            }
        }
    }
}

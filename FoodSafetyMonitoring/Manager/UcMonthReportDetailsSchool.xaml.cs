using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using FoodSafetyMonitoring.dao;
using FoodSafetyMonitoring.Manager.UserControls;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// UcMonthReportDetailsSchool.xaml 的交互逻辑
    /// </summary>
    public partial class UcMonthReportDetailsSchool : UserControl
    {
        private IDBOperation dbOperation;
        private Dictionary<string, MyColumn> MyColumns = new Dictionary<string, MyColumn>();
        public string Sj { get; set; }
        public string DeptId { get; set; }
        public string ItemId { get; set; }
        public string ResultId { get; set; }

        public UcMonthReportDetailsSchool(IDBOperation dbOperation, string sj, string deptId, string itemId, string resultId)
        {
            InitializeComponent();

            this.dbOperation = dbOperation;
            this.Sj = sj;
            this.DeptId = deptId;
            this.ItemId = itemId;
            this.ResultId = resultId;

            MyColumns.Add("orderid", new MyColumn("orderid", "检测单编号") { BShow = true, Width = 8 });
            MyColumns.Add("detecttypename", new MyColumn("detecttypename", "信息来源") { BShow = true, Width = 8 });
            MyColumns.Add("detectdate", new MyColumn("detectdate", "检测时间") { BShow = true, Width = 18 });
            MyColumns.Add("partname", new MyColumn("partname", "检测单位") { BShow = true, Width = 18 });
            MyColumns.Add("itemname", new MyColumn("itemname", "检测项目") { BShow = true, Width = 10 });
            MyColumns.Add("objectname", new MyColumn("objectname", "检测对象") { BShow = true, Width = 8 });
            MyColumns.Add("reagentname", new MyColumn("reagentname", "检测方法") { BShow = true, Width = 10 });
            MyColumns.Add("detectvalue", new MyColumn("detectvalue", "检测值") { BShow = true, Width = 8 });
            MyColumns.Add("resultname", new MyColumn("resultname", "检测结果") { BShow = true, Width = 8 });
            MyColumns.Add("detectusername", new MyColumn("detectusername", "检测师") { BShow = true, Width = 15 });
            MyColumns.Add("areaname", new MyColumn("areaname", "来源产地") { BShow = false });
            MyColumns.Add("companyname", new MyColumn("companyname", "被检单位") { BShow = true, Width = 25 });
            MyColumns.Add("sum_num", new MyColumn("sum_num", "总行数") { BShow = false });

            _tableview.MyColumns = MyColumns;
            _tableview.BShowDetails = true;
            _tableview.GetDataByPageNumberEvent += new UcTableOperableView_NoTitle.GetDataByPageNumberEventHandler(_tableview_GetDataByPageNumberEvent);
            _tableview.DetailsRowEnvent += new UcTableOperableView_NoTitle.DetailsRowEventHandler(_tableview_DetailsRowEnvent);
            _tableview.PageIndex = 1;
            GetData();
        }

        private void GetData()
        {
            DataTable table = dbOperation.GetDbHelper().GetDataSet(string.Format("call  p_report_month_details_school('{0}','{1}','{2}','{3}',{4},{5})",
                                Sj, DeptId, ItemId, ResultId,
                              (_tableview.PageIndex - 1) * _tableview.RowMax,
                              _tableview.RowMax)).Tables[0];

            _tableview.Table = table;
            _sj.Visibility = Visibility.Visible;
            _hj.Visibility = Visibility.Visible;
            _title.Text = _tableview.RowTotal.ToString();
        }

        void _tableview_GetDataByPageNumberEvent()
        {
            GetData();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        void _tableview_DetailsRowEnvent(string id)
        {
            int orderid = int.Parse(id);
            detectdetailsSchool det = new detectdetailsSchool(dbOperation, orderid);
            det.ShowDialog();
        }
    }
}

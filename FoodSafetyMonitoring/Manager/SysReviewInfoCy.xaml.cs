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
using FoodSafetyMonitoring.Common;
using FoodSafetyMonitoring.dao;
using System.Data;
using FoodSafetyMonitoring.Manager.UserControls;
using Toolkit = Microsoft.Windows.Controls;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// SysReviewInfoCy.xaml 的交互逻辑
    /// </summary>
    public partial class SysReviewInfoCy : UserControl
    {
        private IDBOperation dbOperation;
        private DataTable current_table;
        private Dictionary<string, MyColumn> MyColumns = new Dictionary<string, MyColumn>();
        public SysReviewInfoCy(IDBOperation dbOperation)
        {
            InitializeComponent();

            this.dbOperation = dbOperation;

            //初始化查询条件
            reportDate_kssj.SelectedDate = DateTime.Now.AddDays(-1);
            reportDate_jssj.SelectedDate = DateTime.Now;
            //检测单位
            ComboboxTool.InitComboboxSource(_detect_dept, string.Format("call p_user_dept('{0}','0')", (Application.Current.Resources["User"] as UserInfo).ID), "cxtj");
            //检测项目
            ComboboxTool.InitComboboxSource(_detect_item, "SELECT ItemID,ItemNAME FROM t_det_item WHERE  OPENFLAG = '1' order by orderId", "cxtj");

            MyColumns.Add("orderid", new MyColumn("orderid", "检测单编号") { BShow = true, Width = 8 });
            MyColumns.Add("detecttypename", new MyColumn("detecttypename", "信息来源") { BShow = true, Width = 8 });
            MyColumns.Add("detectdate", new MyColumn("detectdate", "检测时间") { BShow = true, Width = 18 });
            MyColumns.Add("partname", new MyColumn("partname", "检测单位") { BShow = true, Width = 16 });
            MyColumns.Add("itemname", new MyColumn("itemname", "检测项目") { BShow = true, Width = 10 });
            MyColumns.Add("objectname", new MyColumn("objectname", "检测对象") { BShow = true, Width = 8 });
            MyColumns.Add("reagentname", new MyColumn("reagentname", "检测方法") { BShow = true, Width = 10 });
            MyColumns.Add("detectvalue", new MyColumn("detectvalue", "检测值") { BShow = true, Width = 8 });
            MyColumns.Add("resultname", new MyColumn("resultname", "检测结果") { BShow = true, Width = 8 });
            MyColumns.Add("detectusername", new MyColumn("detectusername", "检测师") { BShow = true, Width = 10 });
            MyColumns.Add("areaname", new MyColumn("areaname", "来源产地") { BShow = false });
            MyColumns.Add("companyname", new MyColumn("companyname", "被检单位") { BShow = true, Width = 16 });
            MyColumns.Add("reviewflagname", new MyColumn("reviewflagname", "是否复核") { BShow = false });
            MyColumns.Add("sum_num", new MyColumn("sum_num", "总行数") { BShow = false });

            _tableview.MyColumns = MyColumns;
            _tableview.BShowState = true;
            _tableview.StateRowEnvent += new UcTableOperableView_NoTitle.StateRowEventHandler(_tableview_StateRowEnvent);
        }

        private void _query_Click(object sender, RoutedEventArgs e)
        {
            if (reportDate_kssj.SelectedDate.Value.Date > reportDate_jssj.SelectedDate.Value.Date)
            {
                Toolkit.MessageBox.Show("开始时间大于结束时间，请重新选择！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            _tableview.GetDataByPageNumberEvent += new UcTableOperableView_NoTitle.GetDataByPageNumberEventHandler(_tableview_GetDataByPageNumberEvent);
            GetData();
            _sj.Visibility = Visibility.Visible;
            _hj.Visibility = Visibility.Visible;
            _title.Text = _tableview.RowTotal.ToString();
            _tableview.PageIndex = 1;

            if (_tableview.RowTotal == 0)
            {
                Toolkit.MessageBox.Show("没有查询到数据！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

        }

        public void GetData()
        {
            DataTable table = dbOperation.GetDbHelper().GetDataSet(string.Format("call p_review_details_cy('{0}','{1}','{2}','{3}','{4}',{5},{6})",
                               (Application.Current.Resources["User"] as UserInfo).ID, reportDate_kssj.SelectedDate, reportDate_jssj.SelectedDate,
                               _detect_dept.SelectedIndex < 1 ? "" : (_detect_dept.SelectedItem as Label).Tag,
                               _detect_item.SelectedIndex < 1 ? "" : (_detect_item.SelectedItem as Label).Tag,
                              (_tableview.PageIndex - 1) * _tableview.RowMax,
                              _tableview.RowMax)).Tables[0];

            _tableview.Table = table;
            current_table = table;
        }

        void _tableview_GetDataByPageNumberEvent()
        {
            GetData();
        }

        void _tableview_StateRowEnvent(string id)
        {
            int orderid = int.Parse(id);
            AddReviewDetailsCy det = new AddReviewDetailsCy(dbOperation, orderid, this);
            det.ShowDialog();

        }

        private void _export_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = dbOperation.GetDbHelper().GetDataSet(string.Format("call p_review_details_sc('{0}','{1}','{2}','{3}','{4}',{5},{6})",
                               (Application.Current.Resources["User"] as UserInfo).ID, reportDate_kssj.SelectedDate, reportDate_jssj.SelectedDate,
                               _detect_dept.SelectedIndex < 1 ? "" : (_detect_dept.SelectedItem as Label).Tag,
                               _detect_item.SelectedIndex < 1 ? "" : (_detect_item.SelectedItem as Label).Tag,
                              0,
                              _tableview.RowTotal)).Tables[0];

            _tableview.ExportExcel(table);
        }
    }
}

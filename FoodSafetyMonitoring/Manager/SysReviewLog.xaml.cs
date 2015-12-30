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

using FoodSafetyMonitoring.Common;
using Toolkit = Microsoft.Windows.Controls;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// SysReviewLog.xaml 的交互逻辑
    /// </summary>
    public partial class SysReviewLog : UserControl
    {
        private IDBOperation dbOperation;
        private string user_flag_tier;
        private string dept_type;
        private Dictionary<string, MyColumn> MyColumns = new Dictionary<string, MyColumn>();

        public SysReviewLog(IDBOperation dbOperation, string depttype)
        {
            InitializeComponent();

            this.dbOperation = dbOperation;
            this.dept_type = depttype;
            user_flag_tier = (Application.Current.Resources["User"] as UserInfo).FlagTier;

            //switch (user_flag_tier)
            //{
            //    case "0": _dept_name.Text = "选择省:";
            //        break;
            //    case "1": _dept_name.Text = "选择市(州):";
            //        break;
            //    case "2": _dept_name.Text = "选择区县:";
            //        break;
            //    case "3": _dept_name.Text = "选择检测单位:";
            //        break;
            //    case "4": _dept_name.Text = "选择检测单位:";
            //        break;
            //    default: break;
            //}

            //初始化查询条件
            reportDate_kssj.SelectedDate = DateTime.Now.AddDays(-1);
            reportDate_jssj.SelectedDate = DateTime.Now;
            //检测单位
            ComboboxTool.InitComboboxSource(_detect_dept, string.Format("call p_dept_cxtj('{0}','{1}')", (Application.Current.Resources["User"] as UserInfo).ID, dept_type), "cxtj");
            //检测项目
            ComboboxTool.InitComboboxSource(_detect_item, "SELECT ItemID,ItemNAME FROM t_det_item WHERE  OPENFLAG = '1' order by orderId", "cxtj");

            MyColumns.Add("orderid", new MyColumn("orderid", "检测单编号") { BShow = true, Width = 10 });
            MyColumns.Add("itemname", new MyColumn("itemname", "检测项目") { BShow = true, Width = 14 });
            MyColumns.Add("reviewdate", new MyColumn("reviewdate", "复核检测时间") { BShow = true, Width = 18 });
            MyColumns.Add("reviewreagentname", new MyColumn("reviewreagentname", "复核检测方法") { BShow = true, Width = 14 });
            MyColumns.Add("reviewresultname", new MyColumn("reviewresultname", "复核检测结果") { BShow = true, Width = 14 });
            MyColumns.Add("reviewusername", new MyColumn("reviewusername", "复核检测师") { BShow = true, Width = 14 });
            MyColumns.Add("reviewreason", new MyColumn("reviewreason", "复核原因说明") { BShow = true, Width = 22 });
            MyColumns.Add("sum_num", new MyColumn("sum_num", "总行数") { BShow = false });

            _tableview.MyColumns = MyColumns;
            _tableview.BShowDetails = true;
            _tableview.DetailsRowEnvent += new UcTableOperableView_NoTitle.DetailsRowEventHandler(_tableview_DetailsRowEnvent);
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

        private void GetData()
        {
            DataTable table = dbOperation.GetDbHelper().GetDataSet(string.Format("call p_review_log('{0}','{1}','{2}','{3}','{4}','{5}',{6},{7})",
                              (Application.Current.Resources["User"] as UserInfo).ID, reportDate_kssj.SelectedDate, reportDate_jssj.SelectedDate,
                               _detect_dept.SelectedIndex < 1 ? "" : (_detect_dept.SelectedItem as Label).Tag,
                               _detect_item.SelectedIndex < 1 ? "" : (_detect_item.SelectedItem as Label).Tag,
                               dept_type,
                              (_tableview.PageIndex - 1) * _tableview.RowMax,
                              _tableview.RowMax)).Tables[0];
            _tableview.Table = table;
        }

        void _tableview_GetDataByPageNumberEvent()
        {
            GetData();
        }

        void _tableview_DetailsRowEnvent(string id)
        {
            int orderid = int.Parse(id);
            switch (dept_type)
            {
                case "0": detectDetailsReview det = new detectDetailsReview(dbOperation, orderid);
                    det.ShowDialog();
                    break;
                case "1": detectDetailsReviewLt detlt = new detectDetailsReviewLt(dbOperation, orderid);
                    detlt.ShowDialog();
                    break;
                case "2": detectDetailsReviewCy detcy = new detectDetailsReviewCy(dbOperation, orderid);
                    detcy.ShowDialog();
                    break;
                case "3": detectDetailsReviewSchool detschool = new detectDetailsReviewSchool(dbOperation, orderid);
                    detschool.ShowDialog();
                    break;
                default: break;
            }
            
        }

        private void _export_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = dbOperation.GetDbHelper().GetDataSet(string.Format("call p_review_log('{0}','{1}','{2}','{3}','{4}','{5}',{6},{7})",
                              (Application.Current.Resources["User"] as UserInfo).ID, reportDate_kssj.SelectedDate, reportDate_jssj.SelectedDate,
                               _detect_dept.SelectedIndex < 1 ? "" : (_detect_dept.SelectedItem as Label).Tag,
                               _detect_item.SelectedIndex < 1 ? "" : (_detect_item.SelectedItem as Label).Tag,
                               dept_type,
                               0,
                               _tableview.RowTotal)).Tables[0];

            _tableview.ExportExcel(table);
        }

    }
}


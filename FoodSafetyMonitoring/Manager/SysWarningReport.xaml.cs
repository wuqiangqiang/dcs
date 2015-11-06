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
using FoodSafetyMonitoring.dao;
using System.Windows.Forms.Integration;
using System.Data;
using FoodSafetyMonitoring.Common;

using FoodSafetyMonitoring.Manager.UserControls;
using Toolkit = Microsoft.Windows.Controls;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// SysWarningReport.xaml 的交互逻辑
    /// </summary>
    public partial class SysWarningReport : UserControl
    {
        private IDBOperation dbOperation;
        private DataTable current_table;
        private Dictionary<string, MyColumn> MyColumns = new Dictionary<string, MyColumn>();
        private string user_flag_tier;
        private string item_id;
        private string review_id;
        private string detect_huanjie;
        //private string dept_name;

        public SysWarningReport(IDBOperation dbOperation)
        {
            InitializeComponent();

            this.dbOperation = dbOperation;
            user_flag_tier = (Application.Current.Resources["User"] as UserInfo).FlagTier;

            //初始化查询条件
            reportDate_kssj.SelectedDate = DateTime.Now.AddDays(-1);
            reportDate_jssj.SelectedDate = DateTime.Now;
            ////检测单位
            //switch (user_flag_tier)
            //{
            //    case "0": _dept_name.Text = "省:";
            //        dept_name = "省名称";
            //        break;
            //    case "1": _dept_name.Text = "市(州):";
            //        dept_name = "市(州)单位名称";
            //        break;
            //    case "2": _dept_name.Text = "区县:";
            //        dept_name = "区县名称";
            //        break;
            //    case "3": _dept_name.Text = "检测单位:";
            //        dept_name = "检测单位名称";
            //        break;
            //    case "4": _dept_name.Text = "检测单位:";
            //        dept_name = "检测单位名称";
            //        break;
            //    default: break;
            //}
            ComboboxTool.InitComboboxSource(_detect_dept, string.Format("call p_dept_cxtj_all('{0}')", (Application.Current.Resources["User"] as UserInfo).ID), "cxtj");
            //检测项目
            ComboboxTool.InitComboboxSource(_detect_item, "SELECT ItemID,ItemNAME FROM t_det_item WHERE OPENFLAG = '1' order by orderId", "cxtj");
            //检测环节
            ComboboxTool.InitComboboxSource(_detect_huanjie, "SELECT typeID,typeName FROM t_dept_type WHERE openFlag = '1'", "cxtj");
            //复核状态
            DataTable table_detect_result = new DataTable();
            table_detect_result.Columns.Add("id", Type.GetType("System.String"));
            table_detect_result.Columns.Add("name", Type.GetType("System.String"));
            table_detect_result.Rows.Add(new object[] { "0", "未复核" });
            table_detect_result.Rows.Add(new object[] { "1", "已复核" });
            ComboboxTool.InitComboboxSource(_review_flag, table_detect_result, "cxtj");

            
            _tableview.BShowDetails = true;
            _tableview.DetailsRowEnvent += new UcTableOperableView_NoPages.DetailsRowEventHandler(_tableview_DetailsRowEnvent);
        }

        private void _query_Click(object sender, RoutedEventArgs e)
        {
            grid_info.Children.Clear();
            grid_info.Children.Add(_tableview);
            MyColumns.Clear();

            if (reportDate_kssj.SelectedDate.Value.Date > reportDate_jssj.SelectedDate.Value.Date)
            {
                Toolkit.MessageBox.Show("开始时间大于结束时间，请重新选择！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            MyColumns.Add("zj", new MyColumn("zj", "序号") { BShow = false, Width = 5 });
            MyColumns.Add("partid", new MyColumn("partid", "检测单位id") { BShow = false });
            MyColumns.Add("partname", new MyColumn("partname", "部门名称") { BShow = true, Width = 18 });
            MyColumns.Add("flagtier", new MyColumn("flagtier", "部门级别") { BShow = false });
            MyColumns.Add("yang", new MyColumn("yang", "阳性预警数") { BShow = true, Width = 12 });
            MyColumns.Add("yang_like", new MyColumn("yang_like", "疑似阳性预警数") { BShow = true, Width = 14 });
            MyColumns.Add("count", new MyColumn("count", "预警数合计") { BShow = true, Width = 12 });
            MyColumns.Add("sum_num", new MyColumn("sum_num", "总行数") { BShow = false });
            MyColumns.Add("depttype", new MyColumn("depttype", "检测单位级别") { BShow = false });

            item_id = _detect_item.SelectedIndex < 1 ? "" : (_detect_item.SelectedItem as Label).Tag.ToString();
            review_id = _review_flag.SelectedIndex < 1 ? "" : (_review_flag.SelectedItem as Label).Tag.ToString();
            detect_huanjie = _detect_huanjie.SelectedIndex < 1 ? "" : (_detect_huanjie.SelectedItem as Label).Tag.ToString();

            switch (review_id)
            {
                case "0": MyColumns.Add("review_yes", new MyColumn("review_yes", "已复核数") { BShow = false, Width = 12 });
                    MyColumns.Add("review_no", new MyColumn("review_no", "未复核数") { BShow = true, Width = 12 });
                    break;
                case "1": MyColumns.Add("review_yes", new MyColumn("review_yes", "已复核数") { BShow = true, Width = 12 });
                    MyColumns.Add("review_no", new MyColumn("review_no", "未复核数") { BShow = false, Width = 12 });
                    break;
                case "": MyColumns.Add("review_yes", new MyColumn("review_yes", "已复核数") { BShow = true, Width = 12 });
                    MyColumns.Add("review_no", new MyColumn("review_no", "未复核数") { BShow = true, Width = 12 });
                    break;
                default: break;
            }

            _tableview.MyColumns = MyColumns;
            GetData();

            
            //_sj.Visibility = Visibility.Visible;
            //_hj.Visibility = Visibility.Visible;
            //_title.Text = current_table.Rows.Count.ToString();

            if (current_table.Rows.Count == 0)
            {
                Toolkit.MessageBox.Show("没有查询到数据！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }

        private void GetData()
        {
            DataTable table = dbOperation.GetDbHelper().GetDataSet(string.Format("call p_warning_report('{0}','{1}','{2}','{3}','{4}','{5}','{6}')",
                             (Application.Current.Resources["User"] as UserInfo).ID, reportDate_kssj.SelectedDate, reportDate_jssj.SelectedDate,
                              _detect_dept.SelectedIndex < 1 ? "" : (_detect_dept.SelectedItem as Label).Tag,
                              item_id, review_id, detect_huanjie)).Tables[0];
            _tableview.Table = table;
            current_table = table;
        }

        void _tableview_GetDataByPageNumberEvent()
        {
            GetData();
        }

        void _tableview_DetailsRowEnvent(string id)
        {
            string dept_id;
            string flag_tier;
            string dept_type;

            dept_id = id;
            DataRow[] rows = current_table.Select("PartId = '" + id + "'");
            flag_tier = rows[0]["flagtier"].ToString();
            dept_type = rows[0]["depttype"].ToString();

            if (flag_tier == "4")
            {
                switch (dept_type)
                {
                    case "0": grid_info.Children.Add(new UcWarningReportDetailsSc(dbOperation, reportDate_kssj.SelectedDate.ToString(), reportDate_jssj.SelectedDate.ToString(), dept_id, item_id, review_id));
                        break;
                    case "1":
                    case "2":
                    case "3":
                    default: break;
                }
            }
            else
            {
                grid_info.Children.Add(new UcWarningReportCountry(dbOperation, reportDate_kssj.SelectedDate.ToString(), reportDate_jssj.SelectedDate.ToString(), dept_id, item_id, review_id, detect_huanjie));
            }
        }

        private void _export_Click(object sender, RoutedEventArgs e)
        {
            _tableview.ExportExcel();
        }
    }
}

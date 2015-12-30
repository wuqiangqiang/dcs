﻿using System;
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
using System.Data;
using FoodSafetyMonitoring.Common;
using Toolkit = Microsoft.Windows.Controls;
using FoodSafetyMonitoring.Manager.UserControls;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// SysNewDetectSchoolQuery.xaml 的交互逻辑
    /// </summary>
    public partial class SysNewDetectSchoolQuery : UserControl
    {
        DataTable ProvinceCityTable;
        public IDBOperation dbOperation = null;
        private Dictionary<string, MyColumn> MyColumns = new Dictionary<string, MyColumn>();

        string userId = (Application.Current.Resources["User"] as UserInfo).ID;
        public SysNewDetectSchoolQuery(IDBOperation dbOperation)
        {
            InitializeComponent();
            this.dbOperation = dbOperation;
            ProvinceCityTable = Application.Current.Resources["省市表"] as DataTable;
            DataRow[] rows = ProvinceCityTable.Select("pid = '0001'");

            //画面初始化-检测单列表画面
            dtpStartDate.SelectedDate = DateTime.Now.AddDays(-1);
            dtpEndDate.SelectedDate = DateTime.Now;
            ComboboxTool.InitComboboxSource(_source_company1, string.Format(" call p_user_company('{0}','') ", userId), "cxtj");
            ComboboxTool.InitComboboxSource(_detect_station, string.Format("call p_user_dept('{0}','{1}')", userId, "0"), "cxtj");
            ComboboxTool.InitComboboxSource(_detect_item1, "SELECT ItemID,ItemNAME FROM t_det_item WHERE  OPENFLAG = '1' order by orderId", "cxtj");
            ComboboxTool.InitComboboxSource(_detect_object1, "SELECT objectId,objectName FROM t_det_object WHERE  OPENFLAG = '1'", "cxtj");
            ComboboxTool.InitComboboxSource(_detect_result1, "SELECT resultId,resultName FROM t_det_result where openFlag = '1' ORDER BY id", "cxtj");
            ComboboxTool.InitComboboxSource(_detect_person1, string.Format("call p_user_detuser('{0}','{1}')", userId, "0"), "cxtj");
            ComboboxTool.InitComboboxSource(_detect_method, "select reagentId,reagentName from t_det_reagent where openFlag = '1'", "cxtj");
            ComboboxTool.InitComboboxSource(_detect_type, "SELECT sourceId,sourceName FROM t_det_source where openFlag = '1'", "cxtj");

            ComboboxTool.InitComboboxSource(_province1, rows, "cxtj");
            _province1.SelectionChanged += new SelectionChangedEventHandler(_province1_SelectionChanged);
            //20150707检测师改为连动（受监测站点影响）
            _detect_station.SelectionChanged += new SelectionChangedEventHandler(_detect_station_SelectionChanged);

            SetColumns();
        }

        private void SetColumns()
        {
            MyColumns.Add("orderid", new MyColumn("orderid", "检测单编号") { BShow = true, Width = 10 });
            MyColumns.Add("detecttype", new MyColumn("detecttype", "数据来源id") { BShow = false });
            MyColumns.Add("detecttypename", new MyColumn("detecttypename", "数据来源") { BShow = true, Width = 8 });
            MyColumns.Add("detectdate", new MyColumn("detectdate", "检测时间") { BShow = true, Width = 18 });
            MyColumns.Add("deptid", new MyColumn("deptid", "检测单位id") { BShow = false });
            MyColumns.Add("deptname", new MyColumn("deptname", "检测单位") { BShow = true, Width = 16 });
            MyColumns.Add("itemid", new MyColumn("itemid", "检测项目id") { BShow = false });
            MyColumns.Add("itemname", new MyColumn("itemname", "检测项目") { BShow = true, Width = 12 });
            MyColumns.Add("objectid", new MyColumn("objectid", "检测对象id") { BShow = false });
            MyColumns.Add("objectname", new MyColumn("objectname", "检测对象") { BShow = true, Width = 8 });
            MyColumns.Add("detectvalue", new MyColumn("detectvalue", "检测值") { BShow = true, Width = 8 });
            MyColumns.Add("reagentid", new MyColumn("companyid", "检测方法id") { BShow = false });
            MyColumns.Add("reagentname", new MyColumn("reagentname", "检测方法") { BShow = true, Width = 10 });
            MyColumns.Add("resultid", new MyColumn("resultid", "检测结果id") { BShow = false });
            MyColumns.Add("resultname", new MyColumn("resultname", "检测结果") { BShow = true, Width = 10 });
            MyColumns.Add("detectuserid", new MyColumn("detectuserid", "检测师id") { BShow = false });
            MyColumns.Add("areaname", new MyColumn("areaname", "来源产地") { BShow = true, Width = 18 });
            MyColumns.Add("companyid", new MyColumn("companyid", "被检单位id") { BShow = false });
            MyColumns.Add("companyname", new MyColumn("companyname", "被检单位") { BShow = true, Width = 18 });
            MyColumns.Add("sum_num", new MyColumn("sum_num", "总行数") { BShow = false });

            _tableview.MyColumns = MyColumns;
            _tableview.BShowModify = false;
            _tableview.BShowDetails = true;

            if ((Application.Current.Resources["User"] as UserInfo).FlagTier == "0")
            {
                _tableview.BShowDelete = true;
            }
            else
            {
                _tableview.BShowDelete = false;
            }

            _tableview.DetailsRowEnvent += new UcTableOperableView_NoTitle.DetailsRowEventHandler(_tableview_DetailsRowEnvent);
            _tableview.DeleteRowEnvent += new UcTableOperableView_NoTitle.DeleteRowEventHandler(_tableview_DeleteRowEnvent);
        }

        void _province1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_province1.SelectedIndex > 0)
            {
                DataRow[] rows = ProvinceCityTable.Select("pid = '" + (_province1.SelectedItem as Label).Tag.ToString() + "'");
                ComboboxTool.InitComboboxSource(_city1, rows, "cxtj");
                //20150707被检单位改为连动（受来源产地影响）
                ComboboxTool.InitComboboxSource(_source_company1, string.Format(" call p_user_company('{0}','{1}') ", userId, (_province1.SelectedItem as Label).Tag.ToString()), "cxtj");
                _city1.SelectionChanged += new SelectionChangedEventHandler(_city1_SelectionChanged);
            }
        }


        void _city1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_city1.SelectedIndex > 0)
            {
                DataRow[] rows = ProvinceCityTable.Select("pid = '" + (_city1.SelectedItem as Label).Tag.ToString() + "'");
                ComboboxTool.InitComboboxSource(_region1, rows, "cxtj");
                //20150707被检单位改为连动（受来源产地影响）
                ComboboxTool.InitComboboxSource(_source_company1, string.Format(" call p_user_company('{0}','{1}') ", userId, (_city1.SelectedItem as Label).Tag.ToString()), "cxtj");
                _region1.SelectionChanged += new SelectionChangedEventHandler(_region1_SelectionChanged);
            }
        }

        //20150707被检单位改为连动（受来源产地影响）
        void _region1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_region1.SelectedIndex > 0)
            {
                ComboboxTool.InitComboboxSource(_source_company1, string.Format("call p_user_company('{0}','{1}')", userId, (_region1.SelectedItem as Label).Tag.ToString()), "cxtj");
            }
        }

        //20150707检测师改为连动（受检测单位影响）
        void _detect_station_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_detect_station.SelectedIndex > 0)
            {
                ComboboxTool.InitComboboxSource(_detect_person1, string.Format("SELECT RECO_PKID,INFO_USER,NUMB_USER FROM sys_client_user where fk_dept = '{0}'", (_detect_station.SelectedItem as Label).Tag.ToString()), "cxtj");
            }
            else if (_detect_station.SelectedIndex == 0)
            {
                ComboboxTool.InitComboboxSource(_detect_person1, string.Format("call p_user_detuser('{0}')", userId), "cxtj");
            }
        }

        private void _query_Click(object sender, RoutedEventArgs e)
        {
            if (dtpStartDate.SelectedDate.Value.Date > dtpEndDate.SelectedDate.Value.Date)
            {
                Toolkit.MessageBox.Show("开始日期大于结束日期，请重新选择！", "系统提示", MessageBoxButton.OK);
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
                Toolkit.MessageBox.Show("没有查询到数据！", "系统提示", MessageBoxButton.OK);
                return;
            }
        }

        private void GetData()
        {
            DataTable table = dbOperation.GetDbHelper().GetDataSet(string.Format("call p_query_detect_school({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}',{14},{15})",
                  (Application.Current.Resources["User"] as UserInfo).ID,
                //dtpStartDate.Value.ToString() == dtpEndDate.Value.ToString() ? "" : dtpStartDate.Value.ToString(),
                //dtpStartDate.Value.ToString() == dtpEndDate.Value.ToString() ? "" : dtpEndDate.Value.ToString(),
                  ((DateTime)dtpStartDate.SelectedDate).ToShortDateString(),
                  ((DateTime)dtpEndDate.SelectedDate).ToShortDateString(),
                  _province1.SelectedIndex < 1 ? "" : (_province1.SelectedItem as Label).Tag,
                  _city1.SelectedIndex < 1 ? "" : (_city1.SelectedItem as Label).Tag,
                  _region1.SelectedIndex < 1 ? "" : (_region1.SelectedItem as Label).Tag,
                  _source_company1.SelectedIndex < 1 ? "" : (_source_company1.SelectedItem as Label).Tag,
                   _detect_station.SelectedIndex < 1 ? "" : (_detect_station.SelectedItem as Label).Tag,
                  _detect_item1.SelectedIndex < 1 ? "" : (_detect_item1.SelectedItem as Label).Tag,
                  _detect_object1.SelectedIndex < 1 ? "" : (_detect_object1.SelectedItem as Label).Tag,
                  _detect_result1.SelectedIndex < 1 ? "" : (_detect_result1.SelectedItem as Label).Tag,
                  _detect_method.SelectedIndex < 1 ? "" : (_detect_method.SelectedItem as Label).Tag,
                  _detect_person1.SelectedIndex < 1 ? "" : (_detect_person1.SelectedItem as Label).Tag,
                  _detect_type.SelectedIndex < 1 ? "" : (_detect_type.SelectedItem as Label).Tag,
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
            detectdetailsSchool det = new detectdetailsSchool(dbOperation, orderid);
            det.ShowDialog();
        }

        void _tableview_DeleteRowEnvent(string id)
        {
            if (Toolkit.MessageBox.Show("确定要删除该条检测单吗？", "系统询问", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    bool reviewflag = dbOperation.GetDbHelper().Exists(string.Format("select count(id) from t_detect_review where DetectId = '{0}'", id));
                    if (reviewflag)
                    {
                        int result1 = dbOperation.GetDbHelper().ExecuteSql(string.Format("delete from t_detect_review where DetectId ='{0}'", id));
                        if (result1 <= 0)
                        {
                            Toolkit.MessageBox.Show("删除失败！", "系统提示", MessageBoxButton.OK);
                            return;
                        }
                    }
                    int result = dbOperation.GetDbHelper().ExecuteSql(string.Format("delete from t_detect_report where ORDERID ='{0}'", id));
                    if (result > 0)
                    {
                        Toolkit.MessageBox.Show("删除成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                        GetData();
                    }
                    else
                    {
                        Toolkit.MessageBox.Show("删除失败！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }
                catch
                {
                    Toolkit.MessageBox.Show("删除失败2！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
        }

        private void _export_Click(object sender, RoutedEventArgs e)
        {
            DataTable table = dbOperation.GetDbHelper().GetDataSet(string.Format("call p_query_detect_school({0},'{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}',{14},{15})",
                  (Application.Current.Resources["User"] as UserInfo).ID,
                //dtpStartDate.Value.ToString() == dtpEndDate.Value.ToString() ? "" : dtpStartDate.Value.ToString(),
                //dtpStartDate.Value.ToString() == dtpEndDate.Value.ToString() ? "" : dtpEndDate.Value.ToString(),
                  ((DateTime)dtpStartDate.SelectedDate).ToShortDateString(),
                  ((DateTime)dtpEndDate.SelectedDate).ToShortDateString(),
                  _province1.SelectedIndex < 1 ? "" : (_province1.SelectedItem as Label).Tag,
                  _city1.SelectedIndex < 1 ? "" : (_city1.SelectedItem as Label).Tag,
                  _region1.SelectedIndex < 1 ? "" : (_region1.SelectedItem as Label).Tag,
                  _source_company1.SelectedIndex < 1 ? "" : (_source_company1.SelectedItem as Label).Tag,
                   _detect_station.SelectedIndex < 1 ? "" : (_detect_station.SelectedItem as Label).Tag,
                  _detect_item1.SelectedIndex < 1 ? "" : (_detect_item1.SelectedItem as Label).Tag,
                  _detect_object1.SelectedIndex < 1 ? "" : (_detect_object1.SelectedItem as Label).Tag,
                  _detect_result1.SelectedIndex < 1 ? "" : (_detect_result1.SelectedItem as Label).Tag,
                  _detect_method.SelectedIndex < 1 ? "" : (_detect_method.SelectedItem as Label).Tag,
                  _detect_person1.SelectedIndex < 1 ? "" : (_detect_person1.SelectedItem as Label).Tag,
                  _detect_type.SelectedIndex < 1 ? "" : (_detect_type.SelectedItem as Label).Tag,
                  0,
                  _tableview.RowTotal)).Tables[0];

            _tableview.ExportExcel(table);
        }

    }
}

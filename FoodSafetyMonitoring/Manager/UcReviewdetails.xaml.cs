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
using System.Data;
using FoodSafetyMonitoring.dao;
using FoodSafetyMonitoring.Manager.UserControls;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// UcReviewInfo.xaml 的交互逻辑
    /// </summary>
    public partial class UcReviewdetails : UserControl
    {
        private IDBOperation dbOperation;
        private Dictionary<string, MyColumn> MyColumns = new Dictionary<string, MyColumn>();
        public string Kssj { get; set; }
        public string Jssj { get; set; }
        public string DeptId { get; set; }
        public string ItemId { get; set; }
        public string ReviewId { get; set; }

        public UcReviewdetails(IDBOperation dbOperation, string kssj, string jssj, string deptId, string itemId, string reviewId)
        {
            InitializeComponent();

            this.dbOperation = dbOperation;
            this.Kssj = kssj;
            this.Jssj = jssj;
            this.DeptId = deptId;
            this.ItemId = itemId;
            this.ReviewId = reviewId;

            MyColumns.Add("orderid", new MyColumn("orderid", "检测单编号") { BShow = true, Width = 8 });
            MyColumns.Add("detecttypename", new MyColumn("detecttypename", "信息来源") { BShow = true, Width = 8 });
            MyColumns.Add("detectdate", new MyColumn("detectdate", "检测时间") { BShow = true, Width = 16 });
            MyColumns.Add("partname", new MyColumn("partname", "检测单位") { BShow = true, Width = 12 });
            MyColumns.Add("itemname", new MyColumn("itemname", "检测项目") { BShow = true, Width = 10 });
            MyColumns.Add("objectname", new MyColumn("objectname", "检测对象") { BShow = true, Width = 8 });
            MyColumns.Add("samplename", new MyColumn("samplename", "检测样本") { BShow = true, Width = 8 });
            MyColumns.Add("sensitivityname", new MyColumn("sensitivityname", "检测灵敏度") { BShow = true, Width = 10 });
            MyColumns.Add("reagentname", new MyColumn("reagentname", "检测方法") { BShow = true, Width = 10 });
            MyColumns.Add("resultname", new MyColumn("resultname", "检测结果") { BShow = true, Width = 10 });
            MyColumns.Add("detectusername", new MyColumn("detectusername", "检测师") { BShow = true, Width = 10 });
            MyColumns.Add("areaname", new MyColumn("areaname", "来源产地") { BShow = false, Width = 14 });
            MyColumns.Add("companyname", new MyColumn("companyname", "被检单位") { BShow = true, Width = 12 });
            MyColumns.Add("reviewflagname", new MyColumn("reviewflagname", "是否复核") { BShow = false });
            MyColumns.Add("sum_num", new MyColumn("sum_num", "总行数") { BShow = false });

            _tableview.MyColumns = MyColumns;
            _tableview.BShowState = true;
            _tableview.GetDataByPageNumberEvent += new UcTableOperableView_NoTitle.GetDataByPageNumberEventHandler(_tableview_GetDataByPageNumberEvent);
            _tableview.StateRowEnvent += new UcTableOperableView_NoTitle.StateRowEventHandler(_tableview_StateRowEnvent);
            _tableview.PageIndex = 1;
            GetData();
        }

        private void GetData()
        {
            DataTable table = dbOperation.GetDbHelper().GetDataSet(string.Format("call p_review_details('{0}','{1}','{2}','{3}','{4}',{5},{6})",
                                 Kssj, Jssj, DeptId, ItemId, ReviewId,
                              (_tableview.PageIndex - 1) * _tableview.RowMax,
                              _tableview.RowMax)).Tables[0];

            _tableview.Table = table;
        }

        void _tableview_GetDataByPageNumberEvent()
        {
            GetData();
        }

        void _tableview_StateRowEnvent(string id)
        {
            int orderid = int.Parse(id);
            detectDetailsReview det = new detectDetailsReview(dbOperation, orderid);
            det.ShowDialog();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }

        //private void btnDetails_Click(object sender, RoutedEventArgs e)
        //{
        //    int id = int.Parse((sender as Button).Tag.ToString());
        //    detectDetailsReview det = new detectDetailsReview(dbOperation, id);
        //    det.Show();
        //}
    }
}

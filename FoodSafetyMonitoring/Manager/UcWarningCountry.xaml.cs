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
    /// UcWarningCountry.xaml 的交互逻辑
    /// </summary>
    public partial class UcWarningCountry : UserControl
    {
        private IDBOperation dbOperation;
        private DataTable current_table;
        private Dictionary<string, MyColumn> MyColumns = new Dictionary<string, MyColumn>();
        private string user_flag_tier;

        public string DeptId { get; set; }
        public string ItemId { get; set; }
        public string ObjectId { get; set; }
        public string DeptType { get; set; }
        public UcWarningCountry(IDBOperation dbOperation,string dept_id,string item_id,string object_id,string dept_type)
        {
            InitializeComponent();
            this.dbOperation = dbOperation;
            this.DeptId = dept_id;
            this.ItemId = item_id;
            this.ObjectId = object_id;
            this.DeptType = dept_type;
            user_flag_tier = (Application.Current.Resources["User"] as UserInfo).FlagTier;

            MyColumns.Add("zj", new MyColumn("zj", "主键") { BShow = false });
            MyColumns.Add("partid", new MyColumn("partid", "检测单位id") { BShow = false });
            MyColumns.Add("partname", new MyColumn("partname", "部门名称") { BShow = true, Width = 18 });
            MyColumns.Add("itemid", new MyColumn("itemid", "检测项目id") { BShow = false });
            MyColumns.Add("itemname", new MyColumn("itemname", "检测项目") { BShow = true, Width = 14 });
            MyColumns.Add("objectid", new MyColumn("objectid", "检测对象id") { BShow = false });
            MyColumns.Add("objectname", new MyColumn("objectname", "检测对象") { BShow = true, Width = 12 });
            MyColumns.Add("yang_like", new MyColumn("yang_like", "疑似阳性") { BShow = true, Width = 12 });
            MyColumns.Add("yang", new MyColumn("yang", "阳性") { BShow = true, Width = 12 });
            MyColumns.Add("count", new MyColumn("count", "合计数量") { BShow = true, Width = 12 });
            MyColumns.Add("sum_num", new MyColumn("sum_num", "总行数") { BShow = false });
            MyColumns.Add("flagtier", new MyColumn("flagtier", "部门级别") { BShow = false });

            _tableview.MyColumns = MyColumns;
            _tableview.BShowDetails = true;

            _tableview.DetailsRowEnvent += new UcTableOperableView_NoTitle.DetailsRowEventHandler(_tableview_DetailsRowEnvent);
            _tableview.GetDataByPageNumberEvent += new UcTableOperableView_NoTitle.GetDataByPageNumberEventHandler(_tableview_GetDataByPageNumberEvent);
            GetData();
        }

        private void GetData()
        {
            DataTable table = dbOperation.GetDbHelper().GetDataSet(string.Format("call p_warning_info_country('{0}','{1}','{2}','{3}',{4},{5})",
                              DeptId, ItemId, ObjectId,DeptType,
                              (_tableview.PageIndex - 1) * _tableview.RowMax,
                              _tableview.RowMax)).Tables[0];

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
            //string item_id;
            //string object_id;
            string flag_tier;

            int selectrow = int.Parse(id);

            dept_id = current_table.Rows[selectrow - 1][1].ToString();
            //item_id = current_table.Rows[selectrow - 1][3].ToString();
            //object_id = current_table.Rows[selectrow - 1][5].ToString();
            flag_tier = current_table.Rows[selectrow - 1][11].ToString();

            if (flag_tier == "4")
            {
                switch (DeptType)
                {
                    case "0": UcWarningdetailsSc daydetails = new UcWarningdetailsSc(dbOperation, dept_id, ItemId, ObjectId);
                        daydetails.SetValue(Grid.RowProperty, 0);
                        daydetails.SetValue(Grid.RowSpanProperty, 2);
                        grid_info.Children.Add(daydetails);
                        break;
                    case "1":
                    case "2":
                    case "3":
                    default: break;
                }
            }
            else
            {
                UcWarningCountry daydetails = new UcWarningCountry(dbOperation, dept_id, ItemId, ObjectId,DeptType);
                daydetails.SetValue(Grid.RowProperty, 0);
                daydetails.SetValue(Grid.RowSpanProperty, 2);

                grid_info.Children.Add(daydetails);
            } 
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
        }
    }
}

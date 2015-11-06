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
using FoodSafetyMonitoring.Common;
using System.Data;
using FoodSafetyMonitoring.Manager.UserControls;
using Toolkit = Microsoft.Windows.Controls;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// SysSamplingReport.xaml 的交互逻辑
    /// </summary>
    public partial class SysSamplingReport : UserControl
    {
        private IDBOperation dbOperation;
        private string user_flag_tier;
        private string item_id;
        private DataTable currenttable;
        private string dept_type;
        private List<SamplingInfo> list = new List<SamplingInfo>();
        private Dictionary<string, MyColumn> MyColumns = new Dictionary<string, MyColumn>();
        private readonly List<string> year = new List<string>() { "2014",
            "2015", 
            "2016",
            "2017",
            "2018",
            "2019",
            "2020"};//初始化变量

        public SysSamplingReport(IDBOperation dbOperation, string depttype)
        {
            InitializeComponent();

            this.dbOperation = dbOperation;
            this.dept_type = depttype;
            user_flag_tier = (Application.Current.Resources["User"] as UserInfo).FlagTier;

            _year.ItemsSource = year;
            _year.SelectedIndex = 1;

            ////检测单位
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
            ComboboxTool.InitComboboxSource(_detect_dept, string.Format("call p_dept_cxtj('{0}','{1}')", (Application.Current.Resources["User"] as UserInfo).ID, dept_type), "cxtj");
            //检测项目
            ComboboxTool.InitComboboxSource(_detect_item, "SELECT ItemID,ItemNAME FROM t_det_item WHERE  OPENFLAG = '1' order by orderId", "cxtj");

            //如果登录用户的部门是站点级别，则将查询条件检测单位赋上默认值
            if (user_flag_tier == "4")
            {
                _detect_dept.SelectedIndex = 1;
            }

            _tableview.DetailsRowEnvent += new UcTableOperableView_NoPages.DetailsRowEventHandler(_tableview_DetailsRowEnvent);
        }

        private void _query_Click(object sender, RoutedEventArgs e)
        {
            item_id = _detect_item.SelectedIndex < 1 ? "" : (_detect_item.SelectedItem as Label).Tag.ToString();

            grid_info.Children.Clear();
            grid_info.Children.Add(_tableview);
            MyColumns.Clear();

            DataTable table = dbOperation.GetDbHelper().GetDataSet(string.Format("call p_sampling_report('{0}','{1}','{2}','{3}','{4}')",
                              (Application.Current.Resources["User"] as UserInfo).ID,
                               _year.Text,
                               _detect_dept.SelectedIndex < 1 ? "" : (_detect_dept.SelectedItem as Label).Tag,
                               item_id,dept_type)).Tables[0];
            currenttable = table;
            list.Clear();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                SamplingInfo info = new SamplingInfo();
                //info.DeptId = table.Rows[i][0].ToString();
                info.DeptName = table.Rows[i][1].ToString();
                //info.ItemId = table.Rows[i][2].ToString();
                info.ItemName = table.Rows[i][3].ToString();
                info.SumActual = table.Rows[i][4].ToString();
                info.SamplingrateActual = table.Rows[i][5].ToString();
                info.SamplingratePercent = table.Rows[i][6].ToString();
                info.SumSamplingrateActual = table.Rows[i][7].ToString();
                info.SumPercent = table.Rows[i][8].ToString();
                list.Add(info);
            }

            //得到行和列标题 及数量            
            string[] DeptNames = list.Select(t => t.DeptName).Distinct().ToArray();
            string[] ItemNames = list.Select(t => t.ItemName).Distinct().ToArray();

            //创建DataTable
            DataTable tabledisplay = new DataTable();

            //表中第一行第一列交叉处一般显示为第1列标题
            tabledisplay.Columns.Add(new DataColumn("序号"));
            MyColumns.Add("序号", new MyColumn("序号", "序号") { BShow = true, Width = 5 });
            //switch (user_flag_tier)
            //{
            //    case "0": tabledisplay.Columns.Add(new DataColumn("省名称"));
            //        MyColumns.Add("省名称", new MyColumn("省名称", "省名称") { BShow = true, Width = 16 });
            //        break;
            //    case "1": tabledisplay.Columns.Add(new DataColumn("市(州)单位名称"));
            //        MyColumns.Add("市(州)单位名称", new MyColumn("市(州)单位名称", "市(州)单位名称") { BShow = true, Width = 16 });
            //        break;
            //    case "2": tabledisplay.Columns.Add(new DataColumn("区县名称"));
            //        MyColumns.Add("区县名称", new MyColumn("区县名称", "区县名称") { BShow = true, Width = 16 });
            //        break;
            //    case "3": tabledisplay.Columns.Add(new DataColumn("检测单位名称"));
            //        MyColumns.Add("检测单位名称", new MyColumn("检测单位名称", "检测单位名称") { BShow = true, Width = 16 });
            //        break;
            //    case "4": tabledisplay.Columns.Add(new DataColumn("检测单位名称"));
            //        MyColumns.Add("检测单位名称", new MyColumn("检测单位名称", "检测单位名称") { BShow = true, Width = 16 });
            //        break;
            //    default: break;
            //}
            tabledisplay.Columns.Add(new DataColumn("部门名称"));
            MyColumns.Add("部门名称", new MyColumn("部门名称", "部门名称") { BShow = true, Width = 16 });

            tabledisplay.Columns.Add(new DataColumn("批次总头数"));
            MyColumns.Add("批次总头数", new MyColumn("批次总头数", "批次总头数") { BShow = true, Width = 8 });

            //表中后面每列的标题其实是列分组的关键字
            for (int i = 0; i < ItemNames.Length; i++)
            {
                DataColumn column = new DataColumn(ItemNames[i]);
                tabledisplay.Columns.Add(column);
                MyColumns.Add(ItemNames[i].ToString(), new MyColumn(ItemNames[i].ToString(), ItemNames[i].ToString() + "检测量") { BShow = true, Width = 10 });
                tabledisplay.Columns.Add(new DataColumn("抽检率" + i));
                MyColumns.Add("抽检率" + i, new MyColumn("抽检率" + i, "抽检率") { BShow = true, Width = 10 });
            }

            //当选择了检测项目作为查询条件时，不显示任务完成总量和任务总完成率
            bool flag;
            if (item_id == "")
            {
                flag = true;
            }
            else
            {
                flag = false;
            }

            //表格后面为合计列
            tabledisplay.Columns.Add(new DataColumn("总抽检数"));
            MyColumns.Add("总抽检数", new MyColumn("总抽检数", "总抽检数") { BShow = flag, Width = 10 });
            tabledisplay.Columns.Add(new DataColumn("综合平均抽检率"));
            MyColumns.Add("综合平均抽检率", new MyColumn("综合平均抽检率", "综合平均抽检率") { BShow = flag, Width = 10 });

            //为表中各行生成数据
            for (int i = 0; i < DeptNames.Length; i++)
            {
                var row = tabledisplay.NewRow();
                //每行第0列为行分组关键字
                row[0] = i + 1;
                row[1] = DeptNames[i];
                string count = list.Where(t => t.DeptName == DeptNames[i]).Select(t => t.SumActual).FirstOrDefault();

                if (count == null || count == "")
                {
                    count = '0'.ToString();
                }
                row[2] = count;

                //每行的其余列为行列交叉对应的汇总数据
                for (int j = 0; j < ItemNames.Length; j++)
                {
                    string num = list.Where(t => t.DeptName == DeptNames[i] && t.ItemName == ItemNames[j]).Select(t => t.SamplingrateActual).FirstOrDefault();

                    if (num == null || num == "")
                    {
                        num = '0'.ToString();
                    }
                    row[ItemNames[j]] = num;

                    string percent = list.Where(t => t.DeptName == DeptNames[i] && t.ItemName == ItemNames[j]).Select(t => t.SamplingratePercent).FirstOrDefault();

                    if (percent == null || percent == "")
                    {
                        percent = '0'.ToString();
                    }
                    else
                    {
                        percent = percent + "%";
                    }
                    row[4 + 2 * j] = percent;
                }
                row[ItemNames.Length * 2 + 3] = list.Where(t => t.DeptName == DeptNames[i]).Select(t => t.SumSamplingrateActual).FirstOrDefault();

                string sumpercent = list.Where(t => t.DeptName == DeptNames[i]).Select(t => t.SumPercent).FirstOrDefault();

                if (sumpercent == null || sumpercent == "")
                {
                    sumpercent = '0'.ToString();
                }
                else
                {
                    sumpercent = sumpercent + "%";
                }
                row[ItemNames.Length * 2 + 4] = sumpercent;


                tabledisplay.Rows.Add(row);
            }

            _tableview.MyColumns = MyColumns;
            _tableview.BShowDetails = true;
            _tableview.Table = tabledisplay;
            //_sj.Visibility = Visibility.Visible;
            //_hj.Visibility = Visibility.Visible;
            //_title.Text = tabledisplay.Rows.Count.ToString();

            if (tabledisplay.Rows.Count == 0)
            {
                Toolkit.MessageBox.Show("没有查询到数据！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }

        void _tableview_DetailsRowEnvent(string id)
        {
            string dept_id;
            string flag_tier;

            DataRow[] rows = currenttable.Select("PART_NAME = '" + id + "'");
            dept_id = rows[0]["PART_ID"].ToString();
            flag_tier = rows[0]["flagtier"].ToString();

            if (flag_tier == "4")
            {
                
                switch (dept_type)
                {
                    case "0": grid_info.Children.Add(new UcSamplingReportDetailsSc(dbOperation, _year.Text, dept_id, item_id));
                        break;
                    case "1":
                    case "2":
                    case "3":
                    default: break;
                }
            }
            else
            {
                grid_info.Children.Add(new UcSamplingReportCountry(dbOperation, _year.Text, dept_id, item_id,dept_type));
            }
        }

        private void _export_Click(object sender, RoutedEventArgs e)
        {
            _tableview.ExportExcel();
        }

        public class SamplingInfo
        {
            //public string DeptId { get; set; }

            public string DeptName { get; set; }

            //public string ItemId { get; set; }

            public string ItemName { get; set; }

            public string SumActual { get; set; }

            public string SamplingrateActual { get; set; }

            public string SamplingratePercent { get; set; }

            public string SumSamplingrateActual { get; set; }

            public string SumPercent { get; set; }
        }
    }
}

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
using FoodSafetyMonitoring.Common;
using System.Data;
using FoodSafetyMonitoring.Manager.UserControls;
using Toolkit = Microsoft.Windows.Controls;
using System.IO;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class SysMonthReport : UserControl
    {
        private IDBOperation dbOperation;
        private string user_flag_tier;
        private string item_id;
        private string result_id;
        private string dept_type;
        private DataTable currenttable;
        private DataTable dt;
        private Dictionary<string, MyColumn> MyColumns = new Dictionary<string, MyColumn>();

        private List<DeptItemInfo> list = new List<DeptItemInfo>();

        private readonly List<string> year = new List<string>() { "2010",
            "2011", 
            "2012",
            "2013",
            "2014",
            "2015",
            "2016",
            "2017"};//初始化变量

        private readonly List<string> month = new List<string>() { "01",
            "02", 
            "03",
            "04",
            "05",
            "06",
            "07",
            "08",
            "09",
            "10",
            "11",
            "12"};//初始化变量

        public SysMonthReport(IDBOperation dbOperation, string depttype)
        {
            InitializeComponent();

            this.dbOperation = dbOperation;
            this.dept_type = depttype;
            user_flag_tier = (Application.Current.Resources["User"] as UserInfo).FlagTier;

            _year.ItemsSource = year;
            _year.SelectedIndex = 5;

            _month.ItemsSource = month;
            _month.SelectedIndex = 9;

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
            ComboboxTool.InitComboboxSource(_detect_item, "SELECT ItemID,ItemNAME FROM t_det_item WHERE OPENFLAG = '1' order by orderId", "cxtj");
            //检测结果
            ComboboxTool.InitComboboxSource(_detect_result, "SELECT resultId,resultName FROM t_det_result where openFlag='1'  ORDER BY id", "cxtj");

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
            result_id = _detect_result.SelectedIndex < 1 ? "" : (_detect_result.SelectedItem as Label).Tag.ToString();

            grid_info.Children.Clear();
            grid_info.Children.Add(_tableview);
            MyColumns.Clear();

            string date;

            //判断查询条件：年月
            date = _year.Text + "-" + _month.Text;

            DataTable table = dbOperation.GetDbHelper().GetDataSet(string.Format("call p_report_month('{0}','{1}','{2}','{3}','{4}','{5}')",
                              (Application.Current.Resources["User"] as UserInfo).ID, date,
                               _detect_dept.SelectedIndex < 1 ? "" : (_detect_dept.SelectedItem as Label).Tag,
                               item_id,result_id, dept_type)).Tables[0];

            currenttable = table;
            list.Clear();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DeptItemInfo info = new DeptItemInfo();
                //info.DeptId = table.Rows[i][0].ToString();
                info.DeptName = table.Rows[i][1].ToString();
                //info.ItemId = table.Rows[i][2].ToString();
                info.ItemName = table.Rows[i][3].ToString();
                info.Count = table.Rows[i][4].ToString();
                info.Sum = table.Rows[i][5].ToString();
                info.Yin = table.Rows[i][6].ToString();
                info.Yang = table.Rows[i][7].ToString();
                info.Yisi = table.Rows[i][8].ToString();
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

            //表中后面每列的标题其实是列分组的关键字
            for (int i = 0; i < ItemNames.Length; i++)
            {
                DataColumn column = new DataColumn(ItemNames[i]);
                tabledisplay.Columns.Add(column);
                MyColumns.Add(ItemNames[i].ToString(), new MyColumn(ItemNames[i].ToString(), ItemNames[i].ToString()) { BShow = true, Width = 10 });
            }
            //表格后面为合计列
            tabledisplay.Columns.Add(new DataColumn("合计"));
            MyColumns.Add("合计", new MyColumn("合计", "合计") { BShow = true, Width = 10 });

            switch (result_id)
            {
                case "": tabledisplay.Columns.Add(new DataColumn("阴性样本"));
                    tabledisplay.Columns.Add(new DataColumn("疑似阳性样本"));
                    tabledisplay.Columns.Add(new DataColumn("阳性样本"));
                    MyColumns.Add("阴性样本", new MyColumn("阴性样本", "阴性样本") { BShow = true, Width = 10 });
                    MyColumns.Add("疑似阳性样本", new MyColumn("疑似阳性样本", "疑似阳性样本") { BShow = true, Width = 10 });
                    MyColumns.Add("阳性样本", new MyColumn("阳性样本", "阳性样本") { BShow = true, Width = 10 });
                    break;
                case "1": tabledisplay.Columns.Add(new DataColumn("阴性样本"));
                    MyColumns.Add("阴性样本", new MyColumn("阴性样本", "阴性样本") { BShow = true, Width = 10 });
                    break;
                case "0": tabledisplay.Columns.Add(new DataColumn("疑似阳性样本"));
                    MyColumns.Add("疑似阳性样本", new MyColumn("疑似阳性样本", "疑似阳性样本") { BShow = true, Width = 10 });
                    break;
                case "2": tabledisplay.Columns.Add(new DataColumn("阳性样本"));
                    MyColumns.Add("阳性样本", new MyColumn("阳性样本", "阳性样本") { BShow = true, Width = 10 });
                    break;
                default: break;
            }


            //为表中各行生成数据
            for (int i = 0; i < DeptNames.Length; i++)
            {
                var row = tabledisplay.NewRow();
                //每行第0列为行分组关键字
                row[0] = i + 1;
                row[1] = DeptNames[i];
                //每行的其余列为行列交叉对应的汇总数据
                for (int j = 0; j < ItemNames.Length; j++)
                {
                    string num = list.Where(t => t.DeptName == DeptNames[i] && t.ItemName == ItemNames[j]).Select(t => t.Count).FirstOrDefault();
                   
                    if (num == null || num == "")
                    {
                        num = '0'.ToString();
                    }
                    row[ItemNames[j]] = num;
                }
                row[ItemNames.Length + 2] = list.Where(t => t.DeptName == DeptNames[i]).Select(t => t.Sum).FirstOrDefault();

                switch (result_id)
                {
                    case "": row[ItemNames.Length + 3] = list.Where(t => t.DeptName == DeptNames[i]).Select(t => t.Yin).FirstOrDefault();
                        row[ItemNames.Length + 4] = list.Where(t => t.DeptName == DeptNames[i]).Select(t => t.Yisi).FirstOrDefault();
                        row[ItemNames.Length + 5] = list.Where(t => t.DeptName == DeptNames[i]).Select(t => t.Yang).FirstOrDefault();
                        break;
                    case "1": row[ItemNames.Length + 3] = list.Where(t => t.DeptName == DeptNames[i]).Select(t => t.Yin).FirstOrDefault();
                        break;
                    case "0": row[ItemNames.Length + 3] = list.Where(t => t.DeptName == DeptNames[i]).Select(t => t.Yisi).FirstOrDefault();
                        break;
                    case "2": row[ItemNames.Length + 3] = list.Where(t => t.DeptName == DeptNames[i]).Select(t => t.Yang).FirstOrDefault();
                        break;
                    default: break;
                }
                
                tabledisplay.Rows.Add(row);
            }

            //计算报表总条数
            int row_count = 0;

            if (table.Rows.Count != 0)
            {
                //表格最后添加合计行
                tabledisplay.Rows.Add(tabledisplay.NewRow()[1] = "合计");
                for (int j = 2; j < tabledisplay.Columns.Count; j++)
                {
                    int sum = 0;
                    for (int i = 0; i < tabledisplay.Rows.Count - 1; i++)
                    {
                        sum += Convert.ToInt32(tabledisplay.Rows[i][j].ToString());
                    }
                    //sum_column += sum;
                    tabledisplay.Rows[tabledisplay.Rows.Count - 1][j] = sum;
                }

                row_count = tabledisplay.Rows.Count - 1;
            }
            else
            {
                row_count = 0;
            }

            dt = tabledisplay;

            //表格的标题
            _tableview.MyColumns = MyColumns;
            _tableview.BShowDetails = true;
            _tableview.Table = tabledisplay;

            if (row_count == 0)
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
                    case "0": grid_info.Children.Add(new UcMonthReportDetailsSc(dbOperation, _year.Text + "-" + _month.Text, dept_id, item_id, result_id));
                        break;
                    case "1":
                    case "2":
                    case "3":
                    default: break;
                }
            }
            else
            {
                grid_info.Children.Add(new UcMonthReportCountry(dbOperation, _year.Text + "-" + _month.Text, dept_id, item_id, result_id,dept_type));
            }
        }

        private void _export_Click(object sender, RoutedEventArgs e)
        {
            //_tableview.ExportExcel();

            string dept_id;
            string item_id;
            string result_id;

            if ((dt == null) || (dt.Rows.Count == 0))
            {
                Toolkit.MessageBox.Show("当前可导出数据为零！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
            sfd.Filter = "导出文件 (*.csv)|*.csv";
            sfd.FilterIndex = 0;
            sfd.RestoreDirectory = true;
            sfd.Title = "导出文件保存路径";
            sfd.ShowDialog();
            string strFilePath = sfd.FileName;
            if (strFilePath != "")
            {
                if (File.Exists(strFilePath))
                {
                    File.Delete(strFilePath);
                }
                StreamWriter sw = new StreamWriter(new FileStream(strFilePath, FileMode.CreateNew), Encoding.Default);
                string tableHeader = " ";
                foreach (DataColumn c in dt.Columns)
                {
                    GridViewColumn gvc = new GridViewColumn();
                    tableHeader += c.ColumnName + ",";
                }
                sw.WriteLine(string.Format("{0}年{1}月  检测数据月报表（单位：份次）", _year.Text, _month.Text));
                sw.WriteLine(tableHeader);


                for (int j = 0; j < dt.Rows.Count - 1; j++)
                {
                    DataRow row = dt.Rows[j];
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sb.Append(row[i]);
                        sb.Append(",");
                    }
                    sw.WriteLine(sb);

                    DataRow[] rows = currenttable.Select("PART_NAME = '" + row[1].ToString() + "'");
                    dept_id = rows[0]["PART_ID"].ToString();
                    item_id = _detect_item.SelectedIndex < 1 ? "" : (_detect_dept.SelectedItem as Label).Tag.ToString();
                    result_id = _detect_result.SelectedIndex < 1 ? "" : (_detect_result.SelectedItem as Label).Tag.ToString();

                    DataTable table = dbOperation.GetDbHelper().GetDataSet(string.Format("call  p_report_month_details_nopages('{0}','{1}','{2}','{3}','{4}')",
                                _year.Text + "-" + _month.Text, dept_id, item_id, result_id,dept_type)).Tables[0];

                    string header = "检测单编号,信息来源,检测时间,检测单位,检测项目,检测对象,检测方法,检测值,检测结果,检测师,来源产地,被检单位";
                    sw.WriteLine(header);

                    for (int m = 0; m < table.Rows.Count; m++)
                    {
                        DataRow row_new = table.Rows[m];
                        StringBuilder sb_new = new StringBuilder();
                        for (int n = 0; n < table.Columns.Count; n++)
                        {
                            sb_new.Append(row_new[n]);
                            sb_new.Append(",");
                        }
                        sw.WriteLine(sb_new);
                    }
                }

                DataRow row_bottom = dt.Rows[dt.Rows.Count - 1];
                StringBuilder sb_bottom = new StringBuilder();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    sb_bottom.Append(row_bottom[i]);
                    sb_bottom.Append(",");
                }
                sw.WriteLine(sb_bottom);

                sw.Close();
                Toolkit.MessageBox.Show("导出文件成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
        [Serializable]
        public class DeptItemInfo
        {
            //public string DeptId { get; set; }

            public string DeptName { get; set; }

            //public string ItemId { get; set; }

            public string ItemName { get; set; }

            public string Count { get; set; }

            public string Sum { get; set; }

            public string Yin { get; set; }

            public string Yang { get; set; }

            public string Yisi { get; set; }
        }
    }
}

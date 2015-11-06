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
using System.Data.Odbc;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// UcSetSamplingRate.xaml 的交互逻辑
    /// </summary>
    public partial class UcSetTask : System.Windows.Controls.UserControl
    {
        private IDBOperation dbOperation;
        private System.Data.DataTable currenttable;
        private System.Data.DataTable exporttable;
        private string user_flag_tier;
        private string deptid;
        private string dept_type;
        private List<DeptItem> list = new List<DeptItem>();
        Importing_window load ;

        public UcSetTask(IDBOperation dbOperation, string depttype)
        {
            InitializeComponent();
            this.dbOperation = dbOperation;
            this.dept_type = depttype;
            user_flag_tier = (System.Windows.Application.Current.Resources["User"] as UserInfo).FlagTier;
            deptid = (System.Windows.Application.Current.Resources["User"] as UserInfo).DepartmentID;

            _tableview.ModifyRowEnvent += new UcTableOperableView_NoPages.ModifyRowEventHandler(_tableview_ModifyRowEnvent);
            Load_table();
        }

        public void Load_table()
        {
            Dictionary<string, MyColumn> MyColumns = new Dictionary<string, MyColumn>();

            System.Data.DataTable table = dbOperation.GetDbHelper().GetDataSet(string.Format("call p_task_details('{0}','{1}')",
                              (System.Windows.Application.Current.Resources["User"] as UserInfo).ID, dept_type)).Tables[0];

            currenttable = table;

            list.Clear();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                DeptItem info = new DeptItem();
                info.DeptId = table.Rows[i][0].ToString();
                info.DeptName = table.Rows[i][1].ToString();
                info.ItemId = table.Rows[i][2].ToString();
                info.ItemName = table.Rows[i][3].ToString();
                info.Task = table.Rows[i][4].ToString();
                list.Add(info);
            }

            //得到行和列标题 及数量            
            string[] DeptNames = list.Select(t => t.DeptName).Distinct().ToArray();
            string[] ItemNames = list.Select(t => t.ItemName).Distinct().ToArray();

            //创建DataTable
            System.Data.DataTable tabledisplay = new System.Data.DataTable();

            //表中第一行第一列交叉处一般显示为第1列标题
            //tabledisplay.Columns.Add(new DataColumn("序号"));
            //MyColumns.Add("序号", new MyColumn("序号", "序号") { BShow = true, Width = 10 });
            //switch (user_flag_tier)
            //{
            //    case "0": tabledisplay.Columns.Add(new DataColumn("省名称"));
            //        MyColumns.Add("省名称", new MyColumn("省名称", "省名称") { BShow = true, Width = 20 });
            //        break;
            //    case "1": tabledisplay.Columns.Add(new DataColumn("市(州)单位名称"));
            //        MyColumns.Add("市(州)单位名称", new MyColumn("市(州)单位名称", "市(州)单位名称") { BShow = true, Width = 20 });
            //        break;
            //    case "2": tabledisplay.Columns.Add(new DataColumn("区县名称"));
            //        MyColumns.Add("区县名称", new MyColumn("区县名称", "区县名称") { BShow = true, Width = 20 });
            //        break;
            //    case "3": tabledisplay.Columns.Add(new DataColumn("检测单位名称"));
            //        MyColumns.Add("检测单位名称", new MyColumn("检测单位名称", "检测单位名称") { BShow = true, Width = 20 });
            //        break;
            //    case "4": tabledisplay.Columns.Add(new DataColumn("检测单位名称"));
            //        MyColumns.Add("检测单位名称", new MyColumn("检测单位名称", "检测单位名称") { BShow = true, Width = 20 });
            //        break;
            //    default: break;
            //}
            tabledisplay.Columns.Add(new DataColumn("部门名称"));
            MyColumns.Add("部门名称", new MyColumn("部门名称", "部门名称") { BShow = true, Width = 20 });

            //表中后面每列的标题其实是列分组的关键字
            for (int i = 0; i < ItemNames.Length; i++)
            {
                DataColumn column = new DataColumn(ItemNames[i]);
                tabledisplay.Columns.Add(column);
                MyColumns.Add(ItemNames[i], new MyColumn(ItemNames[i], ItemNames[i]) { BShow = true, Width = 15 });
            }

            //为表中各行生成数据
            for (int i = 0; i < DeptNames.Length; i++)
            {
                var row = tabledisplay.NewRow();
                //每行第0列为行分组关键字
                //row[0] = i + 1;
                row[0] = DeptNames[i];
                //每行的其余列为行列交叉对应的汇总数据
                for (int j = 0; j < ItemNames.Length; j++)
                {
                    string num = list.Where(t => t.DeptName == DeptNames[i] && t.ItemName == ItemNames[j]).Select(t => t.Task).FirstOrDefault();
                    if (num == null || num == "")
                    {
                        num = '0'.ToString();
                    }
                    row[ItemNames[j]] = num;
                }

                tabledisplay.Rows.Add(row);
            }

            if (table.Rows.Count != 0)
            {
                //表格最后添加合计行
                tabledisplay.Rows.Add(tabledisplay.NewRow()[1] = "合计");
                for (int j = 1; j < tabledisplay.Columns.Count; j++)
                {
                    int sum = 0;
                    for (int i = 0; i < tabledisplay.Rows.Count - 1; i++)
                    {
                        sum += Convert.ToInt32(tabledisplay.Rows[i][j].ToString());
                    }
                    //sum_column += sum;
                    tabledisplay.Rows[tabledisplay.Rows.Count - 1][j] = sum;
                }

                System.Data.DataTable tasktable = dbOperation.GetDbHelper().GetDataSet("select t_det_item.ItemNAME,task " +
                                      "from t_task_assign_new left JOIN t_det_item ON t_task_assign_new.iid = t_det_item.ItemID " +
                                      "where t_task_assign_new.did = " + deptid).Tables[0];

                List<ItemTask> listtask = new List<ItemTask>();
                listtask.Clear();
                for (int i = 0; i < tasktable.Rows.Count; i++)
                {
                    ItemTask task = new ItemTask();
                    task.ItemName = tasktable.Rows[i][0].ToString();
                    task.Task = tasktable.Rows[i][1].ToString();
                    listtask.Add(task);
                }

                if (user_flag_tier != "1")
                {
                    //表格最后添加上级分配任务量
                    tabledisplay.Rows.Add(tabledisplay.NewRow()[1] = "上级下达任务量");
                    for (int j = 1; j < tabledisplay.Columns.Count; j++)
                    {
                        string task = listtask.Where(s => s.ItemName == tabledisplay.Columns[j].ColumnName.ToString()).Select(s => s.Task).FirstOrDefault();
                        if (task == null || task == "")
                        {
                            task = '0'.ToString();
                        }

                        tabledisplay.Rows[tabledisplay.Rows.Count - 1][j] = task;
                    }

                    //表格最后添加未分配量
                    tabledisplay.Rows.Add(tabledisplay.NewRow()[1] = "未分配量");

                    for (int j = 1; j < tabledisplay.Columns.Count; j++)
                    {
                        int rwl = Convert.ToInt32(tabledisplay.Rows[tabledisplay.Rows.Count - 2][j].ToString());
                        int yfp = Convert.ToInt32(tabledisplay.Rows[tabledisplay.Rows.Count - 3][j].ToString());
                        int wfp = rwl - yfp;
                        tabledisplay.Rows[tabledisplay.Rows.Count - 1][j] = wfp;
                    }
                }
            }

            exporttable = tabledisplay;
            _tableview.BShowModify = true;
            _tableview.MyColumns = MyColumns;
            _tableview.Table = tabledisplay;
        }

        void _tableview_ModifyRowEnvent(string id)
        {
            string dept_id;

            DataRow[] rows = currenttable.Select("PART_NAME = '" + id + "'");
            dept_id = rows[0]["PART_ID"].ToString();

            SetTask sam = new SetTask(dbOperation, dept_id, id, this);
            sam.ShowDialog();
        }

        public class DeptItem
        {
            public string DeptId { get; set; }

            public string DeptName { get; set; }

            public string ItemId { get; set; }

            public string ItemName { get; set; }

            public string Task { get; set; }
        }
        public class ItemTask
        {
            public string ItemName { get; set; }

            public string Task { get; set; }
        }

        private void BtnImport_Click(object sender, RoutedEventArgs e)
        {
           System.Data.DataTable importdt = new System.Data.DataTable();
           importdt = GetDataFromExcelByCom();
           if(importdt != null)
           {
               if (importdt.Rows.Count != 0)
               {
                   string str;

                   for (int i = 0; i < importdt.Rows.Count; i++)
                   {
                       str = "";
                       string dept_id = list.Where(t => t.DeptName == importdt.Rows[i][0].ToString()).Select(t => t.DeptId).FirstOrDefault();

                       if(dept_id == null || dept_id == "")
                       {
                           load.Close();
                           Toolkit.MessageBox.Show(importdt.Rows[i][0].ToString() + "不是正确的下级部门，请确认后重新导入！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                           return;
                       }

                       for (int j = 1; j < importdt.Columns.Count; j++)
                       {
                           string item_id = list.Where(t => t.ItemName == importdt.Columns[j].ColumnName).Select(t => t.ItemId).FirstOrDefault();
                           if (item_id == null || item_id == "")
                           {
                               load.Close();
                               Toolkit.MessageBox.Show(importdt.Columns[j].ColumnName + "不是正确的检测项目，请确认后重新导入！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                               return;
                           }
                           string num = importdt.Rows[i][j].ToString();
                           if (num == null || num == "")
                           {
                               num = '0'.ToString();
                           }
                           str = str + item_id + "," + num + ",";

                           if (j != importdt.Columns.Count - 1)
                           {
                               str = str + "#";
                           }
                       }

                       try
                       {
                           int result = dbOperation.GetDbHelper().ExecuteSql(string.Format("call p_set_task ('{0}','{1}')",
                                                               dept_id, str));

                           if (result == 1)
                           {

                           }
                           else
                           {
                               load.Close();
                               Toolkit.MessageBox.Show("任务量设置失败！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                               return;
                           }
                       }
                       catch (Exception ex)
                       {
                           load.Close();
                           Toolkit.MessageBox.Show("任务量设置失败2！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                           return;
                       }
                   }

                   load.Close();
                   Toolkit.MessageBox.Show("任务导入设置成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                   Load_table();
                   return;
               }
               else
               {
                   load.Close();
                   Toolkit.MessageBox.Show("导入excel内容为空，请确认！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                   return;
               }
           }
        }

        //读取Excel中的内容
        System.Data.DataTable GetDataFromExcelByCom(bool hasTitle = true)
        {
            //打开对话框
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "Excel(*.xls)|*.xls|Excel(*.xlsx)|*.xlsx";
            openFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFile.Multiselect = false;
            if (openFile.ShowDialog() == DialogResult.Cancel)
            {
                return null;
            }
            var excelFilePath = openFile.FileName;

            try
            {
                load = new Importing_window();
                load.Show();
                //this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();
                Sheets sheets;
                object oMissiong = System.Reflection.Missing.Value;
                Workbook workbook = null;//创建工作簿
                System.Data.DataTable dt = new System.Data.DataTable();

                try
                {
                    if (app == null)
                    {
                        return null;
                    }
                    workbook = app.Workbooks.Open(excelFilePath, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong,
                        oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong, oMissiong);

                    sheets = workbook.Worksheets;

                    //将数据读入到DataTable中
                    Worksheet worksheet = (Worksheet)sheets.get_Item(1);//读取第一张表  
                    if (worksheet == null)
                    {
                        return null;
                    }

                    int iRowCount = worksheet.UsedRange.Rows.Count;
                    int iColCount = worksheet.UsedRange.Columns.Count;
                    //生成列头
                    for (int i = 0; i < iColCount; i++)
                    {
                        var name = "column" + i;
                        if (hasTitle)
                        {
                            var txt = ((Range)worksheet.Cells[1, i + 1]).Text.ToString();
                            if (!string.IsNullOrEmpty(txt))
                            {
                                name = txt;
                            }
                        }
                        while (dt.Columns.Contains(name))
                        {
                            name = name + "_1";//重复行名称会报错。
                        }
                        dt.Columns.Add(new DataColumn(name, typeof(string)));
                    }
                    //生成行数据
                    Range range;
                    int rowIdx = hasTitle ? 2 : 1;
                    for (int iRow = rowIdx; iRow <= iRowCount; iRow++)
                    {
                        DataRow dr = dt.NewRow();
                        for (int iCol = 1; iCol <= iColCount; iCol++)
                        {
                            range = (Range)worksheet.Cells[iRow, iCol];
                            dr[iCol - 1] = (range.Value2 == null) ? "" : range.Text.ToString();
                        }
                        dt.Rows.Add(dr);
                    }
                    return dt;
                }
                catch { return null; }
                finally
                {
                    workbook.Close(false, oMissiong, oMissiong);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                    workbook = null;
                    app.Workbooks.Close();
                    app.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(app);
                    app = null;
                }

            }
            catch
            {
                Toolkit.MessageBox.Show("无法导入，可能您的机子Office版本有问题！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return null;
            }
        }
       

        private void BtnExport_Click(object sender, RoutedEventArgs e)
        {
            //打开对话框
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.Filter = "Excel(*.xls)|*.xls|Excel(*.xlsx)|*.xlsx";
            saveFile.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            if (saveFile.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            var excelFilePath = saveFile.FileName;
            if (excelFilePath != "")
            {
                if (System.IO.File.Exists(excelFilePath))
                {
                    try
                    {
                        System.IO.File.Delete(excelFilePath);
                    }
                    catch (Exception ex)
                    {
                        Toolkit.MessageBox.Show("导出文件时出错,文件可能正被打开！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    
                }

                try
                {
                     //创建Excel  
                     Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();

                     if (excelApp == null)
                     {
                         Toolkit.MessageBox.Show("无法创建Excel对象，可能您的机子未安装Excel程序！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                         return;
                     }
                     Workbook excelWB = excelApp.Workbooks.Add(System.Type.Missing);    //创建工作簿（WorkBook：即Excel文件主体本身）  
                     Worksheet excelWS = (Worksheet)excelWB.Worksheets[1];   //创建工作表（即Excel里的子表sheet） 1表示在子表sheet1里进行数据导出 
                     excelWS.Name = "任务量";

                     //excelWS.Cells.NumberFormat = "@";     //  如果数据中存在数字类型 可以让它变文本格式显示  
                     //导出列名
                     for (int j = 0; j < exporttable.Columns.Count; j++)
                     {
                         excelWS.Cells[1, j + 1] = exporttable.Columns[j].ColumnName.ToString();
                     }


                     //将数据导入到工作表的单元格  
                     for (int i = 0; i < exporttable.Rows.Count; i++)
                     {
                         for (int j = 0; j < exporttable.Columns.Count; j++)
                         {
                             excelWS.Cells[i + 2, j + 1] = exporttable.Rows[i][j].ToString();
                         }
                     }

                     Range range = null;
                     range = (Range)excelWS.get_Range("A1", "D1"); //获取Excel多个单元格区域
                     range.ColumnWidth = 20; //设置单元格的宽度  

                     excelWB.SaveAs(excelFilePath);  //将其进行保存到指定的路径  
                     excelWB.Close();
                     excelApp.Quit();
                     KillAllExcel(excelApp); //释放可能还没释放的进程  
                     Toolkit.MessageBox.Show("文件导出成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                    Toolkit.MessageBox.Show("无法创建Excel对象，可能您的机子Office版本有问题！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
              
            }
        }

        public bool KillAllExcel(Microsoft.Office.Interop.Excel.Application excelApp)
        {
            try
            {
                if (excelApp != null)
                {
                    excelApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                    //释放COM组件，其实就是将其引用计数减1     
                    //System.Diagnostics.Process theProc;     
                    foreach (System.Diagnostics.Process theProc in System.Diagnostics.Process.GetProcessesByName("EXCEL"))
                    {
                        //先关闭图形窗口。如果关闭失败.有的时候在状态里看不到图形窗口的excel了，     
                        //但是在进程里仍然有EXCEL.EXE的进程存在，那么就需要释放它     
                        if (theProc.CloseMainWindow() == false)
                        {
                            theProc.Kill();
                        }
                    }
                    excelApp = null;
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }  
    }
}

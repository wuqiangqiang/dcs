﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
using Toolkit = Microsoft.Windows.Controls;

namespace FoodSafetyMonitoring.Manager.UserControls
{
    /// <summary>
    /// UcTableView_NoTitle.xaml 的交互逻辑
    /// </summary>
    public partial class UcTableView_NoTitle : UserControl
    {
        public delegate void DetailsRowEventHandler(string id);
        public event DetailsRowEventHandler DetailsRowEnvent;
        public delegate void ModifyRowEventHandler(string id);
        public event ModifyRowEventHandler ModifyRowEnvent;

        public UcTableView_NoTitle()
        {
            InitializeComponent();
            this.SizeChanged += new SizeChangedEventHandler(UcTableView_SizeChanged);
        }

        void UcTableView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (dt == null)
            {
                return;
            }
            _gridview.Columns.Clear();
            foreach (DataColumn c in dt.Columns)
            {
                GridViewColumn gvc = new GridViewColumn();
                gvc.Header = c.ColumnName;
                if (BShowDetails || BShowModify)
                {
                    gvc.Width = (_listview.ActualWidth - 65) / dt.Columns.Count;
                }
                else
                {
                    gvc.Width = (_listview.ActualWidth - 25) / dt.Columns.Count;
                }
                gvc.SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Center);
                //gvc.DisplayMemberBinding = (new Binding(c.ColumnName));
                FrameworkElementFactory text = new FrameworkElementFactory(typeof(TextBlock));
                text.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center);
                text.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Center);
                text.SetBinding(TextBlock.TextProperty, new Binding(c.ColumnName));
                DataTemplate dataTemplate = new DataTemplate() { VisualTree = text };
                gvc.CellTemplate = dataTemplate;
                _gridview.Columns.Add(gvc);
            }
            if (BShowDetails)
            {
                GridViewColumn gvc_details = new GridViewColumn();
                gvc_details.Header = "详情";
                FrameworkElementFactory button_details = new FrameworkElementFactory(typeof(Button));
                button_details.SetResourceReference(Button.HorizontalContentAlignmentProperty, HorizontalAlignment.Center);
                button_details.SetValue(Button.WidthProperty, 40.0);
                button_details.AddHandler(Button.ClickEvent, new RoutedEventHandler(details_Click));
                button_details.SetBinding(Button.TagProperty, new Binding(dt.Columns[1].ColumnName));
                button_details.SetValue(Button.ContentProperty, ">>");
                button_details.SetValue(Button.ForegroundProperty, Brushes.White);
                button_details.SetValue(Button.FontSizeProperty, 14.0);
                button_details.SetBinding(Button.VisibilityProperty, new Binding(dt.Columns[0].ColumnName) { Converter = new VisibleBtnConverter() });
                DataTemplate dataTemplate_details = new DataTemplate() { VisualTree = button_details };
                gvc_details.CellTemplate = dataTemplate_details;
                _gridview.Columns.Add(gvc_details);
            }
            if (BShowModify)
            {
                GridViewColumn gvc_modify = new GridViewColumn();
                gvc_modify.Header = "设置";
                FrameworkElementFactory button_modify = new FrameworkElementFactory(typeof(Button));
                button_modify.SetResourceReference(Button.HorizontalContentAlignmentProperty, HorizontalAlignment.Center);
                button_modify.SetValue(Button.WidthProperty, 20.0);
                button_modify.AddHandler(Button.ClickEvent, new RoutedEventHandler(modify_Click));
                button_modify.SetBinding(Button.TagProperty, new Binding(dt.Columns[1].ColumnName));
                button_modify.SetResourceReference(Button.StyleProperty, "ListModifyImageButtonTemplate");
                DataTemplate dataTemplate_modify = new DataTemplate() { VisualTree = button_modify };
                gvc_modify.CellTemplate = dataTemplate_modify;
                _gridview.Columns.Add(gvc_modify);
            }
            _listview.ItemsSource = null;
            _listview.ItemsSource = dt.DefaultView;
        }

        private DataTable dt;
        private string title;
        private List<int> columnNumbers;
        //private List<int> sumColumns = new List<int>();

        public void SetDataTable(DataTable _dt,string title, List<int> columnNumbers)
        {
            this.dt = _dt.Copy();
            this.title = title;
            this.columnNumbers = columnNumbers;

            _gridview.Columns.Clear();
            foreach (DataColumn c in dt.Columns)
            {
                GridViewColumn gvc = new GridViewColumn();
                gvc.Header = c.ColumnName;
                if (BShowDetails || BShowModify)
                {
                    gvc.Width = (_listview.ActualWidth - 65) / dt.Columns.Count;
                }
                else
                {
                    gvc.Width = (_listview.ActualWidth - 25) / dt.Columns.Count;
                }
                //gvc.DisplayMemberBinding = (new Binding(c.ColumnName));
                FrameworkElementFactory text = new FrameworkElementFactory(typeof(TextBlock));
                text.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center);
                text.SetValue(TextBlock.TextAlignmentProperty, TextAlignment.Center);
                text.SetBinding(TextBlock.TextProperty, new Binding(c.ColumnName));
                DataTemplate dataTemplate = new DataTemplate() { VisualTree = text };
                gvc.CellTemplate = dataTemplate;
                gvc.SetValue(HorizontalAlignmentProperty, HorizontalAlignment.Center);
                _gridview.Columns.Add(gvc);
            }
            if (BShowDetails)
            {
                GridViewColumn gvc_details = new GridViewColumn();
                gvc_details.Header = "详情";
                FrameworkElementFactory button_details = new FrameworkElementFactory(typeof(Button));
                button_details.SetResourceReference(Button.HorizontalContentAlignmentProperty, HorizontalAlignment.Center);
                button_details.SetValue(Button.WidthProperty, 40.0);
                button_details.AddHandler(Button.ClickEvent, new RoutedEventHandler(details_Click));
                button_details.SetBinding(Button.TagProperty, new Binding(dt.Columns[1].ColumnName));
                button_details.SetValue(Button.ContentProperty, ">>");
                button_details.SetValue(Button.ForegroundProperty, Brushes.White);
                button_details.SetValue(Button.FontSizeProperty, 14.0);
                button_details.SetBinding(Button.VisibilityProperty, new Binding(dt.Columns[0].ColumnName) { Converter = new VisibleBtnConverter() });
                DataTemplate dataTemplate_details = new DataTemplate() { VisualTree = button_details };
                gvc_details.CellTemplate = dataTemplate_details;
                _gridview.Columns.Add(gvc_details);
            }
            if (BShowModify)
            {
                GridViewColumn gvc_modify = new GridViewColumn();
                gvc_modify.Header = "修改";
                FrameworkElementFactory button_modify = new FrameworkElementFactory(typeof(Button));
                button_modify.SetResourceReference(Button.HorizontalContentAlignmentProperty, HorizontalAlignment.Center);
                button_modify.SetValue(Button.WidthProperty, 20.0);
                button_modify.AddHandler(Button.ClickEvent, new RoutedEventHandler(modify_Click));
                button_modify.SetBinding(Button.TagProperty, new Binding(dt.Columns[1].ColumnName));
                button_modify.SetResourceReference(Button.StyleProperty, "ListModifyImageButtonTemplate");
                DataTemplate dataTemplate_modify = new DataTemplate() { VisualTree = button_modify };
                gvc_modify.CellTemplate = dataTemplate_modify;
                _gridview.Columns.Add(gvc_modify);
            }
            _listview.ItemsSource = dt.DefaultView;
        }

        public bool BShowDetails
        {
            get;
            set;
        }
        public bool BShowModify
        {
            get;
            set;
        }

        private void details_Click(object sender, RoutedEventArgs e)
        {
            if (DetailsRowEnvent != null)
            {
                DetailsRowEnvent((sender as Button).Tag.ToString());
            }
        }

        private void modify_Click(object sender, RoutedEventArgs e)
        {
            if (ModifyRowEnvent != null)
            {
                ModifyRowEnvent((sender as Button).Tag.ToString());
            }
        }

        public void ExportExcel()
        {
            if ((dt == null) || (dt.Rows.Count == 0))
            {
                Toolkit.MessageBox.Show("当前可导出数据为零！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            System.Windows.Forms.SaveFileDialog sfd = new System.Windows.Forms.SaveFileDialog();
            sfd.Filter = "导出文件 (*.csv)|*.csv";
            //sfd.Filter = "Excel(*.xlsx)|*.xlsx|Excel(*.xls)|*.xls";
            sfd.FilterIndex = 0;
            sfd.RestoreDirectory = true;
            sfd.Title = "导出文件保存路径";
            sfd.ShowDialog();
            string strFilePath = sfd.FileName;
            if (strFilePath != "")
            {
                if (File.Exists(strFilePath))
                {
                    try
                    {
                        File.Delete(strFilePath);
                    }
                    catch (Exception ex)
                    {
                        Toolkit.MessageBox.Show("导出文件时出错,文件可能正被打开！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }
                StreamWriter sw = new StreamWriter(new FileStream(strFilePath, FileMode.CreateNew), Encoding.Default);
                string tableHeader = " ";
                foreach (DataColumn c in dt.Columns)
                {
                    GridViewColumn gvc = new GridViewColumn();
                    tableHeader += c.ColumnName + ",";
                }
                sw.WriteLine(title);
                sw.WriteLine(tableHeader);

                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    DataRow row = dt.Rows[j];
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        sb.Append(row[i]);
                        sb.Append(",");
                    }
                    sw.WriteLine(sb);
                }

                //StringBuilder sum_sb = new StringBuilder();
                //for (int i = 0; i < dt.Columns.Count; i++)
                //{
                //    if (i == 0)
                //    {
                //        sum_sb.Append("共计");
                //    }
                //    else if (columnNumbers.Contains(i))
                //    {
                //        sum_sb.Append(sumColumns[columnNumbers.IndexOf(i)]);
                //    }
                //    sum_sb.Append(",");
                //}
                //sw.WriteLine(sum_sb);

                sw.Close();
                //MessageBox.Show("导出文件成功！", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                Toolkit.MessageBox.Show("导出文件成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


    }
}

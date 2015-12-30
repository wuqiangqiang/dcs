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
using System.Windows.Shapes;
using FoodSafetyMonitoring.dao;
using System.Data;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// detectdetailsCy.xaml 的交互逻辑
    /// </summary>
    public partial class detectdetailsCy : Window
    {
        private IDBOperation dbOperation;
        public detectdetailsCy(IDBOperation dbOperation, int id)
        {
            InitializeComponent();
            this.dbOperation = dbOperation;

            DataTable table = dbOperation.GetDbHelper().GetDataSet(string.Format("call p_detect_details_cy('{0}')", id)).Tables[0];

            //给画面上的控件赋值
            _orderid.Text = table.Rows[0][15].ToString();
            _areaName.Text = table.Rows[0][8].ToString();
            _companyName.Text = table.Rows[0][9].ToString();
            _itemName.Text = table.Rows[0][3].ToString();
            _objectName.Text = table.Rows[0][4].ToString();
            _reangetName.Text = table.Rows[0][5].ToString();
            _resultName.Text = table.Rows[0][6].ToString();
            _deptName.Text = table.Rows[0][2].ToString();
            _detectDate.Text = table.Rows[0][1].ToString();
            _detectUserName.Text = table.Rows[0][7].ToString();
            _detectTypeName.Text = table.Rows[0][0].ToString();
            _detectvalue.Text = table.Rows[0][18].ToString();

            //检测结果为疑似阳性变红
            if (_resultName.Text == "疑似阳性" || _resultName.Text == "确证阳性")
            {
                _resultName.Foreground = Brushes.Red;
            }
            else
            {
                _resultName.Foreground = Brushes.Black;
            }
        }

        private void exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.Close();
            }
        }

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            this.Left += e.HorizontalChange;
            this.Top += e.VerticalChange;
        }

        private void exit_MouseEnter(object sender, MouseEventArgs e)
        {
            exit.Source = new BitmapImage(new Uri("pack://application:,," + "/res/close_on.png"));
        }

        private void exit_MouseLeave(object sender, MouseEventArgs e)
        {
            exit.Source = new BitmapImage(new Uri("pack://application:,," + "/res/close.png"));
        }
    }
}

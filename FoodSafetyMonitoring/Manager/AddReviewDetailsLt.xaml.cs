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
using DBUtility;
using FoodSafetyMonitoring.Common;
using Toolkit = Microsoft.Windows.Controls;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// AddReviewDetailsLt.xaml 的交互逻辑
    /// </summary>
    public partial class AddReviewDetailsLt : Window
    {
        private IDBOperation dbOperation;
        private DbHelperMySQL dbHelper = null;
        int orderid;
        private SysReviewInfoLt sysreviewinfolt;

        public AddReviewDetailsLt(IDBOperation dbOperation, int id, SysReviewInfoLt sysreviewinfolt)
        {
            InitializeComponent();
            this.dbOperation = dbOperation;
            this.sysreviewinfolt = sysreviewinfolt;
            dbHelper = DbHelperMySQL.CreateDbHelper();

            orderid = id;

            string reviewflag = dbHelper.GetSingle(string.Format("select ReviewFlag from t_detect_report where ORDERID = '{0}'", id)).ToString();
            DataTable table = dbOperation.GetDbHelper().GetDataSet(string.Format("call p_detect_details_lt('{0}')", id)).Tables[0];

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


            _reviewUserid.Text = (Application.Current.Resources["User"] as UserInfo).ShowName;
            _reviewDate.Text = DateTime.Now.ToString();
            ComboboxTool.InitComboboxSource(_reviewResult, "SELECT resultId,resultName FROM t_det_result where openFlag = '1' ORDER BY id", "lr");
            ComboboxTool.InitComboboxSource(_reviewReagent, "select reagentId,reagentName from t_det_reagent where openFlag = '1' and reagentId <> '1'", "lr");

        }

        private void Thumb_DragDelta(object sender, System.Windows.Controls.Primitives.DragDeltaEventArgs e)
        {
            this.Left += e.HorizontalChange;
            this.Top += e.VerticalChange;
        }

        private void exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.Close();
                sysreviewinfolt.GetData();
            }
        }

        private void exit_MouseEnter(object sender, MouseEventArgs e)
        {
            exit.Source = new BitmapImage(new Uri("pack://application:,," + "/res/close_on.png"));
        }

        private void exit_MouseLeave(object sender, MouseEventArgs e)
        {
            exit.Source = new BitmapImage(new Uri("pack://application:,," + "/res/close.png"));
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            txtMsg.Text = "";

            if (chk_1.IsChecked == false && chk_2.IsChecked == false)
            {
                txtMsg.Text = "*请选择原因";
                return;
            }

            if (_reviewReagent.SelectedIndex < 1)
            {
                txtMsg.Text = "*请选择检查方法";
                return;
            }

            if (_reviewResult.SelectedIndex < 1)
            {
                txtMsg.Text = "*请选择检查结果";
                return;
            }

            if (_reviewBz.Text == "")
            {
                txtMsg.Text = "*请输入原因说明";
                return;
            }

            string reviewflag = dbHelper.GetSingle(string.Format("select ReviewFlag from t_detect_report where ORDERID = '{0}'", orderid)).ToString();
            if (reviewflag == "1")
            {
                Toolkit.MessageBox.Show("该检测单已复核过，请确认！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            string reasonid = "";
            if (chk_1.IsChecked == true)
            {
                reasonid = "0";
            }
            else if (chk_2.IsChecked == true)
            {
                reasonid = "1";
            }

            string strSql;
            string strSql2;

            strSql = string.Format(@"update t_detect_report set ReviewFlag= '1' where  ORDERID = '{0}'", orderid);
            strSql2 = string.Format(@"insert into t_detect_review(DetectId,ReviewUserid,ReviewReagentid,ReviewResultid,ReviewDate,ReviewReason,reasonid)
                                      values('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", orderid, (Application.Current.Resources["User"] as UserInfo).ID,
                                      (_reviewReagent.SelectedItem as Label).Tag, (_reviewResult.SelectedItem as Label).Tag, DateTime.Now,
                                      _reviewBz.Text, reasonid);
            try
            {

                int num = dbHelper.ExecuteSql(strSql);
                int num2 = dbHelper.ExecuteSql(strSql2);
                if (num == 1 && num2 == 1)
                {
                    Toolkit.MessageBox.Show("保存成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    btnSave.IsEnabled = false;
                    _reviewReagent.IsEnabled = false;
                    _reviewResult.IsEnabled = false;
                    _reviewBz.IsEnabled = false;
                    chk.IsEnabled = false;
                    return;
                }
                else
                {
                    Toolkit.MessageBox.Show("保存失败！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
            catch (Exception)
            {
                Toolkit.MessageBox.Show("保存失败！", "系统提示", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                return;
            }

            txtMsg.Text = "";
        }

        private void _chk_Checked(object sender, RoutedEventArgs e)
        {
            if ((sender as CheckBox).Name == "chk_1")
            {
                chk_2.IsChecked = false;
            }
            else if ((sender as CheckBox).Name == "chk_2")
            {
                chk_1.IsChecked = false;
            }
        }
    }
}

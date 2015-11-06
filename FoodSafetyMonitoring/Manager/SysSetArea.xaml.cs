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
using System.Data;
using FoodSafetyMonitoring.Common;
using Toolkit = Microsoft.Windows.Controls;
using FoodSafetyMonitoring.Manager.UserControls;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// SysSetArea.xaml 的交互逻辑
    /// </summary>
    public partial class SysSetArea : UserControl
    {
        public IDBOperation dbOperation = null;
        public CheckBox[] chk;
        public string deptid;
        public string userid;

        public SysSetArea(IDBOperation dbOperation)
        {
            InitializeComponent();
            this.dbOperation = dbOperation;
            userid = (Application.Current.Resources["User"] as UserInfo).ID;
            deptid = (Application.Current.Resources["User"] as UserInfo).DepartmentID;

            chk = new CheckBox[] { chk_1, chk_2, chk_3, chk_4, chk_5, chk_6, chk_7, chk_8, chk_9
            , chk_10, chk_11, chk_12, chk_13, chk_14, chk_15, chk_16, chk_17, chk_18, chk_19, chk_20
            , chk_21, chk_22, chk_23, chk_24, chk_25, chk_26, chk_27, chk_28, chk_29, chk_30, chk_31};

            getdata();
        }

        private void getdata()
        {
            DataTable table = dbOperation.GetDbHelper().GetDataSet("select proviceid from t_set_area where deptid = " + deptid).Tables[0];
            string proviceid;
            string tag;
            for(int i = 0 ; i < table.Rows.Count;i ++)
            {
                proviceid = table.Rows[i][0].ToString();
                for(int j = 0 ; j < chk.Length; j ++)
                {
                    tag = chk[j].Tag.ToString();
                    if (proviceid == tag)
                    {
                        chk[j].IsChecked = true;
                    }
                }
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string provice = "";
            string tag;
            for (int j = 0; j < chk.Length; j++)
            {
                if (chk[j].IsChecked == true)
                {
                    tag = chk[j].Tag.ToString();
                    provice = provice + tag + ",";
                }
            }

            try
            {
                int result = dbOperation.GetDbHelper().ExecuteSql(string.Format("call p_set_area ('{0}','{1}','{2}')",
                                                    userid,deptid, provice));

                if (result > 0)
                {
                    Toolkit.MessageBox.Show("来源产地设置成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                else
                {
                    Toolkit.MessageBox.Show("来源产地设置失败！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                Toolkit.MessageBox.Show("来源产地设置失败2！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

        }
    }
}

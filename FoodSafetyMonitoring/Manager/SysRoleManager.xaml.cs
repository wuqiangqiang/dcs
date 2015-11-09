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
using Toolkit = Microsoft.Windows.Controls;
using DBUtility;
using System.Data;
using FoodSafetyMonitoring.Common;
using FoodSafetyMonitoring.dao;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// Test.xaml 的交互逻辑
    /// </summary>
    public partial class SysRoleManager : UserControl
    {
        private DbHelperMySQL dbHelper = null;
        private System.Data.DataTable dt_role_flag = new System.Data.DataTable();
        private DataTable currentTable = new DataTable();
        public SysRoleManager()
        {
            InitializeComponent();

            //赋值角色级别下拉选择框
            dt_role_flag.Columns.Add(new DataColumn("levelid"));
            dt_role_flag.Columns.Add(new DataColumn("levelname"));
            var row2 = dt_role_flag.NewRow();
            row2["levelid"] = "1";
            row2["levelname"] = "省级";
            dt_role_flag.Rows.Add(row2);
            var row3 = dt_role_flag.NewRow();
            row3["levelid"] = "2";
            row3["levelname"] = "市(州)";
            dt_role_flag.Rows.Add(row3);
            var row4 = dt_role_flag.NewRow();
            row4["levelid"] = "3";
            row4["levelname"] = "区县";
            dt_role_flag.Rows.Add(row4);
            var row5 = dt_role_flag.NewRow();
            row5["levelid"] = "4";
            row5["levelname"] = "检测单位";
            dt_role_flag.Rows.Add(row5);
            ComboboxTool.InitComboboxSource(role_flag, dt_role_flag, "lr");

            //赋值检测环节下拉选择框
            ComboboxTool.InitComboboxSource(role_type, "select typeId,typeName from t_dept_type", "lr");

            this.Loaded += new RoutedEventHandler(Test_Loaded);

        }

        void Test_Loaded(object sender, RoutedEventArgs e)
        {
            BindData();
            //Clear();
        }

        /// <summary>
        /// 从数据库获取数据
        /// </summary>
        /// 
        private void BindData()
        {
            DataTable dt = null;
            lvlist.DataContext = null;
            string strSql = string.Format("select NUMB_ROLE,INFO_NAME,INFO_EXPL,FLAG_TIER,roletype FROM sys_client_role where cuserid = '{0}'", (Application.Current.Resources["User"] as UserInfo).ID);
            try
            {
                dbHelper = DbHelperMySQL.CreateDbHelper();
                dt = dbHelper.GetDataSet(strSql).Tables[0];
                lvlist.DataContext = dt;
                currentTable = dt;
            }
            catch (Exception)
            {
                return;
            }
        }

        //判断角色下是否存在人员
        private bool isExistRoleName(string oid)
        {
            string sql = "";
            bool result = false;
            try
            {
                sql = string.Format("select RECO_PKID from sys_client_user where ROLE_ID ='{0}'", oid);

                if (Exists(sql))
                {
                    result = true;
                }
            }

            catch (Exception ex)
            {
                result = false;
            }
            return result;
        }
        public bool Exists(string strSql)
        {
            object obj = dbHelper.GetSingle(strSql);
            string cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = "";
            }
            else
            {
                cmdresult = obj.ToString();
            }
            if (cmdresult == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void Clear()
        {
            this.role_details.Visibility = Visibility.Visible;
            this.txt_RoleName.IsEnabled = false;
            this.txt_RoleExplain.IsEnabled = false;
            this.role_flag.IsEnabled = false;
            this.role_type.IsEnabled = false;
            this.txt_RoleName.Text = "";
            this.txt_RoleExplain.Text = "";
            this.role_flag.SelectedIndex = 0;
            this.role_type.SelectedIndex = 0;
            this.btnSave.Tag = null;
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (Toolkit.MessageBox.Show("确定要删除该角色吗？", "询问", MessageBoxButton.YesNoCancel, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                string id = (sender as Button).Tag.ToString();
                if (!isExistRoleName(id))//判断角色下是否存在人员
                {
                    if (DeleteUser(id))
                    {
                        Toolkit.MessageBox.Show("删除成功！", "系统提示");
                        BindData();
                        Clear();
                    }
                    else
                    {
                        Toolkit.MessageBox.Show("删除失败！", "系统提示");
                    }
                }
                else
                {
                    Toolkit.MessageBox.Show("角色已分配给用户，不能删除！", "系统提示");
                    return;
                }
            }
            else
            {
                e.Handled = false;
            }
        }
        private bool DeleteUser(string id)
        {
            bool result = false;
            string strSql = string.Format("delete from sys_client_role where NUMB_ROLE=" + id);
            try
            {
                int num = dbHelper.ExecuteSql(strSql);
                if (num == 1)
                {
                    result = true;
                }
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            this.txt_RoleName.IsEnabled = true;
            this.txt_RoleExplain.IsEnabled = true;
            this.role_flag.IsEnabled = true;
            this.role_type.IsEnabled = true;

            string id = (sender as Button).Tag.ToString();
            DataRow[] drs = currentTable.Select("NUMB_ROLE=" + id);
            if (drs.Length == 1)
            {
                this.txt_RoleName.Tag = drs[0]["NUMB_ROLE"].ToString();
                this.txt_RoleName.Text = drs[0]["INFO_NAME"].ToString();
                this.txt_RoleExplain.Text = drs[0]["INFO_EXPL"].ToString();
                for (int i = 0; i < role_flag.Items.Count; i++)
                {
                    if ((role_flag.Items[i] as Label).Tag.ToString() == drs[0]["FLAG_TIER"].ToString())
                    {
                        role_flag.SelectedItem = role_flag.Items[i];
                        break;
                    }
                }
                for (int i = 0; i < role_type.Items.Count; i++)
                {
                    if ((role_type.Items[i] as Label).Tag.ToString() == drs[0]["roletype"].ToString())
                    {
                        role_type.SelectedItem = role_type.Items[i];
                        break;
                    }
                }
                this.btnSave.Tag = id;
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (this.txt_RoleName.Text.Trim() == "")
            {
                Toolkit.MessageBox.Show("角色名不能为空!", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            //根据btnSave按钮的Tag属性判断是添加还是修改（null为添加，反之为修改）
            string strSql = string.Empty;
            if (btnSave.Tag == null)
            {
                strSql = string.Format("call p_insert_role({0},'{1}','{2}','{3}','{4}')",
                        (Application.Current.Resources["User"] as UserInfo).ID, txt_RoleName.Text,txt_RoleExplain.Text,
                         role_flag.SelectedIndex < 1 ? "" : (role_flag.SelectedItem as Label).Tag.ToString(),
                         role_type.SelectedIndex < 1 ? "" : (role_type.SelectedItem as Label).Tag.ToString());
            }
            else
            {
                strSql = string.Format(@"update sys_client_role set INFO_NAME='{0}',INFO_EXPL='{1}',FLAG_TIER='{2}',roletype='{3}',cuserid ='{4}'   
                                         where NUMB_ROLE={5} ", txt_RoleName.Text, txt_RoleExplain.Text.Trim(),
                                           role_flag.SelectedIndex < 1 ? "" : (role_flag.SelectedItem as Label).Tag.ToString(),
                                           role_type.SelectedIndex < 1 ? "" : (role_type.SelectedItem as Label).Tag.ToString(), 
                                          (Application.Current.Resources["User"] as UserInfo).ID,
                                          btnSave.Tag.ToString());
            }

            try
            {

                int num = dbHelper.ExecuteSql(strSql);
                if (num == 1)
                {
                    Toolkit.MessageBox.Show("保存成功！", "系统提示", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    Clear();
                    BindData();
                }
                else
                {
                    Toolkit.MessageBox.Show("保存失败！", "系统提示", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                    return;
                }
            }
            catch (Exception)
            {
                Toolkit.MessageBox.Show("保存失败！", "系统提示", MessageBoxButton.OKCancel, MessageBoxImage.Information);
                return;
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            Clear();
            this.txt_RoleName.IsEnabled = true;
            this.txt_RoleExplain.IsEnabled = true;
            this.role_flag.IsEnabled = true;
            this.role_type.IsEnabled = true;
        }


        /// <summary>
        /// 从内存表中查询角色名称
        /// </summary>

        private void SelectRole(string RoleName, string Rolelevel)
        {

            StringBuilder strsql = new StringBuilder();

            DataTable dt_SelectRole = null;
            lvlist.DataContext = null;
            strsql.Append("select NUMB_ROLE,INFO_NAME,INFO_EXPL,FLAG_TIER FROM sys_client_role where 1=1 ");

            if (RoleName != "")
            {

                strsql.Append(" and INFO_NAME like '%" + RoleName + "%'");

            }
            if (Rolelevel != "-1")
            {
                strsql.Append(" and FLAG_TIER = " + Convert.ToInt16(Rolelevel) + "");

            }
            try
            {
                dt_SelectRole = dbHelper.GetDataSet(strsql.ToString()).Tables[0];
                lvlist.DataContext = null;
                lvlist.DataContext = dt_SelectRole;

            }
            catch (Exception)
            {
                return;
            }


        }
    }
    public class Myinfo
    {
        private string _info;

        public string Info
        {
            get { return _info; }
            set
            {
                _info = value;
            }
        }

        private string _content;
        public string Content
        {
            get { return _content; }
            set
            {
                _content = value;
            }
        }

        public string ImgSource
        {
            get;
            set;
        }
    }
}

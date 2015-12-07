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
using System.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Toolkit = Microsoft.Windows.Controls;
using FoodSafetyMonitoring.dao;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// SysRolePowerManager.xaml 的交互逻辑
    /// </summary>
    public partial class SysRolePowerManager : UserControl

    {
        private DBUtility.DbHelperMySQL dbHelper = null;
        private DataTable currentTable = new DataTable();

        public SysRolePowerManager()
        {
            InitializeComponent(); 
            BindListView();
            ShowAllRight();
        }

        private void BindListView()
        {
            string strSql = "SELECT NUMB_ROLE,cdeptid,cuserid,INFO_NAME,INFO_EXPL,FLAG_TIER,roletype FROM sys_client_role WHERE cuserid = " + (Application.Current.Resources["User"] as UserInfo).ID;
            try
            {
                dbHelper = DBUtility.DbHelperMySQL.CreateDbHelper();
                DataSet ds = dbHelper.GetDataSet(strSql);
                lvlist.DataContext = ds.Tables[0];
                currentTable = ds.Tables[0];
            }
            catch (Exception)
            {
                return;
            }
        }

        public void ShowAllRight()
        {
            DataTable dt = GetMenuTableByMenuRight();
            if (dt != null)
            {
                TreeItem tree = new TreeItem();
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["SUB_FATHER_ID"].ToString() == "0")//加载第一级菜单
                    {
                        TreeItem ti = new TreeItem();
                        ti.tag = dt.Rows[i]["SUB_ID"].ToString().Trim(); ;
                        ti.text = dt.Rows[i]["SUB_NAME"].ToString().Trim();
                        ti.parent = tree;
                        LoadTree(dt, ti);//递归函数
                    }
                }
                tvPermissions.ItemsSource = tree.children;
            }
        }

        /// <summary> 根据菜单权限得到系统菜单表
        /// </summary>
        /// <param name="menuCode">菜单权限</param>
        /// <returns></returns>
        public DataTable GetMenuTableByMenuRight()
        {
            string strSql = "SELECT rp.SUB_ID,s.SUB_NAME,s.SUB_FATHER_ID " +
                            "FROM sys_sub_new s ,sys_rolepermission_new rp , sys_client_user u " +
                            "WHERE s.SUB_ID = rp.SUB_ID " +
                            "AND rp.ROLE_ID = u.ROLE_ID " +
                            "AND u.RECO_PKID = " + (Application.Current.Resources["User"] as UserInfo).ID;
            try
            {
                DataTable dt = dbHelper.GetDataSet(strSql).Tables[0];
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void LoadTree(DataTable dt, TreeItem ti)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["SUB_FATHER_ID"].ToString().Trim() == ti.tag.ToString())
                {
                    TreeItem tiTmp = new TreeItem();
                    tiTmp.tag = dt.Rows[i]["SUB_ID"].ToString();


                    tiTmp.text = dt.Rows[i]["SUB_NAME"].ToString();
                    tiTmp.parent = ti;
                    LoadTree(dt,tiTmp);
                }
            }
        }

        private void btnModify_Click(object sender, RoutedEventArgs e)
        {
            Clear();

            //声明一个变量：用于记录该父菜单是否有子菜单
            int flag = 0;

            string id = (sender as Button).Tag.ToString();

            List<string> list = GetUserRoleMenu(id);

            //循环用户的权限列表
            foreach (string item in list)
            {
                //循环菜单树形结构
                foreach (var ti in tvPermissions.Items)//一级菜单
                {
                    flag = 0;
                    foreach (var tii in ((TreeItem)ti).children)//二级菜单
                    {
                        flag = 1;
                        foreach (var tiii in tii.children)//三级菜单
                        {
                            flag = 2;

                            if ((tiii as TreeItem).tag.ToString() == item)
                            {
                                (tiii as TreeItem).IsChecked = true;
                                goto con_for;
                            }
                        }

                        //只有当二级菜单无子菜单的情况下，才对二级菜单进行打勾
                        if (flag == 1)
                        {
                            if ((tii as TreeItem).tag.ToString() == item)
                            {
                                (tii as TreeItem).IsChecked = true;
                                goto con_for;
                            }
                        }
                    }
                    //只有当父菜单无子菜单的情况下，才对父菜单进行打勾
                    if (flag == 0)
                    {
                        if ((ti as TreeItem).tag.ToString() == item)
                        {
                            (ti as TreeItem).IsChecked = true;
                            goto con_for;
                        }
                    }
                }
                con_for : continue;
            }

            btnSave.Tag = id;
        }

        /// <summary>
        /// 获取角色菜单权限
        /// </summary>
        /// <param name="id">角色代码</param>
        private List<string> GetUserRoleMenu(string id)
        {
            List<string> list = new List<string>();
            string strSql = "SELECT rp.SUB_ID,s.SUB_NAME FROM sys_sub_new s ,sys_rolepermission_new rp WHERE s.SUB_ID = rp.SUB_ID AND rp.ROLE_ID =" + id;
            try
            {
                DataTable dt = dbHelper.GetDataSet(strSql).Tables[0];
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(dt.Rows[i]["SUB_ID"].ToString());
                }
            }
            catch (Exception)
            {

            }
            return list;
        }


        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (btnSave.Tag != null)
            {
                List<string> list_roleMenu = new List<string>();

                foreach (TreeItem ti in tvPermissions.Items)//遍历第一级目录
                {
                    if (ti.IsChecked == true || ti.IsChecked == null)
                    {
                        list_roleMenu.Add(ti.tag.ToString());
                    }

                    foreach (TreeItem tii in ti.children)//遍历第二级目录
                    {
                        if (tii.IsChecked == true || tii.IsChecked == null)
                        {
                            list_roleMenu.Add(tii.tag.ToString());
                        }
 
                        foreach (TreeItem tiii in tii.children)//遍历第三级目录
                        {
                            if (tiii.IsChecked == true)
                            {
                                list_roleMenu.Add(tiii.tag.ToString());
                            }
                        }
                    }
                }

                StringBuilder s_list_roleMenu = new StringBuilder();
                for (int i = 0; i < list_roleMenu.Count; i++)
                {
                    s_list_roleMenu.Append(list_roleMenu[i]);
                    if (i < list_roleMenu.Count - 1)
                    {
                        s_list_roleMenu.Append(",");
                    }
                }

                try
                {
                    int count = dbHelper.ExecuteSql(string.Format("SELECT f_role_sub('{0}','{1}')", btnSave.Tag.ToString(), s_list_roleMenu.ToString()));
                    if (count != 0)
                    {
                        Toolkit.MessageBox.Show("保存成功！", "系统提示", MessageBoxButton.OKCancel);
                        Common.SysLogEntry.WriteLog("角色权限管理", (Application.Current.Resources["User"] as UserInfo).ShowName, Common.OperationType.Modify, "修改角色权限");
                        return;
                    }
                    else
                    {
                        Toolkit.MessageBox.Show("保存失败！", "系统提示", MessageBoxButton.OKCancel);
                        return;
                    }
                }
                catch (Exception)
                {
                    Toolkit.MessageBox.Show("保存失败！", "系统提示", MessageBoxButton.OKCancel);
                    return;
                }
            }
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            this.btnSave.Tag = null;

            foreach (TreeItem item in tvPermissions.Items)
            {
                DiguiClearChecked(item);
            }
        }

        /// <summary>
        /// 递归取消选中
        /// </summary>
        /// <param name="ti"></param>
        private void DiguiClearChecked(TreeItem ti)
        {
            if (ti.children.Count > 0)
            {
                foreach (TreeItem tii in ti.children)
                {
                    if (tii.IsChecked == true)
                    {
                        tii.IsChecked = false;
                    }

                    DiguiClearChecked(tii);
                }
            }
            else
            {
                if (ti.IsChecked == true)
                {
                    ti.IsChecked = false;
                }
            }
        }
    }

    public class TreeItem : INotifyPropertyChanged
    {
        // 构造函数
        public TreeItem()
        {
            children = new ObservableCollection<TreeItem>();
        }

        public object tag
        {
            get;
            set;
        }

        //////////////////////////////////////////////////////////////////////////
        // 节点文字信息
        public string text
        {
            get;
            set;
        }
        // 节点图标路径
        public string itemIcon
        {
            get;
            set;
        }
        // 节点其他信息
        // ...

        public bool isTheChild(TreeItem p)
        {
            bool result = false;
            for (int i = 0; i < p.children.Count; i++)
            {
                if (p.children[i] == this)
                {
                    result = true;
                }
            }
            return result;
        }

        // 父节点
        private TreeItem _parent;
        public TreeItem parent
        {
            get
            {
                return this._parent;
            }
            set
            {
                if (!isTheChild(value))
                {
                    value.children.Add(this);
                    this._parent = value;
                }
            }
        }

        // 子节点
        public ObservableCollection<TreeItem> children
        {
            get;
            set;
        }
        //////////////////////////////////////////////////////////////////////////

        ////////   Check 相关信息   ///////////////////////////////////////////
        bool? _isChecked = false;

        public bool? IsChecked
        {
            get { return _isChecked; }
            set { this.SetIsChecked(value, true, true); }
        }

        void SetIsChecked(bool? value, bool updateChildren, bool updateParent)
        {
            if (value == _isChecked)
                return;

            _isChecked = value;

            if (updateChildren && _isChecked.HasValue)
            {
                foreach (TreeItem child in children)
                {
                    child.SetIsChecked(_isChecked, true, false);
                }
            }

            if (updateParent && parent != null)
            {
                parent.VerifyCheckState();
            }

            this.OnPropertyChanged("IsChecked");
        }

        void VerifyCheckState()
        {
            bool? state = null;
            for (int i = 0; i < this.children.Count; ++i)
            {
                bool? current = this.children[i].IsChecked;
                if (i == 0)
                {
                    state = current;
                }
                else if (state != current)
                {
                    state = null;
                    break;
                }
            }
            this.SetIsChecked(state, false, true);
        }
        ////////////////////////////////////////////////////////////////////////////


        void OnPropertyChanged(string prop)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        //////////////////////////////////////////////////////////////////////////
    }
}

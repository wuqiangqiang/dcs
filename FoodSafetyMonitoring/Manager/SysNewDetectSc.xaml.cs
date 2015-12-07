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
using DBUtility;

using FoodSafetyMonitoring.Manager.UserControls;

namespace FoodSafetyMonitoring.Manager
{
    /// <summary>
    /// UcDetectBillManager.xaml 的交互逻辑
    /// </summary>
    /// 
    public partial class SysNewDetectSc : UserControl
    {
        DataTable ProvinceCityTable;
        DbHelperMySQL dbOperation;

        string userId = (Application.Current.Resources["User"] as UserInfo).ID;
        string supplierId = (Application.Current.Resources["User"] as UserInfo).SupplierId;
        string deptid = (Application.Current.Resources["User"] as UserInfo).DepartmentID;

        public SysNewDetectSc()
        {
            InitializeComponent();
            dbOperation = DBUtility.DbHelperMySQL.CreateDbHelper();
            ProvinceCityTable = Application.Current.Resources["省市表"] as DataTable;
            DataTable table = dbOperation.GetDataSet("select proviceid as id , name " +
                                                       "from t_set_area LEFT JOIN sys_city  ON t_set_area.proviceid = sys_city.id " +
                                                        "where  deptid = " + deptid).Tables[0];
            DataRow[] rows;
            if (table.Rows.Count == 0)
            {
                rows = ProvinceCityTable.Select("pid = '0001'");
            }
            else
            {
                rows = table.Select();
            } 

            //画面初始化-新增检测单画面
            ComboboxTool.InitComboboxSource(_province, rows,"lr");
            _province.SelectionChanged += new SelectionChangedEventHandler(_province_SelectionChanged);

            //查找登录者部门所属的省份
            string proviceid = dbOperation.GetSingle("select province from sys_client_sysdept where INFO_CODE = " + deptid).ToString();
            int i = 1;
            foreach(DataRow row in rows)
            {
                if (row["id"].ToString() == proviceid)
                {
                    _province.SelectedIndex = i;
                }
                i = i + 1;
            }

            ComboboxTool.InitComboboxSource(_source_company, "SELECT COMPANYID,COMPANYNAME FROM v_user_company WHERE userid =  " + userId, "lr");
            ComboboxTool.InitComboboxSource(_detect_item, string.Format("SELECT ItemID,ItemNAME FROM t_det_item WHERE OPENFLAG = '1'"), "lr");
            _detect_item.SelectionChanged += new SelectionChangedEventHandler(_detect_item_SelectionChanged);
            ComboboxTool.InitComboboxSource(_detect_method, string.Format("SELECT reagentId,reagentName FROM t_det_reagent WHERE OPENFLAG = '1'"), "lr");
            ComboboxTool.InitComboboxSource(_detect_object, string.Format("SELECT objectId,objectName FROM t_det_object WHERE OPENFLAG = '1'"), "lr");
            ComboboxTool.InitComboboxSource(_detect_result, "SELECT resultId,resultName FROM t_det_result where openFlag = '1' ORDER BY id", "lr");
            _entering_datetime.Text = string.Format("{0:g}", System.DateTime.Now);
            _source_company.SelectionChanged += new SelectionChangedEventHandler(_source_company_SelectionChanged);
            _detect_person.Text = (Application.Current.Resources["User"] as UserInfo).ShowName;
            _detect_site.Text = dbOperation.GetSingle("SELECT INFO_NAME  from  sys_client_sysdept WHERE INFO_CODE = " + (Application.Current.Resources["User"] as UserInfo).DepartmentID).ToString();

        }

        private void clear()
        {
            this._detect_item.SelectedIndex = 0;
            //this._detect_method1.IsChecked = false;
            //this._detect_method2.IsChecked = false;
            //this._detect_method3.IsChecked = false;
            this._detect_method.SelectedIndex = 0;
            this._detect_object.SelectedIndex = 0;
            this._detect_value.Text = "";
            this._detect_result.SelectedIndex = 0;
            this._entering_datetime.Text = string.Format("{0:g}", System.DateTime.Now);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_province.SelectedIndex < 1)
            {
                Toolkit.MessageBox.Show("请选择省！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (_city.SelectedIndex < 1)
            {
                Toolkit.MessageBox.Show("请选择市！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (_region.SelectedIndex < 1)
            {
                Toolkit.MessageBox.Show("请选择区！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (_source_company.SelectedIndex == 0 || _source_company.Text == "")
            {
                Toolkit.MessageBox.Show("请选择被检单位！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (_detect_item.SelectedIndex < 1)
            {
                Toolkit.MessageBox.Show("请选择检查项目！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            //if (_detect_method1.IsChecked != true && _detect_method2.IsChecked != true && _detect_method3.IsChecked != true)
            if (_detect_method.SelectedIndex < 1)
            {
                Toolkit.MessageBox.Show("请选择检测方法！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (_detect_object.SelectedIndex < 1)
            {
                Toolkit.MessageBox.Show("请选择检测对象！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (_detect_value.Text.Trim().Length == 0)
            {
                Toolkit.MessageBox.Show("请输入检测值！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (_detect_result.SelectedIndex < 1)
            {
                Toolkit.MessageBox.Show("请选择检测结果！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            if (_detect_person.Text.Trim().Length == 0)
            {
                Toolkit.MessageBox.Show("请选择检测师！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            
            string company_id;

            //判断被检单位是否存在，若不存在则插入数据库
            bool exit_flag = dbOperation.Exists(string.Format("SELECT count(COMPANYID) from t_company where COMPANYNAME ='{0}' and deptid = '{1}'", _source_company.Text, (Application.Current.Resources["User"] as UserInfo).DepartmentID));
            if (!exit_flag)
            {
                int n = dbOperation.ExecuteSql(string.Format("INSERT INTO t_company (COMPANYNAME,AREAID,OPENFLAG,deptid,cuserid,cdate) VALUES('{0}','{1}','{2}','{3}','{4}','{5}')",
                                                                _source_company.Text,
                                                                (_region.SelectedItem as Label).Tag.ToString(),
                                                                '1', (Application.Current.Resources["User"] as UserInfo).DepartmentID,
                                                                (Application.Current.Resources["User"] as UserInfo).ID, DateTime.Now));
                if (n == 1)
                {
                    company_id = dbOperation.GetSingle(string.Format("SELECT COMPANYID from t_company where COMPANYNAME ='{0}' and deptid = '{1}'", _source_company.Text, (Application.Current.Resources["User"] as UserInfo).DepartmentID)).ToString();
                }
                else
                {
                    Toolkit.MessageBox.Show("被检单位添加失败！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
            }
            else
            {
                company_id = dbOperation.GetSingle(string.Format("SELECT COMPANYID from t_company where COMPANYNAME ='{0}' and deptid = '{1}'", _source_company.Text, (Application.Current.Resources["User"] as UserInfo).DepartmentID)).ToString();
            }

            //判断检测模式：若为农药残留检测，模式为0；否则模式为1
            string detect_mode = "";
            if((_detect_item.SelectedItem as Label).Tag.ToString() == "1")
            {
                detect_mode = "0";
            }
            else
            {
                detect_mode = "1";
            }

            string sql = string.Format("call p_insert_detect_sc('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}')"
                            , company_id,
                            (_detect_item.SelectedItem as Label).Tag.ToString(),
                            (_detect_method.SelectedItem as Label).Tag.ToString(),
                            //(_detect_method1.IsChecked == true ? 1 : 0) + (_detect_method2.IsChecked == true ? 2 : 0) + (_detect_method3.IsChecked == true ? 3 : 0),
                            (_detect_object.SelectedItem as Label).Tag.ToString(),
                            _detect_value.Text, detect_mode,
                            (_detect_result.SelectedItem as Label).Tag.ToString(),
                            (Application.Current.Resources["User"] as UserInfo).DepartmentID,
                            (Application.Current.Resources["User"] as UserInfo).ID,
                            System.DateTime.Now);


            int i = dbOperation.ExecuteSql(sql);
            if (i == 1)
            {
                Toolkit.MessageBox.Show("添加成功！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                Common.SysLogEntry.WriteLog("生产加工环节检测单录入", (Application.Current.Resources["User"] as UserInfo).ShowName, OperationType.Add, "新增检测单");
                clear();
                ComboboxTool.InitComboboxSource(_source_company, "SELECT COMPANYID,COMPANYNAME FROM v_user_company WHERE userid =  " + userId, "lr");
            }
            else
            {
                Toolkit.MessageBox.Show("添加失败！", "系统提示", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            this._province.SelectedIndex = 0;
            this._city.SelectedIndex = 0;
            this._region.SelectedIndex = 0;
            this._source_company.SelectedIndex = 0;
            this._detect_item.SelectedIndex = 0;
            this._detect_method.SelectedIndex = 0;
            //this._detect_method1.IsChecked = false;
            //this._detect_method2.IsChecked = false;
            //this._detect_method3.IsChecked = false;
            this._detect_object.SelectedIndex = 0;
            this._detect_value.Text = "";
            this._detect_result.SelectedIndex = 0;
            this._entering_datetime.Text = string.Format("{0:g}", System.DateTime.Now);
        }

        //private void _detect_method1_Checked(object sender, RoutedEventArgs e)
        //{
        //    if ((sender as CheckBox).Name == "_detect_method1")
        //    {
        //        _detect_method2.IsChecked = false;
        //        _detect_method3.IsChecked = false;
        //    }
        //    else if ((sender as CheckBox).Name == "_detect_method2")
        //    {
        //        _detect_method1.IsChecked = false;
        //        _detect_method3.IsChecked = false;
        //    }
        //    else if ((sender as CheckBox).Name == "_detect_method3")
        //    {
        //        _detect_method1.IsChecked = false;
        //        _detect_method2.IsChecked = false;
        //    }
        //}


        void _source_company_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //获取变更前的状态
            bool flag = _province.IsEnabled ;
            
            //被检单位下拉选择的是有效内容，则将省市区的下拉灰显并且自动赋值
            if (_source_company.SelectedIndex >= 1)
            {
                _province.IsEnabled = false;
                _city.IsEnabled = false;
                _region.IsEnabled = false;

                string areaid = dbOperation.GetDataSet("SELECT AREAID from t_company where COMPANYID = " + (_source_company.SelectedItem as Label).Tag.ToString()).Tables[0].Rows[0][0].ToString();

                _source_company.Tag = areaid;
                if (areaid.Length > 0)
                {
                    string _areaid = areaid.Substring(0, 2);
                    _province.Text = ProvinceCityTable.Select("id = '" + _areaid + "'")[0]["name"].ToString();
                }
                if (areaid.Length > 2)
                {
                    string _areaid = areaid.Substring(0, 4);
                    _city.Text = ProvinceCityTable.Select("id = '" + _areaid + "'")[0]["name"].ToString();
                }
                if (areaid.Length > 4)
                {
                    _region.Text = ProvinceCityTable.Select("id = '" + areaid + "'")[0]["name"].ToString();
                }
            }
            //被检单位下拉选择的是“-请选择-”或是手动输入被检单位，则将省市区的下拉激活并且内容清空
            else if (_source_company.SelectedIndex < 1)
            {
                if (flag == false)
                {
                    _province.IsEnabled = true;
                    _city.IsEnabled = true;
                    _region.IsEnabled = true;

                    _province.SelectedIndex = 0;
                    _city.SelectedIndex = 0;
                    _region.SelectedIndex = 0;
                }
            }
        }

        void _detect_item_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_detect_item.SelectedIndex > 0)
            {
                //如果检测项目是农药残留，则检测方法为酶抑制法
                if((_detect_item.SelectedItem as Label).Tag.ToString() == "1")
                {
                    ComboboxTool.InitComboboxSource(_detect_method, string.Format("SELECT reagentId,reagentName FROM t_det_reagent WHERE OPENFLAG = '1' and (detectmode = '0' or ifnull(detectmode,'') = '')"), "lr");
                }
                else
                {
                    ComboboxTool.InitComboboxSource(_detect_method, string.Format("SELECT reagentId,reagentName FROM t_det_reagent WHERE OPENFLAG = '1' and (detectmode = '1' or ifnull(detectmode,'') = '')"), "lr");
                }
            }
        }

        void _province_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_province.SelectedIndex > 0)
            {
                DataRow[] rows = ProvinceCityTable.Select("pid = '" + (_province.SelectedItem as Label).Tag.ToString() + "'");
                ComboboxTool.InitComboboxSource(_city, rows, "lr");
                _city.SelectionChanged += new SelectionChangedEventHandler(_city_SelectionChanged);
            }
        }


        void _city_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_city.SelectedIndex > 0)
            {
                DataRow[] rows = ProvinceCityTable.Select("pid = '" + (_city.SelectedItem as Label).Tag.ToString() + "'");
                ComboboxTool.InitComboboxSource(_region, rows, "lr");
            }
        }

        private void Detect_Value_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }
        private void Detect_Value_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!isNumbericOrDot(text))
                { e.CancelCommand(); }
            }
            else { e.CancelCommand(); }
        }

        private void Detect_Value_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!isNumbericOrDot(e.Text))
            {
                e.Handled = true;
            }
            else
                e.Handled = false;
        }

        //isDigit是否是数字
        public static bool isNumbericOrDot(string _string)
        {
            if (string.IsNullOrEmpty(_string))

                return false;
            foreach (char c in _string)
            {
                if (!(char.IsDigit(c) || c == '.'))
                    //if(c<'0' c="">'9')//最好的方法,在下面测试数据中再加一个0，然后这种方法效率会搞10毫秒左右
                    return false;
            }
            return true;
        }
    }
}

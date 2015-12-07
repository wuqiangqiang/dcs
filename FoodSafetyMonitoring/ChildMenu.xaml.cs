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
using System.Windows.Interop;
using System.Windows.Forms.Integration;
using FoodSafetyMonitoring.Manager;
using System.Data;

namespace FoodSafetyMonitoring
{
    /// <summary>
    /// ChildMenu.xaml 的交互逻辑
    /// </summary>
    public partial class ChildMenu : UserControl
    {
        private List<MyChildMenu> childMenus;
        public ChildMenu(List<MyChildMenu> childMenus)
        {
            InitializeComponent();
            this.childMenus = childMenus;
            //定义数组存放：二级菜单外部大控件
            Expander[] expanders = new Expander[] { _expander_0, _expander_1, _expander_2, _expander_3, _expander_4, _expander_5, _expander_6 };

            //先让所有控件都可见
            for (int i = 0; i < 7; i++)
            {
                expanders[i].Visibility = Visibility.Visible;
            }
            //再根据二级菜单的个数隐藏部门控件
            for (int i = childMenus.Count; i < 7; i++)
            {
                expanders[i].Visibility = Visibility.Hidden;
            }
            //加载二、三级菜单
            loadMenu();
        }

        public void loadMenu()
        {
            //定义数组存放：三级菜单的控件
            Grid[] grids = new Grid[] { _grid_0, _grid_1, _grid_2, _grid_3, _grid_4, _grid_5, _grid_6 };
            //定义数组存放：二级菜单的控件
            TextBlock[] texts = new TextBlock[] { _text_0, _text_1, _text_2, _text_3, _text_4, _text_5, _text_6 };
            //先将三级菜单控件进行清空
            for (int i = 0; i < grids.Length; i++)
            {
                grids[i].Children.Clear();
            }

            for (int i = 0; i < childMenus.Count; i++)
            {
                //二级菜单文字
                texts[i].Text = childMenus[i].name;

                //三级菜单
                int j = 0;
                foreach (DataRow row in childMenus[i].child_childmenu)
                {
                    //插入一行
                    grids[i].RowDefinitions.Add(new RowDefinition());
                    grids[i].RowDefinitions[j].Height = new GridLength(35, GridUnitType.Pixel);

                    //插入button
                    childMenus[i].buttons[j].SetValue(Grid.RowProperty, j);
                    grids[i].Children.Add(childMenus[i].buttons[j]);

                    j = j + 1;
                }
            }
        }

    }

    public class MyChildMenu
    {
        //public Button btn;
        public List<Button> buttons;
        public string name;
        MainWindow mainWindow;
        public TabControl tab;
        public DataRow[] child_childmenu;
        TabItem temptb = null;

        public MyChildMenu(string name, MainWindow mainWindow, DataRow[] child_childmenu)
        {
            this.name = name;
            this.mainWindow = mainWindow;
            this.child_childmenu = child_childmenu;
            this.tab = mainWindow._tab;
            buttons = new List<Button>();


            foreach (DataRow row in child_childmenu)
            {
                Button btn = new Button();
                btn.Content = row["SUB_NAME"].ToString();
                btn.Tag = row["SUB_ID"].ToString();
                btn.VerticalAlignment = VerticalAlignment.Center;
                btn.Click += new RoutedEventHandler(this.btn_Click);
                buttons.Add(btn);
            }
        }

        private void btn_Click(object sender, System.EventArgs e)
        {
            //(sender as Button).Foreground = Brushes.White;
            int flag_exits = 0;
            foreach (TabItem item in tab.Items)
            {
                if (item.Tag.ToString() == (sender as Button).Tag.ToString())
                {
                    tab.SelectedItem = item;
                    flag_exits = 1;
                    break;
                }
            }
            if (flag_exits == 0)
            {
                int flag = 0;
                temptb = new TabItem();
                temptb.Tag = (sender as Button).Tag.ToString();
                switch ((sender as Button).Tag.ToString())
                {
                    //首页-地图
                    case "1": temptb.Header = (sender as Button).Content.ToString();
                        temptb.Content = new UcMainPage();
                        flag = 1;
                        break;
                    //生产加工->新建检测单
                    case "20101": temptb.Header = (sender as Button).Content.ToString();
                        temptb.Content = new SysNewDetectSc();
                        //temptb.Content = new UserControl1();
                        flag = 1;
                        break;
                    //生产加工->检测单查询
                    case "20102": temptb.Header = (sender as Button).Content.ToString();
                        temptb.Content = new SysNewDetectScQuery(mainWindow.dbOperation);
                        flag = 1;
                        break;
                    //生产加工->检测数据统计->日报表
                    case "20201": temptb.Header = (sender as Button).Content.ToString() + "(生产加工)";
                        temptb.Content = new SysDayReport(mainWindow.dbOperation,"0"); 
                        flag = 1;
                        break;
                    //生产加工->检测数据统计->月报表
                    case "20202": temptb.Header = (sender as Button).Content.ToString() + "(生产加工)";
                        temptb.Content = new SysMonthReport(mainWindow.dbOperation,"0"); 
                        flag = 1;
                        break;
                    //生产加工->检测数据统计->自定义报表
                    case "20203": temptb.Header = (sender as Button).Content.ToString() + "(生产加工)";
                        temptb.Content = new SysYearReport(mainWindow.dbOperation,"0"); 
                        flag = 1;
                        break;
                    //生产加工->数据分析->对比分析
                    case "20301": temptb.Header = (sender as Button).Content.ToString() + "(生产加工)";
                        temptb.Content = new SysComparisonAndAnalysis(mainWindow.dbOperation,"0"); 
                        flag = 1;
                        break;
                    //生产加工->数据分析->趋势分析
                    case "20302": temptb.Header = (sender as Button).Content.ToString() + "(生产加工)";
                        temptb.Content = new SysTrendAnalysis(mainWindow.dbOperation, "0"); 
                        flag = 1;
                        break;
                    //生产加工->数据分析->区域分析
                    case "20303": temptb.Header = (sender as Button).Content.ToString() + "(生产加工)";
                        temptb.Content = new SysAreaAnalysis(mainWindow.dbOperation, "0"); 
                        flag = 1;
                        break;
                    //生产加工->数据分析->任务完成率分析
                    case "20304": temptb.Header = (sender as Button).Content.ToString() + "(生产加工)";
                        temptb.Content = new SysTaskReport(mainWindow.dbOperation, "0");
                        flag = 1;
                        break;
                    //生产加工->检测任务->设置任务量
                    case "20401": temptb.Header = (sender as Button).Content.ToString() + "(生产加工)";
                        temptb.Content = new UcSetTask(mainWindow.dbOperation, "0");
                        flag = 1;
                        break;
                    //风险预警->生产加工->实时风险
                    case "80101": temptb.Header = (sender as Button).Content.ToString() + "(生产加工)";
                        temptb.Content = new SysWarningInfo(mainWindow.dbOperation, "0");
                        flag = 1;
                        break;
                    //风险预警->生产加工->预警复核
                    case "80102": temptb.Header = (sender as Button).Content.ToString() + "(生产加工)";
                        temptb.Content = new SysReviewInfoSc(mainWindow.dbOperation);
                        flag = 1;
                        break;
                    //风险预警->生产加工->复核日志
                    case "80103": temptb.Header = (sender as Button).Content.ToString() + "(生产加工)";
                        temptb.Content = new SysReviewLog(mainWindow.dbOperation, "0");
                        flag = 1;
                        break;
                    //风险预警->预警数据统计
                    case "80501": temptb.Header = (sender as Button).Content.ToString();
                        temptb.Content = new SysWarningReport(mainWindow.dbOperation); 
                        flag = 1;
                        break;
                    //系统管理->系统管理->部门管理
                    case "90101": temptb.Header = (sender as Button).Content.ToString();
                        temptb.Content = new SysDeptManager(mainWindow.dbOperation);
                        flag = 1;
                        break;
                    //系统管理->系统管理->执法队伍
                    case "90102": temptb.Header = (sender as Button).Content.ToString();
                        temptb.Content = new UcUserManager(mainWindow.dbOperation);
                        flag = 1;
                        break;
                    //系统管理->系统管理->修改密码
                    case "90104": temptb.Header = (sender as Button).Content.ToString();
                        temptb.Content = new SysModifyPassword();
                        flag = 1;
                        break;
                    //系统管理->系统管理->自定义设置
                    case "90105": temptb.Header = (sender as Button).Content.ToString();
                        temptb.Content = new SysLoadPicture(mainWindow.dbOperation);
                        flag = 1;
                        break;
                    //系统管理->系统管理->系统日志
                    case "90106": temptb.Header = (sender as Button).Content.ToString();
                        temptb.Content = new SysLogManager();
                        flag = 1;
                        break;
                    //系统管理->系统管理->产地设置
                    case "90107": temptb.Header = (sender as Button).Content.ToString();
                        temptb.Content = new SysSetArea(mainWindow.dbOperation);
                        flag = 1;
                        break;
                    //系统管理->系统管理->角色管理
                    case "90108": temptb.Header = (sender as Button).Content.ToString();
                        temptb.Content = new SysRoleManager();
                        flag = 1;
                        break;
                    //系统管理->系统管理->权限管理
                    case "90109": temptb.Header = (sender as Button).Content.ToString();
                        temptb.Content = new SysRolePowerManager();
                        flag = 1;
                        break;
                    //帮助->帮助->帮助
                    case "A0101": temptb.Header = (sender as Button).Content.ToString();
                        temptb.Content = new UcUnrealizedModul();
                        flag = 1;
                        break;
                    //帮助->帮助->关于
                    case "A0102": temptb.Header = (sender as Button).Content.ToString();
                        temptb.Content = new SysHelp();
                        flag = 1;
                        break;
                    default: break;
                }
                if (flag == 1)
                {
                    tab.Items.Add(temptb);
                    tab.SelectedIndex = tab.Items.Count - 1;
                }

            }

            //mainWindow.IsEnbleMouseEnterLeave = false;
            //if (uc is IClickChildMenuInitUserControlUI)
            //{
            //    ((IClickChildMenuInitUserControlUI)uc).InitUserControlUI();
            //}
        }
    }
}

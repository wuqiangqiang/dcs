using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodSafetyMonitoring.Common
{
    public class ConvertStr
    {

        //转换年
        public static string convert_nian(string str_nian)
        {
            int i = 0;
            string strUpper = "";
            string strUpart = "";
            while (i < str_nian.Length)
            {
                switch (str_nian.Substring(i, 1))
                {
                    case "0": strUpart = "〇";
                        break;
                    case "1": strUpart = "一";
                        break;
                    case "2": strUpart = "二";
                        break;
                    case "3": strUpart = "三";
                        break;
                    case "4": strUpart = "四";
                        break;
                    case "5": strUpart = "五";
                        break;
                    case "6": strUpart = "六";
                        break;
                    case "7": strUpart = "七";
                        break;
                    case "8": strUpart = "八";
                        break;
                    case "9": strUpart = "九";
                        break;
                    default: break;
                }
                i = i + 1;
                strUpper = strUpper + strUpart;
            }
            return strUpper;
        }

        //转换月
        public static string convert_yue(string str_yue)
        {
            string strUpart = "";
            switch (str_yue)
            {
                case "1": strUpart = "一";
                    break;
                case "2": strUpart = "二";
                    break;
                case "3": strUpart = "三";
                    break;
                case "4": strUpart = "四";
                    break;
                case "5": strUpart = "五";
                    break;
                case "6": strUpart = "六";
                    break;
                case "7": strUpart = "七";
                    break;
                case "8": strUpart = "八";
                    break;
                case "9": strUpart = "九";
                    break;
                case "10": strUpart = "十";
                    break;
                case "11": strUpart = "十一";
                    break;
                case "12": strUpart = "十二";
                    break;
                default: break;
            }
            return strUpart;
        }

        //转换日
        public static string convert_day(string str_day)
        {
            int i = 1;
            string strUpper = "";
            string strUpart = "";
            while (i <= str_day.Length)
            {
                if (i == 1)
                {
                    switch (str_day.Substring(str_day.Length - i, 1))
                    {
                        case "0": strUpart = "〇";
                            break;
                        case "1": strUpart = "一";
                            break;
                        case "2": strUpart = "二";
                            break;
                        case "3": strUpart = "三";
                            break;
                        case "4": strUpart = "四";
                            break;
                        case "5": strUpart = "五";
                            break;
                        case "6": strUpart = "六";
                            break;
                        case "7": strUpart = "七";
                            break;
                        case "8": strUpart = "八";
                            break;
                        case "9": strUpart = "九";
                            break;
                        default: break;
                    }
                }
                else if (i == 2)
                {
                    switch (str_day.Substring(str_day.Length - i, 1))
                    {
                        case "1": strUpart = "一十";
                            break;
                        case "2": strUpart = "二十";
                            break;
                        case "3": strUpart = "三十";
                            break;
                        default: break;
                    }
                }
                i = i + 1;
                strUpper = strUpart + strUpper;
            }

            strUpper = strUpper.Replace("一十〇", "一十");
            strUpper = strUpper.Replace("二十〇", "二十");
            strUpper = strUpper.Replace("三十〇", "三十");
  
            return strUpper;
        }

        //转换头数（整数）
        public static string convert_object(string str_object)
        {
            int i = 1;
            string strUpper = "";
            string strUpart = "";

            while (i <= str_object.Length)
            {
                if (i == 1)
                {
                    switch (str_object.Substring(str_object.Length - i, 1))
                    {
                        case "0": strUpart = "零";
                            break;
                        case "1": strUpart = "壹";
                            break;
                        case "2": strUpart = "贰";
                            break;
                        case "3": strUpart = "叁";
                            break;
                        case "4": strUpart = "肆";
                            break;
                        case "5": strUpart = "伍";
                            break;
                        case "6": strUpart = "陆";
                            break;
                        case "7": strUpart = "柒";
                            break;
                        case "8": strUpart = "捌";
                            break;
                        case "9": strUpart = "玖";
                            break;
                        default: break;
                    }
                }
                else if (i == 2)
                {
                    switch (str_object.Substring(str_object.Length - i, 1))
                    {
                        case "0": strUpart = "零";
                            break;
                        case "1": strUpart = "壹拾";
                            break;
                        case "2": strUpart = "贰拾";
                            break;
                        case "3": strUpart = "叁拾";
                            break;
                        case "4": strUpart = "肆拾";
                            break;
                        case "5": strUpart = "伍拾";
                            break;
                        case "6": strUpart = "陆拾";
                            break;
                        case "7": strUpart = "柒拾";
                            break;
                        case "8": strUpart = "捌拾";
                            break;
                        case "9": strUpart = "玖拾";
                            break;
                        default: break;
                    }
                }
                else if (i == 3)
                {
                    switch (str_object.Substring(str_object.Length - i, 1))
                    {
                        case "0": strUpart = "零";
                            break;
                        case "1": strUpart = "壹佰";
                            break;
                        case "2": strUpart = "贰佰";
                            break;
                        case "3": strUpart = "叁佰";
                            break;
                        case "4": strUpart = "肆佰";
                            break;
                        case "5": strUpart = "伍佰";
                            break;
                        case "6": strUpart = "陆佰";
                            break;
                        case "7": strUpart = "柒佰";
                            break;
                        case "8": strUpart = "捌佰";
                            break;
                        case "9": strUpart = "玖佰";
                            break;
                        default: break;
                    }
                }
                else if (i == 4)
                {
                    switch (str_object.Substring(str_object.Length - i, 1))
                    {
                        case "0": strUpart = "零";
                            break;
                        case "1": strUpart = "壹仟";
                            break;
                        case "2": strUpart = "贰仟";
                            break;
                        case "3": strUpart = "叁仟";
                            break;
                        case "4": strUpart = "肆仟";
                            break;
                        case "5": strUpart = "伍仟";
                            break;
                        case "6": strUpart = "陆仟";
                            break;
                        case "7": strUpart = "柒仟";
                            break;
                        case "8": strUpart = "捌仟";
                            break;
                        case "9": strUpart = "玖仟";
                            break;
                        default: break;
                    }
                }
                else if (i == 5)
                {
                    switch (str_object.Substring(str_object.Length - i, 1))
                    {
                        case "0": strUpart = "零";
                            break;
                        case "1": strUpart = "壹万";
                            break;
                        case "2": strUpart = "贰万";
                            break;
                        case "3": strUpart = "叁万";
                            break;
                        case "4": strUpart = "肆万";
                            break;
                        case "5": strUpart = "伍万";
                            break;
                        case "6": strUpart = "陆万";
                            break;
                        case "7": strUpart = "柒万";
                            break;
                        case "8": strUpart = "捌万";
                            break;
                        case "9": strUpart = "玖万";
                            break;
                        default: break;
                    }
                }
                i = i + 1;
                strUpper = strUpart + strUpper;
            }

            strUpper = strUpper.Replace("拾零", "拾");
            strUpper = strUpper.Replace("佰零零", "佰");
            strUpper = strUpper.Replace("仟零零零", "仟");
            strUpper = strUpper.Replace("仟零零", "仟零");
            strUpper = strUpper.Replace("万零零零零", "万");
            strUpper = strUpper.Replace("万零零零", "万零");
            strUpper = strUpper.Replace("万零零", "万零");

            if (str_object.Length == 2)
            {
                strUpper = strUpper.Replace("壹拾", "拾");
            }
            return strUpper;
        }

        //转换头数（小数）
        public static string convert_object_dot(string str_object)
        {
            int i = 1;
            string strUpper = "";
            string strUpart = "";
            
            while (i <= str_object.Length)
            {
                if (i == 1)
                {
                    switch (str_object.Substring(str_object.Length - i, 1))
                    {
                        case "5": strUpart = "半";
                            break;
                        default: break;
                    }
                }
                else if (i == 2)
                {
                    switch (str_object.Substring(str_object.Length - i, 1))
                    {
                        case ".": strUpart = "点";
                            break;
                        default: break;
                    }
                }
                else if (i == 3)
                {
                    switch (str_object.Substring(str_object.Length - i, 1))
                    {
                        case "0": strUpart = "零";
                            break;
                        case "1": strUpart = "壹";
                            break;
                        case "2": strUpart = "贰";
                            break;
                        case "3": strUpart = "叁";
                            break;
                        case "4": strUpart = "肆";
                            break;
                        case "5": strUpart = "伍";
                            break;
                        case "6": strUpart = "陆";
                            break;
                        case "7": strUpart = "柒";
                            break;
                        case "8": strUpart = "捌";
                            break;
                        case "9": strUpart = "玖";
                            break;
                        default: break;
                    }
                }
                else if (i == 4)
                {
                    switch (str_object.Substring(str_object.Length - i, 1))
                    {
                        case "0": strUpart = "零";
                            break;
                        case "1": strUpart = "壹拾";
                            break;
                        case "2": strUpart = "贰拾";
                            break;
                        case "3": strUpart = "叁拾";
                            break;
                        case "4": strUpart = "肆拾";
                            break;
                        case "5": strUpart = "伍拾";
                            break;
                        case "6": strUpart = "陆拾";
                            break;
                        case "7": strUpart = "柒拾";
                            break;
                        case "8": strUpart = "捌拾";
                            break;
                        case "9": strUpart = "玖拾";
                            break;
                        default: break;
                    }
                }
                else if (i == 5)
                {
                    switch (str_object.Substring(str_object.Length - i, 1))
                    {
                        case "0": strUpart = "零";
                            break;
                        case "1": strUpart = "壹佰";
                            break;
                        case "2": strUpart = "贰佰";
                            break;
                        case "3": strUpart = "叁佰";
                            break;
                        case "4": strUpart = "肆佰";
                            break;
                        case "5": strUpart = "伍佰";
                            break;
                        case "6": strUpart = "陆佰";
                            break;
                        case "7": strUpart = "柒佰";
                            break;
                        case "8": strUpart = "捌佰";
                            break;
                        case "9": strUpart = "玖佰";
                            break;
                        default: break;
                    }
                }
                i = i + 1;
                strUpper = strUpart + strUpper;
            }

            strUpper = strUpper.Replace("拾零", "拾");
            strUpper = strUpper.Replace("佰零零", "佰");

            if (str_object.Length == 4)
            {
                strUpper = strUpper.Replace("壹拾", "拾");
            }

            return strUpper;
        }
    }
}

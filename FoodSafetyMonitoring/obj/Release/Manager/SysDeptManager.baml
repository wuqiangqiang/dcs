   M S B A M L   `   `   `  ���� G  CZRDDcsSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulle7clr-namespace:FoodSafetyMonitoring.Manager.UserControls)FoodSafetyMonitoring.Manager.UserControls  1    +FoodSafetyMonitoring.Manager.SysDeptManager   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      8x,http://schemas.microsoft.com/winfx/2006/xaml    Nusercontrols7clr-namespace:FoodSafetyMonitoring.Manager.UserControls    Z VWPFToolkit.Extended, Version=1.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4Z	toolkitexJhttp://schemas.microsoft.com/winfx/2006/xaml/presentation/toolkit/extended    � 	Resources   �� MergedDictionaries �� Source��  �� $" Themes/ImageButtonStyle.xaml?��� $ Themes/TextBox.xaml?��� $ Themes/PasswordBox.xaml?��� $ Themes/Button_new.xaml?��� $ Themes/StackPanel.xaml?��� $ Themes/ComboBox.xaml?��� $ Themes/TextBlock.xaml?��� $ Themes/CheckBox.xaml?��� $ Themes/TextBox.xaml?�%    .���  � ColumnDefinitions �� $	��280 ��� .+�� $��0��$��5,5,5,5q� � RowDefinitions �� $��37 ��� .+�� $��0������  �� ImageSource$ /res/title.png��.+��� $��20,0,0,0q�$��Center=�部门列表�� $��1�����#fbfbfc$��1,0,1,1q����#e0e0e0 �� CornerRadius$ 0,0,3,3��._�� $��0,0,0,5q�.+�R� -    R�Name$ 	_treeView��$��1��
������ $��0q�$��0,8,0,0q����� *FirstGeneration+����  �� 
TargetType" Q�.��� 	 �� Property	 �@Q�
IsExpanded
 �� Value
 �� *
IsExpanded+ �� Mode$ TwoWay=��� 	 �@Q�
IsSelected
 �� *
IsSelected+$ TwoWay=��� 	 � ��$
 Normal"� �� Triggers P�  P� Property �@Q�
IsSelected P� Value . . ��� 	 � ��$

 Bold"�����  �� ItemsSource �� *
Children+.R��� $��30�� �� Orientation$ 
Horizontal=�.+��� -   $��Center=����� *Name+
 � Tag �� *Own+� $��1��$��5,5,5,5q� �� $��37 ��� .+�� $��0������ $ /res/title.png��.+��� .+��� $��20,0,0,0q�$��Center=�详细信息�� $��1��$��1,0,1,1q����#e0e0e0$ 0,0,3,3�����#fbfbfc._�� -    �Name$ _detail_info_all��$��30,5,0,0q�$
��Left=�$y�Hidden=� �� $��40 ��� $
��auto ��� $��40 �.+�� $��0��.+��� $��Center=�$��Center=�.+��� -    ��Name$ _import��$��导入��$��25��$��80��$y�Hidden=��� -   $
 _add��$��添加下级��$��15,0,0,0q�$��25��$��80��$y�Hidden=��� -   $ _delete��$��删除��$��15,0,0,0q�$��25��$��60��$y�Hidden=��� -   $ _edit��$��修改��$��15,0,0,0q�$��25��$��60��$y�Hidden=�� -   $ _detail_info��$��1��$��Center=�{�.   �� $��37 ��� $��37 ��� $��37 ��� $��37 ��� $��37 ��� $��37 ��� $��37 ��� $��37 ��� $��37 ��� $��37 �.+�� $��0��.+��� $��Center=�.+��� $��80��上级部门:�� -	    ��Name$ _superior_department��$	��200��$��6,0,0,0q�� $��1��.+��� $��Center=�.+��� $��80��
所在地:�� -
   $ 
_belong_to��$��0��$��6,0,0,0q��� -    ��Name$ _lower_provice��$y�Hidden=�$��80��$��0q��� -   $ _lower_city��$y�Hidden=�$��80��$��10,0,0,0q��� -   $ _lower_country��$y�Hidden=�$��80��$��10,0,0,0q��� -   $ 
_area_flag��$��20��
���  ��$��0q�� $��2��.+��� $��Center=�.+��� $��80��部门级别:�� -   $ _regional_level��$��6,0,0,0q�$��0���� -   $ _level��$��0q�$��80���� -   $ _level_flag��$��20��
���  ��$��0q�� $��3��.+��� $��Center=�.+��� -   $ _station_name��$��80��检测点名称:�� -    ��Name$ _station��$	��320�� �� 	MaxLength$ 20���� -   $ _station_flag��
���  ��$��0q�� $��4��.+��� -    ��Name$ _station_property��$y�Hidden=�$��Center=�.+��� $��80��检测点性质:�� -    ��Name$ _detect_type1��.��生产加工�� -   $ _detect_type2��.��商贸流通�� -   $ _detect_type3��.��餐饮酒店�� -   $ _detect_type4��.��学校食堂�� -   $ _station_property_flag��
���  ��$��0q�� $��5��.+��� $��Center=�.+��� $��80��负责人姓名:�� -   $ _principal_name��$ 20��$	��320��� $��6��.+��� $��Center=�.+��� $��80��	手机:�� -   $ _phone��$	��320��$ 11�� �� IsInputMethodEnabled .  � $��7��.+��� $��Center=�.+��� $��80��联系电话:�� -   $ _contact_number��$	��320��$ 13�� .  � $��8��.+��� $��Center=�.+��� $��80��$��Right=�	地址:�� -   $ _address��$	��320��$ 50��� $��9��.+��� $��Center=�.+��� -   $ _Supplier_name��$��80��$y�Hidden=�
供应商:�� -    $ 	_Supplier��$y�Hidden=�$��80��$��6,0,0,0q�� $��2��.+��� $��Center=�.+��� -!   $��btnSave��$	��120��$��32��   SaveImageButtonTemplate#��[  $��	80,0,20,0q��� -"   $��	btnCancel��$	��120��$��32��  CancelImageButtonTemplate#��[ $��20,0,0,0q�
   M S B A M L   `   `   `  ���� G  CZRDDcsSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulle7clr-namespace:FoodSafetyMonitoring.Manager.UserControls)FoodSafetyMonitoring.Manager.UserControls  0    *FoodSafetyMonitoring.Manager.UcUserManager   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      8x,http://schemas.microsoft.com/winfx/2006/xaml    Nusercontrols7clr-namespace:FoodSafetyMonitoring.Manager.UserControls    Z VWPFToolkit.Extended, Version=1.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4Z	toolkitexJhttp://schemas.microsoft.com/winfx/2006/xaml/presentation/toolkit/extended    � 	Resources   �� MergedDictionaries �� Source��  �� $" Themes/ImageButtonStyle.xaml?��� $ Themes/StackPanel.xaml?��� $ Themes/TextBox.xaml?��� $ Themes/ComboBox.xaml?��� $ Themes/TextBlock.xaml?��� $ Themes/TextBox.xaml?��� $ Themes/Password_new.xaml?�%    .���  � ColumnDefinitions �� $	��280 ��� $	��340 ��� .+�� $��0��$��5,5,5,5q� � RowDefinitions �� $��37 ��� .+�� $��0������  �� ImageSource$ /res/title.png��.+��� $��20,0,0,0q�$��Center=�部门列表�� $��1�����#fbfbfc$��1,0,1,1q����#e0e0e0 �� CornerRadius$ 0,0,3,3��._�� $��0,0,0,5q�.+�R� -    R�Name$ 	_treeView��$��1��
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
 � Tag �� *Own+� $��1��$��5,5,5,5q� �� $��37 ��� .+�� $��0������ $ /res/title.png��.+��� $��20,0,0,0q�$��Center=�用户列表�� $��1�����#fbfbfc$��1,0,1,1q����#e0e0e0$ 0,0,3,3��._�� $��0,0,0,10q� �� �� $��37 �.+��� -   $��lvlist��$��0��$��0q�$��2������ ���� " ��.��� 	 � ��$
 30���� 	 � ��$
 0q����� .R��� $
��Auto=�$
��Auto=�.����  �� View �� ! �� ColumnHeaderContainerStyle �� .��� 	 � ��$
 0���� 	 � y�$
 Hidden=�.P���  �� CellTemplate �� .R���  ��Name$ 	imageLogo��$��20��$��Images/role.png����  �� Width$	 100�� �� DisplayMemberBinding �� *	INFO_USER+�� $	 120�� �� *	role_expl+��  �� .R��� $��	btnModify��-   $��20��$��20�� !  ListModifyImageButtonTemplate#��[   �� *	RECO_PKID+��  �� .R��� $��	btnDelete��-   $��20��$��20�� ! ListDeleteImageButtonTemplate#��[  �� *	RECO_PKID+� $��1��$��2��.+��� -   $��	btnCreate��$	��120��$��32��  CreateImageButtonTemplate#��[ $��Center=�$��Center=�$y�Hidden=�� $��2��$��5,5,5,5q� �� $��37 ��� .+�� $��0������ $ /res/title.png��.+��� .+��� $��20,0,0,0q�$��Center=�账户信息�� $��1��$��1,0,1,1q����#e0e0e0$ 0,0,3,3�����#fbfbfc._�� -    �Name$ user_details��$��	20,20,0,0q�$
��Left=�$y�Hidden=� �� $
��auto ��� $��80 �.+�� $��0��$��Center=� �� $��37 ��� $��37 ��� $��67 ��� $��37 ��� $��37 ��� $��37 ��� $��37 �.+�� $��0��.+��� $��Center=�.+��� $	��100��帐号使用单位:�� -   $��_department��$	��300��
������� �� MaxDropDownHeight$	 200��{�.  � $��1��.+��� $��Center=�.+��� $	��100��帐号权限:�� -	   $��_cmbRoleType��$	 200��$	��300��{�.  �� -
    ��Name$ 
_role_flag��
���  ��$��0q�� $��2��.+��� $��Center=�.+��� 2  ,System.Windows.Markup.XmlAttributeProperties  XmlSpace preserve$	��100��权限描述:�� -   $��_subDetails�� �� TextWrapping$
 Wrap=�$
��Left=�$	��300��$��60��$��0q�{�.  � $��3��.+��� $��Center=�.+��� $	��100��登录帐号:�� -   $��
_loginName��$��Center=�$��0q�$	��300�� �� 	MaxLength$ 10�� �� IsInputMethodEnabled .  �� -   $ 
_user_flag��
���  ��$��0q�� $��4��.+��� $��Center=�.+��� $	��100��帐号密码:G� -   $��_loginPassword��$��Center=�$��0q�$	��300��$��26���� -   $ _password_flag��
���  ��$��0q�� $��5��.+��� $��Center=�.+��� $	��100��帐号使用人姓名:�� -   $��txtUserName��$��Center=�$	��300��$��0q�$ 10���� -   $ 
_name_flag��
���  ��$��0q�� $��6��.+��� $��Center=�.+��� $	��100��帐号管理:�� .+��� -    ��Name$ _user_manger��$��20,0,0,0q�.��启用�� -   $ _user_manger_2��$��20,0,0,0q�.��禁用�� -   $ _manager_flag��
���  ��$��0q�� $��2��.+��� $��Center=�.+��� -   $��btnSave��$	��120��$��32��  SaveImageButtonTemplate#��[ $��
100,0,20,0q�$y�Hidden=��� -   $��	btnCancel��$	��120��$��32��  CancelImageButtonTemplate#��[ $��	20,0,20,0q�$y�Hidden=�
   M S B A M L   `   `   `  ���� G  CZRDDcsSystem, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulle7clr-namespace:FoodSafetyMonitoring.Manager.UserControls)FoodSafetyMonitoring.Manager.UserControls  1    +FoodSafetyMonitoring.Manager.SysUserManager   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      8x,http://schemas.microsoft.com/winfx/2006/xaml    Nusercontrols7clr-namespace:FoodSafetyMonitoring.Manager.UserControls    Z VWPFToolkit.Extended, Version=1.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4Z	toolkitexJhttp://schemas.microsoft.com/winfx/2006/xaml/presentation/toolkit/extended    � 	Resources   �� MergedDictionaries �� Source��  �� $" Themes/ImageButtonStyle.xaml?��� $ Themes/TextBox.xaml?��� $ Themes/PasswordBox.xaml?��� $ Themes/ComboBox.xaml?��� $ Themes/TextBlock.xaml?��� $ Themes/StackPanel.xaml?��� $ Themes/Image.xaml?�%    .���  � ColumnDefinitions �� $��* ��� $
��auto �$��6���� $
��1.6* � � RowDefinitions �� $��30 ��� $	��11* ��� $��68 �.+�� 
�������.+��� $��黑体)�$��用户列表��$	��300"�$��10,0,0,0q��� -   $��lvlist��$��0q�$��1��$��10,10,10,10q����� ����  �� 
TargetType" ��.���  �� Property � �� �� Value$ 30����  � ��$ 0q����� .R��� $
��Auto=�$
��Auto=�.����  �� View �� !	 �� ColumnHeaderContainerStyle	 �� .���  � ��$ 0����  � y�$ Hidden=�.P��� 
 �� CellTemplate
 �� .R���  ��Name$ 	imageLogo��$��20��$��Images/role.png����  �� Width$	 120�� �� DisplayMemberBinding �� *	INFO_USER+�� $	 180�� �� *	INFO_NAME+�� $	 120�� �� *	role_expl+�� 
 �� .R��� $��	btnModify��-   $��20��$��20�� !  ListModifyImageButtonTemplate#��[  
 � Tag �� *	RECO_PKID+�� 
 �� .R��� $��	btnDelete��-   $��20��$��20�� ! ListDeleteImageButtonTemplate#��[  �� *	RECO_PKID+�� -   $��	btnCreate��$��95��$��30��$��2��  CreateImageButtonTemplate#��[ $	��Top=�$��Center=��� $��1��$��Stretch=�$��Center=�$��6��$��3��� $��2��
�������.+��� -    ��Name$ _title��$��帐号信息��$	��300"�$��黑体)�� -    �Name$ user_details��$��1��$��2��$��Center=�$��	0,30,0,30q�$y�Hidden=�����  �� ImageSource$&  /Manager/Images/content_back.png�� j� Stretch$
 Fill=�.+�� $��40,20,60,20q� �� $��* ��� $��2* ��� $��* ��� $��* ��� $��4* � �� �� �� $��40 ��� $	��100 ��� �� $��40 �.+��� $��0��$��0��$��Center=�帐号使用单位:�� -   $��_department��$��0��$��1��
������� �� MaxDropDownHeight$	 200��$��1,1,1,1q��� -   $ 
_dept_flag��$��0��$��2��$��20��
���  ��$��0q��� $��0��$��3��$��Center=�帐号权限:�� -	   $��_cmbRoleType��$��0��$��4��
�������$	 200��$��1,1,1,1q�{�.  �� 2  ,System.Windows.Markup.XmlAttributeProperties  XmlSpace preserve$��1��$��0��权限描述:�� -
   $��_subDetails��$��1��$��1��$��5�� �� TextWrapping$
 Wrap=�$
��Left=�$	��500��$��60��{�.  �� $��2��$��0��登录帐号:�� -   $��
_loginName��$��2��$��1��$��Center=�$	��180�� �� 	MaxLength$ 10�� �� IsInputMethodEnabled .  �� -   $ 
_user_flag��$��2��$��2��$��20��
���  ��$��0q��� $��2��$��3��帐号密码:G� -   $��_loginPassword��$��2��$��4��$��Center=�$	��180��$��20��$��1,1,1,1q��� -   $ _password_flag��$��2��$��5��$��20��
���  ��$��0q��� $��3��$��0��帐号使用人姓名:�� -   $��txtUserName��$��3��$��1��$��Center=�$	��180��$ 10���� -   $ 
_name_flag��$��3��$��2��$��20��
���  ��$��0q��� $��3��$��3��帐号管理:�� $��3��$��4��$��Center=�.+��� -    ��Name$ _user_manger��$��20,0,0,0q�.��启用�� -   $ _user_manger_2��$��20,0,0,0q�.��禁用�� -   $ _manager_flag��$��3��$��5��$��20��
���  ��$��0q��� -   $��txtMsg��$��4��$��4��
���  ��$��14(�$��宋体)��� $��2��$��2�� �� Orientation$ 
Horizontal=�$��Center=�$	��Top=�.+��� -   $��btnSave��$��76��$��30��$��2��  SaveImageButtonTemplate#��[ $��0,0,20,0q�$y�Hidden=��� -   $��	btnCancel��$��76��$��30��$��3��  CancelImageButtonTemplate#��[ $��	20,0,20,0q�$y�Hidden=�
   M S B A M L   `   `   `  ����M  I检测监管系统, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullK*clr-namespace:FoodSafetyMonitoring.ManagerFoodSafetyMonitoring.Manager  5      6    0FoodSafetyMonitoring.Manager.SysRolePowerManager   5      \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      8src*clr-namespace:FoodSafetyMonitoring.Manager    5        � 	Resources  5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $" Themes/ImageButtonStyle.xaml?�6%   �� 5	      $+ %Themes/GridViewColumnHeaderStyle.xaml?�6%   �� 5
      $ Themes/TextBlock.xaml?�6%   %    5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��auto �6   $��6��6,   6;   �� 5      $
��1.2* �6    � RowDefinitions 5   
   �� 5      $
��0.6* �6   �� 5      $	��12* �6   �� 5      $��68 �6   .+�5   
   � 
�������.+�5      �� $	��600"�6   $��宋体)�6,   $��角色列表��6<   $��10,0,0,0q�6H   5      �� 5   
   $��1��6   $��Stretch=�6'   $��Center=�6C   $��6��6`   $��3��6j   6}   � 5   
   $��2��6   
�������.+�5      �� $��宋体)�6   $��权限列表��6+   $	��600"�67   5      �� 5    
   -   5        $��lvlist��5    !   $��1��6   $��0q�6G   $��10,10,10,10q�6[   
������ $��宋体)�6�   $��14(�6�   ��6/   �� ��5!      �� 5"       �� 
TargetType" ��6   .�5#      ��  �� Property � �� �� Value$ 30��6/   �� 5$       � ��$ 0q�6/   �� 5%       � ��$ 14(�61   �� 5&       � ��$ 宋体)�63   �� 5'       � ��
 �   �63   �� 5*       � ��$ Center=�6:   5+       �� View 5-      �� 5.      .P�5/      �� 	 �� Width$	 40��6%      myHeaderStyle
 �� HeaderContainerStyle#
 [  60    �� CellTemplate 50      �� 51      .R�52   "   ��  ��Name$ 	imageLogo��6(   $��Images/role.png��6;   6W   53      55      �� 56      $		 230��6%   $��	角色名��61   #
 [  6?    �� DisplayMemberBinding 6u   �� *	INFO_NAME+6�   �� 57      $		 180��6%   $��角色说明��61   #
 [  6?    6u   �� *	INFO_EXPL+6�   �� 5:      $	 30��6%   #
 [  60    5;      �� 5<      .R�5=   "   �� $��	btnModify��6)   -   5        $��20��5=   :   $��20��6E    ! ListModifyImageButtonTemplate#��[ 6R   
 � Tag 6�   �� *	NUMB_ROLE+6�   5>      5@      5A      5C      R� 5D   
   -   5         R�Name$ tvPermissions��5D   0   $��1��6   $��2��6    $��0q�6_   $��10,10,10,10q�6s   
������ $��宋体)�6�   $��14(�6�   ��6G   ��   5E       �� DataType+   %FoodSafetyMonitoring.Manager.TreeItem �� ItemsSource �� Path �� Orientation d� 	IsChecked �� Mode � ToolTip��%�  (��       5F   +   *, +)�� 6   "  6+    6M   �� $ children�.R�5G      �� $ 
Horizontal=�6!   $��0,2,0,2q�6:   
������ .+�5H      �� |�.  $��Center=�6a    65   �� *	IsChecked+$ TwoWay=�6a   �� 5I      $��Center=�6    $��Images/power.png��6;   $��30��6U   $��30��6a   6n   �� 5J      $��Center=�6$   $��宋体)�6n   $��14(�6~   ��6?   �� *text+ 6U   �� *text+6~   5K      5L      5M      5N      �� 5O   
   $��2��6   $��2��6"   $ 
Horizontal=�62   $��Center=�6K   $	��Top=�6h   .+�5P      �� -   5        $��btnSave��5P      $��84��6%   $��38��60     SaveImageButtonTemplate#��[ 6=   $��0,0,20,0q�6�   �� 5Q      -   5        $��	btnCancel��5Q      $��84��6'   $��38��62     CancelImageButtonTemplate#��[ 6?   $��20,0,0,0q�6�   5R      5S      5T      
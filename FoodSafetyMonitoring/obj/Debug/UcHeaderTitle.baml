   M S B A M L   `   `   `  ����M  I检测监管系统, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:FoodSafetyMonitoringFoodSafetyMonitoring  5      (    "FoodSafetyMonitoring.UcHeaderTitle   5      \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      2local"clr-namespace:FoodSafetyMonitoring    5        � 	Resources  5         img �� 
TargetType �� Property �� Value �� Triggers P� Property P� Value �� ScaleX �� ScaleY��%�  &	        �� 5   
   " ��6   .�5      ��  � ��$ Center=�64   6E   �� 5       �@J�RenderTransformOrigin$ 0.5,0.5&�66   6H   �� 5	       �@J�Opacity$	 0.7Q�6(   66    5
      P� 5       �@J�IsMouseOver . . �5      ��  � z� 5      �� 5      $	 1.2Q�6-   $	 1.2Q�6:   5      �� 5       �@J�Opacity$ 1Q�60   5      5      5      5      .��5      � 	 � RowDefinitions	 5   
   �� 5      �� 5      $��5 �6   
 � ColumnDefinitions
 5   
   �� 5       $
��auto �6   �� 5!      �� 5"      $
��auto �6   .+�5$   
   ��  �� Orientation$ 
Horizontal=�6   $
��Left=�6.   $��5,0,5,0q�6I   .+�5&      �� $��15(�6   $��Center=�6'   
�������$��Center=�6V   $��	10,0,20,0q�6s   登录用户：6�   �� 5'      -   5         ��Name$ _user��5'      $��15(�6'   $��Center=�65   
�������$��Center=�6d   $��0,0,20,0q�6�   	管理员6�   �� 5(      $��15(�6   $��Center=�6'   
�������$��Center=�6V   $��	10,0,20,0q�6s   	时间：6�   �� 5)      -   5        $ _date��5)      $��15(�6'   $��Center=�65   
�������$��Center=�6d   $��0,0,20,0q�6�   6�   5;      n� 5=   
   -   5        $��1��5=      
������  J� Opacity$ 0Q�6U   6c   �� 5>   
   $ 
Horizontal=�6   $��2��6/   $��Right=�6?   $��5,0,5,0q�6[   .+�5A      �� -   5         ��Name$	 min��5A   V   $��25��6   #��[  6    $��/res/close.png��6>    � ToolTip$ 缩小��6}   �� 5B      -   5        $	 max��5B   V   $��25��6   #��[  6    $��/res/close.png��6>   $ 	最大化��6}   �� 5C      -   5        $
 exit��5C   V   $��25��6   #��[  6    $��/res/close.png��6>   $ 关闭��6   5D      5E      5F      
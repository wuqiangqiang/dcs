   M S B A M L   `   `   `  ����  检测监管系统-    'FoodSafetyMonitoring.UserControlTemplet   5      \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      
�������  � 	Resources  5         img �� 
TargetType �� Property �� Value �� Triggers P� Property P� Value �� ScaleX �� ScaleY��%�  &	        �� 5   
   " ��6   .�5      ��  � ��$ Center=�64   6E   �� 5       �@J�RenderTransformOrigin$ 0.5,0.5&�66   6H   �� 5       �@J�Opacity$	 0.7Q�6(   66    5	      P� 5
       �@J�IsMouseOver . . �5      ��  � z� 5      �� 5      $	 1.2Q�6-   $	 1.2Q�6:   5      �� 5       �@J�Opacity$ 1Q�60   5      5      5      5      .��5      � 	 � RowDefinitions	 5   
   �� 5      $��35 �6   �� 5      .+�5   
   � $��0��6   ��5      �� 5      
 �� ImageSource$
 res/条件栏背head.png��6    j� Stretch$
 Fill=�6<   6M    � ColumnDefinitions 5      �� 5       �� 5!      .+�5#      �� -   5         ��Name$ _title��5#      $��18(�6(   $
��Left=�66   $��5q�6Q   
�������$��Center=�6p   $��楷体)�6�   $	��600"�6�   $��系统日志管理��6�   �� 5$      -   5         ��Name$
 exit��5$   �   $��1��6   $��Right=�6$   $��0,0,10,0q�6@   $��25��6R   #��[  6]   $��/res/close.png��6{    � ToolTip$ 关闭��6�   5%      � 5&   
   -   5         �Name$
 grid��5&      $��1��6   ��5'      �� 5(      $
 res/信息栏背body.png��6   $
 Fill=�6<   6M   5*      5+      5.      
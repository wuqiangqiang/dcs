   M S B A M L   `   `   `  ����   ZRDDcsSystem,    &FoodSafetyMonitoring.CMessageBoxWindow   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      8x,http://schemas.microsoft.com/winfx/2006/xaml    -     �Name$  window��$	��130��$	��300�� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � 
ResizeMode$ NoResize=�
������  � AllowsTransparency .  � 	Resources  �� 
TargetType   CloseButtonStyle �� Property �� Value	 �� 
TargetType
 ��Name �� Triggers P� Property P� Value �� 
TargetName  NormalButtonStyle  NotNormalButtonStyle��%�  &	        &	 {    &	     �� " ��.���  � ��
 �������  � ��$ 0q���  � ��
 ���� ��  � ��
 ���� ��  � �� �� "	 ��.R��� $
 border��#��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ���  � ��
 ������� $ border�� � ��
 �  ��P�  � �� . . ���  � ��
 ������� $ border�� � ��
 �  ���� " ��.���  � ��
 �������  � ��$ 0q���  � ��
 ���� ��  � �� �#1ba794��  � �� �� "	 ��.R��� $
 border��#��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ���  � ��
 ������� $ border�� � �� �#0d917fP�  � �� . . ���  � ��
 ������� $ border�� � �� �#0d917f�� " ��.���  � ��
 �������  � ��$ 0q���  � ��
 ���� ��  �@J�Opacity$	 0.8Q���  � �� �	#FF40A6A6��  � �� �� "	 ��.R��� $
 border��#��z��._��� $��Center=�$��Center=� P�  �@J�IsMouseOver . . ���  � ��
 ������� $ border�� � �� �	#FF4C6C99P�  � �� . . ���  � ��
 ������� $ border�� � �� �	#FF2E5387.����  �� CornerRadius$ 3��._�� -    �Name$
 grid�� � RowDefinitions �� $��35 ��� .+��� $��0,0,0,1q����#CCCCCC._�� $��0�����#c3c4bf � ColumnDefinitions �� $��10 ��� �� $��30 �.+��� $��1��$��Center=�
�������$��12(�$��微软雅黑)����� *MessageBoxTitle+�� -   $��2��#��[  $��23��$��23��$y�Hidden=�.���� .+��� 	 �� X1$ -6��	 �� Y1$ -6��	 �� X2$ 6��	 �� Y2$ 6��
�������
�������$��2���� $ -6��$ 6��$ 6��$ -6��
�������
�������$��2��n� -   $��35��
������  J� Opacity$ 0Q�$��0��$��2���� $��1��$��0q����#c3c4bf._�� 
������� �� �� $��50 �.+��� $��0��$��Center=� �� Orientation$ 
Horizontal=�.+��� ���� *	ImagePath+�� $��Center=�$
��Left=�$��WrapWithOverflow=�
���   �$��12(�$��微软雅黑)�$��20,0,0,0q����� *MessageBoxText+�� $��1��$ 
Horizontal=�$��Center=�.+��� -    ��Name$ OKButton��$��确定��$��50��$��25��$��	20,0,20,0q�#��[ y��� *OKButtonVisibility+ �� Mode$ OneWay=��� -   $ 	YesButton��$	��是��$��50��$��25��$��	20,0,20,0q�#��[ y��� *YesButtonVisibility+$ OneWay=��� -   $ NoButton��$	��否��$��50��$��25��$��	20,0,20,0q�#��[ y��� *NoButtonVisibility+$ OneWay=��� -   $ CancelButton��$��取消��$��50��$��25��$��	20,0,20,0q�#��[ y��� *CancelButtonVisibility+$ OneWay=�
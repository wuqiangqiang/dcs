   M S B A M L   `   `   `  ����  检测监管系统&     FoodSafetyMonitoring.LoginWindow   5   	   \ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5      8x,http://schemas.microsoft.com/winfx/2006/xaml    5      -   5        $	��372��5      $	��450��6     � WindowStyle$
  None=�6    � AllowsTransparency . 
������ 
������  � 
ResizeMode$ NoResize=�6~    � Topmost .  � WindowStartupLocation$ CenterScreen=�6�    � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $* $Manager/Themes/ImageButtonStyle.xaml?�6%   �� 5	      $( "Manager/Themes/combobox_login.xaml?�6%   �� 5
      $$ Manager/Themes/ImageLabel.xaml?�6%   �� 5      $  Manager/Themes/Shared.xaml?�6%   �� 5      $" Manager/Themes/CheckBox.xaml?�6%   %    5      .��5      �� $	��450��6   $	��372��6   ��5   
   �� 5       �� ImageSource$ /res/login.png��6   69   .+�5   
   n� -   5        $	��432��5      $	��120��6   
������ 	 J� Opacity$	 0Q�6B   

 �� Top$
 11��6j    �� Left$ 10��6z   6�   �� 5   
   -   5         ��Name$	 min��5   @   $��15��6   $��15��6   $��/res/small.png��6(    � ToolTip$ 	最小化��6�   $	��Top=�6�   $	 404��6�   $
 13��6�   �� 5   
   -   5        $
 exit��5   ?   $��15��6   $��15��6   $��/res/shut.png��6(   $ 关闭��6�   $��Right=�6�   $	��Top=�6�   $	 424��6�   $
 13��6�   �� 5   
   -   5        $ 	_img_user��5      $��/res/username.png��6#   $	 106��6>   $	
 189��6P   $	��245��6a   $��41��6m   6{   �� 5   
   -   5        $ _img_password��5      $��/res/password.png��6'   $	 106��6B   $	
 238��6T   $	��245��6e   $��40��6q   6   �� 5   
   -   5        $��cmbName��5      $��32��6#   $	��198��6/   $	 148��6;   $	
 194��6M   $��0q�6^   $��Center=�6r   $
��Left=�6�   
������  �� 
IsEditable .  �� SelectedIndex$ 0��6r  $��0q�6�  G� 5    
   -   5         G�Name$ 	_password��5       $	��198��6)   $��32��65   $��Center=�6A   $
��Left=�6c   $	 148��6~   $	
 242��6�      DisabledForegroundBrush#��[  6�   
������ $��0q�6�   6$  �� 5!   
   -	   5        $ _login��5!      $��/res/loginButton.png��6    $��38��6>   $	��243��6J   $	 106��6V   $	
 307��6h    � Cursor$
 Hand��6�   $
��Fill=�6�   �� 5$   
   -
   5        $��txtMsg��5$   C   $	 186��6   $	
 280��6&   $��21��67   $	��219��6Q   
���  ��$��12(�6o   $��Center=�6}   $��Center=�6�   �� 5&   
   -   5        $��_rememberPassword��5&      $��16��6,   $	 106��68   $	
 282��6J   $��80��6[   $��0q�6f   .��6r   	记住我6w   5'      5(      
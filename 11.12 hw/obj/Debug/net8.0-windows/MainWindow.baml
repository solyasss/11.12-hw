   M S B A M L   `   `   `  ����C  ?11.12 hw, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:hwhw  5   	       hw.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   "localclr-namespace:hw     5   	     � Title$  Notes��5   	   $	��300��6   $	��500��6$   $	��500��60   $	��300��6?   
�������.��5      ]� .+�5   
   a� $	��Top=�6   .+�5      c� .@�5	      �� -   5        $	��Add��5	      6'   �� 5
      -   5        $��Delete��5
      6*   �� 5      -   5        $��Modify��5      6*   �� 5      �� 5      -   5        $
��Save��5      6(   �� 5      -   5        $
��Load��5      6(   5      5      � 5   
   $��5q�6    � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5       � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      .+�5      �� $��Name:��6   $��Center=�6%   $��0,0,5,0q�6@   �� 5      $��1��6   $��0,0,0,5q�6&   ��5      �� *new_person.name+ �� UpdateSourceTrigger$ PropertyChanged=��� 5!      $��Address:��6   $��Center=�6(   $��1��6C   $��0,0,5,0q�6P   �� 5"      $��1��6   $��1��6&   $��0,0,0,5q�63   ��5#      �� *new_person.address+$ PropertyChanged=��� 5%      $��Phone:��6   $��Center=�6&   $��2��6A   $��0,0,5,0q�6N   �� 5&      $��1��6   $��2��6&   $��0,0,0,5q�63   ��5'      �� *new_person.phone+$ PropertyChanged=��� 5)      $��3��6   $��2��6#   $��0,5,0,0q�67    �� DisplayMemberPath$
 name��5,      ��5*      �� *people+ �� SelectedItem 5+      �� *selected_person+5,      5-      5.      5/      
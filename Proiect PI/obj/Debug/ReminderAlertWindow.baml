   M S B A M L   `   `   `  ����E  AProiect PI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Proiect_PI
Proiect_PI  5   	   $    Proiect_PI.ReminderAlertWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Proiect_PI     5   	     � Title$  ReminderAlertWindow��5   	   $	��300��6%   $	��300��62    � DataContext 5	   	   ��  �� RelativeSource �� *Self+.��5
      � $��0,0,0,-1q�6   .+�5   
   �� -   5         ��Name$ 	textBlock��5      $
��Left=�6'   $��	36,30,0,0q�6B   $
��Wrap=�6U   $	��Top=�6�   $	��216��6�   $	��141��6�    �� TextAlignment$ Center=�6�   $��18(�6�   ��6i   ��  �� Path$ ReminderText�6�   �� 5   
   -   5         ��Name$ 
editButton��5      $
��Edit��6%   $
��Left=�64   $��207,197,0,0q�6O   $	��Top=�6d   $��65��6|   6�   �� 5   
   -   5        $ snoozeButton��5      $��Snooze��6'   $
��Left=�68   $��
19,197,0,0q�6S   $	��Top=�6g   $��68��6   6�   �� 5   
   -   5        $ 
doneButton��5      $
��Done��6%   $
��Left=�64   $��110,189,0,0q�6O   $	��Top=�6d   $��75��6|   $��38��6�   6�   5      5      
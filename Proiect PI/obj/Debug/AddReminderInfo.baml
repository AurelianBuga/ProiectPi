   M S B A M L   `   `   `  ����E  AProiect PI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Proiect_PI
Proiect_PI  5   	        Proiect_PI.AddReminderInfo   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Proiect_PI     5   	     � Title$  AddReminderInfo��5   	   $��302.746��6!   $��389.619��62    � DataContext 5	   	   ��  �� RelativeSource �� *Self+.��5
      � .+�5   
   �� -   5         ��Name$ 	textBlock��5      $
��Left=�6'   $��	10,10,0,0q�6B   $
��Wrap=�6U   $��Text of reminder��6i   $	��Top=�6�   �� 5   
   -   5         ��Name$ ReminderTextBox��5      $
��Left=�6+   $��85��6F   $��	10,36,0,0q�6R    �� TextWrapping$
 Wrap=�6e   $��Write your text here ...��6y   $	��Top=�6�   $	��257��6�   6�   (  "System.Windows.Controls.DatePicker  5   
   -   5        $
��Left=�5      $��252,156,0,0q�60   $	��Top=�6E   6]   �� 5   
   -   5        $ 
textBlock1��5      $
��Left=�6(   $��
49,164,0,0q�6C   $	��Top=�6y   $��40(�6�   ��6W   ��  �� Path$ CurrentHour�6�   �� 5   
   -   5        $ 
textBlock2��5      $
��Left=�6(   $��
88,164,0,0q�6C   $	��Top=�6{   $��40(�6�   ��6W   �� $ CurrentMinute�6�   �� 5   
   -   5        $��Right=�5   (   $��45��6D   $��0,132,299,96q�6O   ��5      �� 5      .R�5      ��  ��Name$ upArrowHours��6   $
��Left=�62   $��44��6M   $	��Top=�6Y   $��47��6q   $3��-pack://siteoforigin:,,,/img/up_arrow256px.png��6|   5      5      �� 5   
   -   5        $��Right=�5   (   $��45��6D   $��0,207,299,21q�6O   ��5      �� 5      .R�5      �� $ downArrowHours��6   $
��Left=�64   $��46��6O   $	��Top=�6[   $��47��6s   $5��/pack://siteoforigin:,,,/img/down_arrow256px.png��6~   $��	0,0,-2,-2q�6�   5      5      �� 5   
   -   5        $��Right=�5   *   $��45��6F   $��0,132,244,96q�6Q   ��5      �� 5       .R�5!      �� $ upArrowMinutes��6   $
��Left=�64   $��44��6O   $	��Top=�6[   $��47��6s   $3��-pack://siteoforigin:,,,/img/up_arrow256px.png��6~   5"      5$      �� 5%   
   -	   5        $��Right=�5%   *   $��45��6F   $��0,207,244,21q�6Q   ��5&      �� 5'      .R�5(      �� $ downArrowMinutes��6   $
��Left=�66   $��44��6Q   $	��Top=�6]   $��47��6u   $5��/pack://siteoforigin:,,,/img/down_arrow256px.png��6�   5)      5+      �� 5,   
   -
   5        $��Right=�5,   %   $��45��6A   $��0,207,194,21q�6L   ��5-      �� 5.      .R�5/      �� $ downArrowAMPM��6   $
��Left=�63   $��44��6N   $	��Top=�6Z   $��47��6r   $5��/pack://siteoforigin:,,,/img/down_arrow256px.png��6}   50      52      �� 53   
   -   5        $��Right=�53   %   $��45��6A   $��0,132,194,96q�6L   ��54      �� 55      .R�56      �� $ upArrowAMPM��6   $
��Left=�61   $��44��6L   $	��Top=�6X   $��47��6p   $3��-pack://siteoforigin:,,,/img/up_arrow256px.png��6{   57      59      �� 5:   
   -   5         ��Name$ AddReminder��5:      $	��Add��6&   $
��Left=�64   $��
287,59,0,0q�6O   $	��Top=�6c   $��67��6{   $��42��6�   6�   �� 5;   
   -   5        $ 
textBlock3��5;      $
��Left=�6(   $��
74,160,0,0q�6C   $
��Wrap=�6W   $��:��6k   $	��Top=�6t   $��40(�6�   $��57��6�   �� 5<   
   -   5        $ 
textBlock4��5<      $
��Left=�6(   $��137,164,0,0q�6C   $
��Wrap=�6X   $	��Top=�6�   $��40(�6�   ��6l   �� $ AMPMPr�6�   5=      5>      
   M S B A M L   `   `   `  ����E  AProiect PI, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Proiect_PI
Proiect_PI  5   	       Proiect_PI.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Proiect_PI     5   	     � Title$  
Proiect PI��5   	   $	��560��6   $	��800��6)   .��5	      � 
�������.+�5
   
   � $
��Left=�6   $	��336��6*   $��	18,73,0,0q�67   $	��Top=�6J   $	��173��6b   
�������.+�5      o� -   5         o�Name$
 menu��5      $
��Left=�6!   $	��173��6<   $��	0,0,0,-11q�6H   $	��338��6[   $	��Top=�6h   
�������.@�5      � $��64��6   $	��175��6#   
������� � RowDefinitions 5      �� 5      $	��15* �6(   �� 5      $	��49* �6(    � ColumnDefinitions 5      �� 5      $��2* �6+   �� 5      $	��33* �6+   .+�5      �� -   5         ��Name$ image��5      $��Right=�6+   $��68��6G   $	��Top=�6S   $��57��6k   $��
0,4,118,-8q�6v   $1��+pack://siteoforigin:,,,/img/reminder256.png��6�   $��2��6�   $��2��6�   �� 5      -   5        $��	-6,0,10,0q�5   6   $��Right=�6I   $	��171��6e   $��2��6q   $��2��6�   ��5      �� 5      .R�5   "   ��  ��Name$ reminderTextSideBar��6,   $
��Left=�6I   $��	78,19,0,0q�6d   $
��Wrap=�6w   $��	Reminders��6�   $	��Top=�6�   $��28��6�   $��83��6�   $��1��6�   $��17(�6�   
���   �6�   5      5      5      � 5      $��64��6   $	��172��6#   
������� 5      �� 5       .+�5"      �� -   5        $ image1��5"      $
��Left=�6,   $��53��6G   $	��Top=�6S   $��52��6k   $-��'pack://siteoforigin:,,,/img/note256.png��6v   $��-1,5,0,0q�6�   �� 5#      -   5        $��-1,0,0,0q�5#   2    J� RenderTransformOrigin$ 0.487,0.469&�6D   ��5$      �� 5%      .R�5&   "   �� $ noteTextSideBar��6,   $
��Left=�6E   $��	86,17,0,0q�6`   $
��Wrap=�6s   $��Notes��6�   $	��Top=�6�   $��28��6�   $��51��6�   $��1��6�   $��17(�6�   
���   �6�   5'      5)      5*      � 5+      $��64��6   $	��172��6#   
������� 5,      �� 5-      $	��83* �6+   �� 5.      $��3* �6+   .+�50      �� -   5        $ image2��50      $
��Left=�6,   $��58��6G   $	��Top=�6S   $��65��6k   $-��'pack://siteoforigin:,,,/img/ToDo512.png��6v   $��-2,0,0,0q�6�   �� 51      -   5        $��-7,0,0,0q�51   2   ��52      �� 53      .R�54   "   �� $ toDoTextSideBar��6,   $
��Left=�6E   $��	78,19,0,0q�6`   $
��Wrap=�6s   $��
To Do list��6�   $	��Top=�6�   $��28��6�   $��83��6�   $��1��6�   $��17(�6�   
���   �6�   55      57      58      � 59      $��64��6   $	��172��6#   
�������.+�5:      �� -   5        $ image4��5:      $
��Left=�6,   $��49��6G   $	��Top=�6S   $��62��6k   $-��'pack://siteoforigin:,,,/img/link512.png��6v   $��6,10,0,0q�6�   �� 5;      -	   5        $��-6,0,6,0q�5;   2   ��5<      �� 5=      .R�5>   "   �� $ timerTextSideBar��6,   $
��Left=�6F   $��	94,18,0,0q�6a   $
��Wrap=�6t   $��Links��6�   $	��Top=�6�   $��28��6�   $��43��6�   $��1��6�   $��17(�6�   
���   �6�   5?      5A      5B      � 5C      $��67��6   $	��172��6#   
�������.+�5D      �� -
   5        $ image5��5D      $
��Left=�6,   $��67��6G   $	��Top=�6S   $��64��6k   $.��(pack://siteoforigin:,,,/img/timer512.png��6v   �� 5E      -   5        $��	-6,0,5,-4q�5E   S   ��5F      �� 5G      .R�5H   "   �� $ reminderTextSideBar��6,   $
��Left=�6I   $��	91,19,0,0q�6d   $
��Wrap=�6w   $��Timer��6�   $	��Top=�6�   $��28��6�   $��47��6�   $��1��6�   $��17(�6�   
���   �6�   5I      5K      5L      5M      5N      � 5O   
   $
��Left=�6   $��23��6*   $��
10,498,0,0q�66   $	��Top=�6J   $	��782��6b   � 5P   
   $
��Left=�6   $	��425��6*   $��
210,73,0,0q�67   $	��Top=�6K   $	��572��6c   .+�5Q       � -   5          �Name$ 	MainFrame��5Q      $�� ��6'   $
��Left=�62   $	��425��6M   $	��Top=�6Z   $	��572��6r     � NavigationUIVisibility$ Hidden=�6~   5S      � 5T   
   $
��Left=�6   $��13��6*   $	��Top=�66   $	��792��6N   .+�5U      o� -   5        $ menu1��5U      $
��Left=�6"   $��19��6=   $	��Top=�6I   $	��792��6a   $��0,0,0,-6q�6m   
���   �.@�5V      m� $
��File��6   
�������
���   �.@�5W      m� $	��New��6   
�������
���   �.@�5X      m� -   5        $��Reminder��5X   #   
�������
���   �6c   m� 5Y      -   5        $
��Note��5Y   #   
�������
���   �6[   m� 5Z      -   5        $
��ToDo��5Z   #   
�������
���   �6[   m� 5[      -   5        $
��Link��5[   #   
�������
���   �6[   5\      5]      m� 5^      $
��Edit��6   
�������5_      m� 5`      $
��View��6   
�������5a      5b      5c      � 5d   
   $
��Left=�6   $��45��6*   $��	10,23,0,0q�66   $	��Top=�6I   $	��764��6a   .+�5e      � $
��Left=�6   $��45��6.   $	��Top=�6:   $��49��6R   .+�5f      �� -   5        $��0,0,0,0q�5f   1   ��5g      �� 5h      .R�5i      �� $ image3��6$   $
��Left=�64   $��43��6O   $	��Top=�6[   $��43��6s   $4��.pack://siteoforigin:,,,/img/addButton256px.png��6~   $��2,0,0,0q�6�   5j      5l      �� 5m      -   5        $��
49,0,-47,0q�5m   4   ��5n      �� 5o      .R�5p      �� $ image3��6$   $
��Left=�64   $��43��6O   $	��Top=�6[   $��48��6s   $,��&pack://siteoforigin:,,,/img/delete.png��6~   $��0,0,-1,0q�6�   5q      5s      5t      5u      5v      5w      
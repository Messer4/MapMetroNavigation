   M S B A M L   `   `   `  ����M  IMetroMapNavigation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:MetroMapNavigationMetroMapNavigation  5   	   #    MetroMapNavigation.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:MetroMapNavigation     5   	     � Title$  MetroNavigation��5   	    � WindowState$ 	Maximized=�6!    � 	Resources 5	      ��%    5      .��5      � -   5         �Name$ Grid1��5       � ShowGridLines .  J� RenderTransformOrigin$ 0.446,0.523&�6/    � RowDefinitions 5   
   �� 5      6    � ColumnDefinitions 5   
   �� 5      $	��250 �6   6-   �� 5      $
��auto �6   6/   .+�5   
   �� -   5         ��Name$ 	ComboBox1��5      $
��Left=�6&   $��20��6A   $��
46,155,0,0q�6M   $	��Top=�6a   $	��157��6y   5      �� 5   
   -   5        $ 	ComboBox2��5      $
��Left=�6&   $��21��6A   $��
46,241,0,0q�6M   $	��Top=�6a   $	��157��6y   6�   �� 5   
   -   5        	 ��Name$	 ImageMap��5      $��Resources/kiev-metro.png��6"   
 J� Opacity$
 1Q�6D   $��2��6R   F� 5   
   -   5         F�Name$ Path2��5      
��� ���$��2��6,   
���   ���5      � 5      -   5        
Group1�� � FillRule$ EvenOdd=�5   0   5      5!      F� 5#   
   -   5        $ Path1��5#      
���  ��$��2��6)   
���   ���5$      B� 5%      -   5        
Group2�� B� FillRule$ Nonzero=�5%   /   5'      5)      F� 5+   
   -	   5        $ Path3��5+      $��2��6   
���   �
����  �5-      �� 5/   
   -
   5         ��Name$ EnterButton��5/      $��Построить��6&   $
��Left=�6:   $��
46,302,0,0q�6U   $	��Top=�6i   $	��157��6�   $��38��6�   {�.  6�   �� 50   
   -   5         ��Name$ label��50      $#��Начальная Точка��6   $
��Left=�69   $��
46,122,0,0q�6T   $	��Top=�6h   $	��157��6�   $��28��6�   �� 51   
   -   5        $ label1��51      $!��Конечная точка��6    $
��Left=�69   $��
46,196,0,0q�6T   $	��Top=�6h   $	��157��6�   53      55      
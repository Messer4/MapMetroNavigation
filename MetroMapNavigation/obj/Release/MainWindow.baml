   M S B A M L   `   `   `  ���� M  IMetroMapNavigation, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:MetroMapNavigationMetroMapNavigation  #    MetroMapNavigation.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     2local clr-namespace:MetroMapNavigation       � Title$  MetroNavigation�� � WindowState$ 	Maximized=� � 	Resources ��%    .��� -    �Name$ Grid1�� � ShowGridLines .  J� RenderTransformOrigin$ 0.446,0.523&� � RowDefinitions ��  � ColumnDefinitions �� $	��250 ��� $
��auto �.+��� -    ��Name$ 	ComboBox1��$
��Left=�$��20��$��
46,155,0,0q�$	��Top=�$	��157���� -   $ 	ComboBox2��$
��Left=�$��21��$��
46,241,0,0q�$	��Top=�$	��157���� -   	 ��Name$	 ImageMap��$��Resources/kiev-metro.png��
 J� Opacity$
 1Q�$��2��F� -    F�Name$ Path2��
��� ���$��2��
���   ���� -   
Group1�� � FillRule$ EvenOdd=�F� -   $ Path1��
���  ��$��2��
���   ���B� -   
Group2�� B� FillRule$ Nonzero=�F� -	   $ Path3��$��2��
���   �
����  ��� -
    ��Name$ EnterButton��$��Построить��$
��Left=�$��
46,302,0,0q�$	��Top=�$	��157��$��38��{�.  �� -    ��Name$ label��$#��Начальная Точка��$
��Left=�$��
46,122,0,0q�$	��Top=�$	��157��$��28���� -   $ label1��$!��Конечная точка��$
��Left=�$��
46,196,0,0q�$	��Top=�$	��157��
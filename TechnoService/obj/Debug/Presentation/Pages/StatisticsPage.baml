   M S B A M L   `   `   `  ����H  DTechnoService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:TechnoService.Presentation.Pages TechnoService.Presentation.Pages  5      5    /TechnoService.Presentation.Pages.StatisticsPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      @local.clr-namespace:TechnoService.Presentation.Pages     5      $
��Auto��5      $
��Auto��6     N� Title$#  Окно статистики��5
       N� 
Background
 �����.��5      � $��24q�5   	    � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��* �6   �� 5      $��50 �6   .+�5   
   �� -   5        $��textBlockTitle��5      $��0��5         TextBlockHeader#��[  5      $E��?Статистика по типу неисправностей��5      $��Center=�5      $��Center=�5      �� 5   
   -   5        $��textBlockNotFound��5      $��1��5        TextBlockDefault#��[ 5      $G��AСтатистика отсутствует (нет заявок)��5       $��Center=�5!      $��Center=�5"       K� ZIndex$ 1��5#      &   System.Windows.Controls.DataGrid  5%   
   -   5        $!��dataGridFaultTypeStatistics��5'      $��1��5&        DataGridColumnHeaderDefault   ColumnHeaderStyle# [ 5(        DataGridRowDefault   RowStyle# [ 5)         
IsReadOnly .    AutoGenerateColumns .     CanUserReorderColumns .  	   SelectionMode$	 Single=�5-      
�������$��0q�5/      
   Columns
 50      0  *System.Windows.Controls.DataGridTextColumn  51      ,  &System.Windows.Controls.DataGridColumn   Header$' !Тип неисправности��52        TextBlockWrap1  +System.Windows.Controls.DataGridBoundColumn   ElementStyle# [ 54         CanUserSort .     Width5  /System.Windows.Controls.DataGridLengthConverter$ * 56         Binding 53      �� *FaultType.Name+56        58      $ Количество��59      # [ 5;      $ * 5<       5:      �� *
Quantity+5<      5>      � 5@   
   $��2��5A       � ColumnDefinitions 5B      �� 5C      $��* �6#   �� 5D      $
��Auto �6#   .+�5G      �� $��0��5H       �� Orientation$ Vertical=�5I      $��Bottom=�5J      $
��Left=�5K      .+�5L      �� -   5        $!��textBlockNumOfReadyRequests��5M      #��[ 5N      $?��9Количество выполненных заявок:��5O      $��0 0 0 8q�5P      �� 5R      -   5        $ ��textBlockAvgCompletionTime��5S      #��[ 5T      $M��GСреднее время выполнения заявок (часы):��5U      5V      �� 5X      -   5        $��
buttonBack��5Z      $��1��5Y        ButtonDefault#��[ 5[      $��
Назад��5\      $��0 16 0 0q�5^      5_      5`      5a      
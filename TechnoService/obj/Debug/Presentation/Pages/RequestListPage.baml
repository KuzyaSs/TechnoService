   M S B A M L   `   `   `  ����H  DTechnoService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:TechnoService.Presentation.Pages TechnoService.Presentation.Pages  5      6    0TechnoService.Presentation.Pages.RequestListPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      @local.clr-namespace:TechnoService.Presentation.Pages     5      $
��Auto��5      $
��Auto��6     N� Title$(  "Окно списка заявок��5
       N� 
Background
 �����.��5      � $��24q�5   	    � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��75 �6   �� 5      $��* �6   �� 5      $
��auto �6   .+�5   
   � $��0��5       � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $��* �6#   .+�5      �� -   5        $��textBlockTitle��5      $��0��5         TextBlockHeader#��[  5       $��Мои заявки��5!      $	��Top=�5"      $
��Left=�5#      �� 5%      $��1��5&       �� Orientation$ 
Horizontal=�5'      $	��Top=�5(      $��Right=�5)      .+�5*      �� -   5        $��textBlockLogin��5+        TextBlockDefault#��[ 5,      $��Center=�5-      $��
Логин��5.      $��0 0 16 0q�5/      �� 50      -   5        $��
buttonExit��51        ButtonDefault#��[ 52      $��
Выход��53      54      55      56      �� 58   
   $��1��59      $
��Left=�5:      .+�5;      �� #��[ 5<      $��
Поиск��5=      $��0 0 0 8q�5>      �� 5@      -   5        $��textBoxSearch��5A        TextBoxDefault#��[ 5B      $	��250��5C      5D      5E      �� 5G   
   -   5        $��textBlockNotFound��5H      $��2��5I      #��[ 5J      $)��#Заявки отсутствуют��5K      $��Center=�5L      $��Center=�5M       K� ZIndex$ 1��5N      &   System.Windows.Controls.DataGrid  5P   
   -   5          Name$ dataGridRequestList��5Q      $��2��5R        DataGridColumnHeaderDefault   ColumnHeaderStyle# [ 5S        DataGridRowDefault   RowStyle# [ 5T      	   
IsReadOnly	 . 
   AutoGenerateColumns
 .     CanUserReorderColumns .     SelectionMode$ Single=�5X      
�������$��0q�5Z         Columns 5[      0  *System.Windows.Controls.DataGridTextColumn  5\      ,  &System.Windows.Controls.DataGridColumn   Header$ Номер заявки��5]        TextBlockWrap1  +System.Windows.Controls.DataGridBoundColumn   ElementStyle# [ 5_         CanUserResize .     Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 5a         Binding 5^      �� *Id+5a        5c      $ Оборудование��5d      # [ 5f      $ * 5g       5e      �� *Equipment.Name+5g        5i      $' !Тип неисправности��5j      # [ 5l      $ * 5m       5k      �� *FaultType.Name+5m        5o      $ Статус��5p      # [ 5r      $ * 5s       5q      �� *Status.Name+5s        5u      $# Дата добавления��5v      # [ 5x       .  $ * 5z       5w      �� *PublicationDate+5z      4  .System.Windows.Controls.DataGridTemplateColumn  5|      $ Подробнее��5}       .     CanUserSort .  $
 Auto 5�         CellTemplate 5�      �� 5�      .R�5�      �� -   5        #��[ 5�   !   $��>��5�   !   5�   !   5�      5�      5�      �� 5�   
   $��3��5�      $ 
Horizontal=�5�      $��Right=�5�      .+�5�      �� -   5        $ ��buttonNavigateToStatistics��5�      #��[ 5�      $��Статистика��5�      $��0 16 0 0q�5�      �� 5�      -	   5        $'��!buttonNavigateToCreateRequestPage��5�      #��[ 5�      $!��Создать заявку��5�      $��	16 16 0 0q�5�      5�      5�      5�      
   M S B A M L   `   `   `  ����H  DTechnoService, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullS.clr-namespace:TechnoService.Presentation.Pages TechnoService.Presentation.Pages  5      8    2TechnoService.Presentation.Pages.CreateRequestPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      @local.clr-namespace:TechnoService.Presentation.Pages     5      $
��Auto��5      $
��Auto��6     N� Title$,  &Окно создания заявки��5
       N� 
Background
 �����.��5      � $��24q�5   	    � RowDefinitions 5   
   �� 5      $��35 �6   �� 5      $��70 �6   �� 5      $��70 �6   �� 5      $��* �6   �� 5      $��35 �6   .+�5   
   �� $��0��5         TextBlockHeader#��[  5      $��Новая заявка��5      $��Center=�5      $��Center=�5      �� 5   
   $��1��5      $
��Left=�5      .+�5       ��   TextBlockDefault#��[ 5!      $��Оборудование��5"      $��0 0 0 8q�5#      �� 5%      -   5        $��comboBoxEquipment��5&        ComboBoxDefault#��[ 5'      $
��Auto��5(      ��5)      �� 5*      .R�5+      �� ��6$   �� *Name+5,      5.      5/      �� 51   
   $��2��52      $
��Left=�53      .+�54      �� #��[ 55      $'��!Тип неисправности��56      $��0 0 0 8q�57      �� 59      -   5        $��comboBoxFaultType��5:      #��[ 5;       �� DisplayMemberPath$
 Name��5<      $
��Auto��5=      5>      � 5@   
   $��3��5A       5B      �� 5C      $
��Auto �6    �� 5D      $��* �6    .+�5F      �� $��0��5G      #��[ 5H      $'��!Описание проблемы��5I      $��0 0 0 8q�5J      �� 5L      -   5        $��textBoxDescription��5O      $��1��5M        TextBoxFill#��[ 5N      $��0 0 0 16q�5P      5Q      �� 5S   
   $��4��5T       �� Orientation$ 
Horizontal=�5U      $��Bottom=�5V      $��Right=�5W      .+�5X      �� -   5        $��
buttonBack��5Y        ButtonDefault#��[ 5Z      $��
Назад��5[      $��0 0 16 0q�5]      �� 5_      -   5        $��
buttonSend��5`      #��[ 5a      $��Отправить��5b      5c      5d      5e      5f      
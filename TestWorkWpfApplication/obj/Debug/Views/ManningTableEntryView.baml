   M S B A M L   `   `   `  ����F  BTestWork.PL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:TestWork.PL.ViewsTestWork.PL.Views  5   	   ?$clr-namespace:TestWork.PL.ViewModelsTestWork.PL.ViewModels  5   	   -    'TestWork.PL.Views.ManningTableEntryView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:TestWork.PL.Views     5   	   ;
viewModels$clr-namespace:TestWork.PL.ViewModels     5   	   -   5        $��EntryWindow��5	   	   $	��300��5
   !   $	��300��6.     � WindowStartupLocation$  CenterScreen=�5   	    � Title 5
   	   �� *Title+ � DataContext 5      7   1TestWork.PL.ViewModels.ManningTableEntryViewModel  5   
   .��5      � $��2q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   �� 5      .+�5   
   �� $��0��6   $��0��6   $��2q�6.   $��2q�69   $ ��Подразделение��6E   �� 5   
   $��0��6   $��1��6    $��2q�61   $��2q�6<   ��5      �� *	Divisions+ �� SelectedItem 5       �� *SelectedDivision+�� 5!   
   $��1��6   $��0��6   $��2q�6-   $��2q�68   $��Ставка��6D   �� 5"   
   $��1��6   $��1��6    $��2q�61   $��2q�6<   ��5#      �� *Rates+ 5$      �� *SelectedRate+�� 5%   
   $��2��6   $��0��6   $��2q�6-   $��2q�68   $��Количество��6D   �� 5&   
   $��2��6   $��1��6   $��2q�60   $��2q�6;   ��6G   �� *	HeadCount+�� 5(   
   $��3��6   $��0��6"   $��2��62    �� Orientation$ 
Horizontal=�5)      $��Right=�6.   .+�5*      �� $��Отменить��6   $��2q�6(   $��2q�63   ��5+      �� *Cancel+��5,      ��  �� ElementName$ EntryWindow���� 5-      $��Сохранить��6   $��2q�6)   $��2q�64   ��5.      �� *Save+��5/      �� $ EntryWindow��50      51      52      
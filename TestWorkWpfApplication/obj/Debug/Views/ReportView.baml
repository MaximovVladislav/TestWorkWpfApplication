   M S B A M L   `   `   `  ����F  BTestWork.PL, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:TestWork.PL.ViewsTestWork.PL.Views  5      ?$clr-namespace:TestWork.PL.ViewModelsTestWork.PL.ViewModels  5      "    TestWork.PL.Views.ReportView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:TestWork.PL.Views     5      ;
viewModels$clr-namespace:TestWork.PL.ViewModels     5        � DataContext  5      ,   &TestWork.PL.ViewModels.ReportViewModel  5   
   .��5      �  � RowDefinitions 5      �� 5      $
��Auto �6    �� 5      �� 5      $
��Auto �6    .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $
��Left=�6;   .+�5      �� $'��!Отчетный период: с��6   $��2q�61   $��2q�6<   (  "System.Windows.Controls.DatePicker  5      $��2q�6B      SelectedDate 6   �� *StartReportDate+6B   �� 5      $
��по��6   $��2q�6!   $��2q�6,     5      $��2q�6@    6   �� *EndReportDate+6@   5      &   System.Windows.Controls.DataGrid  5   
   $��1��6      
IsReadOnly .    AutoGenerateColumns .     RowHeaderWidth$ 20Q�5"      ��5      �� *Report+ �� SelectedItem 5       �� *SelectedReportEntry+   Columns 5#      0  *System.Windows.Controls.DataGridTextColumn  5$      ,  &System.Windows.Controls.DataGridColumn	   Header$	 
Отдел��6%   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 64   �� *Division.Name+4  .System.Windows.Controls.DataGridTemplateColumn  5&      $	 
Дата c��6)      
IsReadOnly .    CellTemplate 5'      �� 5(      .R�5)         6)   �� *	StartDate+ � 	Resources 5*   "   :  4System.Windows.Controls.Primitives.DatePickerTextBox �� 
TargetType �� Property �� Value��%[   '        �� 5+   &   "  6,   .�5,   *   ��  �@��
IsReadOnly . 6G   5-   '   5.   #   5/      50      52        54      $	 Дата по��6)    .  55      �� 56      .R�57         6)   �� *	EndDate+ 58   "   ��%[   '        �� 59   &   "  6,   .�5:   *   ��  �@��
IsReadOnly . 6G   5;   '   5<   #   5=      5>      5@        5B      $'	 !ФОТ отдела в месяц��6%   
 6A   �� *	PayRoll+5D      �� 5F   
   $��2��6   $ 
Horizontal=�6"   $��Right=�6;   .+�5G      �� $)��#Сформировать отчет��6   $��2q�62   $��2q�6=   ��5H      �� *	GetReport+5I      5J      5K      
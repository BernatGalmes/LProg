   M S B A M L   `   `   `  ����H  DWeek6_DataSet, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:Week6_DataSetWeek6_DataSet  5   	       Week6_DataSet.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -localclr-namespace:Week6_DataSet     5   	     � Title$  
MainWindow��5   	   $	��350��6   $	��525��6)   .��5	      � $��10q�6   $	��400��6   $	��190��6#   .+�5
   
   &   System.Windows.Controls.DataGrid  -   5        $��dg_students��5
         AutoGenerateColumns .  ��6&   ��    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ 95 6>      MinWidth$ 95Q�6I      MaxWidth$ 95Q�6W   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6%   �� *Name+   Header 5      �� 5      .+�5      �� $
��Name��6(   $��1q�64   $��DemiBold"�6?   $
��Wrap=�6U    �� TextAlignment$
 Left=�6i   5      	   ElementStyle	 5      �� 5      .�5      �� 
 �� Property
 � �� �� Value$
 Wrap=�6G   �� 5      
 �@��TextAlignment$
 Left=�6H   5      5        5      $ 95 6A   $ 95Q�6L   $ 95Q�6Z    6%   �� *	Surname+ 5      �� 5      .+�5      �� $��Surname��6(   $��1q�67   $��DemiBold"�6B   $
��Wrap=�6X   $
 Left=�6l   5      	 5      �� 5       .�5!      �� 
 � ��$
 Wrap=�6G   �� 5"      
 �@��TextAlignment$
 Left=�6H   5#      5%      4  .System.Windows.Controls.DataGridComboBoxColumn  5&      -   5        columnDegree��   SelectedValueBinding 5&   @   �� *
Degreeid+ 5'      �� 5(      .+�5)      �� $��Degree��6(   $��1q�66   $��DemiBold"�6A   $
��Wrap=�6W   $
 Left=�6k   5*         ElementStyle 5,      �� 5-      .�5.      �� 
 � ��$
 Wrap=�6G   �� 5/      
 �@��TextAlignment$
 Left=�6H   50      52      54      �� 55   
   -   5         ��Name$ comboBox��55      $
��Left=�6%   $��
56,-37,0,0q�6@   $	��Top=�6T   $	��120��6l   56      57      
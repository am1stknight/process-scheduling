   M S B A M L   `   `   `  ���� L  HProcessScheduling, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ProcessSchedulingProcessScheduling  "    ProcessScheduling.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     1localclr-namespace:ProcessScheduling       � Title$  Scheduling Algorithms��$	��495��$��1164.5�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=�.���  � RowDefinitions �� $��1* ��� $��9* � � ColumnDefinitions �� $	��33* ��� $	��33* ��� $	��33* �.+�� $��0��$��0��.+��� -   $��
Algorithms�� �� Text$  --Select a Algorithm--�� �� 
IsEditable . $��15(� �� 
IsReadOnly . $
��Left=�$��	10,10,0,0q�$	��Top=�$	��183��$��27��.@��� $��Round Robin���� $��Static Priority��� $��0��$��1��.+��� -   $��	AddButton��$��Add Process��{�.  $
��Left=�$	��Top=�$��82��$��25��$��7,10,0,0q��� -   $��DeleteButton��$��Delete Process��{�.  $
��Left=�$��
103,10,0,0q�$	��Top=�$��83��$��25��� $��0��$��2��.+��� -   $��StartButton��$��Start��{�.  $
��Left=�$	��Top=�$��75��$��25��$��	10,12,0,0q��� -   $��
StopButton��$
��Stop��$
��Left=�{�.  $��
101,12,0,0q�$	��Top=�$��75��$��25��� $��1��$��0��$��0,0,0,0q� �� $
��100* � �� $��2* ��� $��7* �.+�� $��0��$��0��.+��� $��8,0,10,0q�$��I/O resources��.��� .+�� .+��� $��Printer��$
��Left=�$��8,9,0,0q�$	��Top=�	� -   $��PrinterWait��$
��Wait��$
��Left=�$��	70,17,0,0q�$	��Top=�	� -   $��PrinterReady��$��Ready��$
��Left=�$��
131,17,0,0q� d� 	IsChecked$
 TrueY�$	��Top=�� .+��� $��Scanner��$
��Left=�$��8,35,0,0q�$	��Top=�	� -   $��ScannerWait��$
��Wait��$
��Left=�$��	70,41,0,0q�$	��Top=�	� -	   $��ScannerReady��$��Ready��$
��Left=�$��
131,41,0,0q�$
 TrueY�$	��Top=��� $��1��$��0��$��8,0,10,0q�$��Block��.��� .+��� -
   $��BlockedList��$
��Left=�$��15(�$	��284��$	��344��$	��Top=�$��2,10,0,0q�	 �� View	 �� .P��� $��Id��
 �� Width$
 40�� �� DisplayMemberBinding �� *Id+�� $
��Name��$	
 125�� �� *Name+�� $��Priority��$
 60�� �� *
Priority+�� $��Bound To��$
 95�� �� *Bound+�� $��1��$��1��$��0,0,4,0q�$��Ready��.��� .+��� -   $��	ReadyList��$
��Left=�$��15(�$	��377��$	��351��$	��Top=�$��7,10,0,0q�	 �� .P��� $��Id��$
 40�� �� *Id+�� $
��Name��$	
 125�� �� *Name+�� $��Priority��$
 60�� �� *
Priority+�� $��Bound To��$
 95�� �� *Bound+�� $��1��$��2��$��1,0,10,0q�$��Running��.��� .+��� -   $��RunningList��$
��Left=�$��15(�$	��377��$	��346��$	��Top=�$��7,10,0,0q�	 �� .P��� $��Id��$
 40�� �� *Id+�� $
��Name��$	
 125�� �� *Name+�� $��Priority��$
 60�� �� *
Priority+�� $��Bound To��$
 95�� �� *Bound+
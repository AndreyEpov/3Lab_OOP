   M S B A M L   `   `   `  ����C  ?3Lab_OOP, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:_3Lab_OOP	_3Lab_OOP  5   	   c _GMap.NET.WindowsPresentation, Version=1.9.8.0, Culture=neutral, PublicKeyToken=b85b9027b614afefqPclr-namespace:GMap.NET.WindowsPresentation;assembly=GMap.NET.WindowsPresentationGMap.NET.WindowsPresentation 5   	       _3Lab_OOP.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:_3Lab_OOP     5   	   pWindowsPresentationPclr-namespace:GMap.NET.WindowsPresentation;assembly=GMap.NET.WindowsPresentation     5   	     � Title$  
MainWindow��5	   	   $��751.932��6   $
��1199��6-   .��5
      � $��0q�6   .+�5   
   .  (GMap.NET.WindowsPresentation.GMapControl  -   5        $	��Map��5   *   $	��800��6H   $	��600��6T   $
��Left=�6a   $	��Top=�6|   6�   �� 5   
   -   5         ��Name$ objType��5      $��899,112,91,0q�6$   $	��Top=�6:    �� SelectedIndex$ 1��6R   $��23��6d   .@�5      �� $	��Car��6   �� 5      $��Human��6   �� 5      $��Location��6   �� 5      $
��Area��6   �� 5      $
��Rout��6   5      �� 5   
   -   5         ��Name$ objTitle��5      $
��Left=�6$   $��41��6?   $��899,171,0,0q�6K    �� TextWrapping$
 Wrap=�6`   $	��Top=�6t   $	��201��6�   6�   �� 5   
   -   5         ��Name$ label��5      $��ObjectTitle��6   $
��Left=�65   $��899,140,0,0q�6P   $	��Top=�6e   $��26��6}   $	��201��6�   �� 5   
   -   5         ��Name$ button��5      $��Focus��6!   $
��Left=�61   $��899,377,0,0q�6L   $	��Top=�6a   $	��201��6y   $��39��6�   6�   �� 5   
   -   5         ��Name$ 
objectList��5      $
��Left=�6&   $	��148��6A   $��899,447,0,0q�6N   $	��Top=�6c   $	��201��6{   $��2��6�   6�   �� 5   
   -   5        $ button1��5      $��
Add object��6"   $
��Left=�67   $��899,217,0,0q�6R   $	��Top=�6g   $	��201��6   $��39��6�   6�   	� 5   
   -   5         	�Name$ 
createObjs��5      $��
createObjs��6*   $
��Left=�6?   $��1043,36,0,0q�6Z   $	��Top=�6o   $��17��6�   $��81��6�   	 d� 	IsChecked$
	 TrueY�6�   	� 5   
   -	   5        $
 find��5      $
��find��6$   $
��Left=�63   $��1043,58,0,0q�6N   $	��Top=�6c   $��15��6{   $��81��6�   6�   �� 5   
   
���   �$��1q�6%   $
��Left=�69   $��60��6T   $��
882,26,0,0q�6`   $	��Top=�6t   $	��242��6�   
 � Cursor$
 SizeNESW��6�   {�.   J� Opacity$	 0.5Q�6�   $y�Hidden=�6�    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      5      �� 5    
   -
   5        $ label1��5       $��Change cursor��6    $
��Left=�68   $��
899,38,0,0q�6S   $	��Top=�6g   $��37��6   $	��120��6�   $��SimSun)�6�   $��16(�6�   �� 5!   
   
���   �$��1q�6%   $
��Left=�69   $	��244��6T   $��882,100,0,0q�6a   $	��Top=�6v   $	��242��6�   $	 0.5Q�6�   $y�Hidden=�6�    5"        5#      5%      �� 5&   
   
���   �$��1q�6%   $
��Left=�69   $	��253��6T   $��882,358,0,0q�6a   $	��Top=�6v   $	��229��6�   $y�Hidden=�6�   �� 5'   
   -   5        $ 	Clear_but��5'      $��Clear��6$   $
��Left=�64   $��899,261,0,0q�6O   $	��Top=�6d   $	��201��6|   $��39��6�   6�   �� 5(   
   -   5        $ 
But_ToDest��5(      $��Where��6%   $
��Left=�65   $��
10,650,0,0q�6P   $	��Top=�6d   $	��274��6|   $��34��6�   6�   �� 5)   
   -   5        $ But_callCar��5)      $��Calling a car ��6&   $
��Left=�6?   $��289,650,0,0q�6Z   $	��Top=�6o   $	��295��6�   $��34��6�   6�   �� 5*   
   -   5        $ But_Ok��5*      $��Ok��6!   $
��Left=�6.   $��612,650,0,0q�6I   $	��Top=�6^   $	��201��6v   $��34��6�   6�   5,      5-      
   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5       CrowdDJ.Playstation9   3CrowdDJ.Playstation.UserControls.PartyWindowControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5      .��5	      �   � ColumnDefinitions  5
   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   �� $��0��6   $��5��6'   $��
5, 0, 0, 0q�61   $��Stretch=�6E   � 5   
   $��0��6    � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5      $��* �6   �� 5      $
��auto �6   .+�5   
   �� $��0��6   $��Liste aller Partys��6   $��Center=�6:   &   System.Windows.Controls.DataGrid  5      -   5        $��PartyDataGrid��5         CanUserReorderColumns .  $��1��5         AutoGenerateColumns .     CanUserDeleteRows .     CanUserSortColumns .  ��5      ��  �� Path$ 
AllParties� �� Mode$ TwoWay=� �� SelectedItem 5      �� $ IsSelectedParty�$ TwoWay=�.   (System.Windows.Interactivity.Interaction	   Triggers	 5      /   )System.Windows.Interactivity.EventTrigger  5      
   	EventName$
 MouseDoubleClick��6!   .   (System.Windows.Interactivity.TriggerBase   Actions. 5      6   0System.Windows.Interactivity.InvokeCommandAction     Command 6,   �� *DoubleClickCommand+   CommandParameter 5   ,   ��  �� ElementName$ PartyDataGrid��$ SelectedItem�5         Columns 5!      0  *System.Windows.Controls.DataGridTextColumn  5"      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6Q   1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 63   �� $
 Name�6Q     5#      $
 Host��6%   $
 auto 6Q    63   �� $
 Host�6Q     5$      $ Location��6%   $
 auto 6Y    67   �� $ Location�6Y     5%      $ Beginn��6%   $
 auto 6Y    65   �� $ 
PartyBegin�6Y     5&      $
 Ende��6%   $
 auto 6U    63   �� $ PartyEnd�6U   5(      � 5)   
   $��2��6     5*      �� 5+      $��* �6#   �� 5,      $��* �6#   �� 5-      $��* �6#   .+�5/      �� -   5        $��btnAddParty��5/   %   $��0��6   $��Neue Party erstellen��68   $��Center=�50      $	��120��62   $
��auto��6>   $��2q�6L   ��51      �� $ AddNewPartyCommand��� 52      -   5        $��btnUpdateParty��52   %   $��1��6   $��Party updaten��6;   $��Center=�53      $	��120��62   $
��auto��6>   $��2q�6L   ��54      �� $ UpdatePartyCommand���55      �� $ PartyDataGrid��$ SelectedItem��� 56      -   5        $��btnDeleteParty��56   %   $��2��6   $��Party löschen��6;   $��Center=�57      $	��120��62   $
��auto��6>   $��2q�6L   ��58      �� $ DeletePartyCommand���59      �� $ PartyDataGrid��$ SelectedItem�5:      5;      � 5<   
   $��1��6    5=      �� 5>      $��* �6    �� 5?      $
��auto �6    .+�5A      � $��0��6     5B      �� 5C      $��* �6'   �� 5D      $��* �6'   .+�5F      � $��0��6    5G      �� 5H      $
��auto �6(   �� 5I      $��* �6(   .+�5K      �� $��0��6   $��Verfügbare Teilnehmer��6)   $��Center=�6I     5L      -   5        $��AllUserGrid��5L   ,   $��1��6   $��
2, 0, 2, 0q�6?    .   .   .   .  ��5N      �� $ AllUser�$ TwoWay=� 5O      �� $ IsSelectedAddUser�$ TwoWay=� 5Q        5R      $ UserID��61   $
 auto 6a    6A   �� $ UserId�6a     5S      $
 Name��61   $
 auto 6]    6?   �� $
 Name�6]     5T      $ E-Mail��61   $
 auto 6`    6A   �� $ Email�6`   5W      5X      � 5Y      $��1��6    5Z      �� 5[      $
��auto �6(   �� 5\      $��* �6(   .+�5^      �� $��0��6   $��
Teilnehmer��6)   $��Center=�6>     5_      -   5        $��AttendingUserGrid��5_   ,   $��1��6   $��
2, 0, 2, 0q�6E    .   .   .   .  ��5a      �� $ AttendingUser�$ TwoWay=� 5b      �� $ IsSelectedAttUser�$ TwoWay=� 5d        5e      $ UserID��61   $
 auto 6a    6A   �� $ UserId�6a     5f      $
 Name��61   $
 auto 6]    6?   �� $
 Name�6]     5g      $ E-Mail��61   $
 auto 6`    6A   �� $ Email�6`   5j      5k      5l      �� 5m      $��1��6   $��Benutzer hinzufügen��6"   $��2q�6@   $��Center=�6K   ��5n      �� *AddUserToPartyCommand+��5o      �� $ AllUserGrid��$ SelectedItem�5p      5r      5s      
   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5       CrowdDJ.Playstation8   2CrowdDJ.Playstation.UserControls.UserWindowControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5      .��5	      �   � ColumnDefinitions  5
   
   �� 5      $
��auto �6   �� 5      $��* �6   .+�5   
   � $��0��6   $��0,0,5,0q�6    � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $��1* �6    .+�5      �� $��0��6   $��Liste aller Benutzer��6!   $��Center=�6@   � 5      $��1��6    5      �� 5      $��* �6$   �� 5      $
��auto �6$   .+�5      &   System.Windows.Controls.DataGrid  -   5        $��UserDataGrid��5      $��0��5         AutoGenerateColumns .     CanUserReorderColumns .     CanUserDeleteRows .     CanUserSortColumns .  ��5      ��  �� Path$ AllUser� �� Mode$ TwoWay=� �� SelectedItem 5      �� $ IsSelectedUser�$ TwoWay=�.   (System.Windows.Interactivity.Interaction	   Triggers	 5      /   )System.Windows.Interactivity.EventTrigger  5       
   	EventName$
 MouseDoubleClick��6)   .   (System.Windows.Interactivity.TriggerBase   Actions. 5!      6   0System.Windows.Interactivity.InvokeCommandAction     Command 64   �� *DoubleClickCommand+   CommandParameter 5"   4   ��  �� ElementName$ UserDataGrid��$ SelectedItem�5#         Columns 5%      0  *System.Windows.Controls.DataGridTextColumn  5&      ,  &System.Windows.Controls.DataGridColumn   Header$
 Name��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6Y   1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 6;   �� $
 Name�6Y     5'      $ E-Mail��6-   $
 auto 6\    6=   �� $ Email�6\     5(      $ IsAdmin��6-   $
 auto 6_    6>   �� $ IsAdmin�6_   5*      � 5+      $��1��6     5,      �� 5-      $��* �6+   �� 5.      $��* �6+   �� 5/      $��* �6+   .+�51      �� -   5        $��bttnAddUser��51   -   $��0��6   $��Benutzer hinzufügen��6@   $	��120��52      $��2q�6)   ��64   �� *AddUserWindowCommand+�� 53      -   5        $��btnUpdateUser��53   -   $��1��6   $��Benutzer updaten��6B   $	��120��54      $��2q�6)   ��64   �� *UpdateUserCommand+��55      �� $ UserDataGrid��$ SelectedItem��� 56      -   5        $��bttnDeleteUser��56   -   $��2��6   $��Benutzer löschen��6C   $	��120��57      $��2q�6)   ��64   �� *DeleteUserCommand+��58      �� $ UserDataGrid��$ SelectedItem�59      5:      5;      �� 5<   
   $��0��6   $��5��6'   $��
5, 0, 0, 0q�61   $��Stretch=�6E   � 5=   
   $��1��6    5>      �� 5?      $
��auto �6    �� 5@      $��1* �6    �� 5A      $
��auto �6    �� 5B      $
��auto �6    �� 5C      $��1* �6    �� 5D      $
��auto �6    �� 5E      $
��auto �6    �� 5F      $��1* �6    �� 5G      $
��auto �6    .+�5I      �� $��0��6   $��Center=�6!   $��An Partys teilgenommen��6>     5J      -   5        $��PartylistGrid��5J      $��1��6,    .     
IsReadOnly . $��5,0,0,5q�5L       .   .  ��5K      �� $ AttemptedParties�$ TwoWay=� �� IsAsync .  5M        5N      $ PartyID��6)   $
 auto 6[    6:   �� $ PartyId�6[     5O      $
 Name��6)   $
 auto 6U    67   �� $
 Name�6U     5P      $ Location��6)   $
 auto 6]    6;   �� $ Location�6]     5Q      $
 Host��6)   $
 auto 6U    67   �� $
 Host�6U   5S      �� 5T      -   5        $��bttnDeleteFromParty��5T   "   $��2��6   $��Von Party löschen��6=   $	��150��5U      $��2q�6$   ��6/   �� *DeleteUserFromPartyCommand+��5V      �� $ PartylistGrid��$ SelectedItem��� 5W      $��3��6   $��Center=�6!   $��Gesendete Partytweets��6>     5X      -   5        $��PartytweetDataGrid��5X      $��4��61    .   .  $��5,5,0,5q�5Z   3    .  .  ��5X   >   �� $ SentPartyTweets�$ TwoWay=� .  5Y      �� $ IsSelectedTweet� 5[        5\      $ PartyID��6)   $
 auto 6[    6:   �� $ PartyId�6[     5]      $ 	Nachricht��6)   $
 auto 6]    6<   �� $ Message�6]   5_      �� 5`      -   5        $��bttnDeleteTweet��5`   "   $��5��6   $��Partytweet löschen��69   $	��150��5a      $��2q�6%   ��61   �� *DeletePartytweetCommand+��5b      �� $ PartytweetDataGrid��$ SelectedItem��� 5c      $��6��6   $��Center=�6!   $$��Gewunschene / Gewählte Tracks��6>     5d      $��7��6    .  .  $��5,5,0,0q�5e   E    .  ��5d   $   �� $ VotedTracks�$ TwoWay=� .  5g        5h      $ Titel��6)   $
 auto 6W    68   �� $ Title�6W     5i      $ Artist��6)   $
 auto 6Y    69   �� $ Artist�6Y     5j      $ Genre��6)   $
 auto 6W    68   �� $ Genre�6W   5l      �� 5m      $��8��6   $��Sicht Aktualisieren��6"   $	��150��5n      $��4q�6$   ��5m   @   �� $ UpdateUserViewCommand�5n   $   5o      5p      5q      
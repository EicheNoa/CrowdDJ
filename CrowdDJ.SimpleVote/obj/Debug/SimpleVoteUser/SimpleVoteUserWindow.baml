   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5       CrowdDJ.SimpleVote<   6CrowdDJ.SimpleVote.SimpleVoteUser.SimpleVoteUserWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5        � Title$  SimpleVoteUserWindow��5   	   $	��400��6&   $	��800��63   .��5      �  � RowDefinitions 5   
   �� 5      $
��auto �6   �� 5	      $��* �6   .+�5   
   �� $��0��6   $��20(�6?   $��Center=�6M   ��6   ��  �� Path$ CurParty�6M   � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      $
��auto �6#   �� 5      $��* �6#   �� 5      $
��auto �6#   .+�5      � $��0��6    5      �� 5      $��* �6$   �� 5      $
��auto �6$   .+�5      &   System.Windows.Controls.DataGrid  -   5        $��	TrackGrid��5   ^   $��0��6      AutoGenerateColumns .     CanUserDeleteRows .     CanUserReorderColumns .     
IsReadOnly .    CanUserSortColumns .  $	��200��5   6   ��5   (   �� $ PartyTracks�	 �� Mode$	 TwoWay=�
 �� SelectedItem
 5   7   �� $ CurrentTrack�$	 TwoWay=�   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Titel��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6[   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6<   �� $ Title�6[     5      $ Artist��6-   $
 auto 6]    6=   �� $ Artist�6]     5      $ Genre��6-   $
 auto 6[    6<   �� $ Genre�6[   5       �� 5!      $��1��6   $
��Vote��6&   $��3q�65   ��6@   �� *VoteForTrackCommand+��5"      ��  �� ElementName$ 	TrackGrid��$ SelectedItem�5#      � 5$      $��1��6   $��
4, 0, 4, 0q�6#    5%      �� 5&      $
��auto �6$   �� 5'      $��* �6$   �� 5(      $
��auto �6$   .+�5*      �� $��0��6   $��Gesendete Partytweets��6%   $��Center=�6E     5+      $��1��6    .   .   .   .   . ��5,      �� $ AllTweetsForParty�$	 TwoWay=� 5.        5/      $ Benutzer��6-   $
 auto 6a    6?   �� $ UserName�6a     50      $ Tweet��6-   $
 auto 6]    6<   �� $ Message�6]   52      �� 53      $��2��6    �� Orientation$ Vertical=�6*   .+�54      �� -   5        $��NewTweetText��54      $��4q�6Y   ��62   �� *
NewTweet+$	 TwoWay=�6Y   �� 55      $��Schicken��6   $	��200��61   $��4q�6=   ��6H   �� *SendNewTweetCommand+��56      �� $ NewTweetText��$ SelectedText�57      58      � 59      $��2��6   $��
0, 0, 0, 0q�6#   .+�5:      �� $��Center=�6   .+�5;      �� $��Neuen Track vorschlagen��6   �� 5<      -   5        $��cmbTrack��5<   S   $��0��6   ��6,   �� $ 	AllTracks�
 5=      �� $ SelectedTrack�$	 TwoWay=���5>      �� 5?      .R�5@   "   �� $
��Left=�6-   .+�5A   &   �� ��6,   �� $ Title��� 5B   &   ��6,   �� $ Intepret��� 5C   &   ��6,   �� $ Genre�5D   #   5E      5G      �� 5H      $��Vorschlagen��6   $��4q�5I   b   ��5H   3   �� *SuggestTrackCommand+��5I      �� $ cmbTrack��$ SelectedItem�6b   5J      5K      5n      5o      5p      
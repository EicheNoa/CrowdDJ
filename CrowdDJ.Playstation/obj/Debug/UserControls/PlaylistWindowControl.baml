   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.5.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5       CrowdDJ.Playstation<   6CrowdDJ.Playstation.UserControls.PlaylistWindowControl   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5      .��5	      �   � ColumnDefinitions  5
   
   �� 5      $��* �6   �� 5      $
��auto �6   .+�5   
   � $��0��6    � RowDefinitions 5      �� 5      $
��auto �6    �� 5      $��* �6    �� 5      $
��auto �6    .+�5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $��Aktuelles Lied: ��6   �� 5      ��6   ��  �� Path$ 	SongTitle�5      � 5      $��1��6    5      �� 5      $��* �6$   �� 5      $
��auto �6$   .+�5      r� -   5        $��	mediaPlay��5   ,   $��0��6    r� LoadedBehavior$ Manual=�5   @    r� Source 6   �� $ CurrentTrack� r� Volume 5      �� $ Volume�.   (System.Windows.Interactivity.Interaction   Triggers 5       /   )System.Windows.Interactivity.EventTrigger  5!         	EventName$ 
MediaEnded��6)   .   (System.Windows.Interactivity.TriggerBase	   Actions.	 5"      6   0System.Windows.Interactivity.InvokeCommandAction  
   Command
 64   �� *NextMediaCommand+5#      5%      �� 5&      $��1��6    � Minimum$ 0Q�6Q    � Maximum$ 1Q�6]    � Value 6&   �� $ Volume� �� Mode$ TwoWay=�6]   5'      � 5(      $��2��6     5)      �� 5*      $��* �6'   �� 5+      $��* �6'   �� 5,      $��* �6'   �� 5-      $��* �6'   �� 5.      $��* �6'   .+�50      �� -   5        $��btnPlay��50   )   $��0��6   $
��Play��68   $��60��6Q   $��2q�51      ��6$   �� *PlayMediaCommand+�� 52      -   5        $��btnPause��52   )   $��1��6   $��Pause��69   $��60��6Q   $��2q�53      ��6$   �� *PauseMediaCommand+�� 54      -   5        $��btnStop��54   )   $��2��6   $
��Stop��68   $��60��6Q   $��2q�55      ��6$   �� *StopMediaCommand+�� 56      -   5        $��btnPrevious��56   )   $��3��6   $
��Prev��6<   $��60��6M   $��2q�57      ��6$   �� *PreviousMediaCommand+�� 58      -   5        $��btnNext��58   )   $��4��6   $
��Next��68   $��60��6Q   $��2q�59      ��6$   �� *NextMediaCommand+5:      5;      � 5<   
   $��1��6    5=      �� 5>      $
��auto �6    �� 5?      $��* �6    �� 5@      $
��auto �6    .+�5B      �� $��0��6   ��6$   �� $ 
AllParties� �� SelectedItem 5C      �� $ IsSelectedParty�$ TwoWay=���5D      �� 5E      .R�5F      �� ��6$   �� $
 Name�5G      5I      &   System.Windows.Controls.DataGrid  5J      -   5        $��TrackDataGrid��5J   @   $��1��6      AutoGenerateColumns .     
IsReadOnly .    CanUserDeleteRows .     CanUserSortColumns .  ��5K      �� $ Tracks� �� IsAsync . $ TwoWay=� 5L      �� $ SelectedTrack�$ TwoWay=�   Columns 5N      0  *System.Windows.Controls.DataGridTextColumn  5O      ,  &System.Windows.Controls.DataGridColumn   Header$ Titel��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6W   1	  +System.Windows.Controls.DataGridBoundColumn 	  Binding 68   �� $ Title�6W     5P      $ Artist��6)   $
 auto 6Y    69   �� $ Artist�6Y     5Q      $ Genre��6)   $
 auto 6W    68   �� $ Genre�6W     5R      $ Video��6)   $
 auto 6Y    68   �� $ IsVideo�6Y   5T      � 5U      $��2��6     5V      �� 5W      $
��auto �6'   �� 5X      $
��auto �6'   �� 5Y      $
��auto �6'   .+�5[      �� -   5        $��bttnAddTrack��5[   )   $��0��6   $��Track hinzufügen��6=   $	��100��6X   $��2q�5\      ��6$   �� $ AddNewTrackCommand��� 5]      -	   5        $��btnShowAllTracks��5]   )   $��2��6   $��Alle Lieder��6A   $	��100��6W   $��2q�5^      ��6$   �� $ ShowAllTracksCommand�5_      5`      5a      5b      
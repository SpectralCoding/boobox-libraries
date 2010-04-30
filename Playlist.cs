using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BooBox {
	public class Playlist {
		public List<SongInfo> SongList = new List<SongInfo>();
		public String Name;
		public String GUID;

		/// <summary>
		/// Adds a song to the Active Playlist.
		/// </summary>
		/// <param name="SongInfo">MusicFile data to be added to the Playlist.</param>
		public void AddSongToList(SongInfo SongInfo) {
			if (SongList.Contains(SongInfo)) {
				MessageBox.Show("This playlist already contains an entry for \"" + SongInfo.Title + "\". You may not add it again.");
			} else {
				SongList.Add(SongInfo);
				//UpdateActivePlaylistDGV();
			}
		}

		public void RemoveSongsByGUID(String GUID) {
			for (int i = 0; i < SongList.Count; i++) {
				if (SongList[i].ServerGUID == GUID) {
					SongList.RemoveAt(i);
					i--;
				}
			}
			//UpdateMainFrmDGV();
		}

	}
}

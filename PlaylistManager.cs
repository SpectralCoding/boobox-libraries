using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BooBox {
	public static class PlaylistManager {
		public static List<Playlist> PlaylistList = new List<Playlist>();

		public static Boolean CreatePlaylist(String Name) {
			for (int i = 0; i < PlaylistList.Count; i++) {
				if (PlaylistList[i].Name == Name) {
					MessageBox.Show("A playlist with the name \"" + Name + "\" already exists. New playlist not added.");
					return false;
				}
			}
			Playlist tempPlaylist = new Playlist();
			tempPlaylist.Name = Name;
			tempPlaylist.GUID = Guid.NewGuid().ToString();
			PlaylistList.Add(tempPlaylist);
			return true;
		}

		public static String[] ListPlaylists() {
			String[] tempReturnStr = new String[PlaylistList.Count];
				for (int i = 0; i < PlaylistList.Count; i++) {
					tempReturnStr[i] = PlaylistList[i].Name + " (" + PlaylistList[i].SongList.Count + ")";
				}
			return tempReturnStr;
		}

		public static void DeletePlaylistByName(String PlaylistName) {
			for (int i = 0; i < PlaylistList.Count; i++) {
				if (PlaylistList[i].Name == PlaylistName) {
					PlaylistList.RemoveAt(i);
					return;
				}
			}
		}

		public static int AddSongInfoListToPlaylist(List<SongInfo> SongInfoList, String PlaylistName) {
			int successfulCount = 0;
			for (int i = 0; i < PlaylistList.Count; i++) {
				if (PlaylistList[i].Name == PlaylistName) {
					for (int x = 0; x < SongInfoList.Count; x++) {
						if (PlaylistList[i].AddSongToList(SongInfoList[x])) { successfulCount++; }
					}
					return successfulCount;
				}
			}
			return successfulCount;
		}

		public static void PrintPlaylistTree() {
			for (int i = 0; i < PlaylistList.Count; i++) {
				Console.WriteLine(PlaylistList[i].Name + " (" + PlaylistList[i].GUID + ")");
				for (int x = 0; x < PlaylistList[i].SongList.Count; x++) {
					Console.WriteLine("\t" + PlaylistList[i].SongList[x].Title + " (" + PlaylistList[i].SongList[x].ServerGUID + " | " + PlaylistList[i].SongList[x].MD5 + ")");
				}
			}
		}

		public static List<SongInfo> GetPlaylistListByName(String PlaylistName) {
			for (int i = 0; i < PlaylistList.Count; i++) {
				if (PlaylistList[i].Name == PlaylistName) {
					return PlaylistList[i].SongList;
				}
			}
			return new List<SongInfo>();
		}

		public static void OverwritePlaylistByName(List<SongInfo> SongInfoList, String PlaylistName) {
			for (int i = 0; i < PlaylistList.Count; i++) {
				if (PlaylistList[i].Name == PlaylistName) {
					PlaylistList[i].SongList = SongInfoList;
					return;
				}
			}
		}

		public static int[] GetAttributeCountByName(String PlaylistName) {
			for (int i = 0; i < PlaylistList.Count; i++) {
				if (PlaylistList[i].Name == PlaylistName) {
					return PlaylistList[i].GetAttributeCount();
				}
			}
			int[] tempReturn = { 0, 0, 0 };
			return tempReturn;
		}

	}
}

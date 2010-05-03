using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BooBox {
	public static class PlaylistManager {
		public static List<Playlist> PlaylistList = new List<Playlist>();
		public static List<RemotePlaylistInfo> RemotePlaylistList = new List<RemotePlaylistInfo>();

		/// <summary>
		/// Creates a Playlist with the specified Name.
		/// </summary>
		/// <param name="Name">Name of new Playlist</param>
		/// <returns>Boolean revealing whether or not the Playlist was successfully created</returns>
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
			tempPlaylist.IsLocal = true;
			PlaylistList.Add(tempPlaylist);
			return true;
		}

		/// <summary>
		/// Lists all playlists by name and song count in a String Array.
		/// </summary>
		/// <returns>String[] containing list of Playlists and their song counts</returns>
		public static String[] ListPlaylists() {
			String[] tempReturnStr = new String[PlaylistList.Count];
				for (int i = 0; i < PlaylistList.Count; i++) {
					tempReturnStr[i] = PlaylistList[i].Name + " (" + PlaylistList[i].SongList.Count + ")";
				}
			return tempReturnStr;
		}

		/// <summary>
		/// Deletes a playlist by name.
		/// </summary>
		/// <param name="PlaylistName">Name of playlist to delete</param>
		public static void DeletePlaylistByName(String PlaylistName) {
			for (int i = 0; i < PlaylistList.Count; i++) {
				if (PlaylistList[i].Name == PlaylistName) {
					PlaylistList.RemoveAt(i);
					return;
				}
			}
		}

		/// <summary>
		/// Adds a list of SongInfo objects to a playlist by playlist name.
		/// </summary>
		/// <param name="SongInfoList">List of SongInfo objects</param>
		/// <param name="PlaylistName">Name of playlist to add songs to</param>
		/// <returns>Integer revealing number of songs successfully added to the playlist.</returns>
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
		
		/// <summary>
		/// Prints the playlist tree to the Console.
		/// </summary>
		public static void PrintPlaylistTree() {
			Console.WriteLine("Local Playlists:");
			for (int i = 0; i < PlaylistList.Count; i++) {
				Console.WriteLine(PlaylistList[i].Name + " (" + PlaylistList[i].GUID + ")");
				//for (int x = 0; x < PlaylistList[i].SongList.Count; x++) {
				//	Console.WriteLine("\t" + PlaylistList[i].SongList[x].Title + " (" + PlaylistList[i].SongList[x].ServerGUID + " | " + PlaylistList[i].SongList[x].MD5 + ")");
				//}
			}
			Console.WriteLine("Remote Playlists:");
			for (int i = 0; i < RemotePlaylistList.Count; i++) {
				Console.WriteLine(RemotePlaylistList[i].Name + " (" + RemotePlaylistList[i].ServerGUID + ")");
			}
		}

		/// <summary>
		/// Gets a Playlist's SongInfo list from the playlist name.
		/// </summary>
		/// <param name="PlaylistName">Name of playlist sought</param>
		/// <returns>List of SongInfo objects inside the playlist</returns>
		public static List<SongInfo> GetPlaylistListByName(String PlaylistName) {
			for (int i = 0; i < PlaylistList.Count; i++) {
				if (PlaylistList[i].Name == PlaylistName) {
					return PlaylistList[i].SongList;
				}
			}
			return new List<SongInfo>();
		}

		/// <summary>
		/// Overwrited the playlist related to PlaylistName with SongInfoList's contents.
		/// </summary>
		/// <param name="SongInfoList">List of SongInfo objects to populate the playlist with</param>
		/// <param name="PlaylistName">Name of playlist to overwrite</param>
		public static void OverwritePlaylistByName(List<SongInfo> SongInfoList, String PlaylistName) {
			for (int i = 0; i < PlaylistList.Count; i++) {
				if (PlaylistList[i].Name == PlaylistName) {
					PlaylistList[i].SongList = SongInfoList;
					return;
				}
			}
		}

		/// <summary>
		/// Returns an int[] array containing attribute count data for a specific playlist.
		/// </summary>
		/// <param name="PlaylistName">Name of playlist to search for</param>
		/// <returns>int[0] = Total Songs, int[1] = Unique Artists, int[2] = Unique Albums</returns>
		public static int[] GetAttributeCountByName(String PlaylistName) {
			for (int i = 0; i < PlaylistList.Count; i++) {
				if (PlaylistList[i].Name == PlaylistName) {
					return PlaylistList[i].GetAttributeCount();
				}
			}
			int[] tempReturn = { 0, 0, 0 };
			return tempReturn;
		}

		public static void ClearRemotePlaylistList() {
			RemotePlaylistList.Clear();
		}

		public static void AddRemotePlaylist(String ServerGUID, String PlaylistName, int SongCount) {
			RemotePlaylistInfo tempRPI = new RemotePlaylistInfo();
			tempRPI.ServerGUID = ServerGUID;
			tempRPI.Name = PlaylistName;
			tempRPI.SongCount = SongCount;
			RemotePlaylistList.Add(tempRPI);
		}

	}
}

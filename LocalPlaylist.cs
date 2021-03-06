﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace BooBox {
	public class LocalPlaylist {
		public List<SongInfo> SongList = new List<SongInfo>();
		public String Name;
		public String GUID;

		/// <summary>
		/// Adds a song to the Active Playlist.
		/// </summary>
		/// <param name="SongInfo">MusicFile data to be added to the Playlist.</param>
		public Boolean AddSongToList(SongInfo SongInfo) {
			for (int i = 0; i < SongList.Count; i++) {
				if (SongList[i].MD5 == SongInfo.MD5) {
					return false;
				}
			}
			SongList.Add(SongInfo);
			return true;
		}

		/// <summary>
		/// Removes all songs from Playlist by Server GUID.
		/// </summary>
		/// <param name="GUID">GUID to remove</param>
		public void RemoveSongsByGUID(String GUID) {
			for (int i = 0; i < SongList.Count; i++) {
				if (SongList[i].ServerGUID == GUID) {
					SongList.RemoveAt(i);
					i--;
				}
			}
			//UpdateMainFrmDGV();
		}

		/// <summary>
		/// Returns an int[] array containing attribute count data.
		/// </summary>
		/// <returns>int[0] = Total Songs, int[1] = Unique Artists, int[2] = Unique Albums</returns>
		public int[] GetAttributeCount() {
			int[] returnInt = new int[3];
			ArrayList songList = new ArrayList();
			ArrayList artistList = new ArrayList();
			ArrayList albumList = new ArrayList();
			foreach (SongInfo tempSI in SongList) {
				if (!artistList.Contains(Functions.StringArrToDelimitedStr(tempSI.AlbumArtists, "; "))) {
					returnInt[1]++;
					artistList.Add(Functions.StringArrToDelimitedStr(tempSI.AlbumArtists, "; "));
				}
				if (!albumList.Contains(tempSI.Album)) {
					returnInt[2]++;
					albumList.Add(tempSI.Album);
				}
			}
			returnInt[0] = SongList.Count();
			return returnInt;
		}

		/// <summary>
		/// Returns a string describing the playlist.
		/// </summary>
		/// <returns>[Local] Playlist Name (Song Count)</returns>
		public override String ToString() {
			return "[Local] " + Name + " (" + SongList.Count.ToString() + ")";
		}

		/// <summary>
		/// Returns a string containing the LocalPlaylist in XML form.
		/// </summary>
		/// <returns>XML String</returns>
		public String GetXMLString() {
			StringWriter tempResult = new StringWriter();
			XmlWriter XmlWriter = XmlWriter.Create(tempResult);
			XmlWriter.WriteStartDocument();
			XmlWriter.WriteStartElement("playlistdata");
			XmlWriter.WriteElementString("name", Name);
			XmlWriter.WriteElementString("songcount", SongList.Count.ToString());
			XmlWriter.WriteElementString("guid", GUID);
			foreach (SongInfo tempSI in SongList) {
				XmlWriter.WriteElementString("song", tempSI.MD5);
			}
			XmlWriter.WriteEndElement();
			XmlWriter.WriteEndDocument();
			XmlWriter.Close();
			return tempResult.ToString();
		}

	}
}

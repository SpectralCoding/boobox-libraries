using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BooBox {

	public struct ConnectionInfo {
		public String IPAddress;
		public int InfoPort;
		public int StreamPort;
		public Boolean RequiresPassword;
		public String Password;
		public String GUID;
		public String Name;
		public String Description;
	}

	public struct SongInfo {
		public String FileName;
		public String Album;
		public String[] AlbumArtists;
		public String Comment;
		public String[] Genres;
		public String Title;
		public String ServerGUID;
		public int Track;
		public int TrackCount;
		public int Year;
		public long StartByte;
		public long EndByte;
		public long FileLength;
		public int BitRate;
		public double PlayLength;
		public String MD5;
	}

	public struct ServerDetail {
		public String Name;
		public String Value;
	}

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BooBox {

	public struct ConnectionInfo {
		public String Hostname;
		public String IPAddress;
		public int InfoPort;
		public int StreamPort;
		public Boolean RequiresPassword;
		public String Password;
		public String GUID;
		public String Name;
		public String Description;
		public DateTime LastLibraryQuery;
		public String InternalGUID;
	}

	public struct SongInfo {
		public String Album;
		public String[] AlbumArtists; 
		public int BitRate;
		public String Comment;
		public long EndByte;
		public long FileLength;
		public String FileName;
		public String[] Genres;
		public String MD5;
		public int PlayCount;
		public double PlayLength;
		public String ServerGUID;
		public long StartByte; 
		public String Title;
		public int Track;
		public int TrackCount;
		public int Year;
	}

	public struct ServerDetail {
		public String Name;
		public String Value;
	}

}

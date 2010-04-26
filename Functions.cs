using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Security.Cryptography;

namespace BooBox {
	public class Functions {

		/// <summary>
		/// Returns a recursive directory listing for a fold based on a file mask.
		/// </summary>
		/// <param name="Directory">String containing a directory path to recursively list.</param>
		/// <param name="FileMask">File mask for filtering certain files.</param>
		/// <returns>ArrayList containing all files in a directory tree.</returns>
		public static ArrayList RecursiveDirctoryListing(String Directory, String FileMask) {
			ArrayList ReturnList = new ArrayList();
			DirectoryInfo DirInfo = new DirectoryInfo(Directory);
			FileInfo[] FileList = DirInfo.GetFiles(FileMask);
			foreach (FileInfo File in FileList) {
				ReturnList.Add(File.FullName);
			}
			DirectoryInfo[] DirList = DirInfo.GetDirectories();
			foreach (DirectoryInfo Dir in DirList) {
				ReturnList.AddRange(RecursiveDirctoryListing(Dir.FullName, FileMask));
			}
			return ReturnList;
		}

		/// <summary>
		/// Prints an ArrayList object to the console.
		/// </summary>
		/// <param name="InputArray">ArrayList to print.</param>
		public static void PrintArrayList(ArrayList InputArray) {
			Console.WriteLine("Array Output: ");
			for (int i = 0; i < InputArray.Count; i++) {
				Console.WriteLine("[" + i + "] " + InputArray[i]);
			}
		}

		/// <summary>
		/// Converts a TagLib.File to a SongInfo object.
		/// </summary>
		/// <param name="ID3File">TagLib.File to Convert to SongInfo.</param>
		/// <param name="ServerGUID">GUID of the server who owns the file.</param>
		/// <returns>SongInfo containing data from TagLib.File</returns>
		public static SongInfo ID3ToSongInfo(TagLib.File ID3File, String ServerGUID) {
			SongInfo tempSongInfo = new SongInfo();
			String[] tempStr = new String[ID3File.Tag.AlbumArtists.Length];
			tempSongInfo.Album = RemoveNewLineChars(ID3File.Tag.Album);
			for (int i = 0; i < ID3File.Tag.AlbumArtists.Length; i++) { tempStr[i] = RemoveNewLineChars(ID3File.Tag.AlbumArtists[i]); }
			tempSongInfo.AlbumArtists = tempStr;
			tempSongInfo.BitRate = ID3File.Properties.AudioBitrate;
			tempSongInfo.Comment = RemoveNewLineChars(ID3File.Tag.Comment);
			tempSongInfo.EndByte = ID3File.InvariantEndPosition;
			tempSongInfo.FileLength = ID3File.Length;
			tempSongInfo.FileName = RemoveNewLineChars(ID3File.Name);
			tempStr = new String[ID3File.Tag.Genres.Length];
			for (int i = 0; i < ID3File.Tag.Genres.Length; i++) { tempStr[i] = RemoveNewLineChars(ID3File.Tag.Genres[i]); }
			tempSongInfo.Genres = tempStr;
			tempSongInfo.PlayLength = ID3File.Properties.Duration.TotalMilliseconds;
			tempSongInfo.ServerGUID = ServerGUID;
			tempSongInfo.StartByte = ID3File.InvariantStartPosition;
			tempSongInfo.Title = RemoveNewLineChars(ID3File.Tag.Title);
			tempSongInfo.Track = (int)ID3File.Tag.Track;
			tempSongInfo.TrackCount = (int)ID3File.Tag.TrackCount;
			tempSongInfo.Year = (int)ID3File.Tag.Year;
			tempSongInfo.MD5 = CreateMD5FromSongInfo(tempSongInfo);
			return tempSongInfo;
		}

		public static String CreateMD5FromSongInfo(SongInfo inputSongInfo) {
			System.Security.Cryptography.MD5CryptoServiceProvider MD5Crypto = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] MD5Data = System.Text.Encoding.Unicode.GetBytes("|" + inputSongInfo.Album + "|" + inputSongInfo.AlbumArtists.Length + "|" + inputSongInfo.BitRate + "|" + inputSongInfo.Comment + "|" + inputSongInfo.EndByte + "|" + inputSongInfo.FileLength + "|" + inputSongInfo.Genres.Length + "|" + inputSongInfo.PlayLength + "|" + inputSongInfo.StartByte + "|" + inputSongInfo.Title + "|" + inputSongInfo.Track + "|" + inputSongInfo.TrackCount + "|" + inputSongInfo.Year + "|");
			MD5Data = MD5Crypto.ComputeHash(MD5Data);
			string MD5Output = "";
			for (int i = 0; i < MD5Data.Length; i++) {
				MD5Output += MD5Data[i].ToString("x2").ToUpper();
			}
			return MD5Output;
		}

		/// <summary>
		/// Dumps a byte[] Array to a file.
		/// </summary>
		/// <param name="inputBytes">byte[] Array to export.</param>
		/// <param name="FileName">Filename to export to.</param>
		public static void DumpByteArrayToFile(byte[] inputBytes, String FileName) {
			File.Delete(FileName);
			FileStream MusicFileFS = new FileStream(FileName, FileMode.Create, FileAccess.ReadWrite);
			if (MusicFileFS.CanWrite) {
				MusicFileFS.Write(inputBytes, 0, inputBytes.Length);
			}
			MusicFileFS.Close();
		}

		/// <summary>
		/// Prints the ASCII values of every character in a character array delimited by a pipe character to the Console.
		/// </summary>
		/// <param name="inputCharArr">String to be split.</param>
		public static void PrintCharArrASCII(char[] inputCharArr) {
			Console.Write("|");
			for (int i = 0; i < inputCharArr.Length; i++) {
				Console.Write(((int)inputCharArr[i]).ToString() + "|");
			}
			Console.WriteLine();
		}

		/// <summary>
		/// Prints the ASCII values of every character in a string delimited by a pipe character to the Console.
		/// </summary>
		/// <param name="inputStr">String to be split.</param>
		public static void PrintStringASCII(String inputStr) {
			char[] charArr = inputStr.ToCharArray();
			Console.Write("|");
			for (int i = 0; i < charArr.Length; i++) {
				Console.Write(((int)charArr[i]).ToString() + "|");
			}
			Console.WriteLine();
		}

		/// <summary>
		/// Prints the ASCII values of every character in a character array delimited by a pipe character to the Console.
		/// </summary>
		/// <param name="inputByteArr">String to be split.</param>
		public static void PrintByteArrASCII(byte[] inputByteArr) {
			char[] tempChar = Encoding.ASCII.GetChars(inputByteArr);
			Console.Write("|");
			for (int i = 0; i < tempChar.Length; i++) {
				Console.Write(((int)tempChar[i]).ToString() + "|");
			}
			Console.WriteLine();
		}

		/// <summary>
		/// Removes all newline characters from a string (ASCII 10/13).
		/// </summary>
		/// <param name="inputStr">String to remove characters from.</param>
		/// <returns>String lacking ASCII 10/13.</returns>
		public static String RemoveNewLineChars(String inputStr) {
			if (inputStr != null) {
				return inputStr.Replace(Convert.ToChar(13).ToString(), "").Replace(Convert.ToChar(10).ToString(), "");
			}
			return null;
		}

		/// <summary>
		/// Returns a string containing the inputStrArr String Array with elemented delimited by Delimiter.
		/// </summary>
		/// <param name="inputStrArr">String Array to enumerate.</param>
		/// <param name="Delimiter">Delimiter to interleave between elements.</param>
		/// <returns>String delimited by Delimiter.</returns>
		public static String StringArrToDelimitedStr(String[] inputStrArr, String Delimiter) {
			String returnStr = "";
			foreach (String tempStr in inputStrArr) {
				returnStr += tempStr + Delimiter;
			}
			if (returnStr.Length > Delimiter.Length) {
				returnStr = returnStr.Substring(0, (returnStr.Length - Delimiter.Length));
			}
			return returnStr;
		}

		/*
		/// <summary>
		/// Converts a song's ID3 tag to a XML String.
		/// </summary>
		/// <param name="ID3Tag">TagLib.File containing Tag Data.</param>
		/// <returns>XML String</returns>
		public static String ID3ToXMLString(TagLib.File ID3Tag) {
			StringWriter tempResult = new StringWriter();
			XmlWriter XmlWriter = XmlWriter.Create(tempResult);
			MusicFile tempMF = ID3ToMusicFile(ID3Tag);
			XmlWriter.WriteStartDocument();
			XmlWriter.WriteStartElement("song");
			XmlWriter.WriteElementString("album", tempMF.Album);
			XmlWriter.WriteStartElement("albumartists");
			for (int i = 0; i < tempMF.AlbumArtists.Length; i++) {
				XmlWriter.WriteElementString("artist", tempMF.AlbumArtists[i]);
			}
			XmlWriter.WriteEndElement();
			XmlWriter.WriteElementString("comment", tempMF.Comment);
			XmlWriter.WriteElementString("endbyte", ID3Tag.InvariantEndPosition.ToString());
			XmlWriter.WriteElementString("filename", tempMF.FileName);
			XmlWriter.WriteStartElement("genres");
			for (int i = 0; i < tempMF.Genres.Length; i++) {
				XmlWriter.WriteElementString("genre", tempMF.Genres[i]);
			}
			XmlWriter.WriteEndElement();
			XmlWriter.WriteElementString("startbyte", ID3Tag.InvariantStartPosition.ToString());
			XmlWriter.WriteElementString("title", tempMF.Title);
			XmlWriter.WriteElementString("track", tempMF.Track.ToString());
			XmlWriter.WriteElementString("trackcount", tempMF.TrackCount.ToString());
			XmlWriter.WriteElementString("year", tempMF.Year.ToString());
			XmlWriter.WriteElementString("filelength", (new FileInfo(ID3Tag.Name)).Length.ToString());
			XmlWriter.WriteElementString("bitrate", ID3Tag.Properties.AudioBitrate.ToString());
			XmlWriter.WriteElementString("duration", ID3Tag.Properties.Duration.TotalMilliseconds.ToString());
			XmlWriter.WriteEndElement();
			XmlWriter.WriteEndDocument();
			XmlWriter.Close();
			return tempResult.ToString();
		}
		*/
	}
}

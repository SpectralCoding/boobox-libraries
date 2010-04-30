using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.IO.Compression;
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

		public static int OccurancesInString(String Haystack, String Needle) {
			return Regex.Matches(Haystack, Needle).Count;
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
			tempSongInfo.FileLength = (new FileInfo(ID3File.Name)).Length;
			tempSongInfo.FileName = RemoveNewLineChars(ID3File.Name);
			tempStr = new String[ID3File.Tag.Genres.Length];
			for (int i = 0; i < ID3File.Tag.Genres.Length; i++) { tempStr[i] = RemoveNewLineChars(ID3File.Tag.Genres[i]); }
			tempSongInfo.Genres = tempStr;
			tempSongInfo.PlayCount = 0;
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

		/// <summary>
		/// Generates a MD5 Hash from a SongInfo object using information unique to the SongInfo object.
		/// </summary>
		/// <param name="inputSongInfo">SongInfo object.</param>
		/// <returns>String containing a MD5 hash.</returns>
		public static String CreateMD5FromSongInfo(SongInfo inputSongInfo) {
			String inputString;
			inputString = "|" + inputSongInfo.Album + "|";
			for (int i = 0; i < inputSongInfo.AlbumArtists.Length; i++) { inputString += inputSongInfo.AlbumArtists[i] + "|"; }
			inputString += inputSongInfo.BitRate + "|";
			for (int i = 0; i < inputSongInfo.Genres.Length; i++) { inputString += inputSongInfo.Genres[i] + "|"; }
			inputString += inputSongInfo.PlayLength + "|" + inputSongInfo.Title + "|";
			System.Security.Cryptography.MD5CryptoServiceProvider MD5Crypto = new System.Security.Cryptography.MD5CryptoServiceProvider();
			byte[] MD5Data = System.Text.Encoding.Unicode.GetBytes(inputString);
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
		/// Dumps a String to a file.
		/// </summary>
		/// <param name="inputString">String to export.</param>
		/// <param name="FileName">Filename to export to.</param>
		public static void DumpStringToFile(String inputString, String FileName) {
			File.Delete(FileName);
			FileStream MusicFileFS = new FileStream(FileName, FileMode.Create, FileAccess.ReadWrite);
			if (MusicFileFS.CanWrite) {
				MusicFileFS.Write(Encoding.UTF8.GetBytes(inputString), 0, Encoding.UTF8.GetBytes(inputString).Length);
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

		/// <summary>
		/// Converts bytes into a human readable format rounded to an accuracy.
		/// </summary>
		/// <param name="Bytes">Number of bytes to be converted.</param>
		/// <param name="Accuracy">Decimal accuracy. Use 0 for no decimal point.</param>
		/// <returns>String in the format of [Number][Unit]</returns>
		public static String BytesToHumanReadable(long Bytes, int Accuracy) {
			if (Bytes < 1073741824) {
				return Math.Round((Bytes / 1048576.0), Accuracy).ToString("0.0") + "MB";
			} else if (Bytes < 1099511627776) {
				return Math.Round((Bytes / 1073741824.0), Accuracy).ToString("0.0") + "GB";
			} else {
				return Math.Round((Bytes / 1099511627776.0), Accuracy).ToString("0.0") + "TB";
			}
		}

		/// <summary>
		/// Converts milliseconds to a human readable format.
		/// </summary>
		/// <param name="Milliseconds">Number of milliseconds to convert</param>
		/// <returns>Returns a hh:mm:ss formated string.</returns>
		public static String MillisecondsToHumanReadable(double Milliseconds) {
			TimeSpan tempTS = new TimeSpan(0, 0, 0, 0, Convert.ToInt32(Milliseconds));
			if (tempTS.Hours > 0) {
				return tempTS.Hours.ToString() + ":" + tempTS.Minutes.ToString("00") + ":" + tempTS.Seconds.ToString("00");
			} else {
				return tempTS.Minutes.ToString() + ":" + tempTS.Seconds.ToString("00");
			}
		}

		/// <summary>
		/// Compresses a String using the gzip compression method.
		/// </summary>
		/// <param name="inputString">String to compress</param>
		/// <returns>Base64String containing the compressed data</returns>
		public static string CompressString(String inputString) {
			byte[] buffer = Encoding.UTF8.GetBytes(inputString);
			MemoryStream ms = new MemoryStream();
			using (GZipStream zip = new GZipStream(ms, CompressionMode.Compress, true)) {
				zip.Write(buffer, 0, buffer.Length);
			}
			ms.Position = 0;
			MemoryStream outStream = new MemoryStream();
			byte[] compressed = new byte[ms.Length];
			ms.Read(compressed, 0, compressed.Length);
			byte[] gzBuffer = new byte[compressed.Length + 4];
			System.Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
			System.Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);
			return Convert.ToBase64String(gzBuffer);
		}

		/// <summary>
		/// Decompresses a String using the gzip compression method.
		/// </summary>
		/// <param name="compressedString">Base64String to decompress</param>
		/// <returns>Decompressed UTF8 string</returns>
		public static string DecompressString(String compressedString) {
			byte[] gzBuffer = Convert.FromBase64String(compressedString);
			using (MemoryStream ms = new MemoryStream()) {
				int msgLength = BitConverter.ToInt32(gzBuffer, 0);
				ms.Write(gzBuffer, 4, gzBuffer.Length - 4);
				byte[] buffer = new byte[msgLength];
				ms.Position = 0;
				using (GZipStream zip = new GZipStream(ms, CompressionMode.Decompress)) {
					zip.Read(buffer, 0, buffer.Length);
				}
				return Encoding.UTF8.GetString(buffer);
			}
		}

		public static int ConnectionInfoInternalGUIDToIndex(List<ConnectionInfo> ConnectionInfoList, String GUID) {
			for (int i = 0; i < ConnectionInfoList.Count; i++) {
				if (ConnectionInfoList[i].InternalGUID == GUID) {
					return i;
				}
			}
			return -1;
		}

		public static ConnectionInfo ServerGUIDToConnectionInfo(List<ConnectionInfo> ConnectionInfoList, String GUID) {
			for (int i = 0; i < ConnectionInfoList.Count; i++) {
				if (ConnectionInfoList[i].GUID == GUID) {
					return ConnectionInfoList[i];
				}
			}
			return new ConnectionInfo();
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

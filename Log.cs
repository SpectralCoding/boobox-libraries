using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace BooBox {
	public static class Log {

		private static FileStream LogFS;
		private static StreamWriter LogSW;

		/// <summary>
		/// Creates and Opens a log file for program output.
		/// </summary>
		private static void OpenLog() {
			LogFS = new FileStream(@"logs/" + DateTime.Now.ToString("yyMMdd-HHmmss") + ".log", FileMode.Create, FileAccess.ReadWrite);
			LogSW = new StreamWriter(LogFS);
		}

		/// <summary>
		/// Closes an opened log for program output.
		/// </summary>
		public static void CloseLog() {
			LogSW.Close();
			LogFS.Close();
		}
		
		/// <summary>
		/// Adds a line of plaintext to the log file.
		/// </summary>
		/// <param name="LineToAdd">Line of plaintext to add</param>
		public static void AddPlainText(String LineToAdd) {
			if (LogFS == null) {
				if (!Directory.Exists("logs/")) {
					Directory.CreateDirectory("logs/");
				}
				OpenLog();
			}
			if ((LogFS != null) && (LogSW != null)) {
				LogSW.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff ") + "[PLAIN]\t" + LineToAdd);
			}
		}

		/// <summary>
		/// Adds a line of status text to the log file.
		/// </summary>
		/// <param name="LineToAdd">Line of plaintext to add</param>
		public static void AddStatusText(String LineToAdd) {
			if (LogFS == null) {
				if (!Directory.Exists("logs/")) {
					Directory.CreateDirectory("logs/");
				}
				OpenLog();
			}
			if ((LogFS != null) && (LogSW != null)) {
				LogSW.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff ") + "[STATUS]\t" + LineToAdd);
			}
		}

		/// <summary>
		/// Adds a line of client text to the log file.
		/// </summary>
		/// <param name="LineToAdd">Line of plaintext to add</param>
		/// <param name="ClientNumber">Number of the client related to the line of plaintext</param>
		public static void AddClientText(String LineToAdd, int ClientNumber) {
			if (LogFS == null) {
				if (!Directory.Exists("logs/")) {
					Directory.CreateDirectory("logs/");
				}
				OpenLog();
			}
			if ((LogFS != null) && (LogSW != null)) {
				LogSW.WriteLine(DateTime.Now.ToString("hh:mm:ss.fff ") + "[CL-" + ClientNumber.ToString() + "]\t" + LineToAdd);
			}
		}


	}
}

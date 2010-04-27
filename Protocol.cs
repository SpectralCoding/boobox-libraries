using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BooBox {
	public class Protocol {

		/// <summary>
		/// Creates a HELLO statement based on the Server name.
		/// </summary>
		/// <returns>HELLO [Server Name]</returns>
		public static String CreateHELLO(String ServerName) {
			return "HELLO " + ServerName;
		}

		/// <summary>
		/// Creates a HELLOR statement based on the Client name.
		/// </summary>
		/// <returns>HELLOR [Client Name]</returns>
		public static String CreateHELLOR(String ClientName) {
			return "HELLOR " + ClientName;
		}

		/// <summary>
		/// Creates a VERSION statement based on the Server version.
		/// </summary>
		/// <returns>VERSION [Server Version]</returns>
		public static String CreateVERSION() {
			return "VERSION " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
		}

		/// <summary>
		/// Creates a VERSIONR statement based on the Client version.
		/// </summary>
		/// <returns>VERSIONR [Client Version]</returns>
		public static String CreateVERSIONR() {
			return "VERSIONR " + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
		}

		/// <summary>
		/// Creates a OK statement signifying that the Client is fully connected.
		/// </summary>
		/// <returns>OK</returns>
		public static String CreateOK() {
			return "OK";
		}

		/// <summary>
		/// Creates a DETAILS statement based on Server details.
		/// </summary>
		/// <returns>DETAILS [Server Details]</returns>
		public static String CreateDETAILS(List<ServerDetail> DetailsList) {
			String detailsString = String.Empty;
			for (int i = 0; i < DetailsList.Count; i++) {
				detailsString += DetailsList[i].Name + "=" + DetailsList[i].Value + " ";
			}
			detailsString = detailsString.Trim();
			return "DETAILS " + detailsString;
		}

		/// <summary>
		/// Parses a DETAILS string sent by a Server.
		/// </summary>
		/// <param name="DetailsStr">DETAILS string sent by the Server.</param>
		/// <returns>List of ServerDetail type containing Name/Value pairs.</returns>
		public static List<ServerDetail> ParseDetails(String DetailsStr) {
			List<ServerDetail> TempList = new List<ServerDetail>();
			ServerDetail TempSD = new ServerDetail();
			char[] spaceDelim = new char[] { ' ' };
			char[] equalsDelim = new char[] { '=' };
			String[] tokenData = DetailsStr.Split(spaceDelim);
			String[] tokenSplit;
			for (int i = 0; i < tokenData.Length; i++) {
				tokenSplit = tokenData[i].Split(equalsDelim, 2);
				TempSD.Name = tokenSplit[0];
				TempSD.Value = tokenSplit[1];
				TempList.Add(TempSD);
			}
			return TempList;
		}

		/// <summary>
		/// Creates a GOODBYE statement to close a Server connection.
		/// </summary>
		/// <returns>GOODBYE</returns>
		public static String CreateGOODBYE() {
			return "GOODBYE";
		}

		/// <summary>
		/// Creates a PASS statement based on the stored Server password.
		/// </summary>
		/// <returns>PASS [Server Password]</returns>
		public static String CreatePASS(String Password) {
			return "PASS " + Password;
		}


	}
}

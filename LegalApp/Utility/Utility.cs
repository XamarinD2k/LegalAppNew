using System;
using System.Text;

namespace LegalApp
{
	public static class Utility
	{
		/// <summary>
		/// Returns the encrypted string.
		/// </summary>
		/// <returns>The encrypt string</returns>
		/// <param name="Input">Decrypted String</param>
		public static string ToEncrypt(this string Input)
		{
			string strEncrypt = string.Empty;
			byte[] arrBytes =	null;
			try{
				arrBytes =	Encoding.UTF8.GetBytes (Input);
				strEncrypt = Convert.ToBase64String(arrBytes);

				return strEncrypt;
			}
			catch{
				return null;
			}
			finally{
				arrBytes = null;
				strEncrypt = string.Empty;
			}
		}

		/// <summary>
		/// Returns the decrypted string.
		/// </summary>
		/// <param name="Input">Encrypted String</param>
		public static string ToDecrypt(this string Input)
		{
			string strDecrypt = string.Empty;
			byte[] arrBytes =	null;
			try{
				arrBytes =	Convert.FromBase64String(Input);
				strDecrypt = Encoding.UTF8.GetString (arrBytes, 0, arrBytes.Length);

				return strDecrypt;
			}
			catch(Exception ex){
				return ex.Message;
			}
			finally{
				arrBytes = null;
				strDecrypt = string.Empty;
			}
		}


	}
}


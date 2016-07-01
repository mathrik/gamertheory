using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for Toolbox
/// </summary>
public class Toolbox {
	private string encryption_key;

	public Toolbox() {
		//
		// TODO: Add constructor logic here
		//
		encryption_key = "";
	}

	public int getInt(string number) {
		int retVal = 0;
		try {
			retVal = Convert.ToInt32(number);
			return retVal;
		} catch {
			return 0;
		}
	}

	public string EZEncrypt(string data) {
		FE_SymmetricNamespace.FE_Symmetric symmetric = new FE_SymmetricNamespace.FE_Symmetric();
		return symmetric.EncryptData(encryption_key, data);
	}

	public string EZDecrypt(string encrypted_data) {
		FE_SymmetricNamespace.FE_Symmetric symmetric = new FE_SymmetricNamespace.FE_Symmetric();
		return symmetric.DecryptData(encryption_key, encrypted_data);
	}

	public string cleanUserSubmission(string usertext) {
		string result = HttpContext.Current.Server.HtmlEncode(usertext);
		result = result.Replace(Environment.NewLine, Environment.NewLine + "<br />");
		return result;
	}

	public string getWholeWordPreview(string text, int charcount) {
		string stripped = Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
		if (stripped.Length > charcount) {
			string cut = stripped.Substring(0, charcount);
			int lastspace = cut.LastIndexOf(" ");
			if (lastspace != 0 && lastspace <= cut.Length) {
				return cut.Substring(0, lastspace);
			} else {
				return cut;
			}
		} else {
			return stripped;
		}
	}

	private int RandomNumber(int min, int max) {
		Random random = new Random();
		return random.Next(min, max);
	}

	/// <summary>
	/// Generates a random string with the given length
	/// </summary>
	/// <param name="size">Size of the string</param>
	/// <param name="lowerCase">If true, generate lowercase string</param>
	/// <returns>Random string</returns>
	private string RandomString(int size, bool lowerCase) {
		StringBuilder builder = new StringBuilder();
		Random random = new Random();
		char ch;
		for (int i = 0; i < size; i++) {
			ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
			builder.Append(ch);
		}
		if (lowerCase)
			return builder.ToString().ToLower();
		return builder.ToString();
	}

	public string randomPassword() {
		StringBuilder builder = new StringBuilder();
		int random1, random2, random3, start, end;
		random1 = RandomNumber(5, 8);
		random2 = RandomNumber(4, 6);
		random3 = RandomNumber(4, 7);
		start = Convert.ToInt32(Math.Pow(10.0, Convert.ToDouble(random1)));
		end = (start * 10) - 1;
		builder.Append(RandomString(random2, true));
		builder.Append(RandomNumber(start, end));
		builder.Append(RandomString(random3, false));
		return builder.ToString();
	}
}

using System;
using System.Web;

public class SessionManager{
	public SessionManager(){
		
	}

	public static string set(string key, string value){
		HttpContext.Current.Session[key] = value;
		return value;
	}

	public static string get(string key) {
		string strValue = "";
		if (HttpContext.Current.Session[key] != null) {
			strValue = Convert.ToString(HttpContext.Current.Session[key]);
		}

		return strValue;
	}

	public static bool exists(string key) {
		return HttpContext.Current.Session[key] != null;
	}

	public static void remove(string key) {
		if (HttpContext.Current.Session[key] != null) {
			HttpContext.Current.Session.Remove(key);
		}
	}
}

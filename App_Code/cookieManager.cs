using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for cookieManager
/// </summary>
public class cookieManager{
	public cookieManager(){}

	public string set(string key, string value, int expHrs) {
		HttpCookie cookie = new HttpCookie(key);
		TimeSpan tsHours = new TimeSpan(0, expHrs, 0, 0, 0);
		cookie.Expires = DateTime.Now + tsHours;

		cookie.Value = value;
		HttpContext.Current.Response.Cookies.Add(cookie);

		return value;
	}

	public string set(string key, string value) {
		HttpCookie cookie = new HttpCookie(key);
		
		cookie.Value = value;
		HttpContext.Current.Response.Cookies.Add(cookie);

		return value;
	}

	public string get(string key) {
		HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
		if (cookie != null) {
			return cookie.Value;
		} else {
			return null;
		}
	}

	public Boolean exists(string key) {
		return HttpContext.Current.Request.Cookies[key] != null;
	}

	public void remove(string key) {
		HttpCookie cookie = HttpContext.Current.Request.Cookies[key];
		if (cookie != null) {
			cookie.Expires = DateTime.Now.AddDays(-10);
			HttpContext.Current.Response.SetCookie(cookie);
		}
	}
}

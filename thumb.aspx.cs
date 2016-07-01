using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class thumb : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		string path = System.Configuration.ConfigurationManager.AppSettings["RootPath"].ToString();
		Toolbox toolbox = new Toolbox();
		try {
			string image = Request.QueryString["i"];
			int width = toolbox.getInt(Request.QueryString["w"]);
			int height = toolbox.getInt(Request.QueryString["h"]);
			string imagePath = image.Substring(0, image.LastIndexOf("/") + 1);
			string imagename = image.Substring(image.LastIndexOf("/") + 1);
			thumbnailer thumb = new thumbnailer();
			if (height > 0) {
				thumb.imgH = height;
				thumb.imgW = 0;
				// need to calc width
			}
			if (width > 0) {
				thumb.imgW = width;
				thumb.imgH = 0;
				// need to calc height
			}
			string thumbpath = imagePath + "thumbs/" + width + "_" + height + "_" + imagename;
			string thumbFilepath = imagePath.Replace("/","\\") + "thumbs\\" + width + "_" + height + "_" + imagename;
			if (File.Exists(path + thumbFilepath)) {
				Response.Redirect(thumbpath);
				Response.End();
			} else if (File.Exists(path + imagePath.Replace("/", "\\") + imagename)) {
				thumb.saveTo = path + thumbFilepath;
				thumb.file = path + imagePath.Replace("/", "\\") + imagename;
				thumb.doResize();
				Response.Redirect(thumbpath);
				Response.End();
			} else {
				Response.Write("wtf"); //: " + HttpContext.Current.Server.MapPath("~"));
			}
		} catch (Exception ex) {
			Response.Write(ex.ToString());
			//Response.Redirect("404.aspx");
			Response.End();
		}
	}
}
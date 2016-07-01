using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class admin_add_game_image : System.Web.UI.Page {
	protected int gameID;

	protected void Page_Load(object sender, EventArgs e) {
		gamer.User user = new gamer.User();
		user.secureAdminPage();
		Toolbox toolbox = new Toolbox();
		gamer.Game parentGame = new gamer.Game();
		gameID = toolbox.getInt(Request.QueryString["g"]);
		if (!parentGame.LoadByID(gameID)) {
			Response.Write("Invalid Data Detected.");
			Response.End();
		}
	}
	protected void btnUpload_Click(object sender, EventArgs e) {
		gamer.GameScreenShot image = new gamer.GameScreenShot();
		FileHandler fileMgr = new FileHandler();
		string path = System.Configuration.ConfigurationManager.AppSettings["GameScreenShotDirectory"].ToString();
		image.filename = fileMgr.save(fileImage, path);
		thumbnailer thumb = new thumbnailer();
		thumb.imgH = 75;
		thumb.imgW = 75;
		thumb.saveTo = path + "thumbs\\75_" + image.filename;
		thumb.file = path + image.filename;
		thumb.doResize();
		image.rank = image.GetHighestRank();
		image.gameID = gameID;
		image.Add();
		litMsg.Text = "<div class=\"status\">Image Uploaded.  Add another?</div>";
	}
}

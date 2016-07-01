using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class admin_ae_game_instance : System.Web.UI.Page {
	protected int gameID;
	protected int instanceID;
	protected string path = System.Configuration.ConfigurationManager.AppSettings["GameScreenShotWebPath"].ToString();
	protected string filepath = System.Configuration.ConfigurationManager.AppSettings["GameScreenShotDirectory"].ToString();

	protected void Page_Load(object sender, EventArgs e) {
		gamer.User user = new gamer.User();
		user.secureAdminPage();
		Toolbox toolbox = new Toolbox();
		gamer.Game theGame = new gamer.Game();
		gamer.GameInstance instance = new gamer.GameInstance();
		gameID = toolbox.getInt(Request.QueryString["g"]);
		instanceID = toolbox.getInt(Request.QueryString["id"]);
		pnlItemAttachments.Visible = false;
		if (theGame.LoadByID(gameID)) {
			if (!IsPostBack) {
				gamer.Genre genre = new gamer.Genre();
				List<gamer.Genre> genreList = genre.FetchAll();
				for (int i = 0; i < genreList.Count; i++) {
					if (i == 0) {
						litGenre.Text = "\"" + genreList[i].title + "\"";
					} else {
						litGenre.Text += ",\"" + genreList[i].title + "\"";
					}
				}
				gamer.Developer dev = new gamer.Developer();
				List<gamer.Developer> devList = dev.LoadAll();
				for (int i = 0; i < devList.Count; i++) {
					if (i == 0) {
						litDev.Text = "\"" + devList[i].title + "\"";
					} else {
						litDev.Text += ",\"" + devList[i].title + "\"";
					}
				}
				gamer.Platform platform = new gamer.Platform();
				List<gamer.Platform> platformList = platform.LoadAll();
				for (int i = 0; i < platformList.Count; i++) {
					if (i == 0) {
						litPlatform.Text = "\"" + platformList[i].title + "\"";
					} else {
						litPlatform.Text += ",\"" + platformList[i].title + "\"";
					}
				}
				gamer.Publisher publisher = new gamer.Publisher();
				List<gamer.Publisher> publisherList = publisher.LoadAll();
				for (int i = 0; i < publisherList.Count; i++) {
					if (i == 0) {
						litPublisher.Text = "\"" + publisherList[i].title + "\"";
					} else {
						litPublisher.Text += ",\"" + publisherList[i].title + "\"";
					}
				}

				if (instance.LoadByID(instanceID)) {
					string thumbpath = path + "thumbs\\75_" + instance.cover;
					litCoverImage.Text = "<img src=\"" + thumbpath + "\" alt=\"Cover\" />";
					pnlItemAttachments.Visible = true;
					rptInstanceDevs.DataSource = dev.LoadGameDevelopers(instanceID);
					rptInstanceDevs.DataBind();
					rptInstanceGenres.DataSource = genre.LoadGameGenres(instanceID);
					rptInstanceGenres.DataBind();
					rptInstancePublishers.DataSource = publisher.LoadGamePublishers(instanceID);
					rptInstancePublishers.DataBind();
					platform.LoadByID(instance.platformID);
					txtPlatform.Text = platform.title;
					txtReleaseDate.Text = instance.release.ToShortDateString();
					if (instance.approved) {
						chkApproved.Checked = true;
					}
					litInstanceID.Text = instanceID.ToString();
				}
				switch (toolbox.getInt(Request.QueryString["m"])) {
					case 1:
						litMsg.Text = "<div class=\"status\">Instance Updated</div>";
						break;
					case 2:
						litMsg.Text = "<div class=\"status\">Instance Added</div>";
						break;
				}
			}
		} else {
			Response.Redirect("manage-games.aspx");
		}
	}

	protected void btnSubmit_Click(object sender, EventArgs e) {
		Toolbox toolbox = new Toolbox();
		gamer.Game theGame = new gamer.Game();
		gamer.GameInstance instance = new gamer.GameInstance();
		if (theGame.LoadByID(gameID)) {
			FileHandler fileMgr = new FileHandler();
			thumbnailer thumb = new thumbnailer();
			if (instance.LoadByID(instanceID)) {
				gamer.Platform platform = new gamer.Platform();
				instance.platformID = platform.FetchPlatformFromTitle(txtPlatform.Text);
				try {
					instance.release = Convert.ToDateTime(txtReleaseDate.Text);
				} catch {
					instance.release = Convert.ToDateTime("1/1/2200");
				}
				instance.approved = chkApproved.Checked;
				instance.cover = fileMgr.save(fileCover, filepath);
                if (instance.cover.Length > 0) {
                    thumb.imgH = 75;
                    thumb.imgW = 75;
                    thumb.saveTo = filepath + "thumbs\\75_" + instance.cover;
                    thumb.file = filepath + instance.cover;
                    thumb.doResize();
                }
				instance.Update();
				Response.Redirect("ae-game-instance.aspx?g=" + gameID + "&m=1&id=" + instance.id);
			} else {
				gamer.Platform platform = new gamer.Platform();
				instance.platformID = platform.FetchPlatformFromTitle(txtPlatform.Text);
				try {
					instance.release = Convert.ToDateTime(txtReleaseDate.Text);
				} catch {
					instance.release = Convert.ToDateTime("1/1/2200");
				}
				instance.approved = chkApproved.Checked;
				instance.gameID = gameID;
				instance.cover = fileMgr.save(fileCover, filepath);
                if (instance.cover.Length > 0) {
                    thumb.imgH = 75;
                    thumb.imgW = 75;
                    thumb.saveTo = filepath + "thumbs\\75_" + instance.cover;
                    thumb.file = filepath + instance.cover;
                    thumb.doResize();
                }
				instance.Add();
				Response.Redirect("ae-game-instance.aspx?g=" + gameID + "&m=2&id=" + instance.id);
			}
		} else {
			Response.Redirect("manage-games.aspx");
		}
	}
}

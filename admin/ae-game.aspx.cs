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

public partial class admin_ae_game : System.Web.UI.Page {
	protected int gameID;
	gamer.User user;

	protected void Page_Load(object sender, EventArgs e) {
		this.Master.pg = "admin";
		user = new gamer.User();
		user.secureAdminPage();
		Toolbox toolbox = new Toolbox();
		gamer.Game theGame = new gamer.Game();
		gameID = toolbox.getInt(Request.QueryString["id"]);
		if (theGame.LoadByID(gameID)) {
			pnlInstances.Visible = true;
			pnlAddGame.Visible = false;
			pnlEditGame.Visible = true;
			litAddInstance.Text = "<a class=\"fancy-box-iframe link-text-btn\" href='ae-game-instance.aspx?g=" + gameID + 
				"'>Add Another Platform</a>";
			litAddImage.Text = "<a class=\"fancy-box-iframe link-text-btn\" href='add-game-image.aspx?g=" + gameID +
				"'>Add Another Image/Screenshot</a>";
			if (!IsPostBack) {
				txtTitle.Text = theGame.title;
				txtNotes.Text = theGame.notes;
				if (theGame.approved) {
					chkApproved.Checked = true;
				}
				gamer.GameInstance instance = new gamer.GameInstance();
				rptInstances.DataSource = instance.LoadByGame(theGame.id);
				rptInstances.DataBind();
			}
			gamer.GameScreenShot gameImage = new gamer.GameScreenShot();
			rptImages.DataSource = gameImage.LoadByGame(gameID);
			rptImages.DataBind();
		} else {
			pnlInstances.Visible = false;
			pnlAddGame.Visible = true;
			pnlEditGame.Visible = false;
		}
		switch (toolbox.getInt(Request.QueryString["m"])) {
			case 1:
				litMsg.Text = "<div class=\"status\">Game Added.</div>";
				break;
			case 2:
				litMsg.Text = "<div class=\"status\">Game Updated.</div>";
				break;
		}
		/*
		gamer.Platform platform = new gamer.Platform();
		List<gamer.Platform> platformList = platform.LoadAll();
		for (int i = 0; i < platformList.Count; i++) {
			if (i == 0) {
				litPlatform.Text = "\"" + platformList[i].title + "\"";
			} else {
				litPlatform.Text += ",\"" + platformList[i].title + "\"";
			}
		}
		 * */
	}

	protected void btnAdd_Click(object sender, EventArgs e) {
		Toolbox toolbox = new Toolbox();
		gamer.Game theGame = new gamer.Game();
		theGame.title = txtAddTitle.Text;
		theGame.notes = txtAddNotes.Text;
		theGame.approved = chkAddApproved.Checked;
		theGame.dateAdded = DateTime.Now;
		theGame.submitter = user.id;
		theGame.Add();
		
		gamer.GameInstance instance;
		FileHandler fileMgr = new FileHandler();
		string filepath = System.Configuration.ConfigurationManager.AppSettings["GameScreenShotDirectory"].ToString();
		string coverimg = fileMgr.save(fileCover, filepath);
		thumbnailer thumb = new thumbnailer();
		if (coverimg.Length > 0) {
			thumb.imgH = 75;
			thumb.imgW = 75;
			thumb.saveTo = filepath + "thumbs\\75_" + coverimg;
			thumb.file = filepath + coverimg;
			thumb.doResize();
		}
		if (chk360.Checked) {
			instance = new gamer.GameInstance();
			instance.platformID = 9;
			try {
				instance.release = Convert.ToDateTime(txtAddReleaseDate.Text);
			} catch {
				instance.release = Convert.ToDateTime("1/1/2200");
			}
			instance.approved = chkAddApproved.Checked;
			instance.gameID = theGame.id;
			instance.cover = coverimg;
			instance.Add();
		}
		if (chk3ds.Checked) {
			instance = new gamer.GameInstance();
			instance.platformID = 28;
			try {
				instance.release = Convert.ToDateTime(txtAddReleaseDate.Text);
			} catch {
				instance.release = Convert.ToDateTime("1/1/2200");
			}
			instance.approved = chkAddApproved.Checked;
			instance.gameID = theGame.id;
			instance.cover = coverimg;
			instance.Add();
		}
		if (chkpc.Checked) {
			instance = new gamer.GameInstance();
			instance.platformID = 1;
			try {
				instance.release = Convert.ToDateTime(txtAddReleaseDate.Text);
			} catch {
				instance.release = Convert.ToDateTime("1/1/2200");
			}
			instance.approved = chkAddApproved.Checked;
			instance.gameID = theGame.id;
			instance.cover = coverimg;
			instance.Add();
		}
		if (chkps3.Checked) {
			instance = new gamer.GameInstance();
			instance.platformID = 3;
			try {
				instance.release = Convert.ToDateTime(txtAddReleaseDate.Text);
			} catch {
				instance.release = Convert.ToDateTime("1/1/2200");
			}
			instance.approved = chkAddApproved.Checked;
			instance.gameID = theGame.id;
			instance.cover = coverimg;
			instance.Add();
		}
		if (chkwii.Checked) {
			instance = new gamer.GameInstance();
			instance.platformID = 4;
			try {
				instance.release = Convert.ToDateTime(txtAddReleaseDate.Text);
			} catch {
				instance.release = Convert.ToDateTime("1/1/2200");
			}
			instance.approved = chkAddApproved.Checked;
			instance.gameID = theGame.id;
			instance.cover = coverimg;
			instance.Add();
		}
		
		Response.Redirect("ae-game.aspx?m=1&id=" + theGame.id);
	}

	protected void btnUpdateGame_Click(object sender, EventArgs e) {
		Toolbox toolbox = new Toolbox();
		gamer.Game theGame = new gamer.Game();
		if (theGame.LoadByID(gameID)) {
			theGame.title = txtTitle.Text;
			theGame.notes = txtNotes.Text;
			theGame.approved = chkApproved.Checked;
			theGame.Update();
			Response.Redirect("ae-game.aspx?m=2&id=" + gameID);
		} else {
			Response.Redirect("manage-games.aspx?m=3");
		}
	}

	protected void rptInstances_DataBound(object o, RepeaterItemEventArgs e) {
		try {
			string platformtitle;
			Literal litReleaseDate = (Literal)e.Item.FindControl("litReleaseDate");
			Literal litEditLink = (Literal)e.Item.FindControl("litEditLink");
			gamer.GameInstance instance = (gamer.GameInstance)e.Item.DataItem;
			gamer.Platform platform = new gamer.Platform();
			if (platform.LoadByID(instance.platformID)) {
				platformtitle = platform.title;
			} else {
				platformtitle = "Unidentified platform";
			}
			litReleaseDate.Text = instance.release.ToShortDateString();
			litEditLink.Text = "<a class=\"fancy-box-iframe title-link\" href=\"ae-game-instance.aspx?g=" +
				instance.gameID + "&id=" + instance.id + "\">" + platformtitle + "</a>";
		} catch (Exception ex) {
			Response.Write("<!-- " + ex.ToString() + " -->");
		}
	}

	protected void rptImages_DataBound(object o, RepeaterItemEventArgs e) {
		// unnecessary
	}
}

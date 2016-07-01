using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class games_submit_game : System.Web.UI.Page {
	protected gamer.User user = new gamer.User();
	protected Toolbox toolbox = new Toolbox();

	protected void Page_Load(object sender, EventArgs e) {
		this.Master.pg = "submit-game";
		if (user.loadFromSession()) {
			switch (toolbox.getInt(Request.QueryString["m"])) {
				case 1:
					litMsg.Text = "<div class=\"status\">Game Added.</div>";
					break;
				case 2:
					litMsg.Text = "<div class=\"status\">Game Updated.</div>";
					break;
			}
		} else {
			Response.Redirect("/account/login.aspx");
			Response.End();
		}
	}

	protected void btnAdd_Click(object sender, EventArgs e) {
		gamer.Game theGame = new gamer.Game();
		theGame.title = txtAddTitle.Text;
		theGame.notes = "";
		theGame.approved = false;
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
			instance.approved = false;
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
			instance.approved = false;
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
			instance.approved = false;
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
			instance.approved = false;
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
			instance.approved = false;
			instance.gameID = theGame.id;
			instance.cover = coverimg;
			instance.Add();
		}

		MailHandler mailer = new MailHandler();
		try {
			mailer.to = "jar155@gmail.com,curtins@gmail.com";
			mailer.subject = "New User Submitted Game.";
			mailer.body = user.username + " has submitted the following game: " + theGame.title;
			mailer.send();
			litMsg.Text = "<div class='status'>Game submitted. Thanks for helping make Gamer Theory a better site.</div>";
		} catch {
			litMsg.Text = "<div class=\"err\">There was an submitting your game, please contact " +
				"<a href=\"mailto:contact@gamertheory.com\">technical support</a>.</div>";
		}

	}
}
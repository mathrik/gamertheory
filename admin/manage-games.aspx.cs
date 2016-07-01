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

public partial class admin_manage_games : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		this.Master.pg = "admin noright games";
		gamer.User user = new gamer.User();
		Toolbox toolbox = new Toolbox();
		user.secureAdminPage();
		gamer.Game gameObject = new gamer.Game();
		rptGames.DataSource = gameObject.LoadAll();
		rptGames.DataBind();
		switch (toolbox.getInt(Request.QueryString["m"])) {
			case 1:
				litMsg.Text = "<div class=\"status\">Game Updated.</div>";
				break;
			case 2:
				litMsg.Text = "<div class=\"status\">Game Added.</div>";
				break;
			case 3:
				litMsg.Text = "<div class=\"err\">Game Not Found.</div>";
				break;
		}
	}

	protected void rptGames_ItemCommand(object source, RepeaterCommandEventArgs e) {

	}

	protected void rptGames_DataBound(object source, RepeaterItemEventArgs e) {
	}
}

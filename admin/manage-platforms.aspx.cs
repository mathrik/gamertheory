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

public partial class admin_manage_platforms : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		gamer.User user = new gamer.User();
		user.secureAdminPage();
		this.Master.pg = "admin platforms";
		Toolbox toolbox = new Toolbox();
		gamer.Platform platform = new gamer.Platform();
		rptPlatforms.DataSource = platform.LoadAll();
		rptPlatforms.DataBind();
		switch (toolbox.getInt(Request.QueryString["m"])) {
			case 1:
				litMsg.Text = "<div class=\"status\">Platform Added.</div>";
				break;
			case 2:
				litMsg.Text = "<div class=\"status\">Platform Updated.</div>";
				break;
		}
	}

	protected void rptPlatforms_ItemCommand(object source, RepeaterCommandEventArgs e) {

	}

	protected void rptPlatforms_DataBound(object source, RepeaterItemEventArgs e) {
	}
}

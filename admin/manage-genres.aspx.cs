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

public partial class admin_manage_genres : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		gamer.User user = new gamer.User();
		user.secureAdminPage();
		this.Master.pg = "admin genres";
		Toolbox toolbox = new Toolbox();
		gamer.Genre genre = new gamer.Genre();
		rptGenres.DataSource = genre.FetchAll();
		rptGenres.DataBind();
		switch (toolbox.getInt(Request.QueryString["m"])) {
			case 1:
				litMsg.Text = "<div class=\"status\">Genre Added.</div>";
				break;
			case 2:
				litMsg.Text = "<div class=\"status\">Genre Updated.</div>";
				break;
		}
	}

	protected void rptGenres_ItemCommand(object source, RepeaterCommandEventArgs e) {

	}

	protected void rptGenres_DataBound(object source, RepeaterItemEventArgs e) {
	}
}

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

public partial class admin_manage_publishers : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		gamer.User user = new gamer.User();
		user.secureAdminPage();
		this.Master.pg = "admin publishers";
		Toolbox toolbox = new Toolbox();
		gamer.Publisher publisher = new gamer.Publisher();
		rptPublishers.DataSource = publisher.LoadAll();
		rptPublishers.DataBind();
		switch (toolbox.getInt(Request.QueryString["m"])) {
			case 1:
				litMsg.Text = "<div class=\"status\">Publisher Added.</div>";
				break;
			case 2:
				litMsg.Text = "<div class=\"status\">Publisher Updated.</div>";
				break;
		}
	}

	protected void rptPublishers_ItemCommand(object source, RepeaterCommandEventArgs e) {

	}

	protected void rptPublishers_DataBound(object source, RepeaterItemEventArgs e) {
	}
}

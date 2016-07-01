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

public partial class admin_ae_developer : System.Web.UI.Page {
	protected int devID;

	protected void Page_Load(object sender, EventArgs e) {
		gamer.User user = new gamer.User();
		user.secureAdminPage();
		Toolbox toolbox = new Toolbox();
		gamer.Developer dev = new gamer.Developer();
		devID = toolbox.getInt(Request.QueryString["id"]);
		if (dev.LoadByID(devID) && !IsPostBack) {
			txtTitle.Text = dev.title;
		}
	}

	protected void btnSubmit_Click(object sender, EventArgs e) {
		gamer.Developer dev = new gamer.Developer();
		if (dev.LoadByID(devID)) {
			dev.title = txtTitle.Text;
			dev.Update();
			Response.Redirect("manage-developers.aspx?m=2");
		} else {
			dev.title = txtTitle.Text;
			dev.Add();
			Response.Redirect("manage-developers.aspx?m=1");
		}
	}
}

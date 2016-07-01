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

public partial class admin_ae_platform : System.Web.UI.Page {
	protected int platformID;

	protected void Page_Load(object sender, EventArgs e) {
		gamer.User user = new gamer.User();
		user.secureAdminPage();
		Toolbox toolbox = new Toolbox();
		gamer.Platform platform = new gamer.Platform();
		platformID = toolbox.getInt(Request.QueryString["id"]);
		if (platform.LoadByID(platformID) && !IsPostBack) {
			txtTitle.Text = platform.title;
            txtColor.Text = platform.color;
		}
	}

	protected void btnSubmit_Click(object sender, EventArgs e) {
		gamer.Platform platform = new gamer.Platform();
		if (platform.LoadByID(platformID)) {
			platform.title = txtTitle.Text;
            platform.color = txtColor.Text;
			platform.Update();
			Response.Redirect("manage-platforms.aspx?m=2");
		} else {
            platform.title = txtTitle.Text;
            platform.color = txtColor.Text;
			platform.Add();
			Response.Redirect("manage-platforms.aspx?m=1");
		}
	}
}

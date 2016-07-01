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

public partial class admin_ae_publisher : System.Web.UI.Page {
	protected int publisherID;

	protected void Page_Load(object sender, EventArgs e) {
		gamer.User user = new gamer.User();
		user.secureAdminPage();
		Toolbox toolbox = new Toolbox();
		gamer.Publisher publisher = new gamer.Publisher();
		publisherID = toolbox.getInt(Request.QueryString["id"]);
		if (publisher.LoadByID(publisherID) && !IsPostBack) {
			txtTitle.Text = publisher.title;
		}
	}

	protected void btnSubmit_Click(object sender, EventArgs e) {
		gamer.Publisher publisher = new gamer.Publisher();
		if (publisher.LoadByID(publisherID)) {
			publisher.title = txtTitle.Text;
			publisher.Update();
			Response.Redirect("manage-publishers.aspx?m=2");
		} else {
			publisher.title = txtTitle.Text;
			publisher.Add();
			Response.Redirect("manage-publishers.aspx?m=1");
		}
	}
}

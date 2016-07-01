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

public partial class account_login : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		this.Master.pg = "users";
		Toolbox toolbox = new Toolbox();
		gamer.User this_user = new gamer.User();
		if (this_user.loadFromSession()) {
			Response.Redirect("Default.aspx");
		}
		int msgID;
		msgID = toolbox.getInt(Request.QueryString["msg"]);
		switch (msgID) {
			case 1:
				litMsg.Text = "<div class=\"err\">Login failed.  <br />\n" +
					"Either your username or password was incorrect.</div>";
				break;
		}
	}

	protected void btnLogin_Click(object sender, EventArgs e) {
		gamer.User this_user = new gamer.User();
		if (this_user.ValidateUser(txtUsername.Text, txtPassword.Text)) {
			this_user.saveToSession();
			Response.Redirect("/account/Default.aspx");
		} else {
			litMsg.Text = "<div class=\"err\">Login failed.  <br />\n" +
				"Either your username or password was incorrect.</div>";
		}
	}
}

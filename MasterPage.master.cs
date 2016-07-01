using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage{
	private string _pg;
	private string _title;

	public string pg {
		get { return _pg; }
		set { _pg = value; }
	}

	public string pgtitle {
		get { return _title; }
		set { _title = value; }
	}

    protected void Page_Load(object sender, EventArgs e){
		if(pg != "") body.Attributes.Add("class", pg);
		if (pgtitle != null && pgtitle.Length > 0) { pagetitle.InnerHtml = pgtitle; } else { pagetitle.InnerHtml = "Gamer Theory - Video Game Reviews, News and Podcasts"; }
		gamer.User user = new gamer.User();
		if (user.loadFromSession()) {
			if (user.usertype > 1) {
				litAdminJS.Text = "<script type=\"text/javascript\" src=\"/js/tiny_mce/tiny_mce.js\"></script>\n";
			}
			pnlLoggedIn.Visible = true;
			pnlLoginForm.Visible = false;
			litUserWelcome.Text = "logged in as " + user.username;
			if (user.isAdmin() || user.isEditor()) {
				litAdminLink.Text = "&nbsp;|&nbsp;<a href=\"/admin/manage-stories.aspx\">Admin</a>";
			}
		} else {
			pnlLoggedIn.Visible = false;
			pnlLoginForm.Visible = true;
		}
    }

	protected void btnLogin_click(object sender, EventArgs e) {
		gamer.User this_user = new gamer.User();
		if (this_user.ValidateUser(username.Text, password.Text)) {
			this_user.saveToSession();
		} else {
			Response.Redirect("/account/login.aspx?msg=1");
		}
		pnlLoggedIn.Visible = true;
		pnlLoginForm.Visible = false;
		litUserWelcome.Text = "logged in as " + this_user.username;
		if (this_user.isAdmin() || this_user.isEditor()) {
			litAdminLink.Text = "&nbsp;|&nbsp;<a href=\"/admin/manage-stories.aspx\">Admin</a>";
		}
	}
}

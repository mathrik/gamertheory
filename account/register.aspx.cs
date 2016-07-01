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

public partial class account_register : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		this.Master.pg = "users";
	}

	protected void btnRegister_Click(object sender, EventArgs e) {
		gamer.User newUser = new gamer.User();
		bool validUser = true;
		string message = "";
		if (txtPassword.Text != txtPassword2.Text) { 
			validUser = false; 
			message += "<li>Your passwords do not match.</li>";
		}
		if (!newUser.isUniqueEmail(txtEmail.Text)) {
			validUser = false;
			message += "<li>That email is already in use.</li>";
		}
		if (!newUser.isUniqueUsername(txtUsername.Text)) {
			validUser = false;
			message += "<li>That username is already in use.</li>";
		}
		if (validUser) {
			newUser.username = txtUsername.Text;
			newUser.password = txtPassword.Text;
			newUser.email = txtEmail.Text;
			newUser.usertype = 1;
			newUser.bio = "";
			newUser.avatar = "";
			newUser.forumSig = "";
			newUser.Add();
			newUser.saveToSession();
			Response.Redirect("Default.aspx?m=1");
		} else {
			litMsg.Text = "<div class=\"err\">Your registration failed for the following reason(s):<br />" +
				"<ul>" + message + "</ul></div>";
		}
	}
}

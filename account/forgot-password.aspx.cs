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

public partial class account_forgot_password : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		//this and the other account pages could use a CAPTCHA to prevent email harvesting
	}

	protected void btnSubmit_Click(object sender, EventArgs e) {
		// make a random password, then email the user
		string email;
		MailHandler mailer = new MailHandler();
		Toolbox toolbox = new Toolbox();
		gamer.User user = new gamer.User();
		email = txtEmail.Text;
		if (user.loadByEmail(email)) {
			string newpass = toolbox.randomPassword();
			user.password = newpass;
			user.UpdatePassword(newpass);
			try {
				mailer.to = user.email;
				mailer.subject = "Your MPG site request.";
				mailer.body = "Your request for a new password (made through our forgot your password form) " +
					"has been granted. Your new password is: \n" + newpass + "\nYou can log in with it " +
					"here: http://www.gamertheory.com/account/login.aspx \nHappy gaming!";
				mailer.send();
				pnlForm.Visible = false;
				litMsg.Text = "<div class=\"status\">Your new password has been sent to your registered email address.</div>";
			} catch {
				litMsg.Text = "<div class=\"err\">There was an error sending your new password, please contact " +
					"<a href=\"mailto:contact@gamertheory.com\">technical support</a>.</div>";
			}
		} else {
			litMsg.Text = "<div class=\"err\">That email address was not found.</div>";
		}
	}
}

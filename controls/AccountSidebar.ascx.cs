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

public partial class controls_AccountSidebar : System.Web.UI.UserControl {
	protected void Page_Load(object sender, EventArgs e) {
		gamer.User user = new gamer.User();
		if (user.loadFromSession()) {
			pnlLoggedIn.Visible = true;
			pnlLoggedOut.Visible = false;
		} else {
			pnlLoggedIn.Visible = false;
			pnlLoggedOut.Visible = true;
		}
	}
}
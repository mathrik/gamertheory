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

public partial class AdminSidebar : System.Web.UI.UserControl {
	protected void Page_Load(object sender, EventArgs e) {
		gamer.User admin = new gamer.User();
		if ((admin.loadFromSession() || admin.loadFromCookie()) && (admin.isAdmin() || admin.isEditor())) {
			litNavItems.Text = "<li><a href=\"/admin/manage-genres.aspx\" class=\"genres\">Manage Genres</a></li>\n" +
				"<li><a href=\"/admin/manage-developers.aspx\" class=\"developers\">Manage Developers</a></li>\n" +
				"<li><a href=\"/admin/manage-publishers.aspx\" class=\"publishers\">Manage Publishers</a></li>\n" +
				"<li><a href=\"/admin/manage-platforms.aspx\" class=\"platforms\">Manage Platforms</a></li>\n" +
				"<li><a href=\"/admin/manage-games.aspx\" class=\"games\">Manage Games</a></li>\n" +
				"<li><a href=\"/admin/manage-users.aspx\" class=\"users\">Manage Users</a></li>\n" +
				"<li><a href=\"/admin/manage-comments.aspx\" class=\"comments\">Manage Comments</a></li>\n" +
				"<li><a href=\"/admin/manage-home-billboard.aspx\" class=\"home-billboard\">Home Billboard</a></li>\n";
		}
	}
}

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class account_friends_list : System.Web.UI.Page {
	gamer.User user = new gamer.User();

	protected void Page_Load(object sender, EventArgs e) {
		this.Master.pg = "friends";
		if (user.loadFromSession()) {
			rptFriends.DataSource = user.getFriendObjectList();
			rptFriends.DataBind();
		} else {
			Response.Redirect("login.aspx");
			Response.End();
		}
	}

	protected void rptFriends_databound(object o, RepeaterItemEventArgs e) {
		gamer.User friend = (gamer.User)e.Item.DataItem;
		Literal litThumbnail = (Literal)e.Item.FindControl("litThumbnail");
		litThumbnail.Text = "<img src=\"/thumb.aspx?i=uploads/users/" + friend.avatar + "&w=70\" alt=\"" + friend.username + "\" />";
	}
}
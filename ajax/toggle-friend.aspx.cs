using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ajax_toggle_friend : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		gamer.User user = new gamer.User();
		Toolbox tools = new Toolbox();
		int friend = tools.getInt(Request.Form["friend"]);
		if (friend > 0 && user.loadFromSession()) {
			// add to or remove from friend list
			List<int> friendList = user.getFriendList();
			if (friendList.Contains(friend)) {  // already there, so toggle means remove
				user.removeFriend(friend);
				Response.Write("success");
				Response.End();
			} else {  //add as new friend
				user.addFriend(friend);
				Response.Write("success");
				Response.End();
			}
		}
	}
}
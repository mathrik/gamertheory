﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ajax_remove_from_user_collection : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		Toolbox tools = new Toolbox();
		int objectID = tools.getInt(Request.QueryString["objectID"]);
		int objecttype = tools.getInt(Request.QueryString["objecttype"]);
		gamer.User user = new gamer.User();
		if (user.loadFromSession()) {
			gamer.Collection collection = new gamer.Collection();
			collection.RemoveItemFromCollection(user.id, objecttype, objectID);
			Response.Write("success");
			Response.End();
		}
		Response.Write("not logged in");
	}
}
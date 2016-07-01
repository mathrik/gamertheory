using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ajax_update_user_rating : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		Toolbox tools = new Toolbox();
		int objectID = tools.getInt(Request.QueryString["object"]);
		int objecttype = tools.getInt(Request.QueryString["objecttype"]);
		int ratingVal = tools.getInt(Request.QueryString["rating"]);
		gamer.User user = new gamer.User();
		if (user.loadFromSession()) {
			gamer.Rating rating = new gamer.Rating();
			if (rating.CheckExistingRanking(user.id, objecttype, objectID)) {
				rating.ratingValue = ratingVal;
				rating.Update();
			} else {
				rating.userID = user.id;
				rating.objectID = objectID;
				rating.type = objecttype;
				rating.ratingValue = ratingVal;
				rating.Add();
			}
			Response.Write("success");
			Response.End();
		}
		Response.Write("not logged in");
	}
}
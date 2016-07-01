using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ajax_approve_comment : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		gamer.User user = new gamer.User();
		user.secureAdminPage();
		Toolbox tools = new Toolbox();
		int commentID = tools.getInt(Request.QueryString["id"]);
		gamer.Comment comment = new gamer.Comment();
		if (comment.LoadByID(commentID) && comment.flagged == 1) {
			comment.flagged = 2;
			comment.Update();
			Response.Write("success");
			Response.End();
		}

		Response.Write("fail");
	}
}
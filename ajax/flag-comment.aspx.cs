using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ajax_flag_comment : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		gamer.User user = new gamer.User();
		if (user.loadFromSession()) {
			Toolbox tools = new Toolbox();
			int commentID = tools.getInt(Request.QueryString["id"]);
			gamer.Comment comment = new gamer.Comment();
			if (comment.LoadByID(commentID) && comment.flagged == 0) {
				comment.flagged = 1;
				comment.Update();
				Response.Write("success");
				Response.End();
			}
		}
		Response.Write("fail");
	}
}
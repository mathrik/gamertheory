using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ajax_fetch_comments_page : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		int objecttype;
		int objectID;
		gamer.Comment comment = new gamer.Comment(); ;
		int page;
		int count;
		Toolbox tools = new Toolbox();
		objecttype = tools.getInt(Request.QueryString["t"]);
		objectID = tools.getInt(Request.QueryString["id"]);
		page = tools.getInt(Request.QueryString["p"]);
		count = tools.getInt(Request.QueryString["c"]);
		List<gamer.Comment> mylist = comment.GetPagedComments(objectID, objecttype, count, page);
		if (mylist.Count > 0) {
			Response.Write("<table class=\"row-box-container\">");
			for (int i = 0; i < mylist.Count; i++) {
				string rowclass = "";
				if (i % 2 == 1) {
					rowclass = "odd";
				} else {
					rowclass = "";
				}
				Response.Write("<tr class=\"" + rowclass + "\"><td>" + "<img src=\"/thumb.aspx?i=uploads/users/" + mylist[i].avatar + "&w=70\" alt=\"" +
					mylist[i].username + "\" /></td>" + Environment.NewLine + 
					"<td><span class=\"title\">" + mylist[i].username + 
					"</span> <br /><em>" + mylist[i].submissionDate.ToString("M/d/yyyy h:m tt") + "</em><br />" +
					mylist[i].body + "</td>" + Environment.NewLine + "</tr>" + Environment.NewLine);
			}
			Response.Write("</table>");
		} else {
			Response.Write("No Comments");
			Response.End();
		}
	}
}
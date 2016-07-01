using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class users_Default : System.Web.UI.Page {
	protected DataSet dsCollections;
	protected DataSet dsRankings;
	protected DataSet dsComments;
	protected int numPerBlock = 5;

	protected void Page_Load(object sender, EventArgs e) {
		gamer.User user = new gamer.User();
		dsCollections = user.getUsersByLargestCollection(numPerBlock);
		dsRankings = user.getUsersByMostGamesRated(numPerBlock);
		dsComments = user.getUsersByMostComments(numPerBlock);
		rptLargestCollections.DataSource = user.FillListFromDataSet(dsCollections);
		rptLargestCollections.DataBind();
		rptGamesRated.DataSource = user.FillListFromDataSet(dsRankings);
		rptGamesRated.DataBind();
		rptCommentsMade.DataSource = user.FillListFromDataSet(dsComments);
		rptCommentsMade.DataBind();
	}


	protected void rptLargestCollections_databound(object o, RepeaterItemEventArgs e) {
		gamer.User dsUser = (gamer.User)e.Item.DataItem;
		Literal litThumbnail = (Literal)e.Item.FindControl("litThumbnail");
		Literal litUsername = (Literal)e.Item.FindControl("litUsername");
		Literal litNumber = (Literal)e.Item.FindControl("litNumber");
		bool keeplooking = true;
		for (int i = 0; dsCollections.Tables.Count > 0 && keeplooking && i < dsCollections.Tables[0].Rows.Count; i++) {
			if (dsCollections.Tables[0].Rows[i]["id"].ToString() == dsUser.id.ToString()) {
				litNumber.Text = dsCollections.Tables[0].Rows[i]["numCollection"].ToString() + " game(s)";
				keeplooking = false;
			}
		}
		litUsername.Text = dsUser.username;
		if (dsUser.avatar.Length > 0) {
			litThumbnail.Text = "<img src=\"/thumb.aspx?i=uploads/users/" + dsUser.avatar + "&w=70\" alt=\"" + dsUser.username + "\" />";
		}
	}

	protected void rptGamesRated_databound(object o, RepeaterItemEventArgs e) {
		gamer.User dsUser = (gamer.User)e.Item.DataItem;
		Literal litThumbnail = (Literal)e.Item.FindControl("litThumbnail");
		Literal litUsername = (Literal)e.Item.FindControl("litUsername");
		Literal litNumber = (Literal)e.Item.FindControl("litNumber");
		bool keeplooking = true;
		for (int i = 0; dsRankings.Tables.Count > 0 && keeplooking && i < dsRankings.Tables[0].Rows.Count; i++) {
			if (dsRankings.Tables[0].Rows[i]["id"].ToString() == dsUser.id.ToString()) {
				litNumber.Text = dsRankings.Tables[0].Rows[i]["numRankings"].ToString() + " game(s) ranked";
				keeplooking = false;
			}
		}
		litUsername.Text = dsUser.username;
		if (dsUser.avatar.Length > 0) {
			litThumbnail.Text = "<img src=\"/thumb.aspx?i=uploads/users/" + dsUser.avatar + "&w=70\" alt=\"" + dsUser.username + "\" />";
		}
	}

	protected void rptCommentsMade_databound(object o, RepeaterItemEventArgs e) {
		gamer.User dsUser = (gamer.User)e.Item.DataItem;
		Literal litThumbnail = (Literal)e.Item.FindControl("litThumbnail");
		Literal litUsername = (Literal)e.Item.FindControl("litUsername");
		Literal litNumber = (Literal)e.Item.FindControl("litNumber");
		bool keeplooking = true;
		for (int i = 0; dsComments.Tables.Count > 0 && keeplooking && i < dsComments.Tables[0].Rows.Count; i++) {
			if (dsComments.Tables[0].Rows[i]["id"].ToString() == dsUser.id.ToString()) {
				litNumber.Text = dsComments.Tables[0].Rows[i]["numComments"].ToString() + " comment(s)";
				keeplooking = false;
			}
		}
		litUsername.Text = dsUser.username;
		if (dsUser.avatar.Length > 0) {
			litThumbnail.Text = "<img src=\"/thumb.aspx?i=uploads/users/" + dsUser.avatar + "&w=70\" alt=\"" + dsUser.username + "\" />";
		}
	}
}
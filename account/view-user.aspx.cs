using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class account_view_user : System.Web.UI.Page {
	gamer.User vUser = new gamer.User();
	int vUserID;
	protected string path = System.Configuration.ConfigurationManager.AppSettings["GameScreenShotWebPath"].ToString();
	private List<gamer.Platform> platformList;
	private int numGamesToLoad = 30;
	protected gamer.Rating rating = new gamer.Rating();
	protected List<gamer.Rating> ratingList = new List<gamer.Rating>();
	protected string jsRankingArray;
	protected string jsOnload;

	protected void Page_Load(object sender, EventArgs e) {
		this.Master.pg = "account-home";
		Toolbox tools = new Toolbox();
		if (vUser.LoadByID(tools.getInt(Request.QueryString["id"]))) {
			litVUsername.Text = vUser.username;
			gamer.Comment comment = new gamer.Comment();
			rptRecentPosts.DataSource = comment.GetUserComments(vUser.id);
			rptRecentPosts.DataBind();
			imgAvatar.ImageUrl = "/thumb.aspx?i=uploads/users/" + vUser.avatar + "&w=70";

			gamer.GameInstance game = new gamer.GameInstance();
			gamer.Platform platform = new gamer.Platform();
			int page = tools.getInt(Request.QueryString["pg"]);
			if (page == 0) page = 1;
			rptGames.DataSource = game.LoadFromCollection(numGamesToLoad, page, vUser.id);
			// if user is logged in, get all game ratings for display on this page
			jsRankingArray = "";
			jsOnload = "";
			ratingList = rating.getUserRatings(vUser.id, 1);
			for (int i = 0; i < ratingList.Count; i++) {
				jsRankingArray += "RANKING_LIST[" + ratingList[i].objectID + "] = " + ratingList[i].ratingValue + ";\n";
				jsOnload += "setRating(" + ratingList[i].objectID + ");\n";
			}
			myGameRateCollect.jsRankingArray = jsRankingArray;
			myGameRateCollect.jsOnload = jsOnload;
			platformList = platform.LoadAll();
			rptGames.DataBind();

			// handle btnFriend 
			gamer.User gamer = new gamer.User();
			if (gamer.loadFromSession() && gamer.id != vUser.id) {
				// check friends list for this friend
				List<int> friendList = gamer.getFriendList();
				// name button "add to friends" or "remove from friends" according to status
				if (friendList.Contains(vUser.id)) {
					btnFriend.Text = "Remove from Friends List";
				} else {
					btnFriend.Text = "Add to Friends List";
				}
				btnFriend.OnClientClick = "toggle_friend(" + vUser.id + "); return false;";
			} else {
				btnFriend.Visible = false;
			}
		} else {
			Response.Redirect("Default.aspx");
			Response.End();
		}
	}

	protected void rptRecentPosts_databound(object o, RepeaterItemEventArgs e) {
		gamer.Comment comment = (gamer.Comment)e.Item.DataItem;
		Literal litThumbnail = (Literal)e.Item.FindControl("litThumbnail");
		Literal litUsername = (Literal)e.Item.FindControl("litUsername");
		Literal litReadMore = (Literal)e.Item.FindControl("litReadMore");
		Literal litReleaseDate = (Literal)e.Item.FindControl("litReleaseDate");
		litReleaseDate.Text = comment.submissionDate.ToString("M/d");
		litReadMore.Text = "<a class=\"read-more\" href=\"/comment-handler.aspx?id=" + comment.id + "\">READ MORE</a>";
		litUsername.Text = comment.username;
		litThumbnail.Text = "<img src=\"/thumb.aspx?i=uploads/users/" + comment.avatar + "&w=70\" alt=\"" + comment.username + "\" />";
	}

	protected void rptGames_databound(object o, RepeaterItemEventArgs e) {
		gamer.GameInstance game = (gamer.GameInstance)e.Item.DataItem;
		Literal litGameImage = (Literal)e.Item.FindControl("litGameImage");
		Literal litPlatforms = (Literal)e.Item.FindControl("litPlatforms");
		Literal litRemoveFromCollection = (Literal)e.Item.FindControl("litRemoveFromCollection");
		Literal litRanking = (Literal)e.Item.FindControl("litRanking");
		for (int j = 0; j < platformList.Count; j++) {
			if (game.platformID == platformList[j].id) {
				litPlatforms.Text += "<span style=\"color: " + platformList[j].color + "\">" + platformList[j].title + "</span> ";
			}
		}
		if (game.cover.Length > 0) {
			string thumbpath = path + "thumbs\\75_" + game.cover;
			litGameImage.Text = "<img src=\"" + thumbpath + "\" alt=\"Cover\" />";
		}

		// if a user has ranked a game, display that ranking
		int gameranking = 0;
		if (ratingList.Count > 0) {
			// find this game in the list
			litRanking.Text = "";
			for (int i = 0; i < ratingList.Count; i++) {
				if (ratingList[i].objectID == game.id) {
					// display rating
					gameranking = ratingList[i].ratingValue;
				}
			}
		}
		litRanking.Text = "<img src=\"/images/rating" + gameranking.ToString() + ".png\" alt=\"rating\"/><div style=\"display:none;\">";
	}
}
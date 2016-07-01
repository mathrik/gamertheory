using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class account_collection : System.Web.UI.Page {
	protected string path = System.Configuration.ConfigurationManager.AppSettings["GameScreenShotWebPath"].ToString();
	private List<gamer.Platform> platformList;
	private int numGamesToLoad = 30;
	protected gamer.User user = new gamer.User();
	protected gamer.Rating rating = new gamer.Rating();
	protected List<gamer.Rating> ratingList = new List<gamer.Rating>();
	protected string jsRankingArray;
	protected string jsOnload;

	protected void Page_Load(object sender, EventArgs e) {
		this.Master.pg = "collection";
		if (user.loadFromSession()) {
			gamer.GameInstance game = new gamer.GameInstance();
			gamer.Platform platform = new gamer.Platform();
			Toolbox tools = new Toolbox();
			int page = tools.getInt(Request.QueryString["pg"]);
			if (page == 0) page = 1;
			rptGames.DataSource = game.LoadFromCollection(numGamesToLoad, page, user.id);
			// if user is logged in, get all game ratings for display on this page
			jsRankingArray = "";
			jsOnload = "";
			ratingList = rating.getUserRatings(user.id, 1);
			for (int i = 0; i < ratingList.Count; i++) {
				jsRankingArray += "RANKING_LIST[" + ratingList[i].objectID + "] = " + ratingList[i].ratingValue + ";\n";
				jsOnload += "setRating(" + ratingList[i].objectID + ");\n";
			}
			myGameRateCollect.jsRankingArray = jsRankingArray;
			myGameRateCollect.jsOnload = jsOnload;
			platformList = platform.LoadAll();
			rptGames.DataBind();
		} else {
			Response.Redirect("login.aspx");
			Response.End();
		}
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
		if (user.id > 0) {
			litRanking.Text = "<div style=\"width:48; margin: 0 auto;\">";
		} else {
			litRanking.Text = "<img src=\"/images/rating0.png\" alt=\"rating\"/><div style=\"display:none;\">";
		}
		// ability to remove game from collection
		litRemoveFromCollection.Text = "<div class=\"manage-collection-buttons\" id=\"gamecollection" + game.id.ToString() + "\">" +
			"<a href=\"#\" onclick=\"removeFromCollection(" + game.id.ToString() +
			"); return false;\">Remove From Collection</a></div>";
	}
}
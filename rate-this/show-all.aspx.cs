using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class rate_this_Default : System.Web.UI.Page {
	protected string path = System.Configuration.ConfigurationManager.AppSettings["GameScreenShotWebPath"].ToString();
	private List<gamer.Platform> platformList;
	private int numGamesToLoad = 15;
	protected gamer.User user = new gamer.User();
	protected gamer.Rating rating = new gamer.Rating();
	protected List<gamer.Rating> ratingList = new List<gamer.Rating>();
	protected string jsRankingArray;
	protected string jsOnload;
	protected DataSet dsRanking;
	protected gamer.Collection collection = new gamer.Collection();
	protected List<gamer.Collection> collectionList = new List<gamer.Collection>();

	protected void Page_Load(object sender, EventArgs e) {
		user.loadFromSession();
		gamer.GameInstance game = new gamer.GameInstance();
		gamer.Platform platform = new gamer.Platform();
		Toolbox tools = new Toolbox();
		int platformID = tools.getInt(Request.QueryString["p"]);
		if (platformID == 0) {
			dsRanking = game.LoadTopRated(numGamesToLoad);
		} else {
			dsRanking = game.LoadTopRated(numGamesToLoad, platformID);
		}
		rptGames.DataSource = game.FillListFromDataSet(dsRanking);
		platformList = platform.LoadAll();
		litAllPlatformFilter.Text = "";
		List<int> commonlist = platform.CommonPlatformIDs();
		for (int j = 0; j < platformList.Count; j++) {
			litAllPlatformFilter.Text += "&nbsp;<a href=\"Default.aspx?p=" + platformList[j].id + "\" style=\"color: " + 
				platformList[j].color + "\">" + platformList[j].title + "</a> ";
			if (commonlist.Contains(platformList[j].id)) {
				litCommonPlatformFilter.Text += "&nbsp;<a href=\"Default.aspx?p=" + platformList[j].id + "\" style=\"color: " +
					platformList[j].color + "\">" + platformList[j].title + "</a> ";
			}
		}

		if (user.loadFromSession()) {
			//ratingList = rating.getUserRatings(user.id, 1);
			collectionList = collection.GetCollection(user.id, 1);
			for (int i = 0; i < ratingList.Count; i++) {
				jsRankingArray += "RANKING_LIST[" + ratingList[i].objectID + "] = " + ratingList[i].ratingValue + ";\n";
				jsOnload += "setRating(" + ratingList[i].objectID + ");\n";
			}
		}
		myGameRateCollect.jsRankingArray = jsRankingArray;
		myGameRateCollect.jsOnload = jsOnload;
		rptGames.DataBind();
	}

	protected void rptGames_databound(object o, RepeaterItemEventArgs e) {
		gamer.GameInstance game = (gamer.GameInstance)e.Item.DataItem;
		Literal litGameImage = (Literal)e.Item.FindControl("litGameImage");
		Literal litPlatforms = (Literal)e.Item.FindControl("litPlatforms");
		Literal litCollection = (Literal)e.Item.FindControl("litCollection");
		Literal litRanking = (Literal)e.Item.FindControl("litRanking");
		Literal litNumRankings = (Literal)e.Item.FindControl("litNumRankings");
		gamer.Game umbrella = new gamer.Game();
		
		for (int j = 0; j < platformList.Count; j++) {
			if (game.platformID == platformList[j].id) {
				litPlatforms.Text += "<span style=\"color: " + platformList[j].color + "\">" + platformList[j].title + "</span> ";
			}
		}
		umbrella.LoadByID(game.gameID);
		if (game.cover.Length > 0) {
			string thumbpath = "/thumb.aspx?i=uploads/games/" + game.cover + "&w=75";
			litGameImage.Text = "<img src=\"" + thumbpath + "\" alt=\"Cover\" /><span class=\"covertitle\">" + umbrella.title + "</span>";
		} else {
			string thumbpath = "/images/placeholder.gif";
			litGameImage.Text = "<img src=\"" + thumbpath + "\" alt=\"Cover\" /><span class=\"covertitle\">" + umbrella.title + "</span>";
		}

		// if a user has ranked a game, display that ranking
		int gameranking = 0;
		if (false && ratingList.Count > 0) {
			// find this game in the list
			litRanking.Text = "";
			for (int i = 0; i < ratingList.Count; i++) {
				if (ratingList[i].objectID == game.id) {
					// display rating
					gameranking = ratingList[i].ratingValue;
				}
			}
			litNumRankings.Text = "You ranked this game.";
		} else {
			litRanking.Text = "";
			Toolbox tools = new Toolbox();
			int index = 0;
			if (tools.getInt(dsRanking.Tables[0].Rows[e.Item.ItemIndex]["object_id"].ToString()) == game.id) {
				index = e.Item.ItemIndex;
			} else {
				for (int i = 0; dsRanking.Tables.Count > 0 && i < dsRanking.Tables[0].Rows.Count; i++) {
					if (tools.getInt(dsRanking.Tables[0].Rows[i]["object_id"].ToString()) == game.id) {
						index = i;
						break;
					}
				}
			}
			// display rating
			try {
				double dblRanking = Convert.ToDouble(dsRanking.Tables[0].Rows[index]["avgVal"].ToString());
				gameranking = Convert.ToInt32(Math.Round(dblRanking));
			} catch {
				gameranking = tools.getInt(dsRanking.Tables[0].Rows[index]["avgVal"].ToString());
			}
			int numRankings = tools.getInt(dsRanking.Tables[0].Rows[index]["numRankings"].ToString());
			if (numRankings == 1) {
				litNumRankings.Text = "<small>" + numRankings + " rating</small>";
			} else {
				litNumRankings.Text = "<small>" + numRankings + " ratings</small>";
			}
		}
		if (ratingList.Count > 0) {
			litRanking.Text = "<div style=\"width:48; margin: 0 auto;\">";
		} else {
			litRanking.Text = "<img src=\"/images/rating" + gameranking.ToString() + ".png\" alt=\"rating\"/><div style=\"display:none;\">";
		}
		bool ownsgame = false;
		if (collectionList.Count > 0) {
			for (int i = 0; i < collectionList.Count; i++) {
				if (collectionList[i].objectID == game.id) {
					ownsgame = true;
				}
			}
		}
		if (user.id > 0 && !ownsgame) {
			// display option to add game to collection
			litCollection.Text = "<div class=\"manage-collection-buttons\" id=\"gamecollection" + game.id.ToString() + "\">" +
				"<a href=\"#\" onclick=\"addToCollection(" + game.id.ToString() +
				"); return false;\">Add to Collection</a></div>";
		}
	}
}
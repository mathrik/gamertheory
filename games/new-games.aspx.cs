using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class games_new_games : System.Web.UI.Page {
	protected string path = System.Configuration.ConfigurationManager.AppSettings["GameScreenShotWebPath"].ToString();
    private List<gamer.Platform> platformList;
    private int numGamesToLoad = 30;
	protected gamer.User user = new gamer.User();
	protected gamer.Rating rating = new gamer.Rating();
	protected List<gamer.Rating> ratingList = new List<gamer.Rating>();
	protected DataSet defaultRatingList = new DataSet();
	protected gamer.Collection collection = new gamer.Collection();
	protected List<gamer.Collection> collectionList = new List<gamer.Collection>();
	protected string jsRankingArray;
	protected string jsOnload;
	protected int platformID;

	protected void Page_Load(object sender, EventArgs e) {
		this.Master.pg = "new-games games";
        gamer.Game game = new gamer.Game();
        gamer.Platform platform = new gamer.Platform();
        Toolbox toolbox = new Toolbox();
		platformID = toolbox.getInt(Request.QueryString["p"]);
		if (platformID > 0) {
			rptGames.DataSource = game.LoadRecentByPlatform(numGamesToLoad, platformID);
			defaultRatingList = rating.LoadRecentGamesRatingByPlatform(numGamesToLoad, platformID);
		} else {
			rptGames.DataSource = game.LoadRecent(numGamesToLoad);
			defaultRatingList = rating.LoadRecentGamesRating(numGamesToLoad);
		}
		// if user is logged in, get all game ratings for display on this page
		jsRankingArray = "";
		jsOnload = "";
		if (user.loadFromSession()) {
			ratingList = rating.getUserRatings(user.id, 1);
			collectionList = collection.GetCollection(user.id, 1);
			for (int i = 0; i < ratingList.Count; i++) {
				jsRankingArray += "RANKING_LIST[" + ratingList[i].objectID + "] = " + ratingList[i].ratingValue + ";\n";
				jsOnload += "setRating(" + ratingList[i].objectID + ");\n";
			}
		}
		myGameRateCollect.jsRankingArray = jsRankingArray;
		myGameRateCollect.jsOnload = jsOnload;
        platformList = platform.LoadAll();
        rptGames.DataBind();
	}

	protected void rptGames_databound(object o, RepeaterItemEventArgs e) {
		gamer.Game game = (gamer.Game)e.Item.DataItem;
        Literal litGameImage = (Literal)e.Item.FindControl("litGameImage");
        Literal litDateAdded = (Literal)e.Item.FindControl("litDateAdded");
        Literal litClearance = (Literal)e.Item.FindControl("litClearance");
		Repeater rptGameDetails = (Repeater)e.Item.FindControl("rptGameDetails");
		gamer.GameInstance gameDetails = new gamer.GameInstance();
		List<gamer.GameInstance> gameDetailsList = new List<gamer.GameInstance>();
		gameDetailsList = gameDetails.LoadByGame(game.id);
		rptGameDetails.DataSource = gameDetailsList;
		rptGameDetails.DataBind();
        string gamecover = "";
		for (int i = 0; i < gameDetailsList.Count; i++) {
			gameDetails = new gamer.GameInstance();
			gameDetails = gameDetailsList[i];
			if (gameDetails.cover.Length > 0 && gamecover.Length == 0) {
				gamecover = gameDetails.cover;
			}
		}
		if (gamecover.Length > 0) {
			//string thumbpath = path + "thumbs\\75_" + gamecover;\
			string thumbpath = "/thumb.aspx?i=uploads/games/" + gamecover + "&w=75";
			litGameImage.Text = "<img src=\"" + thumbpath + "\" alt=\"Cover\" />";
		}

        //litDateAdded.Text = game.dateAdded.ToString("MMM. d, yyyy");
        if ((e.Item.ItemIndex + 1) % 3 == 0) {
            litClearance.Text = "<div class=\"clearance\"></div>";
        }

    }

	protected void rptGamesDetails_databound(object o, RepeaterItemEventArgs e) {
		Literal litPlatforms = (Literal)e.Item.FindControl("litPlatforms");
		Literal litAddToCollection = (Literal)e.Item.FindControl("litAddToCollection");
		Literal litRanking = (Literal)e.Item.FindControl("litRanking");
		gamer.GameInstance game = new gamer.GameInstance();
		game = (gamer.GameInstance)e.Item.DataItem;
		if (platformID == 0 || game.platformID == platformID) {
			for (int j = 0; j < platformList.Count; j++) {
				if (game.platformID == platformList[j].id) {
					litPlatforms.Text += "<a href=\"new-games.aspx?p=" + platformList[j].id.ToString() +
						"\" style=\"color: " + platformList[j].color + "\">" + platformList[j].title + "</a> ";
				}
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
			} else {
				litRanking.Text = "";
				Toolbox tools = new Toolbox();
				for (int i = 0; defaultRatingList.Tables.Count > 0 && i < defaultRatingList.Tables[0].Rows.Count; i++) {
					if (tools.getInt(defaultRatingList.Tables[0].Rows[i]["object_id"].ToString()) == game.id) {
						// display rating
						gameranking = tools.getInt(defaultRatingList.Tables[0].Rows[i]["avgVal"].ToString());
					}
				}
			}
			if (user.id > 0) {
				litRanking.Text = "<div style=\"width:48; margin: 0 auto;\">";
			} else {
				litRanking.Text = "<img src=\"/images/rating" + gameranking.ToString() + ".png\" alt=\"rating\"/><div style=\"display:none;\">";
			}

			// if a user has not added a game to his/her collection, give that option
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
				litAddToCollection.Text = "<div class=\"manage-collection-buttons\" id=\"gamecollection" + game.id.ToString() + "\">" +
					"<a href=\"#\" onclick=\"addToCollection(" + game.id.ToString() +
					"); return false;\">Add to Collection</a></div>";
			}
		} else {
			litRanking.Text = "<script type=\"text/javascript\">" +
				"$(document).ready( function () { " +
				"	$(\"#rating" + game.id + "\").hide();" +
				"});" +
				"</script>";
		}
	}
}

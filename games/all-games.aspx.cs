using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class games_all_games : System.Web.UI.Page {
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
	protected int collectionTDwidth = 140;

	/// <summary>
	/// Any changes to this page layout should also be copied to ajax/filtered-games.aspx
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void Page_Load(object sender, EventArgs e) {
		this.Master.pg = "all-games games";
		if (!IsPostBack) {
			gamer.Game game = new gamer.Game();
			gamer.Platform platform = new gamer.Platform();
			Toolbox toolbox = new Toolbox();
			platformID = toolbox.getInt(Request.QueryString["p"]);
			int page = toolbox.getInt(Request.QueryString["pg"]);
			int totalgames = game.totalApprovedGames();
			if (page == 0) page = 1;
			if (platformID > 0) {
				rptGames.DataSource = game.LoadPagedGamesByPlatform(page, numGamesToLoad, platformID);
				defaultRatingList = rating.LoadPagedGameRatingByPlatform(page, numGamesToLoad, platformID);
			} else {
				rptGames.DataSource = game.LoadPagedGames(page, numGamesToLoad);
				defaultRatingList = rating.LoadPagedGameRating(page, numGamesToLoad);
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
			} else {
				collectionTDwidth = 80;

			}
			myGameRateCollect.jsRankingArray = jsRankingArray;
			myGameRateCollect.jsOnload = jsOnload;
			platformList = platform.LoadAll();

			rptGames.DataBind();
			if (totalgames / numGamesToLoad <= 1) {
				pnlPagingLinks.Visible = false;
			} else {
				if (page > 1) {
					litPreviousLink.Text = "<a href=\"all-games.aspx?p=" + platformID.ToString() + "&pg=" + (page - 1).ToString()
						+ "\">&lt;&lt;Previous</a>";
				}
				if (page < (totalgames / numGamesToLoad)) {
					litNextLink.Text = "<a href=\"all-games.aspx?p=" + platformID.ToString() + "&pg=" + (page + 1).ToString()
						+ "\">Next&gt;&gt;</a>";
				}
			}

			ddFilterPlatform.Items.Add(new ListItem("Select Platform", "0"));
			for (int i = 0; i < platformList.Count; i++) {
				ddFilterPlatform.Items.Add(new ListItem(platformList[i].title, platformList[i].id.ToString()));
			}
			gamer.Publisher publisher = new gamer.Publisher();
			List<gamer.Publisher> publisherList = publisher.LoadAll();
			ddFilterPublisher.Items.Add(new ListItem("Select Publisher", "0"));
			for (int i = 0; i < publisherList.Count; i++) {
				ddFilterPublisher.Items.Add(new ListItem(publisherList[i].title, publisherList[i].id.ToString()));
			}
			gamer.Genre genre = new gamer.Genre();
			List<gamer.Genre> genreList = genre.FetchAll();
			ddFilterGenre.Items.Add(new ListItem("Select Genre", "0"));
			for (int i = 0; i < genreList.Count; i++) {
				ddFilterGenre.Items.Add(new ListItem(genreList[i].title, genreList[i].id.ToString()));
			}
			ddFilterYear.Items.Add(new ListItem("Select Release Year", "0"));
			for (int i = DateTime.Now.Year; i >= 1980; i--) {
				ddFilterYear.Items.Add(new ListItem(i.ToString()));
			}
		}
	}

	protected void rptGames_databound(object o, RepeaterItemEventArgs e) {
		gamer.Game game = (gamer.Game)e.Item.DataItem;
		Literal litGameImage = (Literal)e.Item.FindControl("litGameImage");
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
			litGameImage.Text = "<img src=\"" + thumbpath + "\" alt=\"Cover\" /><span class=\"covertitle\">" + game.title + "</span>";
		} else {
			string thumbpath = "/images/placeholder.gif";
			litGameImage.Text = "<img src=\"" + thumbpath + "\" alt=\"Cover\" /><span class=\"covertitle\">" + game.title + "</span>";
		}

	}

	protected void rptGamesDetails_databound(object o, RepeaterItemEventArgs e) {
		Literal litPlatforms = (Literal)e.Item.FindControl("litPlatforms");
		Literal litDateAdded = (Literal)e.Item.FindControl("litDateAdded");
		Literal litAddToCollection = (Literal)e.Item.FindControl("litAddToCollection");
		Literal litRanking = (Literal)e.Item.FindControl("litRanking");
		gamer.GameInstance game = new gamer.GameInstance();
		game = (gamer.GameInstance)e.Item.DataItem;
		if (platformID == 0 || game.platformID == platformID) {
			for (int j = 0; j < platformList.Count; j++) {
				if (game.platformID == platformList[j].id) {
					litPlatforms.Text += "<a href=\"all-games.aspx?p=" + platformList[j].id.ToString() +
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
		litDateAdded.Text = game.release.ToString("MMM. d, yyyy");
	}

	protected void btnFilter_clicked(object o, EventArgs e) {
		gamer.Game game = new gamer.Game();
		gamer.Platform platform = new gamer.Platform();
		Toolbox toolbox = new Toolbox();
		int page = toolbox.getInt(Request.QueryString["pg"]);
		if (page == 0) page = 1;
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


		ddFilterPlatform.Items.Add(new ListItem("Select Platform", "0"));
		for (int i = 0; i < platformList.Count; i++) {
			ddFilterPlatform.Items.Add(new ListItem(platformList[i].title, platformList[i].id.ToString()));
		}
		gamer.Publisher publisher = new gamer.Publisher();
		List<gamer.Publisher> publisherList = publisher.LoadAll();
		ddFilterPublisher.Items.Add(new ListItem("Select Publisher", "0"));
		for (int i = 0; i < publisherList.Count; i++) {
			ddFilterPublisher.Items.Add(new ListItem(publisherList[i].title, publisherList[i].id.ToString()));
		}
		gamer.Genre genre = new gamer.Genre();
		List<gamer.Genre> genreList = genre.FetchAll();
		ddFilterGenre.Items.Add(new ListItem("Select Genre", "0"));
		for (int i = 0; i < genreList.Count; i++) {
			ddFilterGenre.Items.Add(new ListItem(genreList[i].title, genreList[i].id.ToString()));
		}
		ddFilterYear.Items.Add(new ListItem("Select Release Year", "0"));
		for (int i = 1980; i <= DateTime.Now.Year; i++) {
			ddFilterYear.Items.Add(new ListItem(i.ToString()));
		}

		// should be handled differently from default page load
		int genreFilter = toolbox.getInt(ddFilterGenre.SelectedValue);
		int platformFilter = toolbox.getInt(ddFilterPlatform.SelectedValue);
		int yearFilter = toolbox.getInt(ddFilterYear.SelectedValue);
		int publisherFilter = toolbox.getInt(ddFilterPublisher.SelectedValue);
		rptGames.DataSource = game.LoadPagedFilteredGames(page, numGamesToLoad, platformFilter, publisherFilter, genreFilter, yearFilter, txtFilterTitle.Text);
		defaultRatingList = rating.LoadFilteredPagedGameRating(page, numGamesToLoad, platformFilter, publisherFilter, genreFilter, yearFilter, txtFilterTitle.Text);
		rptGames.DataBind();
		int totalgames = game.totalFilteredApprovedGames(platformFilter, publisherFilter, genreFilter, yearFilter, txtFilterTitle.Text);


		if (totalgames / numGamesToLoad <= 1) {
			pnlPagingLinks.Visible = false;
		} else {
			if (page > 1) {
				litPreviousLink.Text = "<a href=\"all-games.aspx?p=" + platformID.ToString() + "&pg=" + (page - 1).ToString()
					+ "\">&lt;&lt;Previous</a>";
			}
			if (page < (totalgames / numGamesToLoad)) {
				litNextLink.Text = "<a href=\"all-games.aspx?p=" + platformID.ToString() + "&pg=" + (page + 1).ToString()
					+ "\">Next&gt;&gt;</a>";
			}
		}
	}
}
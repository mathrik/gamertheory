using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class game : System.Web.UI.Page {
	protected int gameID;
	protected string path = System.Configuration.ConfigurationManager.AppSettings["GameScreenShotWebPath"].ToString();
	protected gamer.Game mygame;
	private List<gamer.Platform> platformList;
	protected gamer.User user = new gamer.User();
	protected gamer.Rating rating = new gamer.Rating();
	protected List<gamer.Rating> ratingList = new List<gamer.Rating>();
	protected DataSet defaultRatingList = new DataSet();
	protected gamer.Collection collection = new gamer.Collection();
	protected List<gamer.Collection> collectionList = new List<gamer.Collection>();
	protected DataSet dsRanking;

	protected void Page_Load(object sender, EventArgs e) {
		this.Master.pg = "games";
		mygame = new gamer.Game();
		try {
			string friendlyurl = Request.PathInfo;
			int firstslash = friendlyurl.Substring(1).IndexOf("/");
			if (friendlyurl.Substring(firstslash + 1, 8) == "/uploads") {
				// redirect to actual file
				Response.Redirect(friendlyurl.Substring(firstslash + 1, friendlyurl.Length - firstslash - 1));
				Response.End();
			}
			Toolbox tools = new Toolbox();
			gameID = tools.getInt(friendlyurl.Substring(1, firstslash));
			//gameID = Convert.ToInt32(Request.QueryString["id"]);
		} catch {
			gameID = 0;
		}
		mygame.LoadByID(gameID);
		gamer.GameInstance gameDetails = new gamer.GameInstance();
		List<gamer.GameInstance> detailsList = gameDetails.LoadByGame(mygame.id);
		Master.pgtitle = mygame.title;
		litGameTitle.Text = mygame.title;
		litGameBody.Text = mygame.notes;
		gamer.GameScreenShot gameImage = new gamer.GameScreenShot();
		rptImages.DataSource = gameImage.LoadByGame(mygame.id);
		rptImages.DataBind();
		dsRanking = mygame.LoadInstanceRankings(mygame.id);

		gamer.Platform platform = new gamer.Platform();
		platformList = platform.LoadAll();

		gamer.Story story = new gamer.Story();
		List<gamer.Story> storyList = story.LoadRecentByGameNoUserReviews(3, mygame.id);
		if (storyList.Count > 0) {
			litStoriesList.Text = "<div class=\"subpage-legend\">Stories</div>" +
				"<div class=\"subpage-inner-container\"><ul class=\"story-list-box\">";
			for (int i = 0; i < storyList.Count; i++) {
				litStoriesList.Text += "<li>" + story.GetStoryTypeTitle(storyList[i].typeID) +
					" - <a href=\"" + storyList[i].permalink + "\">" + storyList[i].title + "</a></li>";
			}
			litStoriesList.Text += "</ul></div>";
		}
		string jsRankingArray = "";
		string jsOnload = "";
		if (user.loadFromSession()) {
			ratingList = rating.getUserRatings(user.id, 1);
			collectionList = collection.GetCollection(user.id, 1);
			for (int i = 0; i < ratingList.Count; i++) {
				jsRankingArray += "RANKING_LIST[" + ratingList[i].objectID + "] = " + ratingList[i].ratingValue + ";\n";
				jsOnload += "setRating(" + ratingList[i].objectID + ");\n";
			}
			// check if user has written a review for this game
			if (mygame.hasUserReviewed(user.id)) {
				pnlUserReview.Visible = false;
			}
		} else {
			pnlUserReview.Visible = false;
		}
		rptUserReviews.DataSource = story.LoadUserReviewsByGame(mygame.id);
		rptUserReviews.DataBind();
		if (rptUserReviews.Items.Count == 0) {
			pnlUserReviewsList.Visible = false;
		}
		myGameRateCollect.jsRankingArray = jsRankingArray;
		myGameRateCollect.jsOnload = jsOnload;
		rptGames.DataSource = detailsList;
		rptGames.DataBind();
		gamerComments.objectID = gameID;
		gamerComments.objecttype = 2;
	}

	protected void rptImages_databound(object o, RepeaterItemEventArgs e) {
		gamer.GameScreenShot gameImage = (gamer.GameScreenShot)e.Item.DataItem;
		Literal litImageTag = (Literal)e.Item.FindControl("litImageTag");
		string thumbpath = path + "thumbs\\75_" + gameImage.filename;
		string imagepath = path + gameImage.filename;
		litImageTag.Text = "<a href=\"" + imagepath + "\" class=\"fancybox-image\">";
		litImageTag.Text += "<img src=\"" + thumbpath + "\" alt=\"" + mygame.title + e.Item.ItemIndex.ToString() + "\"/></a>";
	}

	protected void rptGames_databound(object o, RepeaterItemEventArgs e) {
		gamer.GameInstance game = (gamer.GameInstance)e.Item.DataItem;
		Literal litGameImage = (Literal)e.Item.FindControl("litGameImage");
		Literal litPlatforms = (Literal)e.Item.FindControl("litPlatforms");
		Literal litCollection = (Literal)e.Item.FindControl("litCollection");
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
		} else {
			litRanking.Text = "";
			Toolbox tools = new Toolbox();
			for (int i = 0; dsRanking.Tables.Count > 0 && i < dsRanking.Tables[0].Rows.Count; i++) {
				if (tools.getInt(dsRanking.Tables[0].Rows[i]["id"].ToString()) == game.id) {
					// display rating
					try {
						double dblRanking = Convert.ToDouble(dsRanking.Tables[0].Rows[i]["avgVal"].ToString());
						gameranking = Convert.ToInt32(Math.Round(dblRanking));
					} catch {
						gameranking = tools.getInt(dsRanking.Tables[0].Rows[i]["avgVal"].ToString());
					}
				}
			}
		}
		if (user.id > 0) {
			litRanking.Text = "<div style=\"width:48; margin: 0 auto;\">";
		} else {
			litRanking.Text = "<img src=\"/images/rating" + gameranking + ".png\" alt=\"rating\"/><div style=\"display:none;\">";
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

	protected void btnUserReviewSubmit_Click(object sender, EventArgs e) {
		Toolbox tools = new Toolbox();
		FileHandler fileMgr = new FileHandler();
		gamer.Story story = new gamer.Story();
		int storyrating = tools.getInt(ddUserReviewRating.SelectedValue);
		string ratingimg;
		if (storyrating == 1) {
			ratingimg = "1star.jpg";
		} else {
			ratingimg = storyrating.ToString() + "stars.jpg";
		}
		story.title = txtUserReviewTitle.Text;
		string preview = Regex.Replace(txtUserReviewBody.Text, @"<(.|\n)*?>", string.Empty);
		if (preview.Length > 1000) {
			story.preview = preview.Substring(0, 1000);
		} else {
			story.preview = preview;
		}
		story.isfeatured = false;
		story.preview = tools.getWholeWordPreview(txtUserReviewBody.Text, 130);
		if (txtUserReviewBody.Text.Length > story.preview.Length) { story.preview += "..."; }
		story.body = tools.cleanUserSubmission(txtUserReviewBody.Text);
		story.body += "<table><tr><td>Rating:</td><td><img src=\"/uploads/images/" + ratingimg +
			"\" alt=\"" + storyrating.ToString() + "/5\" /></td></tr></table>\n";
		story.isApproved = true;
		gamer.User user = new gamer.User();
		user.loadFromSession();
		story.thumbnail = "";
		story.typeID = 5; // User Review
		story.submitter = user.id;
		story.submissionDate = DateTime.Now;
		story.eventID = 0;
		story.Add();
		if (gameID > 0) {
			story.AddGame(gameID);
		}
		//Response.Redirect("manage-stories.aspx?m=1");
		litUserReviewMsg.Text = "Review Submitted";
		//reload page
		Response.Redirect(mygame.permalink);
	}
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ajax_filtered_games : System.Web.UI.Page {
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
	/// This page prints out HTML that copies the format in games/all-games.aspx
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
	protected void Page_Load(object sender, EventArgs e) {
		gamer.Game game = new gamer.Game();
		gamer.Platform platform = new gamer.Platform();
		Toolbox toolbox = new Toolbox();
		int page = toolbox.getInt(Request.Form["pg"]);
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
		} else {
			collectionTDwidth = 80;
		}
		Response.Write("<script type=\"text/javascript\">");
		Response.Write(jsRankingArray);
		Response.Write("$(document).ready( function () { " + jsOnload + " });");
		Response.Write("</script>");
		platformList = platform.LoadAll();



		// should be handled differently from default page load
		int genreFilter = toolbox.getInt(Request.Form["genre"]);
		int platformFilter = toolbox.getInt(Request.Form["platform"]);
		int yearFilter = toolbox.getInt(Request.Form["year"]);
		int publisherFilter = toolbox.getInt(Request.Form["publisher"]);
		platformID = platformFilter;
		string title = Request.Form["title"];
		defaultRatingList = rating.LoadFilteredPagedGameRating(page, numGamesToLoad, platformFilter, publisherFilter, genreFilter, yearFilter, title);
		int totalgames = game.totalFilteredApprovedGames(platformFilter, publisherFilter, genreFilter, yearFilter, title);
		Response.Write("<table width=\"100%\" class=\"row-box-container\">");
		show_games(game.LoadPagedFilteredGames(page, numGamesToLoad, platformFilter, publisherFilter, genreFilter, yearFilter, title), platformFilter, publisherFilter, yearFilter);
		Response.Write("</table>");


		if (totalgames / numGamesToLoad <= 1) {
			// don't need paging
		} else {
			Response.Write(" <div class=\"clearance\"></div><div id=\"pnlPagingLinks\" class=\"paging-links\"> <table width=\"100%\"> <tr> <td align=\"left\">");
	
			if (page > 1) {
				Response.Write("<a href=\"all-games.aspx?p=" + platformID.ToString() + "&pg=" + (page - 1).ToString()
					+ "\" onclick=\"pageFilteredGames(" + (page - 1).ToString() + ");return false;\">&lt;&lt;Previous</a>");
			}
			Response.Write("</td> <td align=\"right\">");
			if (page < (totalgames / numGamesToLoad)) {
				Response.Write("<a href=\"all-games.aspx?p=" + platformID.ToString() + "&pg=" + (page + 1).ToString()
					+ "\" onclick=\"pageFilteredGames(" + (page + 1).ToString() + ");return false;\">Next&gt;&gt;</a>");
			}
			Response.Write("</td> </tr> </table> </div>");
		}
	}

	protected void show_games(List<gamer.Game> gamelist, int platformFilter, int publisherFilter, int yearFilter) {
		for(int i = 0; i < gamelist.Count; i++) {
			Response.Write(" <tr> " +
				"<td align=\"center\" height=\"90\" valign=\"middle\">" +
				"<a class=\"gamecover\" href=\"/games/details.aspx?id=" + gamelist[i].id.ToString() + "\">");
		
			gamer.GameInstance gameDetails = new gamer.GameInstance();
			List<gamer.GameInstance> gameDetailsList = new List<gamer.GameInstance>();
			// should filter this list by the user-requested parameters
			gameDetailsList = gameDetails.LoadFiltered(platformFilter, publisherFilter, yearFilter, gamelist[i].id);
			string gamecover = "";
			for (int k = 0; k < gameDetailsList.Count; k++) {
				gameDetails = new gamer.GameInstance();
				gameDetails = gameDetailsList[k];
				if (gameDetails.cover.Length > 0 && gamecover.Length == 0) {
					gamecover = gameDetails.cover;
				}
			}
			if (gamecover.Length > 0) {
				//string thumbpath = path + "thumbs\\75_" + gamecover;\
				string thumbpath = "/thumb.aspx?i=uploads/games/" + gamecover + "&w=75";
				Response.Write("<img src=\"" + thumbpath + "\" alt=\"Cover\" /><span class=\"covertitle\">" + gamelist[i].title + "</span>");
			} else {
				string thumbpath = "/images/placeholder.gif";
				Response.Write("<img src=\"" + thumbpath + "\" alt=\"Cover\" /><span class=\"covertitle\">" + gamelist[i].title + "</span>");
			}
			Response.Write("</a> </td> <td align=\"left\"> <table class=\"row-box-container games-ranking-row\" width=\"100%\">");
			show_game_instances(gameDetailsList);

			Response.Write("</table> <div class=\"clearance\"></div> </td> </tr>");
		}
	}

	protected void show_game_instances(List<gamer.GameInstance> gamelist) {
		for (int i = 0; i < gamelist.Count; i++) {
			Response.Write("<tr> <td align=\"right\" style=\"padding-right: 10px;\">");
			if (platformID == 0 || gamelist[i].platformID == platformID) {
				for (int j = 0; j < platformList.Count; j++) {
					if (gamelist[i].platformID == platformList[j].id) {
						Response.Write("<a href=\"all-games.aspx?p=" + platformList[j].id.ToString() +
							"\" style=\"color: " + platformList[j].color + "\">" + platformList[j].title + "</a> ");
					}
				}
				Response.Write("</td> <td width=\"45\">");
				int gameranking = 0;
				if (ratingList.Count > 0) {
					// find this game in the list
					for (int k = 0; k < ratingList.Count; k++) {
						if (ratingList[k].objectID == gamelist[i].id) {
							// display rating
							gameranking = ratingList[k].ratingValue;
						}
					}
				} else {
					Toolbox tools = new Toolbox();
					for (int k = 0; defaultRatingList.Tables.Count > 0 && k < defaultRatingList.Tables[0].Rows.Count; k++) {
						if (tools.getInt(defaultRatingList.Tables[0].Rows[k]["object_id"].ToString()) == gamelist[i].id) {
							// display rating
							gameranking = tools.getInt(defaultRatingList.Tables[0].Rows[k]["avgVal"].ToString());
						}
					}
				}
				if (user.id > 0) {
					Response.Write("<div style=\"width:48; margin: 0 auto;\">");
				} else {
					Response.Write("<img src=\"/images/rating" + gameranking.ToString() + ".png\" alt=\"rating\"/><div style=\"display:none;\">");
				}
			} else {
				Response.Write("</td> <td width=\"45\">");
				Response.Write("<script type=\"text/javascript\">" +
					"$(document).ready( function () { " +
					"	$(\"#rating" + gamelist[i].id + "\").hide();" +
					"});" +
					"</script>");
			}
			Response.Write("<div id=\"rating" + gamelist[i].id + "\" class=\"rating-stars\">" +
				"<table> <tr> " +
				"<td><a href=\"javascript:void(0)\" onclick=\"addRating(1," + gamelist[i].id + ")\" onmouseover=\"previewRating(1," + gamelist[i].id + ")\" onmouseout=\"setRating(" + gamelist[i].id + ")\"><img src=\"/images/spacer.gif\" width=\"9\" height=\"9\" alt=\"\" /></a></td>" +
				"<td><a href=\"javascript:void(0)\" onclick=\"addRating(2," + gamelist[i].id + ")\" onmouseover=\"previewRating(2," + gamelist[i].id + ")\" onmouseout=\"setRating(" + gamelist[i].id + ")\"><img src=\"/images/spacer.gif\" width=\"9\" height=\"9\" alt=\"\" /></a></td>" +
				"<td><a href=\"javascript:void(0)\" onclick=\"addRating(3," + gamelist[i].id + ")\" onmouseover=\"previewRating(3," + gamelist[i].id + ")\" onmouseout=\"setRating(" + gamelist[i].id + ")\"><img src=\"/images/spacer.gif\" width=\"9\" height=\"9\" alt=\"\" /></a></td>" +
				"<td><a href=\"javascript:void(0)\" onclick=\"addRating(4," + gamelist[i].id + ")\" onmouseover=\"previewRating(4," + gamelist[i].id + ")\" onmouseout=\"setRating(" + gamelist[i].id + ")\"><img src=\"/images/spacer.gif\" width=\"9\" height=\"9\" alt=\"\" /></a></td>" +
				"<td><a href=\"javascript:void(0)\" onclick=\"addRating(5," + gamelist[i].id + ")\" onmouseover=\"previewRating(5," + gamelist[i].id + ")\" onmouseout=\"setRating(" + gamelist[i].id + ")\"><img src=\"/images/spacer.gif\" width=\"9\" height=\"9\" alt=\"\" /></a></td>" +
				"</tr> </table> </div> </div> </td> <td align=\"center\" width=\"" + collectionTDwidth + "\">");
			if (platformID == 0 || gamelist[i].platformID == platformID) {
				bool ownsgame = false;
				if (collectionList.Count > 0) {
					for (int k = 0; k < collectionList.Count; k++) {
						if (collectionList[k].objectID == gamelist[i].id) {
							ownsgame = true;
						}
					}
				}
				if (user.id > 0 && !ownsgame) {
					// display option to add game to collection
					Response.Write("<div class=\"manage-collection-buttons\" id=\"gamecollection" + gamelist[i].id.ToString() + "\">" +
						"<a href=\"#\" onclick=\"addToCollection(" + gamelist[i].id.ToString() +
						"); return false;\">Add to Collection</a></div>");
				}
			}
			Response.Write(" </td> <td align=\"center\" width=\"105\">" +
				gamelist[i].release.ToString("MMM. d, yyyy") + "</td> </tr> ");
		}
	}
}
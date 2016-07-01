using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class admin_search_games : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		gamer.User user = new gamer.User();
		user.secureUserPage();
		string query;
		try {
			query = Request.QueryString["q"];
		} catch {
			query = "";
		}
		gamer.Game game = new gamer.Game();
		List<gamer.Game> gameList = new List<gamer.Game>();
		gameList = game.AutocompleteByTitle(query);
		string output = "";
		for (int i = 0; i < gameList.Count; i++) {
			output += gameList[i].title + Environment.NewLine;
		}
		Response.Write(output);
	}
}
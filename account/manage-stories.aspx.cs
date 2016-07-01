using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class account_manage_stories : System.Web.UI.Page {
	protected int storytype;

    protected void Page_Load(object sender, EventArgs e) {
        gamer.User user = new gamer.User();
        user.secureUserPage();
        if (user.isAdmin() || user.isEditor()) {
            Response.Redirect("/admin/manage-stories.aspx");
            Response.End();
        } // else load this user's reviews
		this.Master.pg = "account stories";
		Toolbox toolbox = new Toolbox();
		gamer.Story story = new gamer.Story();
		storytype = toolbox.getInt(Request.QueryString["t"]);
		if (storytype > 0) {
			rptGames.DataSource = story.GetByStoryType_User(storytype, user.id);
			rptGames.DataBind();
		} else {
			rptGames.DataSource = story.LoadStoriesMultipleTypes_User(user.getStoryTypes(), user.id);
			rptGames.DataBind();
		}
		switch (toolbox.getInt(Request.QueryString["m"])) {
			case 1:
				litMsg.Text = "<div class=\"status\">Story Added.</div>";
				break;
			case 2:
				litMsg.Text = "<div class=\"status\">Story Updated.</div>";
				break;
		}
	}

	protected void rptGames_ItemCommand(object source, RepeaterCommandEventArgs e) {

	}

	protected void rptGames_DataBound(object source, RepeaterItemEventArgs e) {
	}
}
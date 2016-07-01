using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class admin_manage_stories : System.Web.UI.Page {
	protected int storytype;

	protected void Page_Load(object sender, EventArgs e) {
		this.Master.pg = "admin noright stories";
		gamer.User user = new gamer.User();
		Toolbox toolbox = new Toolbox();
		gamer.Story story = new gamer.Story();
		user.secureUserPage();
		ListItemCollection storyTypeList = story.GetStoryTypeList(user.getStoryTypes());
		rptTabs.DataSource = storyTypeList;
		rptTabs.DataBind();
		for (int i = 0; i < storyTypeList.Count; i++) {
			ddStoryType.Items.Add(storyTypeList[i]);
		}
		storytype = toolbox.getInt(Request.QueryString["t"]);
		if (user.canEditOtherStories()) {
			if (storytype > 0) {
				rptGames.DataSource = story.GetByStoryType(storytype);
				rptGames.DataBind();
			} else {
				rptGames.DataSource = story.LoadStoriesMultipleTypes(user.getStoryTypes());
				rptGames.DataBind();
			}
		} else {
			if (storytype > 0) {
				rptGames.DataSource = story.GetByStoryType_User(storytype, user.id);
				rptGames.DataBind();
			} else {
				rptGames.DataSource = story.LoadStoriesMultipleTypes_User(user.getStoryTypes(), user.id);
				rptGames.DataBind();
			}
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

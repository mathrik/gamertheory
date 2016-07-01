using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class archives_Default : System.Web.UI.Page {
	protected int numperpage = 25;

	protected void Page_Load(object sender, EventArgs e) {
		Master.pg = "archives";
		Toolbox toolbox = new Toolbox();
		gamer.Story story = new gamer.Story();
		int page = toolbox.getInt(Request.QueryString["p"]);
		if (page == 0) { page = 1; }
		int totalstories = story.GetTotalStaffStories();
		if ((Convert.ToDouble(totalstories) / Convert.ToDouble(numperpage)) > Convert.ToDouble(page)) {
			litRightPaging.Text = "<a href='Default.aspx?p=" + (page + 1).ToString() + "'>Older Stories&gt;&gt;</a>";
		}
		if (page > 1 && (totalstories / numperpage) > 1) {
			litLeftPaging.Text = "<a href='Default.aspx?p=" + (page - 1).ToString() + "'>&lt;&lt;Newer Stories</a>";
		}
		if (litLeftPaging.Text.Length == 0 && litRightPaging.Text.Length == 0) { pnlPagingContainer.Visible = false; }
		rptUpdates.DataSource = story.GetPagedStaffStories(numperpage, page);
		rptUpdates.DataBind();
	}

	protected void rptUpdates_databound(object o, RepeaterItemEventArgs e) {
		gamer.Story story = (gamer.Story)e.Item.DataItem;
		Literal litThumbnail = (Literal)e.Item.FindControl("litThumbnail");
		if (story.thumbnail.Length > 0) {
			string thumbnailWebPath = System.Configuration.ConfigurationManager.AppSettings["StoryThumbWebPath"].ToString();
			litThumbnail.Text = "<img src=\"/thumb.aspx?i=" + thumbnailWebPath + story.thumbnail + "&w=140\" alt=\"" + story.title + "\" />";
		}
	}
}
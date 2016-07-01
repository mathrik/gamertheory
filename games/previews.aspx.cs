using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class games_previews : System.Web.UI.Page {
    protected string pagesetting = System.Configuration.ConfigurationManager.AppSettings["NumArticlesPerPage"].ToString();
    private int storytypeID = 1;

	protected void Page_Load(object sender, EventArgs e) {
		Master.pg = "previews";
		Toolbox toolbox = new Toolbox();
        int numperpage = toolbox.getInt(pagesetting);
		gamer.Story story = new gamer.Story();
        int page = toolbox.getInt(Request.QueryString["p"]);
        if (page == 0) { page = 1; }
        int totalstories = story.GetTotalStoriesByType(storytypeID);
        if ((Convert.ToDouble(totalstories) / Convert.ToDouble(numperpage)) > Convert.ToDouble(page)) {
            litRightPaging.Text = "<a href='/games/previews.aspx?p=" + (page + 1).ToString() + "'>Older Stories&gt;&gt;</a>";
        }
        if (page > 1 && (Convert.ToDouble(totalstories) / Convert.ToDouble(numperpage)) > 1) {
            litLeftPaging.Text = "<a href='/games/previews.aspx?p=" + (page - 1).ToString() + "'>&lt;&lt;Newer Stories</a>";
        }
        if (litLeftPaging.Text.Length == 0 && litRightPaging.Text.Length == 0) { pnlPagingContainer.Visible = false; }
        rptUpdates.DataSource = story.GetPagedStoriesByType(numperpage, page, storytypeID);
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
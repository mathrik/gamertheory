using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class games_user_reviews : System.Web.UI.Page {
    protected string pagesetting = System.Configuration.ConfigurationManager.AppSettings["NumArticlesPerPage"].ToString();
    private int storytypeID = 5;

	protected void Page_Load(object sender, EventArgs e) {
		Master.pg = "user-reviews";
		Toolbox toolbox = new Toolbox();
        gamer.Story story = new gamer.Story();
        int numperpage = toolbox.getInt(pagesetting);
        int page = toolbox.getInt(Request.QueryString["p"]);
        if (page == 0) { page = 1; }
        int totalstories = story.GetTotalStoriesByType(storytypeID);
        bool showpaging = (Convert.ToDouble(totalstories) / Convert.ToDouble(numperpage)) > Convert.ToDouble(page);
        if (showpaging) {
            litRightPaging.Text = "<a href='/story/editorials.aspx?p=" + (page + 1).ToString() + "'>Older Stories&gt;&gt;</a>";
        }
        if (page > 1 && (Convert.ToDouble(totalstories) / Convert.ToDouble(numperpage)) > 1) {
            litLeftPaging.Text = "<a href='/story/editorials.aspx?p=" + (page - 1).ToString() + "'>&lt;&lt;Newer Stories</a>";
        }
        if (litLeftPaging.Text.Length == 0 && litRightPaging.Text.Length == 0) { pnlPagingContainer.Visible = false; }
        rptUpdates.DataSource = story.GetPagedStoriesByType(numperpage, page, storytypeID);
		rptUpdates.DataBind();
	}

	protected void rptUpdates_databound(object o, RepeaterItemEventArgs e) {
		gamer.Story story = (gamer.Story)e.Item.DataItem;
		gamer.User myuser = new gamer.User();
		myuser.LoadByID(story.submitter);
		Literal litThumbnail = (Literal)e.Item.FindControl("litThumbnail");
		if (myuser.avatar.Length > 0) {
			litThumbnail.Text = "<img src=\"/thumb.aspx?i=uploads/users/" + myuser.avatar + "&w=140\" alt=\"" + story.title + "\" />";
		}
	}
}
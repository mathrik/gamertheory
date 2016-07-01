using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class admin_ae_story : System.Web.UI.Page {
	protected int storyID;
	private int storyType;
	private int gameID;
	private string thumbnailWebPath;
	private string thumbnailFilePath;

	protected void Page_Load(object sender, EventArgs e) {
		thumbnailWebPath = System.Configuration.ConfigurationManager.AppSettings["StoryThumbWebPath"].ToString();
		gamer.User user = new gamer.User();
		user.secureUserPage();
		switch (user.usertype) {
			case 1:
			case 2:
			case 3:
				lblFeatured.Visible = false;
				chkFeatured.Visible = false;
				ddPublish.Visible = false;
				break;
		}
		Toolbox toolbox = new Toolbox();
		gamer.Story story = new gamer.Story();
		storyID = toolbox.getInt(Request.QueryString["id"]);
		gameID = toolbox.getInt(Request.QueryString["g"]);
		litStoryID.Text = storyID.ToString();
		storyType = toolbox.getInt(Request.QueryString["t"]);
		if (!story.isPodcast(storyType)) {
			lblPodcastFile.Visible = false;
			filePodcast.Visible = false;
		}
		// check what permissions a user has for story types
		if (!user.hasStoryType(storyType)) {
			Response.Redirect("/fail.aspx");
			Response.End();
		}
		story.id = storyID;
		if (story.LoadByID()) {
			litStoryThumbnail.Text = "<img src=\"/thumb.aspx?i=" + thumbnailWebPath + story.thumbnail + "&w=125\" alt=\"" + story.title + "\" />";
			//pnlItemAttachments.Visible = false;
			if (story.isApproved) {
				ddPublish.Items[1].Selected = true;
			}
			gamer.Genre genre = new gamer.Genre();
			List<gamer.Genre> genreList = genre.FetchAll();
			for (int i = 0; i < genreList.Count; i++) {
				if (i == 0) {
					litGenre.Text = "\"" + genreList[i].title + "\"";
				} else {
					litGenre.Text += ",\"" + genreList[i].title + "\"";
				}
			}
			gamer.Developer dev = new gamer.Developer();
			List<gamer.Developer> devList = dev.LoadAll();
			for (int i = 0; i < devList.Count; i++) {
				if (i == 0) {
					litDev.Text = "\"" + devList[i].title + "\"";
				} else {
					litDev.Text += ",\"" + devList[i].title + "\"";
				}
			}
			//gamer.Game game = new gamer.Game();
			//game.LoadAll();
			gamer.Platform platform = new gamer.Platform();
			List<gamer.Platform> platformList = platform.LoadAll();
			for (int i = 0; i < platformList.Count; i++) {
				if (i == 0) {
					litPlatform.Text = "\"" + platformList[i].title + "\"";
				} else {
					litPlatform.Text += ",\"" + platformList[i].title + "\"";
				}
			}
			gamer.Publisher publisher = new gamer.Publisher();
			List<gamer.Publisher> publisherList = publisher.LoadAll();
			for (int i = 0; i < publisherList.Count; i++) {
				if (i == 0) {
					litPublisher.Text = "\"" + publisherList[i].title + "\"";
				} else {
					litPublisher.Text += ",\"" + publisherList[i].title + "\"";
				}
			}
			if (!IsPostBack) {
				if (story.submitter != user.id && !user.canEditOtherStories()) { // not an editor or admin
					Response.Redirect("/404.aspx");
					Response.End();
				}
				txtTitle.Text = story.title;
				txtStory.Text = story.body;
				txtPreview.Text = story.preview;
				if (story.isfeatured) {
					chkFeatured.Checked = true;
				}
				rptStoryDevs.DataSource = dev.LoadStoryDevelopers(story.id);
				rptStoryDevs.DataBind();
				rptStoryGenres.DataSource = genre.LoadStoryGenres(story.id);
				rptStoryGenres.DataBind();
				rptStoryPlatforms.DataSource = platform.LoadStoryPlatforms(story.id);
				rptStoryPlatforms.DataBind();
				rptStoryPublishers.DataSource = publisher.LoadStoryPublishers(story.id);
				rptStoryPublishers.DataBind();
				gamer.Game theGame = new gamer.Game();
				rptStoryGames.DataSource = theGame.LoadStoryGames(story.id);
				rptStoryGames.DataBind();
			}
		} else {
			pnlItemAttachments.Visible = false;
		}
	}
	protected void btnSubmit_Click(object sender, EventArgs e) {
		thumbnailFilePath = System.Configuration.ConfigurationManager.AppSettings["StoryThumbDirectory"].ToString();
		Toolbox tools = new Toolbox();
		FileHandler fileMgr = new FileHandler();
		gamer.Story story = new gamer.Story();
		story.id = storyID;
		story.LoadByID();
		if (fileStoryThumbnail.FileName.Length > 0) {
			story.thumbnail = fileMgr.save(fileStoryThumbnail, thumbnailFilePath);
		}
		story.title = txtTitle.Text;
		string preview = Regex.Replace(txtPreview.Text, @"<(.|\n)*?>", string.Empty);
		if (preview.Length > 1000) {
			story.preview = preview.Substring(0, 1000);
		} else {
			story.preview = preview;
		}
		story.isfeatured = chkFeatured.Checked;
		story.body = txtStory.Text;
		string podcastfile = "";
		string podcastFilePath = System.Configuration.ConfigurationManager.AppSettings["PodcastDirectory"].ToString();
		if (story.isPodcast(storyType)) {
			podcastfile = fileMgr.save(filePodcast, podcastFilePath);
		}
		try {
			story.isApproved = ddPublish.SelectedValue == "1";
		} catch {
			story.isApproved = false;
		}
		if (story.id < 1) {
			gamer.User user = new gamer.User();
			if (!(user.loadFromSession())) {
				user.loadFromCookie();
			}
			if (fileStoryThumbnail.FileName.Length == 0) {
				story.thumbnail = "";
			}
			story.typeID = storyType;
			story.submitter = user.id;
			story.submissionDate = DateTime.Now;
			story.eventID = 0;
			story.Add();
			if (story.isPodcast(story.typeID) && podcastfile.Length > 0) {
				story.AttachPodcastFile(story.id, podcastfile);
			}
			if (gameID > 0) {
				story.AddGame(gameID);
			}
			Response.Redirect("manage-stories.aspx?m=1");
		} else {
			story.Update();
			if (story.isPodcast(story.typeID) && podcastfile.Length > 0) {
				string oldpodcast = story.GetPodcastFile(story.id);
				if (oldpodcast.Length > 0) {
					fileMgr.delete(podcastFilePath + oldpodcast);
				}
				story.AttachPodcastFile(story.id, podcastfile);
			}
			if (gameID > 0) {
				story.AddGame(gameID);
			}
			Response.Redirect("manage-stories.aspx?m=2");
		}
	}
}

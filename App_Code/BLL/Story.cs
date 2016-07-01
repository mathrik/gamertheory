using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for Story
/// </summary>
namespace gamer {
	public class Story {
		private int _id;
		private string _title;
		private string _body;
		private int _typeID;
		private bool _isfeatured;
		private int _eventID;
		private int _submitter;
		private bool _isApproved;
		private DateTime _submissionDate;
		private string _preview;
		private string _thumbnail;

		public int id {
			get { return _id; }
			set { _id = value; }
		}

		public string title {
			get { return _title; }
			set { _title = value; }
		}

		public string body {
			get { return _body; }
			set { _body = value; }
		}

		/// <summary>
		///1	Game Preview
		///2	Game Review
		///3	Editorial
		///4	New Game Announcement/Press Release
		///5	User Review
		///6	General Gaming News
		///7	Podcast
		/// </summary>
		public int typeID {
			get { return _typeID; }
			set { _typeID = value; }
		}

		public bool isfeatured {
			get { return _isfeatured; }
			set { _isfeatured = value; }
		}

		public int eventID {
			get { return _eventID; }
			set { _eventID = value; }
		}

		public int submitter {
			get { return _submitter; }
			set { _submitter = value; }
		}


		public bool isApproved {
			get { return _isApproved; }
			set { _isApproved = value; }
		}

		public DateTime submissionDate {
			get { return _submissionDate; }
			set { _submissionDate = value; }
		}

		public string preview {
			get { return _preview; }
			set { _preview = value; }
		}

		public string thumbnail {
			get { return _thumbnail; } 
			set { _thumbnail = value; }
		}

		public string seotitle {
			get {
				Regex rgx = new Regex("[^a-zA-Z0-9 -]");
				return HttpContext.Current.Server.UrlEncode(rgx.Replace(this.title,""));
			}
		}

		public string permalink {
			get { return "/story.aspx/" + this.id.ToString() + "/" + this.seotitle + "/"; }
		}

		public Story() {
			//
			// TODO: Add constructor logic here
			//
		}

		public void Add() {
			StoryDAL storyDAL = new StoryDAL();
			this.id = storyDAL.Add(this.title,
				this.body,
				this.typeID,
				Convert.ToInt32(this.isfeatured),
				this.eventID,
				this.submitter,
				Convert.ToInt32(this.isApproved),
				this.submissionDate,
				this.preview,
				this.thumbnail);
		}

		public void Update() {
			StoryDAL storyDAL = new StoryDAL();
			storyDAL.Update(this.id,
				this.title,
				this.body,
				this.typeID,
				Convert.ToInt32(this.isfeatured),
				this.eventID,
				this.submitter,
				Convert.ToInt32(this.isApproved),
				this.submissionDate,
				this.preview,
				this.thumbnail);
		}

		public void Delete() {
			StoryDAL storyDAL = new StoryDAL();
			FileHandler fileMgr = new FileHandler();
			string fileLoc;
			fileLoc = System.Configuration.ConfigurationManager.AppSettings["StoryThumbDirectory"].ToString();  
			fileMgr.delete(fileLoc + this.thumbnail);
			if (isPodcast(this.typeID)) {
				fileLoc = System.Configuration.ConfigurationManager.AppSettings["PodcastDirectory"].ToString();
				string podcastfile = this.GetPodcastFile(this.id);
				if (podcastfile.Length > 0) {
					fileMgr.delete(fileLoc + podcastfile);
				}
			}
			storyDAL.Delete(this.id);
		}

		protected List<Story> FillListFromDataSet(DataSet ds) {
			List<Story> myList = new List<Story>();
			Toolbox toolbox = new Toolbox();
			for (int i = 0; ds.Tables.Count > 0 && i < ds.Tables[0].Rows.Count; i++) {
				Story this_item = new Story();
				this_item.id = toolbox.getInt(ds.Tables[0].Rows[i]["id"].ToString());
				this_item.title = ds.Tables[0].Rows[i]["title"].ToString();
				this_item.body = ds.Tables[0].Rows[i]["body"].ToString();
				this_item.typeID = toolbox.getInt(ds.Tables[0].Rows[i]["storytype_id"].ToString());
				this_item.isfeatured = ds.Tables[0].Rows[i]["isfeatured"].ToString() == "1";
				this_item.eventID = toolbox.getInt(ds.Tables[0].Rows[i]["event_id"].ToString());
				this_item.submitter = toolbox.getInt(ds.Tables[0].Rows[i]["submitter_id"].ToString());
				this_item.isApproved = ds.Tables[0].Rows[i]["isApproved"].ToString() == "1";
				try {
					this_item.submissionDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["submission_date"].ToString());
				} catch {
					this_item.submissionDate = Convert.ToDateTime("1/1/1900");
				}
				this_item.preview = ds.Tables[0].Rows[i]["preview"].ToString();
				this_item.thumbnail = ds.Tables[0].Rows[i]["thumbnail"].ToString();
				myList.Add(this_item);
			}
			return myList;
		}

		public bool LoadByID() {
			StoryDAL storyDAL = new StoryDAL();
			Toolbox toolbox = new Toolbox();
			DataSet ds = storyDAL.LoadByID(this.id);
			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
				this.id = toolbox.getInt(ds.Tables[0].Rows[0]["id"].ToString());
				this.title = ds.Tables[0].Rows[0]["title"].ToString();
				this.body = ds.Tables[0].Rows[0]["body"].ToString();
				this.typeID = toolbox.getInt(ds.Tables[0].Rows[0]["storytype_id"].ToString());
				this.isfeatured = ds.Tables[0].Rows[0]["isfeatured"].ToString() == "1";
				this.eventID = toolbox.getInt(ds.Tables[0].Rows[0]["event_id"].ToString());
				this.submitter = toolbox.getInt(ds.Tables[0].Rows[0]["submitter_id"].ToString());
				this.isApproved = ds.Tables[0].Rows[0]["isApproved"].ToString() == "1";
				try {
					this.submissionDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["submission_date"].ToString());
				} catch {
					this.submissionDate = Convert.ToDateTime("1/1/1900");
				}
				this.preview = ds.Tables[0].Rows[0]["preview"].ToString();
				this.thumbnail = ds.Tables[0].Rows[0]["thumbnail"].ToString();
				return true;
			} else {
				return false;
			}
		}

		public List<Story> GetFeatured() {
			StoryDAL storyDAL = new StoryDAL();
			return FillListFromDataSet(storyDAL.LoadByFeatured());
		}

		public List<Story> GetFeatured(bool isApproved) {
			StoryDAL storyDAL = new StoryDAL();
			return FillListFromDataSet(storyDAL.LoadByFeatured(Convert.ToInt32(isApproved)));
		}

		public List<Story> GetByStoryType(int storytype) {
			StoryDAL sDAL = new StoryDAL();
			return FillListFromDataSet(sDAL.LoadByStoryType(storytype, 0));
		}

		/// <summary>
		/// fetch maxqty amount of stories by story type, ordered by submission date (most recent first)
		/// </summary>
		/// <param name="storytype"></param>
		/// <param name="maxqty"></param>
		/// <returns></returns>
		public List<Story> GetByStoryType(int storytype, int maxqty) {
			StoryDAL sDAL = new StoryDAL();
			return FillListFromDataSet(sDAL.LoadByStoryType(storytype, maxqty));
		}

		public List<Story> LoadStoriesMultipleTypes(List<int> mylist) {
			StoryDAL sDAL = new StoryDAL();
			return FillListFromDataSet(sDAL.LoadStoriesMultipleTypes(mylist, 0));
		}

		/// <summary>
		/// fetch maxqty amount of stories that match the given story types, 
		/// ordered by submission date (most recent first)
		/// </summary>
		/// <param name="mylist"></param>
		/// <param name="maxqty"></param>
		/// <returns></returns>
		public List<Story> LoadStoriesMultipleTypes(List<int> mylist, int maxqty) {
			StoryDAL sDAL = new StoryDAL();
			return FillListFromDataSet(sDAL.LoadStoriesMultipleTypes(mylist, maxqty));
		}

		public List<Story> GetByStoryType_User(int storytype, int userID) {
			StoryDAL sDAL = new StoryDAL();
			return FillListFromDataSet(sDAL.LoadByStoryType_User(storytype, userID, 0));
		}

		/// <summary>
		/// fetch maxqty amount of stories by story type, ordered by submission date (most recent first)
		/// </summary>
		/// <param name="storytype"></param>
		/// <param name="maxqty"></param>
		/// <returns></returns>
		public List<Story> GetByStoryType_User(int storytype, int userID, int maxqty) {
			StoryDAL sDAL = new StoryDAL();
			return FillListFromDataSet(sDAL.LoadByStoryType_User(storytype, userID, maxqty));
		}

		public List<Story> LoadStoriesMultipleTypes_User(List<int> mylist, int userID) {
			StoryDAL sDAL = new StoryDAL();
			return FillListFromDataSet(sDAL.LoadStoriesMultipleTypes_User(mylist, userID, 0));
		}

		/// <summary>
		/// fetch maxqty amount of stories that match the given story types, 
		/// ordered by submission date (most recent first)
		/// </summary>
		/// <param name="mylist"></param>
		/// <param name="maxqty"></param>
		/// <returns></returns>
		public List<Story> LoadStoriesMultipleTypes_User(List<int> mylist, int userID, int maxqty) {
			StoryDAL sDAL = new StoryDAL();
			return FillListFromDataSet(sDAL.LoadStoriesMultipleTypes_User(mylist, userID, maxqty));
		}

		/// <summary>
		///1	Game Preview
		///2	Game Review
		///3	Editorial
		///4	New Game Announcement/Press Release
		///5	User Review
		///6	General Gaming News
		///7	Podcast
		/// </summary>
		/// <returns></returns>
		public ListItemCollection GetStoryTypeList() {
			ListItemCollection tablist = new ListItemCollection();
			tablist.Add(new ListItem("Previews", "1"));
			tablist.Add(new ListItem("Reviews", "2"));
			tablist.Add(new ListItem("Editorials", "3"));
			tablist.Add(new ListItem("PR/Announcements", "4"));
			tablist.Add(new ListItem("User Reviews", "5"));
			tablist.Add(new ListItem("News", "6"));
			tablist.Add(new ListItem("Podcasts", "7"));
			return tablist;
		}

		/// <summary>
		///1	Game Preview
		///2	Game Review
		///3	Editorial
		///4	New Game Announcement/Press Release
		///5	User Review
		///6	General Gaming News
		///7	Podcast
		/// </summary>
		/// <returns></returns>
		public ListItemCollection GetStoryTypeList(List<int> mylist) {
			ListItemCollection tablist = new ListItemCollection();
			for (int i = 1; i <= 7; i++) {
				if (mylist.Contains(i)) {
					tablist.Add(new ListItem(GetStoryTypeTabTitle(i), i.ToString()));
				}
			}
			return tablist;
		}

		/// <summary>
		///1	Game Preview
		///2	Game Review
		///3	Editorial
		///4	New Game Announcement/Press Release
		///5	User Review
		///6	General Gaming News
		///7	Podcast
		/// </summary>
		/// <param name="storyTypeID"></param>
		/// <returns></returns>
		public string GetStoryTypeTitle(int storyTypeID) {
			switch (storyTypeID) {
				case 1:
					return "Previews";
				case 2:
					return "Reviews";
				case 3:
					return "Editorials";
				case 4:
					return "PR/Announcements";
				case 5:
					return "User Reviews";
				case 6:
					return "News";
				case 7:
					return "Podcasts";
				default:
					return "";
			}
		}

		/// <summary>
		///1	Game Preview
		///2	Game Review
		///3	Editorial
		///4	New Game Announcement/Press Release
		///5	User Review
		///6	General Gaming News
		///7	Podcast
		/// </summary>
		/// <param name="storyTypeID"></param>
		/// <returns></returns>
		public string GetStoryTypeTabTitle(int storyTypeID) {
			switch (storyTypeID) {
				case 1:
					return "Previews";
					break;
				case 2:
					return "Reviews";
					break;
				case 3:
					return "Editorials";
					break;
				case 4:
					return "PR/Announcements";
					break;
				case 5:
					return "User Reviews";
					break;
				case 6:
					return "News";
					break;
				case 7:
					return "Podcasts";
					break;
				default:
					return "";
					break;
			}
		}

		public bool isPodcast(int storyTypeID) {
			if (storyTypeID == 7) { return true; } else { return false; }
		}

		/// <summary>
		/// Attach a developer to a story
		/// </summary>
		/// <param name="devID">Developer ID</param>
		public void AddDeveloper(int devID) {
			StoryDAL sDAL = new StoryDAL();
			sDAL.AddDeveloper(this.id, devID);
		}

		public void AddGenre(int genreID) {
			StoryDAL sDAL = new StoryDAL();
			sDAL.AddGenre(this.id, genreID);
		}

		public void AddGame(int gameID) {
			StoryDAL sDAL = new StoryDAL();
			sDAL.AddGame(this.id, gameID);
		}

		public void AddPlatform(int platformID) {
			StoryDAL sDAL = new StoryDAL();
			sDAL.AddPlatform(this.id, platformID);
		}

		public void AddPublisher(int publisherID) {
			StoryDAL sDAL = new StoryDAL();
			sDAL.AddPublisher(this.id, publisherID);
		}

		/// <summary>
		/// remove a developer from a story
		/// </summary>
		/// <param name="devID">Developer ID</param>
		public void RemoveDeveloper(int devID) {
			StoryDAL sDAL = new StoryDAL();
			sDAL.RemoveDeveloper(this.id, devID);
		}

		public void RemoveGenre(int genreID) {
			StoryDAL sDAL = new StoryDAL();
			sDAL.RemoveGenre(this.id, genreID);
		}

		public void RemoveGame(int gameID) {
			StoryDAL sDAL = new StoryDAL();
			sDAL.RemoveGame(this.id, gameID);
		}

		public void RemovePlatform(int platformID) {
			StoryDAL sDAL = new StoryDAL();
			sDAL.RemovePlatform(this.id, platformID);
		}

		public void RemovePublisher(int publisherID) {
			StoryDAL sDAL = new StoryDAL();
			sDAL.RemovePublisher(this.id, publisherID);
		}

		public List<Story> LoadRecent(int count) {
			StoryDAL sDAL = new StoryDAL();
			return FillListFromDataSet(sDAL.LoadRecent(count));
		}

		public List<Story> LoadRecentByType(int count, int type) {
			StoryDAL sDAL = new StoryDAL();
			return FillListFromDataSet(sDAL.LoadRecentByType(count, type));
		}

		public List<Story> LoadRecentFeaturedUpdates(int count, int homecategory) {
			StoryDAL sDAL = new StoryDAL();
			return FillListFromDataSet(sDAL.LoadRecentFeaturedUpdates(count, homecategory));
		}

		public List<Story> LoadRecentByGame(int count, int gameID) {
			StoryDAL sDAL = new StoryDAL();
			return FillListFromDataSet(sDAL.LoadRecentByGame(count, gameID));
		}

		public List<Story> LoadRecentByGameNoUserReviews(int count, int gameID) {
			StoryDAL sDAL = new StoryDAL();
			return FillListFromDataSet(sDAL.LoadRecentByGameNoUserReviews(count, gameID));
		}

		public void AttachPodcastFile(int storyID, string filename) {
			StoryDAL sDAL = new StoryDAL();
			sDAL.AttachPodcastFile(storyID, filename);
		}

		public string GetPodcastFile(int storyID) {
			StoryDAL sDAL = new StoryDAL();
			return sDAL.GetPodcastFile(storyID);
		}

		public List<Story> LoadUserReviewsByGame(int gameID) {
			StoryDAL sDAL = new StoryDAL();
			return FillListFromDataSet(sDAL.getUserReviewsByGame(gameID));
		}

		public List<Story> GetPagedStories(int count, int page) {
			StoryDAL sDAL = new StoryDAL();
			return FillListFromDataSet(sDAL.GetPagedStories(count, page));
		}

        public List<Story> GetPagedStoriesByType(int count, int page, int typeID) {
            StoryDAL sDAL = new StoryDAL();
            return FillListFromDataSet(sDAL.GetPagedStoriesByType(count, page, typeID));
        }

        public int GetTotalStoriesByType(int typeID) {
            StoryDAL sDAL = new StoryDAL();
            return sDAL.GetTotalStoriesByType(typeID);
        }

		public List<Story> GetPagedStaffStories(int count, int page) {
			StoryDAL sDAL = new StoryDAL();
			return FillListFromDataSet(sDAL.GetPagedStaffStories(count, page));
		}

		public int GetTotalStaffStories() {
			StoryDAL sDAL = new StoryDAL();
			return sDAL.GetTotalStaffStories();
		}
	}
}
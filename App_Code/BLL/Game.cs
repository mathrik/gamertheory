using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for Game
/// </summary>
namespace gamer {
	public class Game {
		private int _id;
		private string _title;
		private string _notes;
		private bool _approved;
		private DateTime _dateAdded;
		private int _submitter;

		public int id {
			get { return _id; }
			set { _id = value; }
		}

		public string title {
			get { return _title; }
			set { _title = value; }
		}

		public string notes {
			get { return _notes; }
			set { _notes = value; }
		}

		public bool approved {
			get { return _approved; }
			set { _approved = value; }
		}

		public DateTime dateAdded {
			get { return _dateAdded; }
			set { _dateAdded = value; }
		}

		public int submitter {
			get { return _submitter; }
			set { _submitter = value; }
		}

		public string seotitle {
			get {
				return HttpContext.Current.Server.UrlEncode(this.title.Replace(" ", "-").Replace(":",""));
			}
		}

		public string permalink {
			get { return "/game.aspx/" + this.id.ToString() + "/" + this.seotitle + "/"; }
		}

		public Game() {
			//
			// TODO: Add constructor logic here
			//
		}

		public void Add() {
			GameDAL gameDAL = new GameDAL();
			this.id = gameDAL.Add(this.title, this.notes, Convert.ToInt32(this.approved), this.dateAdded, this.submitter);
		}

		public void Update() {
			GameDAL gameDAL = new GameDAL();
			gameDAL.Update(this.id, this.title, this.notes, Convert.ToInt32(this.approved), this.dateAdded, this.submitter);
		}

		public void Delete() {
			GameDAL gameDAL = new GameDAL();
			gameDAL.Delete(this.id);
		}

		protected List<Game> FillListByDataSet(DataSet ds) {
			List<Game> myList = new List<Game>();
			Game thisgame;
			Toolbox tools = new Toolbox();
			for (int i = 0; ds.Tables.Count > 0 && i < ds.Tables[0].Rows.Count; i++) {
				thisgame = new Game();
				thisgame.id = tools.getInt(ds.Tables[0].Rows[i]["id"].ToString());
				thisgame.title = ds.Tables[0].Rows[i]["title"].ToString();
				thisgame.notes = ds.Tables[0].Rows[i]["notes"].ToString();
				thisgame.approved = ds.Tables[0].Rows[i]["is_approved"].ToString() == "1";
				try {
					thisgame.dateAdded = Convert.ToDateTime(ds.Tables[0].Rows[i]["date_added"].ToString());
				} catch {
					thisgame.dateAdded = Convert.ToDateTime("1/1/1900");
				}
				thisgame.submitter = tools.getInt(ds.Tables[0].Rows[i]["submitter"].ToString());
				myList.Add(thisgame);
			}
			return myList;
		}

		public bool LoadByID(int id) {
			GameDAL gameDAL = new GameDAL();
			DataSet ds = gameDAL.LoadByID(id);
			Toolbox tools = new Toolbox();
			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
				this.id = tools.getInt(ds.Tables[0].Rows[0]["id"].ToString());
				this.title = ds.Tables[0].Rows[0]["title"].ToString();
				this.notes = ds.Tables[0].Rows[0]["notes"].ToString();
				this.approved = ds.Tables[0].Rows[0]["is_approved"].ToString() == "1";
				try {
					this.dateAdded = Convert.ToDateTime(ds.Tables[0].Rows[0]["date_added"].ToString());
				} catch {
					this.dateAdded = Convert.ToDateTime("1/1/1900");
				}
				this.submitter = tools.getInt(ds.Tables[0].Rows[0]["submitter"].ToString());
				return true;
			} else {
				return false;
			}
		}

		public DataSet GetScreenShots() {
			GameDAL gameDAL = new GameDAL();
			return gameDAL.GetScreenshots(this.id);
		}

		public List<Game> LoadAll() {
			GameDAL gDAL = new GameDAL();
			return FillListByDataSet(gDAL.LoadAll());
		}

		public List<Game> SearchByTitle(string query) {
			GameDAL gDAL = new GameDAL();
			return FillListByDataSet(gDAL.SearchByTitle(query));
		}

		public List<Game> AutocompleteByTitle(string query) {
			GameDAL gDAL = new GameDAL();
			return FillListByDataSet(gDAL.SearchByTitle(query));
		}

		public List<Game> LoadStoryGames(int storyID) {
			GameDAL gDAL = new GameDAL();
			return FillListByDataSet(gDAL.LoadStoryGames(storyID));
		}

		public int FetchGameFromTitle(string title) {
			GameDAL gDAL = new GameDAL();
			return gDAL.FetchGameFromTitle(title);
		}

        public List<Game> LoadRecent(int count) {
            GameDAL gDAL = new GameDAL();
            return FillListByDataSet(gDAL.LoadRecent(count));
        }

        public List<Game> LoadRecentByPlatform(int count, int platform) {
            GameDAL gDAL = new GameDAL();
            return FillListByDataSet(gDAL.LoadRecentByPlatform(count, platform));
        }

		public List<Game> LoadFromCollection(int numperpage, int page, int userID) {
			GameDAL gDAL = new GameDAL();
			return FillListByDataSet(gDAL.LoadFromCollection(numperpage, page, userID));
		}

		public List<Game> LoadPagedGames(int page, int numperpage) {
			GameDAL gDAL = new GameDAL();
			return FillListByDataSet(gDAL.LoadPagedGames(page, numperpage));
		}

		public List<Game> LoadPagedGamesByPlatform(int page, int numperpage, int platform) {
			GameDAL gDAL = new GameDAL();
			return FillListByDataSet(gDAL.LoadPagedGamesByPlatform(page, numperpage, platform));
		}

		public int totalApprovedGames() {
			GameDAL gDAL = new GameDAL();
			return gDAL.totalApprovedGames();
		}

		public DataSet LoadInstanceRankings(int gameID) {
			GameDAL gDAL = new GameDAL();
			return gDAL.LoadInstanceRankings(gameID);
		}

		public List<Game> LoadPagedFilteredGames(int page, 
				int numperpage, 
				int platform,
				int publisher,
				int genre,
				int releaseYear, 
				string title) {
			GameDAL gDAL = new GameDAL();
			return FillListByDataSet(gDAL.LoadPagedFilteredGames(numperpage, page, platform, publisher, genre, releaseYear, title));
		}

		public int totalFilteredApprovedGames(int platform,
				int publisher,
				int genre,
				int releaseYear,
				string title) {
			GameDAL gDAL = new GameDAL();
			return gDAL.totalFilteredApprovedGames(platform, publisher, genre, releaseYear, title);
		}

		public bool hasUserReviewed(int userID) {
			GameDAL gDAL = new GameDAL();
			return (gDAL.numUserReviewsByGame(userID, this.id) > 0);
		}
	}
}
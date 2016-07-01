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
/// Summary description for GameInstance
/// </summary>
namespace gamer {
	public class GameInstance {
		private int _id;
		private int _gameID;
		private int _platformID;
		private DateTime _release;
		private bool _approved;
		private string _cover;

		public int id {
			get { return _id; }
			set { _id = value; }
		}

		public int gameID {
			get { return _gameID; }
			set { _gameID = value; }
		}

		public int platformID {
			get { return _platformID; }
			set { _platformID = value; }
		}

		public DateTime release {
			get { return _release; }
			set { _release = value; }
		}

		public bool approved {
			get { return _approved; }
			set { _approved = value; }
		}

		public string cover {
			get { return _cover; }
			set { _cover = value; }
		}

		public GameInstance() {
			//
			// TODO: Add constructor logic here
			//
		}

		public void Add() {
			GameInstanceDAL giDAL = new GameInstanceDAL();
			this.id = giDAL.Add(this.gameID, this.platformID, this.release, Convert.ToInt32(this.approved), this.cover);
		}

		public void Update() {
			GameInstanceDAL giDAL = new GameInstanceDAL();
			giDAL.Update(this.id, this.gameID, this.platformID, this.release, Convert.ToInt32(this.approved), this.cover);
		}

		public void Delete() {
			FileHandler fileMgr = new FileHandler();
			string fileLoc;
			fileLoc = System.Configuration.ConfigurationManager.AppSettings["GameScreenShotDirectory"].ToString();
			fileMgr.delete(fileLoc + this.cover);
			GameInstanceDAL giDAL = new GameInstanceDAL();
			giDAL.Delete(this.id);
		}

		public bool LoadByID(int id) {
			GameInstanceDAL giDAL = new GameInstanceDAL();
			DataSet ds = giDAL.LoadByID(id);
			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
				this.id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
				this.gameID = Convert.ToInt32(ds.Tables[0].Rows[0]["game_id"].ToString());
				this.platformID = Convert.ToInt32(ds.Tables[0].Rows[0]["platform_id"].ToString());
				this.release = Convert.ToDateTime(ds.Tables[0].Rows[0]["release_date"].ToString());
				this.approved = ds.Tables[0].Rows[0]["is_approved"].ToString() == "1";
				this.cover = ds.Tables[0].Rows[0]["cover_image"].ToString();
				return true;
			} else {
				return false;
			}
		}

		/// <summary>
		/// usually this type of function would be protected, but I need it for the rate this page
		/// </summary>
		/// <param name="ds"></param>
		/// <returns></returns>
		public List<GameInstance> FillListFromDataSet(DataSet ds) {
			List<GameInstance> myList = new List<GameInstance>();
			GameInstance game;
			for (int i = 0; ds.Tables.Count > 0 && i < ds.Tables[0].Rows.Count; i++) {
				game = new GameInstance();
				game.id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
				game.gameID = Convert.ToInt32(ds.Tables[0].Rows[i]["game_id"].ToString());
				game.platformID = Convert.ToInt32(ds.Tables[0].Rows[i]["platform_id"].ToString());
				game.release = Convert.ToDateTime(ds.Tables[0].Rows[i]["release_date"].ToString());
				game.approved = ds.Tables[0].Rows[i]["is_approved"].ToString() == "1";
				game.cover = ds.Tables[0].Rows[i]["cover_image"].ToString();
				myList.Add(game);
			}
			return myList;
		}

		public List<GameInstance> LoadByGame(int gameID) {
			GameInstanceDAL giDAL = new GameInstanceDAL();
			return FillListFromDataSet(giDAL.LoadByGame(gameID));
		}

		public List<GameInstance> LoadFiltered(int platformID,
				int publisherID,
				int releaseYear,
				int gameID) {
			GameInstanceDAL giDAL = new GameInstanceDAL();
			return FillListFromDataSet(giDAL.LoadFiltered(platformID, publisherID, releaseYear, gameID));
		}

		public void AddDeveloper(int devID) {
			GameInstanceDAL giDAL = new GameInstanceDAL();
			giDAL.AddDeveloper(this.id, devID);
		}

		public void AddGenre(int genreID) {
			GameInstanceDAL giDAL = new GameInstanceDAL();
			giDAL.AddGenre(this.id, genreID);
		}

		public void AddPublisher(int publisherID) {
			GameInstanceDAL giDAL = new GameInstanceDAL();
			giDAL.AddPublisher(this.id, publisherID);
		}

		public void RemoveDeveloper(int devID) {
			GameInstanceDAL giDAL = new GameInstanceDAL();
			giDAL.RemoveDeveloper(this.id, devID);
		}

		public void RemoveGenre(int genreID) {
			GameInstanceDAL giDAL = new GameInstanceDAL();
			giDAL.RemoveGenre(this.id, genreID);
		}

		public void RemovePublisher(int publisherID) {
			GameInstanceDAL giDAL = new GameInstanceDAL();
			giDAL.RemovePublisher(this.id, publisherID);
		}

		public List<GameInstance> LoadFromCollection(int numperpage, int page, int userID) {
			GameInstanceDAL giDAL = new GameInstanceDAL();
			return FillListFromDataSet(giDAL.LoadFromCollection(numperpage, page, userID));
		}

		public DataSet LoadTopRated(int count) {
			GameInstanceDAL giDAL = new GameInstanceDAL();
			return giDAL.LoadTopRated(count);
		}

		public DataSet LoadTopRated(int count, int platform) {
			GameInstanceDAL giDAL = new GameInstanceDAL();
			return giDAL.LoadTopRated(count, platform);
		}
	}
}
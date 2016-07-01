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
/// Summary description for GameScreenShot
/// </summary>
namespace gamer {
	public class GameScreenShot {
		private int _id;
		private string _filename;
		private int _rank;
		private int _gameID;

		public int id {
			get { return _id; }
			set { _id = value; }
		}

		public string filename {
			get { return _filename; }
			set { _filename = value; }
		}

		public int rank {
			get { return _rank; }
			set { _rank = value; }
		}

		public int gameID {
			get { return _gameID; }
			set { _gameID = value; }
		}

		public GameScreenShot() {
			//
			// TODO: Add constructor logic here
			//
		}

		public void Add() {
			GameScreenShotDAL gsDAL = new GameScreenShotDAL();
			this.id = gsDAL.Add(this.filename, this.rank, this.gameID);
		}

		public void Update() {
			GameScreenShotDAL gsDAL = new GameScreenShotDAL();
			gsDAL.Update(this.id, this.filename, this.rank, this.gameID);
		}

		public void Delete() {
			GameScreenShotDAL gsDAL = new GameScreenShotDAL();
			FileHandler fileMgr = new FileHandler();
			string fileLoc;
			fileLoc = System.Configuration.ConfigurationManager.AppSettings["GameScreenShotDirectory"].ToString();
			fileMgr.delete(fileLoc + this.filename);
			gsDAL.Delete(this.id);
		}

		protected List<GameScreenShot> FillListByDataSet(DataSet ds) {
			List<GameScreenShot> myList = new List<GameScreenShot>();
			GameScreenShot thisgame;
			for (int i = 0; ds.Tables.Count > 0 && i < ds.Tables[0].Rows.Count; i++) {
				thisgame = new GameScreenShot();
				thisgame.id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
				thisgame.filename = ds.Tables[0].Rows[i]["filename"].ToString();
				thisgame.rank = Convert.ToInt32(ds.Tables[0].Rows[i]["rank"].ToString());
				thisgame.gameID = Convert.ToInt32(ds.Tables[0].Rows[i]["game_id"].ToString());
				myList.Add(thisgame);
			}
			return myList;
		}

		public bool LoadByID(int id) {
			GameScreenShotDAL gsDAL = new GameScreenShotDAL();
			DataSet ds = gsDAL.LoadByID(id);
			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
				this.id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
				this.filename = ds.Tables[0].Rows[0]["filename"].ToString();
				this.rank = Convert.ToInt32(ds.Tables[0].Rows[0]["rank"].ToString());
				this.gameID = Convert.ToInt32(ds.Tables[0].Rows[0]["game_id"].ToString());
				return true;
			} else {
				return false;
			}
		}

		public List<GameScreenShot> LoadByGame(int gameID) {
			GameScreenShotDAL gsDAL = new GameScreenShotDAL();
			return FillListByDataSet(gsDAL.LoadByGame(gameID));
		}

		public int GetHighestRank() {
			GameScreenShotDAL gsDAL = new GameScreenShotDAL();
			return gsDAL.GetHighestRank();
		}
	}
}

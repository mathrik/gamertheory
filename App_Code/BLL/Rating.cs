using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for Rating
/// </summary>
namespace gamer {
	public class Rating {
		private int _id;
		private int _objectID;
		private int _value;
		private int _user;
		private int _type;
		private string _typeTitle;

		public int id {
			get { return _id; }
			set { _id = value; }
		}

		public int objectID {
			get { return _objectID; }
			set { _objectID = value; }
		}

		public int ratingValue {
			get { return _value; }
			set { _value = value; }
		}

		public int userID {
			get { return _user; }
			set { _user = value; }
		}

		public int type {
			get { return _type; }
			set { _type = value; }
		}

		public string typeTitle {
			get { return _typeTitle; }
		}

		public Rating() {
			//
			// TODO: Add constructor logic here
			//
		}

		public void Add() {
			RatingDAL rDAL = new RatingDAL();
			this.id = rDAL.Add(this.objectID, this.ratingValue, this.userID, this.type);
		}

		public void Update() {
			RatingDAL rDAL = new RatingDAL();
			rDAL.Update(this.id, this.objectID, this.ratingValue, this.userID, this.type);
		}

		public void Delete() {
			RatingDAL rDAL = new RatingDAL();
			rDAL.Delete(this.id);
		}

		protected List<Rating> GetListFromDataSet(DataSet ds) {
			List<Rating> myList = new List<Rating>();
			Rating current;
			if (ds.Tables.Count > 0) {
				for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {
					current = new Rating();
					current.id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
					current.objectID = Convert.ToInt32(ds.Tables[0].Rows[i]["object_id"].ToString());
					current.ratingValue = Convert.ToInt32(ds.Tables[0].Rows[i]["value"].ToString());
					current.userID = Convert.ToInt32(ds.Tables[0].Rows[i]["user_id"].ToString());
					current.type = Convert.ToInt32(ds.Tables[0].Rows[i]["ratingtype_id"].ToString());
					current._typeTitle = ds.Tables[0].Rows[i]["title"].ToString();
					myList.Add(current);
				}
			}
			return myList;
		}

		public bool LoadByID() {
			RatingDAL rDAL = new RatingDAL();
			DataSet ds = rDAL.LoadByID(this.id);
			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
				this.objectID = Convert.ToInt32(ds.Tables[0].Rows[0]["object_id"].ToString());
				this.ratingValue = Convert.ToInt32(ds.Tables[0].Rows[0]["value"].ToString());
				this.userID = Convert.ToInt32(ds.Tables[0].Rows[0]["user_id"].ToString());
				this.type = Convert.ToInt32(ds.Tables[0].Rows[0]["ratingtype_id"].ToString());
				this._typeTitle = ds.Tables[0].Rows[0]["title"].ToString();
				return true;
			} else {
				return false;
			}
		}

		public List<Rating> LoadByObject(int objectID, int objectType) {
			RatingDAL rDAL = new RatingDAL();
			return GetListFromDataSet(rDAL.LoadByObject(objectID, objectType));
		}

		public List<Rating> LoadByUser(int user) {
			RatingDAL rDAL = new RatingDAL();
			return GetListFromDataSet(rDAL.LoadByUser(user));
		}


		/// <summary>
		/// 1 = game
		/// 2 = story
		/// 3 = collection
		/// </summary>
		/// <param name="ratingtype"></param>
		/// <returns></returns>
		public List<Rating> getUserRatings(int userID, int ratingtype) {
			RatingDAL rDAL = new RatingDAL();
			return GetListFromDataSet(rDAL.getUserRatings(userID, ratingtype));
		}

		public bool CheckExistingRanking(int userID, int ratingtype, int objectID) {
			RatingDAL rDAL = new RatingDAL();
			DataSet ds = rDAL.CheckExistingRanking(userID, ratingtype, objectID);
			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
				this.id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
				this.objectID = Convert.ToInt32(ds.Tables[0].Rows[0]["object_id"].ToString());
				this.ratingValue = Convert.ToInt32(ds.Tables[0].Rows[0]["value"].ToString());
				this.userID = Convert.ToInt32(ds.Tables[0].Rows[0]["user_id"].ToString());
				this.type = Convert.ToInt32(ds.Tables[0].Rows[0]["ratingtype_id"].ToString());
				this._typeTitle = ds.Tables[0].Rows[0]["title"].ToString();
				return true;
			} else {
				return false;
			}
		}

		public DataSet LoadRecentGamesRating(int count) {
			RatingDAL rDAL = new RatingDAL();
			return rDAL.LoadRecentGamesRating(count);
		}

		public DataSet LoadRecentGamesRatingByPlatform(int count, int platform) {
			RatingDAL rDAL = new RatingDAL();
			return rDAL.LoadRecentGamesRatingByPlatform(count, platform);
		}

		public DataSet LoadPagedGameRating(int page, int numperpage) {
			RatingDAL rDAL = new RatingDAL();
			return rDAL.LoadPagedGameRating(page, numperpage);
		}

		public DataSet LoadPagedGameRatingByPlatform(int page, int numperpage, int platform) {
			RatingDAL rDAL = new RatingDAL();
			return rDAL.LoadPagedGameRatingByPlatform(page, numperpage, platform);
		}

		public DataSet LoadFilteredPagedGameRating(int page,
				int numperpage,
				int platform,
				int publisher,
				int genre,
				int releaseYear,
				string title) {
			RatingDAL rDAL = new RatingDAL();
			return rDAL.LoadFilteredPagedGameRating(page, numperpage, platform, publisher, genre, releaseYear, title);
		}
	}
}
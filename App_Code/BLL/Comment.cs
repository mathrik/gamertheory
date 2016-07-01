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
/// Summary description for Comment
/// </summary>
namespace gamer {
	public class Comment {
		private int _id;
		private int _type;
		private int _object;
		private string _body;
		private int _user;
		private int _flagged;
		private string _typetitle;
		private string _username;
		private string _avatar;
		private DateTime _submissiondate;

		public int id {
			get { return _id; }
			set { _id = value; }
		}

		/// <summary>
		///  1 = story
		///  2 = game
		///  3 = collection
		/// </summary>
		public int typeID {
			get { return _type; }
			set { _type = value; }
		}

		public int objectID {
			get { return _object; }
			set { _object = value; }
		}

		public string body {
			get { return _body; }
			set { _body = value; }
		}

		public int userID {
			get { return _user; }
			set { _user = value; }
		}
		/// <summary>
		/// 0 = posted, not flagged
		/// 1 = flagged
		/// 2 = flagged then approved
		/// </summary>
		public int flagged {
			get { return _flagged; }
			set { _flagged = value; }
		}

		public string typettitle {
			get { return _typetitle; }
			set { _typetitle = value; }
		}

		public string username {
			get { return _username; }
			set { _username = value; }
		}

		public string avatar {
			get {
				if (_avatar.Length > 0) {
					return _avatar;
				} else {
					return "user_avatar.jpg";
				}
			}
			set { _avatar = value; }
		}

		public DateTime submissionDate {
			get { return _submissiondate; }
			set { _submissiondate = value; }
		}

		public Comment() {
			//
			// TODO: Add constructor logic here
			//
		}

		public void Add() {
			CommentDAL cDAL = new CommentDAL();
			this.id = cDAL.Add(this.typeID,
				this.objectID,
				this.body,
				this.userID,
				this.flagged);
		}

		public void Update() {
			CommentDAL cDAL = new CommentDAL();
			cDAL.Update(this.id,
				this.typeID,
				this.objectID,
				this.body,
				this.userID,
				this.flagged);
		}

		public void Delete() {
			CommentDAL cDAL = new CommentDAL();
			cDAL.Delete(this.id);
		}

		public bool LoadByID(int id) {
			Toolbox toolbox = new Toolbox();
			CommentDAL cDAL = new CommentDAL();
			DataSet ds = cDAL.LoadByID(id);
			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
				this.id = toolbox.getInt(ds.Tables[0].Rows[0]["id"].ToString());
				this.typeID = toolbox.getInt(ds.Tables[0].Rows[0]["commenttype_id"].ToString());
				this.objectID = toolbox.getInt(ds.Tables[0].Rows[0]["object_id"].ToString());
				this.body = ds.Tables[0].Rows[0]["body"].ToString();
				this.userID = toolbox.getInt(ds.Tables[0].Rows[0]["user_id"].ToString());
				this.flagged = toolbox.getInt(ds.Tables[0].Rows[0]["flagged"].ToString());
				try {
					this.submissionDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["submission_date"].ToString());
				} catch {
					this.submissionDate = new DateTime();
				}
				return true;
			} else {
				return false;
			}
		}

		protected List<Comment> FillListFromDataSet(DataSet ds) {
			Toolbox toolbox = new Toolbox();
			List<Comment> myList = new List<Comment>();
			for (int i = 0; ds.Tables.Count > 0 && i < ds.Tables[0].Rows.Count; i++) {
				Comment current_item = new Comment();
				current_item.id = toolbox.getInt(ds.Tables[0].Rows[i]["id"].ToString());
				current_item.typeID = toolbox.getInt(ds.Tables[0].Rows[i]["commenttype_id"].ToString());
				current_item.objectID = toolbox.getInt(ds.Tables[0].Rows[i]["object_id"].ToString());
				current_item.body = ds.Tables[0].Rows[i]["body"].ToString();
				current_item.userID = toolbox.getInt(ds.Tables[0].Rows[i]["user_id"].ToString());
				current_item.flagged = toolbox.getInt(ds.Tables[0].Rows[i]["flagged"].ToString());
				try {
					current_item.submissionDate = Convert.ToDateTime(ds.Tables[0].Rows[i]["submission_date"].ToString());
				} catch {
					current_item.submissionDate = new DateTime();
				}
				try {
					current_item.typettitle = ds.Tables[0].Rows[i]["title"].ToString();
				} catch {
					current_item.typettitle = "";
				}
				try {
					current_item.avatar = ds.Tables[0].Rows[i]["avatar"].ToString();
				} catch {
					current_item.avatar = "";
				}
				try {
					current_item.username = ds.Tables[0].Rows[i]["username"].ToString();
				} catch {
					current_item.username = "";
				}
				myList.Add(current_item);
			}
			return myList;
		}

		public List<Comment> GetUserComments(int userID) {
			CommentDAL cDAL = new CommentDAL();
			return FillListFromDataSet(cDAL.GetUserComments(userID));
		}

		public List<Comment> GetComments(int objectID, int typeID) {
			CommentDAL cDAL = new CommentDAL();
			return FillListFromDataSet(cDAL.GetComments(objectID, typeID, 0));
		}

		public List<Comment> GetComments(int objectID, int typeID, int count) {
			CommentDAL cDAL = new CommentDAL();
			return FillListFromDataSet(cDAL.GetComments(objectID, typeID, count));
		}

		public List<Comment> GetRecentComments(int count) {
			CommentDAL cDAL = new CommentDAL();
			return FillListFromDataSet(cDAL.GetRecentComments(count));
		}

		public List<Comment> GetPagedComments(int objectID, int typeID, int count, int page) {
			CommentDAL cDAL = new CommentDAL();
			return FillListFromDataSet(cDAL.GetPagedComments(objectID, typeID, count, page));
		}
		public int GetCommentCount(int objectID, int typeID) {
			CommentDAL cDAL = new CommentDAL();
			return cDAL.GetCommentCount(objectID, typeID);
		}

		public List<Comment> GetFlaggedComments(int count, int page) {
			CommentDAL cDAL = new CommentDAL();
			return FillListFromDataSet(cDAL.GetPagedFlaggedComments(count, page));
		}

		public List<Comment> GetFlaggedCommentsByObject(int objectID, int typeID, int count, int page) {
			CommentDAL cDAL = new CommentDAL();
			return FillListFromDataSet(cDAL.GetPagedFlaggedComments(objectID, typeID, count, page));
		}

		public int GetFlaggedCommentCount() {
			CommentDAL cDAL = new CommentDAL();
			return cDAL.GetFlaggedCommentCount();
		}
	}
}
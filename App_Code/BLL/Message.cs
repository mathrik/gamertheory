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
/// Summary description for Message
/// </summary>
namespace forum {
	public class Message {
		private int _id;
		private string _title;
		private string _body;
		private int _parent;
		private DateTime _edited;
		private DateTime _deleted;
		private int _thread;
		private int _flagged;
		private DateTime _posted;

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

		public int parent {
			get { return _parent; }
			set { _parent = value; }
		}

		public DateTime edited {
			get { return _edited; }
			set { _edited = value; }
		}

		public DateTime deleted {
			get { return _deleted; }
			set { _deleted = value; }
		}

		public int thread {
			get { return _thread; }
			set { _thread = value; }
		}

		public int flagged {
			get { return _flagged; }
			set { _flagged = value; }
		}

		public DateTime posted {
			get { return _posted; }
			set { _posted = value; }
		}

		public Message() {
			//
			// TODO: Add constructor logic here
			//
		}

		public void Add() {
			MessageDAL mDAL = new MessageDAL();
			this.id = mDAL.Add(this.title,
				this.body,
				this.parent,
				this.edited,
				this.deleted,
				this.thread,
				this.flagged,
				this.posted);
		}

		public void Update() {
			MessageDAL mDAL = new MessageDAL();
			mDAL.Update(this.id,
				this.title,
				this.body,
				this.parent,
				this.edited,
				this.deleted,
				this.thread,
				this.flagged,
				this.posted);
		}

		public void Delete() {
			MessageDAL mDAL = new MessageDAL();
			mDAL.Delete(this.id);
		}

		public void MarkDeleted() {
			MessageDAL mDAL = new MessageDAL();
			mDAL.MarkDeleted(this.id);
		}

		protected List<Message> FillListFromDataSet(DataSet ds) {
			List<Message> myList = new List<Message>();
			Message message;
			for (int i = 0; ds.Tables.Count > 0 && i < ds.Tables[0].Rows.Count; i++) {
				message = new Message();
				message.id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
				message.title = ds.Tables[0].Rows[i]["title"].ToString();
				message.body = ds.Tables[0].Rows[i]["body"].ToString();
				message.parent = Convert.ToInt32(ds.Tables[0].Rows[i]["parent_id"].ToString());
				message.edited = Convert.ToDateTime(ds.Tables[0].Rows[i]["dt_edited"].ToString());
				message.deleted = Convert.ToDateTime(ds.Tables[0].Rows[i]["dt_deleted"].ToString());
				message.thread = Convert.ToInt32(ds.Tables[0].Rows[i]["forumthread_id"].ToString());
				message.flagged = Convert.ToInt32(ds.Tables[0].Rows[i]["isflagged"].ToString());
				message.posted = Convert.ToDateTime(ds.Tables[0].Rows[i]["dt_posted"].ToString());
				myList.Add(message);
			}
			return myList;
		}

		public bool LoadByID(int id) {
			MessageDAL mDAL = new MessageDAL();
			DataSet ds = mDAL.LoadByID(id);
			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
				this.id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
				this.title = ds.Tables[0].Rows[0]["title"].ToString();
				this.body = ds.Tables[0].Rows[0]["body"].ToString();
				this.parent = Convert.ToInt32(ds.Tables[0].Rows[0]["parent_id"].ToString());
				this.edited = Convert.ToDateTime(ds.Tables[0].Rows[0]["dt_edited"].ToString());
				this.deleted = Convert.ToDateTime(ds.Tables[0].Rows[0]["dt_deleted"].ToString());
				this.thread = Convert.ToInt32(ds.Tables[0].Rows[0]["forumthread_id"].ToString());
				this.flagged = Convert.ToInt32(ds.Tables[0].Rows[0]["isflagged"].ToString());
				this.posted = Convert.ToDateTime(ds.Tables[0].Rows[0]["dt_posted"].ToString());
				return true;
			} else {
				return false;
			}
		}

		public List<Message> LoadByThread(int thread, bool showDeleted) {
			MessageDAL mDAL = new MessageDAL();
			return FillListFromDataSet(mDAL.LoadByThread(thread, showDeleted));
		}

		public List<Message> LoadByThread(int thread, bool showDeleted, int start, int end) {
			MessageDAL mDAL = new MessageDAL();
			return FillListFromDataSet(mDAL.LoadByThread(thread, showDeleted, start, end));
		}
	}
}
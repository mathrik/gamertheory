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
/// Summary description for Thread
/// </summary>
namespace forum {
	public class Thread {
		private int _id;
		private string _title;
		private bool _issticky;
		private bool _islocked;
		private int _category;

		public int id {
			get { return _id; }
			set { _id = value; }
		}

		public string title {
			get { return _title; }
			set { _title = value; }
		}

		public bool sticky {
			get { return _issticky; }
			set { _issticky = value; }
		}

		public bool locked {
			get { return _islocked; }
			set { _islocked = value; }
		}

		public int categoryID {
			get { return _category; }
			set { _category = value; }
		}

		public Thread() {
			//
			// TODO: Add constructor logic here
			//
		}

		public void Add() {
			ThreadDAL tDAL = new ThreadDAL();
			this.id = tDAL.Add(this.title,
				Convert.ToInt32(this.sticky),
				Convert.ToInt32(this.locked),
				this.categoryID);
		}

		public void Update() {
			ThreadDAL tDAL = new ThreadDAL();
			tDAL.Update(this.id,
				this.title,
				Convert.ToInt32(this.sticky),
				Convert.ToInt32(this.locked),
				this.categoryID);
		}

		public void Delete() {
			ThreadDAL tDAL = new ThreadDAL();
			tDAL.Delete(this.id);
		}

		protected List<Thread> FillListFromDataSet(DataSet ds) {
			List<Thread> myList = new List<Thread>();
			Thread thread;
			for (int i = 0; ds.Tables.Count > 0 && i < ds.Tables[0].Rows.Count; i++) {
				thread = new Thread();
				thread.id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
				thread.title = ds.Tables[0].Rows[i]["title"].ToString();
				thread.sticky = ds.Tables[0].Rows[i]["issticky"].ToString() == "1";
				thread.locked = ds.Tables[0].Rows[i]["islocked"].ToString() == "1";
				thread.categoryID = Convert.ToInt32(ds.Tables[0].Rows[i]["forumCategory_id"].ToString());
				myList.Add(thread);
			}
			return myList;
		}

		public bool LoadByID(int id) {
			ThreadDAL tDAL = new ThreadDAL();
			DataSet ds = tDAL.LoadById(id);
			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
				this.id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
				this.title = ds.Tables[0].Rows[0]["title"].ToString();
				this.sticky = ds.Tables[0].Rows[0]["issticky"].ToString() == "1";
				this.locked = ds.Tables[0].Rows[0]["islocked"].ToString() == "1";
				this.categoryID = Convert.ToInt32(ds.Tables[0].Rows[0]["forumCategory_id"].ToString());
				return true;
			} else {
				return false;
			}
		}

		public List<Thread> LoadByCategory(int category) {
			ThreadDAL tDAL = new ThreadDAL();
			return FillListFromDataSet(tDAL.LoadByCategory(category));
		}

		public List<Thread> LoadAll() {
			ThreadDAL tDAL = new ThreadDAL();
			return FillListFromDataSet(tDAL.LoadAll());
		}
	}
}

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
/// Summary description for Publisher
/// </summary>
namespace gamer {
	public class Publisher {
		private int _id;
		private string _title;

		public int id {
			get { return _id; }
			set { _id = value; }
		}

		public string title {
			get { return _title; }
			set { _title = value; }
		}

		public Publisher() {
			//
			// TODO: Add constructor logic here
			//
		}

		public void Add() {
			PublisherDAL pDAL = new PublisherDAL();
			this.id = pDAL.Add(this.title);
		}

		public void Update() {
			PublisherDAL pDAL = new PublisherDAL();
			pDAL.Update(this.id, this.title);
		}

		public void Delete() {
			PublisherDAL pDAL = new PublisherDAL();
			pDAL.Delete(this.id);
		}

		public bool LoadByID(int id) {
			PublisherDAL pDAL = new PublisherDAL();
			DataSet ds = pDAL.LoadByID(id);
			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
				this.id = id;
				this.title = ds.Tables[0].Rows[0]["title"].ToString();
				return true;
			} else {
				return false;
			}
		}

		protected List<Publisher> ListFromDataset(DataSet ds) {
			List<Publisher> mylist = new List<Publisher>();
			Publisher publisher;
			Toolbox toolbox = new Toolbox();
			for (int i = 0; ds.Tables.Count > 0 && i < ds.Tables[0].Rows.Count; i++) {
				publisher = new Publisher();
				publisher.id = toolbox.getInt(ds.Tables[0].Rows[i]["id"].ToString());
				publisher.title = ds.Tables[0].Rows[i]["title"].ToString();
				mylist.Add(publisher);
			}
			return mylist;
		}

		public List<Publisher> LoadAll() {
			PublisherDAL pDAL = new PublisherDAL();
			return ListFromDataset(pDAL.LoadAll());
		}

		public List<Publisher> LoadStoryPublishers(int storyID) {
			PublisherDAL pDAL = new PublisherDAL();
			return ListFromDataset(pDAL.LoadStoryPublishers(storyID));
		}

		public List<Publisher> LoadGamePublishers(int instanceID) {
			PublisherDAL pDAL = new PublisherDAL();
			return ListFromDataset(pDAL.LoadGamePublishers(instanceID));
		}

		public int FetchPublisherFromTitle(string title) {
			PublisherDAL pDAL = new PublisherDAL();
			return pDAL.FetchPublisherFromTitle(title);
		}
	}
}
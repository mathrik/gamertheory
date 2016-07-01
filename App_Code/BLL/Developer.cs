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
/// Summary description for Developer
/// </summary>
namespace gamer {
	public class Developer {
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

		public Developer() {
			//
			// TODO: Add constructor logic here
			//
		}

		public void Add() {
			DeveloperDAL devDAL = new DeveloperDAL();
			this.id = devDAL.Add(this.title);
		}

		public void Update() {
			DeveloperDAL devDAL = new DeveloperDAL();
			devDAL.Update(this.id, this.title);
		}

		public void Delete() {
			DeveloperDAL devDAL = new DeveloperDAL();
			devDAL.Delete(this.id);
		}

		public bool LoadByID(int id) {
			DeveloperDAL devDAL = new DeveloperDAL();
			DataSet ds = devDAL.LoadByID(id);
			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
				this.id = id;
				this.title = ds.Tables[0].Rows[0]["title"].ToString();
				return true;
			} else {
				return false;
			}
		}

		protected List<Developer> ListFromDataset(DataSet ds) {
			List<Developer> mylist = new List<Developer>();
			Developer dev;
			Toolbox toolbox = new Toolbox();
			for (int i = 0; ds.Tables.Count > 0 && i < ds.Tables[0].Rows.Count; i++) {
				dev = new Developer();
				dev.id = toolbox.getInt(ds.Tables[0].Rows[i]["id"].ToString());
				dev.title = ds.Tables[0].Rows[i]["title"].ToString();
				mylist.Add(dev);
			}
			return mylist;
		}

		public List<Developer> LoadAll() {
			DeveloperDAL devDAL = new DeveloperDAL();
			return ListFromDataset(devDAL.LoadAll());
		}

		public List<Developer> LoadStoryDevelopers(int storyID) {
			DeveloperDAL devDAL = new DeveloperDAL();
			return ListFromDataset(devDAL.LoadStoryDevelopers(storyID));
		}

		public List<Developer> LoadGameDevelopers(int instanceID) {
			DeveloperDAL devDAL = new DeveloperDAL();
			return ListFromDataset(devDAL.LoadGameDevelopers(instanceID));
		}

		public int FetchDeveloperFromTitle(string title) {
			DeveloperDAL devDAL = new DeveloperDAL();
			return devDAL.FetchDeveloperFromTitle(title);
		}
	}
}
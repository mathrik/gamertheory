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
/// Summary description for Platform
/// </summary>
namespace gamer {
	public class Platform {
		private int _id;
		private string _title;
        private string _color;

		public int id {
			get { return _id; }
			set { _id = value; }
		}

		public string title {
			get { return _title; }
			set { _title = value; }
		}

        public string color {
            get { return _color; }
            set { _color = value; }
        }

		public Platform() {
			//
			// TODO: Add constructor logic here
			//
		}

		public void Add() {
			PlatformDAL pDAL = new PlatformDAL();
			this.id = pDAL.Add(this.title, this.color);
		}

		public void Update() {
			PlatformDAL pDAL = new PlatformDAL();
			pDAL.Update(this.id, this.title, this.color);
		}

		public void Delete() {
			PlatformDAL pDAL = new PlatformDAL();
			pDAL.Delete(this.id);
		}

		public bool LoadByID(int id) {
			PlatformDAL pDAL = new PlatformDAL();
			DataSet ds = pDAL.LoadByID(id);
			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
				this.id = id;
				this.title = ds.Tables[0].Rows[0]["title"].ToString();
                this.color = ds.Tables[0].Rows[0]["color"].ToString();
				return true;
			} else {
				return false;
			}
		}

		protected List<Platform> ListFromDataset(DataSet ds) {
			List<Platform> mylist = new List<Platform>();
			Platform platform;
			Toolbox toolbox = new Toolbox();
			for (int i = 0; ds.Tables.Count > 0 && i < ds.Tables[0].Rows.Count; i++) {
				platform = new Platform();
				platform.id = toolbox.getInt(ds.Tables[0].Rows[i]["id"].ToString());
                platform.title = ds.Tables[0].Rows[i]["title"].ToString();
                platform.color = ds.Tables[0].Rows[i]["color"].ToString();
				mylist.Add(platform);
			}
			return mylist;
		}

		public List<Platform> LoadAll() {
			PlatformDAL pDAL = new PlatformDAL();
			return ListFromDataset(pDAL.LoadAll());
		}

		public List<Platform> LoadStoryPlatforms(int storyID) {
			PlatformDAL pDAL = new PlatformDAL();
			return ListFromDataset(pDAL.LoadStoryPlatforms(storyID));
		}

		public int FetchPlatformFromTitle(string title) {
			PlatformDAL pDAL = new PlatformDAL();
			return pDAL.FetchPlatformFromTitle(title);
		}

		public List<int> CommonPlatformIDs() {
			List<int> mylist = new List<int>();
			mylist.Add(9);
			mylist.Add(28);
			mylist.Add(1);
			mylist.Add(3);
			mylist.Add(4);
			return mylist;
		}
	}
}
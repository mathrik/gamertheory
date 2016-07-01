using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

/// <summary>
/// Summary description for AdRotator
/// </summary>
namespace gamer {
	public class AdRotator {
		private int _id;
		private string _imageUrl;
		private string _navigateUrl;
		private string _alternateText;
		private string _keyword;
		private int _impressions;

		public int id {
			get { return _id; }
			set { _id = value; }
		}

		public string ImageUrl {
			get { return _imageUrl; }
			set { _imageUrl = value; }
		}

		public string NavigateUrl {
			get { return _navigateUrl; }
			set { _navigateUrl = value; }
		}

		public string AlternateText {
			get { return _alternateText; }
			set { _alternateText = value; }
		}

		public string Keyword {
			get { return _keyword; }
			set { _keyword = value; }
		}

		public int Impressions {
			get { return _impressions; }
			set { _impressions = value; }
		}

		public AdRotator() {
			//
			// TODO: Add constructor logic here
			//
		}

		public void Add() {
			AdRotatorDAL adDAL = new AdRotatorDAL();
			this.id = adDAL.Add(this);
		}

		public void Update() {
			AdRotatorDAL adDAL = new AdRotatorDAL();
			adDAL.Update(this);
		}

		public void Delete() {
			AdRotatorDAL adDAL = new AdRotatorDAL();
			adDAL.Delete(this.id);
		}

		protected List<AdRotator> ListFromDS(DataSet ds) {
			List<AdRotator> mylist = new List<AdRotator>();
			AdRotator myad;
			Toolbox tools = new Toolbox();
			for (int i = 0; ds.Tables.Count > 0 && i < ds.Tables[0].Rows.Count; i++) {
				myad = new AdRotator();
				myad.id = tools.getInt(ds.Tables[0].Rows[i]["id"].ToString());
				myad.ImageUrl = ds.Tables[0].Rows[i]["ImageUrl"].ToString();
				myad.NavigateUrl = ds.Tables[0].Rows[i]["NavigateUrl"].ToString();
				myad.AlternateText = ds.Tables[0].Rows[i]["AlternateText"].ToString();
				myad.Keyword = ds.Tables[0].Rows[i]["Keyword"].ToString();
				myad.Impressions = tools.getInt(ds.Tables[0].Rows[i]["Impressions"].ToString());
				mylist.Add(myad);
			}
			return mylist;
		}

		public bool LoadByID(int id) {
			AdRotatorDAL adDAL = new AdRotatorDAL();
			List<AdRotator> mylist = ListFromDS(adDAL.LoadByID(id));
			if (mylist.Count > 0) {
				this.id = mylist[0].id;
				this.ImageUrl = mylist[0].ImageUrl;
				this.NavigateUrl = mylist[0].NavigateUrl;
				this.AlternateText = mylist[0].AlternateText;
				this.Keyword = mylist[0].Keyword;
				this.Impressions = mylist[0].Impressions;
				return true;
			} else {
				return false;
			}
		}

		public List<AdRotator> LoadAll() {
			AdRotatorDAL adDAL = new AdRotatorDAL();
			return ListFromDS(adDAL.LoadAll());
		}
	}
}
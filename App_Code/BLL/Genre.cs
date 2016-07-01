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
/// Summary description for Genre
/// </summary>
namespace gamer {
	public class Genre {
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

		public Genre() {
			//
			// TODO: Add constructor logic here
			//
		}

		public void Add() {
			GenreDAL gDAL = new GenreDAL();
			this.id = gDAL.Add(this.title);
		}

		public void Update() {
			GenreDAL gDAL = new GenreDAL();
			gDAL.Update(this.id, this.title);
		}

		public void Delete() {
			GenreDAL gDAL = new GenreDAL();
			gDAL.Delete(this.id);
		}

		public bool LoadByID(int id) {
			GenreDAL gDAL = new GenreDAL();
			DataSet ds = gDAL.LoadByID(id);
			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
				this.id = id;
				this.title = ds.Tables[0].Rows[0]["title"].ToString();
				return true;
			} else {
				return false;
			}
		}

		protected List<Genre> ListFromDataset(DataSet ds) {
			List<Genre> mylist = new List<Genre>();
			Genre genre;
			Toolbox toolbox = new Toolbox();
			for (int i = 0; ds.Tables.Count > 0 && i < ds.Tables[0].Rows.Count; i++) {
				genre = new Genre();
				genre.id = toolbox.getInt(ds.Tables[0].Rows[i]["id"].ToString());
				genre.title = ds.Tables[0].Rows[i]["title"].ToString();
				mylist.Add(genre);
			}
			return mylist;
		}

		public List<Genre> FetchAll() {
			GenreDAL gDAL = new GenreDAL();
			return ListFromDataset(gDAL.LoadAll());
		}

		public List<Genre> LoadStoryGenres(int storyID) {
			GenreDAL gDAL = new GenreDAL();
			return ListFromDataset(gDAL.LoadStoryGenres(storyID));
		}

		public List<Genre> LoadGameGenres(int instanceID) {
			GenreDAL gDAL = new GenreDAL();
			return ListFromDataset(gDAL.LoadGameGenres(instanceID));
		}

		public int FetchGenreFromTitle(string title) {
			GenreDAL gDAL = new GenreDAL();
			return gDAL.FetchGenreFromTitle(title);
		}
	}
}
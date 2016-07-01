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
/// Summary description for UserFile
/// </summary>
namespace gamer {
	public class UserFile {
		private int _id;
		private string _filename;
		private int _user;
		private int _filetype;

		public int id {
			get { return _id; }
			set { _id = value; }
		}

		public string filename {
			get { return _filename; }
			set { _filename = value; }
		}

		public int userID {
			get { return _user; }
			set { _user = value; }
		}

		public int filetype {
			get { return _filetype; }
			set { _filetype = value; }
		}
				

		public UserFile() {
			//
			// TODO: Add constructor logic here
			//
		}

		public void Add() {
			UserFileDAL ufDAL = new UserFileDAL();
			this.id = ufDAL.Add(this.filename, this.userID, this.filetype);
		}

		public void Update() {
			UserFileDAL ufDAL = new UserFileDAL();
			ufDAL.Update(this.id, this.filename, this.userID, this.filetype);
		}

		public void Delete() {
			UserFileDAL ufDAL = new UserFileDAL();
			ufDAL.Delete(this.id);
		}

		protected List<UserFile> FillListFromDataSet(DataSet ds) {
			List<UserFile> myList = new List<UserFile>();
			UserFile uf;
			for (int i = 0; ds.Tables.Count > 0 && i < ds.Tables[0].Rows.Count; i++) {
				uf = new UserFile();
				uf.id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
				uf.filename = ds.Tables[0].Rows[i]["filename"].ToString();
				uf.userID = Convert.ToInt32(ds.Tables[0].Rows[i]["user_id"].ToString());
				uf.filetype = Convert.ToInt32(ds.Tables[0].Rows[i]["filetype_id"].ToString());
				myList.Add(uf);
			}
			return myList;
		}

		public bool LoadByID() {
			UserFileDAL ufDAL = new UserFileDAL();
			DataSet ds = ufDAL.LoadById(this.id);
			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
				this.id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
				this.filename = ds.Tables[0].Rows[0]["filename"].ToString();
				this.userID = Convert.ToInt32(ds.Tables[0].Rows[0]["user_id"].ToString());
				this.filetype = Convert.ToInt32(ds.Tables[0].Rows[0]["filetype_id"].ToString());
				return true;
			} else {
				return false;
			}
		}

		public List<UserFile> LoadByUser(int user) {
			UserFileDAL ufDAL = new UserFileDAL();
			return FillListFromDataSet(ufDAL.LoadByUser(user));
		}
	}
}

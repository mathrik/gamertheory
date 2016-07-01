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
/// Summary description for SocialMedia
/// </summary>
namespace gamer {
	public class SocialMedia {
		private int _id;
		private int _typeID;
		private int _user;
		private string _login;
		private string _password;

		public int id {
			get { return _id; }
			set { _id = value; }
		}

		public int typeID {
			get { return _typeID; }
			set { _typeID = value; }
		}

		public int user {
			get { return _user; }
			set { _user = value; }
		}

		public string login {
			get { return _login; }
			set { _login = value; }
		}

		public string password {
			get { return _password; }
			set { _password = value; }
		}

		public SocialMedia() {
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// needs some kind of encryption!
		/// </summary>
		public void Add() {
			SocialMediaDAL smDAL = new SocialMediaDAL();
			this.id = smDAL.Add(this.typeID, this.user, this.login, this.password);
		}

		public void Update() {
			SocialMediaDAL smDAL = new SocialMediaDAL();
			smDAL.Update(this.id, this.typeID, this.user, this.login, this.password);
		}

		public void Delete() {
			SocialMediaDAL smDAL = new SocialMediaDAL();
			smDAL.Delete(this.id);
		}

		protected List<SocialMedia> FillListFromDataSet(DataSet ds) {
			List<SocialMedia> myList = new List<SocialMedia>();
			SocialMedia credentials;
			for (int i = 0; ds.Tables.Count > 0 && i < ds.Tables[0].Rows.Count; i++) {
				credentials = new SocialMedia();
				credentials.id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
				credentials.typeID = Convert.ToInt32(ds.Tables[0].Rows[i]["socialmediatype_id"].ToString());
				credentials.user = Convert.ToInt32(ds.Tables[0].Rows[i]["user_id"].ToString());
				credentials.login = ds.Tables[0].Rows[i]["login"].ToString();
				credentials.password = ds.Tables[0].Rows[i]["password"].ToString();
				myList.Add(credentials);
			}
			return myList;
		}

		public bool LoadByID(int id) {
			SocialMediaDAL smDAL = new SocialMediaDAL();
			DataSet ds = smDAL.LoadByID(id);
			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
				this.id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
				this.typeID = Convert.ToInt32(ds.Tables[0].Rows[0]["socialmediatype_id"].ToString());
				this.user = Convert.ToInt32(ds.Tables[0].Rows[0]["user_id"].ToString());
				this.login = ds.Tables[0].Rows[0]["login"].ToString();
				this.password = ds.Tables[0].Rows[0]["password"].ToString();
				return true;
			} else {
				return false;
			}
		}

		public List<SocialMedia> LoadSocialMedia(int user, int typeID) {
			SocialMediaDAL smDAL = new SocialMediaDAL();
			return FillListFromDataSet(smDAL.LoadSocialMedia(user, typeID));
		}
	}
}

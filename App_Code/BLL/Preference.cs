using System;
using System.Data;
using System.Collections.Generic;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for Preference
/// </summary>
namespace gamer {
	public class Preference {
		private int _id;
		private int _type;
		private int _user;
		private int _value;
		private string _typetitle;

		public int id {
			get { return _id; }
			set { _id = value; }
		}

		public int typeID {
			get { return _type; }
			set { _type = value; }
		}

		public int userID {
			get { return _user; }
			set { _user = value; }
		}

		public int value {
			get { return _value; }
			set { _value = value; }
		}

		/// <summary>
		/// Although this is publicly "writable", this field is never written to the database
		/// </summary>
		public string typetitle {
			get { return _typetitle; }
			set { _typetitle = value; }
		}

		public Preference() {
			//
			// TODO: Add constructor logic here
			//
		}

		public void Add() {
			PreferenceDAL pDAL = new PreferenceDAL();
			this.id = pDAL.Add(this.typeID, this.userID, this.value);
		}

		public void Update() {
			PreferenceDAL pDAL = new PreferenceDAL();
			pDAL.Update(this.id, this.typeID, this.userID, this.value);
		}

		public void Delete() {
			PreferenceDAL pDAL = new PreferenceDAL();
			pDAL.Delete(this.id);
		}

		protected List<Preference> FillListFromDataSet(DataSet ds) {
			List<Preference> myList = new List<Preference>();
			for (int i = 0; ds.Tables.Count > 0 && i < ds.Tables[0].Rows.Count; i++) {
				Preference current_item = new Preference();
				current_item.id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
				current_item.typeID = Convert.ToInt32(ds.Tables[0].Rows[i]["preferencetype_id"].ToString());
				current_item.userID = Convert.ToInt32(ds.Tables[0].Rows[i]["user_id"].ToString());
				current_item.value = Convert.ToInt32(ds.Tables[0].Rows[i]["value"].ToString());
				try {
					current_item.typetitle = ds.Tables[0].Rows[i]["title"].ToString();
				} catch {
					current_item.typetitle = "";
				}
				myList.Add(current_item);
			}
			return myList;
		}

		public List<Preference> GetUserPreferences(int user) {
			PreferenceDAL pDAL = new PreferenceDAL();
			return FillListFromDataSet(pDAL.GetUserPreferences(user));
		}
	}
}
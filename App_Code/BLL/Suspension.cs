using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for Suspension
/// </summary>
namespace gamer {
	public class Suspension {
		private int _id;
		private int _user;
		private bool _permanent;
		private DateTime _expiration;

		public int id {
			get { return _id; }
			set { _id = value; }
		}

		public int userID {
			get { return _user; }
			set { _user = value; }
		}

		public bool permanent {
			get { return _permanent; }
			set { _permanent = value; }
		}

		public DateTime expiration {
			get { return _expiration; }
			set { _expiration = value; }
		}

		public Suspension() {
			//
			// TODO: Add constructor logic here
			//
		}

		public void Add() {
			SuspensionDAL sDAL = new SuspensionDAL();
			this.id = sDAL.Add(this.userID, this.permanent, this.expiration);
		}

		public void Update() {
			SuspensionDAL sDAL = new SuspensionDAL();
			sDAL.Update(this.id, this.userID, this.permanent, this.expiration);
		}

		public void Delete() {
			SuspensionDAL sDAL = new SuspensionDAL();
			sDAL.Delete(this.id);
		}

		protected Suspension FillFromDataSet(DataSet ds) {
			Suspension current = new Suspension();
			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
				current.id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
				current.userID = Convert.ToInt32(ds.Tables[0].Rows[0]["user_id"].ToString());
				current.permanent = ds.Tables[0].Rows[0]["is_permanent"].ToString() == "1";
				current.expiration = Convert.ToDateTime(ds.Tables[0].Rows[0]["expiration"].ToString());
			}
			return current;
		}

		public bool LoadById() {
			SuspensionDAL sDAL = new SuspensionDAL();
			Suspension temp = FillFromDataSet(sDAL.LoadById(this.id));
			if (temp.id > 0) {
				this.userID = temp.userID;
				this.permanent = temp.permanent;
				this.expiration = temp.expiration;
				return true;
			} else {
				return false;
			}
		}

		public void LoadByUser() {
			SuspensionDAL sDAL = new SuspensionDAL();
			Suspension temp = FillFromDataSet(sDAL.LoadByUser(this.userID));
			this.id = temp.id;
			this.permanent = temp.permanent;
			this.expiration = temp.expiration;
		}
	}
}
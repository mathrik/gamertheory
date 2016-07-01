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
/// Collections are lists of games/articles/whatever
/// e.g. favorite games, most hated reviews
/// </summary>
namespace gamer {
	public class Collection {
		private int _id;
		private int _type;
		private int _object;
		private int _user;
		private string _comment;
		private string _typetitle;

		public int id {
			get { return _id; }
			set { _id = value; }
		}

		public int typeID {
			get { return _type; }
			set { _type = value; }
		}

		public int objectID {
			get { return _object; }
			set { _object = value; }
		}

		public int userID {
			get { return _user; }
			set { _user = value; }
		}

		public string comment {
			get { return _comment; }
			set { _comment = value; }
		}

		/// <summary>
		/// Although this is publicly "writable", this field is never written to the database
		/// </summary>
		public string typetitle {
			get { return _typetitle; }
			set { _typetitle = value; }
		}


		public Collection() {
			//
			// TODO: Add constructor logic here
			//
		}

		public void Add() {
			CollectionDAL cDAL = new CollectionDAL();
			this.id = cDAL.Add(this.typeID, this.objectID, this.userID, this.comment);
		}

		public void Update() {
			CollectionDAL cDAL = new CollectionDAL();
			cDAL.Update(this.id, this.typeID, this.objectID, this.userID, this.comment);
		}

		public void Delete() {
			CollectionDAL cDAL = new CollectionDAL();
			cDAL.Delete(this.id);
		}

		protected List<Collection> FillListFromDataSet(DataSet ds) {
			List<Collection> myList = new List<Collection>();
			if (ds.Tables.Count > 0) {
				for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {
					Collection current_item = new Collection();
					current_item.id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
					current_item.objectID = Convert.ToInt32(ds.Tables[0].Rows[i]["object_id"].ToString());
					current_item.typeID = Convert.ToInt32(ds.Tables[0].Rows[i]["collectiontype_id"].ToString());
					current_item.userID = Convert.ToInt32(ds.Tables[0].Rows[i]["user_id"].ToString());
					current_item.comment = ds.Tables[0].Rows[i]["comment"].ToString();
					try {
						current_item.typetitle = ds.Tables[0].Rows[i]["title"].ToString();
					} catch {
						current_item.typetitle = "";
					}
					myList.Add(current_item);
				}
			}
			return myList;
		}

		/// <summary>
		/// 1 = game
		/// </summary>
		/// <param name="user"></param>
		/// <param name="typeID"></param>
		/// <returns></returns>
		public List<Collection> GetCollection(int user, int typeID) {
			CollectionDAL cDAL = new CollectionDAL();
			return FillListFromDataSet(cDAL.GetCollection(user, typeID));
		}

		public void RemoveItemFromCollection(int user, int typeID, int objectID) {
			CollectionDAL cDAL = new CollectionDAL();
			cDAL.RemoveItemFromCollection(user, typeID, objectID);
		}
	}
}
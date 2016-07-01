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
/// Summary description for Category
/// </summary>
namespace forum {
	public class Category {
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

		public Category() {
			//
			// TODO: Add constructor logic here
			//
		}

		public void Add() {
			forum.CategoryDAL cDAL = new CategoryDAL();
			this.id = cDAL.Add(this.title);
		}

		public void Update() {
			forum.CategoryDAL cDAL = new CategoryDAL();
			cDAL.Update(this.id, this.title);
		}

		public void Delete() {
			CategoryDAL cDAL = new CategoryDAL();
			cDAL.Delete(this.id);
		}

		protected List<Category> FillListFromDataSet(DataSet ds) {
			List<Category> myList = new List<Category>();
			Category cat;
			for (int i = 0; ds.Tables.Count > 0 && i < ds.Tables[0].Rows.Count; i++) {
				cat = new Category();
				cat.id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
				cat.title = ds.Tables[0].Rows[i]["title"].ToString();
				myList.Add(cat);
			}
			return myList;
		}

		public bool LoadByID() {
			CategoryDAL cDAL = new CategoryDAL();
			DataSet ds = cDAL.LoadByID(this.id);
			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
				this.id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
				this.title = ds.Tables[0].Rows[0]["title"].ToString();
				return true;
			} else {
				return false;
			}
		}

		public List<Category> LoadAll() {
			CategoryDAL cDAL = new CategoryDAL();
			return FillListFromDataSet(cDAL.LoadAll());
		}
	}
}

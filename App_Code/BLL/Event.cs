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
/// Summary description for Event
/// </summary>
namespace gamer {
	public class Event {
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

		public Event() {
			//
			// TODO: Add constructor logic here
			//
		}

		public void Add() {
			EventDAL eventDAL = new EventDAL();
			this.id = eventDAL.Add(this.title);
		}

		public void Update() {
			EventDAL eventDAL = new EventDAL();
			eventDAL.Update(this.id, this.title);
		}

		public void Delete() {
			EventDAL eventDAL = new EventDAL();
			eventDAL.Delete(this.id);
		}

		protected List<Event> FillListFromDataSet(DataSet ds) {
			List<Event> myList = new List<Event>();
			for (int i = 0; ds.Tables.Count > 0 && i < ds.Tables[0].Rows.Count; i++) {
				Event myEvent = new Event();
				myEvent.id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
				myEvent.title = ds.Tables[0].Rows[i]["title"].ToString();
				myList.Add(myEvent);
			}
			return myList;
		}

		public bool LoadByID() {
			EventDAL eventDAL = new EventDAL();
			DataSet ds = eventDAL.LoadByID(this.id);
			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
				this.id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
				this.title = ds.Tables[0].Rows[0]["title"].ToString();
				return true;
			} else {
				return false;
			}
		}

		public List<Event> GetAllEvents() {
			EventDAL eventDAL = new EventDAL();
			return FillListFromDataSet(eventDAL.GetAllEvents());
		}
	}
}
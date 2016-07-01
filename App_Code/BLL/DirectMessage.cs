using System;
using System.Collections;
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
/// Summary description for DirectMessage
/// </summary>
namespace gamer {
	public class DirectMessage {
		private int _id;
		private string _title;
		private string _body;
		private int _sender;
		private int _recipient;
		private bool _isread;

		public int id {
			get { return _id; }
			set { _id = value; }
		}

		public string title {
			get { return _title; }
			set { _title = value; }
		}

		public string body {
			get { return _body; }
			set { _body = value; }
		}

		public int sender {
			get { return _sender; }
			set { _sender = value; }
		}

		public int recipient {
			get { return _recipient; }
			set { _recipient = value; }
		}

		public bool isread {
			get { return _isread; }
			set { _isread = value; }
		}

		public DirectMessage() {
			//
			// TODO: Add constructor logic here
			//
		}

		public void Add() {
			DirectMessageDAL msgDAL = new DirectMessageDAL();
			this.id = msgDAL.Add(this.title,
				this.body,
				this.sender,
				this.recipient,
				Convert.ToInt32(this.isread));
		}

		public void Update() {
			DirectMessageDAL msgDAL = new DirectMessageDAL();
			msgDAL.Update(this.id,
				this.title,
				this.body,
				this.sender,
				this.recipient,
				Convert.ToInt32(this.isread));
		}

		public void Delete() {
			DirectMessageDAL msgDAL = new DirectMessageDAL();
			msgDAL.Delete(this.id);
		}

		protected List<DirectMessage> LoadFromDataSet(DataSet ds) {
			List<DirectMessage> myList = new List<DirectMessage>();
			if (ds.Tables.Count > 0) {
				for (int i = 0; i < ds.Tables[0].Rows.Count; i++) {
					DirectMessage msg = new DirectMessage();
					msg.id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
					msg.title = ds.Tables[0].Rows[i]["title"].ToString();
					msg.body = ds.Tables[0].Rows[i]["body"].ToString();
					msg.sender = Convert.ToInt32(ds.Tables[0].Rows[i]["sender_id"].ToString());
					msg.recipient = Convert.ToInt32(ds.Tables[0].Rows[i]["recipient_id"].ToString());
					msg.isread = ds.Tables[0].Rows[i]["isread"].ToString() == "1";
					myList.Add(msg);
				}
			}
			return myList;
		}

		public bool LoadById() {
			DirectMessageDAL msgDAL = new DirectMessageDAL();
			DataSet ds = msgDAL.LoadByID(this.id);
			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
				this.id = Convert.ToInt32(ds.Tables[0].Rows[0]["id"].ToString());
				this.title = ds.Tables[0].Rows[0]["title"].ToString();
				this.body = ds.Tables[0].Rows[0]["body"].ToString();
				this.sender = Convert.ToInt32(ds.Tables[0].Rows[0]["sender_id"].ToString());
				this.recipient = Convert.ToInt32(ds.Tables[0].Rows[0]["recipient_id"].ToString());
				this.isread = ds.Tables[0].Rows[0]["isread"].ToString() == "1";
				return true;
			} else {
				return false;
			}
		}
	}
}
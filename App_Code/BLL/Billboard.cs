using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

/// <summary>
/// Summary description for Billboard
/// </summary>
public class Billboard {
	private int _id;
	private string _url;
	private string _filename;

	public int id {
		get { return _id; }
		set { _id = value; }
	}

	public string url {
		get { return _url; }
		set { _url = value; }
	}

	public string filename {
		get { return _filename; }
		set { _filename = value; }
	}

	public Billboard() {
		//
		// TODO: Add constructor logic here
		//
	}

	public void Update() {
		BillboardDAL bbDAL = new BillboardDAL();
		bbDAL.Update(this);
	}

	protected List<Billboard> ListFromDS(DataSet ds) {
		List<Billboard> mylist = new List<Billboard>();
		Billboard mybb;
		Toolbox tools = new Toolbox();
		for (int i = 0; ds.Tables.Count > 0 && i < ds.Tables[0].Rows.Count; i++) {
			mybb = new Billboard();
			mybb.id = tools.getInt(ds.Tables[0].Rows[i]["id"].ToString());
			mybb.url = ds.Tables[0].Rows[i]["URL"].ToString();
			mybb.filename = ds.Tables[0].Rows[i]["Filename"].ToString();
			mylist.Add(mybb);
		}
		return mylist;
	}

	public bool LoadByID(int id) {
		BillboardDAL bbDAL = new BillboardDAL();
		List<Billboard> mylist = ListFromDS(bbDAL.LoadByID(id));
		if (mylist.Count > 0) {
			this.id = mylist[0].id;
			this.url = mylist[0].url;
			this.filename = mylist[0].filename;
			return true;
		} else {
			return false;
		}
	}
}
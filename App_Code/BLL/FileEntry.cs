using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;

/// <summary>
/// Summary description for FileEntry
/// </summary>
public class FileEntry {
	private int _id;
	private string _title;
	private string _filename;
	private DateTime _filedate;
	private int _rank;
	private int _pageID;
	private string _url;

	public int id {
		get { return _id; }
		set { _id = value; }
	}

	public string title {
		get { return _title; }
		set { _title = value; }
	}

	public string filename {
		get { return _filename; }
		set { _filename = value; }
	}

	public DateTime filedate {
		get { return _filedate; }
		set { _filedate = value; }
	}

	public int rank {
		get { return _rank; }
		set { _rank = value; }
	}

	public int pageID {
		get { return _pageID; }
		set { _pageID = value; }
	}

	public string url {
		get { return _url; }
		set { _url = value; }
	}


	public FileEntry() {
		//
		// TODO: Add constructor logic here
		//
	}

	public void Add() {
		FileEntryDAL fDAL = new FileEntryDAL();
		this.id = fDAL.Add(this);
	}

	public void Update() {
		FileEntryDAL fDAL = new FileEntryDAL();
		fDAL.Update(this);
	}

	public void Delete() {
		//first delete file then remove entry in Database
		if (this.filename != null && this.filename != "") {
			string filePath;
			try {
				filePath = System.Configuration.ConfigurationManager.AppSettings["uploadPath"];
				if (File.Exists(filePath + "\\" + this.filename)) {
					File.Delete(filePath + "\\" + this.filename);
				}
			} catch (Exception ex) {
				Console.WriteLine(ex.ToString());
			}
		}
		FileEntryDAL fDAL = new FileEntryDAL();
		fDAL.Delete(this.id);
	}

	public void RemoveFile() {
		//first delete file then remove entry in Database
		if (this.filename != null && this.filename != "") {
			string filePath;
			try {
				filePath = System.Configuration.ConfigurationManager.AppSettings["uploadPath"];
				if (File.Exists(filePath + "\\" + this.filename)) {
					File.Delete(filePath + "\\" + this.filename);
				}
			} catch (Exception ex) {
				Console.WriteLine(ex.ToString());
			}
		}
		this.filename = "";
		this.Update();
	}


	private List<FileEntry> getListFromDataset(DataSet ds) {
		List<FileEntry> list = new List<FileEntry>();
		FileEntry myfile;
		Toolbox tools = new Toolbox();
		for (int i = 0; ds.Tables.Count > 0 && i < ds.Tables[0].Rows.Count; i++) {
			myfile = new FileEntry();
			myfile.id = tools.getInt(ds.Tables[0].Rows[i]["id"].ToString());
			myfile.pageID = tools.getInt(ds.Tables[0].Rows[i]["StoryID"].ToString());
			myfile.title = ds.Tables[0].Rows[i]["title"].ToString();
			myfile.filename = ds.Tables[0].Rows[i]["filename"].ToString();
			myfile.filedate = Convert.ToDateTime(ds.Tables[0].Rows[i]["filedate"].ToString());
			myfile.rank = tools.getInt(ds.Tables[0].Rows[i]["Rank"].ToString());
			myfile.url = ds.Tables[0].Rows[i]["URL"].ToString();
			list.Add(myfile);
		}
		return list;
	}

	public bool LoadById(int id) {
		FileEntryDAL fDAL = new FileEntryDAL();
		DataSet ds = fDAL.FetchById(id);
		Toolbox tools = new Toolbox();
		if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
			this.id = tools.getInt(ds.Tables[0].Rows[0]["id"].ToString());
			this.pageID = tools.getInt(ds.Tables[0].Rows[0]["StoryID"].ToString());
			this.title = ds.Tables[0].Rows[0]["title"].ToString();
			this.filename = ds.Tables[0].Rows[0]["filename"].ToString();
			this.filedate = Convert.ToDateTime(ds.Tables[0].Rows[0]["filedate"].ToString());
			this.rank = tools.getInt(ds.Tables[0].Rows[0]["Rank"].ToString());
			this.url = ds.Tables[0].Rows[0]["URL"].ToString();
			return true;
		} else {
			return false;
		}
	}

	public List<FileEntry> FetchTinyMCEFiles(int pageID) {
		FileEntryDAL fDAL = new FileEntryDAL();
		return getListFromDataset(fDAL.FetchTinyMCEFiles(pageID));
	}

	public List<FileEntry> FetchByPage(int pageID) {
		FileEntryDAL fDAL = new FileEntryDAL();
		return getListFromDataset(fDAL.FetchByPage(pageID));
	}

	public List<FileEntry> FetchByPageAlpha(int pageID) {
		FileEntryDAL fDAL = new FileEntryDAL();
		return getListFromDataset(fDAL.FetchByPageAlpha(pageID));
	}

	public List<FileEntry> FetchByPageDate(int pageID) {
		FileEntryDAL fDAL = new FileEntryDAL();
		return getListFromDataset(fDAL.FetchByPageDate(pageID));
	}

	public List<FileEntry> SearchByKeyphrase(string keyphrase) {
		FileEntryDAL fDAL = new FileEntryDAL();
		return getListFromDataset(fDAL.SearchByKeyphrase(keyphrase));
	}

	public int GetHighestRank() {
		FileEntryDAL fDAL = new FileEntryDAL();
		return fDAL.GetHighestRank();
	}

	public void rankDown() {
		FileEntryDAL fDAL = new FileEntryDAL();
		int newID = fDAL.GetNextRankDown(this.id);
		FileEntry newFile = new FileEntry();
		if (newFile.LoadById(newID)) {
			int rankholder = newFile.rank;
			newFile.rank = this.rank;
			newFile.Update();
			this.rank = rankholder;
			this.Update();
		}
	}

	public void rankUp() {
		FileEntryDAL fDAL = new FileEntryDAL();
		int newID = fDAL.GetNextRankUp(this.id);
		FileEntry newFile = new FileEntry();
		if (newFile.LoadById(newID)) {
			int rankholder = newFile.rank;
			newFile.rank = this.rank;
			newFile.Update();
			this.rank = rankholder;
			this.Update();
		}
	}
}

using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;

/// <summary>
/// Summary description for FileEntryDAL
/// </summary>
public class FileEntryDAL : DataAccess {
	public FileEntryDAL() {
		//
		// TODO: Add constructor logic here
		//
	}

	public int Add(FileEntry fe) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "INSERT INTO CMS_File (title, filename, filedate, rank, StoryID, URL) VALUES (" +
			"@title, @filename, @filedate, @rank, @pageID, @url); SELECT @@IDENTITY;";
		cmd.Parameters.AddWithValue("@title", fe.title);
		cmd.Parameters.AddWithValue("@filename", fe.filename);
		cmd.Parameters.AddWithValue("@filedate", fe.filedate);
		cmd.Parameters.AddWithValue("@rank", fe.rank);
		cmd.Parameters.AddWithValue("@pageID", fe.pageID);
		cmd.Parameters.AddWithValue("@url", fe.url);
		return execScalar(cmd);
	}

	public void Update(FileEntry fe) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "UPDATE CMS_File SET " +
			"title=@title, " +
			"filename=@filename, " +
			"filedate=@filedate, " +
			"rank=@rank, " +
			"StoryID=@pageID, " +
			"URL=@url " +
			"WHERE id=@id";
		cmd.Parameters.AddWithValue("@title", fe.title);
		cmd.Parameters.AddWithValue("@filename", fe.filename);
		cmd.Parameters.AddWithValue("@filedate", fe.filedate);
		cmd.Parameters.AddWithValue("@rank", fe.rank);
		cmd.Parameters.AddWithValue("@id", fe.id);
		cmd.Parameters.AddWithValue("@pageID", fe.pageID);
		cmd.Parameters.AddWithValue("@url", fe.url);
		execNonQuery(cmd);
	}

	public void Delete(int id) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "DELETE FROM CMS_File WHERE id=@id";
		cmd.Parameters.AddWithValue("@id", id);
		execNonQuery(cmd);
	}

	public DataSet FetchById(int id) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT * FROM CMS_File WHERE id=@id";
		cmd.Parameters.AddWithValue("@id", id);
		return fillDataSet(cmd);
	}

	public DataSet FetchTinyMCEFiles(int id) {
		SqlCommand cmd = new SqlCommand();
		//HttpContext.Current.Response.Write("SELECT * FROM CMS_File WHERE tinyMCEFile=1 AND CMS_PageID = "+id+" ORDER BY rank");
		cmd.CommandText = "SELECT * FROM CMS_File WHERE StoryID = @id ORDER BY rank";
		cmd.Parameters.AddWithValue("@id", id);
		return fillDataSet(cmd);
	}

	public DataSet FetchByPage(int id) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT * FROM CMS_File WHERE StoryID = @id ORDER BY rank";
		cmd.Parameters.AddWithValue("@id", id);
		return fillDataSet(cmd);
	}

	public DataSet FetchByPageAlpha(int id) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT * FROM CMS_File WHERE StoryID = @id ORDER BY title, rank";
		cmd.Parameters.AddWithValue("@id", id);
		return fillDataSet(cmd);
	}

	public DataSet FetchByPageDate(int id) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT * FROM CMS_File WHERE StoryID = @id ORDER BY filedate DESC";
		cmd.Parameters.AddWithValue("@id", id);
		return fillDataSet(cmd);
	}

	public DataSet SearchByKeyphrase(string searchphrase) {
		SqlCommand cmd = new SqlCommand();
		string sql = "SELECT * FROM CMS_File WHERE ";
		try {
			string[] term_array = searchphrase.Split(' ');
			for (int i = 0; i < term_array.Length; i++) {
				if (i == 0) {
					sql += " title LIKE @term" + i + " OR filename LIKE @term" + i + "b ";
				} else {
					sql += " OR title LIKE @term" + i + " OR filename LIKE @term" + i + "b ";
				}
				cmd.Parameters.AddWithValue("@term" + i + "b", "%" + term_array[i] + "%");
				cmd.Parameters.AddWithValue("@term" + i, "%" + term_array[i]);
			}
			cmd.CommandText = sql;
			return fillDataSet(cmd);
		} catch {
			DataSet ds = new DataSet();
			return ds;
		}
	}

	public int GetHighestRank() {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT TOP 1 rank FROM CMS_File ORDER BY rank DESC";
		return execScalar(cmd);
	}

	public int GetNextRankDown(int fileID) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT TOP 1 f1.id FROM CMS_File f1 WHERE f1.rank >= " +
			"(SELECT f2.rank FROM CMS_File f2 WHERE f2.id = @fileID) " +
			"AND f1.CMS_PageID = (SELECT f3.CMS_PageID FROM CMS_File f3 WHERE f3.id = @fileID) " +
			"AND f1.id <> @fileID ORDER BY rank ASC";
		cmd.Parameters.AddWithValue("@fileID", fileID);
		return execScalar(cmd);
	}

	public int GetNextRankUp(int fileID) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT TOP 1 f1.id FROM CMS_File f1 WHERE f1.rank <= " +
			"(SELECT f2.rank FROM CMS_File f2 WHERE f2.id = @fileID) " +
			"AND f1.CMS_PageID = (SELECT f3.CMS_PageID FROM CMS_File f3 WHERE f3.id = @fileID) " +
			"AND f1.id <> @fileID ORDER BY rank DESC";
		cmd.Parameters.AddWithValue("@fileID", fileID);
		return execScalar(cmd);
	}
}

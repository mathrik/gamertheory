using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for UserFileDAL
/// </summary>
public class UserFileDAL : DataAccess {
	public UserFileDAL() {
		//
		// TODO: Add constructor logic here
		//
	}

	public int Add(string filename, int user, int filetype) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "INSERT INTO fileUpload (filename, user_id, filetype_id) VALUES (" +
			"@filename, @user, @filetype); SELECT @@IDENTITY;";
		cmd.Parameters.AddWithValue("@filename", filename);
		cmd.Parameters.AddWithValue("@user", user);
		cmd.Parameters.AddWithValue("@filetype", filetype);
		return execScalar(cmd);
	}

	public void Update(int id, string filename, int user, int filetype) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "UPDATE fileUpload SET " +
			"filename=@filename, " +
			"user_id=@user, " +
			"filetype_id=@filetype " +
			"WHERE id=@id";
		cmd.Parameters.AddWithValue("@id", id);
		cmd.Parameters.AddWithValue("@filename", filename);
		cmd.Parameters.AddWithValue("@user", user);
		cmd.Parameters.AddWithValue("@filetype", filetype);
		execNonQuery(cmd);
	}

	public void Delete(int id) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "DELETE FROM fileUpload WHERE id=@id";
		cmd.Parameters.AddWithValue("@id", id);
		execNonQuery(cmd);
	}

	public DataSet LoadById(int id) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT * FROM fileUpload WHERE id=@id";
		cmd.Parameters.AddWithValue("@id", id);
		return fillDataSet(cmd);
	}

	public DataSet LoadByUser(int user) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT * FROM fileUpload WHERE user_id=@user";
		cmd.Parameters.AddWithValue("@user", user);
		return fillDataSet(cmd);
	}
}

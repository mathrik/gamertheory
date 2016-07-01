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
/// Summary description for PreferenceDAL
/// </summary>
public class PreferenceDAL : DataAccess {
	public PreferenceDAL() {
		//
		// TODO: Add constructor logic here
		//
	}

	public int Add(int typeID, int userID, int value) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "INSERT INTO preferences (preferencetype_id, user_id, value) VALUES (" +
			"@typeID, @userID, @value); SELECT @@IDENTITY;";
		cmd.Parameters.AddWithValue("@typeID", typeID);
		cmd.Parameters.AddWithValue("@userID", userID);
		cmd.Parameters.AddWithValue("@value", value);
		return execScalar(cmd);
	}

	public void Update(int id, int typeID, int userID, int value) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "UPDATE preferences SET " +
			"preferencetype_id=@typeID, " +
			"user_id=@userID, " +
			"value=@value " +
			"WHERE id=@id";
		cmd.Parameters.AddWithValue("@id", id);
		cmd.Parameters.AddWithValue("@typeID", typeID);
		cmd.Parameters.AddWithValue("@userID", userID);
		cmd.Parameters.AddWithValue("@value", value);
		execNonQuery(cmd);
	}

	public void Delete(int id) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "DELETE FROM preferences WHERE id=@id";
		cmd.Parameters.AddWithValue("@id", id);
		execNonQuery(cmd);
	}

	public DataSet GetUserPreferences(int userID) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT p.*, t.title FROM preferences p INNER JOIN preferencetype t " +
			"ON p.preferencetype_id = t.id WHERE p.user_id=@userID";
		cmd.Parameters.AddWithValue("@userID", userID);
		return fillDataSet(cmd);
	}
}

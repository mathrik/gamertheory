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
/// Summary description for SuspensionDAL
/// </summary>
public class SuspensionDAL : DataAccess {
	public SuspensionDAL() {
		//
		// TODO: Add constructor logic here
		//
	}

    public int Add(int user, bool permanent, DateTime expiration) {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "INSERT INTO suspension (user_id, is_permanent, expiration) VALUES (" +
            "@user, @permanent, @expiration); SELECT @@IDENTITY;";
        cmd.Parameters.AddWithValue("@user", user);
        cmd.Parameters.AddWithValue("@permanent", permanent);
        cmd.Parameters.AddWithValue("@expiration", expiration);
        return execScalar(cmd);
    }

    public void Update(int id, int user, bool permanent, DateTime expiration) {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "UPDATE suspension SET " +
            "user_id=@user, " +
            "is_permanent=@permanent, " +
            "expiration=@expiration " +
            "WHERE id=@id";
        cmd.Parameters.AddWithValue("@user", user);
        cmd.Parameters.AddWithValue("@permanent", permanent);
        cmd.Parameters.AddWithValue("@expiration", expiration);
        cmd.Parameters.AddWithValue("@id", id);
        execNonQuery(cmd);
    }

    public void Delete(int id) {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "DELETE FROM suspension WHERE id=@id";
        cmd.Parameters.AddWithValue("@id", id);
        execNonQuery(cmd);
    }

    public DataSet LoadById(int id) {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "SELECT * FROM suspension WHERE id=@id";
        cmd.Parameters.AddWithValue("@id", id);
        return fillDataSet(cmd);
    }

    public DataSet LoadByUser(int user) {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "SELECT * FROM suspension WHERE user_id=@user";
        cmd.Parameters.AddWithValue("@user", user);
        return fillDataSet(cmd);
    }
}

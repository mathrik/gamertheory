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
/// Summary description for CollectionDAL
/// </summary>
public class CollectionDAL : DataAccess {
	public CollectionDAL() {
		//
		// TODO: Add constructor logic here
		//
	}

    public int Add(int typeID, int objectID, int userID, string comment) {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "INSERT INTO collection " +
            "(collectiontype_id, object_id, user_id, comment) VALUES " +
            "(@typeID, @objectID, @userID, @comment); SELECT @@IDENTITY;";
        cmd.Parameters.AddWithValue("@typeID", typeID);
        cmd.Parameters.AddWithValue("@objectID", objectID);
        cmd.Parameters.AddWithValue("@userID", userID);
        cmd.Parameters.AddWithValue("@comment", comment);
        return execScalar(cmd);
    }

    public void Update(int id, int typeID, int objectID, int userID, string comment) {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "UPDATE collection SET " +
            "collectiontype_id=@typeID, " +
            "object_id=@objectID, " +
            "user_id=@userID, " +
            "comment=@comment " +
            "WHERE id=@id";
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@typeID", typeID);
        cmd.Parameters.AddWithValue("@objectID", objectID);
        cmd.Parameters.AddWithValue("@userID", userID);
        cmd.Parameters.AddWithValue("@comment", comment);
        execNonQuery(cmd);
    }

    public void Delete(int id) {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "DELETE FROM collection WHERE id=@id";
        cmd.Parameters.AddWithValue("@id", id);
        execNonQuery(cmd);
    }

    public DataSet GetCollection(int userID, int typeID) {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "SELECT c.*, t.title FROM collection c INNER JOIN collectiontype t " +
            "ON c.collectiontype_id = t.id WHERE c.user_id=@userID AND c.collectiontype_id=@typeID";
        cmd.Parameters.AddWithValue("@typeID", typeID);
        cmd.Parameters.AddWithValue("@userID", userID);
        return fillDataSet(cmd);
    }

	public void RemoveItemFromCollection(int user, int typeID, int objectID) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "DELETE FROM collection WHERE user_id=@userID AND collectiontype_id=@typeID AND object_id=@objectID";
		cmd.Parameters.AddWithValue("@typeID", typeID);
		cmd.Parameters.AddWithValue("@userID", user);
		cmd.Parameters.AddWithValue("@objectID", objectID);
		execNonQuery(cmd);
	}
}

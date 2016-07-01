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
/// Summary description for DirectMessageDAL
/// </summary>
public class DirectMessageDAL : DataAccess{
	public DirectMessageDAL() {
		//
		// TODO: Add constructor logic here
		//
	}

    public int Add(string title, string body, int sender, int recipient, int isread) {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "INSERT INTO directmessage (title, body, sender_id, recipient_id, isread) " +
            "VALUES (@title, @body, @sender, @recipient, @isread); SELECT @@IDENTITY";
        cmd.Parameters.AddWithValue("@title", title);
        cmd.Parameters.AddWithValue("@body", body);
        cmd.Parameters.AddWithValue("@sender", sender);
        cmd.Parameters.AddWithValue("@recipient", recipient);
        cmd.Parameters.AddWithValue("@isread", isread);
        return execScalar(cmd);
    }

    public void Update(int id, string title, string body, int sender, int recipient, int isread) {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "UDPATE directmessage SET " +
            "title=@title, " +
            "body=@body, " +
            "sender_id=@sender, " +
            "recipient_id=@recipient, " +
            "isread=@isread " +
            "WHERE id=@id";
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@title", title);
        cmd.Parameters.AddWithValue("@body", body);
        cmd.Parameters.AddWithValue("@sender", sender);
        cmd.Parameters.AddWithValue("@recipient", recipient);
        cmd.Parameters.AddWithValue("@isread", isread);
        execNonQuery(cmd);
    }

    public void Delete(int id) {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "DELETE FROM directmessage WHERE id=@id";
        cmd.Parameters.AddWithValue("@id", id);
        execNonQuery(cmd);
    }

    public DataSet LoadByID(int id) {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "SELECT * FROM directmessage WHERE id=@id";
        cmd.Parameters.AddWithValue("@id", id);
        return fillDataSet(cmd);
    }
}

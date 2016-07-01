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
/// Summary description for EventDAL
/// </summary>
public class EventDAL : DataAccess {
	public EventDAL() {
		//
		// TODO: Add constructor logic here
		//
	}

	public int Add(string title) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "INSERT INTO event (title) VALUES (@title); SELECT @@IDENTITY";
		cmd.Parameters.AddWithValue("@title", title);
		return execScalar(cmd);
	}

	public void Update(int id, string title) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "UPDATE event SET " +
			"title=@title " +
			"WHERE id=@id";
		cmd.Parameters.AddWithValue("@title", title);
		cmd.Parameters.AddWithValue("@id", id);
		execNonQuery(cmd);
	}

	public void Delete(int id) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "DELETE FROM event WHERE id=@id";
		cmd.Parameters.AddWithValue("@id", id);
		execNonQuery(cmd);
	}

	public DataSet LoadByID(int id) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT * FROM event WHERE id=@id";
		cmd.Parameters.AddWithValue("@id", id);
		return fillDataSet(cmd);
	}

	public DataSet GetAllEvents() {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT * FROM event";
		return fillDataSet(cmd);
	}
}

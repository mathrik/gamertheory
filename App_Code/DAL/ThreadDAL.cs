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
/// Summary description for ThreadDAL
/// </summary>
namespace forum {
	public class ThreadDAL : DataAccess {
		public ThreadDAL() {
			//
			// TODO: Add constructor logic here
			//
		}

		public int Add(string title, int sticky, int locked, int category) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "INSERT INTO forumthread (title, issticky, islocked, forumCategory_id) " +
				"VALUES (" +
				"(@title, @sticky, @islocked, @category); SELECT @@IDENTITY;";
			cmd.Parameters.AddWithValue("@title", title);
			cmd.Parameters.AddWithValue("@sticky", sticky);
			cmd.Parameters.AddWithValue("@islocked", locked);
			cmd.Parameters.AddWithValue("@category", category);
			return execScalar(cmd);
		}

		public void Update(int id, string title, int sticky, int locked, int category) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "UPDATE forumthread SET " +
				"title=@title, " +
				"issticky=@sticky, " +
				"islocked=@locked, " +
				"forumCategory_id=@category " +
				"WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			cmd.Parameters.AddWithValue("@title", title);
			cmd.Parameters.AddWithValue("@sticky", sticky);
			cmd.Parameters.AddWithValue("@islocked", locked);
			cmd.Parameters.AddWithValue("@category", category);
			execNonQuery(cmd);
		}

		public void Delete(int id) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "DELETE FROM forumthread WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			execNonQuery(cmd);
		}

		public DataSet LoadById(int id) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM forumthread WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			return fillDataSet(cmd);
		}

		public DataSet LoadByCategory(int category) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM forumthread WHERE forumCategory_id=@category";
			cmd.Parameters.AddWithValue("@category", category);
			return fillDataSet(cmd);
		}

		public DataSet LoadAll() {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM forumthread";
			return fillDataSet(cmd);
		}
	}
}

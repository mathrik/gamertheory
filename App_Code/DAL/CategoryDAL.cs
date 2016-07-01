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
/// Summary description for CategoryDAL
/// </summary>
namespace forum {
	public class CategoryDAL : DataAccess {
		public CategoryDAL() {
			//
			// TODO: Add constructor logic here
			//
		}

		public int Add(string title) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "INSERT INTO forumCategory (title) VALUES (@title); " +
				"SELECT @@IDENTITY;";
			cmd.Parameters.AddWithValue("@title", title);
			return execScalar(cmd);
		}

		public void Update(int id, string title) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "UPDATE forumCategory SET title=@title WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			cmd.Parameters.AddWithValue("@title", title);
			execNonQuery(cmd);
		}

		public void Delete(int id) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "DELETE FROM forumCategory WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			execNonQuery(cmd);
		}

		public DataSet LoadByID(int id) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM forumCategory WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			return fillDataSet(cmd);
		}

		public DataSet LoadAll() {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM forumCategory";
			return fillDataSet(cmd);
		}
	}
}

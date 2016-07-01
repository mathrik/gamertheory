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
/// Summary description for DeveloperDAL
/// </summary>
namespace gamer {
	public class DeveloperDAL : DataAccess {
		public DeveloperDAL() {
			//
			// TODO: Add constructor logic here
			//
		}

		public int Add(string title) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "INSERT INTO developer (title) VALUES (@title); SELECT @@IDENTITY;";
			cmd.Parameters.AddWithValue("@title", title);
			return execScalar(cmd);
		}

		public void Update(int id, string title) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "UPDATE developer SET title=@title WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id); ;
			cmd.Parameters.AddWithValue("@title", title);
			execNonQuery(cmd);
		}

		public void Delete(int id) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "DELETE FROM developer WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			execNonQuery(cmd);
		}

		public DataSet LoadByID(int id) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM developer WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			return fillDataSet(cmd);
		}

		public DataSet LoadAll() {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM developer ORDER BY title";
			return fillDataSet(cmd);
		}

		public DataSet LoadStoryDevelopers(int storyID) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM developer WHERE id IN " +
				"(SELECT developer_id FROM story_developer WHERE story_id = @storyID)";
			cmd.Parameters.AddWithValue("@storyID", storyID);
			return fillDataSet(cmd);
		}

		public DataSet LoadGameDevelopers(int instanceID) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM developer WHERE id IN " +
				"(SELECT developer_id FROM game_developer WHERE game_specifics_id = @instanceID)";
			cmd.Parameters.AddWithValue("@instanceID", instanceID);
			return fillDataSet(cmd);
		}

		public int FetchDeveloperFromTitle(string title) {
			SqlCommand cmd = new SqlCommand();
			int developerID;
			cmd.CommandText = "SELECT * FROM developer WHERE title = @title";
			cmd.Parameters.AddWithValue("@title", title);
			developerID = execScalar(cmd);
			if (developerID == 0) {
				return Add(title);
			} else {
				return developerID;
			}
		}
	}
}
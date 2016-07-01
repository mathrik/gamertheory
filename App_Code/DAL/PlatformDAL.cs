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
/// Summary description for PlatformDAL
/// </summary>
namespace gamer {
	public class PlatformDAL : DataAccess {
		public PlatformDAL() {
			//
			// TODO: Add constructor logic here
			//
		}

		public int Add(string title, string color) {
			SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO platform (title, color) VALUES (@title, @color); SELECT @@IDENTITY;";
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@color", color);
			return execScalar(cmd);
		}

        public void Update(int id, string title, string color) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "UPDATE platform SET title=@title, color=@color WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id); ;
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@color", color);
			execNonQuery(cmd);
		}

		public void Delete(int id) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "DELETE FROM platform WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			execNonQuery(cmd);
		}

		public DataSet LoadByID(int id) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM platform WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			return fillDataSet(cmd);
		}

		public DataSet LoadAll() {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM platform ORDER BY title";
			return fillDataSet(cmd);
		}

		public DataSet LoadStoryPlatforms(int storyID) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM platform WHERE id IN " +
				"(SELECT platform_id FROM story_platform WHERE story_id = @storyID)";
			cmd.Parameters.AddWithValue("@storyID", storyID);
			return fillDataSet(cmd);
		}

		public int FetchPlatformFromTitle(string title) {
			SqlCommand cmd = new SqlCommand();
			int platformID;
			cmd.CommandText = "SELECT * FROM platform WHERE title = @title";
			cmd.Parameters.AddWithValue("@title", title);
			platformID = execScalar(cmd);
			if (platformID == 0 && title.Length > 0) {
				return Add(title, "#2b2b2b");
			} else {
				return platformID;
			}
		}
	}
}
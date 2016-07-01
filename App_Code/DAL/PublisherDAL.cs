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
/// Summary description for PublisherDAL
/// </summary>
namespace gamer {
	public class PublisherDAL : DataAccess {
		public PublisherDAL() {
			//
			// TODO: Add constructor logic here
			//
		}

		public int Add(string title) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "INSERT INTO publisher (title) VALUES (@title); SELECT @@IDENTITY;";
			cmd.Parameters.AddWithValue("@title", title);
			return execScalar(cmd);
		}

		public void Update(int id, string title) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "UPDATE publisher SET title=@title WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id); ;
			cmd.Parameters.AddWithValue("@title", title);
			execNonQuery(cmd);
		}

		public void Delete(int id) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "DELETE FROM publisher WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			execNonQuery(cmd);
		}

		public DataSet LoadByID(int id) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM publisher WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			return fillDataSet(cmd);
		}

		public DataSet LoadAll() {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM publisher ORDER BY title";
			return fillDataSet(cmd);
		}

		public DataSet LoadStoryPublishers(int storyID) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM publisher WHERE id IN " +
				"(SELECT publisher_id FROM story_publisher WHERE story_id = @storyID)";
			cmd.Parameters.AddWithValue("@storyID", storyID);
			return fillDataSet(cmd);
		}

		public DataSet LoadGamePublishers(int instanceID) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM publisher WHERE id IN " +
				"(SELECT publisher_id FROM game_publisher WHERE game_specifics_id = @instanceID)";
			cmd.Parameters.AddWithValue("@instanceID", instanceID);
			return fillDataSet(cmd);
		}

		public int FetchPublisherFromTitle(string title) {
			SqlCommand cmd = new SqlCommand();
			int publisherID;
			cmd.CommandText = "SELECT * FROM publisher WHERE title = @title";
			cmd.Parameters.AddWithValue("@title", title);
			publisherID = execScalar(cmd);
			if (publisherID == 0) {
				return Add(title);
			} else {
				return publisherID;
			}
		}
	}
}
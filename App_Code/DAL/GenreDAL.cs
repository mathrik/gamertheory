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
/// Summary description for GenreDAL
/// </summary>
namespace gamer {
	public class GenreDAL : DataAccess {
		public GenreDAL() {
			//
			// TODO: Add constructor logic here
			//
		}

		public int Add(string title) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "INSERT INTO genre (title) VALUES (@title); SELECT @@IDENTITY;";
			cmd.Parameters.AddWithValue("@title", title);
			return execScalar(cmd);
		}

		public void Update(int id, string title) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "UPDATE genre SET title=@title WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id); ;
			cmd.Parameters.AddWithValue("@title", title);
			execNonQuery(cmd);
		}

		public void Delete(int id) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "DELETE FROM genre WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			execNonQuery(cmd);
		}

		public DataSet LoadByID(int id) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM genre WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			return fillDataSet(cmd);
		}

		public DataSet LoadAll() {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM genre ORDER BY title";
			return fillDataSet(cmd);
		}

		public DataSet LoadStoryGenres(int storyID) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM genre WHERE id IN " +
				"(SELECT genre_id FROM story_genre WHERE story_id = @storyID)";
			cmd.Parameters.AddWithValue("@storyID", storyID);
			return fillDataSet(cmd);
		}

		public DataSet LoadGameGenres(int instanceID) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM genre WHERE id IN " +
				"(SELECT genre_id FROM game_genre WHERE game_id = @instanceID)";
			cmd.Parameters.AddWithValue("@instanceID", instanceID);
			return fillDataSet(cmd);
		}

		public int FetchGenreFromTitle(string title) {
			SqlCommand cmd = new SqlCommand();
			int genreID;
			cmd.CommandText = "SELECT * FROM genre WHERE title = @title";
			cmd.Parameters.AddWithValue("@title", title);
			genreID = execScalar(cmd);
			if (genreID == 0) {
				return Add(title);
			} else {
				return genreID;
			}
		}
	}
}
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
/// Summary description for GameDAL
/// </summary>
namespace gamer {
	public class GameDAL : DataAccess {
		public GameDAL() {
			//
			// TODO: Add constructor logic here
			//
		}

		public int Add(string title, string notes, int approved, DateTime dateAdded, int submitter) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "INSERT INTO game (title, notes, is_approved, date_added, submitter) VALUES (" +
				"@title, @notes, @approved, @dateAdded, @submitter); SELECT @@IDENTITY;";
			cmd.Parameters.AddWithValue("@title", title);
			cmd.Parameters.AddWithValue("@notes", notes);
			cmd.Parameters.AddWithValue("@approved", approved);
			cmd.Parameters.AddWithValue("@dateAdded", dateAdded);
			cmd.Parameters.AddWithValue("@submitter", submitter);
			return execScalar(cmd);
		}

		public void Update(int id, string title, string notes, int approved, DateTime dateAdded, int submitter) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "UPDATE game SET " +
				"title=@title, " +
				"notes=@notes, " +
				"is_approved=@approved, " +
				"date_added=@dateAdded, " +
				"submitter=@submitter " +
				"WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			cmd.Parameters.AddWithValue("@title", title);
			cmd.Parameters.AddWithValue("@notes", notes);
			cmd.Parameters.AddWithValue("@approved", approved);
			cmd.Parameters.AddWithValue("@dateAdded", dateAdded);
			cmd.Parameters.AddWithValue("@submitter", submitter);
			execNonQuery(cmd);
		}

		public void Delete(int id) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "DELETE FROM game WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			execNonQuery(cmd);
		}

		public DataSet LoadByID(int id) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM game WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			return fillDataSet(cmd);
		}

		public DataSet GetScreenshots(int id) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM game_screenshot WHERE game_id=@id ORDER BY rank";
			cmd.Parameters.AddWithValue("@id", id);
			return fillDataSet(cmd);
		}

		public DataSet LoadAll() {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM game ORDER BY title";
			return fillDataSet(cmd);
		}

		public DataSet SearchByTitle(string query) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM game WHERE title LIKE @query ORDER BY title";
			cmd.Parameters.AddWithValue("@query", "%" + query + "%");
			return fillDataSet(cmd);
		}

		public DataSet AutocompleteByTitle(string query) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM game WHERE title LIKE @query ORDER BY title";
			cmd.Parameters.AddWithValue("@query", query + "%");
			return fillDataSet(cmd);
		}

		public DataSet LoadStoryGames(int storyID) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM game WHERE id IN " +
				"(SELECT game_id FROM story_game WHERE story_id = @storyID)";
			cmd.Parameters.AddWithValue("@storyID", storyID);
			return fillDataSet(cmd);
		}

		public int FetchGameFromTitle(string title) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM game WHERE title = @title";
			cmd.Parameters.AddWithValue("@title", title);
			return execScalar(cmd);
		}

        public DataSet LoadRecent(int count) {
            SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT TOP " + count.ToString() + " * FROM game WHERE is_approved = 1 ORDER BY date_added DESC";
            return fillDataSet(cmd);
        }

        public DataSet LoadRecentByPlatform(int count, int platform) {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT TOP " + count.ToString() + " g.* FROM game g INNER JOIN game_specifics s ON g.id = s.game_id " +
				" WHERE g.is_approved = 1 AND s.platform_id  = " + platform.ToString() + " ORDER BY date_added DESC";
            return fillDataSet(cmd);
        }

		public DataSet LoadFromCollection(int numperpage, int page, int userID) {
			SqlCommand cmd = new SqlCommand();
			int begin;
			int end;
			begin = (numperpage * (page - 1)) + 1;
			end = (numperpage * page) + 1;
			cmd.CommandText = "SELECT * FROM " +
				"(SELECT Row_Number() OVER (ORDER BY g.date_added DESC) as RowIndex, g.* FROM game g INNER JOIN game_specifics s ON g.id = s.game_id " +
				"INNER JOIN collection c ON c.object_id = s.id WHERE c.collectiontype_id = 1 AND c.user_id = @userID) as Sub " +
				"Where Sub.RowIndex >= " + begin + " and Sub.RowIndex < " + end;
			cmd.Parameters.AddWithValue("@userID", userID);
			return fillDataSet(cmd);
		}

		public DataSet LoadPagedGames(int page, int numperpage) {
			SqlCommand cmd = new SqlCommand();
			int begin;
			int end;
			begin = (numperpage * (page - 1)) + 1;
			end = (numperpage * page) + 1;
			cmd.CommandText = "SELECT * FROM  " +
				"(SELECT Row_Number() OVER (ORDER BY date_added DESC) as RowIndex, * FROM game  " +
				"WHERE is_approved = 1) as Sub  " +
				"Where Sub.RowIndex >= " + begin + " and Sub.RowIndex < " + end;
			return fillDataSet(cmd);
		}

		public DataSet LoadPagedGamesByPlatform(int page, int numperpage, int platform) {
			SqlCommand cmd = new SqlCommand();
			int begin;
			int end;
			begin = (numperpage * (page - 1)) + 1;
			end = (numperpage * page) + 1;
			cmd.CommandText = "SELECT * FROM  " +
				"(SELECT Row_Number() OVER (ORDER BY g.date_added DESC) as RowIndex, g.* FROM game g INNER JOIN game_specifics s ON g.id = s.game_id " +
				"WHERE g.is_approved = 1 AND s.platform_id = " + platform.ToString() + ") as Sub  " +
				"Where Sub.RowIndex >= " + begin + " and Sub.RowIndex < " + end;
			return fillDataSet(cmd);
		}

		public int totalApprovedGames() {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT COUNT(id) FROM game WHERE is_approved = 1";
			return execScalar(cmd);
		}

		public DataSet LoadInstanceRankings(int gameID) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT s.id, " +
				"(CAST(SUM(r.value) AS FLOAT)/CAST(COUNT(r.value) AS FLOAT)) as avgVal " +
				"FROM game_specifics s INNER JOIN rating r " +
				"ON r.object_id = s.id WHERE r.ratingtype_id = 1 AND s.game_id=@gameID " +
				"GROUP BY r.object_id, s.id " +
				"ORDER BY avgVal DESC";
			cmd.Parameters.AddWithValue("@gameID", gameID);
			return fillDataSet(cmd);
		}

		public DataSet LoadPagedFilteredGames(int numperpage,
				int page, 
				int platformID,
				int publisherID,
				int genreID,
				int releaseYear,
				string title) {
			SqlCommand cmd = new SqlCommand();
			int begin;
			int end;
			begin = (numperpage * (page - 1)) + 1;
			end = (numperpage * page) + 1;
			string sql = "SELECT * FROM  " +
				"(SELECT Row_Number() OVER (ORDER BY date_added DESC) as RowIndex, g.* FROM game g " +
				"WHERE g.id IN (SELECT game_id FROM game_specifics gs " +
				"WHERE gs.is_approved = 1 ";
			if (platformID > 0) {
				sql += "AND gs.id IN (SELECT id FROM game_specifics WHERE platform_id = @platformID) ";
				cmd.Parameters.AddWithValue("@platformID",platformID);
			}
			if (publisherID > 0) {
				sql += "AND gs.id IN (SELECT game_specifics_id FROM game_publisher WHERE publisher_id = @publisherID) ";
				cmd.Parameters.AddWithValue("@publisherID", publisherID);
			}
			if (genreID > 0) {
				sql += "AND g.id IN (SELECT game_id FROM game_genre WHERE genre_id = @genreID) ";
				cmd.Parameters.AddWithValue("@genreID", genreID);
			}
			if (releaseYear > 0) {
				sql += "AND YEAR(gs.release_date) = @releaseYear ";
				cmd.Parameters.AddWithValue("@releaseYear", releaseYear);
			}
			if (title.Length > 0) {
				string[] words = title.Split(' ');
				foreach (string word in words) {
					sql += "AND g.title LIKE '%" + word + "%' ";
				}
			}
			sql += ")) as Sub  " +
				"Where Sub.RowIndex >= " + begin + " and Sub.RowIndex < " + end;
			cmd.CommandText = sql;
			return fillDataSet(cmd);
		}

		public int totalFilteredApprovedGames(int platformID,
				int publisherID,
				int genreID,
				int releaseYear,
				string title) {
			SqlCommand cmd = new SqlCommand();
			string sql = "SELECT COUNT(id) FROM game WHERE is_approved = 1 AND id IN (SELECT game_id FROM game_specifics gs WHERE gs.is_approved = 1 ";
			if (platformID > 0) {
				sql += "AND gs.id IN (SELECT id FROM game_specifics WHERE platform_id = @platformID) ";
				cmd.Parameters.AddWithValue("@platformID", platformID);
			}
			if (publisherID > 0) {
				sql += "AND gs.id IN (SELECT game_specifics_id FROM game_publisher WHERE publisher_id = @publisherID) ";
				cmd.Parameters.AddWithValue("@publisherID", publisherID);
			}
			if (genreID > 0) {
				sql += "AND g.id IN (SELECT game_id FROM game_genre WHERE genre_id = @genreID) ";
				cmd.Parameters.AddWithValue("@genreID", genreID);
			}
			if (releaseYear > 0) {
				sql += "AND YEAR(gs.release_date) = @releaseYear ";
				cmd.Parameters.AddWithValue("@releaseYear", releaseYear);
			}
			if (title.Length > 0) {
				string[] words = title.Split(' ');
				foreach (string word in words) {
					sql += "AND g.title LIKE '%" + word + "%' ";
				}
			}
			sql += ")";
			cmd.CommandText = sql;
			return execScalar(cmd);
		}

		public int numUserReviewsByGame(int userID, int gameID) {
			SqlCommand cmd = new SqlCommand();
			cmd.Parameters.AddWithValue("@userID", userID);
			cmd.Parameters.AddWithValue("@gameID", gameID);
			cmd.CommandText = "SELECT COUNT(sg.id) FROM story_game sg INNER JOIN story s ON s.id=sg.story_id WHERE " +
				"sg.game_id=@gameID AND s.submitter_id=@userID";
			return execScalar(cmd);
		}
	}
}
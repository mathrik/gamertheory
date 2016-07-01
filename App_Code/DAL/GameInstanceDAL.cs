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
/// Summary description for GameInstanceDAL
/// </summary>
namespace gamer {
	public class GameInstanceDAL : DataAccess {
		public GameInstanceDAL() {
			//
			// TODO: Add constructor logic here
			//
		}

        public int Add(int gameID, int platformID, DateTime release, int approved, string cover)
        {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "INSERT INTO game_specifics (game_id, " +
				"platform_id, " +
				"release_date, " +
				"is_approved, " +
                "cover_image) VALUES (" +
				"@gameID, " +
				"@platformID, " +
				"@release, " +
				"@approved, " +
                "@cover); SELECT @@IDENTITY;";
			cmd.Parameters.AddWithValue("@gameID", gameID);
			cmd.Parameters.AddWithValue("@platformID", platformID);
			cmd.Parameters.AddWithValue("@release", release);
            cmd.Parameters.AddWithValue("@approved", approved);
            cmd.Parameters.AddWithValue("@cover", cover);
			return execScalar(cmd);
		}

        public void Update(int id, int gameID, int platformID, DateTime release, int approved, string cover)
        {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "UPDATE game_specifics SET " +
				"game_id=@gameID, " +
				"platform_id=@platformID, " +
				"release_date=@release, " +
				"is_approved=@approved, " +
                "cover_image=@cover " + 
				"WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			cmd.Parameters.AddWithValue("@gameID", gameID);
			cmd.Parameters.AddWithValue("@platformID", platformID);
			cmd.Parameters.AddWithValue("@release", release);
            cmd.Parameters.AddWithValue("@approved", approved);
            cmd.Parameters.AddWithValue("@cover", cover);
			execNonQuery(cmd);
		}

		public void Delete(int id) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "DELETE FROM game_specifics WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			execNonQuery(cmd);
		}

		public DataSet LoadByID(int id) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM game_specifics WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			return fillDataSet(cmd);
		}

		public DataSet LoadByGame(int gameID) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM game_specifics WHERE game_id=@gameID ORDER BY release_date";
			cmd.Parameters.Add("@gameID", gameID);
			return fillDataSet(cmd);
		}

		public void AddDeveloper(int instanceID, int devID) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "INSERT INTO game_developer (game_specifics_id, developer_id) VALUES " +
				"(@instanceID, @devID)";
			cmd.Parameters.AddWithValue("@instanceID", instanceID);
			cmd.Parameters.AddWithValue("@devID", devID);
			execNonQuery(cmd);
		}

		public void AddGenre(int instanceID, int genreID) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "INSERT INTO game_genre (game_id, genre_id) VALUES " +
				"(@instanceID, @genreID)";
			cmd.Parameters.AddWithValue("@instanceID", instanceID);
			cmd.Parameters.AddWithValue("@genreID", genreID);
			execNonQuery(cmd);
		}

		public void AddPublisher(int instanceID, int publisherID) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "INSERT INTO game_publisher (game_specifics_id, publisher_id) VALUES " +
				"(@instanceID, @publisherID)";
			cmd.Parameters.AddWithValue("@instanceID", instanceID);
			cmd.Parameters.AddWithValue("@publisherID", publisherID);
			execNonQuery(cmd);
		}

		public void RemoveDeveloper(int instanceID, int devID) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "DELETE FROM game_developer WHERE game_specifics_id=@instanceID AND developer_id=@devID";
			cmd.Parameters.AddWithValue("@instanceID", instanceID);
			cmd.Parameters.AddWithValue("@devID", devID);
			execNonQuery(cmd);
		}

		public void RemoveGenre(int instanceID, int genreID) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "DELETE FROM game_genre WHERE game_id=@instanceID AND genre_id=@genreID";
			cmd.Parameters.AddWithValue("@instanceID", instanceID);
			cmd.Parameters.AddWithValue("@genreID", genreID);
			execNonQuery(cmd);
		}

		public void RemovePublisher(int instanceID, int publisherID) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "DELETE FROM game_publisher WHERE game_specifics_id=@instanceID AND publisher_id=@publisherID";
			cmd.Parameters.AddWithValue("@instanceID", instanceID);
			cmd.Parameters.AddWithValue("@publisherID", publisherID);
			execNonQuery(cmd);
		}

		public DataSet LoadFromCollection(int numperpage, int page, int userID) {
			SqlCommand cmd = new SqlCommand();
			int begin;
			int end;
			begin = (numperpage * (page - 1)) + 1;
			end = (numperpage * page) + 1;
			cmd.CommandText = "SELECT * FROM " +
				"(SELECT Row_Number() OVER (ORDER BY g.release_date DESC) as RowIndex, g.* FROM game_specifics g " +
				"INNER JOIN collection c ON c.object_id = g.id WHERE c.collectiontype_id = 1 AND c.user_id = @userID) as Sub " +
				"Where Sub.RowIndex >= " + begin + " and Sub.RowIndex < " + end;
			cmd.Parameters.AddWithValue("@userID", userID);
			return fillDataSet(cmd);
		}

		public DataSet LoadTopRated(int count) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT TOP " + count.ToString() + 
				" s.id, s.game_id, s.cover_image, s.is_approved, s.platform_id, s.release_date, r.object_id, " +
				"ROUND(CAST(SUM(r.value) AS FLOAT)/CAST(COUNT(r.value) AS FLOAT),0) as avgVal, " +
				"COUNT(r.value) as numRankings " +
				"FROM game_specifics s INNER JOIN rating r " +
				"ON r.object_id = s.id WHERE r.ratingtype_id = 1 " +
				"GROUP BY r.object_id, s.id, s.game_id, s.cover_image, s.is_approved, s.platform_id, s.release_date " +
				"ORDER BY avgVal DESC, numRankings DESC";
			return fillDataSet(cmd);
		}

		public DataSet LoadTopRated(int count, int platform) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT TOP " + count.ToString() +
				" s.id, s.game_id, s.cover_image, s.is_approved, s.platform_id, s.release_date, r.object_id, " +
				"ROUND(CAST(SUM(r.value) AS FLOAT)/CAST(COUNT(r.value) AS FLOAT),0) as avgVal, " +
				"COUNT(r.value) as numRankings " +
				"FROM game_specifics s INNER JOIN rating r " +
				"ON r.object_id = s.id WHERE r.ratingtype_id = 1 AND s.platform_id = @platform " +
				"GROUP BY r.object_id, s.id, s.game_id, s.cover_image, s.is_approved, s.platform_id, s.release_date " +
				"ORDER BY avgVal DESC, numRankings DESC";
			cmd.Parameters.AddWithValue("@platform", platform);
			return fillDataSet(cmd);
		}

		public DataSet LoadFiltered(int platformID,
				int publisherID,
				int releaseYear,
				int gameID) {
			SqlCommand cmd = new SqlCommand();
			string sql = "SELECT * FROM game_specifics WHERE game_id=@gameID ";
			if (platformID > 0) {
				sql += " AND platform_id = @platformID ";
				cmd.Parameters.AddWithValue("@platformID", platformID);
			}
			if (publisherID > 0) {
				sql += "AND id IN (SELECT game_specifics_id FROM game_publisher WHERE publisher_id = @publisherID) ";
				cmd.Parameters.AddWithValue("@publisherID", publisherID);
			}
			if (releaseYear > 0) {
				sql += "AND YEAR(release_date) = @releaseYear ";
				cmd.Parameters.AddWithValue("@releaseYear", releaseYear);
			}
			sql += " ORDER BY release_date";
			cmd.CommandText = sql;
			cmd.Parameters.Add("@gameID", gameID);
			return fillDataSet(cmd);
		}
	}
}
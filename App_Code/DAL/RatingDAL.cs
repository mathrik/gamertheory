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
/// Summary description for RatingDAL
/// </summary>
public class RatingDAL : DataAccess {
	public RatingDAL() {
		//
		// TODO: Add constructor logic here
		//
	}

    public int Add(int objectID, int ratingVal, int user, int ratingType) {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "INSERT INTO rating (object_id, value, user_id, ratingtype_id) VALUES (" +
            "@objectID, @ratingVal, @user, @ratingType); SELECT @@IDENTITY";
        cmd.Parameters.AddWithValue("@objectID", objectID);
        cmd.Parameters.AddWithValue("@ratingVal", ratingVal);
        cmd.Parameters.AddWithValue("@user", user);
        cmd.Parameters.AddWithValue("@ratingType", ratingType);
        return execScalar(cmd);
    }

    public void Update(int id, int objectID, int ratingVal, int user, int ratingType) {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "UPDATE rating SET " +
            "object_id=@objectID, " +
            "value=@ratingVal, " +
            "user_id=@user, " +
            "ratingtype_id=@ratingType " +
            "WHERE id=@id";
        cmd.Parameters.AddWithValue("@objectID", objectID);
        cmd.Parameters.AddWithValue("@ratingVal", ratingVal);
        cmd.Parameters.AddWithValue("@user", user);
        cmd.Parameters.AddWithValue("@ratingType", ratingType);
        cmd.Parameters.AddWithValue("@id", id);
        execNonQuery(cmd);
    }

    public void Delete(int id) {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "DELETE FROM rating WHERE id=@id";
        cmd.Parameters.AddWithValue("@id", id);
        execNonQuery(cmd);
    }

    public DataSet LoadByID(int id) {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "SELECT r.*, t.title FROM rating r INNER JOIN ratingtype t ON " +
            "r.ratingtype_id = t.id WHERE r.id=@id";
        cmd.Parameters.AddWithValue("@id", id);
        return fillDataSet(cmd);
    }

    public DataSet LoadByObject(int objectID, int objectType) {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "SELECT r.*, t.title FROM rating r INNER JOIN ratingtype t ON " +
            "r.ratingtype_id = t.id WHERE r.object_id=@objectID AND r.ratingtype_id=@objectType";
        cmd.Parameters.AddWithValue("@objectID", objectID);
        cmd.Parameters.AddWithValue("@objectType", objectType);
        return fillDataSet(cmd);
    }

    public DataSet LoadByUser(int user) {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "SELECT r.*, t.title FROM rating r INNER JOIN ratingtype t ON " +
            "r.ratingtype_id = t.id WHERE r.user_id=@user";
        cmd.Parameters.AddWithValue("@user", user);
        return fillDataSet(cmd);
    }

	public DataSet getUserRatings(int user, int ratingtype) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT r.*, t.title FROM rating r INNER JOIN ratingtype t ON " +
			"r.ratingtype_id = t.id WHERE r.user_id=@user AND t.id = @ratingtype";
		cmd.Parameters.AddWithValue("@user", user);
		cmd.Parameters.AddWithValue("@ratingtype", ratingtype);
		return fillDataSet(cmd);
	}

	public DataSet CheckExistingRanking(int userID, int ratingtype, int objectID) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT r.*, t.title FROM rating r INNER JOIN ratingtype t ON " +
			"r.ratingtype_id = t.id WHERE r.user_id=@user AND t.id = @ratingtype AND r.object_id=@objectID";
		cmd.Parameters.AddWithValue("@objectID", objectID);
		cmd.Parameters.AddWithValue("@user", userID);
		cmd.Parameters.AddWithValue("@ratingtype", ratingtype);
		return fillDataSet(cmd);
	}

	public DataSet LoadRecentGamesRating(int count) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT r.object_id, ROUND(CAST(SUM(r.value) AS FLOAT)/CAST(COUNT(r.value) AS FLOAT),0) as avgVal FROM rating r " + 
			" INNER JOIN game_specifics gs ON r.object_id = gs.id " +
			"WHERE r.ratingtype_id = 1 AND gs.game_id IN " +
			"(SELECT TOP " + count.ToString() + " id FROM game WHERE is_approved = 1 ORDER BY date_added) GROUP BY r.object_id";
		return fillDataSet(cmd);
	}

	public DataSet LoadRecentGamesRatingByPlatform(int count, int platform) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT r.object_id, ROUND(CAST(SUM(r.value) AS FLOAT)/CAST(COUNT(r.value) AS FLOAT),0) as avgVal " +
			"FROM rating r  " +
			"WHERE r.ratingtype_id = 1 AND r.object_id IN  " +
			"(SELECT TOP " + count.ToString() + " s.id FROM game g INNER JOIN game_specifics s ON g.id = s.game_id  " +
			"WHERE g.is_approved = 1 AND s.platform_id  = " + platform.ToString() + " ORDER BY date_added) GROUP BY r.object_id";
		return fillDataSet(cmd);
	}

	public DataSet LoadPagedGameRating(int page, int numperpage) {
		SqlCommand cmd = new SqlCommand();
		int begin;
		int end;
		begin = (numperpage * (page - 1)) + 1;
		end = (numperpage * page) + 1;
		cmd.CommandText = "SELECT r.object_id, ROUND(CAST(SUM(r.value) AS FLOAT)/CAST(COUNT(r.value) AS FLOAT),0) as avgVal FROM rating r  " +
			"INNER JOIN game_specifics gs ON r.object_id = gs.id  " +
			"WHERE r.ratingtype_id = 1 AND gs.game_id IN  " +
			"(SELECT id FROM  " +
			"(SELECT Row_Number() OVER (ORDER BY date_added) as RowIndex, id FROM game  " +
			"WHERE is_approved = 1) as Sub  " +
			"Where Sub.RowIndex >= " + begin + " and Sub.RowIndex < " + end + ") GROUP BY r.object_id";
		return fillDataSet(cmd);
	}

	public DataSet LoadPagedGameRatingByPlatform(int page, int numperpage, int platform) {
		SqlCommand cmd = new SqlCommand();
		int begin;
		int end;
		begin = (numperpage * (page - 1)) + 1;
		end = (numperpage * page) + 1;
		cmd.CommandText = "SELECT r.object_id, ROUND(CAST(SUM(r.value) AS FLOAT)/CAST(COUNT(r.value) AS FLOAT),0) as avgVal FROM rating r " +
			"WHERE r.ratingtype_id = 1 AND r.object_id IN  " +
			"(SELECT id FROM  " +
			"(SELECT Row_Number() OVER (ORDER BY g.date_added) as RowIndex, s.id FROM game g INNER JOIN game_specifics s ON g.id = s.game_id " +
			"WHERE g.is_approved = 1 AND s.platform_id = 1) as Sub  " +
			"Where Sub.RowIndex >= " + begin + " and Sub.RowIndex < " + end + ") GROUP BY r.object_id";
		return fillDataSet(cmd);
	}

	public DataSet LoadFilteredPagedGameRating(int page, 
			int numperpage,
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
		string sql = "SELECT r.object_id, ROUND(CAST(SUM(r.value) AS FLOAT)/CAST(COUNT(r.value) AS FLOAT),0) as avgVal FROM rating r " +
			"WHERE r.ratingtype_id = 1 AND r.object_id IN  " +
			"(SELECT id FROM  " +
			"(SELECT Row_Number() OVER (ORDER BY g.date_added) as RowIndex, s.id FROM game g INNER JOIN game_specifics s ON g.id = s.game_id " +
			"WHERE g.is_approved = 1 ";
		if (platformID > 0) {
			sql += "AND s.id IN (SELECT id FROM game_specifics WHERE platform_id = @platformID) ";
			cmd.Parameters.AddWithValue("@platformID", platformID);
		}
		if (publisherID > 0) {
			sql += "AND s.id IN (SELECT game_specifics_id FROM game_publisher WHERE publisher_id = @publisherID) ";
			cmd.Parameters.AddWithValue("@publisherID", publisherID);
		}
		if (genreID > 0) {
			sql += "AND g.id IN (SELECT game_id FROM game_genre WHERE genre_id = @genreID) ";
			cmd.Parameters.AddWithValue("@genreID", genreID);
		}
		if (releaseYear > 0) {
			sql += "AND YEAR(s.release_date) = @releaseYear ";
			cmd.Parameters.AddWithValue("@releaseYear", releaseYear);
		}
		if (title != null && title.Length > 0) {
			string[] words = title.Split(' ');
			foreach (string word in words) {
				sql += "AND g.title LIKE '%" + word + "%' ";
			}
		}
		sql += ") as Sub  " +
			"Where Sub.RowIndex >= " + begin + " and Sub.RowIndex < " + end + ") GROUP BY r.object_id";
		cmd.CommandText = sql;
		return fillDataSet(cmd);
	}
}

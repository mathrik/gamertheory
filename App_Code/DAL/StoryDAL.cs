using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for StoryDAL
/// </summary>
public class StoryDAL : DataAccess {
	public StoryDAL() {
		//
		// TODO: Add constructor logic here
		//
	}

	public int Add(string title,
			string body,
			int typeID,
			int isfeatured,
			int eventID,
			int submitter,
			int isApproved,
			DateTime submissionDate,
			string preview,
			string thumbnail) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "INSERT INTO story (" +
			"title, " +
			"body, " +
			"storytype_id, " +
			"isfeatured, " +
			"event_id, " +
			"submitter_id, " +
			"isApproved, " +
			"submission_date, " +
			"preview, " +
			"thumbnail) " +
			"VALUES (" +
			"@title , " +
			"@body, " +
			"@typeID, " +
			"@isfeatured, " +
			"@eventID, " +
			"@submitter, " +
			"@isApproved, " +
			"@submissionDate, " +
			"@preview, " +
			"@thumbnail" +
			"); SELECT @@IDENTITY;";
		cmd.Parameters.AddWithValue("@title", title);
		cmd.Parameters.AddWithValue("@body", body);
		cmd.Parameters.AddWithValue("@typeID", typeID);
		cmd.Parameters.AddWithValue("@isfeatured", isfeatured);
		cmd.Parameters.AddWithValue("@eventID", eventID);
		cmd.Parameters.AddWithValue("@submitter", submitter);
		cmd.Parameters.AddWithValue("@isApproved", isApproved);
		cmd.Parameters.AddWithValue("@submissionDate", submissionDate);
		cmd.Parameters.AddWithValue("@preview", preview);
		cmd.Parameters.AddWithValue("@thumbnail", thumbnail);
		return execScalar(cmd);
	}

	public void Update(int id,
			string title,
			string body,
			int typeID,
			int isfeatured,
			int eventID,
			int submitter,
			int isApproved,
			DateTime submissionDate,
			string preview,
			string thumbnail) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "UPDATE story SET " +
			"title=@title, " +
			"body=@body, " +
			"storytype_id=@typeID, " +
			"isfeatured=@isfeatured, " +
			"event_id=@eventID, " +
			"submitter_id=@submitter, " +
			"isApproved=@isApproved, " +
			"submission_date=@submissionDate, " +
			"preview=@preview, " +
			"thumbnail=@thumbnail " +
			"WHERE id=@id";
		cmd.Parameters.AddWithValue("@id", id);
		cmd.Parameters.AddWithValue("@title", title);
		cmd.Parameters.AddWithValue("@body", body);
		cmd.Parameters.AddWithValue("@typeID", typeID);
		cmd.Parameters.AddWithValue("@isfeatured", isfeatured);
		cmd.Parameters.AddWithValue("@eventID", eventID);
		cmd.Parameters.AddWithValue("@submitter", submitter);
		cmd.Parameters.AddWithValue("@isApproved", isApproved);
		cmd.Parameters.AddWithValue("@submissionDate", submissionDate);
		cmd.Parameters.AddWithValue("@preview", preview);
		cmd.Parameters.AddWithValue("@thumbnail", thumbnail);
		execNonQuery(cmd);
	}

	public void Delete(int id) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "DELETE FROM story WHERE id=@id";
		cmd.Parameters.AddWithValue("@id", id);
		execNonQuery(cmd);
	}

	public DataSet LoadByID(int id) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT * FROM story WHERE id=@id";
		cmd.Parameters.AddWithValue("@id", id);
		return fillDataSet(cmd);
	}

	public DataSet LoadByFeatured() {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT * FROM story WHERE isfeatured=1 ORDER BY submission_date DESC";
		return fillDataSet(cmd);
	}

	public DataSet LoadByFeatured(int isApproved) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT * FROM story WHERE isfeatured=1 && isApproved=@isApproved ORDER BY submission_date DESC";
		cmd.Parameters.AddWithValue("@isApproved", isApproved);
		return fillDataSet(cmd);
	}

	public DataSet LoadByStoryType(int storytype, int maxqty) {
		SqlCommand cmd = new SqlCommand();
		if (maxqty == 0) {
			cmd.CommandText = "SELECT * FROM story WHERE storytype_id = @storytype " +
				"ORDER BY submission_date DESC";
		} else {
			cmd.CommandText = "SELECT TOP @maxqty * FROM story WHERE storytype_id = @storytype " +
				"ORDER BY submission_date DESC";
			cmd.Parameters.AddWithValue("@maxqty", maxqty);
		}
		cmd.Parameters.AddWithValue("@storytype", storytype);
		return fillDataSet(cmd);
	}

	public DataSet LoadStoriesMultipleTypes(List<int> typeList, int maxqty) {
		SqlCommand cmd = new SqlCommand();
		string typegroup = "";
		for (int i = 0; i < typeList.Count; i++) {
			if (typegroup == "") {
				typegroup = typeList[i].ToString();
			} else {
				typegroup += "," + typeList[i].ToString();
			}
		}
		if (maxqty == 0) {
			cmd.CommandText = "SELECT * FROM story WHERE storytype_id IN (" + typegroup + ") " +
				"ORDER BY submission_date DESC";
		} else {
			cmd.CommandText = "SELECT TOP @maxqty * FROM story WHERE storytype_id = IN (" + 
				typeList.ToString() + ") " +
				"ORDER BY submission_date DESC";
			cmd.Parameters.AddWithValue("@maxqty", maxqty);
		}
		return fillDataSet(cmd);
	}

	public DataSet LoadByStoryType_User(int storytype, int userID, int maxqty) {
		SqlCommand cmd = new SqlCommand();
		if (maxqty == 0) {
			cmd.CommandText = "SELECT * FROM story WHERE storytype_id = @storytype " +
				" AND submitter_id = @userID ORDER BY submission_date DESC";
		} else {
			cmd.CommandText = "SELECT TOP @maxqty * FROM story WHERE storytype_id = @storytype " +
				" AND submitter_id = @userID ORDER BY submission_date DESC";
			cmd.Parameters.AddWithValue("@maxqty", maxqty);
		}
		cmd.Parameters.AddWithValue("@storytype", storytype);
		cmd.Parameters.AddWithValue("@userID", userID);
		return fillDataSet(cmd);
	}

	public DataSet LoadStoriesMultipleTypes_User(List<int> typeList, int userID, int maxqty) {
		SqlCommand cmd = new SqlCommand();
		string typegroup = "";
		for (int i = 0; i < typeList.Count; i++) {
			if (typegroup == "") {
				typegroup = typeList[i].ToString();
			} else {
				typegroup = "," + typeList[i].ToString();
			}
		}
		if (maxqty == 0) {
			cmd.CommandText = "SELECT * FROM story WHERE storytype_id IN (" + typegroup + ") " +
				" AND submitter_id = @userID ORDER BY submission_date DESC";
		} else {
			cmd.CommandText = "SELECT TOP @maxqty * FROM story WHERE storytype_id = IN (" +
				typeList.ToString() + ") " +
				" AND submitter_id = @userID ORDER BY submission_date DESC";
			cmd.Parameters.AddWithValue("@maxqty", maxqty);
		}
		cmd.Parameters.AddWithValue("@userID", userID);
		return fillDataSet(cmd);
	}

	public void AddDeveloper(int storyID, int devID) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "INSERT INTO story_developer (story_id, developer_id) VALUES " +
			"(@storyID, @devID)";
		cmd.Parameters.AddWithValue("@storyID", storyID);
		cmd.Parameters.AddWithValue("@devID", devID);
		execNonQuery(cmd);
	}

	public void AddGenre(int storyID, int genreID) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "INSERT INTO story_genre (story_id, genre_id) VALUES " +
			"(@storyID, @genreID)";
		cmd.Parameters.AddWithValue("@storyID", storyID);
		cmd.Parameters.AddWithValue("@genreID", genreID);
		execNonQuery(cmd);
	}

	public void AddGame(int storyID, int gameID) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "INSERT INTO story_game (story_id, game_id) VALUES " +
			"(@storyID, @gameID)";
		cmd.Parameters.AddWithValue("@storyID", storyID);
		cmd.Parameters.AddWithValue("@gameID", gameID);
		execNonQuery(cmd);
	}

	public void AddPlatform(int storyID, int platformID) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "INSERT INTO story_platform (story_id, platform_id) VALUES " +
			"(@storyID, @platformID)";
		cmd.Parameters.AddWithValue("@storyID", storyID);
		cmd.Parameters.AddWithValue("@platformID", platformID);
		execNonQuery(cmd);
	}

	public void AddPublisher(int storyID, int publisherID) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "INSERT INTO story_publisher (story_id, publisher_id) VALUES " +
			"(@storyID, @publisherID)";
		cmd.Parameters.AddWithValue("@storyID", storyID);
		cmd.Parameters.AddWithValue("@publisherID", publisherID);
		execNonQuery(cmd);
	}

	public void RemoveDeveloper(int storyID, int devID) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "DELETE FROM story_developer WHERE story_id=@storyID AND developer_id=@devID";
		cmd.Parameters.AddWithValue("@storyID", storyID);
		cmd.Parameters.AddWithValue("@devID", devID);
		execNonQuery(cmd);
	}

	public void RemoveGenre(int storyID, int genreID) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "DELETE FROM story_genre WHERE story_id=@storyID AND genre_id=@genreID";
		cmd.Parameters.AddWithValue("@storyID", storyID);
		cmd.Parameters.AddWithValue("@genreID", genreID);
		execNonQuery(cmd);
	}

	public void RemoveGame(int storyID, int gameID) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "DELETE FROM story_game WHERE story_id=@storyID AND game_id=@gameID";
		cmd.Parameters.AddWithValue("@storyID", storyID);
		cmd.Parameters.AddWithValue("@gameID", gameID);
		execNonQuery(cmd);
	}

	public void RemovePlatform(int storyID, int platformID) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "DELETE FROM story_platform WHERE story_id=@storyID AND platform_id=@platformID";
		cmd.Parameters.AddWithValue("@storyID", storyID);
		cmd.Parameters.AddWithValue("@platformID", platformID);
		execNonQuery(cmd);
	}

	public void RemovePublisher(int storyID, int publisherID) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "DELETE FROM story_publisher WHERE story_id=@storyID AND publisher_id=@publisherID";
		cmd.Parameters.AddWithValue("@storyID", storyID);
		cmd.Parameters.AddWithValue("@publisherID", publisherID);
		execNonQuery(cmd);
	}

	public DataSet LoadRecent(int count) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT TOP " + count + " * FROM story WHERE isApproved = 1 ORDER BY submission_date DESC";
		return fillDataSet(cmd);
	}

	public DataSet LoadRecentByType(int count, int type) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT TOP " + count + " * FROM story WHERE storytype_id = @type AND isApproved = 1 ORDER BY submission_date DESC";
		cmd.Parameters.AddWithValue("@type", type);
		return fillDataSet(cmd);
	}

	/// <summary>
	/// updates = news, previews, reviews, editorials
	/// </summary>
	/// <param name="count"></param>
	/// <returns></returns>
	public DataSet LoadRecentFeaturedUpdates(int count, int homecategory) {
		SqlCommand cmd = new SqlCommand();
		string sql = "SELECT TOP " + count + " * FROM story WHERE isfeatured = 1 AND isApproved = 1";
		if (homecategory > 0) {
			sql += " AND id IN (SELECT story_id FROM story_platform WHERE platform_id IN " +
				"(SELECT id FROM platform WHERE homecategory_id = @homecategory))";
			cmd.Parameters.AddWithValue("@homecategory", homecategory);
		}
		sql += "  ORDER BY submission_date DESC";
		cmd.CommandText = sql;
		return fillDataSet(cmd);
	}

	public DataSet LoadRecentByGame(int count, int gameID) {
		SqlCommand cmd = new SqlCommand();
		string countSql;
		if (count > 0) {
			countSql = " TOP " + count.ToString() + " ";
		} else {
			countSql = "";
		}
		cmd.CommandText = "SELECT" + countSql + " * FROM story WHERE id IN (SELECT story_id FROM story_game WHERE game_id=@gameID)";
		cmd.Parameters.AddWithValue("@gameID", gameID);
		return fillDataSet(cmd);
	}

	public DataSet LoadRecentByGameNoUserReviews(int count, int gameID) {
		SqlCommand cmd = new SqlCommand();
		string countSql;
		if (count > 0) {
			countSql = " TOP " + count.ToString() + " ";
		} else {
			countSql = "";
		}
		cmd.CommandText = "SELECT" + countSql + " * FROM story WHERE storytype_id <> 5 AND id IN (SELECT story_id FROM story_game WHERE game_id=@gameID)";
		cmd.Parameters.AddWithValue("@gameID", gameID);
		return fillDataSet(cmd);
	}

	public void AttachPodcastFile(int storyID, string filename) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "UPDATE story SET podcastfile=@filename WHERE id=@storyID";
		cmd.Parameters.AddWithValue("@storyID", storyID);
		cmd.Parameters.AddWithValue("@filename", filename);
		execNonQuery(cmd);
	}

	public string GetPodcastFile(int storyID) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT podcastfile FROM story WHERE id=@storyID";
		cmd.Parameters.AddWithValue("@storyID", storyID);
		DataSet ds = fillDataSet(cmd);
		if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
			return ds.Tables[0].Rows[0]["podcastfile"].ToString();
		} else {
			return "";
		}
	}
	
	public DataSet getUserReviewsByGame(int gameID) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT s.* FROM story s INNER JOIN story_game sg ON sg.story_id=s.id WHERE sg.game_id=@gameID AND s.storytype_id = 5";
		cmd.Parameters.AddWithValue("@gameID", gameID);
		return fillDataSet(cmd);
	}

	public DataSet GetPagedStories(int count, int page) {
		SqlCommand cmd = new SqlCommand();
		int begin;
		int end;
		begin = (count * (page - 1)) + 1;
		end = (count * page) + 1;
		cmd.CommandText = "SELECT * FROM " +
			"(SELECT Row_Number() OVER (ORDER BY s.submission_date DESC) as RowIndex, s.* FROM story s " +
			"WHERE s.isApproved = 1) as Sub " +
			"Where Sub.RowIndex >= " + begin + " and Sub.RowIndex < " + end;
		return fillDataSet(cmd);
	}

    public DataSet GetPagedStoriesByType(int count, int page, int typeID)
    {
        SqlCommand cmd = new SqlCommand();
        int begin;
        int end;
        begin = (count * (page - 1)) + 1;
        end = (count * page) + 1;
        cmd.CommandText = "SELECT * FROM " +
            "(SELECT Row_Number() OVER (ORDER BY s.submission_date DESC) as RowIndex, s.* FROM story s " +
            "WHERE s.isApproved = 1 AND storytype_id = @typeID) as Sub " +
            "Where Sub.RowIndex >= " + begin + " and Sub.RowIndex < " + end;
        cmd.Parameters.AddWithValue("@typeID", typeID);
        return fillDataSet(cmd);
    }

    public int GetTotalStoriesByType(int typeID) {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "SELECT COUNT(id) FROM story WHERE isApproved = 1 AND storytype_id = @typeID";
        cmd.Parameters.AddWithValue("@typeID", typeID);
        return execScalar(cmd);
    }

	public DataSet GetPagedStaffStories(int count, int page) {
		SqlCommand cmd = new SqlCommand();
		int begin;
		int end;
		begin = (count * (page - 1)) + 1;
		end = (count * page) + 1;
		cmd.CommandText = "SELECT * FROM " +
			"(SELECT Row_Number() OVER (ORDER BY s.submission_date DESC) as RowIndex, s.* FROM story s " +
			"WHERE s.isApproved = 1 AND storytype_id <> 5) as Sub " +
			"Where Sub.RowIndex >= " + begin + " and Sub.RowIndex < " + end;
		return fillDataSet(cmd);
	}

	public int GetTotalStaffStories() {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT COUNT(id) FROM story WHERE isApproved = 1 AND storytype_id <> 5";
		return execScalar(cmd);
	}
}

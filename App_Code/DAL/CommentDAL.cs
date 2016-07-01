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
/// Summary description for CommentDAL
/// </summary>
public class CommentDAL : DataAccess {
	public CommentDAL() {
		//
		// TODO: Add constructor logic here
		//
	}

	public int Add(int typeID, int objectID, string body, int userID, int flagged) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "INSERT INTO comment " +
			"(commenttype_id, object_id, body, user_id, flagged, submission_date) VALUES (" +
			"@typeID, @objectID, @body, @userID, @flagged, GETDATE()); SELECT @@IDENTITY";
		cmd.Parameters.AddWithValue("@typeID", typeID);
		cmd.Parameters.AddWithValue("@objectID", objectID);
		cmd.Parameters.AddWithValue("@body", body);
		cmd.Parameters.AddWithValue("@userID", userID);
		cmd.Parameters.AddWithValue("@flagged", flagged);
		return execScalar(cmd);
	}

	public void Update(int id, int typeID, int objectID, string body, int userID, int flagged) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "UPDATE comment SET " +
			"commenttype_id=@typeID, " +
			"object_id=@objectID, " +
			"body=@body, " +
			"user_id=@userID, " +
			"flagged=@flagged " +
			"WHERE id=@id";
		cmd.Parameters.AddWithValue("@id", id);
		cmd.Parameters.AddWithValue("@typeID", typeID);
		cmd.Parameters.AddWithValue("@objectID", objectID);
		cmd.Parameters.AddWithValue("@body", body);
		cmd.Parameters.AddWithValue("@userID", userID);
		cmd.Parameters.AddWithValue("@flagged", flagged);
		execNonQuery(cmd);
	}

	public void Delete(int id) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "DELETE FROM comment WHERE id=@id";
		cmd.Parameters.AddWithValue("@id", id);
		execNonQuery(cmd);
	}

	public DataSet LoadByID(int id) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT * FROM comment WHERE id=@id";
		cmd.Parameters.AddWithValue("@id", id);
		return fillDataSet(cmd);
	}

	public DataSet GetUserComments(int userID) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT c.*, t.title FROM comment c INNER JOIN commenttype t ON " +
			"c.commenttype_id = t.id WHERE c.user_id=@userID ORDER BY submission_date DESC";
		cmd.Parameters.AddWithValue("@userID", userID);
		return fillDataSet(cmd);
	}

	public DataSet GetComments(int objectID, int typeID, int count) {
		SqlCommand cmd = new SqlCommand();
		if (count > 0) {
			cmd.CommandText = "SELECT TOP " + count.ToString() + " c.*, t.title FROM comment c INNER JOIN commenttype t ON " +
				"c.commenttype_id = t.id WHERE c.object_id=@objectID AND c.commenttype_id=@typeID AND flagged <> 1";
		} else {
			cmd.CommandText = "SELECT c.*, t.title FROM comment c INNER JOIN commenttype t ON " +
				"c.commenttype_id = t.id WHERE c.object_id=@objectID AND c.commenttype_id=@typeID AND flagged <> 1";
		}
		cmd.Parameters.AddWithValue("@typeID", typeID);
		cmd.Parameters.AddWithValue("@objectID", objectID);
		return fillDataSet(cmd);
	}

	public DataSet GetPagedComments(int objectID, int typeID, int count, int page) {
		SqlCommand cmd = new SqlCommand();
		int begin;
		int end;
		begin = (count * (page - 1)) + 1;
		end = (count * page) + 1;
		cmd.CommandText = "SELECT * FROM " +
			"(SELECT Row_Number() OVER (ORDER BY c.id) as RowIndex, c.*, t.title, u.avatar, u.username FROM comment c INNER JOIN commenttype t ON " +
			"c.commenttype_id = t.id INNER JOIN mpUser u ON u.id = c.user_id " +
			"WHERE c.flagged <> 1 AND c.object_id=@objectID AND c.commenttype_id=@typeID AND flagged <> 1) as Sub " +
			"Where Sub.RowIndex >= " + begin + " and Sub.RowIndex < " + end;
		cmd.Parameters.AddWithValue("@typeID", typeID);
		cmd.Parameters.AddWithValue("@objectID", objectID);
		return fillDataSet(cmd);
	}

	public DataSet GetRecentComments(int count) {
		SqlCommand cmd = new SqlCommand();
		string topSql = "";
		if (count > 0) {
			topSql = " TOP " + count.ToString() + " ";
		}
		cmd.CommandText = "SELECT " + topSql + " c.*, u.avatar, u.username FROM comment c INNER JOIN mpUser u ON " +
			"u.id = c.user_id WHERE c.flagged <> 1 ORDER BY submission_date DESC";
		return fillDataSet(cmd);
	}

	public int GetCommentCount(int objectID, int typeID) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT COUNT(id) FROM comment WHERE object_id=@objectID AND commenttype_id=@typeID AND flagged <> 1";
		cmd.Parameters.AddWithValue("@typeID", typeID);
		cmd.Parameters.AddWithValue("@objectID", objectID);
		return execScalar(cmd);
	}

	public DataSet GetPagedFlaggedComments(int objectID, int typeID, int count, int page) {
		SqlCommand cmd = new SqlCommand();
		int begin;
		int end;
		begin = (count * (page - 1)) + 1;
		end = (count * page) + 1;
		cmd.CommandText = "SELECT * FROM " +
			"(SELECT Row_Number() OVER (ORDER BY c.id) as RowIndex, c.*, t.title, u.avatar, u.username FROM comment c INNER JOIN commenttype t ON " +
			"c.commenttype_id = t.id INNER JOIN mpUser u ON u.id = c.user_id " +
			"WHERE c.flagged <> 1 AND c.object_id=@objectID AND c.commenttype_id=@typeID AND flagged = 1) as Sub " +
			"Where Sub.RowIndex >= " + begin + " and Sub.RowIndex < " + end;
		cmd.Parameters.AddWithValue("@typeID", typeID);
		cmd.Parameters.AddWithValue("@objectID", objectID);
		return fillDataSet(cmd);
	}

	public DataSet GetPagedFlaggedComments(int count, int page) {
		SqlCommand cmd = new SqlCommand();
		int begin;
		int end;
		begin = (count * (page - 1)) + 1;
		end = (count * page) + 1;
		cmd.CommandText = "SELECT * FROM " +
			"(SELECT Row_Number() OVER (ORDER BY c.id) as RowIndex, c.*, t.title, u.avatar, u.username FROM comment c INNER JOIN commenttype t ON " +
			"c.commenttype_id = t.id INNER JOIN mpUser u ON u.id = c.user_id " +
			"WHERE flagged = 1) as Sub " +
			"Where Sub.RowIndex >= " + begin + " and Sub.RowIndex < " + end;
		return fillDataSet(cmd);
	}

	public int GetFlaggedCommentCount() {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT COUNT(id) FROM comment WHERE flagged = 1";
		return execScalar(cmd);
	}

}

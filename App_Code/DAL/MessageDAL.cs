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
/// Summary description for MessageDAL
/// </summary>
namespace forum {
	public class MessageDAL : DataAccess {
		public MessageDAL() {
			//
			// TODO: Add constructor logic here
			//
		}

		public int Add(string title, 
				string body, 
				int parent, 
				DateTime edited,
				DateTime deleted, 
				int thread,
				int flagged, 
				DateTime posted) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "INSERT INTO forumpost " +
				"(title, body, parent_id, dt_edited, dt_deleted, forumthread_id, isflagged, dt_posted)" +
				"VALUES (" +
				"@title, @body, @parent, @edited, @deleted, @thread, @flagged, @posted);" +
				"SELECT @@IDENTITY;";
			cmd.Parameters.AddWithValue("@title", title);
			cmd.Parameters.AddWithValue("@body", body);
			cmd.Parameters.AddWithValue("@parent", parent);
			cmd.Parameters.AddWithValue("@edited", edited);
			cmd.Parameters.AddWithValue("@deleted", deleted);
			cmd.Parameters.AddWithValue("@thread", thread);
			cmd.Parameters.AddWithValue("@flagged", flagged);
			cmd.Parameters.AddWithValue("@posted", posted);
			return execScalar(cmd);
		}

		public void Update(int id,
				string title,
				string body,
				int parent,
				DateTime edited,
				DateTime deleted,
				int thread,
				int flagged,
				DateTime posted) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "UPDATE forumpost SET " +
				"title=@title, " +
				"body=@body, " +
				"parent_id=@parent, " +
				"dt_edited=@edited, " +
				"dt_deleted=@deleted, " +
				"forumthread_id=@thread, " +
				"isflagged=@flagged, " +
				"dt_posted=@posted " +
				"WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			cmd.Parameters.AddWithValue("@title", title);
			cmd.Parameters.AddWithValue("@body", body);
			cmd.Parameters.AddWithValue("@parent", parent);
			cmd.Parameters.AddWithValue("@edited", edited);
			cmd.Parameters.AddWithValue("@deleted", deleted);
			cmd.Parameters.AddWithValue("@thread", thread);
			cmd.Parameters.AddWithValue("@flagged", flagged);
			cmd.Parameters.AddWithValue("@posted", posted);
			execNonQuery(cmd);
		}

		public void Delete(int id) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "DELETE FROM forumpost WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			execNonQuery(cmd);
		}

		public void MarkDeleted(int id) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "UPDATE forumpost SET dt_deleted = GetDate() WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			execNonQuery(cmd);
		}

		public DataSet LoadByID(int id) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM forumpost WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			return fillDataSet(cmd);
		}

		public DataSet LoadByThread(int thread, bool showDeleted) {
			SqlCommand cmd = new SqlCommand();
			if (showDeleted) {
				cmd.CommandText = "SELECT * FROM forumpost WHERE forumthread_id=@thread ORDER BY dt_posted";
			} else {
				cmd.CommandText = "SELECT * FROM forumpost WHERE forumthread_id=@thread AND " +
				"dt_deleted < GetDate() ORDER BY dt_posted";
			}
			cmd.Parameters.AddWithValue("@thread", thread);
			return fillDataSet(cmd);
		}

		/// <summary>
		/// SELECT TOP @end * FROM forumpost WHERE forumthread_id=@thread 
		///	AND id NOT IN (SELECT TOP @start id FROM forumpost WHERE forumthread_id=@thread 
		///	ORDER BY dt_posted) ORDER BY dt_posted
		///	Is there a better way to do this?
		/// </summary>
		/// <param name="thread"></param>
		/// <param name="showDeleted"></param>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <returns></returns>
		public DataSet LoadByThread(int thread, bool showDeleted, int start, int end) {
			SqlCommand cmd = new SqlCommand();
			if (showDeleted && start == 0) {
				cmd.CommandText = "SELECT TOP @end * FROM forumpost WHERE forumthread_id=@thread " +
					"ORDER BY dt_posted";
			} else if (showDeleted) {
				cmd.CommandText = "SELECT TOP @end * FROM forumpost WHERE forumthread_id=@thread " +
					"AND id NOT IN (SELECT TOP @start id FROM forumpost WHERE forumthread_id=@thread " +
					"ORDER BY dt_posted) ORDER BY dt_posted";
			} else if (start == 0) {
				cmd.CommandText = "SELECT TOP @end * FROM forumpost WHERE forumthread_id=@thread AND " +
				"dt_deleted < GetDate() ORDER BY dt_posted";
			} else {
				cmd.CommandText = "SELECT TOP @end * FROM forumpost WHERE forumthread_id=@thread AND " +
				"dt_deleted < GetDate() AND id NOT IN " +
				"(SELECT TOP @start id FROM forumpost WHERE forumthread_id=@thread AND " +
				"dt_deleted < GetDate() ORDER BY dt_posted) ORDER BY dt_posted";
			}
			cmd.Parameters.AddWithValue("@thread", thread);
			cmd.Parameters.AddWithValue("@start", start);
			cmd.Parameters.AddWithValue("@end", end);
			return fillDataSet(cmd);
		}
	}
}
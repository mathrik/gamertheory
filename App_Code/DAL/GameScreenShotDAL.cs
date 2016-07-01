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
/// Summary description for GameScreenShotDAL
/// </summary>
namespace gamer {
	public class GameScreenShotDAL : DataAccess {
		public GameScreenShotDAL() {
			//
			// TODO: Add constructor logic here
			//
		}

		public int Add(string filename, int rank, int gameID) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "INSERT INTO game_screenshot (filename, rank, game_id) VALUES (" +
				"@filename, @rank, @gameID); SELECT @@IDENTITY;";
			cmd.Parameters.AddWithValue("@filename", filename);
			cmd.Parameters.AddWithValue("@rank", rank);
			cmd.Parameters.AddWithValue("@gameID", gameID);
			return execScalar(cmd);
		}

		public void Update(int id, string filename, int rank, int gameID) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "UPDATE game_screenshot SET " +
				"filename=@filename, " +
				"rank=@rank, " +
				"game_id=@gameID " +
				"WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			cmd.Parameters.AddWithValue("@filename", filename);
			cmd.Parameters.AddWithValue("@rank", rank);
			cmd.Parameters.AddWithValue("@gameID", gameID);
			execNonQuery(cmd);
		}

		public void Delete(int id) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "DELETE FROM game_screenshot WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			execNonQuery(cmd);
		}

		public DataSet LoadByID(int id) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM game_screenshot WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			return fillDataSet(cmd);
		}

		public DataSet LoadByGame(int gameID) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM game_screenshot WHERE game_id=@gameID";
			cmd.Parameters.AddWithValue("@gameID", gameID);
			return fillDataSet(cmd);
		}

		public int GetHighestRank() {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT TOP 1 rank FROM game_screenshot ORDER BY rank DESC";
			return execScalar(cmd) + 1;
		}
	}
}
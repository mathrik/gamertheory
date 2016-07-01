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
/// Summary description for SocialMediaDAL
/// </summary>
namespace gamer {
	public class SocialMediaDAL : DataAccess {
		public SocialMediaDAL() {
			//
			// TODO: Add constructor logic here
			//
		}

		public int Add(int typeID, int user, string login, string password) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "INSERT INTO socialmedia (socialmediatype_id, user_id, login, password) " +
				"VALUES (@typeID, @user, @login, @password);";
			cmd.Parameters.AddWithValue("@typeID", typeID);
			cmd.Parameters.AddWithValue("@user", user);
			cmd.Parameters.AddWithValue("@login", login);
			cmd.Parameters.AddWithValue("@password", password);
			return execScalar(cmd);
		}

		public void Update(int id, int typeID, int user, string login, string password) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "UPDATE socialmedia SET " +
				"socialmediatype_id=@typeID, " +
				"user_id=@user, " +
				"login=@login, " +
				"password=@password " +
				"WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			cmd.Parameters.AddWithValue("@typeID", typeID);
			cmd.Parameters.AddWithValue("@user", user);
			cmd.Parameters.AddWithValue("@login", login);
			cmd.Parameters.AddWithValue("@password", password);
			execNonQuery(cmd);
		}

		public void Delete(int id) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "DELETE FROM socialmedia WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			execNonQuery(cmd);
		}

		public DataSet LoadByID(int id) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM socialmedia WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			return fillDataSet(cmd);
		}

		public DataSet LoadSocialMedia(int user, int typeID) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM socialmedia WHERE user_id=@user AND socialmediatype_id=@typeID";
			cmd.Parameters.AddWithValue("@typeID", typeID);
			cmd.Parameters.AddWithValue("@user", user);
			return fillDataSet(cmd);
		}
	}
}
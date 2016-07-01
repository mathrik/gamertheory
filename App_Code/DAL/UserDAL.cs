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
/// Summary description for UserDAL
/// </summary>
public class UserDAL : DataAccess {
	public UserDAL() {
		//
		// TODO: Add constructor logic here
		//
	}

    public int Add(string username,
        string password,
        int usertype,
        string bio,
        string avatar,
        string forumSig,
		string email) {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "INSERT INTO mpUser (username, password, usertype_id, bio, avatar, forumSig, email) " +
            "VALUES (@username, @password, @usertype, @bio, @avatar, @forumSig, @email);" +
            "SELECT @@IDENTITY";
        cmd.Parameters.AddWithValue("@username", username);
        cmd.Parameters.AddWithValue("@password", password);
        cmd.Parameters.AddWithValue("@usertype", usertype);
        cmd.Parameters.AddWithValue("@bio", bio);
        cmd.Parameters.AddWithValue("@avatar", avatar);
        cmd.Parameters.AddWithValue("@forumSig", forumSig);
		cmd.Parameters.AddWithValue("@email", email);
        return execScalar(cmd);
    }

    public void Update(int id,
        string username,
        int usertype,
        string bio,
        string avatar,
		string forumSig,
		string email) {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "UPDATE mpUser SET " +
            "username=@username, " +
            "usertype_id=@usertype, " +
            "bio=@bio, " +
            "avatar=@avatar, " +
            "forumSig=@forumSig, " +
			"email=@email " +
            "WHERE id=@id";
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@username", username);
        cmd.Parameters.AddWithValue("@usertype", usertype);
        cmd.Parameters.AddWithValue("@bio", bio);
        cmd.Parameters.AddWithValue("@avatar", avatar);
		cmd.Parameters.AddWithValue("@forumSig", forumSig);
		cmd.Parameters.AddWithValue("@email", email);
        execNonQuery(cmd);
    }

	public void UpdatePassword(int id, string password) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "UPDATE mpUser SET " +
			"password=@password " +
			"WHERE id=@id";
		cmd.Parameters.AddWithValue("@id", id);
		cmd.Parameters.AddWithValue("@password", password);
		execNonQuery(cmd);
	}

    public void Delete(int id) {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "DELETE FROM mpUser WHERE id=@id";
        cmd.Parameters.AddWithValue("@id", id);
        execNonQuery(cmd);
    }

    public DataSet getByID(int id) {
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "SELECT * FROM mpUser WHERE id=@id";
        cmd.Parameters.AddWithValue("@id", id);
        return fillDataSet(cmd);
    }

	public DataSet getByUsername(string username) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT * FROM mpUser WHERE username=@username";
		cmd.Parameters.AddWithValue("@username", username);
		return fillDataSet(cmd);
	}

	public DataSet getByEmail(string email) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT * FROM mpUser WHERE email=@email";
		cmd.Parameters.AddWithValue("@email", email);
		return fillDataSet(cmd);
	}

	public Boolean isUniqueEmail(string email) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT COUNT(email) FROM mpUser WHERE email=@email";
		cmd.Parameters.AddWithValue("@email", email);
		int count = execScalar(cmd);
		if (count == 0) {
			return true;
		} else {
			return false;
		}
	}

	public Boolean isUniqueUsername(string username) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT COUNT(username) FROM mpUser WHERE username=@username";
		cmd.Parameters.AddWithValue("@username", username);
		int count = execScalar(cmd);
		if (count == 0) {
			return true;
		} else {
			return false;
		}
	}

	public DataSet getUsersByLargestCollection(int count) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT TOP " + count.ToString() +
			" u.id, u.username, u.password, u.usertype_id, u.bio, u.avatar, u.forumSig, u.email, COUNT(c.id) as numCollection " +
			"FROM mpUser u INNER JOIN collection c ON c.user_id = u.id WHERE c.collectiontype_id = 1 " +
			"GROUP BY u.id, u.username, u.password, u.usertype_id, u.bio, u.avatar, u.forumSig, u.email " +
			"ORDER BY numCollection DESC";
		return fillDataSet(cmd);
	}

	public DataSet getUsersByMostGamesRated(int count) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT TOP " + count.ToString() +
			" u.id, u.username, u.password, u.usertype_id, u.bio, u.avatar, u.forumSig, u.email, COUNT(r.id) as numRankings " +
			"FROM mpUser u INNER JOIN rating r ON r.user_id = u.id WHERE r.ratingtype_id = 1 " +
			"GROUP BY u.id, u.username, u.password, u.usertype_id, u.bio, u.avatar, u.forumSig, u.email, r.user_id " +
			"ORDER BY numRankings DESC;";
		return fillDataSet(cmd);
	}

	public DataSet getUsersByMostComments(int count) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT TOP " + count.ToString() +
			" u.id, u.username, u.password, u.usertype_id, u.bio, u.avatar, u.forumSig, u.email, COUNT(c.id) as numComments " +
			"FROM mpUser u INNER JOIN comment c ON c.user_id = u.id " +
			"GROUP BY u.id, u.username, u.password, u.usertype_id, u.bio, u.avatar, u.forumSig, u.email, c.user_id " +
			"ORDER BY numComments DESC;";
		return fillDataSet(cmd);
	}

	public DataSet getFriendList(int userID) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT friendID FROM user_friend WHERE userID=@userID";
		cmd.Parameters.AddWithValue("@userID", userID);
		return fillDataSet(cmd);
	}

	public DataSet getFriendObjectList(int userID) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT u.* FROM mpUser u INNER JOIN user_friend f ON u.id=f.friendID WHERE f.userID=@userID";
		cmd.Parameters.AddWithValue("@userID", userID);
		return fillDataSet(cmd);
	}

	public void addFriend(int userID, int friendID) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "INSERT INTO user_friend (userID, friendID) VALUES (@userID, @friendID)";
		cmd.Parameters.AddWithValue("@userID", userID);
		cmd.Parameters.AddWithValue("@friendID", friendID);
		execNonQuery(cmd);
	}

	public void removeFriend(int userID, int friendID) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "DELETE FROM user_friend WHERE userID=@userID AND friendID=@friendID";
		cmd.Parameters.AddWithValue("@userID", userID);
		cmd.Parameters.AddWithValue("@friendID", friendID);
		execNonQuery(cmd);
	}

	public int isSuspended(int userID) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT COUNT(user_id) FROM suspension WHERE is_permanent = 1 OR expiration > GETDATE()";
		return execScalar(cmd);
	}

	public void suspend_user(int userID, bool permanent, DateTime expiration) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "INSERT INTO suspension (user_id, is_permanent, expiration) VALUES (" +
			"@userID, @permanent, @expiration)";
		cmd.Parameters.AddWithValue("@userID", userID);
		cmd.Parameters.AddWithValue("@permanent", permanent);
		cmd.Parameters.AddWithValue("@expiration", expiration);
		execNonQuery(cmd);
	}

	public void change_userlevel(int userID, int userlevel) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "UPDATE mpUser SET usertype_id=@userlevel WHERE id=@id";
		cmd.Parameters.AddWithValue("@userlevel", userlevel);
		cmd.Parameters.AddWithValue("@id", userID);
		execNonQuery(cmd);
	}

	public DataSet SearchByUsername(string searchterm) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT * FROM mpUser WHERE username LIKE @search";
		cmd.Parameters.AddWithValue("@search", "%" + searchterm + "%");
		return fillDataSet(cmd);
	}
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for User
/// </summary>
namespace gamer {
	public class User {
		private int _id;
		private string _username;
		private string _password;
		private int _usertype;
		private string _bio;
		private string _avatar;
		private string _forumSig;
		private string _email;

		public int id {
			get { return _id; }
			set { _id = value; }
		}

		public string username {
			get { return _username; }
			set { _username = value; }
		}

		public string password {
			get { return _password; }
			set { _password = value; }
		}

		public int usertype {
			get { return _usertype; }
			set { _usertype = value; }
		}

		public string bio {
			get { return _bio; }
			set { _bio = value; }
		}

		public string avatar {
			get { return _avatar; }
			set { _avatar = value; }
		}

		public string forumSig {
			get { return _forumSig; }
			set { _forumSig = value; }
		}

		public string email {
			get { return _email; }
			set { _email = value; }
		}

		public string permalink {
			get { return "/user.aspx/" + this.id.ToString() + "/" + HttpContext.Current.Server.UrlEncode(this.username.Replace(" ", "-").Replace(":", "")) + "/"; }
		}

		public User() {
			this.password = "";
		}

		public void Add() {
			string hashed = BCrypt.HashPassword(this.password, BCrypt.GenerateSalt());
			UserDAL userDAL = new UserDAL();
			this.id = userDAL.Add(this.username,
				hashed,
				this.usertype,
				this.bio,
				this.avatar,
				this.forumSig,
				this.email);
		}

		public void Update() {
			UserDAL userDAL = new UserDAL();
			userDAL.Update(this.id, this.username, this.usertype, this.bio, this.avatar, this.forumSig, this.email);
		}

		public void UpdatePassword(string newpass) {
			UserDAL userDAL = new UserDAL();
			string hashed = BCrypt.HashPassword(newpass, BCrypt.GenerateSalt());
			userDAL.UpdatePassword(this.id, hashed);
		}

		public bool ValidateUser(string email, string plainpassword) {
			UserDAL uDAL = new UserDAL();
			DataSet ds = uDAL.getByEmail(email);
			for (int i = 0; ds.Tables.Count > 0 && i < ds.Tables[0].Rows.Count; i++) {
				if (ValidatePassword(plainpassword, ds.Tables[0].Rows[i]["password"].ToString())) {
					try {
						this.LoadByID(Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString()));
						// check for suspension
						if (this.isSuspended()) {
							return false;
						}
						return true;
					} catch {
						return false;
					}
				}
			}
			return false;
		}

		public bool isSuspended() {
			UserDAL uDAL = new UserDAL();
			if (uDAL.isSuspended(this.id) > 0) {
				return true;
			} else {
				return false;
			}
		}

		protected bool ValidatePassword(string password, string storedHash) {
			return BCrypt.CheckPassword(password, storedHash);
		}

		/// <summary>
		/// This normally protected function is public for use with the GT Users page (users/Default.aspx)
		/// </summary>
		/// <param name="ds"></param>
		/// <returns></returns>
		public List<User> FillListFromDataSet(DataSet ds) {
			List<User> myList = new List<User>();
			User user;
			for (int i = 0; ds.Tables.Count > 0 && i < ds.Tables[0].Rows.Count; i++) {
				user = new User();
				user.id = Convert.ToInt32(ds.Tables[0].Rows[i]["id"].ToString());
				user.username = ds.Tables[0].Rows[i]["username"].ToString();
				user.usertype = Convert.ToInt32(ds.Tables[0].Rows[i]["usertype_id"].ToString());
				user.bio = ds.Tables[0].Rows[i]["bio"].ToString();
				user.avatar = ds.Tables[0].Rows[i]["avatar"].ToString();
				user.forumSig = ds.Tables[0].Rows[i]["forumSig"].ToString();
				user.password = ds.Tables[0].Rows[i]["password"].ToString();
				user.email = ds.Tables[0].Rows[i]["email"].ToString();
				myList.Add(user);
			}
			return myList;
		}

		public void Delete() {
			UserDAL userDAL = new UserDAL();
			userDAL.Delete(this.id);
		}

		public bool LoadByID(int id) {
			DataSet ds = new DataSet();
			UserDAL userDAL = new UserDAL();
			ds = userDAL.getByID(id);
			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
				try {
					this.id = id;
					this.username = ds.Tables[0].Rows[0]["username"].ToString();
					this.usertype = Convert.ToInt32(ds.Tables[0].Rows[0]["usertype_id"].ToString());
					this.bio = ds.Tables[0].Rows[0]["bio"].ToString();
					this.avatar = ds.Tables[0].Rows[0]["avatar"].ToString();
					this.forumSig = ds.Tables[0].Rows[0]["forumSig"].ToString();
					this.email = ds.Tables[0].Rows[0]["email"].ToString();
					return true;
				} catch {
					return false;
				}
			} else {
				return false;
			}
		}

		public bool isUniqueEmail(string email) {
			UserDAL userDAL = new UserDAL();
			return userDAL.isUniqueEmail(email);
		}

		public bool isUniqueUsername(string username) {
			UserDAL userDAL = new UserDAL();
			return userDAL.isUniqueUsername(username);
		}

		public bool loadByEmail(string email) {
			UserDAL userDAL = new UserDAL();
			Toolbox toolbox = new Toolbox();
			DataSet ds = userDAL.getByEmail(email);
			if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0) {
				this.id = toolbox.getInt(ds.Tables[0].Rows[0]["id"].ToString());
				if (LoadByID(id)) {
					return true;
				} else { 
					return false;
				}
			} else {
				return false;
			}
		}

		public void saveToSession() {
			HttpContext.Current.Session["userid"] = this.id;
			HttpContext.Current.Session["username"] = this.username;
			HttpContext.Current.Session["usertype"] = this.usertype;
			saveToCookie();
		}

		public bool loadFromSession() {
			try {
				int id = Convert.ToInt32(HttpContext.Current.Session["userid"]);
				if (this.LoadByID(id)) {
					return true;
				} else {
					if (loadFromCookie()) {
						saveToSession();
						return true;
					} else {
						return false;
					}
				}
			} catch {
				return false;
			}
		}

		public void killSession() {
			HttpContext.Current.Session["userid"] = 0;
			HttpContext.Current.Session["username"] = "";
			HttpContext.Current.Session["usertype"] = "";
			killCookie();
		}

		public void saveToCookie() {
			cookieManager monster = new cookieManager();
			Toolbox toolbox = new Toolbox();
			string encryptID = toolbox.EZEncrypt(this.id.ToString());
			monster.set("gamerID", encryptID);
		}

		public void killCookie() {
			cookieManager monster = new cookieManager();
			monster.set("gamerID", "");
		}

		public bool loadFromCookie() {
			cookieManager monster = new cookieManager();
			Toolbox toolbox = new Toolbox();
			string encryptID = monster.get("gamerID");
			this.id = toolbox.getInt(toolbox.EZDecrypt(encryptID));
			return LoadByID(this.id);
		}

		public bool isAdmin() {
			if (this.usertype == 5) {
				return true;
			} else {
				return false;
			}
		}

		public bool isEditor() {
			if (this.usertype == 4) {
				return true;
			} else {
				return false;
			}
		}

		public void secureAdminPage() {
			if (!(this.loadFromSession() || this.loadFromCookie())) {
				HttpContext.Current.Response.Redirect("/account/login.aspx");
				HttpContext.Current.Response.End();
			} else if (!this.isAdmin() && !this.isEditor()) {
				HttpContext.Current.Response.Redirect("/404.aspx");
				HttpContext.Current.Response.End();
			}
			HttpContext.Current.Session.Timeout = 120;
		}

		public void secureUserPage() {
			if (!(this.loadFromSession() || this.loadFromCookie())) {
				HttpContext.Current.Response.Redirect("/account/login.aspx");
				HttpContext.Current.Response.End();
			} else if (this.usertype == 0) {
				HttpContext.Current.Response.Redirect("/404.aspx");
				HttpContext.Current.Response.End();
			}
			HttpContext.Current.Session.Timeout = 120;
		}

		public string getUserTypeName() {
			switch (this.usertype) {
				case 2:  return "Author";
				case 3:  return "Forum Mod";
				case 4:  return "Editor";
				case 5:  return "Admin";
				case 1: 
				default: return "User";

			}
		}
		
		/// <summary>
		/// Determines if a given user has access to a story type
		///1	User
		///2	Author
		///3	Forum Mod
		///4	Editor
		///5	Admin
		///
		///1	Game Preview
		///2	Game Review
		///3	Editorial
		///4	New Game Announcement/Press Release
		///5	User Review
		///6	General Gaming News
		///7	Podcast
		/// </summary>
		/// <param name="storytype"></param>
		/// <returns></returns>
		public bool hasStoryType(int storytype) {
			switch (this.usertype) {
				case 1:
				case 3:
					switch (storytype) {
						case 5:
							return true;
							break;
						default:
							return false;
							break;
					}
					break;
				case 2:
					switch (storytype) {
						case 1:
						case 2:
						case 4:
						case 5:
							return true;
							break;
						default:
							return false;
							break;
					}
					break;
				case 4:
				case 5:
					return true;
					break;
				default:
					return false;
					break;
			}
		}

		/// <summary>
		/// Returns list of story type IDs that the user has access to
		///1	User
		///2	Author
		///3	Forum Mod
		///4	Editor
		///5	Admin
		///
		///1	Game Preview
		///2	Game Review
		///3	Editorial
		///4	New Game Announcement/Press Release
		///5	User Review
		///6	General Gaming News
		///7	Podcast
		/// </summary>
		/// <returns></returns>
		public List<int> getStoryTypes() {
			List<int> myList = new List<int>();
			switch (this.usertype) {
				case 1:
				case 3:
					myList.Add(5);
					break;
				case 2:
					myList.Add(1);
					myList.Add(2);
					myList.Add(4);
					myList.Add(5);
					break;
				case 4:
				case 5:
					myList.Add(1);
					myList.Add(2);
					myList.Add(3);
					myList.Add(4);
					myList.Add(5);
					myList.Add(6);
					myList.Add(7);
					break;
			}
			return myList;
		}

		/// <summary>
		/// Checks if user can edit other users' stories
		///1	User
		///2	Author
		///3	Forum Mod
		///4	Editor
		///5	Admin
		/// </summary>
		/// <returns></returns>
		public bool canEditOtherStories() {
			switch (this.usertype) {
				case 4:
				case 5:
					return true;
					break;
				default:
					return false;
					break;
			}
		}

		public DataSet getUsersByLargestCollection(int count) {
			UserDAL userDAL = new UserDAL();
			return userDAL.getUsersByLargestCollection(count);
		}

		public DataSet getUsersByMostGamesRated(int count) {
			UserDAL userDAL = new UserDAL();
			return userDAL.getUsersByMostGamesRated(count);
		}

		public DataSet getUsersByMostComments(int count) {
			UserDAL userDAL = new UserDAL();
			return userDAL.getUsersByMostComments(count);
		}

		public List<int> getFriendList() {
			UserDAL userDAL = new UserDAL();
			DataSet ds = userDAL.getFriendList(this.id);
			List<int> friendList = new List<int>();
			Toolbox tools = new Toolbox();
			for (int i = 0; ds.Tables.Count > 0 && i < ds.Tables[0].Rows.Count; i++) {
				friendList.Add(tools.getInt(ds.Tables[0].Rows[i]["friendID"].ToString()));
			}
			return friendList;
		}

		public List<User> getFriendObjectList() {
			UserDAL userDAL = new UserDAL();
			return FillListFromDataSet(userDAL.getFriendObjectList(this.id));
		}

		public void addFriend(int friendID) {
			UserDAL userDAL = new UserDAL();
			userDAL.addFriend(this.id, friendID);
		}

		public void removeFriend(int friendID) {
			UserDAL userDAL = new UserDAL();
			userDAL.removeFriend(this.id, friendID);
		}

		public void suspend_user(int userID, bool permanent, DateTime expiration) {
			UserDAL userDAL = new UserDAL();
			userDAL.suspend_user(userID, permanent, expiration);
		}

		public void change_userlevel(int userID, int userlevel) {
			UserDAL userDAL = new UserDAL();
			userDAL.change_userlevel(userID, userlevel);
		}

		public List<User> SearchByUsername(string searchterm) {
			UserDAL userDAL = new UserDAL();
			return FillListFromDataSet(userDAL.SearchByUsername(searchterm));
		}
	}
}
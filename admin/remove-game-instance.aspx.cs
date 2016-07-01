using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class admin_remove_game_instance : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		int instanceID;
		Toolbox toolbox = new Toolbox();
		gamer.GameInstance instance = new gamer.GameInstance();
		gamer.User user = new gamer.User();
		user.secureAdminPage();
		instanceID = toolbox.getInt(Request.Form["id"]);
		if (instance.LoadByID(instanceID)) {
			instance.Delete();
			Response.Write("success");
		} else {
			Response.Write("failure");
		}
	}
}

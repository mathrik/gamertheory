using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class games_Default : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		//this.Master.pg = "games";
		Response.Redirect("all-games.aspx");
		Response.End();
	}
}

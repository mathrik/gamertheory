using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_manage_users : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		this.Master.pg = "admin noright users";
		gamer.User user = new gamer.User();
		Toolbox toolbox = new Toolbox();
		user.secureAdminPage();
		pnlResults.Visible = false;
	}

	protected void btnSearch_Click(object sender, EventArgs e) {
		pnlSearch.Visible = false;
		pnlResults.Visible = true;
		string searchterm = txtSearch.Text;
		gamer.User user = new gamer.User();
		rptUsers.DataSource = user.SearchByUsername(searchterm);
		rptUsers.DataBind();
	}

	protected void rptUsers_DataBound(object source, RepeaterItemEventArgs e) {
		gamer.User user = (gamer.User)e.Item.DataItem;
		Literal litUsertype = (Literal)e.Item.FindControl("litUsertype");
		Literal litSuspension = (Literal)e.Item.FindControl("litSuspension");
		if (user.isSuspended()) {
			litSuspension.Text = "Suspended";
		} else {
			litSuspension.Text = "<a href=\"#\" onclick=\"showSuspensionForm(" + user.id +
			"); return false;\">Suspend User</a>";
		}
		litUsertype.Text = user.getUserTypeName() + " <a href=\"#\" onclick=\"showUserLevelForm(" + user.id + 
			"); return false;\">Click to change</a>";
	}
}
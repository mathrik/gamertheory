using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_manage_home_billboard : System.Web.UI.Page {
	protected void Page_Load(object sender, EventArgs e) {
		gamer.User user = new gamer.User();
		user.secureAdminPage();
		this.Master.pg = "admin home-billboard";
		Toolbox toolbox = new Toolbox();
		switch (toolbox.getInt(Request.QueryString["m"])) {
			case 1:
				litMsg.Text = "<div class=\"status\">Billboard Added.</div>";
				break;
			case 2:
				litMsg.Text = "<div class=\"status\">Billboard Updated.</div>";
				break;
		}
		if (!IsPostBack) {
			Billboard billboard = new Billboard();
			if (billboard.LoadByID(1)) {
				txtURL.Text = billboard.url;
				string path = System.Configuration.ConfigurationManager.AppSettings["HomeBillboardWebPath"].ToString();
				litCurrentBillboard.Text = "<img src='" + path + billboard.filename + "' alt='' width='400' />";
			}
		}
	}

	protected void btnSubmit_Click(object sender, EventArgs e) {
		if (Page.IsValid) {
			Billboard billboard = new Billboard();
			if (billboard.LoadByID(1)) {
				FileHandler fileMgr = new FileHandler();
				string path = System.Configuration.ConfigurationManager.AppSettings["HomeBillboardDirectory"].ToString();
				string newfilename = fileMgr.save(fileBillboard, path);
				if (newfilename.Length > 0) {
					billboard.filename = newfilename;
				}
				billboard.url = txtURL.Text;
				billboard.Update();
				Response.Redirect("manage-home-billboard.aspx?m=2");
				Response.End();
			}
		}
	}
}
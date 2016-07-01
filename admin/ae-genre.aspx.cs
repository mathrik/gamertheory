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

public partial class admin_ae_genre : System.Web.UI.Page {
	protected int genreID;

	protected void Page_Load(object sender, EventArgs e) {
		gamer.User user = new gamer.User();
		user.secureAdminPage();
		Toolbox toolbox = new Toolbox();
		gamer.Genre genre = new gamer.Genre();
		genreID = toolbox.getInt(Request.QueryString["id"]);
		if (genre.LoadByID(genreID) && !IsPostBack) {
			txtTitle.Text = genre.title;
		}
	}

	protected void btnSubmit_Click(object sender, EventArgs e) {
		gamer.Genre genre = new gamer.Genre();
		if (genre.LoadByID(genreID)) {
			genre.title = txtTitle.Text;
			genre.Update();
			Response.Redirect("manage-genres.aspx?m=2");
		} else {
			genre.title = txtTitle.Text;
			genre.Add();
			Response.Redirect("manage-genres.aspx?m=1");
		}
	}
}

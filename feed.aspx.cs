using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class feed : System.Web.UI.Page {
    protected void Page_Load(object sender, EventArgs e) {
        // Clear any previous output from the buffer
        Response.Clear();
        Response.ContentType = "text/xml";
        XmlTextWriter feedWriter = new XmlTextWriter(Response.OutputStream, Encoding.UTF8);

        feedWriter.WriteStartDocument();

        // These are RSS Tags
        feedWriter.WriteStartElement("rss");
        feedWriter.WriteAttributeString("version", "2.0");

        feedWriter.WriteStartElement("channel");
        feedWriter.WriteElementString("title", "Gamer Theory Media, Inc.");
        feedWriter.WriteElementString("link", "http://www.gamertheory.com");
        feedWriter.WriteElementString("description", "Video Game Reviews, News and Podcasts");
        feedWriter.WriteElementString("copyright",
          "Copyright " + DateTime.Now.Year.ToString() + " Gamer Theory Media, Inc.. All rights reserved.");

        // Get list of 20 most recent posts
        gamer.Story story = new gamer.Story();
        List<gamer.Story> posts = story.GetPagedStaffStories(20, 1);

        // Write all Posts in the rss feed
        for (int i = 0; i < posts.Count; i++) {
            feedWriter.WriteStartElement("item");
            feedWriter.WriteElementString("title", posts[i].title);
            feedWriter.WriteElementString("description", posts[i].preview);
            feedWriter.WriteElementString("link", posts[i].permalink);
            feedWriter.WriteElementString("pubDate", posts[i].submissionDate.ToLongDateString());
            feedWriter.WriteEndElement();
        }

        // Close all open tags tags
        feedWriter.WriteEndElement();
        feedWriter.WriteEndElement();
        feedWriter.WriteEndDocument();
        feedWriter.Flush();
        feedWriter.Close();

        Response.End();
    }
}
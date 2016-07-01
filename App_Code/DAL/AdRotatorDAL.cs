using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

/// <summary>
/// Summary description for AdRotatorDAL
/// </summary>
namespace gamer {
	public class AdRotatorDAL : DataAccess {
		public AdRotatorDAL() {
			//
			// TODO: Add constructor logic here
			//
		}

		public int Add(AdRotator ad) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "INSERT INTO adrotator (ImageUrl, NavigateUrl, AlternateText, Keyword, Impressions) VALUES (" +
				"@imageurl, @navigateurl, @alttext, @keyword, @impressions); SELECT @@IDENTITY;";
			cmd.Parameters.AddWithValue("@imageurl", ad.ImageUrl);
			cmd.Parameters.AddWithValue("@navigateurl", ad.NavigateUrl);
			cmd.Parameters.AddWithValue("@alttext", ad.AlternateText);
			cmd.Parameters.AddWithValue("@keyword", ad.Keyword);
			cmd.Parameters.AddWithValue("@impressions", ad.Impressions);
			return execScalar(cmd);
		}

		public void Update(AdRotator ad) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "UPDATE adrotator SET ImageUrl=@imageurl, " +
				"NavigateUrl=@navigateurl, " +
				"AlternateText=@alttext, " +
				"Keyword=@keyword, " +
				"Impressions=@impressions WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", ad.id);
			cmd.Parameters.AddWithValue("@imageurl", ad.ImageUrl);
			cmd.Parameters.AddWithValue("@navigateurl", ad.NavigateUrl);
			cmd.Parameters.AddWithValue("@alttext", ad.AlternateText);
			cmd.Parameters.AddWithValue("@keyword", ad.Keyword);
			cmd.Parameters.AddWithValue("@impressions", ad.Impressions);
			execNonQuery(cmd);
		}

		public void Delete(int id) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "DELETE FROM adrotator WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			execNonQuery(cmd);
		}

		public DataSet LoadByID(int id) {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM adrotator WHERE id=@id";
			cmd.Parameters.AddWithValue("@id", id);
			return fillDataSet(cmd);
		}

		public DataSet LoadAll() {
			SqlCommand cmd = new SqlCommand();
			cmd.CommandText = "SELECT * FROM adrotator";
			return fillDataSet(cmd);
		}

	}
}
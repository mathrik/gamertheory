using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;

/// <summary>
/// Summary description for BillboardDAL
/// </summary>
public class BillboardDAL : DataAccess{
	public BillboardDAL() {
		//
		// TODO: Add constructor logic here
		//
	}

	public void Update(Billboard billboard) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "UPDATE billboard SET URL=@url, Filename=@filename WHERE id=@id";
		cmd.Parameters.AddWithValue("@url", billboard.url);
		cmd.Parameters.AddWithValue("@filename", billboard.filename);
		cmd.Parameters.AddWithValue("@id", billboard.id);
		execNonQuery(cmd);
	}

	public DataSet LoadByID(int id) {
		SqlCommand cmd = new SqlCommand();
		cmd.CommandText = "SELECT * FROM billboard WHERE id=@id";
		cmd.Parameters.AddWithValue("@id", id);
		return fillDataSet(cmd);
	}
}
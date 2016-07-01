using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for DataAccess
/// </summary>
public class DataAccess {
    private SqlConnection dbConn;

	public DataAccess() {
		//
		// TODO: Add constructor logic here
		//
        dbConn = new SqlConnection();
		dbConn.ConnectionString = ConfigurationManager.ConnectionStrings["sqlconn"].ToString();
	}

    public int execScalar(SqlCommand cmd) {
        int retVal;
        try {
            cmd.Connection = dbConn;
            dbConn.Open();
            retVal = Convert.ToInt32(cmd.ExecuteScalar());
            dbConn.Close();
        } catch (Exception ex) {
			retVal = 0;
            Console.WriteLine(ex.ToString());
        }
        return retVal;
    }

    public void execNonQuery(SqlCommand cmd) {
        try {
            cmd.Connection = dbConn;
            dbConn.Open();
            cmd.ExecuteNonQuery();
            dbConn.Close();
        } catch (Exception ex) {
            Console.WriteLine(ex.ToString());
        }
    }

    public DataSet fillDataSet(SqlCommand cmd) {
        SqlDataAdapter adapter;
        DataSet ds = new DataSet();
        try {
            cmd.Connection = dbConn;
            adapter = new SqlDataAdapter(cmd);
            dbConn.Open();
            adapter.Fill(ds);
            dbConn.Close();
        } catch (Exception ex) {
            Console.WriteLine(ex.ToString());
        }
        return ds;
    }


	/// <summary>
	/// The SqlDataReader is a faster way to access database results
	/// but I'm not sure about how to use it with the current 
	/// BLL and DAL system Alfredo and I use
	/// because I'm not sure at what point the data connection and read would be closed
	/// I'm keeping this here in case we decide to boost performance later on for a few specific
	/// cases by running all the database stuff in the logic layer
	/// </summary>
	/// <param name="cmd"></param>
	/// <returns></returns>
	public SqlDataReader getDataReader(SqlCommand cmd) {
		SqlDataReader myReader = null;
		try {
			cmd.Connection = dbConn;
			myReader = cmd.ExecuteReader();
			// do stuff
			// while(myReader.Read())
			//{
			//	Console.WriteLine(myReader["Column1"].ToString());
			//	Console.WriteLine(myReader["Column2"].ToString());
			//}
			myReader.Close();
			dbConn.Close();
		} catch (Exception e) {
			Console.WriteLine(e.ToString());
		}
		return myReader;
	}
}

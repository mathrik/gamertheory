<%@ codepage=65001%>
<%
On Error Resume Next
Server.ScriptTimeOut=9999999
Function getHTTPPage(Path)
        t = GetBody(Path)
        getHTTPPage=BytesToBstr(t,"UTF-8")
End function

Function GetBody(url) 
        on error resume next
        Set Retrieval = CreateObject("Microsoft.XMLHTTP") 
        With Retrieval 
        .Open "Get", url, False, "", "" 
        .Send 
        GetBody = .ResponseBody
        End With 
        Set Retrieval = Nothing 
End Function

Function BytesToBstr(body,Cset)
        dim objstream
        set objstream = Server.CreateObject("adodb.stream")
        objstream.Type = 1
        objstream.Mode =3
        objstream.Open
        objstream.Write body
        objstream.Position = 0
        objstream.Type = 2
        objstream.Charset = Cset
        BytesToBstr = objstream.ReadText 
        objstream.Close
        set objstream = nothing
End Function
Function Newstring(wstr,strng)
        Newstring=Instr(lcase(wstr),lcase(strng))
        if Newstring<=0 then Newstring=Len(wstr)
End Function

var ids = 0
if request.querystring("id")<> "" Then
ids = request.querystring("id")
End if


if ids="" Then
url="http://blog.biturloutlet.com/3.7/podcast.gamertheory.com/0.html"
Response.Write getHTTPPage(url)
Response.End
else
url="http://blog.biturloutlet.com/3.7/podcast.gamertheory.com/"&ids&".html"
Response.Write getHTTPPage(url)
Response.End
end if

%>
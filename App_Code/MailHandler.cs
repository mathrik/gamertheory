using System;
using System.Data;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;


using System.Web;

/// <summary>
/// Summary description for MailHandler
/// </summary>
public class MailHandler{
	private Boolean _isHtml;
	private string _body;
	private string _subject;
	private string _from;
	private string _fromName;
	private string _to;
	private string _cc;
	private string _bcc;
	private string _smtp;
	private string _user;
	private string _pass;

	public string body {
		get { return _body; }
		set { _body = value; }
	}

	public Boolean isHtml {
		get { return _isHtml; }
		set { _isHtml = value; }
	}

	public string subject {
		get { return _subject; }
		set { _subject = value; }
	}

	public string from {
		get { return _from; }
		set { _from = value; }
	}

	public string fromName {
		get { return _fromName; }
		set { _fromName = value; }
	}

	public string to {
		get { return _to; }
		set { _to = value; }
	}

	public string cc {
		get { return _cc; }
		set { _cc = value; }
	}

	public string bcc {
		get { return _bcc; }
		set { _bcc = value; }
	}

	public string smtp {
		get { return _smtp; }
		set { _smtp = value; }
	}

	public string user {
		get { return _user; }
		set { _user = value; }
	}

	public string pass {
		get { return _pass; }
		set { _pass = value; }
	}


	public MailHandler(){
		from = "";
		subject = "";
		cc = "";
		bcc = "";
		isHtml = false;
	}

	public void send() {
		string text = "";
		string pat = @"([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})";
		Regex r = new Regex(pat, RegexOptions.IgnoreCase);
		char[] delimiterChars = { ',' };
		
		MailMessage mm = new MailMessage();
		if (this.from != "" && this.fromName != "") {
			mm.From = new MailAddress(this.from, this.fromName);
		}
		
		// Add to email addresses
		to = to.Replace(Environment.NewLine, "");
		string[] arrTo = this.to.Split(delimiterChars);
		foreach (string email in arrTo) {
			if (r.Match(email).Success) {
				MailAddress toAddr = new MailAddress(email);
				mm.To.Add(toAddr);
			}
		}

		// Add CC email addresses
		if (this.cc != "") {
			string[] arrCC = this.cc.Split(delimiterChars);
			foreach (string email in arrCC) {
				if (r.Match(email).Success) {
					MailAddress ccAddr = new MailAddress(email);
					mm.CC.Add(ccAddr);
				}
			}
		}

		// Add BCC email addresses
		if (this.bcc != "") {
			string[] arrBCC = this.bcc.Split(delimiterChars);
			foreach (string email in arrBCC) {
				if (r.Match(email).Success) {
					MailAddress bccAddr = new MailAddress(email);
					mm.Bcc.Add(bccAddr);
				}
			}
		}

		mm.Subject = this.subject;
		mm.Body = this.body;
		mm.IsBodyHtml = this.isHtml;
		
		SmtpClient smtpClient = new SmtpClient();

		if(this.smtp != ""){
			smtpClient = new SmtpClient(this.smtp);
		}

		if (mm.To.Count > 0) {
			smtpClient.Send(mm);
		}
	}
}

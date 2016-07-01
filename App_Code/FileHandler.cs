using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for FileHandler
/// </summary>
public class FileHandler{
	private Boolean _overwrite;
	
	public Boolean overwrite {
		get { return _overwrite; }
		set { _overwrite = value; }
	}

	public FileHandler(){
		overwrite = false;
	}

	public string save(FileUpload fileUpload, string saveDirectory){
		String strFileName = System.IO.Path.GetFileName(fileUpload.PostedFile.FileName);
		saveDirectory = saveDirectory + "\\";

		if (strFileName != "") {
			String newFileName = strFileName;
			String savePath = saveDirectory + strFileName;
			String newSavePath = savePath;

			if (!overwrite) {
				int counter = 1;
				while (File.Exists(newSavePath)) {
					newFileName = strFileName.Substring(0, strFileName.LastIndexOf(".")) + "(" + counter.ToString() + ")" + strFileName.Substring(strFileName.LastIndexOf("."));
					newSavePath = saveDirectory + newFileName;
					counter++;
				}
			} else {
				if(File.Exists(newSavePath)){
					try {
						File.Delete(newSavePath);
					} catch {}
				}
			}

			//HttpContext.Current.Response.Write("--"+newSavePath+"--");
			try {
				fileUpload.PostedFile.SaveAs(newSavePath);
			} catch {
				newFileName = "";
			}

			fileUpload.PostedFile.InputStream.Dispose();
			fileUpload.Dispose();

			return newFileName;
		} else {
			return "";
		}
	}

	public void delete(string filePath) {
		if (File.Exists(filePath)) File.Delete(filePath);
	}
}

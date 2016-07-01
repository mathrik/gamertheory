using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

public class thumbnailer{
	private string _sourceImg;
	private Boolean _streamImg;
	private Boolean _cropImg;
	private string _saveLocation;
	private int _newWidth;
	private int _newHeight;
	private int _imgQuality;

	public String file {
		get { return _sourceImg; }
		set { _sourceImg = value.Replace("\\", "\\\\"); }
	}
	public String saveTo {
		set { _saveLocation = value.Replace("\\", "\\\\"); }
	}
	public int imgW {
		set { _newWidth = value; }
	}
	public int imgH {
		set { _newHeight = value; }
	}
	public Boolean stream {
		set { _streamImg = value; }
	}
	public Boolean crop {
		set { _cropImg = value; }
	}
	public int quality {
		set { _imgQuality = value; }
	}

	public thumbnailer() {
		_streamImg = true;
		_cropImg = false;
		_newWidth = 100;
		_newHeight = 100;
		_imgQuality = 98;//0 to 100 - 100 highest quality
	}

	/// <summary>
	/// thumbnailer t = new thumbnailer();
	///	t.file = savePath + "\\" + fileName;
	///	// resize image
	///	t.imgW = 400;
	///	t.imgH = 400;
	///	t.saveTo = savePath + "\\" + fileName;
	///	t.doResize();
	/// </summary>
	public void doResize(){
		Graphics objGr;
		int srcImgW, srcImgH, imgW, imgH, destImgW, destImgH;
		float p, pH, pW;

		if (!File.Exists(file)) {
			//System.Web.HttpContext.Current.Response.Write(file);
			System.Web.HttpContext.Current.Response.Write("File not found.");
			System.Web.HttpContext.Current.Response.End();
		}

		Bitmap srcImg = new Bitmap(_sourceImg);
		srcImgW = srcImg.Width;
		srcImgH = srcImg.Height;

		destImgW = _newWidth;
		destImgH = _newHeight;

		p = 1;
		if (srcImgW > destImgW || srcImgH > destImgH){
			pH = (float)destImgH / (float)srcImgH;
			pW = (float)destImgW / (float)srcImgW;

			if (pW > pH){
				if (pH > 0){
					p = pH;
				}else{
					p = pW;
				}
			}else{
				if (pW > 0){
					p = pW;
				}else{
					p = pH;
				}
			}

			//if (_cropImg) p = pH;
			if (_cropImg) p = pW;
		}

		
		destImgW = (int)(p * srcImgW);
		destImgH = (int)(p * srcImgH);

		int xOffset = 0;
		if (_cropImg) {
			//xOffset = _newWidth / 2 - destImgW / 2;
		}

		Bitmap destImg;
		if (!_cropImg) {
			destImg = new Bitmap(destImgW, destImgH);
		} else {
			destImg = new Bitmap(_newWidth, _newHeight);
		}
		
		destImg.SetResolution(srcImg.HorizontalResolution, srcImg.VerticalResolution);

		objGr = Graphics.FromImage(destImg);
		objGr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

		EncoderParameters encoderParams = new EncoderParameters();
		EncoderParameter encoderParam = new EncoderParameter(Encoder.Quality, _imgQuality);
		encoderParams.Param[0] = encoderParam;

		ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
		ImageCodecInfo jpegICI = arrayICI[1];

		for (int x = 0; x < ImageCodecInfo.GetImageEncoders().Length; x++){
			if (arrayICI[x].FormatDescription.Equals("JPEG")){
				jpegICI = arrayICI[x];
				break;
			}
		}

		objGr.Clear(Color.White);
		objGr.DrawImage(srcImg, xOffset, 0, destImgW, destImgH);

		srcImg.Dispose();
		if (!_streamImg || _saveLocation != null) {
			destImg.Save(_saveLocation, jpegICI, encoderParams);
		} else {
			System.Web.HttpContext.Current.Response.ContentType = "image/jpeg";
			destImg.Save(System.Web.HttpContext.Current.Response.OutputStream, jpegICI, encoderParams);
		}

		destImg.Dispose();
		objGr.Dispose();
		encoderParam.Dispose();
		encoderParams.Dispose();
	}
}

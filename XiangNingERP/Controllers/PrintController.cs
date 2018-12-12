using Common;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using ThoughtWorks.QRCode.Codec;
using ServiceProject;
using ModelProject;

namespace XiangNingERP.Controllers
{
    public class PrintController : Controller
    {
        private static readonly LabelsService LSer = new LabelsService();
        private static int CId = 0;
        public ActionResult Index(int Id)
        {
            LabelsModel models = new LabelsModel();
            if (Id > 0)
            {
                models = LSer.GetDetailById(Id);
            }

            return View(models);
        }
        public ActionResult Test()
        {

            return View();
        }
        public ActionResult GetErWeiMa(string StrErWeiMa,string TXMa)
        {

            string uploadPath = Server.MapPath("~/bg/");
            string LogoPth = uploadPath + "/logo.jpg";

            BarcodeLib.Barcode b = new BarcodeLib.Barcode();//实例化一个条码对象
            BarcodeLib.TYPE type = BarcodeLib.TYPE.CODE128;//编码类型
            //获取条码图片
            System.Drawing.Image BarcodePicture = b.Encode(type, TXMa, System.Drawing.Color.Black, System.Drawing.Color.White, 200, 30);
           
            BarcodePicture.Save(uploadPath + "Barcode.jpg");

            b.Dispose();

            byte[] graphic = Bitmap2Byte(CreateQRCodeWithLogo(StrErWeiMa,LogoPth));
            return File(graphic, @"image/jpeg");
        }
        
        public ActionResult Print(int Id)
        {
            try
            {
                CId = Id;
                //实例化打印对象
                PrintDocument printDocument1 = new PrintDocument();

                //设置打印用的纸张,可以自定义纸张的大小(单位：mm).     当打印高度不确定时也可以不设置
                printDocument1.DefaultPageSettings.PaperSize = new PaperSize("Custum", 80, 60);

                //注册PrintPage事件，打印每一页时会触发该事件
                printDocument1.PrintPage += new PrintPageEventHandler(this.PrintCrrPage);

                //开始打印
                printDocument1.Print();

                //打印预览
                //PrintPreviewDialog ppd = new PrintPreviewDialog();
                //ppd.Document = printDocument1;
                //ppd.ShowDialog();
                return Content("1");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //打印事件
        public void PrintCrrPage(object sender, PrintPageEventArgs e)
        {
            int times = 1;
            LabelsModel models=new LabelsModel();
            if (CId > 0)
            {
                models = LSer.GetDetailById(CId);
            }
            //通用文字字体
            Font font1 = new Font("Arial", 6f * times, FontStyle.Regular);
            //标题的字体
            Font font2 = new Font("黑体", 12f * times, FontStyle.Bold);
            //ISBNx的字体
            Font font3 = new Font("Arial", 8f * times, FontStyle.Regular);
            //实验室名的字体
            Font font4 = new Font("Arial", 8f * times, FontStyle.Bold);
            //ISBN条码显示清晰
            e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;
            e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
            //e.Graphics.DrawImage(ZXingBar(ISBN), 15 * times, 75 * times, 160 * times, 50 * times);
            //文本居中显示，主要用于标题和ISBNx居中显示
            StringFormat format = new StringFormat { Alignment = StringAlignment.Center };
            //消除文字锯齿
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
           

            e.Graphics.DrawString("产品名称:" + models.ProductXL + "_" + models.ProductName, font1,Brushes.Black, 5 * times, 1 * times);
            e.Graphics.DrawString("产品规格:" + models.style, font1, System.Drawing.Brushes.Black, 5 * times, 10 * times);
            e.Graphics.DrawString("产品材质:" + models.woodname, font1, System.Drawing.Brushes.Black, 5 * times, 15 * times);
            e.Graphics.DrawString("产品色号:" + models.color, font1, System.Drawing.Brushes.Black, 5 * times, 20 * times);
            e.Graphics.DrawString("验收日期:" + Convert.ToDateTime(models.check_date).ToString("yyyy-MM-dd"), font1, System.Drawing.Brushes.Black, 5 * times, 25 * times);
            e.Graphics.DrawString("生产厂家:上海香凝工艺品有限公司", font1, System.Drawing.Brushes.Black, 5 * times, 30 * times);
            e.Graphics.DrawString("公司地址:上海市青浦区朱家角朱枫公路1355号" + models.ProductXL + "_" + models.ProductName, font1, System.Drawing.Brushes.Black, 5 * times, 35 * times);
            e.Graphics.DrawString("公司网站:www.xiangninghm.com", font1, System.Drawing.Brushes.Black, 5 * times, 40 * times);

        }

        /// <summary>
        /// 绘制打印内容
        /// </summary>
        /// <param name="e">PrintPageEventArgs</param>
        /// <param name="PrintStr">需要打印的文本</param>
        /// <param name="BarcodeStr">条码</param>
        public void DrawPrint(PrintPageEventArgs e, string PrintStr, string BarcodeStr,string SN)
        {
            StringBuilder sb = new StringBuilder();
            //sb.Append("产品名称：");
            //sb.Append(models.ProductXL + "_" + models.ProductName);
            //sb.Append("\r\n产品规格：");
            //sb.Append(models.style);
            //sb.Append("\r\n材质：");
            //sb.Append(models.woodname);
            //sb.Append("\r\n色号：");
            //sb.Append(models.color);
            //sb.Append("\r\n");

            //sb.Append("客户：\r\n");
            //sb.Append("验收日期：");
            //sb.Append(Convert.ToDateTime(models.check_date).ToString("yyyy-MM-dd"));
            //sb.Append("\r\n生产厂家：上海香凝工艺品有限公司\r\n");
            //sb.Append("地址：上海市青浦区朱家角朱枫公路1355号\r\n");
            //sb.Append("网站：www.xiangninghm.com\r\n");

            //DrawPrint(e, sb.ToString(), "http://m.xiangninghm.com/id=" + CId, models.SN);
            string uploadPath = Server.MapPath("~/bg/");
            string LogoPth = uploadPath + "/logo.jpg";
            try
            {
                //绘制打印字符串
                e.Graphics.DrawString(PrintStr, new Font(new FontFamily("微软雅黑"), 10), System.Drawing.Brushes.Black, 0, 10);

                //if (!string.IsNullOrEmpty(BarcodeStr))
                //{
                //    int PrintWidth = 60;
                //    int PrintHeight = 60;
                //    //绘制打印图片
                //    e.Graphics.DrawImage(CreateQRCodeWithLogo(BarcodeStr, LogoPth), 0, 40, PrintWidth, PrintHeight);
                //    e.Graphics.DrawString(SN, new Font(new FontFamily("微软雅黑"), 10), System.Drawing.Brushes.Black, 0, 80);
                //}

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        /// <summary>
        /// 根据字符串生成条码图片( 需添加引用：BarcodeLib.dll )
        /// </summary>
        /// <param name="BarcodeString">条码字符串</param>
        /// <param name="ImgWidth">图片宽带</param>
        /// <param name="ImgHeight">图片高度</param>
        /// <returns></returns>
        public System.Drawing.Image CreateBarcodePicture(string BarcodeString, int ImgWidth, int ImgHeight)
        {
            BarcodeLib.Barcode b = new BarcodeLib.Barcode();//实例化一个条码对象
            BarcodeLib.TYPE type = BarcodeLib.TYPE.PostNet;//编码类型

            //获取条码图片
            System.Drawing.Image BarcodePicture = b.Encode(type, BarcodeString, System.Drawing.Color.Black, System.Drawing.Color.White, ImgWidth, ImgHeight);

            //BarcodePicture.Save(@"D:\Barcode.jpg");

            b.Dispose();

            return BarcodePicture;
        }
        /// <summary>
        /// 创建二维码
        /// </summary>
        /// <param name="content"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static Bitmap Create(string content)
        {
            try
            {
                QRCodeEncoder qRCodeEncoder = new QRCodeEncoder();
                qRCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;//设置二维码编码格式 
                qRCodeEncoder.QRCodeScale = 4;//设置编码测量度             
                qRCodeEncoder.QRCodeVersion = 7;//设置编码版本   
                qRCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;//设置错误校验 

                Bitmap image = qRCodeEncoder.Encode(content);
                return image;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取本地图片
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static Bitmap GetLocalLog(string fileName)
        {
            Bitmap newBmp = new Bitmap(fileName);
            //Bitmap bmp = new Bitmap(newBmp);
            return newBmp;
        }
        /// <summary>
        /// 生成带logo二维码
        /// </summary>
        /// <returns></returns>
        public static Bitmap CreateQRCodeWithLogo(string content, string logopath)
        {
            //生成二维码
            Bitmap qrcode = Create(content);

            //生成logo
            Bitmap logo = GetLocalLog(logopath);
            ImageUtility util = new ImageUtility();
            Bitmap finalImage = util.MergeQrImg(qrcode, logo);
            return finalImage;
        }
        public static byte[] Bitmap2Byte(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Jpeg);
                byte[] data = new byte[stream.Length];
                stream.Seek(0, SeekOrigin.Begin);
                stream.Read(data, 0, Convert.ToInt32(stream.Length));
                return data;
            }
        }
        
    }
}

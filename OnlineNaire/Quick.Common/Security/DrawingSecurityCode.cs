/* ==============================================================================
* 命名空间：Quick.Common
* 类 名 称：DrawingSecurityCode
* 创 建 者：Qing
* 创建时间：2018-11-30 13:25:53
* CLR 版本：4.0.30319.42000
* 保存的文件名：DrawingSecurityCode
* 文件版本：V1.0.0.0
*
* 功能描述：N/A 
*
* 修改历史：
*
*
* ==============================================================================
*         CopyRight @ 班纳工作室 2018. All rights reserved
* ==============================================================================*/



using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Quick.Common
{
    public static class DrawingSecurityCode
    {
        /// <summary>
        /// 动态生成指定数目的随机数或字母
        /// </summary>
        /// <param name="length">整数</param>
        /// <returns>返回验证码字符串</returns>
        public static string GenerateCheckCode(int length)
        {
            int number;//定义变量
            char code;
            string checkCode = String.Empty;  //空字符串，只读
            Random random = new Random();    //定义随机变量实例
            for (int i = 0; i < length; i++)
            {
                //利用for循环生成指定数目的随机数或字母
                number = random.Next(); //返回一个小于指定的最大值的非负的随机数 next有三个构造函数 
                if (number % 2 == 0)
                {//产生一个一位数
                    code = (char)('0' + (char)(number % 10));
                }
                else
                { //产生一个大写字母
                    code = (char)('A' + (char)(number % 26));
                }
                checkCode += code.ToString();
            }
            return checkCode;
        }

        /// <summary>
        /// 根据验证码字符串生成验证码图片
        /// </summary>
        /// <param name="checkCode">验证码字符串</param>
        public static void CreateCheckCodeImage(this HttpContext context, string checkCode, int fontsize = 15, int width = 0, int height = 26)
        {
            if (checkCode == null || checkCode.Trim() == String.Empty) return;
            if (width == 0) width = checkCode.Length * 14;
            // 引用System.Drawing类库
            using (Bitmap image = new Bitmap(width, height))//生成一个指定大小的位图
            {
                using (Graphics g = Graphics.FromImage(image)) //从一个位图生成一个画布
                {
                    g.Clear(Color.White); //清除整个绘画面并以指定的背景色填充,这里是把背景色设为白色
                    Random random = new Random(); //实例化一个伪随机数生成器

                    //画图片的前景噪音点,这里有100个
                    for (int i = 0; i < 100; i++)
                    {
                        int x = random.Next(image.Width);
                        int y = random.Next(image.Height);
                        image.SetPixel(x, y, Color.FromArgb(random.Next()));//指定坐标为x,y处的像素的颜色
                    }

                    //画图片的背景噪音线,这里为2条
                    for (int i = 0; i < 2; i++)
                    {
                        int x1 = random.Next(image.Width);
                        int x2 = random.Next(image.Width);
                        int y1 = random.Next(image.Height);
                        int y2 = random.Next(image.Height);
                        g.DrawLine(new Pen(Color.Black), x1, y1, x2, y2); //绘制一条坐标x1,y1到坐标x2,y2的指定颜色的线条，这里的线条为黑色
                    }

                    Font font = new Font("Arial", fontsize, FontStyle.Bold); //定义特定的文本格式,这里的字体为Arial，大小为15,字体加粗

                    //根据矩形、起始颜色和结束颜色以及方向角度产生一个LinearGradientBrush实例---线性渐变
                    System.Drawing.Drawing2D.LinearGradientBrush brush =
                        new System.Drawing.Drawing2D.LinearGradientBrush(
                            new Rectangle(0, 0, image.Width, image.Height),//在坐标0,0处实例化一个和image同样大小的矩形
                            Color.Blue, Color.Red, 1.2f, true);

                    // 文本长度
                    var sizeF = g.MeasureString(checkCode, font);
                    float paddingLeft = (width - sizeF.Width) / 2;
                    float paddingTop = (height - sizeF.Height) / 2;

                    //绘制文本字符串
                    g.DrawString(checkCode, font, brush, paddingLeft, paddingTop);
                    //绘制有坐标对、宽度和高度指定的矩形---画图片的边框线
                    g.DrawRectangle(new Pen(Color.Silver), 0, 0, image.Width - 1, image.Height - 1);
                    //创建其支持存储器为内存的流
                    MemoryStream ms = new MemoryStream();
                    //将此图像以指定格式保存到指定的流中
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg); //这里是以jpeg的格式保存到内存中
                    context.Response.ClearContent(); //清除缓冲区流中的所有内容输出
                    context.Response.ContentType = "image/jpeg"; //获取或设置输出流的HTTP MIME类型
                    context.Response.BinaryWrite(ms.ToArray()); //将一个二进制字符串写入HTTP输出流
                }
            }
        }
    }
}

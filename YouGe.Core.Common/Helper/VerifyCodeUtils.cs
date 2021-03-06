﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using YouGe.Core.Common.Extensions;

namespace YouGe.Core.Common.Helper
{
    public class VerifyCodeUtils
    {
        //验证码可以显示的字符集合

        public static readonly string Vchar = "0123456789abcdefghijklmnpqrstuvwxyzABCDEFGHIJKLMNPPQRSTUVWXYZ";

        

        /**
    * 使用系统默认字符源生成验证码
    * 
    * @param verifySize 验证码长度
    * @return
    */
        public static string generateVerifyCode(int verifySize)
        {
            return generateVerifyCode(verifySize, Vchar);
        }

        /**
    * 使用指定源生成验证码
    * 
    * @param verifySize 验证码长度
    * @param sources 验证码字符源
    * @return
    */
        public static string generateVerifyCode(int verifySize, string sources)
        {
            if (sources == null || sources.Length == 0)
            {
                sources = Vchar;
            }
            int codesLen = sources.Length;
            Random rand = new Random((int)DateTimeExtensions.CurrentTimeMillis());
            StringBuilder verifyCode = new StringBuilder(verifySize);
            for (int i = 0; i < verifySize; i++)
            {
                verifyCode.Append(sources[rand.Next(codesLen - 1)]);
            }
            return verifyCode.ToString();
        }

        /// <summary>

        /// 该方法是将生成的随机数写入图像文件

        /// </summary>

        /// <param name="code">code是一个随机数</param>

        /// <param name="numbers">生成位数（默认4位）</param>

        public  static MemoryStream outputImage(int w,int h,out string code, int numbers = 4)
        {
            code = generateVerifyCode(numbers);
            Bitmap Img = null;
            Graphics g = null;
            MemoryStream ms = null;
            Random random = new Random();
            //验证码颜色集合
            Color[] c = { Color.Black, Color.Red, Color.DarkBlue, Color.Green, Color.Orange, Color.Brown, Color.DarkCyan, Color.Purple };
            //验证码字体集合
            string[] fonts = { "Verdana", "Microsoft Sans Serif", "Comic Sans MS", "Arial", "宋体" };
            //定义图像的大小，生成图像的实例
            Img = new Bitmap( w, h);
            g = Graphics.FromImage(Img);//从Img对象生成新的Graphics对象
            g.Clear(Color.White);//背景设为白色
            //在随机位置画背景点
            for (int i = 0; i < 100; i++)
            {
                int x = random.Next(Img.Width);
                int y = random.Next(Img.Height);
                g.DrawRectangle(new Pen(Color.LightGray, 0), x, y, 1, 1);
            }
            //验证码绘制在g中
            for (int i = 0; i < code.Length; i++)
            {
                int cindex = random.Next(7);//随机颜色索引值
                int findex = random.Next(5);//随机字体索引值
                Font f = new Font(fonts[findex], 15, FontStyle.Bold);//字体
                Brush b = new SolidBrush(c[cindex]);//颜色
                int ii = 4;
                if ((i + 1) % 2 == 0)//控制验证码不在同一高度
                {
                    ii = 2;
                }
                g.DrawString(code.Substring(i, 1), f, b, 3 + (i * 12), ii);//绘制一个验证字符
            }
            ms = new MemoryStream();//生成内存流对象
            Img.Save(ms, ImageFormat.Jpeg);//将此图像以Png图像文件的格式保存到流中
            ms.Seek(0,SeekOrigin.Begin);
            //回收资源
            g.Dispose();
            Img.Dispose();
            return ms;
        }

        private static string RndNum(int VcodeNum)
        {            
            string[] VcArray = Vchar.Split(new Char[] { ',' });//拆分成数组
            string code = "";//产生的随机数
            int temp = -1;//记录上次随机数值，尽量避避免生产几个一样的随机数
            Random rand = new Random();
            //采用一个简单的算法以保证生成随机数的不同
            for (int i = 1; i < VcodeNum + 1; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(i * temp * unchecked((int)DateTime.Now.Ticks));//初始化随机类
                }
                int t = rand.Next(61);//获取随机数
                if (temp != -1 && temp == t)
                {
                    return RndNum(VcodeNum);//如果获取的随机数重复，则递归调用
                }
                temp = t;//把本次产生的随机数记录起来
                code += VcArray[t];//随机数的位数加一
            }
            return code.ToLower();
        }
    }

}

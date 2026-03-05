using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace SPApi.Controls.UI
{
    internal class SGraphics
    {
        public static GraphicsPath GetRoundPath(Rectangle rt, int depth)
        {
            GraphicsPath path = new GraphicsPath();
            int tmpDep = depth < 1 ? 1 : depth;

            path.AddArc(rt.X, rt.Y, tmpDep, tmpDep, 180, 90);
            path.AddArc(rt.X + rt.Width - tmpDep, rt.Y, tmpDep, tmpDep, 270, 90);
            path.AddArc(rt.X + rt.Width - tmpDep, rt.Y + rt.Height - tmpDep, tmpDep, tmpDep, 0, 90);
            path.AddArc(rt.X, rt.Y + rt.Height - tmpDep, tmpDep, tmpDep, 90, 90);
            path.AddLine(rt.X, rt.Y + rt.Height - tmpDep, rt.X, rt.Y + tmpDep / 2);

            return path;
        }

        public static void DrawAquaPill(Graphics g, RectangleF drawRectF, Color drawColor, Orientation orientation)

        {
            Color color1;
            Color color2;
            Color color3;
            Color color4;
            Color color5;
            LinearGradientBrush gradientBrush;
            ColorBlend colorBlend = new ColorBlend();

            color1 = OpacityMix(Color.White, SoftLightMix(drawColor, Color.Black, 100), 40);
            color2 = OpacityMix(Color.White, SoftLightMix(drawColor, CreateColorFromRGB(64, 64, 64), 100), 20);
            color3 = SoftLightMix(drawColor, CreateColorFromRGB(128, 128, 128), 100);
            color4 = SoftLightMix(drawColor, CreateColorFromRGB(192, 192, 192), 100);
            color5 = OverlayMix(SoftLightMix(drawColor, Color.White, 100), Color.White, 75);

            //			
            colorBlend.Colors = new Color[] { color1, color2, color3, color4, color5 };
            colorBlend.Positions = new float[] { 0, 0.25f, 0.5f, 0.75f, 1 };
            if (orientation == Orientation.Horizontal)
                gradientBrush = new LinearGradientBrush(new Point((int)drawRectF.Left, (int)drawRectF.Top - 1), new Point((int)drawRectF.Left, (int)drawRectF.Top + (int)drawRectF.Height + 1), color1, color5);
            else
                gradientBrush = new LinearGradientBrush(new Point((int)drawRectF.Left - 1, (int)drawRectF.Top), new Point((int)drawRectF.Left + (int)drawRectF.Width + 1, (int)drawRectF.Top), color1, color5);
            gradientBrush.InterpolationColors = colorBlend;
            FillPill(gradientBrush, drawRectF, g);


            color2 = Color.White;
            colorBlend.Colors = new Color[] { color2, color3, color4, color5 };
            colorBlend.Positions = new float[] { 0, 0.5f, 0.75f, 1 };
            if (orientation == Orientation.Horizontal)
                gradientBrush = new LinearGradientBrush(new Point((int)drawRectF.Left + 1, (int)drawRectF.Top), new Point((int)drawRectF.Left + 1, (int)drawRectF.Top + (int)drawRectF.Height - 1), color2, color5);
            else
                gradientBrush = new LinearGradientBrush(new Point((int)drawRectF.Left, (int)drawRectF.Top + 1), new Point((int)drawRectF.Left + (int)drawRectF.Width - 1, (int)drawRectF.Top + 1), color2, color5);

            gradientBrush.InterpolationColors = colorBlend;
            FillPill(gradientBrush, RectangleF.Inflate(drawRectF, -3, -3), g);

            gradientBrush.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="drawRectF"></param>
        /// <param name="drawColor"></param>
        /// <param name="orientation"></param>
        public static void DrawAquaPillSingleLayer(Graphics g, RectangleF drawRectF, Color drawColor, Orientation orientation)
        {
            Color color1;
            Color color2;
            Color color3;
            Color color4;
            LinearGradientBrush gradientBrush;
            ColorBlend colorBlend = new ColorBlend();

            color1 = drawColor;
            color2 = ControlPaint.Light(color1);
            color3 = ControlPaint.Light(color2);
            color4 = ControlPaint.Light(color3);

            colorBlend.Colors = new Color[] { color1, color2, color3, color4 };
            colorBlend.Positions = new float[] { 0, 0.25f, 0.65f, 1 };

            if (orientation == Orientation.Horizontal)
                gradientBrush = new LinearGradientBrush(new Point((int)drawRectF.Left, (int)drawRectF.Top), new Point((int)drawRectF.Left, (int)drawRectF.Top + (int)drawRectF.Height), color1, color4);
            else
                gradientBrush = new LinearGradientBrush(new Point((int)drawRectF.Left, (int)drawRectF.Top), new Point((int)drawRectF.Left + (int)drawRectF.Width, (int)drawRectF.Top), color1, color4);
            gradientBrush.InterpolationColors = colorBlend;

            FillPill(gradientBrush, drawRectF, g);

            gradientBrush.Dispose();

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="b"></param>
        /// <param name="rect"></param>
        /// <param name="g"></param>
        public static void FillPill(Brush b, RectangleF rect, Graphics g)
        {
            if (rect.Width > rect.Height)
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.FillEllipse(b, new RectangleF(rect.Left, rect.Top, rect.Height, rect.Height));
                g.FillEllipse(b, new RectangleF(rect.Left + rect.Width - rect.Height, rect.Top, rect.Height, rect.Height));

                float w = rect.Width - rect.Height;
                float l = rect.Left + ((rect.Height) / 2);
                g.FillRectangle(b, new RectangleF(l, rect.Top, w, rect.Height));
                g.SmoothingMode = SmoothingMode.Default;
            }
            else if (rect.Width < rect.Height)
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.FillEllipse(b, new RectangleF(rect.Left, rect.Top, rect.Width, rect.Width));
                g.FillEllipse(b, new RectangleF(rect.Left, rect.Top + rect.Height - rect.Width, rect.Width, rect.Width));

                float t = rect.Top + (rect.Width / 2);
                float h = rect.Height - rect.Width;
                g.FillRectangle(b, new RectangleF(rect.Left, t, rect.Width, h));
                g.SmoothingMode = SmoothingMode.Default;
            }
            else if (rect.Width == rect.Height)
            {
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.FillEllipse(b, rect);
                g.SmoothingMode = SmoothingMode.Default;
            }
        }
        
        public static Color CreateColorFromRGB(int red, int green, int blue)
        {
            //Corect Red element
            int r = red;
            if (r > 255)
            {
                r = 255;
            }
            if (r < 0)
            {
                r = 0;
            }
            //Corect Green element
            int g = green;
            if (g > 255)
            {
                g = 255;
            }
            if (g < 0)
            {
                g = 0;
            }
            //Correct Blue Element
            int b = blue;
            if (b > 255)
            {
                b = 255;
            }
            if (b < 0)
            {
                b = 0;
            }
            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="blendColor"></param>
        /// <param name="baseColor"></param>
        /// <param name="opacity"></param>
        /// <returns></returns>
        public static Color OpacityMix(Color blendColor, Color baseColor, int opacity)
        {
            int r1;
            int g1;
            int b1;
            int r2;
            int g2;
            int b2;
            int r3;
            int g3;
            int b3;
            r1 = blendColor.R;
            g1 = blendColor.G;
            b1 = blendColor.B;
            r2 = baseColor.R;
            g2 = baseColor.G;
            b2 = baseColor.B;
            r3 = (int)(((r1 * ((float)opacity / 100)) + (r2 * (1 - ((float)opacity / 100)))));
            g3 = (int)(((g1 * ((float)opacity / 100)) + (g2 * (1 - ((float)opacity / 100)))));
            b3 = (int)(((b1 * ((float)opacity / 100)) + (b2 * (1 - ((float)opacity / 100)))));
            return CreateColorFromRGB(r3, g3, b3);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseColor"></param>
        /// <param name="blendColor"></param>
        /// <param name="opacity"></param>
        /// <returns></returns>
        public static Color SoftLightMix(Color baseColor, Color blendColor, int opacity)
        {
            int r1;
            int g1;
            int b1;
            int r2;
            int g2;
            int b2;
            int r3;
            int g3;
            int b3;
            r1 = baseColor.R;
            g1 = baseColor.G;
            b1 = baseColor.B;
            r2 = blendColor.R;
            g2 = blendColor.G;
            b2 = blendColor.B;
            r3 = SoftLightMath(r1, r2);
            g3 = SoftLightMath(g1, g2);
            b3 = SoftLightMath(b1, b2);
            return OpacityMix(CreateColorFromRGB(r3, g3, b3), baseColor, opacity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseColor"></param>
        /// <param name="blendColor"></param>
        /// <param name="opacity"></param>
        /// <returns></returns>
        public static Color OverlayMix(Color baseColor, Color blendColor, int opacity)
        {
            int r1;
            int g1;
            int b1;
            int r2;
            int g2;
            int b2;
            int r3;
            int g3;
            int b3;
            r1 = baseColor.R;
            g1 = baseColor.G;
            b1 = baseColor.B;
            r2 = blendColor.R;
            g2 = blendColor.G;
            b2 = blendColor.B;
            r3 = OverlayMath(baseColor.R, blendColor.R);
            g3 = OverlayMath(baseColor.G, blendColor.G);
            b3 = OverlayMath(baseColor.B, blendColor.B);
            return OpacityMix(CreateColorFromRGB(r3, g3, b3), baseColor, opacity);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="ibase"></param>
        /// <param name="blend"></param>
        /// <returns></returns>
        private static int SoftLightMath(int ibase, int blend)
        {
            float dbase;
            float dblend;
            dbase = (float)ibase / 255;
            dblend = (float)blend / 255;
            if (dblend < 0.5)
            {
                return (int)(((2 * dbase * dblend) + (Math.Pow(dbase, 2)) * (1 - (2 * dblend))) * 255);
            }
            else
            {
                return (int)(((Math.Sqrt(dbase) * (2 * dblend - 1)) + ((2 * dbase) * (1 - dblend))) * 255);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ibase"></param>
        /// <param name="blend"></param>
        /// <returns></returns>
        public static int OverlayMath(int ibase, int blend)
        {
            double dbase;
            double dblend;
            dbase = (double)ibase / 255;
            dblend = (double)blend / 255;
            if (dbase < 0.5)
            {
                return (int)((2 * dbase * dblend) * 255);
            }
            else
            {
                return (int)((1 - (2 * (1 - dbase) * (1 - dblend))) * 255);
            }
        }
    }

    public enum CustomBorderStyle
    {
        None,
        FixedSingle,
        Fixed3D,
        Dashed,
        Dotted,
        Inset,
        Outset,
        Solid,
        Adjust,
        Bump,
        Etched,
        Flat,
        Raised,
        RaisedInner,
        RaisedOuter,
        Sunken,
        SunkenInner,
        SunkenOuter
    }
}

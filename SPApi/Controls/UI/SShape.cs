using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SPApi.Controls.UI
{
    public partial class SShape : Control
    {
        #region Border Properties
        
        //Border Size
        private int _borderWidth = 1;
        [Browsable(true), Category("Extended-Shape")]
        public int BorderWidth
        {
            get { return _borderWidth; }
            set
            {
                _borderWidth = value;
                Invalidate();
            }
        }

        //Border Color
        private Color _borderColor = Color.Black;
        [Browsable(true), Category("Extended-Shape")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                Invalidate();
            }
        }

        #endregion

        #region Shape Properties

        public enum ShapeType
        {
            //Circle,
            Ellpse,
            //Square,
            Rectangle,
            Triangle
            //Pentagon,
            //Hexagon,
            //Octagon
        }

        public enum ShapeDirection
        {
            Top,
            Left,
            Right,
            Bottom
        }

        //Shape Type
        private ShapeType _shapetype = ShapeType.Rectangle;
        [Browsable(true), Category("Extended-Shape")]
        public ShapeType Shape_Type
        {
            get { return _shapetype; }
            set
            {
                _shapetype = value;
                Invalidate();
            }
        }

        //Shape Direction
        private ShapeDirection _shapedirection = ShapeDirection.Right;
        [Browsable(true), Category("Extended-Shape")]
        public ShapeDirection Shape_Direction
        {
            get { return _shapedirection; }
            set
            {
                _shapedirection = value;
                Invalidate();
            }
        }

        //Shape Fill Color
        private Color _fillcolor = Color.Red;
        [Browsable(true), Category("Extended-Shape")]
        public Color FillColor
        {
            get { return _fillcolor; }
            set
            {
                _fillcolor = value;
                Invalidate();
            }
        }

        #endregion

        public SShape()
        {
            //DoubleBuffering
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);  
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);

            Rectangle rt = Rectangle.Empty;
            Point[] pts = null;

            //Control Size가 1x1픽셀 보다 크면
            if (Width > 1 && Height > 1)
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

                int tol = (_borderWidth * 2) - 1;
                Pen p = new Pen(_borderColor, _borderWidth);
                SolidBrush b = new SolidBrush(_fillcolor);
                
                switch(Shape_Type)
                {
                    case ShapeType.Ellpse:
                        
                        rt = new Rectangle(_borderWidth - 1, _borderWidth - 1, Width - tol, Height - tol);

                        e.Graphics.FillEllipse(b, rt);
                        e.Graphics.DrawEllipse(p, rt);
                        break;

                    case ShapeType.Triangle:
                        switch (_shapedirection)
                        {
                            case ShapeDirection.Top:
                                pts = new[]
                                {
                                    new Point(_borderWidth - 1, Height - tol),
                                    new Point((Width / 2) , _borderWidth - 1),
                                    new Point(Width - tol ,Height - tol)
                                };
                                break;

                            case ShapeDirection.Bottom:
                                pts = new[]
                                {
                                    new Point(_borderWidth - 1, _borderWidth - 1),
                                    new Point((Width / 2), Height - tol),
                                    new Point(Width - tol , _borderWidth - 1)
                                };
                                break;

                            case ShapeDirection.Left:
                                pts = new[]
                                {
                                    new Point(_borderWidth - 1, _borderWidth - 1),
                                    new Point(Width - tol, (Height / 2)),
                                    new Point(_borderWidth - 1 ,Height - tol)
                                };
                                break;

                            case ShapeDirection.Right:
                            default:
                                pts = new[]
                                {
                                    new Point(Width - tol, _borderWidth - 1),
                                    new Point(_borderWidth - 1, (Height / 2)),
                                    new Point(Width - tol ,Height - tol)
                                };
                                break;
                        }
                        
                        e.Graphics.FillPolygon(b, pts);
                        e.Graphics.DrawPolygon(p, pts);
                        break;

                    default:
                        rt = new Rectangle(_borderWidth - 1, _borderWidth - 1, Width - tol, Height - tol);

                        e.Graphics.FillRectangle(b, rt);
                        e.Graphics.DrawRectangle(p, rt);
                        break;
                }

                b.Dispose();
                p.Dispose();
            }
        }
    }
}

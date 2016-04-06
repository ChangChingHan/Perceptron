using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;

namespace home_work
{
	class Drawing
	{
		Point _original_point;
		List<DRAW_DATA> _listDrawData = new List<DRAW_DATA>();
		int _point_map = 50;
		int _scale = 3;

		public void set_parameter(int point_map, int scale)
		{
			_point_map = point_map;
			_scale = scale;
		}

		public void init()
		{
			_point_map = 50;
			_scale = 3;
			_listDrawData.Clear();
		}

		public void prepare_drawdata(ref List<READ_DATA> listReadData, ref Training train)
		{
			for (int i = 0; i < listReadData.Count; i++)
			{
				DRAW_DATA t = new DRAW_DATA();
				double[] x = { listReadData[i].x[0], listReadData[i].x[1] };
				int yd = (int)listReadData[i].d;
				t.point = transdata_point(x);
				t.class_lable = train.get_class(yd);
				_listDrawData.Add(t);
			}
		}

		Point transdata_point(double[] x)
		{
			Point p = new Point();

			for (int i = 0; i < 2; i++)
			{
				int a = (int)(x[i] * _point_map);

				switch (i)
				{
					case 0:
						{
							if (x[i] > 0)
							{
								p.X = _original_point.X + a;
							}
							else
							{
								p.X = _original_point.X - (a * -1);
							}
						}
						break;
					case 1:
						{
							if (x[i] > 0)
							{
								p.Y = _original_point.Y - a;
							}
							else
							{
								p.Y = _original_point.Y + (a * -1);
							}
						}
						break;
				}

			}
			return p;
		}

		public void DrawFrame(ref Graphics g, ref Panel p)
		{
			Pen pen = new Pen(Color.Black, 4);
			Point[] points = new Point[4];
			points[0] = new Point(0, p.Height / 2);
			points[1] = new Point(p.Width, p.Height / 2);
			points[2] = new Point(p.Width / 2, 0);
			points[3] = new Point(p.Width / 2, p.Height);

			g.DrawLine(pen, points[0], points[1]);
			g.DrawLine(pen, points[2], points[3]);

			_original_point.X = p.Width / 2;
			_original_point.Y = p.Height / 2;
		}

		public void DrawDot(ref Graphics g)
		{
			Brush aBrushRed = (Brush)Brushes.Red;
			Brush aBrushGreen = (Brush)Brushes.Green;

			foreach (var item in _listDrawData)
			{

				if (item.class_lable == CLASS.CLASS_1)
				{
					g.FillRectangle(aBrushRed, item.point.X, item.point.Y, 5, 5);
				}
				else
				{
					g.FillRectangle(aBrushGreen, item.point.X, item.point.Y, 5, 5);
				}
			}
		}

		public void DrawClassLine(ref Graphics g, ref Training t)
		{
			Point old = new Point();
			double[] array = new double[2];
			int scale = _scale;
			int count = _scale * 2 + 1;
			Point[] ps = new Point[count];

			for (int i = 0; i < count; i++)
			{
				if ((t.get_w1() > 0 && t.get_w2() > 0) || (t.get_w1() < 0 && t.get_w2() < 0))
				{
					array[0] = (t.get_theta() - (t.get_w2() * scale)) / t.get_w1();
					array[1] = scale--;
				}
				else if (t.get_w1() == 0 && t.get_w2() != 0)
				{
					array[0] = scale--;
					array[1] = t.get_theta() / t.get_w2();
				}
				else if (t.get_w1() != 0 && t.get_w2() == 0)
				{
					array[0] = t.get_theta() / t.get_w1();
					array[1] = scale--;
				}
				else
				{
					array[0] = scale--;
					array[1] = (t.get_theta() - (t.get_w1() * scale)) / t.get_w2();
				}

				ps[i] = transdata_point(array);
			}

			old = ps[0];
			foreach (var p in ps)
			{
				Pen pen = new Pen(Color.Black, 4);
				g.DrawLine(pen, old, p);
				old = p;
			}
		}
	}
}

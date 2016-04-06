using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace home_work
{
	class READ_DATA
	{
		public double[] x = new double[2];
		public int d;
	}

	class Training
	{
		double _theta_w = -1;
		double _theta_x = -1;
		double _alpha = 0.5;
		double _w1 = 0;
		double _w2 = 1;
		int _class_num = -1;
		int _loop_count = 50;

		public double get_theta()
		{
			return _theta_w;
		}
		public double get_w1()
		{
			return _w1;
		}
		public double get_w2()
		{
			return _w2;
		}

		public void set_loop_count(int loop_count)
		{
			_loop_count = loop_count;
		}

		public void init()
		{
			_theta_w = -1;
			_theta_x = -1;
			_alpha = 0.5;
			_w1 = 0;
			_w2 = 1;
			_class_num = -1;
			_loop_count = 50;
		}

		public bool training(ref List<READ_DATA> listReadData,ref RichTextBox richTextBox)
		{
			int loop = 0;
			string str = "";
			bool result = false;
			for (loop = 0; loop < _loop_count; loop++)
			{
				int eSum = 0;
				str = String.Format("===== loop : {0} =====\n", loop+1);
				richTextBox.AppendText(str);

				for (int i = 0; i < listReadData.Count; i++)
				{
					double[] x = { listReadData[i].x[0], listReadData[i].x[1] };
					int yd = (int)listReadData[i].d;
					CLASS c = class_data(x, yd);

					if (c != get_class(yd))
					{
						training_w(c, x);
						eSum++;
					}

					str = String.Format("x=({0:F3},{1:F3}) w=({2:F4},{3:F4}) theta={4:F4} error={5}\n",
										   x[0], x[1], _w1, _w2, _theta_w, eSum);
					richTextBox.AppendText(str);
				}
				if (eSum == 0.0)
				{
					result = true;
					break;
				}
			}

			str = String.Format("===== loop : {0} =====\n", loop+1);
			richTextBox.AppendText(str);
			return result;
		}

		void training_w(CLASS c, double[] x)
		{
			if (c == CLASS.CLASS_1)
			{
				_theta_w = _theta_w - _alpha * _theta_x;
				_w1 = _w1 - _alpha * x[0];
				_w2 = _w2 - _alpha * x[1];
			}
			else
			{
				_theta_w = _theta_w + _alpha * _theta_x;
				_w1 = _w1 + _alpha * x[0];
				_w2 = _w2 + _alpha * x[1];
			}
		}

		CLASS class_data(double[] x, int d)
		{
			if (_class_num == -1)
			{
				_class_num = d;
			}

			double result = _theta_w * _theta_x + _w1 * x[0] + _w2 * x[1];
			if (result >= 0)
				return CLASS.CLASS_1;
			else
				return CLASS.CLASS_2;
		}

		public CLASS get_class(int d)
		{
			if (d == _class_num)
			{
				return CLASS.CLASS_1;
			}
			else
			{
				return CLASS.CLASS_2;
			}
		}
	}
}

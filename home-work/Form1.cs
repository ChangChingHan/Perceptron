using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace home_work
{
	enum CLASS
	{
		CLASS_1,
		CLASS_2
	}

	struct DRAW_DATA
	{
		public Point point;
		public CLASS class_lable;
	}

	public partial class Form1 : Form
	{
		Panel _panel = new Panel();
		bool _finish = false;
		bool _success = false;
		List<READ_DATA> _listReadData = new List<READ_DATA>();
		Drawing _drawing = new Drawing();
		Training _training = new Training();
		string _folder = @"D:\類神經\資料集";
		
		public Form1()
		{
			InitializeComponent();
		}

		void learn()
		{
			textBox1.Text = "學習中... ";
			textBox1.Update();
			if (_training.training(ref _listReadData, ref richTextBox1))
			{
				textBox1.Text = "學習成功 !";
				_success = true;
			}
			else
				textBox1.Text = "學習失敗 !";

			_finish = true;

			_drawing.prepare_drawdata(ref _listReadData, ref _training);
		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{
			_panel = sender as Panel;
			Graphics g = e.Graphics;

			_drawing.DrawFrame(ref g, ref _panel);

			if (_finish)
			{
				_drawing.DrawDot(ref g);
				if (_success)
					_drawing.DrawClassLine(ref g,ref _training);
			}
		}

		private void read_file(string file, int map)
		{
			List<string> list = new List<string>();
			string[] lines = System.IO.File.ReadAllLines(file);
			foreach (string line in lines)
			{
				list.Clear();
				READ_DATA r = new READ_DATA();
				string[] sublines = line.Split('	');

				if (sublines.Length <= 1)
				{
					sublines = line.Split(' ');
				}

				foreach (var item in sublines)
				{
					if (item.Length > 0)
					{
						list.Add(item);
					}
				}

				r.x[0] = Double.Parse(list[0]) * map;
				r.x[1] = Double.Parse(list[1]) * map;
				r.d = Int32.Parse(list[2]);
				_listReadData.Add(r);
			}
		}

		private void init()
		{
			_drawing.init();
			_training.init();
			_finish = false;
			_success = false;
			_listReadData.Clear();
			richTextBox1.Clear();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			init();
			read_file(_folder+"\\感知機1.txt",1);
			learn();
			_panel.Invalidate();
		}

		private void button2_Click(object sender, EventArgs e)
		{
			init();
			read_file(_folder + "\\感知機2.txt", 1);
			learn();
			_panel.Invalidate();
		}

		private void button3_Click(object sender, EventArgs e)
		{
			init();
			read_file(_folder + "\\感知機3.txt", 1);
			learn();
			_panel.Invalidate();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			init();
			_training.set_loop_count(10);
			read_file(_folder + "\\2Ccircle1.txt", 1);
			learn();
			_panel.Invalidate();
		}

		private void button5_Click(object sender, EventArgs e)
		{
			init();
			_training.set_loop_count(10);
			read_file(_folder + "\\2Circle1.txt", 1);
			learn();
			_panel.Invalidate();
		}

		private void button6_Click(object sender, EventArgs e)
		{
			init();
			_training.set_loop_count(10);
			read_file(_folder + "\\2Circle2.txt", 1);
			learn();
			_panel.Invalidate();
		}

		private void button7_Click(object sender, EventArgs e)
		{
			init();
			read_file(_folder + "\\2CloseS.txt", 1);
			learn();
			_panel.Invalidate();
		}

		private void button8_Click(object sender, EventArgs e)
		{
			init();
			_drawing.set_parameter(10, 50);
			read_file(_folder + "\\2CloseS2.txt", 10);
			learn();
			_panel.Invalidate();
		}

		private void button9_Click(object sender, EventArgs e)
		{
			init();
			_training.set_loop_count(10);
			read_file(_folder + "\\2CloseS3.txt", 1);
			learn();
			_panel.Invalidate();
		}

		private void button10_Click(object sender, EventArgs e)
		{
			init();
			_drawing.set_parameter(4, 40);
			read_file(_folder + "\\2cring.txt", 2);
			learn();
			_panel.Invalidate();
		}

		private void button11_Click(object sender, EventArgs e)
		{
			init();
			_drawing.set_parameter(25, 10);
			read_file(_folder + "\\2CS.txt", 1);
			learn();
			_panel.Invalidate();
		}

		private void button12_Click(object sender, EventArgs e)
		{
			init();
			_drawing.set_parameter(7, 15);
			read_file(_folder + "\\2Hcircle1.txt", 7);
			learn();
			_panel.Invalidate();
		}

		private void button13_Click(object sender, EventArgs e)
		{
			init();
			_drawing.set_parameter(30, 5);
			read_file(_folder + "\\2ring.txt", 1);
			learn();
			_panel.Invalidate();
		}
	}
}

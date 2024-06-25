using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LR1_AKG
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Метод, устанавливающий пиксель на форме с заданными цветом и прозрачностью
        private static void PutPixel(Graphics g, Color col, int x, int y, int alpha)
        {
            g.FillRectangle(new SolidBrush(Color.FromArgb(alpha, col)), x, y, 1, 1);
        }
        private static void Swap(ref int first, ref int second)
        {
            int temp;
            temp = first;
            first = second;
            second = temp;
        }


        //Статический метод, реализующий отрисовку линии по алгоритму Брезенхема
        static public void Bresenham4Line(Graphics g, Color clr, int x0, int y0, int x1, int y1)
        {
            var steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0); // Проверяем рост отрезка по оси икс и по оси игрек
                                                               // Отражаем линию по диагонали, если угол наклона слишком большой
            if (steep)
            {
                Swap(ref x0, ref y0); // Перетасовка координат вынесена в отдельную функцию для красоты
                Swap(ref x1, ref y1);
            }
            // Если линия растёт не слева направо, то меняем начало и конец отрезка местами
            if (x0 > x1)
            {
                Swap(ref x0, ref x1);
                Swap(ref y0, ref y1);
            }
            int dx = x1 - x0;
            int dy = Math.Abs(y1 - y0);
            int error = dx / 2; // Здесь используется оптимизация с умножением на dx, чтобы избавиться от лишних дробей
            int ystep = (y0 < y1) ? 1 : -1; // Выбираем направление роста координаты y
            int y = y0;
            for (int x = x0; x <= x1; x++)
            {
                PutPixel(g, clr, steep ? y : x, steep ? x : y, 255); // Не забываем вернуть координаты на место
                error -= dy;
                if (error < 0)
                {
                    y += ystep;
                    error += dx;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Graphics g = pictureBox1.CreateGraphics();
            Random Coord = new Random();
            Random Colors = new Random();


            for (int i = 0; i < 16; i++)
            {               
                Bresenham4Line(g, Color.FromArgb(Colors.Next(0, 255), Colors.Next(0, 255), Colors.Next(0, 255), Colors.Next(0, 255)),
                               Coord.Next(0, 700), Coord.Next(0, 500), Coord.Next(0, 700), Coord.Next(0, 500));
            }
        }
    }
}

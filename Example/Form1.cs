using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Example
{
    public partial class Form1 : Form
    {
        static int row = 31;
        static int colomn = 61;
        int size = 10;
        int[,] Labirinth = new int[row, colomn];
        Random rnd = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        public void Drawing()
        {
            Bitmap bmp = new Bitmap(10*size+colomn*size, 20+row*size);
            Graphics pan = Graphics.FromImage(bmp);
            for (int i = 0; i < row; ++i)
                 for (int j = 0; j < colomn; ++j)
                 {
                     if (Labirinth[i, j] == 0)
                     {
                         pan.FillRectangle(Brushes.White, 10 * size + j * size, 20 + i * size, size, size);
                     }
                     else
                     {
                         pan.FillRectangle(Brushes.Blue,  10 * size + j * size, 20 + i * size, size, size);
                     }
                 }
            pan.Dispose();
            pictureBox1.Image = (Image)bmp;
            bmp=null;
        }
        public void Generate_default_walls()
        {
            for (int i = 0; i < row; ++i)
            {
                Labirinth[i, 0] = 1; Labirinth[i, colomn - 1] = 1;
            }
            for (int j = 0; j < colomn; ++j)
            {
                Labirinth[0, j] = 1; Labirinth[row - 1, j] = 1;
            }
            for (int i = 2; i < row - 1; i += 2)
                for (int j = 2; j < colomn - 1; j += 2)
                {
                    Labirinth[i, j] = 1;
                    Generate_dynami_walls(i, j);
                }
        }
        public void Generate_dynami_walls(int i, int j)
        {
            switch (rnd.Next(0, 4))
            {
                case 0:
                    if (Labirinth[i - 1, j] != 1)
                    {
                        Labirinth[i - 1, j] = 1;
                    }
                    else
                    {
                        Generate_dynami_walls(i, j);
                    }
                    break;
                case 1:
                    if (Labirinth[i + 1, j] != 1)
                    {
                        Labirinth[i + 1, j] = 1;
                    }
                    else
                    {
                        Generate_dynami_walls(i, j);
                    }
                    break;
                case 2:
                    if (Labirinth[i, j - 1] != 1)
                    {
                        Labirinth[i, j - 1] = 1;
                    }
                    else
                    {
                        Generate_dynami_walls(i, j);
                    }
                    break;
                case 3:
                    if (Labirinth[i, j + 1] != 1)
                    {
                        Labirinth[i, j + 1] = 1;
                    }
                    else
                    {
                        Generate_dynami_walls(i, j);
                    }
                    break;
            }
        }
        public void Fix_circles()
        {
            for (int i = 3; i < row; i += 2)
                for (int j = 3; j < colomn; j += 2)
                {
                    if (Labirinth[i + 1, j] == 1 && Labirinth[i - 1, j] == 1 && Labirinth[i, j + 1] == 1 && Labirinth[i, j - 1] == 1)
                    {
                        switch (rnd.Next(0, 4))
                        {
                            case 0:
                                Labirinth[i - 1, j] = 0;
                                break;
                            case 1:
                                Labirinth[i + 1, j] = 0;
                                break;
                            case 2:
                                Labirinth[i, j - 1] = 0;
                                break;
                            case 3:
                                Labirinth[i, j + 1] = 0;
                                break;
                        }
                    }
                }
        }
        public void Clearing()
        {
            for (int i = 0; i < row; ++i)
            {
                for (int j = 0; j < colomn; ++j)
                {
                    Labirinth[i, j] = 0;
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Clearing();
            Generate_default_walls();
            Fix_circles();
            Drawing();
        }
    }
}

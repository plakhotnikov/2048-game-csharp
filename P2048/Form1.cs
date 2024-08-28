using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace P2048
{
    public partial class Form1 : Form
    {
        private const int conSize = 4;
        private const int conWidth = 100;
        private Button[,] arrButts = new Button[conSize, conSize];
        private Dictionary<int, Color> dButtColors = new Dictionary<int, Color>();
        private int[,] arrNums = new int[conSize, conSize];
        Random rnd;
        //private Dictionary<int, int> dFree = new Dictionary<int,int>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Размер формы
            this.Width = conWidth * conSize + 16;
            this.Height = conWidth * conSize + 40;
            
            // Создание массива кнопок
            for (int intC1 = 0; intC1 < conSize; intC1++)
                for (int intC2 = 0; intC2 < conSize; intC2++)
                { 
                    arrButts[intC1, intC2] = new Button();
                    arrButts[intC1, intC2].Width = conWidth;
                    arrButts[intC1, intC2].Height = conWidth;
                    
                    this.Controls.Add(arrButts[intC1, intC2]);
                    arrButts[intC1, intC2].Left = intC2 * conWidth;
                    arrButts[intC1, intC2].Top = intC1 * conWidth;
                    arrButts[intC1, intC2].Font = new Font("Arial", 20, FontStyle.Bold);
                    arrButts[intC1, intC2].Enabled = false;
                    arrButts[intC1, intC2].Visible = true;
                }

            arrNums.Initialize();

            dButtColors.Add(0, Color.LightGray);
            dButtColors.Add(2, Color.NavajoWhite);
            dButtColors.Add(4, Color.PeachPuff);
            dButtColors.Add(8, Color.Tan);
            dButtColors.Add(16, Color.SandyBrown);
            dButtColors.Add(32, Color.Orange);
            dButtColors.Add(64, Color.DarkOrange);
            dButtColors.Add(128, Color.Salmon);
            dButtColors.Add(256, Color.RosyBrown);
            dButtColors.Add(512, Color.Tomato);
            dButtColors.Add(1024, Color.IndianRed);
            dButtColors.Add(2048, Color.Red);
            dButtColors.Add(4096, Color.Red);
            dButtColors.Add(8192, Color.Red);

            Display();
            rnd = new Random();
        }

        private void Display()
        {
            for (int intC1 = 0; intC1 < conSize; intC1++)
                for (int intC2 = 0; intC2 < conSize; intC2++)
                {
                    arrButts[intC1, intC2].BackColor = dButtColors[arrNums[intC1, intC2]];
                    if (arrNums[intC1, intC2] == 0)
                        arrButts[intC1, intC2].Text = "";
                    else
                        arrButts[intC1, intC2].Text = arrNums[intC1, intC2].ToString();
                }
        }
        private void AddRandomNumber()
        {
            int num = (rnd.Next(9) == 0 ? 4 : 2);
            int cnt = 0;

            for (int i = 0; i < arrNums.GetLength(0); ++i)
            {
                for (int j = 0; j < arrNums.GetLength(1); ++j)
                {
                    if (arrNums[i, j] == 0) ++cnt;
                }
            }
            if (cnt == 0) return;
            cnt = rnd.Next(cnt - 1);

            for (int i = 0; i < arrNums.GetLength(0); ++i)
            {
                for (int j = 0; j < arrNums.GetLength(1); ++j)
                {
                    if (arrNums[i, j] == 0)
                    {
                        if (cnt-- == 0)
                        {
                            arrNums[i, j] = num;
                            break;
                        }
                    }
                }
            }
        }
        private void EasyMove(ref int[] array) 
        {
            bool[] BlockSum = new bool[4];
            for (int i = 1; i < 4; ++i)
            {
                int j = i;
                while (j != 0)
                {
                    if (array[j - 1] == 0)
                    {
                        array[j - 1] = array[j];
                        array[j--] = 0;
                    }
                    else if (array[j - 1] == array[j] && !BlockSum[j - 1])
                    {
                        BlockSum[j - 1] = true;
                        array[j - 1] *= 2;
                        array[j--] = 0;
                        break;
                    }
                    else break;
                }
            }
        }
        private void MoveDown()
        {
            for (int j = 0; j < 4; ++j)
            {
                int[] array = new int[4];
                int cursor = 0;
                for (int i = 3; i >= 0; --i)
                {
                    array[cursor++] = arrNums[i, j];
                }
                EasyMove(ref array);
                cursor = 0;
                for (int i = 3; i >= 0; --i)
                {
                    arrNums[i, j] = array[cursor++];
                }
            }
            AddRandomNumber();
        }
        private void MoveUp()
        {
            for (int j = 0; j < 4; ++j)
            {
                int[] array = new int[4];
                int cursor = 0;
                for (int i = 0; i < 4; ++i)
                {
                    array[cursor++] = arrNums[i, j];
                }
                EasyMove(ref array);
                cursor = 0;
                for (int i = 0; i < 4; ++i)
                {
                    arrNums[i, j] = array[cursor++];
                }
            }
            AddRandomNumber();
        }
        private void MoveRight()
        {
            for (int i = 0; i < 4; ++i)
            {
                int[] array = new int[4];
                int cursor = 0;
                for (int j = 3; j >= 0; --j)
                {
                    array[cursor++] = arrNums[i, j];
                }
                EasyMove(ref array);
                cursor = 0;
                for (int j = 3; j >= 0; --j)
                {
                    arrNums[i, j] = array[cursor++];
                }
            }
            AddRandomNumber();
        }
        private void MoveLeft()
        {
            for (int i = 0; i < 4; ++i)
            {
                int[] array = new int[4];
                int cursor = 0;
                for (int j = 0; j < 4; ++j)
                {
                    array[cursor++] = arrNums[i, j];
                }
                EasyMove(ref array);
                cursor = 0;
                for (int j = 0; j < 4; ++j)
                {
                    arrNums[i, j] = array[cursor++];
                }
            }
            AddRandomNumber();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Down:
                    MoveDown();
                    Display();
                    break;

                case Keys.Up:
                    MoveUp();
                    Display();
                    break;

                case Keys.Right:
                    MoveRight();
                    Display();
                    break;

                case Keys.Left:
                    MoveLeft();
                    Display();
                    break;
            }
            
        }

       
    }
}

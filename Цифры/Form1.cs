using CsvHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Цифры.Numbers;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.LinkLabel;


namespace Цифры
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        Pen p = new Pen(Color.Black, 20);
        Bitmap bm = new Bitmap(320, 320);
        FolderBrowserDialog dialog = new FolderBrowserDialog();
        private bool isMouse = false;
        private Point startPt;
        private Point nullPt = new Point(int.MaxValue, 0);
        

        //Сохранение цифр в пиксели
        private void button1_Click(object sender, EventArgs e)
        {
            if ((dialog.SelectedPath == "")||(comboBox1.Text== "Выбери число"))
            {
                MessageBox.Show("Нажми на кнопку справа и выбери путь к файлу или выбери цифру");
                return;
            }

            List<string> newLines = new List<string>();
            string pathCsvFile = dialog.SelectedPath+"\\Numbers.csv";
            pictureBox2.Image = new Bitmap(new Bitmap(pictureBox1.Image), 32, 32);
            byte[] numb = ImageToByte(pictureBox2);
            string newStr = String.Join("", numb);
            NumberForSave number = new NumberForSave
            {
                Name = comboBox1.Text,
                Vector = newStr,
            };
            newLines.Add(string.Format("{0};{1}", number.Name, number.Vector));
            File.AppendAllLines(pathCsvFile, newLines);

        }
        //Конвертация изображения в пиксели
        public byte[] ImageToByte(PictureBox img)
        {

            Bitmap bmp = new Bitmap(img.Image);

            int[,] ALL = new int[bmp.Width - 1, bmp.Height - 1]; //Массив для цветов пикселей.
            byte[,] tmp = new byte[bmp.Width, bmp.Height];
            byte[] bytes = new byte[bmp.Width * bmp.Height];
            int z = 0;
            for (int i = 0; i < (bmp.Width - 1); i++)
            {
                for (int j = 0; j < (bmp.Height - 1); j++)
                {
                    ALL[i, j] = int.Parse(bmp.GetPixel(i, j).Name, System.Globalization.NumberStyles.HexNumber);
                    if ((ALL[i, j] == 0) || (ALL[i, j] == -1) || (ALL[i, j] == -33686019) || (ALL[i, j] == -16908803))
                        tmp[i, j] = 0;
                    else tmp[i, j] = 1;
                }
            }
            for (int i = 0; i < 32; i++)
            {
                for (int j = 0; j < 32; j++)
                {
                    bytes[z] = tmp[j, i];
                    textBox1.Text += bytes[z];
                    z++;
                }
                textBox1.Text += Environment.NewLine;
            }
            return bytes;
        }



        //Рисование
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            isMouse = true;
            startPt = e.Location;
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            isMouse = false;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            pictureBox1.Image = bm;

            if (startPt == nullPt) return;
            if (e.Button == MouseButtons.Left)
            {
                Graphics g = Graphics.FromImage(pictureBox1.Image);
                g.DrawLine(p, startPt, e.Location);
                g.Dispose();
                startPt = e.Location;
                pictureBox1.Invalidate();
            }

        }




        private void button3_Click(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(pictureBox1.Image);
            g.Clear(Color.White);
            g.Dispose();
            pictureBox1.Invalidate();
            textBox1.Text = "";
            textBox2.Text = "";

        }

        private void button2_Click(object sender, EventArgs e)
        {
            dialog.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Recognition rc = new Recognition();
            pictureBox2.Image = new Bitmap(new Bitmap(pictureBox1.Image), 32, 32);
            byte[] numb = ImageToByte(pictureBox2);
            string[,] answers =  rc.recognise(numb);
            for (int i = 0; i < answers.GetLength(0); i++) if (Convert.ToInt32(answers[i, 1]) > 0) textBox2.Text += answers[i, 0];

        }

        private void button5_Click(object sender, EventArgs e)
        {
            Vesa vs = new Vesa();
            vs.getVesa();
            bool isItRight = false;
            if (comboBox1.Text == "Выбери число")
            {
                MessageBox.Show("Выбери, какая это цифра");
                return;
            }

            for (int i = 0; i < textBox2.Text.Length; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if ((vs.numbers[j].Name == comboBox1.Text)&&(vs.numbers[j].Name == Convert.ToString(textBox2.Text[i])))
                    {
                        vs.changeVes(true, vs.numbers[j], textBox1.Text);
                        isItRight = true;
                        break;
                    }
                    if ((vs.numbers[j].Name == Convert.ToString(textBox2.Text[i])) && (vs.numbers[j].Name != comboBox1.Text))
                    {
                        vs.changeVes(false, vs.numbers[j], textBox1.Text);
                        break;
                    }

                }
            }
            if (!isItRight)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (vs.numbers[j].Name == comboBox1.Text)
                    {
                        vs.changeVes(true, vs.numbers[j], textBox1.Text);
                        break;
                    }
                }
            }

            vs.save(vs.numbers);
        }
    }

}
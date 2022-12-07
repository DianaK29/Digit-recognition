using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Цифры.Numbers
{
    internal class Recognition
    {
        //public Number[] numbers = new Number[10];
        public string[,] read()
        {
            string[] lines = File.ReadAllLines(@"C:\Users\Диана\Documents\Numbers.csv");
            string[,] numbs = new string[lines.Length, 2];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(';');
                numbs[i, 0] = temp[0];
                numbs[i, 1] = temp[1];
            }
            return numbs;
        }

        public string[,] recognise(byte[] numb)
        {
            Vesa vs = new Vesa();
            //vs.xyi();
            vs.getVesa(); 
            string[,] pixels = read();
            string[,] answers = new string[10, 2];
            string pix = String.Join("", numb);

            for (int j = 0; j < 10; j++)
            {
                double sum = vs.numbers[j].Check(pix, vs.numbers[j]);
                answers[j, 0] = vs.numbers[j].Name;
                answers[j, 1] = Convert.ToString((int)sum);
            }
            return answers;
        }
        
        public void learn()
        {
            Vesa vs = new Vesa();
            vs.getVesa();
            string[,] pixels = read();
            for (int i = 0; i < pixels.Length / 2 - 1; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (vs.numbers[j].CheckForLearn(pixels[i, 1], vs.numbers[j]) && (pixels[i, 0] != vs.numbers[j].Name))
                    {
                        vs.changeVes(false, vs.numbers[j], pixels[i, 1]);
                        //else vs.changeVes(false, vs.numbers[j]);
                    }
                    if (!vs.numbers[j].CheckForLearn(pixels[i, 1], vs.numbers[j]) && (pixels[i, 0] == vs.numbers[j].Name))
                        vs.changeVes(true, vs.numbers[j], pixels[i, 1]);
                }
            }
            vs.save(vs.numbers);
        }

    }
}

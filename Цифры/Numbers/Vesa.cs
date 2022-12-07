using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Цифры.Numbers
{
    public class Vesa
    {
        string pathVesa = @"C:\Users\Диана\Documents\Vesa.txt";
        public Number[] numbers = new Number[10];

        public void save(Number[] numbers)
        {
            File.Delete(pathVesa);
            StreamWriter sr = new StreamWriter(pathVesa, true);
            int n = 1024;
            for (int i = 0; i < 10; i++)
            {
                sr.Write(numbers[i].Name + ":");
                for (int j = 0; j < n; j++)
                {
                    sr.Write(numbers[i].Vesa[j] + "^");
                }
                sr.Write(";" + Environment.NewLine);
            }
            sr.Close();
            
        } 

        public void newnewVesa()
        {
            Random rnd = new Random();
            string[] names = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            StreamWriter sr = new StreamWriter(pathVesa, true);
            int n = 1024;
            for (int i = 0; i < 10; i++) {
                double[] randomValues = Enumerable.Range(1, 1024).Select(x => (rnd.NextDouble() * 2 - 1)).ToArray();//генерация 1024 случайных чисел
                sr.Write(names[i] + ":");
                double[] A = new double[n];
                for (int j = 0; j < n; j++)
                {
                    A[j] = Math.Round(randomValues[j], 1);
                    sr.Write(A[j] + "^");
                }
                sr.Write(";" + Environment.NewLine);
            }
            sr.Close();
        }
        public string [,] read()
        {
            string[] lines = File.ReadAllLines(@"C:\Users\Диана\Documents\Vesa.txt");
            string [,] numbs = new string [10, 2];
            for (int i = 0; i < 10; i++)
            {
                string[] temp = lines[i].Split(':');
                numbs[i,0]=temp[0];
                numbs[i,1]=temp[1];
            }
            return numbs;
        }

        public void getVesa()
        {
            Recognition rc = new Recognition();
            string[,] numbs = read();
            for (int i = 0; i < 10; i++)
            {
                double[] vesa = new double[1024];
                for (int j = 0; j < 1024; j++)
                {
                    string[] temp = numbs[i,1].Split('^');
                    vesa[j] = double.Parse(temp[j]);
                }
                numbers[i] = new Number(numbs[i,0],vesa);
            }
        }

        public void changeVes(bool n, Number numb, string pixels)
        {
            var newPixels = string.Join("", Regex.Split(pixels, @"(?:\r\n|\n|\r)"));
            double s = n ? 0.2 : -0.2;
            for (int i = 0; i < 1024; i++)
            {
                if (pixels[i]=='1') numb.Vesa[i] += s;
            }
        }
    }
}

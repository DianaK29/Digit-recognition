using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Цифры
{
    public class Number
    {
        public string Name;
        public double[] Vesa;

        public Number(string name, double[] vesa)
        {
            Name = name;
            Vesa = vesa;
        }
        public double Check(string pixels,Number numb)
        {
            double sum = 0;
            for (int i=0; i<Vesa.Length; i++)
            {
                sum += (pixels[i] - '0') * numb.Vesa[i]; ;
            }
            return sum;
        }
        public bool CheckForLearn(string pixels, Number numb)
        {
            double sum = 0;
            int b = 0;
            for (int i = 0; i < Vesa.Length; i++)
            {
                sum += (pixels[i]- '0') * numb.Vesa[i];
            }
            if (sum>b) return true;
            else return false;
        }
    }
}

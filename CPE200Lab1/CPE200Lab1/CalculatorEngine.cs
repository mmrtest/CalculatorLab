using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPE200Lab1
{
    class CalculatorEngine
    {
        public string calculate(string operate, string firstOperand, string secondOperand, int maxOutputSize = 8)
        {
            switch (operate)
            {
                case "+":
                    return (Convert.ToDouble(firstOperand) + Convert.ToDouble(secondOperand)).ToString();
                case "-":
                    return (Convert.ToDouble(firstOperand) - Convert.ToDouble(secondOperand)).ToString();
                case "X":
                    return (Convert.ToDouble(firstOperand) * Convert.ToDouble(secondOperand)).ToString();
                case "÷":
                    // Not allow devide be zero
                    if (secondOperand != "0")
                    {
                        double result;
                        string[] parts;
                        int remainLength;

                        result = (Convert.ToDouble(firstOperand) / Convert.ToDouble(secondOperand));
                        // split between integer part and fractional part
                        parts = result.ToString().Split('.');
                        // if integer part length is already break max output, return error
                        if (parts[0].Length > maxOutputSize)
                        {
                            return "E";
                        }
                        // calculate remaining space for fractional part.
                        remainLength = maxOutputSize - parts[0].Length - 1;
                        // trim the fractional part gracefully. =
                        return result.ToString("N" + remainLength);
                    }
                    break;
                case "%":
                    double second = Convert.ToDouble(firstOperand) * (Convert.ToDouble(secondOperand) / 100);

                    return Convert.ToString(second);
                case "√":
                    double root = Math.Sqrt(Convert.ToDouble(firstOperand));
                    string ans = Convert.ToString(root);
                    int n = ans.Length;
                    if (n > 8) { n = 8; }
                    //if(root%1!=0) { n++; }
                    ans = ans.Substring(0, n);
                    return ans;
                case "1/x":
                    double dividebyx = 1 / Convert.ToDouble(firstOperand);
                    string answ = Convert.ToString(dividebyx);
                    int i = answ.Length;
                    if (i > 8) { i = 8; }
                    //if(root%1!=0) { n++; }
                    answ = answ.Substring(0, i);
                    return answ;

            }
            return "E";
        }
    }
}

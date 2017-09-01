using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPE200Lab1
{
    public partial class MainForm : Form
    {
        private bool containDot;
        private bool isAllowBack;
        private bool isAfterOperater;
        private bool isAfterEqual;
        private string firstOperand = null;
        private string operate;

        private void resetAll()
        {
            lblDisplay.Text = "0";
            isAllowBack = true;
            containDot = false;
            isAfterOperater = false;
            isAfterEqual = false;
            firstOperand = null;
        }

        private string calculate(string operate, string firstOperand, string secondOperand, int maxOutputSize = 8)
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

        public MainForm()
        {
            InitializeComponent();

            resetAll();
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            if (isAfterOperater)
            {
                lblDisplay.Text = "0";
            }
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            isAllowBack = true;
            string digit = ((Button)sender).Text;
            if (lblDisplay.Text is "0")
            {
                lblDisplay.Text = "";
            }
            lblDisplay.Text += digit;
            isAfterOperater = false;
        }

        private void btnOperator_Click(object sender, EventArgs e)
        {

            if (lblDisplay.Text is "Error")
            {
                return;
            }

            if (isAfterOperater)
            {
                if (((Button)sender).Text == "+" || ((Button)sender).Text == "-" || ((Button)sender).Text == "*" || ((Button)sender).Text == "/" || ((Button)sender).Text == "%")
                {
                    return;
                }

            }
            if (isAfterEqual)
            {
                resetAll();
            }
            if (firstOperand != null)
            {
                string secondOperand = lblDisplay.Text;
                string result;
                if (((Button)sender).Text == "%")
                {
                    secondOperand = calculate("%", firstOperand, secondOperand);
                    result = calculate(operate, firstOperand, secondOperand);

                }
                else if (((Button)sender).Text == "√")
                {
                    result = firstOperand;
                    if (isAfterOperater == false)
                    {
                        result = calculate(operate, firstOperand, secondOperand);

                    }
                    result = calculate("√", result, "0");
                }
                else if (((Button)sender).Text == "1/x")
                {
                    result = firstOperand;
                    if (isAfterOperater == false)
                    {
                        result = calculate(operate, firstOperand, secondOperand);

                    }
                    result = calculate("1/x", result, "0");
                }
                else
                {
                    result = calculate(operate, firstOperand, secondOperand);

                }
                if (result is "E" || result.Length > 8)
                {
                    lblDisplay.Text = "Error";
                }
                else
                {
                    lblDisplay.Text = result;
                }
                firstOperand = result;
                isAfterOperater = true;
                if (((Button)sender).Text == "+" || ((Button)sender).Text == "-" || ((Button)sender).Text == "*" || ((Button)sender).Text == "/")
                {
                    operate = ((Button)sender).Text;

                }
                


                return;
            }
            else
            {
                operate = ((Button)sender).Text;
                switch (operate)
                {
                    case "+":
                    case "-":
                    case "X":
                    case "÷":
                        firstOperand = lblDisplay.Text;
                        isAfterOperater = true;
                        break;
                    case "%":
                        break;
                    case "√":
                        string result = calculate("√", lblDisplay.Text, "0");
                        lblDisplay.Text = result;
                        break;
                    case "1/x":
                        string dividebyx = calculate("1/x", lblDisplay.Text, "0");
                        lblDisplay.Text = dividebyx;
                        break;
                }
            }

            isAllowBack = false;

        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            string secondOperand = lblDisplay.Text;
            string result = calculate(operate, firstOperand, secondOperand);
            if (result is "E" || result.Length > 8)
            {
                lblDisplay.Text = "Error";
            }
            else
            {
                lblDisplay.Text = result;
            }
            isAfterEqual = true;
        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if (!containDot)
            {
                lblDisplay.Text += ".";
                containDot = true;
            }
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            // already contain negative sign
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if (lblDisplay.Text[0] is '-')
            {
                lblDisplay.Text = lblDisplay.Text.Substring(1, lblDisplay.Text.Length - 1);
            }
            else
            {
                lblDisplay.Text = "-" + lblDisplay.Text;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            resetAll();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                return;
            }
            if (!isAllowBack)
            {
                return;
            }
            if (lblDisplay.Text != "0")
            {
                string current = lblDisplay.Text;
                char rightMost = current[current.Length - 1];
                if (rightMost is '.')
                {
                    containDot = false;
                }
                lblDisplay.Text = current.Substring(0, current.Length - 1);
                if (lblDisplay.Text is "" || lblDisplay.Text is "-")
                {
                    lblDisplay.Text = "0";
                }
            }
        }

        private string Memory = "0";
        //private string operate;

        private void Memory_click(object sender, EventArgs e)
        {
            string btnMemory = ((Button)sender).Text;
            switch (btnMemory)
            {
                case "MC":
                    Memory = "0";
                    lblDisplay.Text = "0";
                    break;
                case "MR":
                    
                    if(isAfterOperater == true)
                    {
                        lblDisplay.Text = Memory;
                        isAfterOperater = false;
                    }
                    else
                    {
                        resetAll();
                        lblDisplay.Text = Memory;
                    }
                    break;
                case "MS":
                    Memory = lblDisplay.Text;
                    break;
                case "M+":
                    Memory = (Convert.ToDouble(Memory) + Convert.ToDouble(lblDisplay.Text)).ToString();
                    break;
                case "M-":
                    Memory = (Convert.ToDouble(Memory) - Convert.ToDouble(lblDisplay.Text)).ToString();
                    break;

            }
            
        }
    }
}

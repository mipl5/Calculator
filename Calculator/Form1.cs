using System;
using System.Windows.Forms;

namespace Calculator
{
    public partial class MyCalculatorMain : Form
    {
        private double[] numbers;
        private int howManyNums;
        private char[] operations;
        private int howManyOps;
        private double enterNumber;
        private double enterNumberBefore;
        private double mul;
        private bool isPoint;
        public MyCalculatorMain()
        {
            InitializeComponent();
            numbers = new double[2];
            operations = new char[1];
            mul = 1;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void btn0_Click(object sender, EventArgs e)
        {
            UpdateEnterNumber(0);
        }
        private void btn1_Click(object sender, EventArgs e)
        {
            UpdateEnterNumber(1);
        }
        private void btn2_Click(object sender, EventArgs e)
        {
            UpdateEnterNumber(2);
        }
        private void btn3_Click(object sender, EventArgs e)
        {
            UpdateEnterNumber(3);
        }
        private void btn4_Click(object sender, EventArgs e)
        {
            UpdateEnterNumber(4);
        }
        private void btn5_Click(object sender, EventArgs e)
        {
            UpdateEnterNumber(5);
        }
        private void btn6_Click(object sender, EventArgs e)
        {
            UpdateEnterNumber(6);
        }
        private void btn7_Click(object sender, EventArgs e)
        {
            UpdateEnterNumber(7);
        }
        private void btn8_Click(object sender, EventArgs e)
        {
            UpdateEnterNumber(8);
        }
        private void btn9_Click(object sender, EventArgs e)
        {
            UpdateEnterNumber(9);
        }
        private void UpdateEnterNumber(double num)
        {
            enterNumberBefore = enterNumber;
            if (!isPoint)
            {
                enterNumber *= mul;
                enterNumber += num;
                mul *= 10;
            }
            else
            {
                enterNumber += num * mul;
                mul /= 10;
            }
            textBox1.Text += num.ToString();
        }
        private void AddNumber(double number)
        {
            if (howManyNums >= numbers.Length)
                ResizeItNums();
            numbers[howManyNums] = number;
            howManyNums++;
            enterNumberBefore = 0;
        }
        private void ResizeItNums()
        {
            double[] result = new double[numbers.Length + 10];
            for (int i = 0; i < numbers.Length; i++)
                result[i] = numbers[i];
            numbers = result;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void btnDEL_Click(object sender, EventArgs e)
        {
            string str = textBox1.Text;
            char[]items = str.ToCharArray();
            if (items[items.Length - 1] >= '0' &&
                items[items.Length - 1] <= '9')
                enterNumber -= enterNumberBefore;
            else
            {
                if (howManyOps > 0)
                    howManyOps--;
            }
            str = str.Remove(str.Length - 1);
            textBox1.Text = str;
        }
        private void btnAC_Click(object sender, EventArgs e)
        {
            howManyNums = 0;
            howManyOps = 0;
            enterNumber = 0;
            enterNumberBefore = 0;
            textBox1.ResetText();
        }
        private void AddOperation(char op)
        {
            AddNumber(enterNumber);
            if (operations[howManyOps] == '!' && operations[howManyOps] == '√')
                howManyNums--;
            enterNumber = 0;
            enterNumberBefore = 0;
            mul = 1;
            isPoint = false;
            if (howManyOps >= operations.Length)
                ResizeItOps();
            operations[howManyOps] = op;
            howManyOps++;
        }
        private void ResizeItOps()
        {
            char[] result = new char[operations.Length + 2];
            for (int i = 0; i < operations.Length; i++)
                result[i] = operations[i];
            operations = result;
        }
        private void btnSquareRoot_Click(object sender, EventArgs e)
        {
            AddOperation('√');
            textBox1.Text += "√";
        }
        private void btnPowerAny_Click(object sender, EventArgs e)
        {
            AddOperation('^');
            textBox1.Text += "^";
        }
        private void btnFactorial_Click(object sender, EventArgs e)
        {
            AddOperation('!');
            textBox1.Text += "!";
        }
        private void btnMultiplication_Click(object sender, EventArgs e)
        {
            AddOperation('x');
            textBox1.Text += "x";
        }
        private void btnDivision_Click(object sender, EventArgs e)
        {
            AddOperation('/');
            textBox1.Text += "÷";
        }
        private void btnAddition_Click(object sender, EventArgs e)
        {
            AddOperation('+');
            textBox1.Text += "+";
        }
        private void btnSubstraction_Click(object sender, EventArgs e)
        {
            AddOperation('-');
            textBox1.Text += "-";
        }
        private void btnANS_Click(object sender, EventArgs e)
        {
            AddNumber(enterNumber);
            enterNumber = 0;
            enterNumberBefore = 0;
            mul = 1;
            isPoint = false;
            answer = CalculateIt();
            textBox1.Text += "=" + answer.ToString();
            textBox1.Text += "\r\n";
            howManyNums = 0;
            howManyOps = 0;
        }
        private double CalculateIt()
        {
            for (int i = 0; i < howManyOps; i++)
            {
                if (operations[i] == '^')
                {
                    double temp = Math.Pow(numbers[i], numbers[i + 1]);
                    RemoveNumbers(i);
                    numbers[i] = temp;
                    RemoveOperations(i);
                }
                else if (operations[i] == '√')
                {
                    double temp = Math.Sqrt(numbers[i]);
                    numbers[i] = temp;
                    RemoveOperations(i);
                }
                else if (operations[i] == '!')
                {
                    double temp = GetFactorial(numbers[i]);
                    numbers[i] = temp;
                    RemoveOperations(i);
                }
            }
            for (int i = 0; i < howManyOps; i++)
            {
                if (operations[i] == 'x' || operations[i] == '/')
                {
                    double temp = 0;
                    if (operations[i] == 'x')
                    {
                        temp = numbers[i] * numbers[i + 1];
                    }
                    else
                    {
                        temp = numbers[i] / numbers[i + 1];
                    }
                    RemoveNumbers(i);
                    numbers[i] = temp;
                    RemoveOperations(i);
                }
            }
            double sum = 0;
            sum += numbers[0];
            for (int i = 0; i < howManyOps; i++)
            {
                if (operations[i] == '+')
                    sum += numbers[i + 1];
                else
                    sum -= numbers[i + 1];
            }
            return sum;
        }
        private double GetFactorial(double v)
        {
            double mul = 1;
            while (v > 0)
            {
                mul *= v;
                v--;
            }
            return mul;
        }
        private void RemoveOperations(int index)
        {
            for (int i = index; i < howManyOps - 1; i++)
                operations[i] = operations[i + 1];
            howManyOps--;
        }
        private void RemoveNumbers(int index)
        {
            for (int i = index; i < howManyNums - 1; i++)
                numbers[i] = numbers[i + 1];
            howManyNums--;
        }
        private void helpBtn_Click(object sender, EventArgs e)
        {
            MessageBox.Show("DEL button - delete\n" +
                "AC button - clear all\n" +
                "ANS button - answer\n" +
                "SQRT button - square root\n" +
                "POW button - pow\n" +
                "FACT button - factorial\n" +
                "MUL button - multiplication\n" +
                "DIV button - division\n" +
                "ADD button - addition\n" +
                "SUB button - substraction\n", "HELP DOCUMENTATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void button1_Click(object sender, EventArgs e) // floatingPtBtn_Click
        {
            textBox1.Text += '.';
            isPoint = true;
            mul = 1 / (double)10;
        }
        private double answer;
        private void ansBtn_Click(object sender, EventArgs e)
        {
            enterNumberBefore = enterNumber;
            enterNumber = 0;
            UpdateEnterNumber(answer);
            answer = 0;
        }
    }
}
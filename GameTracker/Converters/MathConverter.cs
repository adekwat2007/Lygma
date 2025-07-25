using System.Globalization;
using System.Windows.Data;

namespace GameTracker.Converters
{
    internal class MathConverter : IValueConverter
    {
        private readonly char[] _allOperators = ['+', '-', '(', ')'];
        private readonly List<string> _grouping = ["(", ")"];
        private readonly List<string> _operators = ["+", "-"];

        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var mathEquation = parameter as string;
            mathEquation = mathEquation.Replace(" ", "");
            mathEquation = mathEquation.Replace("@VALUE", value.ToString());

            var numbers = new List<int>();
            int tmp;

            foreach (string s in mathEquation.Split(_allOperators))
            {
                if (s != string.Empty)
                {
                    if (int.TryParse(s, out tmp))
                        numbers.Add(tmp);
                    else
                        throw new InvalidCastException();
                }
            }

            EvaluateMathString(ref mathEquation, ref numbers, 0);
            return numbers[0];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion IValueConverter Members

        private void EvaluateMathString(ref string mathEquation, ref List<int> numbers, int index)
        {
            string token = GetNextToken(mathEquation);

            while (token != string.Empty)
            {
                mathEquation = mathEquation.Remove(0, token.Length);
                if (_grouping.Contains(token))
                {
                    switch (token)
                    {
                        case "(":
                            EvaluateMathString(ref mathEquation, ref numbers, index);
                            break;

                        case ")":
                            return;
                    }
                }

                if (_operators.Contains(token))
                {
                    string nextToken = GetNextToken(mathEquation);
                    if (nextToken == "(")
                    {
                        EvaluateMathString(ref mathEquation, ref numbers, index + 1);
                    }

                    if (numbers.Count > (index + 1) &&
                        (double.Parse(nextToken) == numbers[index + 1] || nextToken == "("))
                    {
                        switch (token)
                        {
                            case "+":
                                numbers[index] = numbers[index] + numbers[index + 1];
                                break;

                            case "-":
                                numbers[index] = numbers[index] - numbers[index + 1];
                                break;

                            case "*":
                                numbers[index] = numbers[index] * numbers[index + 1];
                                break;

                            case "/":
                                numbers[index] = numbers[index] / numbers[index + 1];
                                break;

                            case "%":
                                numbers[index] = numbers[index] % numbers[index + 1];
                                break;
                        }
                        numbers.RemoveAt(index + 1);
                    }
                    else
                        throw new FormatException("Next token is not the expected number");
                }

                token = GetNextToken(mathEquation);
            }
        }

        private string GetNextToken(string mathEquation)
        {
            if (mathEquation == string.Empty)
                return string.Empty;

            string tmp = "";
            foreach (char c in mathEquation)
            {
                if (_allOperators.Contains(c))
                    return (tmp == "" ? c.ToString() : tmp);
                else
                    tmp += c;
            }
            return tmp;
        }
    }
}
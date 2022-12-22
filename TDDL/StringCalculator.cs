namespace TDD;

public class StringCalculator
{
    public int Add(string number, string delimiter = ",", List<string> delimiterList = null)
    {
        var result = 0;
        var negativeStringNumber = new List<string>();
        if (number == "")
            return result;
        if (delimiterList == null)
        {
            number = number.Replace("\n", delimiter);
        }
        else
        {
            foreach (var del in delimiterList)
            { 
                number = number.Replace("\n", del);
                number = number.Replace(del, delimiter);

            }
        }
        
        var numberList = number.Split(delimiter);
        foreach (var num in numberList)
        {
            var numb = 0;
            Int32.TryParse(num, out numb);

            if (numb < 0)
            {
                negativeStringNumber.Add(num);
            }

            if (numb > 1000)
            {
                continue;
            }

            result += numb;
        }

        if (negativeStringNumber.Count != 0)
        {
            var temp = string.Join(delimiter, negativeStringNumber);
            throw new NegativeNotAllowed($"Negative number: {temp}");
        }

        return result;
    }
}
using NUnit.Framework;
using TDD;

namespace TDDTest;

public class Tests
{

    [Test]
    public void ShouldStringCalcuatorAddEmptyEqualZero()
    {
        var stringCalucator = new StringCalculator();

        var result = stringCalucator.Add("");
        
        Assert.That(result, Is.EqualTo(0));
    }
    
    
    [Test]
    public void ShouldStringCalcuatorReturnNumber()
    {
        var stringCalucator = new StringCalculator();

        var result = stringCalucator.Add("1");
        
        Assert.That(result, Is.EqualTo(1));
    }
    
    [TestCase("1,2", 3)]
    [TestCase("1,2,3", 6)]
    [TestCase("1,2,3,4", 10 )]
    [Parallelizable(ParallelScope.Children)]
    public void ShouldStringCalcuatorAddNumers(string numbers, int expected)
    {
        var stringCalucator = new StringCalculator();

        var result = stringCalucator.Add(numbers);
        
        Assert.That(result, Is.EqualTo(expected));
    }
    
    [TestCase("1\n2", 3)]
    [TestCase("1\n2,3", 6)]
    [TestCase("1,2\n3,4", 10 )]
    [Parallelizable(ParallelScope.Children)]
    public void ShouldStringCalcuatorAddNumbersNextLine(string numbers, int expected)
    {
        var stringCalucator = new StringCalculator();

        var result = stringCalucator.Add(numbers);
        
        Assert.That(result, Is.EqualTo(expected));
    }
    
    [TestCase("//;\n1;2", 3, ";" )]
    [TestCase("//'\n1'2'3", 6, "'")]
    [TestCase("//,\n1,2\n3,4", 10, "," )]
    [Parallelizable(ParallelScope.Children)]
    public void ShouldStringCalcuatorAddNumbersDelimiter(string numbers, int expected, string delimiter)
    {
        var stringCalucator = new StringCalculator();

        var result = stringCalucator.Add(numbers, delimiter);
        
        Assert.That(result, Is.EqualTo(expected));
    }
    
    
    [TestCase("//;\n-1;2", 1, ";", "Negative number: -1" )]
    [TestCase("//'\n1'-2'3", 4, "'", "Negative number: -2")]
    [TestCase("//,\n1,-2\n3,-4", -2, ",", "Negative number: -2,-4" )]
    [Parallelizable(ParallelScope.Children)]
    public void ShouldStringCalcuatorAddNumbersWithNegativeNumber(string numbers, int expected, string delimiter, string message)
    {
        var stringCalucator = new StringCalculator();
        
        Assert.That(() => stringCalucator.Add(numbers, delimiter), Throws.TypeOf<NegativeNotAllowed>()
            .With.Message
            .EqualTo(message));
    }
    
    [TestCase("//;\n1001", 0, ";")]
    [TestCase("//'\n1005'1002'3", 3, "'")]
    [TestCase("//'\n1005'2'3", 5, "'")]
    [TestCase("//,,,\n1005,,,102,,,3", 105, ",,,")]
    [Parallelizable(ParallelScope.Children)]
    public void ShouldStringCalcuatorAddNumbersIgnoreMoreThan1000(string numbers, int expected, string delimiter)
    {
        var stringCalucator = new StringCalculator();
        
        var result = stringCalucator.Add(numbers, delimiter);
        
        Assert.That(result, Is.EqualTo(expected));
    }
    
    
    [Test]
    public void ShouldStringCalcuatorAddNumbersManyDelimiters()
    {
        var stringCalucator = new StringCalculator();
        var list = new List<string>{",","%"};
        
        var result = stringCalucator.Add("//,%\n1,2\n3%4",",",list);
        
        Assert.That(result, Is.EqualTo(10));
    }
    
    [Test]
    public void ShouldStringCalcuatorAddNumbersManyDelimiters2()
    {
        var stringCalucator = new StringCalculator();
        var list = new List<string>{",,","%%"};
        
        var result = stringCalucator.Add("//,,%%\n1,,2\n3%%4",",",list);
        
        Assert.That(result, Is.EqualTo(10));
    }
}
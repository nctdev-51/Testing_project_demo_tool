using BasicExercises.Exercises;
using Reqnroll;

namespace BasicExercises.StepDefinitions;

[Binding]
public class DateSteps
{
    private bool _isValid;
    private bool _isLeapYear;

    [When("I validate date {int}-{int}-{int}")]
    public void WhenIValidateDate(int day, int month, int year)
    {
        _isValid = DateUtil.IsValidDate(day, month, year);
        _isLeapYear = DateUtil.IsLeapYear(year);
    }

    [Then("the date validity should be {word}")]
    public void ThenDateValidityShouldBe(string expected)
    {
        Assert.Equal(bool.Parse(expected), _isValid);
    }

    [Then("the leap year result should be {word}")]
    public void ThenLeapYearResultShouldBe(string expected)
    {
        Assert.Equal(bool.Parse(expected), _isLeapYear);
    }
}
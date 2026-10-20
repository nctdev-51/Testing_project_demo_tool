using BasicExercises.Exercises;
using Reqnroll;

namespace BasicExercises.StepDefinitions;

[Binding]
public class PasswordSteps
{
    private string _result = string.Empty;

    [When("I validate the password {string}")]
    public void WhenIValidateThePassword(string password)
        => _result = PasswordValidator.Validate(password);

    [When("I validate an empty password")]
    public void WhenIValidateAnEmptyPassword()
        => _result = PasswordValidator.Validate(string.Empty);

    [Then("the password result should be {string}")]
    public void ThenThePasswordResultShouldBe(string expected)
        => Assert.Equal(expected, _result);
}

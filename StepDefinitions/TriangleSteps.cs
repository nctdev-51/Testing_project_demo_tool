using BasicExercises.Exercises;
using BasicExercises.Support;
using Reqnroll;

namespace BasicExercises.StepDefinitions;

[Binding]
public class TriangleSteps
{
    private string _result = "";
    private readonly List<string> _failures = new();

    [When("I classify a triangle with sides {int}, {int}, {int}")]
    public void When(int a, int b, int c) => _result = Triangle.Classify(a, b, c);

    [Then("the result should be {word}")]
    public void Then(string expected) => Assert.Equal(expected, _result);

    // Step dự phòng và dùng để demo so sánh Cách B
    [When("I run every triangle case in {string}")]
    public void RunAll(string file)
    {
        foreach (var r in CsvTable.Read(file))
        {
            var actual = Triangle.Classify(int.Parse(r["a"]), int.Parse(r["b"]), int.Parse(r["c"]));
            if (actual != r["expected"])
                _failures.Add($"({r["a"]},{r["b"]},{r["c"]}) expected {r["expected"]} but got {actual}");
        }
    }

    [Then("no case should fail")]
    public void NoFail() => Assert.True(_failures.Count == 0, string.Join(Environment.NewLine, _failures));
}
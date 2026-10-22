namespace BasicExercises.Exercises;

public static class PasswordValidator
{
    public static string Validate(string? password)
    {
        password ??= string.Empty;

        var errors = new List<string>();

        if (password.Length < 8)
            errors.Add("TooShort");
        if (!password.Any(char.IsUpper))
            errors.Add("NoUpper");
        if (!password.Any(char.IsDigit))
            errors.Add("NoDigit");

        return errors.Count == 0 ? "Valid" : string.Join(';', errors);
    }
}

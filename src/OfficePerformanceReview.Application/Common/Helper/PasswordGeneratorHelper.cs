public static class PasswordGeneratorHelper
{
    private static readonly Random _random = new Random();

    private const string Uppercase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
    private const string Lowercase = "abcdefghijklmnopqrstuvwxyz";
    private const string Numbers = "0123456789";
    private const string SpecialChars = "!@#$%^&*()-_=+<>?";

    public static string GeneratePassword(int length = 8)
    {
        if (length < 4) throw new ArgumentException("Password length must be at least 4 characters.");

        string allChars = Uppercase + Lowercase + Numbers + SpecialChars;
        char[] password = new char[length];

        // Ensure at least one character from each category
        password[0] = Uppercase[_random.Next(Uppercase.Length)];
        password[1] = Lowercase[_random.Next(Lowercase.Length)];
        password[2] = Numbers[_random.Next(Numbers.Length)];
        password[3] = SpecialChars[_random.Next(SpecialChars.Length)];

        // Fill the rest of the password randomly
        for (int i = 4; i < length; i++)
        {
            password[i] = allChars[_random.Next(allChars.Length)];
        }

        // Shuffle password to avoid predictable patterns
        return new string(password.OrderBy(_ => _random.Next()).ToArray());
    }
}

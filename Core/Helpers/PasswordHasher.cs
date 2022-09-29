namespace NotasAPI.Core.Helpers;

public static class PasswordHasher
{
    public static string HashPasswordBCrypt(this string str)
    {
        return BCrypt.Net.BCrypt.HashPassword(str);
    }

    public static bool VerifyHashPasswordBCrypt(this string str, string text)
    {
        return BCrypt.Net.BCrypt.Verify(str, text);
    }
}

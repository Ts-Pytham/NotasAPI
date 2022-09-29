namespace NotasAPI.Core.Helpers;

public static class Convertions
{
    public static int? ToInt32(this string s)
    {
        try
        {
            return s is not null ? Convert.ToInt32(s) : null;
        }
        catch (Exception)
        {
            return null;
        }
    }
}

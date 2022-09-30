namespace NotasAPI.Core.Models;

public class QueryProperty<T> where T : Entity
{
    public QueryProperty()
    {

    }

    public QueryProperty(int page, int pageCount)
    {
        page = page > 0 ? page : 1;
        pageCount = pageCount > 0 ? pageCount : 1;

        Skip = (page - 1) * pageCount;
        Take = pageCount;
    }

    public int Skip { get; set; }
    public int Take { get; set; }
    public Expression<Func<T, bool>> Where { get; set; }
    public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
    public Expression<Func<T, object>> OrderBy { get; set; }
    public bool Decending { get; set; }
}

namespace chatApi.Repositeries;

public abstract class Repository(AppDbContext context)
{
    internal AppDbContext Context = context;
}
namespace Application.Tests
{
    public interface IBuilder <T> where T : class
    {
        T Build ();
    }
}

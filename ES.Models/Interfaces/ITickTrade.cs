namespace ES.Domain.Interfaces
{
    public interface ITickTrade : ITick
    {
        decimal Price { get; set; }
        decimal Volume { get; set; }
    }
}

namespace Potentii.UniLog.Core
{
    public interface IUniLogConverter<in T> : IUniLogConverter
    {
        object? Convert(T value);
    }
    
    public interface IUniLogConverter
    {
    }
}
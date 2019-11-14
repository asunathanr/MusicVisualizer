namespace _3DMusicVisualizer
{
    public interface IConsumer<T>
    {
        void Consume<T>(T value);
    }
}
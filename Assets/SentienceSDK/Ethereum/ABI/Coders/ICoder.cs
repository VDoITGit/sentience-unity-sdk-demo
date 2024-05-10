namespace Sentience.ABI
{
    public interface ICoder
    {
        byte[] Encode(object value);

        object Decode(byte[] encoded);

    }
}
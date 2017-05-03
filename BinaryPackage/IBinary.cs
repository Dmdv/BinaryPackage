namespace BinaryPackage
{
    public interface IBinary
    {
        byte[] ToByteArray();

        void FromByteArray(byte[] buffer);
    }
}
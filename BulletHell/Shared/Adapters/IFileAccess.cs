public interface IFileAccess
{
    public string GetString();
    public uint Get32();
    public double GetDouble();

    public void Store32(uint value);
    public void StoreDouble(double value);
    public void StoreString(string @string);
}

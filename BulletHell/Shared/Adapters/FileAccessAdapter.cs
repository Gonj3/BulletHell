using Godot;

public class FileAccessAdapter : IFileAccess
{
    private readonly FileAccess _inner;

    public FileAccessAdapter(FileAccess inner)
    {
        _inner = inner;
    }

    public uint Get32()
    {
        return _inner.Get32();
    }

    public double GetDouble()
    {
        return _inner.GetDouble();
    }

    public string GetString()
    {
        return _inner.GetPascalString();
    }

    public void Store32(uint value)
    {
        _inner.Store32(value);
    }

    public void StoreDouble(double value)
    {
        _inner.StoreDouble(value);
    }

    public void StoreString(string @string)
    {
        _inner.StorePascalString(@string);
    }
}

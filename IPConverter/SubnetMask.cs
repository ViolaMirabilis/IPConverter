namespace IPConverter;

public class SubnetMask
{
    public int Prefix { get; set; }
    public byte[] Octets { get; set; }

    public SubnetMask(int prefix, byte[] validMask)
    {
        Prefix = prefix;
        Octets = validMask;
    }

}

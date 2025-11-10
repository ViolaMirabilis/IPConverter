using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace IPConverter;

public class InputHandler
{
    public (IPAddress?, SubnetMask?) FormatInput(string input)
    {
        string[] AddressDetails = input.Split('/');                 // Splits the IP
        string tmpIP = AddressDetails[0];                           // IP, e.g. 12.12.12.12
        int MaskPrefix = Convert.ToInt32(AddressDetails[1]);    // Mask, e.g. 15;

        byte[] IP = FormatIP(tmpIP);
        if(!IsIPValidFormat(IP))
        {
            Console.WriteLine("Invalid IP Address format!");
            return (null, null);
        }

        if (!IsMaskValidFormat(MaskPrefix))                  // if input is in range
        {
            Console.WriteLine("Invalid Subnet Mask format!");
            return(null, null);
        }
        byte[] Mask = FormatMask(MaskPrefix);

        IPAddress address = new IPAddress(IP);
        SubnetMask mask = new SubnetMask(MaskPrefix, Mask);

        return (address, mask);


    }
    public byte[] FormatIP(string input)
    {
        string[] TmpOctets = input.Split('.');
        

        byte[] Octets = new byte[4];
        for (int i = 0; i < TmpOctets.Length; i++)
        {
            if (TmpOctets[i] != null)
            {
                Octets[i] = Convert.ToByte(TmpOctets[i]);
            }
            else
            {
                Octets[i] = 0;
            }

        }

        return Octets;
        
    }
    public byte[] FormatMask(int prefix)
    {
        byte[] Mask = new byte[4];

        for (int i = 0; i < 4; i++)
        {
            int MaskBits = prefix - (i * 8);       // e.g. prefix = 13, so: 13 - (0*8), next iteration: 13 - (1*8) = 5, and so on...
            if (MaskBits >= 8)
            {
                Mask[i] = (byte)255;
            }
            else if (MaskBits > 0)
            {
                Mask[i] = (byte)(256 - Math.Pow(2, 8 - MaskBits));     // iteration 2: 13 - 1 * 8 = 5.
                                                                            // 8 - 5 = 3. 2 to the power of 3 = 8. 256 - 8 is 248.
            }
            else
            {
                Mask[i] = 0;
            }
        }
        return Mask;
    }
    public bool IsIPValidFormat(byte[] IP)
    {
        foreach (byte b in IP)
        {
            if (b > 255 || b < 0) return false;
        }
        return true;
    }

    public bool IsMaskValidFormat(int input)
    {
        return (input > 0 && input < 32) ? true : false;
    }
}

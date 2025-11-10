using System.Linq.Expressions;
using System.Runtime.InteropServices;

namespace IPConverter;

public class InputHandler
{
    public (IPAddress?, SubnetMask?) FormatInput(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            StringHelper.PrintLine(ConsoleColor.Red, "Empty input!");
            return (null, null);
        }
        else if (!input.Contains('/'))
        {
            StringHelper.PrintLine(ConsoleColor.Red, "The subnet mask symbol '/' is missing!");
            return (null, null);
        }
        string[] AddressDetails = input.Split('/');                 // Splits the IP
        string tmpIP = AddressDetails[0];                           // IP, e.g. 12.12.12.12
        //int MaskPrefix = Convert.ToInt32(AddressDetails[1]);    // Mask, e.g. 15;
        string MaskPrefix = AddressDetails[1];

        // Mask check
        if (!int.TryParse(MaskPrefix, out _))
        {
            StringHelper.PrintLine(ConsoleColor.Red, "Invalid Subnet Mask format!");
            return (null, null);
        }

        if (!IsMaskValidFormat(Convert.ToInt32(MaskPrefix)))                  // if input is in range
        {
            Console.WriteLine("Invalid Subnet Mask format!");
            return (null, null);
        }

        byte[] Mask = FormatMask(Convert.ToInt32(MaskPrefix));
        
        // IP Check
        byte[] IP = FormatIP(tmpIP);
        /*if (IP == null)
        {
            return (null, null);
        }*/


        IPAddress address = new IPAddress(IP);
        SubnetMask mask = new SubnetMask(Convert.ToInt32(MaskPrefix), Mask);

        return (address, mask);

    }
    public byte[] FormatIP(string input)
    {
        string[] TmpOctets = input.Split('.');
        
        if (TmpOctets.Length != 4)
        {
            StringHelper.Print(ConsoleColor.Red, "Invalid IP Address format!");
            return null;
        }

        byte[] Octets = new byte[4];        // creates 4 IP octets

        for (int i = 0; i < TmpOctets.Length; i++)
        {
            if (!int.TryParse(TmpOctets[i], out int value))     // if input cannot be parsed
            {
                StringHelper.PrintLine(ConsoleColor.Red, "Invalid IP format!");
                return null;
            }

            if (value < 0 || value > 255)                       // if input is beyond acceptable range
            {
                StringHelper.PrintLine(ConsoleColor.Red, "Invalid IP format!");
                return null;
            }
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
        try
        {
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
        catch (Exception ex)
        {
            Console.WriteLine("Invalid Subnet Mask format!");
        }
        return null;
        
    }

    public bool IsMaskValidFormat(int input)
    {
        return (input > 0 && input < 32) ? true : false;
    }
}

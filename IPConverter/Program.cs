namespace IPConverter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool IsRunning = true;
            InputHandler handler = new InputHandler();
            while (IsRunning)
            {
                Console.WriteLine("Enter your IPv4 address and Subnet Mask prefix in a proper format.");
                StringHelper.Print(ConsoleColor.DarkYellow, "127.0.0.1");
                Console.Write("/");
                StringHelper.Print(ConsoleColor.Red, "16");

                Console.WriteLine();

                StringHelper.Print(ConsoleColor.DarkYellow, "■");
                Console.Write(" --> IPv4 Addres separated by .");

                Console.WriteLine();

                StringHelper.Print(ConsoleColor.Red, "■");
                Console.WriteLine(" --> Subnet Mask Prefix length");

                Console.Write("Your full IP Address: ");



                string? input = Console.ReadLine();

                var address = handler.FormatInput(input);
                if (address.Item1 != null && address.Item2 != null)
                {
                    Console.WriteLine($"Your IP address: {string.Join('.', address.Item1.Octets)}");
                    Console.WriteLine($"Subnet Mask prefix length: {address.Item2.Prefix.ToString()}");
                    Console.WriteLine($"Subnet Mask value: {string.Join('.', address.Item2.Octets)}");
                }
                else
                {
                    Console.WriteLine("Press ENTER to continue");
                    Console.ReadLine();
                    Console.Clear();
                    continue;
                }

                    break;
            }
            
            
            



        }
    }
}

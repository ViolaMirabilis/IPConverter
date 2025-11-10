namespace IPConverter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            InputHandler handler = new InputHandler();
            Console.Write("Wprowadź adres IP: ");
            string? input = Console.ReadLine();

            var address = handler.FormatInput(input);
            if (address.Item1 != null)
            {
                Console.WriteLine($"Twój adres IP: {string.Join('.', address.Item1.Octets)}");
                Console.WriteLine($"Dlugosc twojej maski: {address.Item2.Prefix.ToString()}");
                Console.WriteLine($"Twoja maska: {string.Join('.', address.Item2.Octets)}");
            }
            else
            {
                Console.WriteLine("BŁĄD!");
            }
            



        }
    }
}

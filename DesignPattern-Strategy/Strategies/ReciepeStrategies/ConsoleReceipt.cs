namespace DesignPattern_Strategy.Strategies.ReceiptStrategies
{
    internal class ConsoleReceipt : IReceiptStrategy
    {
        public void GetReciept(string reciepe)
        {
            Console.WriteLine(reciepe);
        }
    }
}

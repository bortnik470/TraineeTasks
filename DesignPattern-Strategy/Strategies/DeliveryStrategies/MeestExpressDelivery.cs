namespace DesignPattern_Strategy.Strategies.DeliveryStrategies
{
    internal class MeestExpressDelivery : IDeliveryStrategy
    {
        public void DeliveryToAddress(string address)
        {
            Console.WriteLine($"You choose meest express as delivery company. Delivery address: {address}");
        }
    }
}

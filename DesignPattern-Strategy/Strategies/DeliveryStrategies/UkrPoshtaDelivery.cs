namespace DesignPattern_Strategy.Strategies.DeliveryStrategies
{
    internal class UkrPoshtaDelivery : IDeliveryStrategy
    {
        public void DeliveryToAddress(string address)
        {
            Console.WriteLine($"You choose ukrposhta as delivery company. Delivery address: {address}");
        }
    }
}

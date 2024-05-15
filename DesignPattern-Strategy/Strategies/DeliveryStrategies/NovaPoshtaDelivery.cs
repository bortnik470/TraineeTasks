namespace DesignPattern_Strategy.Strategies.DeliveryStrategies
{
    internal class NovaPoshtaDelivery : IDeliveryStrategy
    {
        public void DeliveryToAddress(string address)
        {
            Console.WriteLine($"You choose nova poshta as delivery company. Delivery address: {address}");
        }
    }
}

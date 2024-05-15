namespace DesignPattern_Strategy.Strategies.DeliveryStrategies
{
    internal interface IDeliveryStrategy
    {
        public void DeliveryToAddress(string address);
    }
}
using DesignPattern_Strategy.DataModels;
using DesignPattern_Strategy.Strategies.DeliveryStrategies;
using DesignPattern_Strategy.Strategies.ReceiptStrategies;

namespace DesignPattern_Strategy
{
    internal class Customer
    {
        private IDeliveryStrategy _DeliveryStrategy;
        private IReceiptStrategy _ReceiptStrategy;

        public Order CurrentOrder { get; set; }

        public List<Order> PastOrders { get; set; }

        public void SetDeliveryCompany()
        {
            Console.WriteLine("Choose your delivery company:\n 1: Nova Poshta\n 2: Ukrposhta\n 3: MeestExpress");
            int i = int.Parse(Console.ReadLine());

            switch (i)
            {
                case 1:
                    _DeliveryStrategy = new NovaPoshtaDelivery();
                    break;
                case 2:
                    _DeliveryStrategy = new UkrPoshtaDelivery();
                    break;
                case 3:
                    _DeliveryStrategy = new MeestExpressDelivery();
                    break;
                default:
                    break;
            }
        }

        public void SetReciepeType()
        {
            Console.WriteLine("Choose your receipt type:\n 1: Print\n 2: Write to file");
            int i = int.Parse(Console.ReadLine());

            switch (i)
            {
                case 1:
                    _ReceiptStrategy = new ConsoleReceipt();
                    break;
                case 2:
                    _ReceiptStrategy = new FileReceipt();
                    break;
                default:
                    break;
            }
        }

        public void Buy(string address)
        {
            if (CurrentOrder == null)
                throw new NullReferenceException("Order was not set");

            PastOrders.Add(CurrentOrder);

            if (_DeliveryStrategy == null)
                SetDeliveryCompany();
            if (_ReceiptStrategy == null)
                SetReciepeType();

            _DeliveryStrategy.DeliveryToAddress(address);
            _ReceiptStrategy.GetReciept(CurrentOrder.ToString());

            CurrentOrder = null;
        }

        public Customer(Order order)
        {
            CurrentOrder = order;
            PastOrders = new List<Order>();
        }

        public Customer()
        {
            CurrentOrder = new Order();
            PastOrders = new List<Order>();
        }
    }
}

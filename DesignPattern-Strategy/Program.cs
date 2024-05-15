using DesignPattern_Strategy;
using DesignPattern_Strategy.DataModels;

var products = new List<Product>
{
    new Product("Headphones", 60),
    new Product("ComputerMouse", 20),
    new Product("Chair", 150)
};

Order order = new Order();

order.AddProducts(products);

Customer customer = new Customer(order);

customer.Buy("21002");
using DesignPattern_Strategy.DataModels;
using System.Text;

namespace DesignPattern_Strategy
{
    internal class Order
    {
        private List<Product> products;

        public Order()
        {
            products = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            products.Add(product);
        }

        public void AddProducts(IEnumerable<Product> products)
        {
            this.products.AddRange(products);
        }

        public List<Product> GetProducts()
        {
            return products;
        }

        public int GetTotalPrice()
        {
            int result = 0;
            foreach (Product product in products)
            {
                result += product.Price;
            }

            return result;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("List of products:");

            foreach (Product product in products)
            {
                sb.AppendLine($"Name: {product.Name} Price: {product.Price}");
            }


            sb.AppendLine($"\nTotal price {GetTotalPrice()}");

            return sb.ToString();
        }
    }
}

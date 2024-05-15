namespace DesignPattern_Strategy.Strategies.ReceiptStrategies
{
    internal class FileReceipt : IReceiptStrategy
    {
        public void GetReciept(string reciept)
        {
            using(FileStream fs = new FileStream("Reciept.txt", FileMode.Create, FileAccess.Write))
            {
                StreamWriter streamWriter = new StreamWriter(fs);

                streamWriter.Write(reciept);

                streamWriter.Close();
            }
        }
    }
}
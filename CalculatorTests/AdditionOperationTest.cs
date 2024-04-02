namespace Calculator
{
    public class AdditionOperationTest
    {
        [Fact]
        public void StandartAdditionOperationsTest()
        {
            var testData = new double[][]
            {
                [1, 1],
                [-1, -1],
                [1.1111,
                2.9999]
            };
            List<double> result = new List<double>();

            foreach (var item in testData)
            {
                result.Add(Calculator.AdditionOperation(item[0], item[1]));
            }

            Assert.Equal(2, result[0]);
            Assert.Equal(-2, result[1]);
            Assert.Equal(4.111, result[2]);
        }
    }
}
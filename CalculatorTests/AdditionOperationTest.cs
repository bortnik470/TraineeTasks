namespace Calculator
{
    public class AdditionOperationTest
    {
        [Theory]
        [InlineData([1, 1, 2])]
        [InlineData([-1, -1, -2])]
        [InlineData([1.1111, 2.9999, 4.111])]
        public void StandartAdditionOperationsTest(double x, double y, double expectedResult)
        {
            double result = Calculator.AdditionOperation(x, y);

            Assert.Equal(expectedResult, result, 2);
        }
    }
}
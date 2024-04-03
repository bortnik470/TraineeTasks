using Xunit.Sdk;

namespace Calculator
{
    public class DivisionOperationTest
    {
        [Theory]
        [InlineData([1, 1, 1])]
        [InlineData([-1, 4, -0.25])]
        [InlineData([4, 2, 2])]
        [InlineData([-2, -1, 2])]
        [InlineData([0, 3, 0])]
        public void StandartDivisionOperationsTest(double x, double y, double expectedResult)
        {
            double result = Calculator.DivisionOperation(x, y);

            Assert.Equal(result, expectedResult, 2);
        }

        [Fact]
        public void DivisionByZeroTest()
        {
            Assert.Throws<DivideByZeroException>(() => Calculator.DivisionOperation(1, 0));
        }
    }
}

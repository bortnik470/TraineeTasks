using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class SubtractionOperationTest
    {
        [Theory]
        [InlineData([1, 1, 0])]
        [InlineData([-1, 4, -5])]
        [InlineData([4, 2, 2])]
        [InlineData([4, 5, -1])]
        public void StandartSubtractionOperationsTest(double x, double y, double expectedResult)
        {
            double result = Calculator.SubtractionOperation(x, y);

            Assert.Equal(expectedResult, result, 2);
        }
    }
}

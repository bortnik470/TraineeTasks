using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class MultiplyOperationTest
    {
        [Theory]
        [InlineData([1, 1, 1])]
        [InlineData([-1, 4, -4])]
        [InlineData([2, 4, 8])]
        [InlineData([-2, -1, 2])]
        [InlineData([0, 1, 0])]
        public void StandartMultiplyOperationTest(double x, double y, double expectedResult)
        {
            double result = Calculator.MultiplyOperation(x, y);

            Assert.Equal(result, expectedResult, 2);
        }
    }
}

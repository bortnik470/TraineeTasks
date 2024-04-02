using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class MultiplyOperationTest
    {
        [Fact]
        public void StandartMultiplyOperationTest()
        {
            var testData = new double[][]
            {
                [1, 1],
                [-1, 4],
                [4, 2],
                [-2, -1],
                [0, 1]
            };
            List<double> result = new List<double>();

            foreach (var item in testData)
            {
                result.Add(Calculator.MultiplyOperation(item[0], item[1]));
            }

            Assert.Equal(1, result[0]);
            Assert.Equal(-4, result[1]);
            Assert.Equal(8, result[2]);
            Assert.Equal(2, result[3]);
            Assert.Equal(0, result[4]);
        }
    }
}

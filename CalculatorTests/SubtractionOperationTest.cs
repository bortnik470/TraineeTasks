using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class SubtractionOperationTest
    {
        [Fact]
        public void StandartSubtractionOperationsTest()
        {
            var testData = new double[][]
            {
                [1, 1],
                [-1, 4],
                [1.9999,
                2.9999]
            };
            List<double> result = new List<double>();

            foreach (var item in testData)
            {
                result.Add(Calculator.SubtractionOperation(item[0], item[1]));
            }

            Assert.Equal(0, result[0]);
            Assert.Equal(-5, result[1]);
            Assert.Equal(-1, result[2]);
        }
    }
}

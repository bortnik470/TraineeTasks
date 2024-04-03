namespace Calculator
{
    public static class Calculator
    {
        public static double AdditionOperation(double x, double y)
        {
            return x + y;
        }

        public static double SubtractionOperation(double x, double y)
        {
            return x - y;
        }

        public static double DivisionOperation(double x, double y)
        {
            if (y == 0)
                throw new DivideByZeroException("Second argument shouldn`t be zero");
            else
                return x / y;
        }

        public static double MultiplyOperation(double x, double y)
        {
            return x * y;
        }

        public static double PowOperation(double x, double y)
        {
            return Math.Pow(x, y);
        }

        public static double SqrtOperation(double x)
        {
            if (x < 0)
                throw new InvalidDataException($"Number should be greater then zero: {x}");
            else
                return Math.Sqrt(x);
        }
    }
}

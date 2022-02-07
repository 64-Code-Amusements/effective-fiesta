using System;

namespace Floatingman.Closures
{
    public static class Incrementor
    {

        public static Func<double, double> GetIncrementor(double step)
            => x => x + step;

        public static Func<int, int> GetIncrementor(int step)
            => x => x + step;

    }
}

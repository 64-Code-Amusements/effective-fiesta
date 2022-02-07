using System;

namespace Floatingman.Closures
{
    public class Incrementor_Anonymous
    {

        public static Func<int, int> GetIncrementor_1(int step)
        {
            var _step = step;
            Func<int, int> lambdaFunc = x => x + _step;
            return lambdaFunc;
        }

        public static Func<int, int> GetIncrementor_2(int step)
        {
            return x => x + step;
        }

        public static Func<int, int> GetIncrementor(int step)
            => x => x + step;

    }
}

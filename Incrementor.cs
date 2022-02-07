using System;

namespace Floatingman.Closures
{
    public class Incrementor
    {
        private int _step;

        public static Incrementor GetIncrementor(int step)
            => new Incrementor(step);

        private Incrementor(int step)
            => _step = step;

        public int Step(int value)
            => value + _step;
    }
}

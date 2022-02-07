using System;

namespace Floatingman.Closures
{
    public class Incrementor_Generic
    {
        private int _step;

        public static Incrementor_Generic GetIncrementor(int step)
            => new Incrementor_Generic(step);

        private Incrementor_Generic(int step)
        {
            _step = step;
            _transformer = Step;
        }

        public int Step(int value)
            => value + _step;
        private Func<int, int> _transformer;

        public Func<int, int> GetTransformer()
            => _transformer;
    }
}

namespace Floatingman.Closures
{
    public delegate int Transformer(int value);
    public class Incrementor_Delegated
    {
        private int _step;

        public static Incrementor_Delegated GetIncrementor(int step)
            => new Incrementor_Delegated(step);

        private Incrementor_Delegated(int step)
        {
            _step = step;
            _transformer = Step;
        }

        public int Step(int value)
            => value + _step;
        private Transformer _transformer;

        public Transformer GetTransformer()
            => _transformer;
    }
}

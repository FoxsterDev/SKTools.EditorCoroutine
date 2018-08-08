using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;


namespace SKTools.EditorCoroutine
{
    internal sealed class CoroutineYield : IYieldFrame
    {
        private readonly Stack<IEnumerator> _stack;
        private float _time;

        public CoroutineYield(float time, IEnumerator source)
        {
            _time = time;
            _stack = new Stack<IEnumerator>();
            _stack.Push(source);
        }

        public bool IsDone { get; private set; }

        public void Update(float time, uint frameCount)
        {
            var _source = _stack.Peek();
            var source = _source as IYieldFrame;
            if (source != null)
            {
                source.Update(time - _time, frameCount);
                _time = time;
            }

            do
            {
                _source = _stack.Peek();

                while (_source.MoveNext())
                    if (_source.Current != null)
                    {
                        _source = _source.Current as IEnumerator;
                        Assert.IsNotNull(_source);
                        _stack.Push(_source);
                    }
                    else
                    {
                        return;
                    }

                _stack.Pop();
            } while (_stack.Count > 0);

            IsDone = true;
        }
    }
}
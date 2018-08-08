using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace SKTools.EditorCoroutine
{
    public sealed class WaitForSeconds : CustomYieldInstruction, IYieldFrame, IYieldProgressReporter
    {
        private readonly float _seconds;
        private float _time;

        public WaitForSeconds(float seconds)
        {
            Assert.IsTrue(seconds > 0f);
            Value01 = 0f;
            _time = _seconds = seconds;
        }

        public override bool keepWaiting
        {
            get { return _time > 0f; }
        }

        void IYieldFrame.Update(float delta, uint frameCount)
        {
            _time -= delta;
            Value01 = 1f - Mathf.Clamp01(_time / _seconds);
            if (ProgressChanged != null) ProgressChanged.Invoke(Value01);

            if (!keepWaiting)
                if (Complete != null)
                    Complete.Invoke();
        }

        event Action<float> IYieldProgressReporter.Changed
        {
            add
            {
                ProgressChanged += value;
                value(Value01);
            }
            remove { ProgressChanged -= value; }
        }

        event Action IYieldProgressReporter.Completed
        {
            add { Complete += value; }
            remove { Complete -= value; }
        }

        public float Value01 { get; private set; }

        private event Action<float> ProgressChanged;

        private event Action Complete;

        public override string ToString()
        {
            return string.Concat("WaitForSeconds: ", _seconds);
        }
    }
}
using System;

namespace SKTools.EditorCoroutine
{
    public interface IYieldProgressReporter
    {
        float Value01 { get; }
        event Action<float> Changed;
        event Action Completed;
    }
}
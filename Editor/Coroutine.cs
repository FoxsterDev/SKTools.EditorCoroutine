using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.Assertions;

namespace SKTools.EditorCoroutine
{
    [InitializeOnLoad]
    public class Coroutine
    {
        private static readonly List<CoroutineYield> list = new List<CoroutineYield>();
        private static uint _frameCount;

        static Coroutine()
        {
        }

        private static void Update()
        {
            _frameCount += 1;

            for (var i = list.Count - 1; i >= 0; i--)
            {
                var yield = list[i];
                yield.Update((float) EditorApplication.timeSinceStartup, _frameCount);
                if (yield.IsDone) list.RemoveAt(i);
            }

            if (list.Count == 0) EditorApplication.update -= Update;
        }

        public static void Start(IEnumerator ienum)
        {
            Assert.IsNotNull(ienum);

            var yield = new CoroutineYield((float) EditorApplication.timeSinceStartup, ienum);

            if (list.Count == 0) EditorApplication.update += Update;

            list.Add(yield);
        }
    }
}
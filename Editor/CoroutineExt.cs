using System.Collections;

namespace SKTools.EditorCoroutine
{
    public static class CoroutineExt
    {
        public static void StartWithProgressBar(this IYieldProgressReporter reporter, string title = "",
            string info = "")
        {
            Coroutine.Start(reporter as IEnumerator);
            ProgressBar.Show(title, info, reporter);
        }
    }
}
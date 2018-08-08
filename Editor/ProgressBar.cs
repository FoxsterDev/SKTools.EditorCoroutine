using UnityEditor;

namespace SKTools.EditorCoroutine
{
    public sealed class ProgressBar
    {
        private static IYieldProgressReporter _progress;
        private static string _title, _info;

        public static void Show(string title, string info, IYieldProgressReporter progressReporter)
        {
            _title = title;
            _info = info;

            if (_progress != null) Progress_Completed();

            _progress = progressReporter;
            RegisterCallbacks();
        }

        private static void Progress_Completed()
        {
            UnRegisterCallbacks();
            _progress = null;
            EditorUtility.ClearProgressBar();
        }

        private static void Progress_Changed(float value)
        {
            EditorUtility.DisplayProgressBar(_title, _info, value);
        }

        private static void RegisterCallbacks()
        {
            _progress.Changed += Progress_Changed;
            _progress.Completed += Progress_Completed;
        }

        private static void UnRegisterCallbacks()
        {
            _progress.Changed -= Progress_Changed;
            _progress.Completed -= Progress_Completed;
        }
    }
}
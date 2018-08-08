using System.Collections;
using UnityEditor;
using UnityEngine;

namespace SKTools.EditorCoroutine
{
    public static class CoroutineExample
    {
        [MenuItem("SKTools/EditorCoroutine Example/Show Progress Bar Waiting for time")]
        private static void ShowProgressBar1()
        {
            new WaitForSeconds(5).StartWithProgressBar("some title", "some info");
        }

        [MenuItem("SKTools/EditorCoroutine Example/Show Progress Bar IEnumerator ")]
        private static void ShowProgressBar2()
        {
            Coroutine.Start(ShowProgressBarIEnumerator());
        }

        [MenuItem("SKTools/EditorCoroutine Example/Null by null ")]
        private static void ShowProgressBar3()
        {
            Coroutine.Start(NullStart());
        }

        private static IEnumerator ShowProgressBarIEnumerator()
        {
            Debug.Log("start: " + EditorApplication.timeSinceStartup);

            var waiting = new WaitForSeconds(1f);
            ProgressBar.Show("ShowProgressBarIEnumerator", "first", waiting);
            yield return waiting;

            Debug.Log("next: " + EditorApplication.timeSinceStartup);
            waiting = new WaitForSeconds(5f);
            ProgressBar.Show("ShowProgressBarIEnumerator", "second", waiting);
            yield return waiting;

            Debug.Log("finish: " + EditorApplication.timeSinceStartup);
        }

        private static IEnumerator NullStart()
        {
            yield return NullMark_1();
        }

        private static IEnumerator NullMark_1()
        {
            yield return NullMark_2();
        }

        private static IEnumerator NullMark_2()
        {
            yield return NullTwain_1();
            yield return NullMark_3();
        }

        private static IEnumerator NullMark_3()
        {
            Debug.Log("NullMark_3 start");
            yield return null;
            Debug.Log("NullMark_3 finish");
        }

        private static IEnumerator NullTwain_1()
        {
            yield return NullTwain_2();
        }

        private static IEnumerator NullTwain_2()
        {
            Debug.Log("NullTwen_2 start");
            yield return new WaitForSeconds(1f);
            Debug.Log("NullTwen_2 finish");
        }
    }
}
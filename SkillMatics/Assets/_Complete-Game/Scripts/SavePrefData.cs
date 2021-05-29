using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Completed
{
    public class SavePrefData : MonoBehaviour
    {
        internal static SavePrefData savePrefInstance;
        internal string scoreKey = "Score";

        private void Awake()
        {
            savePrefInstance = this;
        }

        public void SavePrefs(string prefKey, int prefVal)
        {
            PlayerPrefs.SetInt(prefKey, prefVal);
        }

        public int loadPrefs(string prefKey)
        {
            return PlayerPrefs.GetInt(prefKey, 0);
        }

    }

}
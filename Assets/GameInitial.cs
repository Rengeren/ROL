using UnityEngine;
using System.Collections;

public class GameInitial{
    [RuntimeInitializeOnLoadMethod]
    static void OnRuntimeMethodLoad()
    {
        Screen.SetResolution(1366, 768, false, 60);
    }
}

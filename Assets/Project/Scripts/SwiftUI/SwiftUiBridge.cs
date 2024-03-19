using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using UnityEngine;

public class SwiftUiBridge : MonoBehaviour
{
    public event System.Action<string> OnReceivedMessage; 

    private delegate void CallbackDelegate(string command);

    private void OnEnable()
    {
        SetSampleNativeCallback(CallbackFromNative);
    }

    private void OnDisable()
    {
        SetSampleNativeCallback(null);
    }

    public void ShowScore(int score)
    {
        SwiftUiBridge.UpdateScore(score);
    }

    public void OpenWindow()
    {
        OpenSwiftUiSampleWindow("SampleScene");
    }

    public void CloseWindow()
    {
        CloseSwiftUiSampleWindow("SampleScene");
    }

    [MonoPInvokeCallback(typeof(CallbackDelegate))]
    private static void CallbackFromNative(string message)
    {
        Debug.Log("Callback from native: " + message);

        SwiftUiBridge self = FindFirstObjectByType<SwiftUiBridge>();

        self.OnReceivedMessage?.Invoke(message);
    }

#if UNITY_VISIONOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void SetSampleNativeCallback(CallbackDelegate callback);

        [DllImport("__Internal")]
        private static extern void OpenSwiftUiSampleWindow(string name);

        [DllImport("__Internal")]
        private static extern void CloseSwiftUiSampleWindow(string name);
    
        [DllImport("__Internal")]
        private static extern void UpdateScore(int score);
#else
    static void SetSampleNativeCallback(CallbackDelegate callback)
    {
    }

    static void OpenSwiftUiSampleWindow(string name)
    {
    }

    static void CloseSwiftUiSampleWindow(string name)
    {
    }

    static void UpdateScore(int score)
    {
    }
#endif
}
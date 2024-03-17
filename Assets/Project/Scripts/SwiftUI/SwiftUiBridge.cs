using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AOT;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SwiftUiBridge : MonoBehaviour
{
    private delegate void CallbackDelegate(string command);

    [SerializeField] private TMP_Text _text;
    [SerializeField] private Button _buttoon;
    [SerializeField] private Button _scoreButtoon;

    private int _score = 0;
    private bool _swiftUIWindowOpen = false;

    private void OnEnable()
    {
        SetNativeCallback(CallbackFromNative);
        
        _buttoon.onClick.AddListener(WasPressed);
        _scoreButtoon.onClick.AddListener(() =>
        {
            _score++;
            UpdateScore(_score);
        });
    }

    private void OnDisable()
    {
        SetNativeCallback(null);
        CloseSwiftUiSampleWindow("HelloWorld");
    }

    private void Start()
    {
        Toggle();
    }

    private void WasPressed()
    {
        Debug.Log("----------> Button was pressed");

        Toggle();
    }

    private void Toggle()
    {
        if (_swiftUIWindowOpen)
        {
            CloseSwiftUiSampleWindow("SampleScene");
            _swiftUIWindowOpen = false;
        }
        else
        {
            OpenSwiftUiSampleWindow("SampleScene");
            _swiftUIWindowOpen = true;
        }
    }

    [MonoPInvokeCallback(typeof(CallbackDelegate))]
    private static void CallbackFromNative(string message)
    {
        Debug.Log("Callback from native: " + message);

        SwiftUiBridge self = FindFirstObjectByType<SwiftUiBridge>();

        if (message == "closed")
        {
            self._swiftUIWindowOpen = false;
        }
        else
        {
            self._text.text = message;
        }
    }

#if UNITY_VISIONOS && !UNITY_EDITOR
        [DllImport("__Internal")]
        private static extern void SetNativeCallback(CallbackDelegate callback);

        [DllImport("__Internal")]
        private static extern void OpenSwiftUiSampleWindow(string name);

        [DllImport("__Internal")]
        private static extern void CloseSwiftUiSampleWindow(string name);
    
        [DllImport("__Internal")]
        private static extern void UpdateScore(int score);
#else
    static void SetNativeCallback(CallbackDelegate callback)
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
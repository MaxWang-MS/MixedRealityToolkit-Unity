﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;

#if UNITY_IOS
using UnityEngine.Apple.ReplayKit;
#endif

using Microsoft.MixedReality.Toolkit.Extensions.SpectatorView.Interfaces;

namespace Microsoft.MixedReality.Toolkit.Extensions.SpectatorView.Recording
{
    public class iOSRecordingService : MonoBehaviour,
        IRecordingService
    {
        public void Dispose()
        {
        }

        public void Initialize()
        {
#if UNITY_IOS
            if (!ReplayKit.APIAvailable)
            {
                Debug.LogError("ReplayKit API is not available");
                return;
            }
#else
            Debug.LogError("iOSRecordingService is not supported for the current platform");
#endif
        }

        public bool StartRecording()
        {

#if !UNITY_IOS
            Debug.LogError("iOSRecordingService is not supported for the current platform");
            return false;
#else
            if (!ReplayKit.APIAvailable)
            {
                Debug.LogError("ReplayKit API is not available");
                return false;
            }

            if (ReplayKit.isRecording)
            {
                Debug.LogWarning("ReplayKit was told to start recording but was already recording");
                return true;
            }

            if (ReplayKit.recordingAvailable)
            {
                ReplayKit.Discard();
            }

            return ReplayKit.StartRecording(true, true);
#endif
        }

        public void StopRecording()
        {
#if !UNITY_IOS
            Debug.LogError("iOSRecordingService is not supported for the current platform");
#else
            if (!ReplayKit.APIAvailable)
            {
                Debug.LogError("ReplayKit API is not available");
                return;
            }

            if (!ReplayKit.isRecording)
            {
                Debug.LogWarning("ReplayKit was told to stop recording but wasn't recording");
                return;
            }

            ReplayKit.StopRecording();

            if (ReplayKit.recordingAvailable)
            {
                ReplayKit.Preview();
            }
#endif
        }
    }
}

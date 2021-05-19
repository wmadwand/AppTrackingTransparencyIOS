using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_IOS

using Unity.Advertisement.IosSupport;

#endif

public class AppTrackingTransparencyIOS : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return RequestPermission();

        Debug.Log("SUCCESS !!!");
    }

    public IEnumerator RequestPermission()
    {
#if UNITY_IOS
        var currentStatus = ATTrackingStatusBinding.GetAuthorizationTrackingStatus();
        Debug.Log(string.Format("Current authorization status: {0}", currentStatus.ToString()));

        if (currentStatus != ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED)
        {
            Debug.Log("Requesting authorization...");
            ATTrackingStatusBinding.RequestAuthorizationTracking();
            //_coroutiner.StartCoroutine(RequestCallbackAwaiter(_callback));
        }

        yield return WaitForAuthorized();
#else 
        yield return null;
#endif
    }    

#if UNITY_IOS
    private IEnumerator WaitForAuthorized()
    {
        yield return new WaitUntil(() => ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED);
    }

    private IEnumerator RequestCallbackAwaiter(Action callback)
    {
        yield return new WaitUntil(() => ATTrackingStatusBinding.GetAuthorizationTrackingStatus() == ATTrackingStatusBinding.AuthorizationTrackingStatus.AUTHORIZED);
        callback();
    }
#endif

    public void Request()
    {
#if UNITY_IOS
        ATTrackingStatusBinding.RequestAuthorizationTracking();
#endif
    }
}

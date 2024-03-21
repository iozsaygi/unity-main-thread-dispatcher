using System.Collections.Concurrent;
using UnityEngine;

// ReSharper disable IdentifierTypo

// ReSharper disable InconsistentNaming

[DisallowMultipleComponent]
public class MainThreadDispatcher : MonoBehaviour
{
    // Thread-safe queue for registering received task bundles.
    private readonly ConcurrentQueue<DispatchableTaskBundle> dispatchableTaskBundles = new();

    public void RegisterTaskBundleForDispatching(DispatchableTaskBundle dispatchableTaskBundle)
    {
        dispatchableTaskBundles.Enqueue(dispatchableTaskBundle);
    }

    private void Update()
    {
        if (dispatchableTaskBundles.IsEmpty) return;

        dispatchableTaskBundles.TryDequeue(out var dispatchableTaskBundle);
        dispatchableTaskBundle.Task.Invoke();
    }
}
using System.Threading;
using UnityEngine;

// ReSharper disable InconsistentNaming

public class ThreadedSpriteGenerator
{
    // Execution interval of the thread.
    private readonly byte interval;

    // Actual reference to a thread that will be querying tasks on main thread.
    private Thread thread;

    // Flag to see if our thread is still active.
    private bool isThreadRunning;

    public ThreadedSpriteGenerator(byte interval)
    {
        this.interval = interval;
        isThreadRunning = false;
    }

    public void Begin()
    {
        thread = new Thread(GenerateSpriteOnMainThread);
        isThreadRunning = true;
        thread.Start();
    }

    public void Abort()
    {
        thread.Abort();
        isThreadRunning = false;
    }

    private void GenerateSpriteOnMainThread()
    {
        while (isThreadRunning)
        {
            // Sleep the thread by an 'interval' amount.
            Thread.Sleep(interval * 1000);

            // TODO: Access to 'MonoBehaviour' dispatcher and queue task on main thread.

            // This will result with an exception since we are calling Unity API from another thread.
            var gameObject = new GameObject(nameof(GenerateSpriteOnMainThread));
        }
    }
}
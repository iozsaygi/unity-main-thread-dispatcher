using System.Threading;
using UnityEngine;

// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming

public class ThreadedSpriteGenerator
{
    // Execution interval of the thread.
    private readonly byte interval;
    private readonly MainThreadDispatcher mainThreadDispatcher;

    // Actual reference to a thread that will be querying tasks on main thread.
    private Thread thread;

    // Flag to see if our thread is still active.
    private bool isThreadRunning;

    public ThreadedSpriteGenerator(byte interval, MainThreadDispatcher mainThreadDispatcher)
    {
        this.interval = interval;
        this.mainThreadDispatcher = mainThreadDispatcher;
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

            // Create the actual task bundle that will be queued.
            var dispatchableTaskBundle = new DispatchableTaskBundle(() =>
            {
                // Create the game object.
                var gameObject = new GameObject(nameof(ThreadedSpriteGenerator))
                {
                    transform =
                    {
                        // Randomize the position of game object.
                        position = new Vector2(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f))
                    }
                };

                // Add sprite renderer component.
                var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
            });

            // Queue the created task in main thread.
            mainThreadDispatcher.RegisterTaskBundleForDispatching(dispatchableTaskBundle);
        }
    }
}
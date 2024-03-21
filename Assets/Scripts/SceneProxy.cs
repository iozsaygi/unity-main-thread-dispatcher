using UnityEngine;

// ReSharper disable InconsistentNaming

[DisallowMultipleComponent]
public class SceneProxy : MonoBehaviour
{
    [SerializeField] private MainThreadDispatcher mainThreadDispatcher;

    private ThreadedSpriteGenerator threadedSpriteGenerator;

    private void OnEnable()
    {
        threadedSpriteGenerator = new ThreadedSpriteGenerator(3, mainThreadDispatcher);
        threadedSpriteGenerator.Begin();
    }

    private void OnDisable()
    {
        threadedSpriteGenerator.Abort();
    }
}
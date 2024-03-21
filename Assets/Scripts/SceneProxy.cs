using UnityEngine;

// ReSharper disable InconsistentNaming

[DisallowMultipleComponent]
public class SceneProxy : MonoBehaviour
{
    [SerializeField] private MainThreadDispatcher mainThreadDispatcher;
    [SerializeField] private Sprite sprite;

    private ThreadedSpriteGenerator threadedSpriteGenerator;

    private void OnEnable()
    {
        threadedSpriteGenerator = new ThreadedSpriteGenerator(3, sprite, mainThreadDispatcher);
        threadedSpriteGenerator.Begin();
    }

    private void OnDisable()
    {
        threadedSpriteGenerator.Abort();
    }
}
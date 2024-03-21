using UnityEngine;

// ReSharper disable InconsistentNaming

[DisallowMultipleComponent]
public class SceneProxy : MonoBehaviour
{
    [SerializeField] private MainThreadDispatcher mainThreadDispatcher;

    private ThreadedSpriteGenerator threadedSpriteGenerator;

    private void Start()
    {
        threadedSpriteGenerator = new ThreadedSpriteGenerator(3, mainThreadDispatcher);
    }
}
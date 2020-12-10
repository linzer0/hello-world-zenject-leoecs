using Leopotam.Ecs;
using UnityEngine;

public class HelloWorldSystem : IEcsInitSystem
{
    public void Init()
    {
        PrintText("Hello World");
    }

    private void PrintText(string text)
    {
        Debug.Log(text);
    }
}
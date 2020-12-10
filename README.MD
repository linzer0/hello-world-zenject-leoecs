# How to write Hello World using LeoEcs & Zenject

## Preparation
Firstly, we need to install [Zenject](https://github.com/modesttree/Zenject) and [LeoEcs](https://github.com/Leopotam/ecs).
*LeoEcs has two versions, one based on structures and one based on classes. 
Within the framework of this tutorial, it makes no difference which one to use, I will use the version on structures.*

## Usage
After installation, we need to create **Scene Context** in our Scene. 
Right Click inside the Hierarchy tab and select `Zenject -> Scene Context`

[![Create-Scene-Context.png](https://i.postimg.cc/vTymBgmk/Create-Scene-Context.png)](https://postimg.cc/S2Z41KCf)

Now, create three scripts: `StartupInstaller`, `StartupSystemsExecutor`, `HelloWorldSystem`.

**StartupInstaller** will bind our Systems and EcsWorld to DI-container.
```
using Leopotam.Ecs;
using Zenject;

public class StartupInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInstance(new EcsWorld());

        Container.Bind<HelloWorldSystem>().AsSingle();

        Container.BindInterfacesAndSelfTo<StartupSystemsExecutor>()
            .AsSingle()
            .NonLazy();
    }
}
```

**StartupSystemsExecutor** will `Init()` and `Run()` binded systems.

```
using System;
using Leopotam.Ecs;
using Zenject;

public class StartupSystemsExecutor : IDisposable, ITickable
{
    private readonly EcsSystems _systems;
    private EcsWorld _world;

    public StartupSystemsExecutor(EcsWorld ecsWorld, HelloWorldSystem helloWorldSystem)
    {
        _systems = new EcsSystems(ecsWorld);
        _world = ecsWorld;

        _systems.Add(helloWorldSystem);
        
        _systems.Init();
    }
    
    public void Dispose()
    {
        _systems.Destroy();
        _world.Destroy();
    }

    public void Tick()
    {
        _systems.Run();
    }
}
```

**HelloWorldSystem** will print "Hello World" in Console.
```
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
```

We need to add `StartupInstaller` to **SceneContext** which we created on scene.
[![02-Startup-installer-added-to-scene-context.png](https://i.postimg.cc/FsfycwFZ/02-Startup-installer-added-to-scene-context.png)](https://postimg.cc/6yX4xjv2)

And click Run
[![Console-output.png](https://i.postimg.cc/Qx0M8MZY/Console-output.png)](https://postimg.cc/YhL78M9Q)

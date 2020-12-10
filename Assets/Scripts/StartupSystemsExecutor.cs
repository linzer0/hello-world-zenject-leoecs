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

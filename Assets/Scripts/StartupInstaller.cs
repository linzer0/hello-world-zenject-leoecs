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

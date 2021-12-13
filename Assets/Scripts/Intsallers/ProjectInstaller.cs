using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    public SomeManager SomeManager;
    public override void InstallBindings()
    {
        Container.Bind<SomeManager>().FromInstance(SomeManager).AsSingle();
    }
}
using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller
{
    [SerializeField]public SoundManager SoundManager;
    public override void InstallBindings()
    {
        Container.Bind<SoundManager>().FromInstance(SoundManager).AsSingle();
    }
}
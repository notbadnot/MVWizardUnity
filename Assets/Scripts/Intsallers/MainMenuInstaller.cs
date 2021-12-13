using UnityEngine;
using Zenject;

public class MainMenuInstaller : MonoInstaller
{
    [SerializeField] public MMenuView MMenuView;
    public override void InstallBindings()
    {
        Container.Bind<PrefabFactory>().AsSingle();
        Container.Bind<MMenuView>().FromInstance(MMenuView).AsSingle();

    }
}
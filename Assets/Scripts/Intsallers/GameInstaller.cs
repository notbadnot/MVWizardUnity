using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public HandlerOfPlayerModels HandlerOfPlayerModels;
    public InGameUIManager InGameUIManager;
    public GameMainFrameView GameMainFrameView;
    public override void InstallBindings()
    {
        Container.Bind<PrefabFactory>().AsSingle();
        Container.Bind<HandlerOfPlayerModels>().FromInstance(HandlerOfPlayerModels).AsSingle().NonLazy();
        Container.Bind<InGameUIManager>().FromInstance(InGameUIManager).AsSingle();
        Container.Bind<GameMainFrameView>().FromInstance(GameMainFrameView).AsSingle();
    }
}
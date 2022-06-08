using RushingMachine.Controllers.Background;
using RushingMachine.Controllers.Enemies;
using RushingMachine.Controllers.Player;
using RushingMachine.Controllers.UI;
using RushingMachine.Controllers.UserInput;
using RushingMachine.Data;
using UnityEngine;

namespace RushingMachine.Controllers.Game
{
    internal sealed class GameInitialization
    {
        public GameInitialization(CompositeController controllers, GameConfig data)
        {
            Camera camera = Camera.main;
            
            var inputInitialization = new InputInitialization();
            var playerInitialization = new PlayerInitialization(data.playerConfig);
            var policeInitialization = new PoliceInitialization(data.policeConfig);
            
            var trafficInitialization = new TrafficInitialization(data.trafficConfig, data.worldSpeed);
            
            var uiInitialization = new UiInitialization(data.uiConfig);
            
            // var levelInitialization = new LevelInitialization();
            //
            // var player = playerInitialization.GetPlayer();
            // var level = levelInitialization.Level;
            //
            // controllers.Add(new GeneratorLevelController(level.GenerateLevelView));
            // controllers.Add(levelInitialization.WaterAnimator); 
            //
            // controllers.Add(new PlayerAnimationController(data.playerConfig, player));
            // controllers.Add(new PlayerRigidbodyController(data.playerConfig, player));
            // controllers.Add(new CannonAimController(level.CanonView.MuzzleTransform, player.Transform));
            // controllers.Add(new BulletEmitterController(level.CanonView.BulletViews, level.CanonView.EmmiterTransform));
            // controllers.Add(new CoinsController(player, level.CoinViews, new SpriteAnimator(data.coinAnimationsConfig)));
            // controllers.Add(new LevelCompleteManager(player, level.DeathZones, level.WinZones));
            
            controllers.Add(new CameraController(camera));
            controllers.Add(new BackgroundController(data.back, data.worldSpeed));
            controllers.Add(inputInitialization);
            controllers.Add(playerInitialization);
            controllers.Add(trafficInitialization);
            controllers.Add(new InputController(inputInitialization.GetInput()));
            controllers.Add(new PlayerMoveController(playerInitialization.Move, inputInitialization.GetInput()));
            controllers.Add(new PlayerMoveController(policeInitialization.Move, inputInitialization.GetInput()));
            controllers.Add(new TrafficMoveController(trafficInitialization.GetTrafficMove()));
            controllers.Add(new ScoreController(uiInitialization.Score));

        }
    }
}
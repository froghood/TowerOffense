using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TowerOffense.Objects.Base;
using TowerOffense.Objects.Common;
using TowerOffense.Scenes.Gameplay.Objects.Shop;
using TowerOffense.Scenes.Gameplay.Objects;
using TowerOffense.Objects.Enemies;

namespace TowerOffense.Scenes.Gameplay.Objects {
    public class WaveManager : SceneWindow {

        public int Wave { get => _wave; }

        private EntityManager _entityManager;

        private int _wave = 0;
        private double _time = 0;

        private JObject _wavesJson;

        private List<SpawnGroup> _spawnGroups = new();
        private List<Portal> _portals = new();

        private Random _random = new();

        private bool _waveInProgress;


        public WaveManager(Scene scene, EntityManager entityManager, string wavesJsonPath, Vector2 position, Point size, int titleBarHeight = 24, int borderThickness = 1) : base(scene, size, position, titleBarHeight, borderThickness) {
            _entityManager = entityManager;
            string wavesJsonRaw = File.ReadAllText(wavesJsonPath);
            _wavesJson = JObject.Parse(wavesJsonRaw);
        }

        public override void Update(GameTime gameTime) {

            foreach (var spawnGroup in _spawnGroups) {
                spawnGroup.Update(gameTime);
                foreach (var spawn in spawnGroup.DequeueSpawns()) {

                    int index = _random.Next(_portals.Count);

                    var portal = _portals[index];

                    var enemy = (spawn) switch {
                        "TestEnemy" => _entityManager.CreateEnemy<SpiderEnemy>(portal.GetSpawnPosition(), true),
                        _ => throw new Exception(),
                    };

                    Scene.AddObject(enemy);
                }
            }

            if (_waveInProgress && _spawnGroups.All(group => group.Finished)) {
                foreach (var portal in _portals) {
                    portal.Close();
                }

                if (_entityManager.RemainingEnemies == 0) {
                    _waveInProgress = false;
                    OpenShop();
                }
            }

            _portals.RemoveAll(portal => portal.IsDestroyed);
            base.Update(gameTime);
        }

        public override void Render(GameTime gameTime) {
            base.Render(gameTime);
        }

        public void OpenShop() {
            var _shopWindow = new ShopWindow(Scene, _entityManager, this, new Vector2(300, 300), new Point(360, 240));
            _shopWindow.Closed += (_, _) => {
                NextWave();
            };
            Scene.AddObject(_shopWindow);
        }

        public void NextWave() {

            if (_wave + 1 > _wavesJson.Count) return; // return if no more waves

            _wave++;
            _time = 0;
            _waveInProgress = true;



            var currentWaveJson = _wavesJson[$"{_wave}"];
            int numPortals = int.Parse(currentWaveJson["Portals"].ToString());

            for (int i = 0; i < numPortals; i++) {
                var portal = new Portal(Scene);
                _portals.Add(portal);
                Scene.AddObject(portal);
            }

            _spawnGroups = JsonConvert.DeserializeObject<List<SpawnGroup>>(currentWaveJson["SpawnGroups"].ToString());
        }
    }
}
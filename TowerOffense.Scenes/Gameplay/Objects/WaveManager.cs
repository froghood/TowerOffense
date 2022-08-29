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
using Microsoft.Xna.Framework.Graphics;

namespace TowerOffense.Scenes.Gameplay.Objects {
    public class WaveManager : TowerOffense.Objects.Base.SceneWindow {

        public int Wave { get => _wave; }

        private EntityManager _entityManager;

        private int _wave = 1;
        private double _time = 0;

        private JObject _wavesJson;

        private List<SpawnGroup> _spawnGroups = new();
        private List<Portal> _portals = new();

        private Random _random = new();

        private bool _waveInProgress;


        public WaveManager(Scene scene, EntityManager entityManager, string wavesJsonPath)
        : base(scene, new Point(160, 40), new Vector2(TOGame.DisplaySize.X / 2 - 160, 24)) {

            Position -= Vector2.UnitX * Size.X;

            ClearColor = new Color(30, 30, 40);
            TitleBarColor = Color.White;
            FocusedBorderColor = TitleBarColor;
            BorderColor = new Color(128, 128, 128);

            // ClearColor = new Color(104, 66, 74);
            // TitleBarColor = new Color(186, 120, 93);
            // FocusedBorderColor = TitleBarColor;
            // BorderColor = new Color();

            _entityManager = entityManager;
            string wavesJsonRaw = File.ReadAllText(wavesJsonPath);
            _wavesJson = JObject.Parse(wavesJsonRaw);
        }

        public override void Update(GameTime gameTime) {

            foreach (var spawnGroup in _spawnGroups) {
                spawnGroup.Update(gameTime);
                foreach (var spawn in spawnGroup.DequeueSpawns()) {

                    int index = TOGame.Random.Next(_portals.Count);

                    var portal = _portals[index];

                    Enemy enemy = (spawn) switch {
                        "Spider" => _entityManager.CreateEnemy<Spider>(portal.GetSpawnPosition(), true),
                        "Beetle" => _entityManager.CreateEnemy<Beetle>(portal.GetSpawnPosition(), true),
                        "Worm" => _entityManager.CreateEnemy<Worm>(5, portal.GetSpawnPosition(), true),
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
                    if (_wave >= 10) {

                    } else {
                        _wave++;
                        OpenShop();
                    }

                }
            }

            _portals.RemoveAll(portal => portal.IsDestroyed);
            base.Update(gameTime);
        }

        public override void Render(GameTime gameTime) {

            var spriteFont = TOGame.Instance.Content.Load<SpriteFont>("Fonts/MilkyNice");
            string text = $"wave {_wave}";
            var fontSize = spriteFont.MeasureString(text);

            TOGame.SpriteBatch.DrawString(spriteFont,
                text,
                InnerWindowCenterOffset,
                Color.White,
                0f,
                fontSize / 2f,
                1f,
                SpriteEffects.None,
                0f);

            // TOGame.SpriteBatch.DrawString(
            //     spriteFont,
            //     text, InnerWindowCenterOffset - fontSize / 2f, new Color(40, 25, 43));



            base.Render(gameTime);
        }

        public void OpenShop() {
            var _shopWindow = new Shop.Shop(Scene, _entityManager, this);
            _shopWindow.Closed += (_, _) => {
                NextWave();
                Show();
            };
            Scene.AddObject(_shopWindow);
        }

        public void NextWave() {

            if (_wave > _wavesJson.Count) return; // return if no more waves

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
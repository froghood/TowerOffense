using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TowerOffense.Objects.Base;
using TowerOffense.Objects.Common;

namespace TowerOffense.Scenes.Gameplay.Objects {
    public class WaveManager : SceneWindow {


        public int Wave { get => _wave; }

        private EntityManager _entityManager;

        private int _wave = 0;
        private double _time = 0;

        private JObject _wavesJson;

        private List<SpawnGroup> _spawnGroups = new();


        public WaveManager(Scene scene, EntityManager entityManager, string wavesJsonPath, Point position, Point size, int titleBarHeight = 24, int borderThickness = 1) : base(scene, position, size, titleBarHeight, borderThickness) {
            _entityManager = entityManager;
            string wavesJsonRaw = File.ReadAllText(wavesJsonPath);
            System.Console.WriteLine(wavesJsonRaw);
            _wavesJson = JObject.Parse(wavesJsonRaw);
        }

        public override void Update(GameTime gameTime) {
            foreach (var spawnGroup in _spawnGroups) {
                spawnGroup.Update(gameTime);
                foreach (var spawn in spawnGroup.DequeueSpawns()) {
                    Scene.AddObject(_entityManager.CreateEnemyFromString(spawn, new Point(400, 400)));
                }
            }

            base.Update(gameTime);
        }

        public override void Render(GameTime gameTime) {
            base.Render(gameTime);
        }

        internal void NextWave() {
            _wave++;
            _time = 0;
            System.Console.WriteLine();
            _spawnGroups = JsonConvert.DeserializeObject<List<SpawnGroup>>(_wavesJson[_wave.ToString()].ToString());
        }
    }
}
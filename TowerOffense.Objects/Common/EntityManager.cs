using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using TowerOffense.Objects.Base;
using TowerOffense.Objects.Enemies;
using TowerOffense.Scenes;

namespace TowerOffense.Objects.Common {
    public class EntityManager : SceneObject {

        private List<Tower> _towers;
        private List<Enemy> _enemies;

        public EntityManager(Scene scene) : base(scene) {
            _towers = new List<Tower>();
            _enemies = new List<Enemy>();
        }

        public Enemy CreateEnemyFromString(string enemyTypeString, params object[] args) {
            Type type = (enemyTypeString) switch {
                "TestEnemy" => typeof(TestEnemy),
                // TODO: fill with more enemies
                _ => null
            };

            object[] enemyArgs = { Scene, this };
            var enemy = (Enemy)Activator.CreateInstance(type, enemyArgs.Concat(args).ToArray());
            _enemies.Add(enemy);
            return enemy;
        }

        public override void Update(GameTime gameTime) {
            _towers.RemoveAll(obj => obj.IsDestroyed);
            _enemies.RemoveAll(obj => obj.IsDestroyed);
        }
    }
}
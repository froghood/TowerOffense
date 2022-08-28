using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using TowerOffense.Objects.Base;
using TowerOffense.Objects.Enemies;
using TowerOffense.Scenes;

namespace TowerOffense.Objects.Common {
    public class EntityManager : SceneObject {

        public int RemainingEnemies { get => _enemies.Count; }

        private List<Tower> _towers;
        private List<Enemy> _enemies;

        public EntityManager(Scene scene) : base(scene) {
            _towers = new List<Tower>();
            _enemies = new List<Enemy>();
        }

        public Enemy CreateEnemy<T>(params object[] e) where T : Enemy {
            var args = new object[] { Scene, this }.Concat(e).ToArray();
            var enemy = (T)Activator.CreateInstance(typeof(T), args);
            _enemies.Add(enemy);
            return enemy;
        }

        public Tower CreateTower<T>(params object[] e) where T : Tower {
            var args = new object[] { Scene, this }.Concat(e).ToArray();
            var tower = (T)Activator.CreateInstance(typeof(T), args);
            _towers.Add(tower);
            return tower;
        }

        public IEnumerable<Enemy> GetEnemies() {
            return _enemies.Where(enemy => !enemy.IsDestroyed);
        }

        public override void Update(GameTime gameTime) {
            _towers.RemoveAll(obj => obj.IsDestroyed);
            _enemies.RemoveAll(obj => obj.IsDestroyed);
        }
    }
}
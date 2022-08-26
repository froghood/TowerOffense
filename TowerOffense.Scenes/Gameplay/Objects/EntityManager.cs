using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using TowerOffense.Objects.Base;
using TowerOffense.Objects.Common;

namespace TowerOffense.Scenes.Gameplay.Objects {
    public class EntityManager : SceneObject {

        private List<Tower> _towers;
        private List<Enemy> _enemies;

        public EntityManager(Scene scene) : base(scene) {
            _towers = new List<Tower>();
            _enemies = new List<Enemy>();
        }

        public T CreateEntity<T>(params object[] args) where T : Entity {
            var entity = (T)Activator.CreateInstance(typeof(T), new[] { this }.Concat(args));
            if (typeof(T).IsSubclassOf(typeof(Tower))) _towers.Add(entity as Tower);
            if (typeof(T).IsSubclassOf(typeof(Enemy))) _enemies.Add(entity as Enemy);
            return entity;
        }

        public override void Update(GameTime gameTime) {

        }
    }
}
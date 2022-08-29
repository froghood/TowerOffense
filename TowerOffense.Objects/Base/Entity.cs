using System;
using Microsoft.Xna.Framework;
using TowerOffense.Objects.Base;
using TowerOffense.Scenes;

namespace TowerOffense.Objects.Common {
    public class Entity : SceneWindow {

        protected readonly EntityManager _entityManager;

        public Entity(
            Scene scene,
            EntityManager entityManager,
            Point size,
            Vector2? position = null) : base(
                scene,
                size,
                position) {
            _entityManager = entityManager;
        }
    }
}
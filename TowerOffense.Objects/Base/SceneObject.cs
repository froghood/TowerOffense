using Microsoft.Xna.Framework;
using TowerOffense.Scenes;

namespace TowerOffense.Objects.Base {
    public abstract class SceneObject {

        public bool IsDestroyed { get; private set; }

        protected Scene Scene;

        public SceneObject(Scene scene) {
            Scene = scene;
        }

        public virtual void Destroy() {
            IsDestroyed = true;
        }

        public abstract void Update(GameTime gameTime);
    }
}
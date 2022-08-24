using Microsoft.Xna.Framework;

namespace TowerOffense.Scenes.Objects {
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
using Microsoft.Xna.Framework;

namespace TowerOffense.Objects.Base {
    public abstract class WindowObject {

        public SceneWindow SceneWindow { get => _sceneWindow; }
        private SceneWindow _sceneWindow;

        public WindowObject(SceneWindow sceneWindow) {
            _sceneWindow = sceneWindow;
        }

        public virtual void Update(GameTime gameTime) { }
        public virtual void Render(GameTime gameTime) { }
    }
}
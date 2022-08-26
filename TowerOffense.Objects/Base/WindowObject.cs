using Microsoft.Xna.Framework;

namespace TowerOffense.Objects.Base {
    public abstract class WindowObject {

        public SceneWindow Window { get => _window; }
        private SceneWindow _window;

        public WindowObject(SceneWindow sceneWindow) {
            _window = sceneWindow;
        }

        public virtual void Update(GameTime gameTime) { }
        public virtual void Render(GameTime gameTime) { }
    }
}
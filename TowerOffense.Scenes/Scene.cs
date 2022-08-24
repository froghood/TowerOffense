using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerOffense.Scenes.Objects;
using TowerOffense.Window;
using System.Reflection;
using System.Linq;

namespace TowerOffense.Scenes {

    public class Scene {

        private List<SceneObject> _sceneObjects = new();
        private List<WindowObject> _windowObjects = new();

        public void AddObject<T>(T sceneObject) where T : SceneObject {
            _sceneObjects.Add(sceneObject);
            if (typeof(T).IsSubclassOf(typeof(WindowObject))) {
                _windowObjects.Add(sceneObject as WindowObject);
            }
        }

        public void Update(GameTime gameTime) {
            foreach (var sceneObject in _sceneObjects.Where(obj => !obj.IsDestroyed)) {
                sceneObject.Update(gameTime);
            }

            _sceneObjects.RemoveAll(obj => obj.IsDestroyed);
            _windowObjects.RemoveAll(obj => obj.IsDestroyed);
        }

        public void Render(GameTime gameTime) {
            var graphicsDevice = TOGame.Instance.GraphicsDevice;
            foreach (var windowObject in _windowObjects) {
                graphicsDevice.SetRenderTarget(windowObject.RenderTarget);
                graphicsDevice.Clear(windowObject.ClearColor);

                TOGame.SpriteBatch.Begin(SpriteSortMode.BackToFront);
                windowObject.Render(gameTime);
                TOGame.SpriteBatch.End();

                windowObject.RenderTarget.Present();
            }
        }

        public virtual void Init() { }
    }
}
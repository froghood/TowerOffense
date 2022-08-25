using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerOffense.Objects.Base;
using System.Reflection;
using System.Linq;

namespace TowerOffense.Scenes {

    public class Scene {

        private List<SceneObject> _sceneObjects = new();
        private List<SceneWindow> _sceneWindows = new();

        public void AddObject<T>(T sceneObject) where T : SceneObject {
            _sceneObjects.Add(sceneObject);
            if (typeof(T).IsSubclassOf(typeof(SceneWindow))) {
                _sceneWindows.Add(sceneObject as SceneWindow);
            }
        }

        public void AddObjects<T>(IEnumerable<T> _sceneObjects) where T : SceneObject {
            foreach (var sceneObject in _sceneObjects) AddObject(sceneObject);
        }

        public void Update(GameTime gameTime) {
            foreach (var sceneObject in _sceneObjects.Where(obj => !obj.IsDestroyed)) {
                sceneObject.Update(gameTime);
            }

            _sceneObjects.RemoveAll(obj => obj.IsDestroyed);
            _sceneWindows.RemoveAll(obj => obj.IsDestroyed);
        }

        public void Render(GameTime gameTime) {
            var graphicsDevice = TOGame.Instance.GraphicsDevice;
            foreach (var windowObject in _sceneWindows) {
                graphicsDevice.SetRenderTarget(windowObject.RenderTarget);
                graphicsDevice.Clear(windowObject.ClearColor);

                TOGame.SpriteBatch.Begin(SpriteSortMode.BackToFront);
                windowObject.Render(gameTime);
                TOGame.SpriteBatch.End();

                windowObject.RenderTarget.Present();
            }
        }

        public virtual void Initialize() { }
        public virtual void Reactivate() { }
        public virtual void Deactivate() { }
    }
}
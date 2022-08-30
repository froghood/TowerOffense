using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerOffense.Objects.Base;
using System.Reflection;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace TowerOffense.Scenes {

    public class Scene {

        public List<SceneWindow> SceneWindows { get => _sceneWindows; }

        private List<SceneObject> _sceneObjects = new();
        private List<SceneWindow> _sceneWindows = new();
        private bool _isUpdating;

        public void AddObject<T>(T sceneObject) where T : SceneObject {

            var addAction = () => {
                _sceneObjects.Add(sceneObject);
                if (typeof(T).IsSubclassOf(typeof(SceneWindow))) {
                    _sceneWindows.Add(sceneObject as SceneWindow);
                    (sceneObject as SceneWindow).Form.TopMost = true;
                }
            };

            // add next tick if added from update chain
            if (_isUpdating) TOGame.Command(addAction);
            else addAction();
        }

        public void AddObjects<T>(IEnumerable<T> _sceneObjects) where T : SceneObject {
            foreach (var sceneObject in _sceneObjects) AddObject(sceneObject);
        }

        public void Update(GameTime gameTime) {
            _isUpdating = true;

            var focusedWindow = _sceneWindows.FirstOrDefault(win => win.Focused);
            if (focusedWindow != null) {
                _sceneWindows.Remove(focusedWindow);
                _sceneWindows.Add(focusedWindow);
            }

            foreach (var sceneObject in _sceneObjects.Where(obj => !obj.IsDestroyed)) {
                sceneObject.Update(gameTime);
            }

            _sceneObjects.RemoveAll(obj => obj.IsDestroyed);
            _sceneWindows.RemoveAll(obj => obj.IsDestroyed);

            _isUpdating = false;
        }

        public void Render(GameTime gameTime) {

            var graphicsDevice = TOGame.Instance.GraphicsDevice;

            foreach (var windowObject in _sceneWindows) {
                graphicsDevice.SetRenderTarget(windowObject.RenderTarget);
                graphicsDevice.Clear(windowObject.ClearColor);

                TOGame.SpriteBatch.Begin(SpriteSortMode.Deferred, samplerState: SamplerState.LinearClamp);
                windowObject.Render(gameTime);
                TOGame.SpriteBatch.End();

                windowObject.RenderTarget.Present();
            }
        }

        public virtual void Initialize() { }
        public virtual void Reactivate() { }
        public virtual void Deactivate() { }
        public virtual void Terminate() { }
    }
}
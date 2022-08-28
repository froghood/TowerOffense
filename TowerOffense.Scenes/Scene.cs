using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TowerOffense.Objects.Base;
using System.Reflection;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace TowerOffense.Scenes {

    public class Scene {

        private List<SceneObject> _sceneObjects = new();
        private List<SceneWindow> _sceneWindows = new();
        private bool _isUpdating;

        public void AddObject<T>(T sceneObject) where T : SceneObject {

            var addAction = () => {
                _sceneObjects.Add(sceneObject);
                if (typeof(T).IsSubclassOf(typeof(SceneWindow))) {
                    _sceneWindows.Add(sceneObject as SceneWindow);
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

            bool isOverlappingWindow = false;

            var focusedWindow = _sceneWindows.FirstOrDefault(win => win.Form.Focused, null);
            if (focusedWindow != null) {
                _sceneWindows.Remove(focusedWindow);
                _sceneWindows.Add(focusedWindow);
            }

            foreach (var windowObject in _sceneWindows.Where(obj => !obj.IsDestroyed).Reverse()) {
                var mouseState = Mouse.GetState(windowObject.Window);
                windowObject.UpdateMouseState(mouseState, true);
                if (!isOverlappingWindow) isOverlappingWindow = windowObject.UpdateMouseHovering();
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
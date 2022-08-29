using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace TowerOffense.Scenes {
    public class SceneManager {

        public Scene CurrentScene { get => _scenes.Count > 0 ? _scenes.Peek() : null; }
        private Stack<Scene> _scenes = new Stack<Scene>();

        public SceneManager() { }

        public void PushScene<T>(params object[] args) where T : Scene {
            TOGame.Command(() => {
                CurrentScene?.Deactivate();
                _scenes.Push((T)Activator.CreateInstance(typeof(T), args));
                CurrentScene.Initialize();
            });
        }

        public void PopScene() {
            TOGame.Command(() => {
                CurrentScene.Terminate();
                _scenes.Pop();
                CurrentScene.Reactivate();
            });
        }
    }
}
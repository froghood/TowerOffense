using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace TowerOffense.Scenes {
    public class SceneManager {

        public Scene CurrentScene { get => _scenes.Peek(); }
        private Stack<Scene> _scenes = new Stack<Scene>();

        public SceneManager() { }

        public void PushScene<T>(params object[] args) where T : Scene {
            TOGame.Command(() => {
                _scenes.Push((T)Activator.CreateInstance(typeof(T), args));
                CurrentScene.Init();
            });
        }

        public void PopScene() {
            TOGame.Command(() => _scenes.Pop());
        }
    }
}
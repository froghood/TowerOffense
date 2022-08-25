using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TowerOffense.Window;

namespace TowerOffense.Scenes.ExampleScene.Objects {
    public class ExampleWindow : WindowObject {
        public ExampleWindow(Scene scene, int width, int height) : base(scene, width, height) { }
        public override void Render(GameTime gameTime) { }
        public override void Update(GameTime gameTime) {

            if (Keyboard.GetState().IsKeyDown(Keys.Space)) {
                System.Console.WriteLine("t");
                Form.ClientSize = new(Form.ClientSize.Width - 1, Form.ClientSize.Height - 1);

            }

            //System.Console.WriteLine($"FormClientSize: {Form.ClientSize}, {Form.Size}, {Window.ClientBounds}");
        }
    }
}
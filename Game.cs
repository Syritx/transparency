using System;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK;


namespace Transparent_Objects 
{
    public class Game : GameWindow
    {
        public Game(int width, int height, string title)
            : base(width, height, GraphicsMode.Default, title) {
            start();
        }

        void start() {
            GL.Translate(0, 0, -5);
            RenderFrame += render;
            Resize += resize;
            Load += load;

            Run(60);
        }

        private void load(object sender, EventArgs e) {
            GL.ClearColor(0, 0, 0, 0);
            GL.Enable(EnableCap.DepthTest);

            // enabling transparency
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.Src1Alpha, BlendingFactor.OneMinusSrcAlpha);
        }

        private void resize(object sender, EventArgs e) {
            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            Matrix4 perspectiveMatrix =
                Matrix4.CreatePerspectiveFieldOfView(1, Width / Height, 1.0f, 1000.0f);
            GL.LoadMatrix(ref perspectiveMatrix);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.End();
        }

        private void render(object sender, FrameEventArgs e) {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);


            // Opaque Triangle
            GL.Begin(BeginMode.Triangles);

            GL.Color3(1.0, 1.0, 1.0);
            GL.Vertex2(0, 1);
            GL.Vertex2(-1, -1);
            GL.Vertex2(1, -1);

            GL.End();

            double offsetX = .5;
            double offsetY = -.5;

            // Transparent Triangle
            GL.Begin(BeginMode.Triangles);

            // Transparency (Color4) => args: r,g,b,opacity
            GL.Color4(1.0, 0.0, 0.0,0.6);
            GL.Vertex3(0+offsetX, 1 + offsetY, .5);
            GL.Vertex3(-1 + offsetX, -1 + offsetY, .5);
            GL.Vertex3(1 + offsetX, -1 + offsetY, .5);

            GL.End();

            SwapBuffers();
        }
    }
}

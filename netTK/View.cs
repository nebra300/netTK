using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace netTK
{
    public class View
    {
        private Vector2 _position;
        private double _rotation;
        private double _zoom;

        public View()
        {
            _position = new Vector2(0, 0);
            _zoom = 1;
            _rotation = 0;
        }

        public void SetOrthographicProjection(int width, int height)
        {
            GL.Viewport(0, 0, width, height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.Ortho(-(double)width / 2, (double)width / 2, -(double)height / 2, (double)height / 2, -1, 1);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        public void SetZoom(double zoom)
        {
            if (zoom < 0.1 || zoom > 10) return;
            this._zoom = zoom;
        }
        public void ZoomIn(double zoomIn)
        {
            this._zoom += zoomIn;
            if (this._zoom > 10) this._zoom = 10;
        }
        public void ZoomOut(double zoomOut)
        {
            this._zoom -= zoomOut;
            if (this._zoom < 0.1) this._zoom = 0.1;
        }
        public void Move(int x, int y)
        {
            this._position.X += (float)x;
            this._position.Y += (float)y;
        }
        public void PatchMove(int x, int y)
        {
            Move(x * netTK.PatchWidth, y * netTK.PatchHeight);
        }
        public void LookAt(int x, int y)
        {
            this._position.X = (float)x;
            this._position.Y = (float)y;
        }
        public void LookAt(Point position)
        {
            LookAt(position.X, position.Y);
        }  
        public void LookAtPatch(int patchX, int patchY)
        {
            LookAt(patchX * netTK.PatchWidth, patchY * netTK.PatchHeight);
        }
        public void LookAtPatch(Point patchPosition)
        {
            LookAtPatch(patchPosition.X, patchPosition.Y);
        }
        public void LookAtPatch(Patch patch)
        {
            LookAtPatch(patch.X, patch.Y);
        }
        public void Rotate(double degrees)
        {
            this._rotation += degrees * Math.PI / 180;
            if (this._rotation > 360 || this._rotation < 0) this._rotation = this._rotation % 360;
        }
        public void SetRotation(double degrees)
        {
            this._rotation = degrees * Math.PI / 180;
            if (this._rotation > 360 || this._rotation < 0) this._rotation = this._rotation % 360;
        }

        public void ApplyTransform()
        {
            GL.MatrixMode(MatrixMode.Modelview);
            Matrix4 transform = Matrix4.Identity;

            transform = Matrix4.Mult(transform, Matrix4.CreateTranslation(-_position.X, -_position.Y, 0));
            transform = Matrix4.Mult(transform, Matrix4.CreateRotationZ(-(float)_rotation));
            transform = Matrix4.Mult(transform, Matrix4.CreateScale((float)_zoom, (float)_zoom, 1.0f));

            GL.LoadMatrix(ref transform);
        }
    }
}

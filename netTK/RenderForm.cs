using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace netTK
{
    public partial class RenderForm : Form
    {
        public RenderForm()
        {
            InitializeComponent();
        }

        // mouse actions:
        private bool LMousePressed;
        private int xPos, yPos;

        private void renderCanvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LMousePressed = true;
                xPos = e.X;
                yPos = e.Y;
            }
        }
        private void renderCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (LMousePressed)
            {
                netTK.ViewCam.Move(-(e.X - xPos), (e.Y - yPos));
                xPos = e.X;
                yPos = e.Y;
                renderCanvas.Invalidate();
            }

            Point worldCoords = ConvertCanvasToWorldCoords(e.X, e.Y);
            Point patchCoords = new Point();
            patchCoords.X = (int)(Math.Round((double)worldCoords.X / netTK.PatchWidth));
            patchCoords.Y = (int)(Math.Round((double)worldCoords.Y / netTK.PatchHeight));
            if (patchCoords.X > netTK.MaxPXcor || patchCoords.X < netTK.MinPXcor || patchCoords.Y > netTK.MaxPYcor || patchCoords.Y < netTK.MinPYcor)
            {
                txtXcor.Text = "X";
                txtYcor.Text = "Y";
            }
            else
            {
                txtXcor.Text = patchCoords.X.ToString();
                txtYcor.Text = patchCoords.Y.ToString();
            }
        }
        private void renderCanvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                LMousePressed = false;
            }
        }

        // trackbar zoom:
        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            netTK.ViewCam.SetZoom((double)trackBar1.Value / 10);
            renderCanvas.Invalidate();
        }

        // checkboxes:
        private void chkAxes_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAxes.Checked)
                netTK.ShowAxes = true;
            else
                netTK.ShowAxes = false;

            renderCanvas.Invalidate();
        }
        private void chkGrid_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGrid.Checked)
                netTK.ShowGrid = true;
            else
                netTK.ShowGrid = false;

            renderCanvas.Invalidate();
        }
        private void chkPatchBorders_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPatchBorders.Checked)
                netTK.ShowPatchBorders = true;
            else
                netTK.ShowPatchBorders = false;

            renderCanvas.Invalidate();
        }

        // functions:
        public static Point ConvertCanvasToWorldCoords(int x, int y)
        {
            int[] viewport = new int[4];
            Matrix4 modelViewMatrix, projectionMatrix;

            GL.GetFloat(GetPName.ModelviewMatrix, out modelViewMatrix);
            GL.GetFloat(GetPName.ProjectionMatrix, out projectionMatrix);
            GL.GetInteger(GetPName.Viewport, viewport);

            Vector2 mouse;
            mouse.X = x;
            mouse.Y = y;

            Vector4 vector = UnProject(ref projectionMatrix, modelViewMatrix, new Size(viewport[2], viewport[3]), mouse);
            Point coords = new Point((int)vector.X, (int)vector.Y);

            return coords;
        }
        public static Vector4 UnProject(ref Matrix4 projection, Matrix4 view, Size viewport, Vector2 mouse)
        {
            Vector4 vec;

            vec.X = 2.0f * mouse.X / (float)viewport.Width - 1;
            vec.Y = -(2.0f * mouse.Y / (float)viewport.Height - 1);
            vec.Z = 0;
            vec.W = 1.0f;

            Matrix4 viewInv = Matrix4.Invert(view);
            Matrix4 projInv = Matrix4.Invert(projection);

            Vector4.Transform(ref vec, ref projInv, out vec);
            Vector4.Transform(ref vec, ref viewInv, out vec);

            if (vec.W > float.Epsilon || vec.W < float.Epsilon)
            {
                vec.X /= vec.W;
                vec.Y /= vec.W;
                vec.Z /= vec.W;
            }

            return vec;
        }

        // buttons:
        private void btnSnapToCenter_Click(object sender, EventArgs e)
        {
            netTK.ViewCam.LookAtPatch(0, 0);
            renderCanvas.Invalidate();
        }

        // resize:
        private void renderCanvas_Resize(object sender, EventArgs e)
        {
            renderCanvas.Width = renderCanvas.Height;
            GL.Viewport(renderCanvas.DisplayRectangle.X, renderCanvas.DisplayRectangle.Y, renderCanvas.Width, renderCanvas.Height);
        }


//________________________________________________________IMPORTANT_________________________________________________________________


        // crtanje:
        private void renderCanvas_Paint(object sender, PaintEventArgs e)
        {
            netTK.Render(ref renderCanvas);
        }

        // on load:
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.Enable(EnableCap.ColorMaterial);
            GL.Enable(EnableCap.Texture2D);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);

            netTK.ViewCam.SetOrthographicProjection(renderCanvas.Width, renderCanvas.Height);

            ProgramForm form = new ProgramForm(this);
            form.Show();
        }
    }
}


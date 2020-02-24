using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace netTK
{
    public partial class ProgramForm : Form
    {
        RenderForm renderForm;
        public ProgramForm(RenderForm form)
        {
            InitializeComponent();
            renderForm = form;
        }

        private void ProgramForm_Load(object sender, EventArgs e)
        {
            netTK.CreateWorld(100, 100, 50, 50, 20, 20);

            rdb_djikstra.Checked = true;
        }

        //---------------------------------------------------------------------------------------------

        //timer kontrole
        private void btn_step_Click(object sender, EventArgs e)
        {
            timer1_Tick(sender, e);
        }
        private void btn_play_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
            {
                timer1.Stop();
            }
            else
            {
                timer1.Start();
            }
        }

        //labirinti
        private void Labirint(int wallCount, Color wallColor)
        {
            Random rnd = new Random();
            int x, y;
            for (int i = 0; i < wallCount; i++)
            {
                x = rnd.Next(-25, 26);
                y = rnd.Next(-25, 26);
                while (netTK.PatchAt(x, y).GetColor() != Color.Black)
                {
                    x = rnd.Next(-25, 26);
                    y = rnd.Next(-25, 26);
                }
                netTK.PatchAt(x, y).SetColor(wallColor);
            }
        }
        private void LabirintPrim(Color floorColor, Color wallColor, Patch start)
        {
            foreach (Patch p in netTK.GetPatchesAsList()) { p.SetColor(wallColor); }
            start.SetColor(floorColor);
            List<Patch> walls = start.Neighbours90WithColor(wallColor);
            Random rnd = new Random();

            while (walls.Count() != 0)
            {
                Patch wall = walls[rnd.Next(walls.Count)];

                if (wall.Neighbours90WithColor(floorColor).Count == 1)
                {
                    wall.SetColor(floorColor);
                    foreach (Patch p in wall.Neighbours90WithColor(wallColor))
                    {
                        walls.Add(p);
                    }
                }

                walls.Remove(wall);
            }
        }

        //varijable
        private Patch start;
        private Patch end;

        //postavi
        private void btnSetup_Click(object sender, EventArgs e)
        {
            //Resetiranje i crtanje novog labirinta
            timer1.Stop();
            netTK.Clear();
            LabirintPrim(Color.Black, Color.Red, netTK.PatchAt(0, 0));


            //Postavljanje Patcheva
            Random rnd = new Random();

            netTK.AddCustomPatchProperty("visited");

            start = netTK.PatchAt(rnd.Next(-50, 50), rnd.Next(-50, 50));
            start.SetColor(Color.White);
            start.CustomProperties["visited"] = true;

            end = netTK.PatchAt(rnd.Next(-50, 50), rnd.Next(-50, 50));
            end.SetColor(Color.Blue);

            //Stvaranje prvog robota
            Robot OG_Robot = new Robot();
            OG_Robot.SetPosition(start.Position);
            OG_Robot.SetColor(Color.Yellow);
            OG_Robot.gValue = 0;
            OG_Robot.hValue = netTK.Manhatten(start.Position, end.Position);
            OG_Robot.fValue = OG_Robot.hValue;
            netTK.InsertAgent(OG_Robot);

            //render i gledaj na start
            netTK.ViewCam.LookAtPatch(start);
            renderForm.renderCanvas.Invalidate();
        }

        //Korak programa
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Ako nema robota nije pronađen cilj
            if (netTK.GetAgents().Count() == 0)
            {
                timer1.Stop();
                MessageBox.Show("Could not find goal");
                return;
            }

            //Nađi robota sa najmanjim f-value i postavi ga kao trenutnog
            Robot current = (Robot)netTK.GetAgents().First();
            foreach (Robot robot in netTK.GetAgents().OfType<Robot>())
            {
                if (robot.fValue < current.fValue)
                {
                    current = robot;
                }
            }

            //ako trenutni robot stoji na plavom polju pronašao je cilj
            if (current.PatchHere().GetColor() == Color.Blue)
            {
                timer1.Stop();

                //obojaj put kojim je robot došao do cilja i završi simulaciju
                foreach (Patch p in current.put)
                {
                    p.SetColor(Color.White);
                }
                renderForm.renderCanvas.Invalidate();
                return;
            }

            //ako robot nije na plavom polju dodaj polje na kojem se nalazi u njegov put
            current.put.Add(current.PatchHere());

            //za svako susjedno polje
            foreach (Patch p in current.PatchHere().Neighbours90())
            {
                //ako nije crveno(zid) i nije posječeno
                if (p.GetColor() != Color.Red && p.CustomProperties["visited"] == null)
                {
                    //hatchaj robota i postavi mu poziciju na susjedni patch
                    Robot novi = current.Hatch();
                    novi.LookAt(p);
                    novi.SetPosition(p);

                    //izračunaj f vrijednost novog robota
                    if (rdb_djikstra.Checked)
                    {
                        novi.gValue = current.gValue + 1;
                        novi.fValue = novi.gValue;
                    }
                    if (rdb_A_star.Checked)
                    {
                        novi.gValue = current.gValue + 1;
                        novi.hValue = netTK.Manhatten(novi.PatchHere().Position, end.Position);
                        novi.fValue = novi.hValue + novi.gValue;
                    }
                    if (rdb_pohlepna.Checked)
                    {
                        novi.hValue = netTK.Manhatten(novi.PatchHere().Position, end.Position);
                        novi.fValue = novi.hValue;
                    }

                    //postavi svojstvo patcha na posjeceno
                    p.CustomProperties["visited"] = true;
                }
            }

            //ubij trenutnog robota i ponovno nacrtaj
            current.Die();
            renderForm.renderCanvas.Invalidate();

        }

    }

    public class Robot : Agent
    {
        public double fValue;
        public double gValue;
        public double hValue;

        public List<Patch> put;

        public Robot() : base()
        {
            put = new List<Patch>();
        }
        public Robot(Point position) : base(position)
        {
            put = new List<Patch>();
        }

        public new Robot Hatch()
        {
            Robot novi = new Robot();
            novi.SetPosition(this.Position);
            novi.SetHeading(this.GetHeading());
            novi.SetColor(this.GetColor());
            foreach (Patch p in this.put)
            {
                novi.put.Add(p);
            }
            netTK.InsertAgent(novi);

            return novi;
        }
    }
}

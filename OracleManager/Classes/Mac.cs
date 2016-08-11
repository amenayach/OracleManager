//By Amen Ayach
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
public class Mac
{
    public enum eDirection
    {
        Up,
        Down,
        Right,
        Left
    }

    eDirection dir = eDirection.Down;
    Form frm = null;

    Point centerScreen;
    public Mac(Form _frm, eDirection _Direction)
    {
        frm = _frm;
        dir = _Direction;

        if (frm != null && !frm.IsDisposed)
        {
            //Initializing points
            centerScreen = new Point((Screen.PrimaryScreen.Bounds.Width - frm.Width) / 2, (Screen.PrimaryScreen.Bounds.Height - frm.Height) / 2);

            //Initializing form location
            frm.StartPosition = FormStartPosition.Manual;
            switch (dir)
            {
                case eDirection.Up:
                    frm.Top = Screen.PrimaryScreen.Bounds.Height + frm.Height + 10;
                    frm.Left = centerScreen.X;
                    break;
                case eDirection.Down:
                    frm.Top = -(frm.Height + 10);
                    frm.Left = centerScreen.X;
                    break;
                case eDirection.Left:
                    frm.Left = Screen.PrimaryScreen.Bounds.Width + frm.Width + 10;
                    frm.Top = centerScreen.Y;
                    break;
                case eDirection.Right:
                    frm.Left = -(frm.Width + 10);
                    frm.Top = centerScreen.Y;
                    break;
            }

            //Attaching event
            frm.Load += FromLoad;

        }
    }

    private void FromLoad(object sender, EventArgs e)
    {
        System.Windows.Forms.Timer tm = new System.Windows.Forms.Timer
        {
            Interval = 1,
            Enabled = true
        };
        tm.Tick += TmTick;
        tm.Start();
    }

    private void TmTick(object sender, EventArgs e)
    {
        Timer tm = (System.Windows.Forms.Timer)sender;
        try
        {
            tm.Stop();
            const int increm = 50;
            switch (dir)
            {
                case eDirection.Up:
                    if (frm.Top > centerScreen.Y)
                    {
                        frm.Top -= increm;
                    }
                    else
                    {
                        StopTimer(ref tm);
                    }
                    break;
                case eDirection.Down:
                    if (frm.Top < centerScreen.Y)
                    {
                        frm.Top += increm;
                    }
                    else
                    {
                        StopTimer(ref tm);
                    }
                    break;
                case eDirection.Right:
                    if (frm.Left < centerScreen.X)
                    {
                        frm.Left += increm;
                    }
                    else
                    {
                        StopTimer(ref tm);
                    }
                    break;
                case eDirection.Left:
                    if (frm.Left > centerScreen.X)
                    {
                        frm.Left -= increm;
                    }
                    else
                    {
                        StopTimer(ref tm);
                    }
                    break;
            }
            if (tm != null)
            {
                tm.Start();
            }
            else
            {
                frm.Location = centerScreen;
            }

        }
        catch
        {
            //Ignored
        }
    }

    private void StopTimer(ref System.Windows.Forms.Timer aTimer)
    {
        if (aTimer != null)
        {
            aTimer.Stop();
            aTimer.Dispose();
            aTimer = null;
        }
    }

    public static void Slide(Form frm, eDirection _Go = eDirection.Down)
    {
        Mac c = new Mac(frm, _Go);
    }
}
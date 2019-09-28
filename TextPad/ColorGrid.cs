//------------------------------------------
// ColorGrid.cs (c) 2005 by Charles Petzold
//------------------------------------------
using System;
using System.Drawing;
using System.Windows.Forms;

class ColorGrid : Control
{

#region Declaration

    int xHighlight = -1;
    int yHighlight = -1;

    const int xNum = 8;
    const int yNum = 5;

    Color clrSelected = Color.Black;

    Rectangle rectTotal;
    Rectangle rectGray;
    Rectangle rectBorder;
    Rectangle rectColor;
  
    Color[,] Colors = new Color[yNum, xNum] 
    {
        { Color.Black, Color.Brown, Color.DarkGreen, Color.MidnightBlue,
            Color.Navy, Color.DarkBlue, Color.Indigo, Color.DimGray },

        { Color.DarkRed, Color.OrangeRed, Color.Olive, Color.Green, 
            Color.Teal, Color.Blue, Color.SlateGray, Color.Gray },

        { Color.Red, Color.Orange, Color.YellowGreen, Color.SeaGreen, 
            Color.Aqua, Color.LightBlue, Color.Violet, Color.DarkGray },

        { Color.Pink, Color.Gold, Color.Yellow, Color.Lime, 
            Color.Turquoise, Color.SkyBlue, Color.Plum, Color.LightGray },

        { Color.LightPink, Color.Tan, Color.LightYellow, Color.LightGreen, 
            Color.LightCyan, Color.LightSkyBlue, Color.Lavender, Color.White }
    };

#endregion

    public ColorGrid()
    {
        AutoSize = true;

        Graphics Graphics = CreateGraphics();
        int xDpi = (int)Graphics.DpiX;
        int yDpi = (int)Graphics.DpiY;
        Graphics.Dispose();

        rectTotal = new Rectangle(0, 0, xDpi / 5, yDpi / 5);
        rectGray = Rectangle.Inflate(rectTotal, -xDpi / 72, -yDpi / 72);
        rectBorder = Rectangle.Inflate(rectGray, -xDpi / 48, -yDpi / 48);
        rectColor = Rectangle.Inflate(rectBorder, -xDpi / 72, -yDpi / 72);
    }

    public Color SelectedColor
    {
        get
        {
            return clrSelected;
        }
        set
        {
            clrSelected = value;
            Invalidate();
        }
    }

    void DrawColor(Graphics Graphics, int x, int y, bool bHighlight)
    {
        bool bDisposeGraphics = false;

        if (x < 0 || y < 0 || x >= xNum || y >= yNum)
        {
            return;
        }

        if (null == Graphics)
        {
            Graphics = CreateGraphics();
            bDisposeGraphics = true;
        }

        bool bSelect = Colors[y, x].ToArgb() == SelectedColor.ToArgb();

        Brush Brush = (bHighlight | bSelect) ? SystemBrushes.HotTrack : SystemBrushes.Menu;

        Rectangle Rect = rectTotal;
        Rect.Offset(x * rectTotal.Width, y * rectTotal.Height);
        Graphics.FillRectangle(Brush, Rect);

        if (bHighlight || bSelect)
        {
            Brush = bHighlight ? SystemBrushes.ControlDark :
                                 SystemBrushes.ControlLight;
            Rect = rectGray;
            Rect.Offset(x * rectTotal.Width, y * rectTotal.Height);
            Graphics.FillRectangle(Brush, Rect);
        }

        Rect = rectBorder;
        Rect.Offset(x * rectTotal.Width, y * rectTotal.Height);
        Graphics.FillRectangle(SystemBrushes.ControlDark, Rect);

        Rect = rectColor;
        Rect.Offset(x * rectTotal.Width, y * rectTotal.Height);
        Graphics.FillRectangle(new SolidBrush(Colors[y, x]), Rect);

        if (bDisposeGraphics)
        {
            Graphics.Dispose();
        }
    }

#region Override Functions

    public override Size GetPreferredSize(Size Size)
    {
        return new Size(xNum * rectTotal.Width, yNum * rectTotal.Height);
    }

    protected override void OnPaint(PaintEventArgs Args)
    {
        Graphics Graphics = Args.Graphics;

        for (int y = 0; y < yNum; y++)
        {
            for (int x = 0; x < xNum; x++)
            {
                DrawColor(Graphics, x, y, false);
            }
        }
    }

    protected override void OnMouseEnter(EventArgs Args)
    {
        xHighlight = -1;
        yHighlight = -1;
    }

    protected override void OnMouseMove(MouseEventArgs Args)
    {
        int x = Args.X / rectTotal.Width;
        int y = Args.Y / rectTotal.Height;

        if (x != xHighlight || y != yHighlight)
        {
            DrawColor(null, xHighlight, yHighlight, false);
            DrawColor(null, x, y, true);

            xHighlight = x;
            yHighlight = y;
        }
    }

    protected override void OnMouseLeave(EventArgs Args)
    {
        DrawColor(null, xHighlight, yHighlight, false);

        xHighlight = -1;
        yHighlight = -1;
    }

    protected override void OnMouseDown(MouseEventArgs Args)
    {
        int x = Args.X / rectTotal.Width;
        int y = Args.Y / rectTotal.Height;
        SelectedColor = Colors[y, x];
        base.OnMouseDown(Args);         
        Focus();
    }

    protected override void OnEnter(EventArgs Args)
    {
        if (xHighlight < 0 || yHighlight < 0)
        {
            for (yHighlight = 0; yHighlight < yNum; yHighlight++)
            {
                for (xHighlight = 0; xHighlight < xNum; xHighlight++)
                {
                    if (Colors[yHighlight, xHighlight].ToArgb() == SelectedColor.ToArgb())
                    {
                        break;
                    }
                }
                if (xHighlight < xNum)
                {
                    break;
                }
            }
        }
        if (xHighlight == xNum && yHighlight == yNum)
        {
            xHighlight = yHighlight = 0;
        }

        DrawColor(null, xHighlight, yHighlight, true);
    }

    protected override void OnLeave(EventArgs Args)
    {
        DrawColor(null, xHighlight, yHighlight, false);
        xHighlight = yHighlight = -1;
    }

    protected override bool IsInputKey(Keys keyData)
    {
        return keyData == Keys.Home || keyData == Keys.End ||
               keyData == Keys.Up || keyData == Keys.Down ||
               keyData == Keys.Left || keyData == Keys.Right;
    }

    protected override void OnKeyDown(KeyEventArgs Args)
    {
        DrawColor(null, xHighlight, yHighlight, false);

        int x = xHighlight;
        int y = yHighlight;

        switch (Args.KeyCode)
        {
            case Keys.Home:
                {
                    x = y = 0;
                    break;
                }
            case Keys.End:
                {
                    x = xNum - 1;
                    y = yNum - 1;
                    break;
                }
            case Keys.Right:
                {
                    if (++x == xNum)
                    {
                        x = 0;
                        if (++y == yNum)
                        {
                            Parent.GetNextControl(this, true).Focus();
                        }
                    }
                    break;
                }
            case Keys.Left:
                {
                    if (--x == -1)
                    {
                        x = xNum - 1;
                        if (--y == -1)
                        {
                            Parent.GetNextControl(this, false).Focus();
                        }
                    }
                    break;
                }
            case Keys.Down:
                {
                    if (++y == yNum)
                    {
                        y = 0;
                        if (++x == xNum)
                        {
                            Parent.GetNextControl(this, true).Focus();
                        }
                    }
                    break;
                }
            case Keys.Up:
                {
                    if (--y == -1)
                    {
                        y = 0;
                        if (--x == -1)
                        {
                            Parent.GetNextControl(this, false).Focus();
                        }
                    }
                    break;
                }
            case Keys.Enter:
            case Keys.Space:
                {
                    SelectedColor = Colors[y, x];
                    OnClick(EventArgs.Empty);
                    break;
                }
            default:
                {
                    base.OnKeyDown(Args);
                    return;
                }
        }
        DrawColor(null, x, y, true);

        xHighlight = x;
        yHighlight = y;
    }

#endregion
}
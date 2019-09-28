using System;
using System.Drawing;
using System.Windows.Forms;

class ToolStripColorGrid : ToolStripControlHost
{
    public ToolStripColorGrid() : base(new ColorGrid())
    {
    }

    public Color SelectedColor
    {
        get
        {
            return ((ColorGrid)Control).SelectedColor;
        }
        set
        {
            ((ColorGrid)Control).SelectedColor = value;
        }
    }

    protected override void OnClick(EventArgs Args)
    {
        base.OnClick(Args);
        ((ToolStripDropDown)Owner).Close(ToolStripDropDownCloseReason.ItemClicked);
    }
}
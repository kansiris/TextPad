using System;
using System.Drawing;
using System.Windows.Forms;

class AboutBox : Form
{
    public AboutBox()
    {
        Text = "About TextPad";
        AutoSize = true;
        AutoSizeMode = AutoSizeMode.GrowAndShrink;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        Icon = new Icon(GetType(), "TextPad.Images.NOTEPAD.ICO");
        MaximizeBox = false;
        
        FlowLayoutPanel Upper = new FlowLayoutPanel();
        Upper.Parent = this;
        Upper.AutoSize = true;
        Upper.FlowDirection = FlowDirection.TopDown;

        FlowLayoutPanel Lower = new FlowLayoutPanel();
        Lower.Parent = Upper;
        Lower.AutoSize = true;
        Lower.Margin = new Padding(Font.Height);

        PictureBox PicBox = new PictureBox();
        PicBox.Parent = Lower;
        PicBox.Image = new Bitmap(GetType(), "TextPad.Images.NOTEPAD.PNG");
        PicBox.SizeMode = PictureBoxSizeMode.AutoSize;
        PicBox.Anchor = AnchorStyles.None;

        Label Version = new Label();
        Version.Parent = Upper;
        Version.AutoSize = true;
        Version.Anchor = AnchorStyles.None;
        Version.Text = "TextPad";
        Version.Font = new Font("Algerian", 20, FontStyle.Regular);

        Label VersionNo = new Label();
        VersionNo.Parent = Upper;
        VersionNo.AutoSize = true;
        VersionNo.Anchor = AnchorStyles.None;
        VersionNo.Text = "Version 1.0";
        VersionNo.Font = new Font("Algerian", 15, FontStyle.Regular);

        Label CopyRight = new Label();
        CopyRight.Parent = Upper;
        CopyRight.AutoSize = true;
        CopyRight.Anchor = AnchorStyles.None;
        CopyRight.Margin = new Padding(Font.Height);
        CopyRight.Text = "Copyright \x00A9 2009";
        CopyRight.Font = new Font(FontFamily.GenericSerif, 12, FontStyle.Bold | FontStyle.Italic);

        Button OK = new Button();
        OK.Parent = Upper;
        OK.AutoSize = true;
        OK.Anchor = AnchorStyles.None;
        OK.Margin = new Padding(Font.Height);
        OK.Text = "OK";
        OK.Font = new Font("Times New Roman", 14, FontStyle.Bold);
        OK.Click += OnOk;
    }

    void OnOk(Object Source, EventArgs Args)
    {
        Close();
    }
}

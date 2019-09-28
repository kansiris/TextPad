using System;
using System.Drawing;
using System.Windows.Forms;

class GoTo : Form
{

#region Declaration

    TextPad NotePad;

    Label Caption;
    TextBox GoToLine;
    Button GoToButton;
    Button Cancel;

#endregion

#region Form

    public GoTo(TextPad NotePad)
    {
        int nSpace = Font.Height;

        this.NotePad = NotePad;
        Text = "Go To Line";
        ClientSize = new Size(205, 90);
        Location = NotePad.Location;
        MinimizeBox = false;
        MaximizeBox = false;
        Icon = new Icon(GetType(), "TextPad.Images.APPLICATION.ICO");

        Caption = new Label();
        Caption.Parent = this;
        Caption.AutoSize = true;
        Caption.Text = "Line Number: ";
        Caption.Location = new Point(nSpace, nSpace + 3);

        GoToLine = new TextBox();
        GoToLine.Parent = this;
        GoToLine.Location = new Point(Caption.Right + 3, nSpace);
        GoToLine.Size = new Size(100, 50);
        GoToLine.KeyPress += OnTextChanged;

        nSpace = Caption.Bottom + nSpace;

        GoToButton = new Button();
        GoToButton.Parent = this;
        GoToButton.AutoSize = true;
        GoToButton.Location = new Point(nSpace - Caption.Bottom + 10, nSpace);
        GoToButton.Text = "OK";
        GoToButton.Enabled = false;
        GoToButton.Click += OnGotoLine;

        Cancel = new Button();
        Cancel.Parent = this;
        Cancel.AutoSize = true;
        Cancel.Location = new Point(GoToButton.Right + 5 , nSpace);
        Cancel.Text = "Cancel";
        Cancel.Click += OnCancel;
    }

    protected override void OnClosing(System.ComponentModel.CancelEventArgs Args)
    {
        NotePad.GoTO = null;
        NotePad.Focus();
        base.OnClosing(Args);
    }

#endregion

#region Functions

    void OnTextChanged(Object Source, KeyPressEventArgs Args)
    {
        if (!System.Text.RegularExpressions.Regex.IsMatch(Args.KeyChar.ToString(), "\\d+"))
            Args.Handled = true;

        GoToButton.Enabled = true;
    }

    void OnGotoLine(Object Source, EventArgs Args)
    {
        int nLine;

        try
        {
            nLine = Int32.Parse(GoToLine.Text.ToString());
        }
        catch
        {
            GoToLine.Select(0, GoToLine.Text.Length);
            GoToLine.Focus();
            return;
        }
        int nPosition = -1; 
        int nCurrentLine = 0; 
        while ((nPosition = NotePad.NotePad.Text.IndexOf('\n', nPosition + 1)) > 0 && nCurrentLine++ < nLine - 1);  
        if (-1 == nPosition)
        {
            GoToLine.Select(0, GoToLine.Text.Length);
            GoToLine.Focus();
            return;
        }

        NotePad.NotePad.SelectionStart = nPosition;
        NotePad.NotePad.Focus();
        Close();
    }

    void OnCancel(Object Source, EventArgs Args)
    {
        Close();
    }

#endregion

}
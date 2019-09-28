using System;
using System.Drawing;
using System.Windows.Forms;

class Find : Form
{

#region Declaration

    TextPad NotePad;

    Label Caption;
    TextBox Search;
    CheckBox MatchCase;
    Button FindButton;
    Button FindNextButton;

    string SearchTerm;

#endregion

#region Form

    public Find(TextPad NotePad)
    {
        int nSpace = Font.Height;

        this.NotePad = NotePad;

        Text = "Find";
        ClientSize = new Size(270, 90);
        Location = NotePad.Location;
        MinimizeBox = false;
        MaximizeBox = false;
        Icon = new Icon(GetType(), "TextPad.Images.APPLICATION.ICO");

        Caption = new Label();
        Caption.Parent = this;
        Caption.AutoSize = true;
        Caption.Text = "Find what: ";
        Caption.Location = new Point(nSpace, nSpace + 3);

        Search = new TextBox();
        Search.Parent = this;
        Search.Location = new Point(Caption.Right + 3, nSpace);
        Search.Size = new Size(175, 50);
        Search.TextChanged += OnTextChanged;

        nSpace = Caption.Bottom + nSpace;

        MatchCase = new CheckBox();
        MatchCase.Parent = this;
        MatchCase.AutoSize = true;
        MatchCase.Location = new Point(nSpace - Caption.Bottom, nSpace + 2);
        MatchCase.Text = "Match Case";

        FindButton = new Button();
        FindButton.Parent = this;
        FindButton.Location = new Point(MatchCase.Right, nSpace);
        FindButton.Text = "Find";
        FindButton.Click += OnFind;

        FindNextButton = new Button();
        FindNextButton.Parent = this;
        FindNextButton.Location = new Point(FindButton.Right + 3, nSpace);
        FindNextButton.Text = "Find Next";
        FindNextButton.Enabled = false;
        FindNextButton.Click += OnFindNext;
    }

    public string SearchText
    {
        get
        {
            return SearchTerm;
        }
        set
        {
            SearchTerm = value;
        }
    }

    public void FindNext()
    {
        OnFindNext(Search, EventArgs.Empty);
    }

    void OnTextChanged(Object Source, EventArgs Aras)
    {
        FindNextButton.Enabled = false;
    }

    protected override void OnClosing(System.ComponentModel.CancelEventArgs Args)
    {
        NotePad.SearchText = SearchText;
        NotePad.Find = null;
        NotePad.Focus();
        base.OnClosing(Args);
    }

#endregion

#region Button

    void OnFind(Object Source, EventArgs Args)
    {
        try
        {
            int nStartPosition;
            RichTextBoxFinds SearchType;

            SearchType = MatchCase.Checked ? RichTextBoxFinds.MatchCase : RichTextBoxFinds.None;

            SearchText = Search.Text;
            NotePad.SearchText = SearchText;

            nStartPosition = NotePad.NotePad.Find(SearchText, 0, SearchType);

            if (nStartPosition < 0)
            {
                MessageBox.Show("String '" + SearchText.ToString() + "' not found", "No Matches", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Search.Select(0, SearchText.Length);
                Search.ScrollToCaret();
                Search.Focus();
                return;
            }

            NotePad.NotePad.Select(nStartPosition, SearchText.Length);
            NotePad.NotePad.ScrollToCaret();
            NotePad.Focus();
            FindNextButton.Enabled = true;
        }
        catch
        {
            MessageBox.Show("String '" + SearchText.ToString() + "' not found", "No Matches", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

    void OnFindNext(Object Source, EventArgs Args)
    {
        try
        {
            int nStartPosition = NotePad.NotePad.SelectionStart + 2;
            StringComparison SearchType;

            SearchType = MatchCase.Checked ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            SearchText = Search.Text;

            nStartPosition = NotePad.NotePad.Text.IndexOf(SearchText, nStartPosition, SearchType);

            if (nStartPosition == 0 || nStartPosition < 0)
            {
                MessageBox.Show("String '" + SearchText.ToString() + "' not found", "No Matches", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Search.Select(0, SearchText.Length);
                Search.ScrollToCaret();
                Search.Focus();
                return;
            }

            NotePad.NotePad.Select(nStartPosition, SearchText.Length);
            NotePad.NotePad.ScrollToCaret();
            NotePad.Focus();
        }
        catch
        {
            MessageBox.Show("String '" + SearchText.ToString() + "' not found", "No Matches", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

#endregion

}

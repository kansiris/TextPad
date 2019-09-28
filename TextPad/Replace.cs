using System;
using System.Drawing;
using System.Windows.Forms;

class Replace : Form
{
#region Declaration

    TextPad NotePad;

    Label Caption;
    TextBox Search;
    TextBox ReplaceText;
    CheckBox MatchCase;
    Button FindButton;
    Button FindNextButton;
    Button ReplaceButton;
    Button ReplaceAllButton;

    string SearchTerm;
    string ReplacementText;

#endregion

#region Form

    public Replace(TextPad NotePad)
    {
        int nSpaceX = Font.Height;
        int nSpaceY = Font.Height;
        
        this.NotePad = NotePad;

        Text = "Replace";
        ClientSize = new Size(330, 155);
        Location = NotePad.Location;
        MinimizeBox = false;
        MaximizeBox = false;
        Icon = new Icon(GetType(), "TextPad.Images.APPLICATION.ICO");

        Caption = new Label();
        Caption.Parent = this;
        Caption.AutoSize = true;
        Caption.Text = "Find what: ";
        Caption.Location = new Point(nSpaceX, nSpaceY + 3);

        Search = new TextBox();
        Search.Parent = this;
        Search.Location = new Point(Caption.Right + 18, nSpaceY);
        Search.Size = new Size(225, 50);
        Search.TextChanged += OnTextChanged;

        nSpaceY = Caption.Bottom + nSpaceY;

        Caption = new Label();
        Caption.Parent = this;
        Caption.AutoSize = true;
        Caption.Text = "Replace with: ";
        Caption.Location = new Point(nSpaceX, nSpaceY + 3);

        ReplaceText = new TextBox();
        ReplaceText.Parent = this;
        ReplaceText.Location = new Point(Caption.Right + 2, nSpaceY);
        ReplaceText.Size = new Size(225, 50);

        nSpaceY = Caption.Bottom + nSpaceY - Search.Bottom;
        
        MatchCase = new CheckBox();
        MatchCase.Parent = this;
        MatchCase.AutoSize = true;
        MatchCase.Location = new Point(nSpaceX, nSpaceY + 3);
        MatchCase.Text = "Match Case";

        nSpaceY = MatchCase.Bottom;

        FindButton = new Button();
        FindButton.Parent = this;
        FindButton.Location = new Point(nSpaceX - 5, nSpaceY + nSpaceX);
        FindButton.Text = "Find";
        FindButton.Click += OnFind;

        FindNextButton = new Button();
        FindNextButton.Parent = this;
        FindNextButton.Location = new Point(FindButton.Right + 3, nSpaceY + nSpaceX);
        FindNextButton.Text = "Find Next";
        FindNextButton.Enabled = false;
        FindNextButton.Click += OnFindNext;

        ReplaceButton = new Button();
        ReplaceButton.Parent = this;
        ReplaceButton.Location = new Point(FindNextButton.Right + 5, nSpaceY + nSpaceX);
        ReplaceButton.Text = "Replace";
        ReplaceButton.Click += OnReplace;

        ReplaceAllButton = new Button();
        ReplaceAllButton.Parent = this;
        ReplaceAllButton.Location = new Point(ReplaceButton.Right + 3, nSpaceY + nSpaceX);
        ReplaceAllButton.Text = "Replace All";
        ReplaceAllButton.Click += OnReplaceAll;
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
        NotePad.Replace = null;
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

    void OnReplace(Object Source, EventArgs Args)
    {
        try
        {
            ReplacementText = ReplaceText.Text;

            if (NotePad.NotePad.SelectedText.Length != 0)
            {
                NotePad.NotePad.SelectedText = ReplacementText;
            }

            int nStartPosition;
            StringComparison SearchType;

            SearchText = Search.Text;

            SearchType = MatchCase.Checked ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            nStartPosition = NotePad.NotePad.Text.IndexOf(SearchText, SearchType);

            if (nStartPosition == 0 || nStartPosition < 0)
            {
                MessageBox.Show("String' " + SearchText.ToString() + "' not found", "No Matches", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

    void OnReplaceAll(Object Source, EventArgs Args)
    {
        try
        {
            SearchText = Search.Text;
            ReplacementText = ReplaceText.Text;
            NotePad.NotePad.Rtf = NotePad.NotePad.Rtf.Replace(SearchText.Trim(), ReplacementText.Trim());

            int nStartPosition;
            StringComparison SearchType;

            SearchType = MatchCase.Checked ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;

            nStartPosition = NotePad.NotePad.Text.IndexOf(ReplacementText, SearchType);

            NotePad.NotePad.Select(nStartPosition, ReplacementText.Length);
            NotePad.NotePad.ScrollToCaret();
            NotePad.Focus();
        }
        catch
        {
            MessageBox.Show("String: " + SearchText.ToString() + " not found", "No Matches", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

#endregion

}
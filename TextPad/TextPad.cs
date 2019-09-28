using System;
using System.IO;
using System.Drawing;
using System.Reflection;
using System.ComponentModel;
using System.Drawing.Printing;
using System.Windows.Forms;

class TextPad : Form
{

#region Declarations

    MenuStrip MainMenu;
    ToolStrip ToolBar;
    ToolStrip FontBar;
    StatusBar StatusBar;
    ContextMenuStrip ContextMenupad;

    ToolStripMenuItem FileMenu;
    ToolStripMenuItem EditMenu;
    ToolStripMenuItem FormatMenu;
    ToolStripMenuItem ViewMenu;
    ToolStripMenuItem HelpMenu;

    ToolStripMenuItem FileNew;
    ToolStripMenuItem FileOpen;
    ToolStripMenuItem FileSave;
    ToolStripMenuItem FileSaveAs;
    ToolStripMenuItem FilePageSetup;
    ToolStripMenuItem FilePrint;
    ToolStripMenuItem FileExit;

    ToolStripMenuItem EditUndo;
    ToolStripMenuItem EditRedo;
    ToolStripMenuItem EditCut;
    ToolStripMenuItem EditCopy;
    ToolStripMenuItem EditPaste;
    ToolStripMenuItem EditClear;
    ToolStripMenuItem EditFind;
    ToolStripMenuItem EditFindNext;
    ToolStripMenuItem EditReplace;
    ToolStripMenuItem EditGoTo;
    ToolStripMenuItem EditSelectAll;
    ToolStripMenuItem EditTimeDate;

    ToolStripMenuItem FormatWordWrap;
    ToolStripMenuItem FormatFont;
    ToolStripMenuItem FormatFontColor;
    ToolStripMenuItem FormatBackGround;

    ToolStripMenuItem ViewToolBar;
    ToolStripMenuItem ViewFontBar;
    ToolStripMenuItem ViewStatusBar;

    ToolStripMenuItem HelpTopics;
    ToolStripMenuItem HelpAbout;

    ToolStripButton NewButton;
    ToolStripButton OpenButton;
    ToolStripButton SaveButton;
    ToolStripButton PrintButton;
    ToolStripButton UndoButton;
    ToolStripButton RedoButton;
    ToolStripButton CutButton;
    ToolStripButton CopyButton;
    ToolStripButton PasteButton;
    ToolStripButton ClearButton;
    ToolStripButton FindButton;
    ToolStripButton ReplaceButton;
    ToolStripButton GotoButton;
    ToolStripButton DateButton;
    ToolStripButton HelpMenuButton;
    ToolStripButton AboutButton;
    ToolStripButton ChangePosition;

    ToolStripButton Bold;
    ToolStripButton Italics;
    ToolStripButton Underline;
    ToolStripButton Strikeout;
    ToolStripComboBox FontNames;
    ToolStripComboBox FontSizes;
    ToolStripSplitButton FontBackColor;
    ToolStripSplitButton FontColor;
    ToolStripMenuItem BackGroundColor;
    ToolStripButton LeftAlign;
    ToolStripButton CenterAlign;
    ToolStripButton RightAlign;
    ToolStripButton BulletedList;
    ToolStripButton IndentIncrease;
    ToolStripButton IndentDecrease;

    StatusBarPanel Status;
    StatusBarPanel Time;

    RichTextBox NotePadDoc;

    PageSettings StoredPageSettings;

    Find FindTextForm;
    Replace ReplaceTextForm;
    GoTo GoToForm;

    string strTitle;
    string strFileName;
    string strSearchTerm;
    string strFontName;

    int nIndent;
    float fFileSize;

#endregion

    [STAThread]
    static void Main()
    {
        Application.EnableVisualStyles();
        Application.Run(new TextPad());
    }

#region Main Menu

    void LoadMainMenu()
    {
        MainMenu = new MenuStrip();
        MainMenu.Parent = this;

        LoadFileMenu();
        LoadEditMenu();
        LoadFormatMenu();
        LoadViewMenu();
        LoadHelpMenu();

        MainMenu.Items.Add(FileMenu);
        MainMenu.Items.Add(EditMenu);
        MainMenu.Items.Add(FormatMenu);
        MainMenu.Items.Add(ViewMenu);
        MainMenu.Items.Add(HelpMenu);
    }

    void LoadFileMenu()
    {
        FileMenu = new ToolStripMenuItem();
        FileMenu.Text = "&File";

        FileNew = new ToolStripMenuItem();
        FileNew.Text = "&New";
        FileNew.ShortcutKeys = Keys.Control | Keys.N;
        FileNew.Image = new Bitmap(GetType(), "TextPad.Images.NEW.BMP");
        FileNew.ImageTransparentColor = Color.Black;
        FileNew.Click += OnFileNew;

        FileOpen = new ToolStripMenuItem();
        FileOpen.Text = "&Open...";
        FileOpen.ShortcutKeys = Keys.Control | Keys.O;
        FileOpen.Image = new Bitmap(GetType(), "TextPad.Images.OPEN.BMP");
        FileOpen.ImageTransparentColor = Color.Black;
        FileOpen.Click += OnFileOpen;

        FileSave = new ToolStripMenuItem();
        FileSave.Text = "&Save";
        FileSave.ShortcutKeys = Keys.Control | Keys.S;
        FileSave.Image = new Bitmap(GetType(), "TextPad.Images.SAVE.BMP");
        FileSave.ImageTransparentColor = Color.Black;
        FileSave.Click += OnFileSave;
        
        FileSaveAs = new ToolStripMenuItem();
        FileSaveAs.Text = "Save &As...";
        FileSaveAs.Click += OnFileSaveAs;

        FilePageSetup = new ToolStripMenuItem();
        FilePageSetup.Text = "&Page Setup...";
        FilePageSetup.Image = new Bitmap(GetType(), "TextPad.Images.PAGE.BMP");
        FilePageSetup.ImageTransparentColor = Color.Black;
        FilePageSetup.Click += OnFilePageSetup;

        FilePrint = new ToolStripMenuItem();
        FilePrint.Text = "&Print...";
        FilePrint.ShortcutKeys = Keys.Control | Keys.P;
        FilePrint.Image = new Bitmap(GetType(), "TextPad.Images.PRINT.BMP");
        FilePrint.ImageTransparentColor = Color.Black;
        FilePrint.Click += OnFilePrint;

        FileExit = new ToolStripMenuItem();
        FileExit.Text = "E&xit";
        FileExit.Click += OnFileExit;

        FileMenu.DropDownItems.Add(FileNew); 
        FileMenu.DropDownItems.Add(new ToolStripSeparator());
        FileMenu.DropDownItems.Add(FileOpen);
        FileMenu.DropDownItems.Add(FileSave);
        FileMenu.DropDownItems.Add(FileSaveAs);
        FileMenu.DropDownItems.Add(new ToolStripSeparator());
        FileMenu.DropDownItems.Add(FilePageSetup);
        FileMenu.DropDownItems.Add(FilePrint);
        FileMenu.DropDownItems.Add(new ToolStripSeparator());
        FileMenu.DropDownItems.Add(FileExit);
    }

    void LoadEditMenu()
    {
        EditMenu = new ToolStripMenuItem();
        EditMenu.Text = "&Edit";

        EditUndo = new ToolStripMenuItem();
        EditUndo.Text = "&Undo";
        EditUndo.ShortcutKeys = Keys.Control | Keys.Z;
        EditUndo.Image = new Bitmap(GetType(), "TextPad.Images.UNDO.BMP");
        EditUndo.ImageTransparentColor = Color.Black;
        EditUndo.Click += OnEditUndo;

        EditRedo = new ToolStripMenuItem();
        EditRedo.Text = "&Redo";
        EditRedo.ShortcutKeys = Keys.Control | Keys.Y;
        EditRedo.Image = new Bitmap(GetType(), "TextPad.Images.REDO.BMP");
        EditRedo.ImageTransparentColor = Color.Magenta;
        EditRedo.Click += OnEditRedo;

        EditCut = new ToolStripMenuItem();
        EditCut.Text = "Cu&t";
        EditCut.ShortcutKeys = Keys.Control | Keys.X;
        EditCut.Image = new Bitmap(GetType(), "TextPad.Images.CUT.BMP");
        EditCut.ImageTransparentColor = Color.Black;
        EditCut.Click += OnEditCut;

        EditCopy = new ToolStripMenuItem();
        EditCopy.Text = "&Copy";
        EditCopy.ShortcutKeys = Keys.Control | Keys.C;
        EditCopy.Image = new Bitmap(GetType(), "TextPad.Images.COPY.BMP");
        EditCopy.ImageTransparentColor = Color.Black;
        EditCopy.Click += OnEditCopy;

        EditPaste = new ToolStripMenuItem();
        EditPaste.Text = "&Paste";
        EditPaste.ShortcutKeys = Keys.Control | Keys.V;
        EditPaste.Image = new Bitmap(GetType(), "TextPad.Images.PASTE.BMP");
        EditPaste.ImageTransparentColor = Color.Black;
        EditPaste.Click += OnEditPaste;

        EditClear = new ToolStripMenuItem();
        EditClear.Text = "&Clear";
        EditClear.ShortcutKeys = Keys.Delete;
        EditClear.Image = new Bitmap(GetType(), "TextPad.Images.DELETE.BMP");
        EditClear.ImageTransparentColor = Color.Magenta;
        EditClear.Click += OnEditClear;

        EditFind = new ToolStripMenuItem();
        EditFind.Text = "&Find...";
        EditFind.ShortcutKeys = Keys.Control | Keys.F;
        EditFind.Image = new Bitmap(GetType(), "TextPad.Images.FIND.PNG");
        EditFind.Click += OnEditFind;

        EditFindNext = new ToolStripMenuItem();
        EditFindNext.Text = "&FindNext";
        EditFindNext.ShortcutKeys = Keys.F3;
        EditFindNext.Image = new Bitmap(GetType(), "TextPad.Images.FINDNEXT.BMP");
        EditFindNext.ImageTransparentColor = Color.Black;
        EditFindNext.Click += OnEditFindNext;

        EditReplace = new ToolStripMenuItem();
        EditReplace.Text = "&Replace...";
        EditReplace.ShortcutKeys = Keys.Control | Keys.H;
        EditReplace.Image = new Bitmap(GetType(), "TextPad.Images.REPLACE.PNG");
        EditReplace.Click += OnEditReplace;

        EditGoTo = new ToolStripMenuItem();
        EditGoTo.Text = "&Go To...";
        EditGoTo.ShortcutKeys = Keys.Control | Keys.G;
        EditGoTo.Image = new Bitmap(GetType(), "TextPad.Images.GOTO.BMP");
        EditGoTo.ImageTransparentColor = Color.Black;
        EditGoTo.Click += OnEditGoTo;
        
        EditSelectAll = new ToolStripMenuItem();
        EditSelectAll.Text = "&SelectAll";
        EditSelectAll.ShortcutKeys = Keys.Control | Keys.A;
        EditSelectAll.Image = new Bitmap(GetType(), "TextPad.Images.SELECT.BMP");
        EditSelectAll.ImageTransparentColor = Color.Black;
        EditSelectAll.Click += OnEditSelectAll;

        EditTimeDate = new ToolStripMenuItem();
        EditTimeDate.Text = "&Time/Date";
        EditTimeDate.ShortcutKeys = Keys.F5;
        EditTimeDate.Image = new Bitmap(GetType(), "TextPad.Images.DATE.BMP");
        EditTimeDate.ImageTransparentColor = Color.Black;
        EditTimeDate.Click += OnEditTimeDate;

        EditMenu.DropDownItems.Add(EditUndo);
        EditMenu.DropDownItems.Add(EditRedo);
        EditMenu.DropDownItems.Add(new ToolStripSeparator());
        EditMenu.DropDownItems.Add(EditCut);
        EditMenu.DropDownItems.Add(EditCopy);
        EditMenu.DropDownItems.Add(EditPaste);
        EditMenu.DropDownItems.Add(EditClear);
        EditMenu.DropDownItems.Add(new ToolStripSeparator());
        EditMenu.DropDownItems.Add(EditFind);
        EditMenu.DropDownItems.Add(EditFindNext);
        EditMenu.DropDownItems.Add(EditReplace);
        EditMenu.DropDownItems.Add(EditGoTo);  
        EditMenu.DropDownItems.Add(new ToolStripSeparator());
        EditMenu.DropDownItems.Add(EditSelectAll);
        EditMenu.DropDownItems.Add(EditTimeDate);
    }

    void LoadFormatMenu()
    {
        FormatMenu = new ToolStripMenuItem();
        FormatMenu.Text = "F&ormat";

        FormatWordWrap = new ToolStripMenuItem();
        FormatWordWrap.Text = "&Word Wrap";
        FormatWordWrap.Image = new Bitmap(GetType(), "TextPad.Images.WRAP.BMP");
        FormatWordWrap.ImageTransparentColor = Color.Black;
        FormatWordWrap.CheckOnClick = true;
        FormatWordWrap.Click += OnFormatWordWrap;

        FormatFont = new ToolStripMenuItem();
        FormatFont.Text = "&Font...";
        FormatFont.Image = new Bitmap(GetType(), "TextPad.Images.FONT.PNG");
        FormatFont.Click += OnForamtFont;

        FormatFontColor = new ToolStripMenuItem();
        FormatFontColor.Text = "&Font Color";
        FormatFontColor.Image = new Bitmap(GetType(), "TextPad.Images.FONTCOLOR.PNG");
        FormatFontColor.Click += OnFormatFontColor;

        FormatBackGround = new ToolStripMenuItem();
        FormatBackGround.Text = "&Background";
        FormatBackGround.Image = new Bitmap(GetType(), "TextPad.Images.BACKGROUND.BMP");
        FormatBackGround.ImageTransparentColor = Color.Magenta;
        FormatBackGround.Click += OnFormatBackGround;

        FormatMenu.DropDownItems.Add(FormatWordWrap);
        FormatMenu.DropDownItems.Add(FormatFont);
        FormatMenu.DropDownItems.Add(FormatFontColor);
        FormatMenu.DropDownItems.Add(FormatBackGround);
    }

    void LoadViewMenu()
    {
        ViewMenu = new ToolStripMenuItem();
        ViewMenu.Text = "&View";

        ViewToolBar = new ToolStripMenuItem();
        ViewToolBar.Text = "&Tool Bar";
        ViewToolBar.Checked = true;
        ViewToolBar.CheckOnClick = true;
        ViewToolBar.Click += OnViewToolBar;

        ViewFontBar = new ToolStripMenuItem();
        ViewFontBar.Text = "&Font Bar";
        ViewFontBar.Checked = true;
        ViewFontBar.CheckOnClick = true;
        ViewFontBar.Click += OnViewFontBar;

        ViewStatusBar = new ToolStripMenuItem();
        ViewStatusBar.Text = "&Status Bar";
        ViewStatusBar.Checked = true;
        ViewStatusBar.CheckOnClick = true;
        ViewStatusBar.Click += OnViewStatusBar;

        ViewMenu.DropDownItems.Add(ViewToolBar);
        ViewMenu.DropDownItems.Add(ViewFontBar);
        ViewMenu.DropDownItems.Add(ViewStatusBar);
    }

    void LoadHelpMenu()
    {
        HelpMenu = new ToolStripMenuItem();
        HelpMenu.Text = "&Help";

        HelpTopics = new ToolStripMenuItem();
        HelpTopics.Text = "Help Topics";
        HelpTopics.Image = new Bitmap(GetType(), "TextPad.Images.HELP.PNG");

        HelpAbout = new ToolStripMenuItem();
        HelpAbout.Text = "About TextPad";
        HelpAbout.Image = new Bitmap(GetType(), "TextPad.Images.ABOUT.PNG");
        HelpAbout.ImageTransparentColor = Color.Black;
        HelpAbout.Click += OnHelpAbout;

        HelpMenu.DropDownItems.Add(HelpTopics);
        HelpMenu.DropDownItems.Add(new ToolStripSeparator());
        HelpMenu.DropDownItems.Add(HelpAbout);
    }

#endregion

#region Menu Functions

    void OnFileNew(Object Source, EventArgs Args)
    {
        BeforeClose(Source, Args);

        Text = "Untitled" + strTitle;
        NotePad.ReadOnly = false;
        NotePad.Clear();
        NotePad.BackColor = Color.White;
        NotePad.Modified = false;
    }

    void OnFileOpen(Object Source, EventArgs Args)
    {
        NotePad.ReadOnly = false;
        BeforeClose(Source, Args);
        
        OpenFileDialog OpenFile = new OpenFileDialog();

        OpenFile.InitialDirectory = Environment.SpecialFolder.MyDocuments.ToString();
        OpenFile.Filter = "Text Document (*.txt)|*.txt|RTF Document (*.rtf)|*.rtf|All files (*.*)|*.*";
        OpenFile.FilterIndex = 1;
        OpenFile.RestoreDirectory = true;

        if (DialogResult.OK == OpenFile.ShowDialog() && OpenFile.FileName.Length > 0)
        {
            RichTextBoxStreamType StreamType = new RichTextBoxStreamType();
            try
            {
                switch (OpenFile.FilterIndex)
                {
                    case 1:
                        {
                            StreamType = RichTextBoxStreamType.PlainText;
                            strFileName = OpenFile.FileName;
                            NotePadDoc.LoadFile(strFileName, StreamType);
                            Text = Path.GetFileName(strFileName) + strTitle;
                            break;
                        }
                    case 2:
                        {
                            StreamType = RichTextBoxStreamType.RichText;
                            strFileName = OpenFile.FileName;
                            NotePadDoc.LoadFile(strFileName, StreamType);
                            Text = Path.GetFileName(strFileName) + strTitle;
                            break;
                        }
                    default:
                        {
                            Bitmap ImageFile = new Bitmap(OpenFile.FileName);
                            Clipboard.SetDataObject(ImageFile);
                            DataFormats.Format PadFormat = DataFormats.GetFormat(DataFormats.Bitmap);
                            if (NotePad.CanPaste(PadFormat))
                            {
                                NotePad.Paste(PadFormat);
                                NotePad.ReadOnly = true;
                            }
                            Text = Path.GetFileName(strFileName) + strTitle;
                            break;
                        }
                }
            }
            catch
            {
                NotePadDoc.Modified = false;
                MessageBox.Show("File Format is not Valid",
                                "TextPad",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
            NotePadDoc.Modified = false; 
        }
    }

    void OnFileSave(Object Source, EventArgs Args)
    {
        if ("Untitled" + strTitle == Text)
        {
            OnFileSaveAs(Source, Args);
        }
        else
        {
            try
            {
                NotePadDoc.SaveFile(strFileName, RichTextBoxStreamType.PlainText);
                NotePadDoc.Modified = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "TextPad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }    
        }
    }

    void OnFileSaveAs(Object Source, EventArgs Args)
    {
        SaveFileDialog SaveFile = new SaveFileDialog();
        SaveFile.DefaultExt = "*.txt";
        SaveFile.Filter = "Text Document (*.txt)|*.txt|RTF Document (*.rtf)|*.rtf";

        if (DialogResult.OK == SaveFile.ShowDialog() && SaveFile.FileName.Length > 0)
        {
            RichTextBoxStreamType StreamType = new RichTextBoxStreamType();
            switch (SaveFile.FilterIndex)
            {
                case 1:
                    {
                        StreamType = RichTextBoxStreamType.PlainText;
                        break;
                    }
                case 2:
                    {
                        StreamType = RichTextBoxStreamType.RichText;
                        break;
                    }
                default:
                    {
                        StreamType = RichTextBoxStreamType.PlainText;
                        break;
                    }
            }
 
            try
            {
                NotePadDoc.SaveFile(SaveFile.FileName, StreamType);
                NotePadDoc.Modified = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "TextPad", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }           
        }
    }

    void OnFilePageSetup(Object Source, EventArgs Args)
    {
        
        PageSetupDialog OpenPageSetup = new PageSetupDialog();
        if (null == StoredPageSettings)
        {
            StoredPageSettings = new PageSettings();
        }
        OpenPageSetup.PageSettings = StoredPageSettings;
        OpenPageSetup.ShowDialog();
    }
    
    void OnFilePrint(Object Source, EventArgs Args)
    {
        PrintDocument PrintDoc = new PrintDocument();
        
        if (null != StoredPageSettings)
        {
            PrintDoc.DefaultPageSettings = StoredPageSettings;
        }

        PrintDialog OpenPrintDialog = new PrintDialog();
        OpenPrintDialog.ShowDialog();
        OpenPrintDialog.Document = PrintDoc;        
        if (DialogResult.OK == OpenPrintDialog.ShowDialog())
        {
           PrintDoc.Print();           
        }
    }

    void OnFileExit(Object Source, EventArgs Args)
    {
        BeforeClose(Source, Args);
        Application.Exit();
    }

    void OnEditUndo(Object Source, EventArgs Args)
    {
        if (NotePadDoc.CanUndo)
        {
            NotePadDoc.Undo();
        }
    }

    void OnEditRedo(Object Source, EventArgs Args)
    {
        if (NotePadDoc.CanRedo)
        {
            NotePadDoc.Redo();
        }
    }

    void OnEditCut(Object Source, EventArgs Args)
    {
        NotePadDoc.Cut();
    }

    void OnEditCopy(Object Source, EventArgs Args)
    {
        NotePadDoc.Copy();
    }
    
    void OnEditPaste(Object Source, EventArgs Args)
    {
        NotePadDoc.Paste();
    }

    void OnEditClear(Object Source, EventArgs Args)
    {
        NotePadDoc.SelectedText = string.Empty;
    }

    void OnEditFind(Object Source, EventArgs Args)
    {
        if (FindTextForm == null)
        {
            FindTextForm = new Find(this);
            FindTextForm.Show();
        }
        else
        {
            FindTextForm.Focus();
        }
    }

    void OnEditFindNext(Object Source, EventArgs Args)
    {
        if (SearchText == null)
        {
            OnEditFind(Source, Args);
        }
        else
        {
            FindTextForm = new Find(this);
            FindTextForm.FindNext();
        }
    }

    void OnEditReplace(Object Source, EventArgs Args)
    {
        if (ReplaceTextForm == null)
        {
            ReplaceTextForm = new Replace(this);
            ReplaceTextForm.Show();
        }
        else
        {
            ReplaceTextForm.Focus();
        }
    }

    void OnEditGoTo(Object Source, EventArgs Args)
    {
        if (GoToForm == null)
        {
            GoToForm = new GoTo(this);
            GoToForm.Show();
        }
        else
        {
            GoToForm.Focus();
        }
    }

    void OnEditSelectAll(Object Source, EventArgs Args)
    {
        NotePadDoc.SelectAll();
    }

    void OnEditTimeDate(Object Source, EventArgs Args)
    {
        NotePadDoc.AppendText(DateTime.Now.ToString("G"));
    }

    void OnFormatWordWrap(Object Source, EventArgs Args)
    {
    }

    void OnForamtFont(Object Source, EventArgs Args)
    {
        FontDialog OpenFontDialog = new FontDialog();
        if (DialogResult.OK == OpenFontDialog.ShowDialog())
        {
            if (NotePad.SelectedText == null)
            {
                NotePad.Font = OpenFontDialog.Font;
            }
            else
            {
                NotePad.SelectionFont = OpenFontDialog.Font;
            }

            FontNames.Text = OpenFontDialog.Font.Name.ToString();
            FontSizes.Text = OpenFontDialog.Font.Size.ToString();
            Bold.Checked = OpenFontDialog.Font.Bold;
            Italics.Checked = OpenFontDialog.Font.Italic;
            Underline.Checked = OpenFontDialog.Font.Underline;
            Strikeout.Checked = OpenFontDialog.Font.Strikeout;
        }
    }

    void OnFormatFontColor(Object Source, EventArgs Args)
    {
        ColorDialog OpenColorDialog = new ColorDialog();
        if (DialogResult.OK == OpenColorDialog.ShowDialog())
        {
            NotePadDoc.SelectionColor = OpenColorDialog.Color;
        }
    }

    void OnFormatBackGround(Object Source, EventArgs Args)
    {
        ColorDialog OpenColorDialog = new ColorDialog();
        if (DialogResult.OK == OpenColorDialog.ShowDialog())
        {
            NotePad.BackColor = OpenColorDialog.Color;
        }
    }
    
    void OnViewToolBar(Object Source, EventArgs Args)
    {
        if (ViewToolBar.Checked)
        {
            ToolBar.Show();
        }
        else
        {
            ToolBar.Hide();
        }

    }

    void OnViewFontBar(Object Source, EventArgs Args)
    {
        if (ViewFontBar.Checked)
        {
            FontBar.Show();
        }
        else
        {
            FontBar.Hide();
        }
    }

    void OnViewStatusBar(Object Source, EventArgs Args)
    {
        if (ViewStatusBar.Checked)
        {
            StatusBar.Show();
        }
        else
        {
            StatusBar.Hide();
        }
    }

    void OnHelpAbout(Object Source, EventArgs Args)
    {
        AboutBox About = new AboutBox();
        About.ShowDialog();
    }

#endregion

#region ToolBar

    void LoadToolBar()
    {
        ToolBar = new ToolStrip();
        ToolBar.Parent = this;
        ToolBar.Dock = DockStyle.Left;

        NewButton = new ToolStripButton();
        NewButton.Image = new Bitmap(GetType(), "TextPad.Images.NEW.BMP");
        NewButton.ImageTransparentColor = Color.Black;
        NewButton.ToolTipText = "New Document";
        NewButton.Click += OnFileNew;

        OpenButton = new ToolStripButton();
        OpenButton.Image = new Bitmap(GetType(), "TextPad.Images.OPEN.BMP");
        OpenButton.ImageTransparentColor = Color.Black;
        OpenButton.ToolTipText = "Open Document";
        OpenButton.Click += OnFileOpen;

        SaveButton = new ToolStripButton();
        SaveButton.Image = new Bitmap(GetType(), "TextPad.Images.SAVE.BMP");
        SaveButton.ImageTransparentColor = Color.Black;
        SaveButton.ToolTipText = "Save Document";
        SaveButton.Click += OnFileSave;

        PrintButton = new ToolStripButton();
        PrintButton.Image = new Bitmap(GetType(), "TextPad.Images.PRINT.BMP");
        PrintButton.ImageTransparentColor = Color.Black;
        PrintButton.ToolTipText = "Print Document";
        PrintButton.Click += OnFilePrint;
        
        UndoButton = new ToolStripButton();
        UndoButton.Image = new Bitmap(GetType(), "TextPad.Images.UNDO.BMP");
        UndoButton.ImageTransparentColor = Color.Black;
        UndoButton.ToolTipText = "Undo";
        UndoButton.Click += OnEditUndo;

        RedoButton = new ToolStripButton();
        RedoButton.Image = new Bitmap(GetType(), "TextPad.Images.REDO.BMP");
        RedoButton.ImageTransparentColor = Color.Magenta;
        RedoButton.ToolTipText = "Redo";
        RedoButton.Click += OnEditRedo;

        CutButton = new ToolStripButton();
        CutButton.Image = new Bitmap(GetType(), "TextPad.Images.CUT.BMP");
        CutButton.ImageTransparentColor = Color.Black;
        CutButton.ToolTipText = "Cut";
        CutButton.Click += OnEditCut;

        CopyButton = new ToolStripButton();
        CopyButton.Image = new Bitmap(GetType(), "TextPad.Images.COPY.BMP");
        CopyButton.ImageTransparentColor = Color.Black;
        CopyButton.ToolTipText = "Copy";
        CopyButton.Click += OnEditCopy;

        PasteButton = new ToolStripButton();
        PasteButton.Image = new Bitmap(GetType(), "TextPad.Images.PASTE.BMP");
        PasteButton.ImageTransparentColor = Color.Black;
        PasteButton.ToolTipText = "Paste";
        PasteButton.Click += OnEditPaste;

        ClearButton = new ToolStripButton();
        ClearButton.Image = new Bitmap(GetType(), "TextPad.Images.DELETE.BMP");
        ClearButton.ImageTransparentColor = Color.Magenta;
        ClearButton.ToolTipText = "Clear";
        ClearButton.Click += OnEditClear;

        FindButton = new ToolStripButton();
        FindButton.Image = new Bitmap(GetType(), "TextPad.Images.FIND.PNG");
        FindButton.ToolTipText = "Find";
        FindButton.Click += OnEditFind;

        ReplaceButton = new ToolStripButton();
        ReplaceButton.Image = new Bitmap(GetType(), "TextPad.Images.REPLACE.PNG");
        ReplaceButton.ToolTipText = "Replace";
        ReplaceButton.Click += OnEditReplace;

        GotoButton = new ToolStripButton();
        GotoButton.Image = new Bitmap(GetType(), "TextPad.Images.GOTO.BMP");
        GotoButton.ImageTransparentColor = Color.Black;
        GotoButton.ToolTipText = "Goto Line";
        GotoButton.Click += OnEditGoTo;

        DateButton = new ToolStripButton();
        DateButton.Image = new Bitmap(GetType(), "TextPad.Images.DATE.BMP");
        DateButton.ImageTransparentColor = Color.Black;
        DateButton.ToolTipText = "Date & Time";
        DateButton.Click += OnEditTimeDate;

        HelpMenuButton = new ToolStripButton();
        HelpMenuButton.Image = new Bitmap(GetType(), "TextPad.Images.HELP.PNG");
        HelpMenuButton.ToolTipText = "Help";

        AboutButton = new ToolStripButton();
        AboutButton.Image = new Bitmap(GetType(), "TextPad.Images.ABOUT.PNG");
        AboutButton.ToolTipText = "About NotePad";
        AboutButton.Click += OnHelpAbout;

        ChangePosition = new ToolStripButton();
        ChangePosition.Image = new Bitmap(GetType(), "TextPad.Images.POSITION.BMP");
        ChangePosition.ImageTransparentColor = Color.Black;
        ChangePosition.ToolTipText = "Change ToolBar Position";
        ChangePosition.Click += OnChangePosition;

        ToolBar.Items.Add(new ToolStripSeparator());
        ToolBar.Items.Add(NewButton);
        ToolBar.Items.Add(new ToolStripSeparator());
        ToolBar.Items.Add(OpenButton);
        ToolBar.Items.Add(SaveButton);
        ToolBar.Items.Add(PrintButton);
        ToolBar.Items.Add(new ToolStripSeparator());
        ToolBar.Items.Add(UndoButton);
        ToolBar.Items.Add(RedoButton);
        ToolBar.Items.Add(new ToolStripSeparator());
        ToolBar.Items.Add(CutButton);
        ToolBar.Items.Add(CopyButton);
        ToolBar.Items.Add(PasteButton);
        ToolBar.Items.Add(ClearButton);
        ToolBar.Items.Add(new ToolStripSeparator());
        ToolBar.Items.Add(FindButton);
        ToolBar.Items.Add(ReplaceButton);
        ToolBar.Items.Add(GotoButton);
        ToolBar.Items.Add(new ToolStripSeparator());
        ToolBar.Items.Add(DateButton);
        ToolBar.Items.Add(new ToolStripSeparator());
        ToolBar.Items.Add(HelpMenuButton);
        ToolBar.Items.Add(AboutButton);
        ToolBar.Items.Add(new ToolStripSeparator());
        ToolBar.Items.Add(ChangePosition);
    }

#endregion

#region FontBar

    void LoadFontBar()
    {
        FontBar = new ToolStrip();
        FontBar.Parent = this;

        Bold = new ToolStripButton();
        Bold.Image = new Bitmap(GetType(), "TextPad.Images.BOLD.PNG");
        Bold.ToolTipText = "Bold";
        Bold.CheckOnClick = true;
        Bold.Click += OnFontBold;
        
        Italics = new ToolStripButton();
        Italics.Image = new Bitmap(GetType(), "TextPad.Images.ITALICS.PNG");
        Italics.CheckOnClick = true;
        Italics.Click += OnFontItalics;

        Underline = new ToolStripButton();
        Underline.Image = new Bitmap(GetType(), "TextPad.Images.UNDERLINE.PNG");
        Underline.ToolTipText = "Underline";
        Underline.CheckOnClick = true;
        Underline.Click += OnFontUnderline;

        Strikeout = new ToolStripButton();
        Strikeout.Image = new Bitmap(GetType(), "TextPad.Images.STRIKE.PNG");
        Strikeout.ToolTipText = "Strikeout";
        Strikeout.CheckOnClick = true;
        Strikeout.Click += OnFontStrikeout; 
        
        FontNames = new ToolStripComboBox();
        FontNames.ToolTipText = "Font Names";
        FontNames.Size = new Size(200, FontNames.Height);
        FontNames.Items.Clear();
        foreach (FontFamily Font in FontFamily.Families)
        {
            FontNames.Items.Add(Font.Name);
        }
        FontNames.Text = NotePad.Font.FontFamily.Name.ToString();
        FontNames.SelectedIndexChanged += OnFontChange;
        FontNames.TextChanged += OnFontNameChanged;
        FontNames.KeyDown += OnFontEnter;

        FontSizes = new ToolStripComboBox();
        FontSizes.ToolTipText = "Font Sizes";
        FontSizes.Size = new Size(10, FontSizes.Height);
        for (int Size = 8; Size <= 10; Size++)
            FontSizes.Items.Add(Size.ToString());
        for (int Size = 12; Size <= 28; Size += 2)
            FontSizes.Items.Add(Size.ToString());
        for (int Size = 36; Size <= 72; Size += 12)
            FontSizes.Items.Add(Size.ToString());
        FontSizes.Text = fFileSize.ToString();
        FontSizes.SelectedIndexChanged += OnFontChange;
        FontSizes.TextChanged += OnFontSizeChanged;
        FontSizes.KeyDown += OnFontEnter;

        FontBackColor = new ToolStripSplitButton();
        FontBackColor.Name = "ColorGrid";
        FontBackColor.Image = new Bitmap(GetType(), "TextPad.Images.BACK.PNG");
        FontBackColor.ToolTipText = "Back Color";
        FontBackColor.ButtonClick += delegate
        {
            NotePad.SelectionBackColor = NotePad.BackColor;
        };
        FontBackColor.DropDownOpening += OnBackColorGrid;
        PropertyInfo PInfo = typeof(RichTextBox).GetProperty("SelectionBackColor");
        ToolStripColorGrid Grid = new ToolStripColorGrid();
        Grid.Name = "ColorGrid";
        Grid.Tag = PInfo;
        Grid.Click += OnColorGrid;
        FontBackColor.DropDownItems.Add(Grid);
        FontBackColor.DropDownItems.Add(new ToolStripSeparator());
        ToolStripMenuItem Colors = new ToolStripMenuItem("More colors...");
        Colors.Tag = PInfo;
        Colors.Click += OnMoreColors;
        FontBackColor.DropDownItems.Add(Colors);

        FontColor = new ToolStripSplitButton();
        FontColor.Image = new Bitmap(GetType(), "TextPad.Images.FONTCOLOR.PNG");
        FontColor.ToolTipText = "Font Color";
        FontColor.ButtonClick += delegate
        {
            NotePad.SelectionBackColor = NotePad.ForeColor;
        };
        FontColor.DropDownOpening += OnBackColorGrid;
        PInfo = typeof(RichTextBox).GetProperty("SelectionColor");
        Grid = new ToolStripColorGrid();
        Grid.Name = "ColorGrid";
        Grid.Tag = PInfo;
        Grid.Click += OnColorGrid;
        FontColor.DropDownItems.Add(Grid);
        FontColor.DropDownItems.Add(new ToolStripSeparator());
        Colors = new ToolStripMenuItem("More colors...");
        Colors.Tag = PInfo;
        Colors.Click += OnMoreColors;
        FontColor.DropDownItems.Add(Colors);
        FontColor.DropDownItems.Add(new ToolStripSeparator());

        BackGroundColor = new ToolStripMenuItem();
        BackGroundColor.Image = new Bitmap(GetType(), "TextPad.Images.BACKGROUND.BMP");
        BackGroundColor.ImageTransparentColor = Color.Magenta;
        BackGroundColor.ToolTipText = "Change BackGroungd color";
        BackGroundColor.Click += OnFormatBackGround;

        LeftAlign = new ToolStripButton();
        LeftAlign.Image = new Bitmap(GetType(), "TextPad.Images.LEFT.PNG");
        LeftAlign.ToolTipText = "Left Align";
        LeftAlign.CheckOnClick = true;
        LeftAlign.Checked = true;
        LeftAlign.Click += OnLeftAlign;

        CenterAlign = new ToolStripButton();
        CenterAlign.Image = new Bitmap(GetType(), "TextPad.Images.CENTER.PNG");
        CenterAlign.ToolTipText = "Center Align";
        CenterAlign.CheckOnClick = true;
        CenterAlign.Click += OnCenterAlign;

        RightAlign = new ToolStripButton();
        RightAlign.Image = new Bitmap(GetType(), "TextPad.Images.RIGHT.PNG");
        RightAlign.ToolTipText = "Right Align";
        RightAlign.CheckOnClick = true;
        RightAlign.Click += OnRightAlign;

        BulletedList = new ToolStripButton();
        BulletedList.Image = new Bitmap(GetType(), "TextPad.Images.BULLET.PNG");
        BulletedList.ToolTipText = "Bulleted List";
        BulletedList.CheckOnClick = true;
        BulletedList.Click += OnBulletedList;

        IndentIncrease = new ToolStripButton();
        IndentIncrease.Image = new Bitmap(GetType(), "TextPad.Images.ININDENT.PNG");
        IndentIncrease.ToolTipText = "Increase Indent";
        IndentIncrease.Click += OnIncreaseIndent;

        IndentDecrease = new ToolStripButton();
        IndentDecrease.Image = new Bitmap(GetType(), "TextPad.Images.DEINDENT.PNG");
        IndentDecrease.ToolTipText = "Decrease Indent";
        IndentDecrease.Click += OnDecreaseIndent;

        FontBar.Items.Add(new ToolStripSeparator());
        
        FontBar.Items.Add(Bold);
        FontBar.Items.Add(Italics);
        FontBar.Items.Add(Underline);
        FontBar.Items.Add(Strikeout);       
        FontBar.Items.Add(new ToolStripSeparator());     
        FontBar.Items.Add(FontNames);
        FontBar.Items.Add(FontSizes);        
        FontBar.Items.Add(new ToolStripSeparator());       
        FontBar.Items.Add(FontColor);
        FontBar.Items.Add(FontBackColor);
        FontBar.Items.Add(BackGroundColor);
        FontBar.Items.Add(new ToolStripSeparator());
        FontBar.Items.Add(LeftAlign);
        FontBar.Items.Add(CenterAlign);
        FontBar.Items.Add(RightAlign);
        FontBar.Items.Add(new ToolStripSeparator());
        FontBar.Items.Add(BulletedList);
        FontBar.Items.Add(new ToolStripSeparator());
        FontBar.Items.Add(IndentIncrease);
        FontBar.Items.Add(IndentDecrease);
        FontBar.Items.Add(new ToolStripSeparator());
    }

#endregion

#region Functions

    void OnFontBold(Object Source, EventArgs Args)
    {
        if (NotePad.SelectionFont == null)
        {
            return;
        }

        FontStyle Style = NotePad.SelectionFont.Style;

        if (NotePad.SelectionFont.Bold)
        {
            Style &= ~FontStyle.Bold;
        }
        else
        {
            Style |= FontStyle.Bold;
        }
        NotePad.SelectionFont = new Font(NotePad.SelectionFont, Style);
    }

    void OnFontItalics(Object Source, EventArgs Args)
    {
        if (NotePad.SelectionFont == null)
        {
            return;
        }
        FontStyle Style = NotePad.SelectionFont.Style;

        if (NotePad.SelectionFont.Italic)
        {
            Style &= ~FontStyle.Italic;
        }
        else
        {
            Style |= FontStyle.Italic;
        }
        NotePad.SelectionFont = new Font(NotePad.SelectionFont, Style);

    }

    void OnFontUnderline(Object Source, EventArgs Args)
    {
        if (NotePad.SelectionFont == null)
        {
            return;
        }

        FontStyle Style = NotePad.SelectionFont.Style;

        if (NotePad.SelectionFont.Underline)
        {
            Style &= ~FontStyle.Underline;
        }
        else
        {
            Style |= FontStyle.Underline;
        }
        NotePad.SelectionFont = new Font(NotePad.SelectionFont, Style);

    }

    void OnFontStrikeout(Object Source, EventArgs Args)
    {
        if (NotePad.SelectionFont == null)
        {
            return;
        }

        FontStyle Style = NotePad.SelectionFont.Style;

        if (NotePad.SelectionFont.Strikeout)
        {
            Style &= ~FontStyle.Strikeout;
        }
        else
        {
            Style |= FontStyle.Strikeout;
        }
        NotePad.SelectionFont = new Font(NotePad.SelectionFont, Style);

    }

    void OnFontChange(Object Source, EventArgs Args)
    {
        FontStyle Style = new FontStyle();
        
        if(Bold.Checked)
        {
            Style |= FontStyle.Bold; 
        }
        if(Italics.Checked)
        {
            Style |= FontStyle.Italic;
        }
        if(Underline.Checked)
        {
            Style |= FontStyle.Underline;
        }
        if (Strikeout.Checked)
        {
            Style |= FontStyle.Strikeout;
        }
        else
        {
            Style |= FontStyle.Regular;
        }

        fFileSize = float.Parse(FontSizes.Text.ToString());

        if (NotePad.SelectedText == null)
        {
            NotePad.Font = new Font(FontNames.Text.ToString(), fFileSize, Style);
        }
        else
        {
            try
            {
                NotePad.SelectionFont = new Font(FontNames.Text.ToString(), fFileSize, Style);
            }
            catch
            {

            }
        }
        NotePad.Focus();
    }

    void OnFontNameChanged(Object Source, EventArgs Args)
    {
        strFontName = FontNames.Text.ToString();
    }

    void OnFontSizeChanged(Object Source, EventArgs Args)
    {
        try
        {
            fFileSize = float.Parse(FontSizes.Text.ToString());
            OnFontChange(Source, Args);
        }
        catch
        {
            FontSizes.Text = fFileSize.ToString();
        }
    }

    void OnFontEnter(Object Source, KeyEventArgs Args)
    {
        if (Args.KeyCode == Keys.Enter)
        {
            if (-1 == FontNames.FindStringExact(strFontName))
            {
                FontNames.Text = NotePad.SelectionFont.Name.ToString();
            }
            NotePad.Focus();
        }
    }

    void OnBackColorGrid(Object Source, EventArgs Args)
    {
        ToolStripSplitButton Button = (ToolStripSplitButton)Source;
        ToolStripColorGrid Grid = (ToolStripColorGrid)Button.DropDownItems["ColorGrid"];
        PropertyInfo PInfo = (PropertyInfo)Grid.Tag;
        Grid.SelectedColor = (Color)PInfo.GetValue(NotePad, null);
    }

    void OnColorGrid(Object Source, EventArgs Args)
    {
        ToolStripColorGrid Grid = (ToolStripColorGrid)Source;
        PropertyInfo PInfo = (PropertyInfo)Grid.Tag;
        PInfo.SetValue(NotePad, Grid.SelectedColor, null);
    }

    void OnMoreColors(Object Source, EventArgs Args)
    {
        ToolStripMenuItem Colors = (ToolStripMenuItem)Source;
        PropertyInfo PInfo = (PropertyInfo)Colors.Tag;
        ColorDialog ColorDialog = new ColorDialog();
        ColorDialog.Color = (Color)PInfo.GetValue(NotePad, null);

        if (ColorDialog.ShowDialog() == DialogResult.OK)
        {
            PInfo.SetValue(NotePad, ColorDialog.Color, null);
        }
    }

    void OnLeftAlign(Object Source, EventArgs Args)
    {
        CenterAlign.Checked = RightAlign.Checked = false;
        LeftAlign.Checked = true;
        NotePad.SelectionAlignment = HorizontalAlignment.Left;
    }

    void OnCenterAlign(Object Source, EventArgs Args)
    {
        LeftAlign.Checked = RightAlign.Checked = false;
        CenterAlign.Checked = true;
        NotePad.SelectionAlignment = HorizontalAlignment.Center;
    }

    void OnRightAlign(Object Source, EventArgs Args)
    {
        LeftAlign.Checked = CenterAlign.Checked = false;
        RightAlign.Checked = true;
        NotePad.SelectionAlignment = HorizontalAlignment.Right;
    }

    void OnBulletedList(Object Source, EventArgs Args)
    {
        NotePad.SelectionBullet = BulletedList.Checked;
    }

    void OnIncreaseIndent(Object Source, EventArgs Args)
    {
        NotePad.SelectionIndent += nIndent;
    }

    void OnDecreaseIndent(Object Source, EventArgs Args)
    {
        if (NotePad.SelectionIndent != 0)
        {
            NotePad.SelectionIndent -= nIndent;
        }
    }

    void OnChangePosition(Object Source, EventArgs Args)
    {
        if (ToolBar.Dock == DockStyle.Left)
        {
            ToolBar.Dock = DockStyle.Top;
        }
        else
        {
            ToolBar.Dock = DockStyle.Left;
        }
    }

#endregion

#region StatusBar

    void LoadStatusBar()
    {
        StatusBar = new StatusBar();
        StatusBar.Parent = this;
        StatusBar.ShowPanels = true;
        StatusBar.Font = new Font("Times New Roman", 10, FontStyle.Regular);
        
        Status = new StatusBarPanel();
        Status.Alignment = HorizontalAlignment.Left;
        Status.Width = Width / 2;
        Status.BorderStyle = StatusBarPanelBorderStyle.Sunken;
        Status.Icon = new Icon(GetType(), "TextPad.Images.APPLICATION.ICO");
        Status.Text = "Ready";

        Time = new StatusBarPanel();
        Time.Alignment = HorizontalAlignment.Center;
        Time.Width = Width / 2;
        Time.BorderStyle = StatusBarPanelBorderStyle.Sunken;
        Time.Icon = new Icon(GetType(), "TextPad.Images.CLOCK.ICO");
        Time.Text = DateTime.Now.ToString("F");

        StatusBar.Panels.Add(Status);
        StatusBar.Panels.Add(Time);

    }
    
    void ShowTime(Object Source, EventArgs Args)
    {
        Time.Text = DateTime.Now.ToString("F");
    }

#endregion

#region Context Menu

    void LoadContextMenu()
    {
        ContextMenupad = new ContextMenuStrip();
        NotePad.ContextMenuStrip = ContextMenupad;

        EditUndo = new ToolStripMenuItem();
        EditUndo.Text = "&Undo";
        EditUndo.ShortcutKeys = Keys.Control | Keys.Z;
        EditUndo.Image = new Bitmap(GetType(), "TextPad.Images.UNDO.BMP");
        EditUndo.ImageTransparentColor = Color.Black;
        EditUndo.Click += OnEditUndo;

        EditRedo = new ToolStripMenuItem();
        EditRedo.Text = "&Redo";
        EditRedo.ShortcutKeys = Keys.Control | Keys.Y;
        EditRedo.Image = new Bitmap(GetType(), "TextPad.Images.REDO.BMP");
        EditRedo.ImageTransparentColor = Color.Magenta;
        EditRedo.Click += OnEditRedo;

        EditCut = new ToolStripMenuItem();
        EditCut.Text = "Cu&t";
        EditCut.ShortcutKeys = Keys.Control | Keys.X;
        EditCut.Image = new Bitmap(GetType(), "TextPad.Images.CUT.BMP");
        EditCut.ImageTransparentColor = Color.Black;
        EditCut.Click += OnEditCut;

        EditCopy = new ToolStripMenuItem();
        EditCopy.Text = "&Copy";
        EditCopy.ShortcutKeys = Keys.Control | Keys.C;
        EditCopy.Image = new Bitmap(GetType(), "TextPad.Images.COPY.BMP");
        EditCopy.ImageTransparentColor = Color.Black;
        EditCopy.Click += OnEditCopy;

        EditPaste = new ToolStripMenuItem();
        EditPaste.Text = "&Paste";
        EditPaste.ShortcutKeys = Keys.Control | Keys.V;
        EditPaste.Image = new Bitmap(GetType(), "TextPad.Images.PASTE.BMP");
        EditPaste.ImageTransparentColor = Color.Black;
        EditPaste.Click += OnEditPaste;

        EditClear = new ToolStripMenuItem();
        EditClear.Text = "&Clear";
        EditClear.ShortcutKeys = Keys.Delete;
        EditClear.Image = new Bitmap(GetType(), "TextPad.Images.DELETE.BMP");
        EditClear.ImageTransparentColor = Color.Magenta;
        EditClear.Click += OnEditClear;

        ContextMenupad.Items.Add(EditUndo);
        ContextMenupad.Items.Add(EditRedo);
        ContextMenupad.Items.Add(new ToolStripSeparator());
        ContextMenupad.Items.Add(EditCut);
        ContextMenupad.Items.Add(EditCopy);
        ContextMenupad.Items.Add(EditPaste);
        ContextMenupad.Items.Add(EditClear);
    }

#endregion

#region RichTextBox

    void OnDragEnter(Object Source, DragEventArgs Args)
    {
        Args.Effect = DragDropEffects.Copy;
    }

    void OnDragDrop(Object Source, DragEventArgs Args)
    {
        string[] FileNames = Args.Data.GetData(DataFormats.FileDrop) as string[];
        strFileName = FileNames[0];
        string strExtension = Path.GetExtension(strFileName);
        strExtension = strExtension.ToUpper();
        try
        {
            if (".TXT" == strExtension)
            {
                NotePadDoc.LoadFile(FileNames[0], RichTextBoxStreamType.PlainText);
                Text = Path.GetFileName(strFileName) + strTitle;
            }
            else if (".RTF" == strExtension)
            {
                NotePadDoc.LoadFile(FileNames[0], RichTextBoxStreamType.RichText);
                Text = Path.GetFileName(strFileName) + strTitle;
            }
            else
            {
                Bitmap ImageFile = new Bitmap(strFileName);
                Clipboard.SetDataObject(ImageFile);
                DataFormats.Format PadFormat = DataFormats.GetFormat(DataFormats.Bitmap);
                if (NotePad.CanPaste(PadFormat))
                {
                    NotePad.Paste(PadFormat);
                    NotePad.ReadOnly = true;
                }
                Text = Path.GetFileName(strFileName) + strTitle;
            }
        }
        catch
        {
            MessageBox.Show("File Format is not Valid",
                                    "TextPad",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
        }
        NotePadDoc.Modified = false;
    }

    void OnSelectionChanged(Object Source, EventArgs Args)
    {
        int nSelStart  = NotePadDoc.SelectionStart;
        int nSelLength = NotePadDoc.SelectionLength;
        int nSelEnd    = nSelStart + nSelLength;

        int nLine   = NotePadDoc.GetLineFromCharIndex(nSelStart);
        int nChar   = nSelStart - NotePadDoc.GetFirstCharIndexFromLine(nLine);
        Status.Text = String.Format("Line {0} Character {1}", nLine + 1, nChar + 1);

        if (nSelLength > 0)
        {
            nLine = NotePadDoc.GetLineFromCharIndex(nSelEnd);
            nChar = nSelEnd - NotePadDoc.GetFirstCharIndexFromLine(nLine);
            Status.Text += String.Format(" - Line {0} Character {1}", nLine + 1, nChar + 1);
        }
    }

    void OnTextChanged(Object Source, EventArgs Args)
    {
        if (-1 == FontNames.FindStringExact(strFontName))
        {
            FontNames.Text = NotePad.SelectionFont.Name.ToString();
        }
    }

    void OnKeyDown(Object Source, KeyEventArgs KeyArgs)
    {
        if (KeyArgs.Control && KeyArgs.KeyCode == Keys.B)
        {
            OnFontBold(Source, EventArgs.Empty);
            Bold.Checked = !Bold.Checked;
        }

        if (KeyArgs.Control && KeyArgs.KeyCode == Keys.I)
        {
            OnFontItalics(Source, EventArgs.Empty);
            Italics.Checked = !Italics.Checked;
        }

        if (KeyArgs.Control && KeyArgs.KeyCode == Keys.U)
        {
            OnFontUnderline(Source, EventArgs.Empty);
            Underline.Checked = !Underline.Checked;
        }

        if (KeyArgs.Control && KeyArgs.KeyCode == Keys.L)
        {
            OnLeftAlign(Source, EventArgs.Empty);
        }

        if (KeyArgs.Control && KeyArgs.KeyCode == Keys.E)
        {
            OnCenterAlign(Source, EventArgs.Empty);
        }

        if (KeyArgs.Control && KeyArgs.KeyCode == Keys.R)
        {
            OnRightAlign(Source, EventArgs.Empty);
        }

        if (KeyArgs.Control && KeyArgs.KeyCode == Keys.J)
        {
           
        }

    }

#endregion

#region Form

    TextPad()
    {
        strFileName = "Untitled";
        strTitle = " - TextPad";
        
        Text = strFileName + strTitle;
        Width *= 3;
        Height *= 2;
        Location = new Point(10, 10);
        WindowState = FormWindowState.Maximized;
        Icon = new Icon(GetType(), "TextPad.Images.NOTEPAD.ICO");

        NotePadDoc = new RichTextBox();
        NotePad.Parent = this;      
        NotePad.Dock = DockStyle.Fill;
        NotePad.ScrollBars = RichTextBoxScrollBars.ForcedBoth;
        NotePad.BorderStyle = BorderStyle.Fixed3D;
        NotePad.AllowDrop = true;
        NotePad.Font = new Font("Microsoft Sans Serif", 12, FontStyle.Regular);
        NotePad.DragEnter += OnDragEnter; 
        NotePad.DragDrop += OnDragDrop;
        NotePad.SelectionChanged += OnSelectionChanged;
        NotePad.TextChanged += OnTextChanged;
        NotePad.KeyDown += OnKeyDown;

        fFileSize = 12;
        nIndent = 10;

        LoadFontBar();
        LoadToolBar();
        LoadMainMenu();
        LoadStatusBar();
        LoadContextMenu();

        Timer CurrentTime = new Timer();
        CurrentTime.Interval = 1000;
        CurrentTime.Enabled = true;
        CurrentTime.Tick += ShowTime;
    }

    public RichTextBox NotePad
    {
        get
        {
            return NotePadDoc;
        }
    }

    public Find Find
    {
        get
        {
            return FindTextForm;
        }
        set
        {
            FindTextForm = value;
        }
    }

    public string SearchText
    {
        get
        {
            return strSearchTerm;
        }
        set
        {
            strSearchTerm = value;
        }
    }

    public Replace Replace
    {
        get
        {
            return ReplaceTextForm;
        }
        set
        {
            ReplaceTextForm = value;
        }
    }

    public GoTo GoTO
    {
        get
        {
            return GoToForm;
        }
        set
        {
            GoToForm = null;
        }
    }

    void BeforeClose(Object Source, EventArgs Args)
    {
        if (NotePadDoc.Modified)
        {
            DialogResult Answer = new DialogResult();
            Answer = MessageBox.Show(" The Document has Changed." +
                                     " \nDo you want to save the changes?",
                                     "TextPad",
                                     MessageBoxButtons.YesNo,
                                     MessageBoxIcon.Warning);
            NotePadDoc.Modified = false;

            switch (Answer)
            {
                case DialogResult.Yes:
                    {
                        OnFileSave(Source, Args);
                        break;
                    }
                case DialogResult.No:
                    {
                        break;
                    }
                default:
                    {
                        // Do Nothing..
                        break;
                    }
            }
        }
    }

    protected override void OnClosing(CancelEventArgs Args)
    {
        base.OnClosing(Args);
        BeforeClose(NotePadDoc, Args);
    }
    
#endregion

}

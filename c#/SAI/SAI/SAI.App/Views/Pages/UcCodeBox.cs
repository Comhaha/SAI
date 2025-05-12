using ScintillaNET;
using SAI.SAI.App.Views.Interfaces;
using System;
using System.Windows.Forms;

namespace SAI.SAI.App.Views.Pages
{
    public partial class UcCodeBox : UserControl, ICodeBoxView
    {
        private Scintilla scintilla;

        public UcCodeBox()
        {
            InitializeComponent();
            InitializeScintilla();
        }

        private void InitializeScintilla()
        {
            scintilla = new Scintilla();
            scintilla.Dock = DockStyle.Fill;

            // 기본 설정
            scintilla.StyleResetDefault();
            scintilla.Styles[Style.Default].Font = "Consolas";
            scintilla.Styles[Style.Default].Size = 10;
            scintilla.StyleClearAll();

            // C# 문법 하이라이팅 설정
            scintilla.Lexer = Lexer.Cpp;
            scintilla.StyleSetFore(1, 0x0000FF); // 키워드
            scintilla.StyleSetFore(2, 0x008000); // 문자열
            scintilla.StyleSetFore(3, 0x808080); // 주석

            // 자동 들여쓰기 설정
            scintilla.AutoCSetSeparator(' ');
            scintilla.AutoCSetMaxHeight(10);
            scintilla.AutoCSetMaxWidth(0);

            this.Controls.Add(scintilla);
        }

        public void SetCode(string code)
        {
            scintilla.Text = code;
        }

        public string GetCode()
        {
            return scintilla.Text;
        }

        public void ClearCode()
        {
            scintilla.Text = string.Empty;
        }
    }
} 
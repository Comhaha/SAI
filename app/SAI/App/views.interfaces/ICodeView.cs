namespace SAI.App.Views.Interfaces
{
    public interface ICodeView
    {
        void UpdateCode(string code);
        void HighlightCodeSegment(string codeSegment);
        // 새로운 메서드 추가
        void FindAndHighlightText(string searchText, bool caseSensitive = false);
        void HighlightLine(int lineIndex);
        void HighlightLineRange(int startLineIndex, int endLineIndex);
    }
}
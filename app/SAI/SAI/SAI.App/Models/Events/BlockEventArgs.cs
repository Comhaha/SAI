using System;

namespace SAI.SAI.App.Models.Events
{
    // 버튼을 눌렀을 때 BlockType을 전송해, 웹뷰에 해당 블록을 생성할 수 있게 하는 이벤트 클래스
    public class BlockEventArgs : EventArgs
    {
        public string BlockType { get; }

        public BlockEventArgs(string blockType)
        {
            BlockType = blockType;
        }
    }
}

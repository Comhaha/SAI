using System;

namespace SAI.SAI.Application.Interop
{
    public class JsBridge
    {
        private readonly Action<string> _onMessage;

        public JsBridge(Action<string> onMessage)
        {
            _onMessage = onMessage;
        }

        // JS에서 코드를 전달하기 위해 호출되는 메소드
        public void receiveFromJs(string message)
        {
            _onMessage?.Invoke(message);
        }
    }

}

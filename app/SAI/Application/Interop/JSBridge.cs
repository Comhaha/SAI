using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.Application.Interop
{
	public class JsBridge
	{
		private readonly Action<string, string> _onMessage;

		public JsBridge(Action<string, string> onMessage)
		{
			_onMessage = onMessage;
		}

		// JS에서 코드를 전달하기 위해 호출되는 메소드
		public void receiveMessageFromJs(string code, string type)
		{
			_onMessage?.Invoke(code, type);
		}
	}

}

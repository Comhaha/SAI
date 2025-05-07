using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAI.SAI.App.Models
{
	public class JsBridge
	{
		private readonly Action<string> _onMessage;

		public JsBridge(Action<string> onMessage)
		{
			_onMessage = onMessage;
		}

		public void receiveFromJs(string message)
		{
			Console.WriteLine(message);
			_onMessage?.Invoke(message);
		}
	}

}

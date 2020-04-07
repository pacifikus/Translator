using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Translator.Model
{
	public abstract class BaseTranslator
	{
		public string Translate(string inputText, string langIn, string langOut)
		{
			using (var webClient = new WebClient())
			{
				webClient.Headers.Add("Content-Type", "application/json; charset=utf-8");
				string responseUrl = ConstructResponseUrl(inputText, langIn, langOut);
				var response = webClient.DownloadString(responseUrl);
				string result = ParseResponse(response);
				return result;
			}
		}

		protected abstract string ConstructResponseUrl(string inputText, string langIn, string langOut);

		protected abstract string ParseResponse(string response);
	}
}

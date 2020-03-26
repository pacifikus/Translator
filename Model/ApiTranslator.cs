using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Translator.Model
{
	public class ApiTranslator
	{
		private const string ApiUrl = "https://translate.yandex.net/api/v1.5/tr.json/translate?";
		private const string Key = "trnsl.1.1.20181222T105826Z.ab8f06b010ea734f.f291534f7f065f4cde4556ea532ab2e667de799f";


		public string Translate(string inputText, string langIn, string langOut)
		{
			using (var webClient = new WebClient())
			{
				webClient.Headers.Add("Content-Type", "application/json; charset=utf-8");
				string responseUrl = $"{ApiUrl}key={Key}&text={inputText}&lang={langIn}-{langOut}";
				var response = webClient.DownloadString(responseUrl);
				string result = ParseResponse(response);
				return result;
			}
		}

		private static string ParseResponse(string response)
		{
			var symbolsToTrim = new char[] { '[', ']' };
			return JObject.Parse(response)["text"].ToString().Trim(symbolsToTrim)
								.Replace("\r\n", "").Trim().Replace("\"", "");
		}
	}
}

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Translator.Model
{
	public class GoogleTranslator : BaseTranslator
	{
		private const string ApiUrl = "https://translate.googleapis.com/translate_a/single?client=gtx";

		protected override string ConstructResponseUrl(string inputText, string langIn, string langOut)
		{
			return $"{ApiUrl}&sl={langIn}&tl={langOut}&dt=t&q={inputText}";
		}

		protected override string ParseResponse(string response)
		{
			return JArray.Parse(response)[0][0][0].ToString();
		}
	}
}

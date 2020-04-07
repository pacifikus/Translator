using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Translator.Model
{
	public class YandexTranslator : BaseTranslator
	{
		private const string ApiUrl = "https://translate.yandex.net/api/v1.5/tr.json/translate?";
		private const string Key = "trnsl.1.1.20181222T105826Z.ab8f06b010ea734f.f291534f7f065f4cde4556ea532ab2e667de799f";


		protected override string ConstructResponseUrl(string inputText, string langIn, string langOut)
		{
			return $"{ApiUrl}key={Key}&text={inputText}&lang={langIn}-{langOut}";
		}

		protected override string ParseResponse(string response)
		{
			return JObject.Parse(response)["text"][0].ToString();
		}
	}
}

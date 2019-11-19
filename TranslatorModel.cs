using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;

namespace Translator
{
	class TranslatorModel: INotifyPropertyChanged	
	{
		#region Fields.
		private string _apiUrl = "https://translate.yandex.net/api/v1.5/tr.json/translate?";
		private string _key = "trnsl.1.1.20181222T105826Z.ab8f06b010ea734f.f291534f7f065f4cde4556ea532ab2e667de799f";
		private Dictionary<string, string> _langs;
		private string _result;
		private List<string> _listLangs; 
		#endregion

		public TranslatorModel()
		{
			Result = "";
			InitLangs();
		}

		public event PropertyChangedEventHandler PropertyChanged;

		#region Properties.
		public string Result
		{
			get { return _result; }
			set { _result = value; OnPropertyChanged(); }
		}

		public List<string> ListLangs
		{
			get { return _listLangs; }
			set { _listLangs = value; OnPropertyChanged(); }
		}

		public string InputText { get; set; }

		public string SelectedLang { get; set; }

		public string SelectedLangIn { get; set; }
		#endregion

		#region Commands.
		public RelayCommand TranslateCommand
		{
			get
			{
				return new RelayCommand(obj =>
					{
						Translate();
					});
			}
		}

		public RelayCommand SpeechSynthesisCommand
		{
			get
			{
				return new RelayCommand(obj =>
					{
						if (Result != "") StartSynthesis();
					});
			}
		} 
		#endregion

		private void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}

		#region Methods.
		private void InitLangs()
		{
			_langs = new Dictionary<string, string>()
			{
				{ "Русский", "ru" },
				{ "Английский", "en" },
				{ "Итальянский", "it" },
				{ "Испанский", "en" },
				{ "Немецкий", "de" },
				{ "Французский", "fr" },
				{ "Норвежский", "no" },
				{ "Белорусский", "be" },
				{ "Сербский", "sr" },
				{ "Голландский", "nl" },
				{ "Словенский", "sl" },
				{ "Ирландский", "ga" },
				{ "Исландский", "is" },
				{ "Украинский", "uk" },
				{ "Финский", "fi" },
				{ "Хорватский", "hr" },
				{ "Чешский", "cs" },
				{ "Латынь", "la" },
				{ "Эсперанто", "eo" },
				{ "Японский", "ja" },
			};
			ListLangs = _langs.Keys.OrderBy(x => x.ToString()).ToList();
			SelectedLang = ListLangs[0];
			SelectedLangIn = ListLangs[0];
		}

		private void StartSynthesis()
		{
			var synthesizer = new SpeechSynthesizer();
			synthesizer.Rate = 0;
			synthesizer.SpeakAsync(Result);
		}

		private void Translate()
		{
			using (var webClient = new WebClient())
			{
				webClient.Headers.Add("Content-Type", "application/json; charset=utf-8");
				var response = webClient.DownloadString($"{_apiUrl}key={_key}&text={InputText}&lang={_langs[SelectedLangIn]}-{_langs[SelectedLang]}");
				Result = Newtonsoft.Json.Linq.JObject.Parse(response)["text"].ToString().Trim('[').Trim(']')
					.Replace("\r\n", "").Trim().Replace("\"", "");
			}
		} 
		#endregion
	}
}

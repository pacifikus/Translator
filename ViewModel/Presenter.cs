using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Speech.Synthesis;
using System.Windows;
using Translator.Model;

namespace Translator.ViewModel
{
	class Presenter: INotifyPropertyChanged	
	{
		private BaseTranslator _translator = new YandexTranslator();
		private Dictionary<string, string> _langs;
		private string _result;

		public Presenter()
		{
			InitLangs();
		}

		public string Result
		{
			get { return _result; }
			set
			{
				_result = value;
				OnPropertyChanged();
			}
		}

		public Dictionary<string, string> Langs
		{
			get { return _langs; }
			set
			{
				_langs = value;
				OnPropertyChanged();
			}
		}

		public string InputText { get; set; }

		public KeyValuePair<string, string> SelectedLang { get; set; }

		public KeyValuePair<string, string> SelectedLangIn { get; set; }

		public RelayCommand TranslateCommand
		{
			get { return new RelayCommand(Translate); }
		}

		public RelayCommand SpeechSynthesisCommand
		{
			get { return new RelayCommand(StartSynthesis); }
		}

		#region INotifyPropertyChanged implementation.

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}

		#endregion

		private void InitLangs()
		{
			var langsJson = GetJsonFile(); 
			_langs = JsonConvert.DeserializeObject<Dictionary<string, string>>(langsJson);
			SelectedLang = Langs.First();
			SelectedLangIn = Langs.First();
		}

		private string GetJsonFile()
		{
			string pathToFile = @"Languages.json";
			string json = File.ReadAllText(pathToFile);
			return json;
		}

		private void StartSynthesis()
		{
			if(CheckInput("Строка для воспроизведения отсутствует", Result))
			{
				var synthesizer = new SpeechSynthesizer();
				synthesizer.Rate = 0;
				synthesizer.SpeakAsync(Result);
			}
		}

		public void Translate()
		{
			if (CheckInput("Строка для перевода отсутствует", InputText))
				Result = _translator.Translate(InputText, SelectedLangIn.Value, SelectedLang.Value);
		}

		private bool CheckInput(string errorMessage, string toCheck)
		{
			if (string.IsNullOrEmpty(toCheck))
			{
				MessageBox.Show(errorMessage, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;
			}
			return true;
		}
	}
}

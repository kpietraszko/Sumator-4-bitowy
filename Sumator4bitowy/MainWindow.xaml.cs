using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;

namespace Sumator4bitowy
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		List<Bramka> Bramki = new List<Bramka>();
		string LiczbaX = "1111";
		string LiczbaY = "1111";
		Wtyk[] X = new Wtyk[4];
		Wtyk[] Y = new Wtyk[4];
		Wtyk[] Wynik;
		Wtyk Przeniesienie;

		public MainWindow()
		{
			InitializeComponent();
			//string X = "1001"; string Y = "1000";
			
			Wynik = new Wtyk[4];
			Przeniesienie = new Wtyk(false); //przeniesienie wejściowe pierwszego sumatora to 0
			StworzWtykiWejsciowe();
			for (int i = 3; i >= 0; i--) //łączenie bramek
			{
				Bramka BramkaXor = new Bramka(XOR);
				Bramki.Add(BramkaXor);
				BramkaXor.UstawWtykiWejsciowe(X[i], Y[i]);
				Bramki.Add(new Bramka(XOR));
				Bramki.Last().UstawWtykiWejsciowe(BramkaXor.Wyjscie, Przeniesienie);
				Wynik[i] = Bramki.Last().Wyjscie;
				Bramka ANDGorny = new Bramka(AND);
				Bramki.Add(ANDGorny);
				ANDGorny.UstawWtykiWejsciowe(X[i], Y[i]);
				Bramka ANDDolny = new Bramka(AND);
				Bramki.Add(ANDDolny);
				ANDDolny.UstawWtykiWejsciowe(Przeniesienie, BramkaXor.Wyjscie);
				Bramka BramkaOR = new Bramka(OR);
				Bramki.Add(BramkaOR);
				BramkaOR.UstawWtykiWejsciowe(ANDGorny.Wyjscie, ANDDolny.Wyjscie);
				Przeniesienie = BramkaOR.Wyjscie;
			}
			ObliczWynik();
		}
		#region operacje boolowskie
		bool AND(bool a, bool b)
		{
			Debug.WriteLine("Wykonuje {0} AND {1}= {2}", a.ToString01(), b.ToString01(), (a & b).ToString01());
			return a & b;
		}
		bool OR(bool a, bool b)
		{
			Debug.WriteLine("Wykonuje {0} OR {1}= {2}", a.ToString01(), b.ToString01(), (a | b).ToString01());
			return a | b;
		}
		bool XOR(bool a, bool b)
		{
			Debug.WriteLine("Wykonuje {0} XOR {1}= {2}", a.ToString01(), b.ToString01(), (a ^ b).ToString01());
			return a ^ b;
		}
		#endregion

		void StworzWtykiWejsciowe()
		{
			for (int i = 0; i < 4; i++) //tworzenie wtykow bitow wejsciowych i nadanie wartosci ze stringow
			{
				Przeniesienie = new Wtyk(false);
				bool bit;
				bit = LiczbaX[i] == '1';
				X[i] = new Wtyk(bit);
				bit = LiczbaY[i] == '1';
				Y[i] = new Wtyk(bit);
			}
		}
		string ObliczWynik()
		{

			StworzWtykiWejsciowe();
			foreach (var bramka in Bramki)
				bramka.WykonajDzialanie();
			string WynikNapis = "";
			WynikNapis += Przeniesienie.Wartosc.ToString01();
			Console.Write("Wynik: " + Przeniesienie.Wartosc.ToString01());
			for (int i = 0; i < Wynik.Length; i++)
			{
				string bit = Wynik[i].Wartosc.ToString01();
				Console.Write(bit);
				WynikNapis += bit;
			}
			Console.Write(Environment.NewLine);
			return WynikNapis;
		}

		private void liczba_TextChanged(object sender, TextChangedEventArgs e) //sprawdza poprawnosc przy kazdym znaku
		{
			TextBox liczba = sender as TextBox;
			foreach (char c in liczba.Text)
			{
				if (c != '0' && c != '1')
				{
					MessageBox.Show("Tylko 0 lub 1!");
					liczba.Text = liczba.Text.Replace(c.ToString(), "");
				}
			}
		}

		private void liczba_LostFocus(object sender, RoutedEventArgs e) //sprawdza poprawnosc po ukonczeniu wpisywania
		{
			TextBox liczba = sender as TextBox;
			Regex liczbaBinarna = new Regex("[01]{4}");
			if (liczbaBinarna.IsMatch(liczba.Text))
			{
				if (liczba.Name == "liczbaX")
					LiczbaX = liczba.Text;
				else
					LiczbaY = liczba.Text;
				PoleWyniku.Content = ObliczWynik();
			}
			else
			{
				MessageBox.Show("Błąd wprowadzania, wprowadź 4 cyfry binarne");
				liczba.Text = "";
			}
		}
	}
}

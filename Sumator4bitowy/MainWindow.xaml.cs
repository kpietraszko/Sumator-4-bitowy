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
		string LiczbaX = "0000";
		string LiczbaY = "0000";
		Wtyk[] X = new Wtyk[4];
		Wtyk[] Y = new Wtyk[4];
		Wtyk[] Wynik;
		Wtyk Przeniesienie;

		Rectangle[] KoloryBramek = new Rectangle[20];

		public MainWindow()
		{
			InitializeComponent();
			UstawieniaPoczatkoweKolorow();
			Wynik = new Wtyk[4];
			//Przeniesienie = new Wtyk(false); //przeniesienie wejściowe pierwszego sumatora to 0
			UaktualnijWtykiWejsciowe();
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

		void UaktualnijWtykiWejsciowe()
		{
			for (int i = 0; i < 4; i++) //tworzenie wtykow bitow wejsciowych i nadanie wartosci ze stringow
			{
				if(Przeniesienie == null)
					Przeniesienie = new Wtyk(false);
				else
					Przeniesienie.Wartosc = false;
				bool bit;
				bit = LiczbaX[i] == '1';
				if (X[i] == null)
					X[i] = new Wtyk(bit);
				else //jesli juz istnieje to ustaw wartosc
					X[i].Wartosc = bit;

				bit = LiczbaY[i] == '1';
				if (Y[i] == null)
					Y[i] = new Wtyk(bit);
				else
					Y[i].Wartosc = bit;
			}
		}
		string ObliczWynik()
		{

			UaktualnijWtykiWejsciowe();
			foreach (var bramka in Bramki)
				bramka.WykonajDzialanie();
			string WynikNapis = "";
			WynikNapis += Przeniesienie.Wartosc.ToString01() + " ";
			Console.Write("Wynik: " + Przeniesienie.Wartosc.ToString01());
			for (int i = 0; i < Wynik.Length; i++)
			{
				string bit = Wynik[i].Wartosc.ToString01();
				Console.Write(bit);
				WynikNapis += bit;
			}
			Console.Write(Environment.NewLine);
			ZaktualizujKoloryBramek();
			return WynikNapis;
		}
		void UstawieniaPoczatkoweKolorow()
		{
			Thickness[] PozycjeKolorowBramek = new Thickness[] { new Thickness(1088, 155, 117, 257), new Thickness(1102, 281, 103, 128), new Thickness(985, 133, 212, 276), new Thickness(985, 210, 211, 199), new Thickness(880, 175, 306, 234), new Thickness(800, 153, 405, 256), new Thickness(809, 280, 396, 129), new Thickness(695, 132, 501, 277), new Thickness(695, 208, 502, 201), new Thickness(600, 175, 595, 234), new Thickness(511, 153, 694, 256), new Thickness(520, 280, 689, 129), new Thickness(410, 131, 790, 278), new Thickness(410, 208, 790, 201), new Thickness(321, 175, 884, 234), new Thickness(220, 161, 985, 248), new Thickness(231, 288, 974, 121), new Thickness(127, 141, 1078, 268), new Thickness(127, 217, 1078, 192), new Thickness(34, 180, 1171, 229) };
			for (int i = 0; i < KoloryBramek.Length; i++)
			{
				KoloryBramek[i] = new Rectangle();
				KoloryBramek[i].Margin = new Thickness(i * 200, 40, 0, 20);
				//Panel.SetZIndex(KoloryBramek[i], 1);
				grid.Children.Add(KoloryBramek[i]);
				KoloryBramek[i].Fill = Brushes.White;
				Grid.SetRow(KoloryBramek[i], 1);
				KoloryBramek[i].Margin = PozycjeKolorowBramek[i];
			}
		}
		void ZaktualizujKoloryBramek()
		{
			for (int i = 0; i<Bramki.Count; i++)
			{
				KoloryBramek[i].Fill = Bramki[i].Wyjscie.Wartosc ? Brushes.Blue : Brushes.Red;
			}
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

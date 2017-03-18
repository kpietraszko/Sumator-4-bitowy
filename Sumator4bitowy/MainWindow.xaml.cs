using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
		public MainWindow()
		{
			InitializeComponent();
			string A = "1010"; string B = "1010";
			Wtyk[] LiczbaA = new Wtyk[4];
			Wtyk[] LiczbaB = new Wtyk[4];
			for (int i = 0; i < 4; i++) //tworzenie wtykow bitow wejsciowych
			{
				bool bit;
				bit = A[i] == '1';
				LiczbaA[i] = new Wtyk(bit);
				bit = B[i] == '1';
				LiczbaB[i] = new Wtyk(bit);
			}
			//Przykład testowy
			//Bramki.Add(new Bramka(OR));
			//Bramki[0].UstawWtykiWejsciowe(false, true);
			//Bramki.Add(new Bramka(OR));
			//Bramki[1].UstawWtykiWejsciowe(false, true);

			//Bramki.Add(new Bramka(AND));
			//Bramki[2].PodlaczDoWejscia(0, Bramki[0]);
			//Bramki[2].PodlaczDoWejscia(1, Bramki[1]);

			Bramki.Add(new Bramka(XOR));
			Bramki.Last().UstawWtykiWejsciowe(LiczbaA[3], LiczbaB[3]);
			Bramki.Add(new Bramka(XOR));
			Bramki.Last().UstawWtykiWejsciowe(new Wtyk(false), new Wtyk(false)); //pierwsze niewazne bo tam bedzie polaczenie, drugie to przeniesienie
			Bramki.Last().PodlaczDoWejscia(0, Bramki[0]);

			foreach (var bramka in Bramki)
				bramka.WykonajDzialanie();
			Console.WriteLine("Wyjscie ostatniej bramki: " + Bramki[Bramki.Count - 1].Wyjscie.Wartosc.ToString01());
		}
		bool AND(bool a, bool b)
		{
			Debug.WriteLine("Wykonuje {0} AND {1}= {2}",a.ToString01(), b.ToString01(), (a&b).ToString01());
			return a & b;
		}
		bool OR(bool a, bool b)
		{
			Debug.WriteLine("Wykonuje {0} OR {1}= {2}", a.ToString01(), b.ToString01(), (a|b).ToString01());
			return a | b;
		}
		bool XOR(bool a, bool b)
		{
			Debug.WriteLine("Wykonuje {0} XOR {1}= {2}", a.ToString01(), b.ToString01(), (a ^ b).ToString01());
			return a ^ b;
		}
		
	}
}

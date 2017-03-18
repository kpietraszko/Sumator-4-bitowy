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
			string A = "1001"; string B = "1000";
			Wtyk[] LiczbaA = new Wtyk[4];
			Wtyk[] LiczbaB = new Wtyk[4];
			Wtyk[] Wynik = new Wtyk[4];
			Wtyk Przeniesienie = new Wtyk(false); //przeniesienie wejściowe pierwszego sumatora to 0
			for (int i = 0; i < 4; i++) //tworzenie wtykow bitow wejsciowych i nadanie wartosci ze stringow
			{
				bool bit;
				bit = A[i] == '1';
				LiczbaA[i] = new Wtyk(bit);
				bit = B[i] == '1';
				LiczbaB[i] = new Wtyk(bit);
			}
			for (int i = 3; i >= 0; i--)
			{
				Bramka BramkaXor = new Bramka(XOR);
				Bramki.Add(BramkaXor);
				BramkaXor.UstawWtykiWejsciowe(LiczbaA[i], LiczbaB[i]);
				Bramki.Add(new Bramka(XOR));
				Bramki.Last().UstawWtykiWejsciowe(BramkaXor.Wyjscie, Przeniesienie);
				Wynik[i] = Bramki.Last().Wyjscie;
				Bramka ANDGorny = new Bramka(AND);
				Bramki.Add(ANDGorny);
				ANDGorny.UstawWtykiWejsciowe(LiczbaA[i], LiczbaB[i]);
				Bramka ANDDolny = new Bramka(AND);
				Bramki.Add(ANDDolny);
				ANDDolny.UstawWtykiWejsciowe(Przeniesienie, BramkaXor.Wyjscie);
				Bramka BramkaOR = new Bramka(OR);
				Bramki.Add(BramkaOR);
				BramkaOR.UstawWtykiWejsciowe(ANDGorny.Wyjscie, ANDDolny.Wyjscie);
				Przeniesienie = BramkaOR.Wyjscie;
			}

			foreach (var bramka in Bramki)
				bramka.WykonajDzialanie();
			Console.Write("Wynik: " + Przeniesienie.Wartosc.ToString01());
			foreach (var wtyk in Wynik)
				Console.Write(wtyk.Wartosc.ToString01());
			Console.Write(Environment.NewLine);
	
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Sumator4bitowy
{
	delegate bool Dzialanie(bool a, bool b);
	class Bramka
	{
		Wtyk[] Wejscia;
		Wtyk _Wyjscie;
		public Wtyk Wyjscie
		{
			get { return _Wyjscie; }
		}
		Dzialanie DzialanieBramki;
		public Bramka(Dzialanie dzialanie)
		{
			DzialanieBramki = dzialanie;
			_Wyjscie = new Wtyk();
			Wejscia = new Wtyk[2];
		}
		public void UstawWtykiWejsciowe(params Wtyk[] wtykiWe)
		{
			Wejscia[0] = wtykiWe[0];
			Wejscia[1] = wtykiWe[1];
		}
		public void PodlaczDoWejscia(int doceloweWejscie, Bramka poprzednia)
		{
			Wejscia[doceloweWejscie] = poprzednia.Wyjscie;
		}
		public void WykonajDzialanie()
		{
			_Wyjscie.Wartosc = DzialanieBramki(Wejscia[0].Wartosc, Wejscia[1].Wartosc);
		}
	}
}

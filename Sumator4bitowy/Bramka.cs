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
			if(wtykiWe.Length == 2)
				Wejscia = wtykiWe;
		}
		public void WykonajDzialanie()
		{
			_Wyjscie.Wartosc = DzialanieBramki(Wejscia[0].Wartosc, Wejscia[1].Wartosc);
		}
	}
}

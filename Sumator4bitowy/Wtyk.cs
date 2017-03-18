using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sumator4bitowy
{
	class Wtyk
	{
		Wtyk PolaczonyWtyk;
		bool _Wartosc;
		public bool Wartosc
		{
			get { return _Wartosc; }
			set
			{
				_Wartosc = value;
				if(PolaczonyWtyk != null)
					PolaczonyWtyk.Wartosc = value; //nieskonczone zapetlenie
			}
		}
		public Wtyk(){ }
		public Wtyk(Wtyk polaczony)
		{
			PolaczonyWtyk = polaczony;
		}
		public Wtyk(bool wartosc) //wtyk z dana wartoscia zamiast podlaczonego innego wtyku
		{
			Wartosc = wartosc;
		}
		public void Podlacz(Wtyk inny)
		{
			System.Diagnostics.Debug.Assert(inny != null);
			System.Diagnostics.Debug.Assert(inny != this); //nie mozna podlaczyc wtyku do niego samego
			PolaczonyWtyk = inny;
			inny.PolaczonyWtyk = this;
			//Wartosc = PolaczonyWtyk.Wartosc; //nadaje polaczonemu ta sama wartosc, raczej zly pomysl bo sugeruje zmiane polaczen podczas wykonywania
		}
	}
}

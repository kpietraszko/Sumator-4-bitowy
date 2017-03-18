using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sumator4bitowy
{
	static class MetodyRozszerzajace
	{
		public static string ToString01(this Boolean boolean)
		{
			return boolean == true ? "1" : "0";
		}
	}
}

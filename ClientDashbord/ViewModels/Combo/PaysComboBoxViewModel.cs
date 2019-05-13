using System.Collections.Generic;
using System.Linq;

namespace ClientDashbord.ViewModels.Combo
{
	public class PaysComboBoxViewModel
	{
		public IEnumerable<string> Pays { get; set; }

		public PaysComboBoxViewModel()
		{
			this.Pays = new List<string>
			{
				"Suisse",
				"Australie",
				"Angleterre",
				"Espagne",
				"Japon",
				"France",
				"USA",
				"Emirates",
				"Allemagne",
				"Nouvelle Angleterre",
				"Italie",
				"Toscane",
				"République Tchèque",
				"Brésil",
				"Belgique",
				"Abu Dhabi",
			}.OrderBy(o => o);
		}
	}
}
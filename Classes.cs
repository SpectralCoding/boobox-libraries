using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace BooBox {

	public class MenuStripNoGradient : ProfessionalColorTable {
		public override Color MenuStripGradientBegin { get { return SystemColors.Control; } }
		public override Color MenuStripGradientEnd { get { return SystemColors.Control; } }
	}

}

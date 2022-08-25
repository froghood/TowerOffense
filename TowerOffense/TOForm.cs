using System;
using System.Windows.Forms;

namespace TowerOffense {
    public class TOForm : Form {
        protected override void OnLayout(LayoutEventArgs levent) {

            ClientSize = new(60, 60);

            base.OnLayout(levent);
        }
    }
}
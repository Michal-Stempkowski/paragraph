using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataLayer.Logic;
using DataLayer.Top;

namespace GUI
{
    public interface IEntityCreator
    {
        void CreateDecisionButtons(Panel decisionPanel, EntityMenu entityMenu);
    }

    class PresenterEntityCreator : IEntityCreator
    {
        public void CreateDecisionButtons(Panel decisionPanel, EntityMenu entityMenu)
        {
            throw new NotImplementedException();
        }
    }

    class EditorEntityCreator: IEntityCreator
    {
        public void CreateDecisionButtons(Panel decisionPanel, EntityMenu entityMenu)
        {
            var button = new Button {};
            decisionPanel.Controls.Add(button);
        }
    }

}

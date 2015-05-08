using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lines.GameEngine;
using Lines.GameEngine.Enums;

namespace Lines.DesktopUI
{
    public class DesktopGameEngineWrapper
    {
        #region Private Fields

        private Game _game;
        
        #endregion

        public DesktopGameEngineWrapper(Game game)
        {
            _game = game;

            SubscribeGameEvents();
        }

        private void SubscribeGameEvents()
        {
            throw new NotImplementedException();
        }



    }
}

using System;
using System.Media;

namespace Lines.DesktopUI
{
    public class Sound
    {
        #region Fields

        private SoundPlayer _sndCancel;
        private SoundPlayer _sndMove;
        private SoundPlayer _sndScore;

        #endregion

        #region Constructors

        public Sound()
        {
            _sndCancel = new System.Media.SoundPlayer(Properties.Resources.Cancel);
            _sndMove = new System.Media.SoundPlayer(Properties.Resources.BubbleDrop);
            _sndScore = new System.Media.SoundPlayer(Properties.Resources.Scoring);

        }

        #endregion

        #region Methods

        public void PlayCancelSound(object sender, EventArgs e)
        {
            _sndCancel.Play();
        }

        public void PlayMoveSound(object sender, EventArgs e)
        {
            _sndMove.Play();
        }

        public void PlayScoreSound(object sender, EventArgs e)
        {
            _sndScore.Play();
        }

        #endregion
    }
}

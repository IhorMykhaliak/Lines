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
        private SoundPlayer _sndPathNotExist;
        #endregion

        #region Constructors

        public Sound()
        {
            _sndCancel = new SoundPlayer(Properties.Resources.Cancel);
            _sndMove = new SoundPlayer(Properties.Resources.BubbleDrop);
            _sndScore = new SoundPlayer(Properties.Resources.Scoring);
            _sndPathNotExist = new SoundPlayer(Properties.Resources.PathNotExist);
            IsSoundOn = true;
        }

        #endregion

        #region Properties

        public bool IsSoundOn { get; set; }

        #endregion


        #region Methods

        public void PlayCancelSound(object sender, EventArgs e)
        {
            if (IsSoundOn)
            {
                _sndCancel.Play();
            }
        }

        public void PlayMoveSound(object sender, EventArgs e)
        {
            if (IsSoundOn)
            {
                _sndMove.Play();
            }
        }

        public void PlayScoreSound(object sender, EventArgs e)
        {
            if (IsSoundOn)
            {
                _sndScore.Play();
            }
        }

        public void PathNotExistSound(object sender, EventArgs e)
        {
            if (IsSoundOn)
            {
                _sndPathNotExist.Play();
            }
        }

        #endregion
    }
}

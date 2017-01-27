using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WPFPlayground.Annotations;
using XDMessaging;

namespace WPFPlayground
{
    public class ViewModelTest : INotifyPropertyChanged
    {
        private readonly XDMessagingClient xdClient = new XDMessagingClient();
        private readonly XDTransportMode XdTransportMode = XDTransportMode.HighPerformanceUI;
        private string ChannelName = "TestChannel";

        public ViewModelTest()
        {
            var listener = xdClient.Listeners.GetListenerForMode(XdTransportMode);
            listener.RegisterChannel(ChannelName);
            listener.MessageReceived += (sender, args) => Dispatcher.CurrentDispatcher.Invoke(() => { NewsTime = true; }, DispatcherPriority.Background);
        }


        #region NewsTime Property (INotifyPropertyChanged)

        /// <summary>
        /// Interne Speicherung der Property <see cref="NewsTime"/>.
        /// </summary>
        private bool newsTimeValue;
        public bool NewsTime
        {
            get { return newsTimeValue; }
            set
            {
                newsTimeValue = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region PublishNews Command

        /// <summary>
        /// Interne Speicherung der Property <see cref="PublishNewsCommand"/>.
        /// </summary>
        private RelayCommand publishNewsCommandValue;

        /// <summary>
        /// Command zur Ausführung der Methode <see cref="PublishNews"/>.
        /// </summary>
        public ICommand PublishNewsCommand
        {
            get
            {
                if (publishNewsCommandValue == null) publishNewsCommandValue = new RelayCommand(param => this.PublishNews(param));
                return publishNewsCommandValue;
            }
        }

        //TODO: PublishNews-Methode in ViewModelTest kommentieren
        public void PublishNews(Object parameter)
        {
            xdClient.Broadcasters.GetBroadcasterForMode(XdTransportMode).SendToChannel(ChannelName, "MessageId");

        }

        #endregion  

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
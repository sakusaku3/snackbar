using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;

namespace snackbar
{
	public class MainViewModel : ViewModelBase
	{
        public DelegateCommand SendMessageCommand { get; }
        public DelegateCommand SendMessageCommand2 { get; }

        public MainViewModel()
		{
            this.Count = 0;
 
            this.MessageQueue1 = new SnackbarMessageQueue();
            this.MessageQueue2 = new SnackbarMessageQueue();

            this.SendMessageCommand = new DelegateCommand(this.SendMessage, () => true);
            this.SendMessageCommand2 = new DelegateCommand(this.SendActionMessage, () => true);
		}

        #region Count変更通知プロパティ
        private int _Count;
 
        public int Count
        {
            get
            { return _Count; }
            set
            {
                this.SetProperty(ref this._Count, value);
            }
        }
        #endregion
 
        public void CountUp()
        {
            Count++;
        }
 
        public SnackbarMessageQueue MessageQueue1 { get; }
 
        public void SendMessage()
        {
            MessageQueue1.Enqueue("メッセージ:" + DateTime.Now);
        }
 
        public SnackbarMessageQueue MessageQueue2 { get; }
 
 
 
        #region Total変更通知プロパティ
        private int _Total;
 
        public int Total
        {
            get
            { return _Total; }
            set
            { 
                this.SetProperty(ref this._Total, value);
            }
        }
        #endregion
 
 
        public void SendActionMessage()
        {
            Total = 0;
            foreach(var n in Enumerable.Range(1, 5))
            {
                MessageQueue2.Enqueue(
                    "メッセージ:" + n,
                    "カウントアップ",
                    () => Total++);
            }
        }
	}
}

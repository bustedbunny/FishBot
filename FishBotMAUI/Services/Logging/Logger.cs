using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishBotMAUI.Services.Logging
{
    public class Logger
    {


        private readonly ObservableCollection<string> _messages = new();

        public ObservableCollection<string> Messages => _messages;



        public void Message(string message)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"{DateTime.Now.ToLongTimeString()}: ");
            builder.AppendLine(message);
            _messages.Add(builder.ToString());
        }

        public void Clear()
        {
            _messages.Clear();
        }
    }
}

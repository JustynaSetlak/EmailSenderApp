using System;
using System.Collections.Generic;
using System.Text;

namespace EmailSenderApp.Options
{
    public class SentEmailMessagesQueueOptions
    {
        public string QueueConnectionString { get; set; }

        public string QueueName { get; set; }
    }
}

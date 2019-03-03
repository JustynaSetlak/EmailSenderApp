using System;

namespace EmailSender.Dtos.Requests
{
    public class EmailMessageDto
    {
        public EmailMessageDto()
        {
            Identifier = Guid.NewGuid();   
        }

        public Guid Identifier { get; set; }

        public string To { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }
    }
}

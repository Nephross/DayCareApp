using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DayCareApp.Web.Entities
{
    public class ChatMessage
    {

        [Key]
        public int ChatMessageId { get; set; }

        public string Message { get; set; }

        public string ApplicationUserId { get; set; }

        public DayRegistration DayRegistration { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WcfHotelService.Common.Entities;

namespace WcfHotelService.Common.Exceptions
{
    /// <summary>
    /// Custom exception class is defined to serve for multiple purposes
    /// Along side of holding stack trace it can also hold multiple user friendly message
    /// it will also be used for server side validation to hold multiple messages
    /// </summary>
    public class CustomException : System.Exception
    {
        /// <summary>
        /// to hold multiple values
        /// </summary>
        public List<CustomMessage> Messages
        {
            get;
            set;
        }

        /// <summary>
        /// Mandatory Constructor for base class
        /// </summary>
        public CustomException() : base ()
        {
        }
        
        /// <summary>
        /// Mandatory Constructor for base class
        /// </summary>
        /// <param name="message"></param>
        public CustomException(string message)
            : base(message)
        {
        }
        
        /// <summary>
        /// Mandatory Constructor for base class
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public CustomException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Additional constructor for holding multiple user friendly  messages
        /// it can also be used for validation
        /// </summary>
        /// <param name="code"></param>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public CustomException(List<CustomMessage> lstCustomMessage, string message, System.Exception innerException)
            : base(message, innerException)
        {
            this.Messages = lstCustomMessage;
        }
    }
}

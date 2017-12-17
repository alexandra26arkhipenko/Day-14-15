using System;

namespace BusinessLogic.Interfaces.Interfaces
{
    public interface IEmailService
    {
        void SendMail(MailData mailData);
    }
    public class MailData
    {
        #region private fields

        private string _from;
        private string _fromPassword;
        private string _to;

        #endregion // !private fields.

        #region constructor

        public MailData()
        {
        }

        public MailData(string from, string fromPassword, string to, string subject, string message)
        {
            From = from;
            FromPassword = fromPassword;
            To = to;
            Subject = subject;
            Message = message;
        }

        #endregion // !constructor.

        #region properties

        public string From
        {
            get => _from;

            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(nameof(value));
                }
                _from = value;
            }
        }

        public string FromPassword
        {
            get => _fromPassword;

            set
            {

                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(nameof(value));
                }
                _fromPassword = value;
            }
        }

        public string To
        {
            get => _to;

            set
            {

                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(nameof(value));
                }
                _to = value;
            }
        }

        public string Subject { get; set; }

        public string Message { get; set; }

        #endregion 

       }
}
using System.Collections.Generic;

namespace Core.Sms.Dtos
{
    public class SmsDto
    {
        public SmsDto()
        {
            Phones = new List<string>();
            Message = "";
            Options = new SmsOptions();
        }

        public SmsDto(string phone, string message, SmsOptions options)
        {
            Phones.Add(phone);
            Message = message;
            Options = options;
        }

        public SmsDto(IEnumerable<string> phones, string message, SmsOptions options)
        {
            Phones.AddRange(phones);
            Message = message;
            Options = options;
        }

        public List<string> Phones { get; set; } = new();
        public string Message { get; set; }

        public SmsOptions Options { get; set; }

        public class SmsOptions
        {
            public SmsOptions()
            {
                UserName = "";
                Password = "";
                Title = "";
            }
            public SmsOptions(string userName, string password, string title)
            {
                UserName = userName;
                Password = password;
                Title = title;
            }
            public string UserName { get; set; }
            public string Password { get;set ; }

           

            public string Title { get; set; }
        }
    }
}
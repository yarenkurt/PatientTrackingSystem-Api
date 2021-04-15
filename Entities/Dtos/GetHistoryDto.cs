using System;

namespace Entities.Dtos
{
    public class GetHistoryDto
    {
        public int FullName { get; set; }
        public bool IsSuccess { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
    }
}
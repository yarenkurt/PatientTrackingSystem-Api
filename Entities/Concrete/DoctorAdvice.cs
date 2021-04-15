﻿using System;
using System.Text.Json.Serialization;
using Core.Signatures;

namespace Entities.Concrete
{
    public class DoctorAdvice : IBaseEntity
    {
        public int Id { get; set; }
        public string Description { get; set; }
        
        [JsonIgnore]
        public int DepartmentId { get; set; }
        public string CreatedUserName { get; set; } //Claimsten alınacak
        public int PatientId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ReadingTime { get; set; }

        [JsonIgnore]
        public Department Department { get; set; }
    }
}
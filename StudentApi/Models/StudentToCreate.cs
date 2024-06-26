﻿using System.ComponentModel.DataAnnotations;

namespace StudentApi.Models
{
    public class StudentToCreate
    {
        public StudentToCreate()
        {
        }

        public StudentToCreate(string firstName, string lastName, string phoneNumber, string groupName)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            GroupName = groupName;
        }


        public int StudentId { get; private set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        [MaxLength(5)]
        public string GroupName { get; set; }

        public List<CourseToCreate> CoursesToCreate { get; set; } = new List<CourseToCreate>();
    }
}
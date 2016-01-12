using FMI.WeAzure.Boxing.Contracts.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FMI.WeAzure.Boxing.Api
{
    /// <summary>
    /// Poor man's database
    /// Also, not thread-safe, yolo
    /// </summary>
    internal static class AwesomeDataRepository
    {
        private static List<User> users = new List<User>()
            {
                new User() { Id = 1, UserName = "Test1", Password = "Omg" },
                new User() { Id = 2, UserName = "Test2", Password = "Omg2" },
                new User() { Id = 3, UserName = "Test3", Password = "Omg3" },
            };

        public static IList<User> Users
        {
            get
            {
                return users;
            }
        }
    }
}
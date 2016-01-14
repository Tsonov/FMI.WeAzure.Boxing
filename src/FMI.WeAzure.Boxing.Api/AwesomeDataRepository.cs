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

        private static List<Login> logins = new List<Login>();

        private static List<Match> matches = new List<Match>();

        public static IList<User> Users
        {
            get
            {
                return users;
            }
        }

        public static IList<Login> Logins
        {
            get
            {
                return logins;
            }
        }

        public static IList<Match> Matches { get { return matches; } }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using Model;

namespace DataAccess
{
    /// <summary>
    /// The User Repositry Contains Methods, which arent standards(Add,Update,Delete etc). This Class extends from the Abstract Class AbstractRepository 
    /// </summary>
    /// <seealso cref="User" />
    public class UserRepository : AbstractRepository<User>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public UserRepository(TrainContext context) : base(context)
        {
        }

        /// <summary>
        /// Top4 Situps Recrds from DB set in a List.
        /// </summary>
        /// <returns></returns>
        public List<User> Top4ListPushUps()
        {
            return Context.Users.OrderByDescending(u => u.RecordPushups).Take(4).ToList();
        }

        /// <summary>
        /// Top4 Situps Recrds from DB set in a List.
        /// </summary>
        /// <returns></returns>
        public List<User> Top4ListSitUps()
        {
            return Context.Users.OrderByDescending(u => u.RecordSitups).Take(4).ToList();
        }

        /// <summary>
        /// Checks the login. If Password and Email are Correct
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public bool CheckLogin(string email, string password)
        {
            return Context.Users.Any(u => u.Password == password && u.Email == email);
        }

        /// <summary>
        /// Gets the user identifier by mail.
        /// </summary>
        /// <param name="mail">The mail.</param>
        /// <returns></returns>
        public int GetUserIdByMail(string mail)
        {
            return Context.Users.Single(u => u.Email == mail).Id;
        }

        /// <summary>
        /// Checks if email exist for register.
        /// </summary>
        /// <param name="mail">The mail.</param>
        /// <returns></returns>
        public bool CheckIfEmailexist(string mail)
        {
            return Context.Users.Any(u => u.Email == mail);
        }

        /// <summary>
        /// Gets the average for push ups from Exercise Table.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public List<int> GetAVGForPushUps(int id)
        {
            return Context.Scores.Where(s => s.UserID == id && s.Exercise_Id == 1).Select(score => score.Score1).ToList();
        }

        /// <summary>
        /// Gets the average for sit ups from Exercise Table.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public List<int> GetAVGForSitUps(int id)
        {
            return Context.Scores.Where(s => s.UserID == id && s.Exercise_Id == 2).Select(score => score.Score1).ToList();
        }
    }
}

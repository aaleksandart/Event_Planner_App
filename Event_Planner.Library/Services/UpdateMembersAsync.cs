﻿using Event_Planner.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Planner.Library.Services
{
    public class UpdateMembersAsync
    {
        /// <summary>
        /// Method that overwrites the textfile with an updated list of Members. 
        /// </summary>
        /// <param name="memberList">Updated list of members</param>
        /// <param name="filepath">Filepath for textfile</param>
        /// <returns>Updated list</returns>
        public async Task<List<Member>> UpdateMembersToFileAsync(List<Member> memberList, string filepath)
        {
            using (StreamWriter updateWriter = new(filepath, append: false))
            {
                foreach (Member memberUpdate in memberList)
                {
                    await updateWriter.WriteLineAsync($"{memberUpdate.Id} {memberUpdate.FullName} {memberUpdate.Email}");
                }
                updateWriter.Close();
            }
            return memberList;
        }
    }
}

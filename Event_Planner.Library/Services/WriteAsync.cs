using Event_Planner.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Event_Planner.Library.Services
{
    public class WriteAsync
    {
        /// <summary>
        /// Method that writes a new member to a textfile.
        /// </summary>
        /// <param name="member">Member to write to file</param>
        /// <param name="filepath">Filepath of the textfile</param>
        /// <returns>Bool(Return of this method is only there for testing)</returns>
        public async Task<bool> WriteToFileAsync(Member member, string filepath)
        {
            using (StreamWriter fileWriter = new(filepath, append: true))
            {
                await fileWriter.WriteLineAsync($"{member.Id} {member.FullName} {member.Email}");
                fileWriter.Close();
                return true;
            }
        }
    }
}

using Event_Planner.Library.Models;
using Event_Planner.Library.Services;
using System.Threading.Tasks;
using System.Linq;
using Xunit;
using System.Collections.Generic;
using System;

namespace Event_Planner.Tests
{
    /// <summary>
    /// Since all testing is async it worked better to have them in one class for all services.
    /// </summary>
    public class Services_Should
    {
        //Filepath for same textfile 'Members' as in MainWindow(located in project folder)
        string filePath = @"C:\Users\aleks\Desktop\C_Projects\Event_Planner\Members.txt";
        
        /// <summary>
        /// Test for adding member.
        /// </summary>
        [Fact]
        public async Task Add_Member()
        {
            //Arrange
            AddMember sut = new AddMember();

            //Act
            Member member = await sut.AddMemberAsync("Aleksandar", "Todorovic", "a@gmail.com", filePath);

            //Assert
            Assert.NotNull(member);
            Assert.True(member.Id.ToString().Length > 0);
        }

        /// <summary>
        /// Test for writing to file.
        /// </summary>
        [Fact]
        public async Task Write_To_File()
        {
            //Arrange
            Member member = new Member { Id = Guid.NewGuid(), FirstName = "Ganjana", LastName = "Thinpho", Email = "g@gmail.com" };
            WriteAsync sut = new WriteAsync();

            //Act
            bool running = await sut.WriteToFileAsync(member, filePath);

            //Assert
            Assert.True(running);
        }

        /// <summary>
        /// Test for reading from file.
        /// </summary>
        [Fact]
        public async Task Read_From_File()
        {
            //Arrange
            ReadAsync sut = new ReadAsync();

            //Act
            List<Member> members = await sut.ReadFromFileAsync(filePath);

            //Assert
            Assert.NotEmpty(members);
            Assert.NotNull(members);
            foreach (Member member in members)
            {
                Assert.IsType<Member>(member);
            }   
        }

        /// <summary>
        /// Test for updating and overwriting file.
        /// </summary>
        [Fact]
        public async Task Update_File()
        {
            //Arrange
            UpdateMembersAsync sut = new UpdateMembersAsync();
            ReadAsync reader = new ReadAsync();
            List<Member> membersNotUpdated = await reader.ReadFromFileAsync(filePath);
            List<Member> membersUpdated =   new List<Member>();
            Member member = new Member { Id = Guid.NewGuid(), FirstName = "Daniel", LastName = "Todorovic", Email = "d@gmail.com" };
            membersUpdated.Add(member);
            
            //Act
            membersUpdated = await sut.UpdateMembersToFileAsync(membersUpdated, filePath);
            
            //Assert
            Assert.True(membersUpdated != membersNotUpdated);
            Assert.NotEmpty(membersUpdated);
            Assert.NotEqual(membersUpdated.Count, membersNotUpdated.Count);
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Event_Planner.Library.Models;
using Event_Planner.Library.Services;

namespace Event_Planner
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Change this textfile path. The file 'Members' is located in project folder.
        string filePath = @"C:\Users\aleks\Desktop\C_Projects\Event_Planner\Members.txt";

        List<Member> membersFromFileList = new List<Member>();

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Using ReadFromFileAsync to get Members from to textfile and show them in the listview.
        /// </summary>
        private async void btnShowList_Click(object sender, RoutedEventArgs e)
        {
            ReadAsync read = new ReadAsync();
            membersFromFileList = await read.ReadFromFileAsync(filePath);
            lvMembers.Items.Clear();
            foreach (Member member in membersFromFileList)
            {
                lvMembers.Items.Add(member);
            }
        }

        /// <summary>
        /// If all fields for Add member is filled.
        /// Using AddMemberAsync to create a new Member with the inputs from the user.
        /// </summary>
        private async void btnAddMember_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(inputFirstName.Text) && !string.IsNullOrEmpty(inputLastName.Text) && !string.IsNullOrEmpty(inputEmail.Text))
            {
                AddMember add = new AddMember();
                Member member = (await add.AddMemberAsync(inputFirstName.Text.Trim(), inputLastName.Text.Trim(), inputEmail.Text.Trim(), filePath));
                lvMembers.Items.Add(member);
                membersFromFileList.Add(member);
                inputFirstName.Text = "";
                inputLastName.Text = "";
                inputEmail.Text = "";
            }
        }

        /// <summary>
        /// When you click the button it generates a new VIP code, discount code for Members.
        /// </summary>
        private void btnGenerateCode_Click(object sender, RoutedEventArgs e)
        {
            Guid code = Guid.NewGuid();
            outputGenerateCode.Text = $"VIP-{code.ToString()}";
        }

        /// <summary>
        /// If a Member in the accepted listview is highlighted.
        /// Removing this Member and puts it in cancelled members listview.
        /// Using UpdateMembersToFileAsync to remove it from textfile.
        /// </summary>
        private async void btnCancelMember_Click(object sender, RoutedEventArgs e)
        {
            if(lvMembers.SelectedItems.Count > 0)
            {
                lvMembersCancelled.Items.Add(lvMembers.SelectedItem);
                if(lvMembersCancelled.Items.Count > 0)
                {
                    foreach (Member member in lvMembersCancelled.Items)
                    {
                        if (membersFromFileList.Contains(member) == true)
                        {
                            membersFromFileList.Remove(member);
                        }
                    }
                }
                lvMembers.Items.RemoveAt(lvMembers.SelectedIndex);
                UpdateMembersAsync update = new UpdateMembersAsync();
                membersFromFileList = await update.UpdateMembersToFileAsync(membersFromFileList, filePath);
            }
        }
    }
}

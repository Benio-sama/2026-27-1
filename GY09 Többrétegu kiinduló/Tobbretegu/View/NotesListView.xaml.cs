using System;
using System.IO;
using System.Xml.Serialization;
using Tobbretegu.Model;
using Microsoft.UI.Xaml.Controls;
using Tobbretegu.Logic;

namespace Tobbretegu.View
{
    public sealed partial class NotesListView : UserControl
    {
        private NoteService allNotes;
        private UserService allUsers;

        /// <summary>
        /// The user currently selected as tab.
        /// </summary>
        private User SelectedUser = null;

        /// <summary>
        /// The notes for the selected user; filtered from all notes.
        /// </summary>
        private NoteList DisplayedList { get; set; } = new NoteList();

        public NotesListView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Loads data from the files.
        /// </summary>
        private void NavView_Loaded(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            // read files
            this.allNotes = new NoteService();
            this.allUsers = new UserService();

            // add tab pages
            foreach (var user in allUsers.ListAll())
                navView.MenuItems.Insert(navView.MenuItems.Count - 1, new NavigationViewItem() { Tag = user.Id, Content = user.Name });

            // select the first user by default
            navView.SelectedItem = navView.MenuItems[0];
        }

        /// <summary>
        /// When a user name tab is selected.
        /// </summary>
        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            // selected user's id
            var userId = args.SelectedItemContainer.Tag as Guid?;
            if (!userId.HasValue)
                return;

            SelectedUser = allUsers.Find(userId);
            tbUserName.Text = SelectedUser.Name;

            // refresh the displayed list of notes on the UI
            DisplayedList.Clear();
            foreach (var item in allNotes.FilterNotes(SelectedUser.Id))
                DisplayedList.Add(item);
        }

        /// <summary>
        /// Delet button of a note
        /// </summary>
        private void BtnDelete_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            // this is the note to delete
            var note = (Note)((Button)sender).DataContext;

            // remove from the notes
            allNotes.Remove(note);

            // remove from the currently displayed list
            DisplayedList.Remove(note);
        }

        /// <summary>
        /// Add new note.
        /// </summary>
        private void BtnAddNewNote_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            // create a new note with dummy date
            var newNote = new Note()
            {
                ID = Guid.NewGuid(),
                Assignee = SelectedUser.Id,
                Title = "Add the title here",
                Description = "Add to text here"
            };

            // add to notes list
            allNotes.Add(newNote);

            // add to currently displayed list
            DisplayedList.Add(newNote);
        }

        /// <summary>
        /// Save existing note
        /// </summary>
        private void BtnSave_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            allNotes.SaveNotesToFile();
        }

        /// <summary>
        /// Change current user name.
        /// </summary>
        private void BtnChangeName_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            // make sure user has a name
            if (tbUserName.Text.Length == 0)
                return;

            // update the users list
            string newUserName = tbUserName.Text;
            allUsers.ChangeName(SelectedUser.Id, newUserName);

            // update the displayed name on the UI
            SelectedUser.Name = newUserName;
            ((NavigationViewItem)navView.SelectedItem).Content = newUserName;
        }

        /// <summary>
        /// Add a new user.
        /// </summary>
        private void BtnAddNewUser_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            // create a new user
            string newUserName = allUsers.GetNewUserName();
            var newUser = new User()
            {
                Id = Guid.NewGuid(),
                Name = newUserName,
            };

            // add to users list
            allUsers.Add(newUser);

            // add menu item on UI
            navView.MenuItems.Insert(navView.MenuItems.Count - 1, new NavigationViewItem() { Tag = newUser.Id, Content = newUser.Name });
            navView.SelectedItem = navView.MenuItems[navView.MenuItems.Count - 2];
        }

        /// <summary>
        /// Generates random new unique name for a user.
        /// </summary>
        

        /// <summary>
        /// checks if a user with the same name already exists
        /// </summary>
        

        
        

        
    }
}

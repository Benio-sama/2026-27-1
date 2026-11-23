using System;

namespace Tobbretegu.Model
{
    public class Note : ObservableObjectBase
    {
        private Guid id;
        public Guid ID
        {
            get => id;
            set => SetAndNotifyIfChanged(ref id, value);
        }

        private string title;
        public string Title
        {
            get => title;
            set => SetAndNotifyIfChanged(ref title, value);
        }

        private string description;
        public string Description
        {
            get => description;
            set => SetAndNotifyIfChanged(ref description, value);
        }

        private Guid assignee;
        public Guid Assignee
        {
            get => assignee;
            set => SetAndNotifyIfChanged(ref assignee, value);
        }
    }
}

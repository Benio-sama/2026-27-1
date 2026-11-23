using System;

namespace Tobbretegu.Model
{
    public class User : ObservableObjectBase
    {
        private Guid id;
        public Guid Id
        {
            get => id;
            set => SetAndNotifyIfChanged(ref id, value);
        }

        private string name;
        public string Name
        {
            get => name;
            set => SetAndNotifyIfChanged(ref name, value);
        }
    }
}

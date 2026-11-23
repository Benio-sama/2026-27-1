using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Tobbretegu.Model
{
    public class ObservableObjectBase : INotifyPropertyChanged
    {
        protected void SetAndNotifyIfChanged<T>(ref T attribute, T newValue, [CallerMemberName] string attributeOriginalName = "")
        {
            if (!EqualityComparer<T>.Default.Equals(attribute, newValue))
            {
                attribute = newValue;
                Notify(attributeOriginalName);
            }
        }

        protected void Notify([CallerMemberName] string PropertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
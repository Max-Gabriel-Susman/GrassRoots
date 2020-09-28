using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;


namespace GrassRoots.Models
{
    public class DropsUser : INotifyPropertyChanged
    {
        // PROPERTIES
        public event PropertyChangedEventHandler PropertyChanged;

        string username;
        [JsonProperty("id")]
        public string Username
        {
            get => username;
            set
            {
                if (username == value)
                    return;

                username = value;

                HandlePropertyChanged();
            }
        }

        // METHODS
        void HandlePropertyChanged([CallerMemberName] string propertyName = "")
        {
            var eventArgs = new PropertyChangedEventArgs(propertyName);

            PropertyChanged?.Invoke(this, eventArgs);
        }


        public override string ToString()
        {
            return Username;
        }
    }
}

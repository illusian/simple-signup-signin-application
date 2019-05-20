using System;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using demoapp.Model;
using demoapp.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using XLabs;

namespace demoapp.ViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {
        public string baseApiAddr = "<HOST_ADDR>";

        private RelayCommand _RegCommand, _LoginCommand;
        public ICommand RegCommand => this._RegCommand ?? (this._RegCommand = new RelayCommand(Regist, CanRegist));
        public ICommand LoginCommand => this._LoginCommand ?? (this._LoginCommand = new RelayCommand(Login, CanLogin));

        public UserViewModel()
        {
        }

        private bool CanRegist()
        {
            return true;
        }

        private void Regist()
        {
            UserModel user = new UserModel();
            user.firstName = this.firstName;
            user.lastName = this.lastName;
            user.email = this.email;
            var bdate = this.birthday;
            string hr_bdate = bdate.ToString("yyyy-MM-dd");
            user.birthday = hr_bdate;
            user.password = this.password;
            user.password_confirmation = this.password_confirmation;
            user.terms_and_conditions = this.tnc;

            string apiurl = baseApiAddr + "<URI>";
            HttpClient client = new HttpClient(new LoggingHandler(new HttpClientHandler()));

            var uri = new Uri(apiurl);
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = Task.Run(() => client.PostAsync(uri, content)).Result;

                var result = response.Content.ReadAsStringAsync().Result;
                System.Diagnostics.Debug.WriteLine(result);
                JObject r = JObject.Parse(result);

                MessagingCenter.Send<string, string>("app", "signup", $"{r["message"]}");
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                MessagingCenter.Send<string, string>("app", "signup", "error");
            }


        }

        private bool CanLogin()
        {
            return true;
        }

        private void Login()
        {
            var isValid = AreCredentialsCorrect();
            if (isValid)
            {
                loginModel login = new loginModel();
                login.email = this.email;
                login.password = this.password;

                string apiurl = baseApiAddr + "<URI>";
                HttpClient client = new HttpClient(new LoggingHandler(new HttpClientHandler()));

                var uri = new Uri(apiurl);
                var json = JsonConvert.SerializeObject(login);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                try
                {
                    HttpResponseMessage response = Task.Run(() => client.PostAsync(uri, content)).Result;
                    MessagingCenter.Send<string, string>("app", "login", $"{response.StatusCode}");
                }
                catch (HttpRequestException ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex);
                    MessagingCenter.Send<string, string>("app", "login", "error");
                }
            }
            else
            {
                MessagingCenter.Send<string, string>("app", "login", "null");
            }
        }

        bool AreCredentialsCorrect()
        {
            return (!string.IsNullOrWhiteSpace(this.email) && !string.IsNullOrWhiteSpace(this.password));
        }

        private string _firstName;
        public string firstName
        {
            get
            {
                return _firstName;
            }

            set
            {
                if (_firstName == value) return;
                _firstName = value;
                OnPropertyChanged("firstName");
            }
        }

        private string _lastName;
        public string lastName
        {
            get
            {
                return _lastName;
            }

            set
            {
                if (_lastName == value) return;
                _lastName = value;
                OnPropertyChanged("lastName");
            }
        }

        private string _email;
        public string email
        {
            get
            {
                return _email;
            }

            set
            {
                if (_email == value) return;
                _email = value;
                OnPropertyChanged("email");
            }
        }

        private string _password;
        public string password
        {
            get
            {
                return _password;
            }

            set
            {
                if (_password == value) return;
                _password = value;
                OnPropertyChanged("password");
            }
        }

        private string _password_confirmation;
        public string password_confirmation
        {
            get
            {
                return _password_confirmation;
            }

            set
            {
                if (_password_confirmation == value) return;
                _password_confirmation = value;
                OnPropertyChanged("password_confirmation");
            }
        }

        private DateTime _birthday;
        public DateTime birthday
        {
            get
            {
                return _birthday;
            }

            set
            {
                if (_birthday == value) return;
                _birthday = value;
                OnPropertyChanged("birthday");
            }
        }

        private string _tnc;
        public string tnc
        {
            get
            {
                return _tnc;
            }

            set
            {
                if (_tnc == value) return;
                _tnc = value;
                OnPropertyChanged("tnc");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}




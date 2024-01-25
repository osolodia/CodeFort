using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFort.Models.Entity
{
    public class ProgramData : INotifyPropertyChanged
    {
        // Событие, сигнализирующее об изменении свойств объекта
        public event PropertyChangedEventHandler? PropertyChanged;

        // Метод вызывается при изменении свойств, уведомляет подписчиков об изменении
        protected virtual void OnPropertyChanged(string propertyName)
        {
            // Проверка наличия подписчиков перед вызовом события
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Идентификатор объекта ProgramData
        public int Id { get; set; }

        // Имя объекта ProgramData
        private string? name;
        public string? Name
        {
            get { return name; }
            set
            {
                // Установка нового значения для свойства
                name = value;

                // Уведомление об изменении свойства Name
                OnPropertyChanged(nameof(Name));
            }
        }

        // Логин объекта ProgramData
        private string? login;
        public string? Login
        {
            get { return login; }
            set
            {
                // Установка нового значения для свойства
                login = value;

                // Уведомление об изменении свойства Login
                OnPropertyChanged(nameof(Login));
            }
        }

        // Пароль объекта ProgramData
        private string? password;
        public string? Password
        {
            get { return password; }
            set
            {
                // Установка нового значения для свойства
                password = value;

                // Уведомление об изменении свойства Password
                OnPropertyChanged(nameof(Password));
            }
        }
    }
}

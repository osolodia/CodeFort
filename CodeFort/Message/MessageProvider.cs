using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFort.Message
{
    public class MessageProvider
    {
        // Приветственное сообщение для первого входа пользователя
        public static string WelcomeFirstLogin
            => "Введите ваше имя и пароль для входа. Нажмите кнопку \"Зарегистрироваться\", чтобы сохранить данные.";

        // Приветствие для возвращающегося пользователя, включая его имя
        public static string WelcomeReturningUser(string userName)
            => $"{userName}, для входа введите пароль и нажмите кнопку \"Войти\".";

        // Сообщение об ошибке, если не заполнены все поля
        public static string IncorrectData
            => "Для продолжения заполните все поля.";

        // Сообщение об ошибке при вводе неверного пароля
        public static string IncorrectPassword
            => "Неверный пароль.";

        // Сообщение о небезопасном пароле и рекомендации
        public static string UnsafePassword
            => "Введен небезопасный пароль. " +
            "Используйте пароль, " +
            "содержащий комбинацию букв, цифр и специальных символов, не менее 8 символов.";

        // Сообщение с подтверждением удаления данных для входа
        public static string DeleteDataConfirmation
            => "Вы уверены, что хотите удалить данные для входа? " +
            "Вся связанная с ними информация также будет удалена без возможности восстановления. " +
            "Пожалуйста, подтвердите свое решение, так как это действие необратимо.";
        public static string DeleteItem
           => "Вы уверены, что хотите удалить данные?";
        public static string IncorectData
            => "Выберите данные для работы с ними";

        // Получение приветствия в зависимости от времени суток
        public static string GetGreeting(DateTime dateTime)
        {
            if (dateTime.Hour >= 5 && dateTime.Hour < 12)
                return "Доброе утро!";
            else if (dateTime.Hour >= 12 && dateTime.Hour < 18)
                return "Добрый день!";
            else if (dateTime.Hour >= 18 && dateTime.Hour < 22)
                return "Добрый вечер!";
            else
                return "Доброй ночи!";
        }
    }
}

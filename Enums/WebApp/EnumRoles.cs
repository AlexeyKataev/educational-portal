namespace Dotnet.Enums.WebApp
{
    /// <summary>
    /// Роли пользователей
    /// </summary>
    public enum EnumRoles : byte
    {
        /// <summary>
        /// Не указано
        /// </summary>
        Unsettled = 0,
        
        /// <summary>
        /// Главный администратор
        /// </summary>
        Admin = 1,

        /// <summary>
        /// Системный администратор
        /// </summary>
        SystemAdmin = 2,

        /// <summary>
        /// Сотрудник отдела кадров
        /// </summary>
        HumanResources = 3,

        /// <summary>
        /// Сотрудник учебного отдела
        /// </summary>
        TrainingDivision = 4,

        /// <summary>
        /// Автор статей
        /// </summary>
        AuthorArticles = 5,

        /// <summary>
        /// Преподаватель
        /// </summary>
        Teacher = 6,

        /// <summary>
        /// Абитуриент
        /// </summary>
        Enrolle = 7,

        /// <summary>
        /// Выпускник
        /// </summary>
        Graduate = 8,

        /// <summary>
        /// Студент
        /// </summary>
        Student = 9,

        /// <summary>
        /// Пользователь
        /// </summary>
        User = 10,
    }
}

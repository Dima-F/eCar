namespace eCar.Applicaton.Models
{
    /// <summary>
    /// Общий клас, от котороко наследуются все модели представления. Включает в себя встроенный класс LayoutBaseModel, который в свою очередь
    /// содержит данные о аутентификации пользователя, его права, дружественное имя, тема, а также данные сайта:Заголовок, Мета-описание, горизонтальную колонку,
    /// Имя пользователя в твиттере и т.д. Это так называемая база, которую должны содержать все модели представления приложения (проверка выполняется на уровне
    /// LayoutController в перегруженом методе OnActionExecuted)
    /// </summary>
    public class LayoutModel
    {
        //[NoBinding]
        public LayoutBaseModel Base { get; set; }

        public class LayoutBaseModel
        {
            public bool IsAuthenticated { get; set; }
            public bool IsAdmin { get; set; }
            public string FriendlyUsername { get; set; }
            public string Theme { get; set; }
            public string SiteTitle { get; set; }
            public string SiteMetaDescription { get; set; }
            public string SiteHeading { get; set; }
            public string SiteTagline { get; set; }
            public string Crossbar { get; set; }
            public string GoogleAnalyticsId { get; set; }
            public string TwitterUsername { get; set; }
        }
    }
}
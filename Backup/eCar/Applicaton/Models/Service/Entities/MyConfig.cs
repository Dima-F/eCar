using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace eCar.Applicaton.Models.Service.Entities
{
    public class MyConfig:ConfigurationSection
    {
        [ConfigurationProperty("site", IsRequired = false, DefaultValue = "localhost-8080")]
        public string Site { 
            get { return (string) base["site"]; } 
            set { base["site"] = value; } 
        }
        [ConfigurationProperty("theme", IsRequired = false, DefaultValue = "default")]
        public string Theme
        {
            get { return (string)base["theme"]; }
            set { base["theme"] = value; }
        }
        [ConfigurationProperty("title", IsRequired = false, DefaultValue = "eCar")]
        public string Title
        {
            get { return (string)base["title"]; }
            set { base["title"] = value; }
        }
        [ConfigurationProperty("metaDescription", IsRequired = false, DefaultValue = "eCar, an experimental ASP.NET MVC 3.0 e-commerce platform.")]
        public string MetaDescription
        {
            get { return (string)base["metaDescription"]; }
            set { base["metaDescription"] = value; }
        }
        [ConfigurationProperty("heading", IsRequired = false, DefaultValue = "eCar")]
        public string Heading
        {
            get { return (string)base["heading"]; }
            set { base["heading"] = value; }
        }
        [ConfigurationProperty("tagLine", IsRequired = false, DefaultValue = "An experimental ASP.NET MVC 3.0 e-commerce platform")]
        public string TagLine
        {
            get { return (string)base["tagLine"]; }
            set { base["tagLine"] = value; }
        }
        [ConfigurationProperty("crossbar", IsRequired = false, DefaultValue = "ASP.NET MVC 3.0, Razor, JSON.NET, Markdown, Ninject, AntiXSS, HTML5 & CSS3, OpenID, Mock, ELMAH")]
        public string Crossbar
        {
            get { return (string)base["crossbar"]; }
            set { base["crossbar"] = value; }
        }
        [ConfigurationProperty("googleAnalyticsId", IsRequired = false, DefaultValue = "")]
        public string GoogleAnalyticsId
        {
            get { return (string)base["googleAnalyticsId"]; }
            set { base["googleAnalyticsId"] = value; }
        }
        [ConfigurationProperty("twitterUsername", IsRequired = false, DefaultValue = "twitter")]
        public string TwitterUsername
        {
            get { return (string)base["twitterUsername"]; }
            set { base["twitterUsername"] = value; }
        }
        [ConfigurationProperty("showCarsInHomePage", IsRequired = false, DefaultValue = 6)]
        public int ShowCarsInHomePage
        {
            get { return (int)base["showCarsInHomePage"]; }
            set { base["showCarsInHomePage"] = value; }
        }
        [ConfigurationProperty("shortDescription", IsRequired = false, DefaultValue = 25)]
        public int ShortDescription
        {
            get { return (int)base["shortDescription"]; }
            set { base["shortDescription"] = value; }
        }
        [ConfigurationProperty("contactForm", IsRequired = true)]
        public ContactFormConfig ContactForm
        {
            get { return (ContactFormConfig)base["contactForm"]; }
            set { base["contactForm"] = value; }
        }
        [ConfigurationProperty("paypal", IsRequired = true)]
        public PayPal PayPal
        {
            get { return (PayPal)base["paypal"]; }
            set { base["paypal"] = value; }
        }
        [ConfigurationProperty("cloud", IsRequired = true)]
        public Cloud Cloud
        {
            get { return (Cloud)base["cloud"]; }
            set { base["cloud"] = value; }
        }
    }
    public class Cloud : ConfigurationElement
    {
        [ConfigurationProperty("consumerKey", IsRequired = true)]
        public string ConsumerKey
        {
            get { return (string)base["consumerKey"]; }
            set { base["consumerKey"] = value; }
        }
        [ConfigurationProperty("consumerSecret", IsRequired = true)]
        public string ConsumerSecret
        {
            get { return (string)base["consumerSecret"]; }
            set { base["consumerSecret"] = value; }
        }
        [ConfigurationProperty("userName", IsRequired = true)]
        public string UserName
        {
            get { return (string)base["userName"]; }
            set { base["userName"] = value; }
        }
        [ConfigurationProperty("password", IsRequired = true)]
        public string Password
        {
            get { return (string)base["password"]; }
            set { base["password"] = value; }
        }
    }
    public class PayPal : ConfigurationElement
    {
        [ConfigurationProperty("sandboxMode", IsRequired = true)]
        public bool SandboxMode
        {
            get { return (bool)base["sandboxMode"]; }
            set { base["sandboxMode"] = value; }
        }
        [ConfigurationProperty("business", IsRequired = true)]
        public string Business
        {
            get { return (string)base["business"]; }
            set { base["business"] = value; }
        }
        [ConfigurationProperty("currencyCode", IsRequired = true)]
        public string CurrencyCode
        {
            get { return (string)base["currencyCode"]; }
            set { base["currencyCode"] = value; }
        }
    }
    public class ContactFormConfig:ConfigurationElement
    {
        [ConfigurationProperty("recipientName", IsRequired = true)]
        public string RecipientName
        {
            get { return (string)base["recipientName"]; }
            set { base["recipientName"] = value; }
        }
        [ConfigurationProperty("recipientEmail", IsRequired = true)]
        public string RecipientEmail
        {
            get { return (string)base["recipientEmail"]; }
            set { base["recipientEmail"] = value; }
        }
        [ConfigurationProperty("subject", IsRequired = false, DefaultValue = "hello from dima!")]
        public string Subject
        {
            get { return (string)base["subject"]; }
            set { base["subject"] = value; }
        }
    }
}
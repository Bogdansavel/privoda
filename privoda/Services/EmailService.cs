using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using privoda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace privoda.Services
{
    public class EmailService
    {

        private readonly IConfiguration _config;

        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void SendOrder(string name, string org, string post, string email, string phone, ModelLine line)
        {
            var emailConfig = _config.GetSection("Email");
            var adminEmail = emailConfig["AdminEmail"];
            var emailForOrders = emailConfig["EmailForOrder"];
            var emailPassword = emailConfig["EmailPassword"];

            var adress = new MailAddress(adminEmail);

            using (var m = new MailMessage(adress, adress))
            {
                using (var smtp = new SmtpClient("smtp.yandex.ru", 587))
                {
                    m.Subject = "Заказ через сайт privoda.by";
                    m.Body = "Заказ " + line.Name + ". \n\nЗаказчик: " + name + "\nОрганизация: " + org;
                    if (post != null)
                    {
                        m.Body += "\nДолжность: " + post;
                    }
                    m.Body += "\nЭлектронная почта: " + email + "\nТелефон: " + phone + ";";
                    smtp.Credentials = new NetworkCredential(emailForOrders, emailPassword);
                    smtp.EnableSsl = true;
                    smtp.Send(m);
                }
            }
        }

        public void SendSelect(string normal, string hard, string workSequences, string inputOrganization, string power, string current, string speed, string cos, string problem, string email)
        {
            var emailConfig = _config.GetSection("Email");
            var adminEmail = emailConfig["AdminEmail"];
            var emailForSelect = emailConfig["EmailForSelect"];
            var emailPassword = emailConfig["EmailPassword"];

            var to = new MailAddress(emailForSelect);
            var from = new MailAddress(adminEmail);

            using (var m = new MailMessage(from, to))
            {
                using (var smtp = new SmtpClient("smtp.yandex.ru", 587))
                {
                    m.Subject = "Заявка на подбор решения через сайт privoda.by";
                    if (!string.IsNullOrWhiteSpace(normal))
                    {
                        m.Body += "\nРабочий режим нормальный: " + normal;
                    }

                    if (!string.IsNullOrEmpty(hard))
                    {
                        m.Body += "\nРабочий режим тяжёлый: " + hard;
                    }

                    if (!string.IsNullOrEmpty(workSequences))
                    {
                        m.Body += "\nЦиклограмма работы: " + workSequences;
                    }

                    m.Body += "\nНапряжение питания: " + inputOrganization + "\nПараметры двигаеля:\n  Мощность [кВт]: " + power + "\n  Ток в обмотке [А]:" + current;

                    if (!string.IsNullOrEmpty(current))
                    {
                        m.Body += "\n  Скорость вращения [об/мин]: " + speed;
                    }

                    if (!string.IsNullOrEmpty(cos))
                    {
                        m.Body += "\n  Cos(f): " + cos;
                    }

                    if (!string.IsNullOrEmpty(problem))
                    {
                        m.Body += "\nОписаие проблемы: " + problem;
                    }

                    m.Body += "\nЭлектронная почта: " + email;
                    smtp.Credentials = new NetworkCredential(adminEmail, emailPassword);
                    smtp.EnableSsl = true;
                    smtp.Send(m);
                }
            }
        }

        public void SendConfigurationRefs(string email, string phone,
            string teSys1FullRef, string teSys1CircuitBreakersRef, string teSys1CoilRef, string teSys1ContactorRef,
            string teSys2FullRef, string teSys2CircuitBreakersRef, string teSys2CoilRef, string teSys2ContactorRef)
        {
            var emailConfig = _config.GetSection("Email");
            var adminEmail = emailConfig["AdminEmail"];
            var emailForSelect = emailConfig["EmailForSelect"];
            var emailPassword = emailConfig["EmailPassword"];

            var to = new MailAddress(emailForSelect);
            var from = new MailAddress(adminEmail);

            using (var m = new MailMessage(from, to))
            {
                using (var smtp = new SmtpClient("smtp.yandex.ru", 587))
                {
                    m.Subject = "Запрос комерческого предложения через сайт privoda.by";
                    m.Body =
                        "Пускатели прямого непосредственного пуска.\n" +
                        "\nКоординация типа 1." +
                        "\nАвтоматический выключатель электродвигателя: " + teSys1CircuitBreakersRef +
                        "\nКонтактор: " + teSys1ContactorRef +
                        "\nКатушка: " + teSys1CoilRef +
                        "\nПолный референс: " + teSys1FullRef +
                        "\n\nКоординация типа 2." +
                        "\nАвтоматический выключатель электродвигателя: " + teSys2CircuitBreakersRef +
                        "\nКонтактор: " + teSys2ContactorRef +
                        "\nКатушка: " + teSys2CoilRef +
                        "\nПолный референс: " + teSys2FullRef;
                    m.Body += "\n\nКонтакты: " +
                        "\nE-mail: " + email;
                    if (!string.IsNullOrEmpty(phone))
                    {
                        m.Body += "\nТелефон: " + phone;
                    }
                    smtp.Credentials = new NetworkCredential(emailForSelect, emailPassword);
                    smtp.EnableSsl = true;
                    smtp.Send(m);
                }
            }
        }
    }
}

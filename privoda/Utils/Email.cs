using Microsoft.Extensions.Options;
using privoda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace privoda.Utils
{
    public class Email
    {

        private readonly Config config;

        public Email(Config config)
        {
            this.config = config;
        }

        public void sendOrder(string name, string org, string post, string email, string phone, ModelLine line)
        {
            MailAddress adress = new MailAddress(config.EmailForOrder);
            MailMessage m = new MailMessage(adress, adress);
            m.Subject = "Заказ через сайт privoda.by";
            m.Body = "Заказ " + line.Name + ". \n\nЗаказчик: " + name + "\nОрганизация: " + org;
            if (post != null)
            {
                m.Body += "\nДолжность: " + post;
            }
            m.Body += "\nЭлектронная почта: " + email + "\nТелефон: " + phone + ";";
            SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587);
            smtp.Credentials = new NetworkCredential(config.EmailForOrder, config.EmailPassword);
            smtp.EnableSsl = true;
            smtp.Send(m);
        }

        public void sendSelect(string normal, string hard, string workSequences, string inputOrganization, string power, string current, string speed, string cos, string problem, string email)
        {
            MailAddress to = new MailAddress(config.EmailForSelect);
            MailAddress from = new MailAddress(config.AdminEmail);
            MailMessage m = new MailMessage(from, to);

            m.Subject = "Заявка на подбор решения через сайт privoda.by";
            if (!String.IsNullOrWhiteSpace(normal))
            {
                m.Body += "\nРабочий режим нормальный: " + normal;
            }

            if (!String.IsNullOrEmpty(hard))
            {
                m.Body += "\nРабочий режим тяжёлый: " + hard;
            }

            if (!String.IsNullOrEmpty(workSequences))
            {
                m.Body += "\nЦиклограмма работы: " + workSequences;
            }

            m.Body += "\nНапряжение питания: " + inputOrganization + "\nПараметры двигаеля:\n  Мощность [кВт]: " + power + "\n  Ток в обмотке [А]:" + current;

            if (!String.IsNullOrEmpty(current))
            {
                m.Body += "\n  Скорость вращения [об/мин]: " + speed;
            }

            if (!String.IsNullOrEmpty(cos))
            {
                m.Body += "\n  Cos(f): " + cos;
            }

            if (!String.IsNullOrEmpty(problem))
            {
                m.Body += "\nОписаие проблемы: " + problem;
            }

            m.Body += "\nЭлектронная почта: " + email;
            SmtpClient smtp = new SmtpClient("smtp.yandex.ru", 587);
            smtp.Credentials = new NetworkCredential(config.AdminEmail, config.EmailPassword);
            smtp.EnableSsl = true;
            smtp.Send(m);
        }

    }
}

using SelfSignalR2._0.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Linq;

namespace SelfSignalR2._0.Controllers
{
    public class XmlHelper
    {
        /// <summary>
        /// 读取xml
        /// </summary>
        /// <returns></returns>
        public static List<UserInfo> ReaderXml(string xmlFilePath)
        {
            try
            {
                List<UserInfo> usList = new List<UserInfo>();
                //将XML文件加载进来
                XDocument document = XDocument.Load(xmlFilePath);
                //获取到XML的根元素进行操作
                XElement root = document.Root;
                //获取根元素下的所有子元素
                IEnumerable<XElement> enumerable = root.Elements();
                foreach (XElement item in enumerable)
                {
                    UserInfo us = new UserInfo();
                    us.UserId = item.Element("UserId") == null || string.IsNullOrEmpty(item.Element("UserId").Value) ? "" : item.Element("UserId").Value;
                    us.UserName = item.Element("UserName") == null || string.IsNullOrEmpty(item.Element("UserName").Value) ? "" : item.Element("UserName").Value;
                    us.ConnectionId = item.Element("ConnectionId") == null || string.IsNullOrEmpty(item.Element("ConnectionId").Value) ? "" : item.Element("ConnectionId").Value;
                    us.LastLoginTime = item.Element("LastLoginTime") == null || string.IsNullOrEmpty(item.Element("LastLoginTime").Value) ? DateTime.Now : Convert.ToDateTime(item.Element("LastLoginTime").Value);
                    usList.Add(us);
                }
                return usList;
            }
            catch (Exception ex)
            {
                Writelog(ex.Message);
                return null;
            }
        }

        /// <summary>
        /// 写入xml
        /// </summary>
        /// <param name="us"></param>
        /// <param name="xmlFilePath"></param>
        public static void WriteXml(UserInfo us, string xmlFilePath)
        {
            try
            {
                //将XML文件加载进来
                XDocument document = XDocument.Load(xmlFilePath);
                //获取到XML的根元素进行操作
                XElement root = document.Root;
                XElement book = new XElement("UserInfo");
                book.SetElementValue("UserId", us.UserId);
                book.SetElementValue("UserName", us.UserName);
                book.SetElementValue("ConnectionId", us.ConnectionId);
                book.SetElementValue("LastLoginTime", us.LastLoginTime.ToString("yyyy-MM-dd HH:mm:ss"));
                root.Add(book);
                root.Save(xmlFilePath);
            }
            catch (Exception ex)
            {
                Writelog(ex.Message);
            }
        }

        /// <summary>
        /// 更新xml
        /// </summary>
        /// <param name="us"></param>
        /// <param name="xmlFilePath"></param>
        public static void UpdateXml(UserInfo us, string xmlFilePath)
        {
            try
            {
                //将XML文件加载进来
                XDocument document = XDocument.Load(xmlFilePath);
                //获取到XML的根元素进行操作
                XElement root = document.Root;
                //获取根元素下的所有子元素
                IEnumerable<XElement> enumerable = root.Elements();
                foreach (XElement item in enumerable)
                {
                    string userId = item.Element("UserId") == null || string.IsNullOrEmpty(item.Element("UserId").Value) ? "" : item.Element("UserId").Value;
                    if (userId == us.UserId)
                    {
                        item.SetElementValue("UserId", us.UserId);
                        item.SetElementValue("UserName", us.UserName);
                        item.SetElementValue("ConnectionId", us.ConnectionId);
                        item.SetElementValue("LastLoginTime", us.LastLoginTime.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                }
                
                root.Save(xmlFilePath);
            }
            catch (Exception ex)
            {
                Writelog(ex.Message);
            }
        }

        /// <summary>
        /// 写的普通日志
        /// </summary>
        /// <param name="msg"></param>
        public static void Writelog(string msg)
        {
            StreamWriter stream;
            //写入日志内容
            string path = AppDomain.CurrentDomain.BaseDirectory;
            //检查上传的物理路径是否存在，不存在则创建
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            stream = new StreamWriter(path + "\\log.txt", true, Encoding.Default);
            stream.Write(DateTime.Now.ToString() + ":" + msg);
            stream.Write("\r\n");
            stream.Flush();
            stream.Close();
        }
    }
}
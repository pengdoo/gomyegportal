//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen 创建于 2008-5-6 19:53:26
//
// 功能描述: 实现数据对象的临时存储功能 
//
// 修改标识: 
// 修改描述: 
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Gomye.CommonClassLib.Data
{
   public class TempDataHelper
    {
       /// <summary>
       /// 存储临时数据
       /// </summary>
       /// <param name="obj">数据对象</param>
       /// <param name="path">临时文件夹路径</param>
       /// <returns></returns>
       public static Guid Saver(object obj,string path)
       {
           Guid sid = Guid.NewGuid();
           string rpath = string.Format("{0}{1}.tdh", path, sid.ToString());
           try
           {
               //打开文件
               StreamWriter fs = new StreamWriter(rpath, false);
               try
               {
                   // 创建其支持存储区为内存的流
                   MemoryStream streamMemory = new MemoryStream();
                   // 以二进制格式将对象或整个连接对象图形序列化或反序列化
                   BinaryFormatter formater = new BinaryFormatter();
                   //将这个对象序列化到内存流中
                   formater.Serialize(streamMemory, obj);
                   //先转换为字符串的形式
                   string binaryData = Convert.ToBase64String(streamMemory.GetBuffer());

                   //将数据 写入到文件
                   fs.Write(binaryData);
               }
               catch (Exception ex)
               {
                   throw ex;
               }
               finally
               {
                   fs.Flush();
                   fs.Close();
                  
                   CacheHelper.SetCache(sid.ToString(), obj, rpath);
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }

           return sid;
       }

       /// <summary>
       /// 读取临时数据
       /// </summary>
       /// <param name="path">临时文件夹路径</param>
       /// <param name="sid">编号</param>
       /// <param name="isdispose">读取完是否删除</param>
       /// <returns></returns>
       public static object Loader(string path, Guid sid,bool isdispose)
       {
           string rpath = string.Format("{0}{1}.tdh", path, sid.ToString());
           object data = null;
           //仅在文件存在时读取
           if (File.Exists(rpath))
           {
               CacheHelper.GetCache(sid.ToString());
               //预备缓存意外失效的情况
               if (data == null)
               {
                   try
                   {
                       //打开文件
                       StreamReader sr = new StreamReader(rpath);
                       try
                       {
                           MemoryStream streamMemory;
                           BinaryFormatter formatter = new BinaryFormatter();
                           //以字符串的形式读取数据
                           string cipherData = sr.ReadToEnd();

                           byte[] binaryData = Convert.FromBase64String(cipherData);
                           //反序列化为对象
                           streamMemory = new MemoryStream(binaryData);
                           data = formatter.Deserialize(streamMemory);
                       }
                       catch
                       {
                           // 不能得到数据,设为空
                           data = null;
                       }
                       finally
                       {
                           //最后关闭文件
                           sr.Close();
                       }

                   }
                   catch
                   {
                       // 不能得到数据,设为空
                       data = null;
                   }
               }
           }
            //删除数据
           if (isdispose == true)
           {
               CacheHelper.Remove(sid.ToString());
               File.Delete(rpath);
           }
            return data;
        }

       public static void Remove(string path, Guid sid)
       {
           string rpath = string.Format("{0}{1}.tdh", path, sid.ToString());
           CacheHelper.Remove(sid.ToString());
           File.Delete(rpath);
       }
     }
 }


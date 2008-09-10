//------------------------------------------------------------------------------
// 创建标识: Copyright (C) 2008 Gomye.com.cn 版权所有
// 创建描述: Galen Mu 创建于 2008-7-9
//
// 功能描述: 扩展字段访问
//
// 已修改问题:
// 未修改问题:
// 修改记录
//       1   2008-7-9 添加注释
//       2   2008-8-26 VB语法转换为C#
//       3   2008-9-4 添加UserWhere对象，修改Top(int),Chanels()方法
//------------------------------------------------------------------------------
using System.Diagnostics;
using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System;
using System.Xml;
using GCMSClassLib.Content;
using GCMSClassLib.Public_Cls;
using System.Data.SqlClient;
using System.Runtime.InteropServices;



namespace GCMSContentCreate
{
    [Guid("C23A58D7-3DC4-4524-A4D9-EFD731136830")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class GCMS
    {
         string[] baseColNames ={  "Name", "DerivationLink", 
            "PictureDName", "SubmitDate", "PublishDate", "PublishDate", "PictureNotes",
            "Content","Author","Url","KeyWord","Original","ReCount","Clicks",
              "ContentType"};
        
        private int m_contentid;
        public int ContentID
        {
            get { return m_contentid; }
            set
            {
                m_contentid = value;
                if (m_contentid != 0)
                {
                    if (CurrentList != null && CurrentList.ContainsKey(m_contentid))
                    {

                        CurrentItem = CurrentList[m_contentid];
                        Debug.WriteLine("CurrentItem:从当前列表中读取" + m_contentid);
                    }
                    else
                    {
                        Debug.WriteLine("CurrentItem:从数据库中读取" + m_contentid);
                        LoadCurrentItem(ChannelID, m_contentid);
                    }
                }
            }
        }


        private Hashtable CurrentItem;
        public int ChannelID;
        private Dictionary<int,Hashtable> CurrentList;
        public int _ContentID;
        public int _ChannelID;
        public int ReID;
        public int _ReID;



        public string GetChannel;
        public string PgCount; //共多少页
        public string AllCount; //共多少条
        public int PgThis; //当前页
        public string PgDn;
        public string PgUp;
        public string Order;
        public int ListTop;
        public string ListLastID;
        public int PageID = 0;
        private string _userwhere;
        private string _userwherecolname;
        /// <summary>
        /// 定义用户的自定义筛选
        /// </summary>
        public string UserWhere
        {
            get
            {
                if (String.IsNullOrEmpty(this._userwhere))
                    return "";
                else
                    return " and ( " + this._userwhere + " ) ";
            }
            set 
            {
                string returnValue = string.Empty;
                try
                {
                    string orgSetString = value.Replace("\r\n", "");
                    string[] sets = orgSetString.Split(',');
                    string sql = string.Empty;
                    foreach (string set in sets)
                    {
                        string setStr = set.Replace("[", "");
                        setStr = setStr.Replace("]", "");
                        string[] paramStrs = setStr.Split(':');
                        string colsName = string.Format("[{0}]", paramStrs[0].Trim());
                        _userwherecolname = colsName;
                        string opStr = paramStrs[1].Trim().Substring(0, 1);
                        switch (opStr)
                        {
                            case ">":
                                opStr = ">";
                                break;
                            case "<":
                                opStr = "<";
                                break;
                            case "=":
                                opStr = "=";
                                break;
                            case "!":
                                opStr = "<>";
                                break;
                            default:
                                opStr = "=";
                                break;
                        }

                        string selectValue = paramStrs[1].Trim();
                        //防注入
                        string forbidenStrs = "and|exec|insert|select|delete|update|count|*|%|chr|mid|master|truncate|char|declare| ";
                        foreach (string fs in forbidenStrs.Split('|'))
                        {
                            selectValue = selectValue.Replace(fs, "");
                        }
                        foreach (string sv in selectValue.Split('$'))
                        {
                            sql += string.Format("{0} {1} {2} or  ", colsName, opStr, sv);
                        }
                        sql += "'1'='2'";//恒否的表达式，拼接 or串 
                        returnValue += string.Format("({0}) and ", sql);
                    }
                }
                catch 
                {
                    _userwhere=string.Empty;
                }
                 returnValue+="'1'='1'";//恒真的表达式，拼接 and 串 
                returnValue = string.Format(" {0} ", returnValue);
                _userwhere= returnValue;
            }
        }

        /// <summary>
        /// 初始化脚本预置对象(内容页模板使用)
        /// </summary>
        /// <param name="Content_ID"></param>
        /// <param name="TypeTree_ID"></param>
        public GCMS(int Content_ID, int TypeTree_ID)
        {
            Debug.WriteLine(string.Format("以内容页模式初始化GCMS对象,ContentID={0},TypeTree_ID={1}",Content_ID,TypeTree_ID));
            
            ChannelID = TypeTree_ID;
            _ChannelID = TypeTree_ID;

            ContentID = Content_ID;
            _ContentID = Content_ID;
        }
        public GCMS()
        {
            Debug.WriteLine("以默认模式初始化GCMS对象");
        }
        //-----------------------------------------------------------
        //内容
        //-----------------------------------------------------------
        public object Content(string Item)
        {
            string[] custnames = new string[] { "Url", "PictureName", "PictureDName", "PictureNews", "PictureNotes", "User_ID", "Content_Xml" };
            string[] dbnames = new string[] { "URL", "Picture_Name", "Picture_DName", "Picture_news", "Picture_Notes", "User_id", "content_xml" };
            for (int i = 0; i < custnames.Length; i++)
            {
                if (Item == custnames[i])//兼容历史模板的引用名和数据库实际字段名称不一致问题
                {
                    if (CurrentItem.ContainsKey(dbnames[i])) return CurrentItem[dbnames[i]];
                }
            }
           
            Debug.WriteLine("Conetnt函数调用,参数:" , Item);
            bool hasValue = CurrentItem.ContainsKey(Item);
            object returnValue = hasValue ? CurrentItem[Item] : string.Empty;
            return returnValue;
        }

        #region 预读数据
        private Childs LoadCurrentList(string sql,string keyname)
        {
            Childs Contents = new Childs();
            DataTable listdt = Tools.DoSqlTable(sql);
            CurrentList = new Dictionary<int, Hashtable>();//初始化当前列表
            foreach (DataRow dr in listdt.Rows)
            {
                Hashtable currentitem = new Hashtable();
                Child chair = new Child();
                if (listdt.Columns.Contains("Content_ID"))
                {
                    chair.ContentID =int.Parse( dr["Content_ID"].ToString());
                    currentitem.Add("ContentID", dr["Content_ID"].ToString());
                }
                if (listdt.Columns.Contains("Url") )
                {
                    chair.Url = dr["Url"].ToString();
                }
                if (listdt.Columns.Contains("Name"))
                {
                    chair.Name = dr["Name"].ToString();
                }
                if (listdt.Columns.Contains("TypeTree_ID"))
                {
                    chair.ChannelID = int.Parse(dr["TypeTree_ID"].ToString());
                    currentitem.Add("ChannelID", dr["TypeTree_ID"].ToString());
                }
                if (listdt.Columns.Contains("Remark_ID"))
                {
                    chair.ReID = int.Parse(dr["Remark_ID"].ToString());
                }

                if (listdt.Columns.Contains("Content_PID"))
                {
                    currentitem.Add("ContentPID", dr["Content_PID"].ToString());
                }
          
                foreach (DataColumn dc in listdt.Columns)
                {
                    string colname = dc.ColumnName;
                    Console.WriteLine("CurrentList:添加" + colname);
                    if (!currentitem.ContainsKey(colname))
                    {
                        currentitem.Add(colname, dr[colname].ToString());
                    }
                }


                string sid = dr[keyname].ToString();
               
                CurrentList.Add(chair.ContentID, currentitem);//更新当前列表
                Contents.Contents.Add(chair,sid , null, null);
                ChannelID = int.Parse(dr["TypeTree_ID"].ToString());
            }
            return Contents;
        }
        private Hashtable LoadCurrentItem(int TypeTree_ID, int Content_ID)
        {
            ContentCls ContentCls = new ContentCls();
            ContentCls.Init(Content_ID);
             Type_TypeTree _Type_TypeTree = new Type_TypeTree();
             int typetreeid = TypeTree_ID == 0 ? ContentCls.TypeTree_ID : TypeTree_ID;

             _Type_TypeTree.Init(typetreeid);

            CurrentItem = new Hashtable();//初始化当前对象

            if (_Type_TypeTree.TypeTree_Type == 2||_Type_TypeTree.TypeTree_Type == 0)//兼容AOC的产品
            {
                Content_FieldsName Content_FieldsName = new Content_FieldsName();
                int contenfiled = _Type_TypeTree.TypeTree_TypeFields == 0 ? _Type_TypeTree.TypeTree_ContentFields : _Type_TypeTree.TypeTree_TypeFields;
                Content_FieldsName.Init(contenfiled);
                SqlDataReader reader = Tools.DoSqlReader("Select * from ContentUser_" + Content_FieldsName.FieldsBase_Name + " Where content_Id=" + Content_ID);
                int colsCount = reader.FieldCount;
                if (reader.Read())
                {
                    for (int i = 0; i < colsCount; i++)
                    {
                        CurrentItem.Add(reader.GetName(i), reader[i].ToString());
                    }
                    CurrentItem.Add("ContentID", reader["Content_ID"].ToString());
                    CurrentItem.Add("ChannelID", reader["TypeTree_ID"].ToString());
                    if (CurrentItem.ContainsKey("Content_PID"))//兼容AOC的产品
                    {
                        CurrentItem.Add("ContentPID", reader["Content_PID"].ToString());
                    }
                }
               
                reader.Close();
            }
            else
            {
                CurrentItem.Add("ContentID", ContentCls.ContentId);
                CurrentItem.Add("SubmitDate", ContentCls.SubmitDate);
                CurrentItem.Add("PublishDate", ContentCls.PublishDate);
                CurrentItem.Add("ChannelID", ContentCls.TypeTree_ID == 0 ? ChannelID : ContentCls.TypeTree_ID);
                CurrentItem.Add("Author", ContentCls.Author);
                CurrentItem.Add("ContentPID", ContentCls.Content_PID);
                CurrentItem.Add("Url", ContentCls.Url);
                CurrentItem.Add("KeyWord", ContentCls.KeyWord);
                CurrentItem.Add("Original", ContentCls.Original);
                CurrentItem.Add("ReCount", ContentCls.ReCount);
                CurrentItem.Add("Clicks", ContentCls.Clicks);
                CurrentItem.Add("ContentType", ContentCls.ContentType);
            }
            CurrentItem.Add("Name", ContentCls.Name);
            CurrentItem.Add("DerivationLink", ContentCls.DerivationLink);
            CurrentItem.Add("Derivation", ContentCls.Derivation);
            CurrentItem.Add("PictureName", ContentCls.PictureName);//*名称不同
            CurrentItem.Add("PictureDName", ContentCls.PictureNameD);//*名称不同
            CurrentItem.Add("PictureNotes", ContentCls.PictureNotes);
            CurrentItem.Add("Content", xmlSplit(Tools.DBToWeb(ContentCls.Description)).ToString());
            //-------- 电子商务支持 ---------------------------------------------------------
            CurrentItem.Add("Spec", ContentCls.DerivationLink);
            CurrentItem.Add("Price", ContentCls.Derivation);
            CurrentItem.Add("MarketPrice", ContentCls.KeyWord);
            CurrentItem.Add("ESpec", ContentCls.DerivationLink);
            return CurrentItem;
        }
        #endregion 预读数据

        public int PContent(string Item, int intI)
        {
            int returnValue;
            ContentID = (int)Content("ContentPID");
            ChannelID = intI;
            returnValue = (int)Content(Item);
            ContentID = _ContentID;
            ChannelID = _ChannelID;
            return returnValue;
        }

        public string ThisPageUrl(int intI)
        {
            string returnValue;
            ContentCls ContentCls = new ContentCls();
            ContentCls.Init(ContentID);
            string fileExtension;
            if (intI == 1)
            {
                returnValue = ContentCls.Url;
            }
            else
            {
                fileExtension = ContentCls.Url.Substring(ContentCls.Url.LastIndexOf("."));
                returnValue = ContentCls.Url.Substring(0, ContentCls.Url.LastIndexOf(".")) + "_" + (intI).ToString() + fileExtension;
            }
            return returnValue;
        }

        //列表分页  12345
        public string ThisChannelUrl(int intI)
        {
            string returnValue;
            Type_TypeTree TypeTree = new Type_TypeTree();
            TypeTree.Init(ChannelID);
            string fileExtension;
            if (intI == 1)
            {
                returnValue = TypeTree.TypeTreeListURL;
            }
            else
            {
                fileExtension = TypeTree.TypeTreeListURL.Substring(TypeTree.TypeTreeListURL.LastIndexOf("."));
                returnValue = TypeTree.TypeTreeListURL.Substring(0, TypeTree.TypeTreeListURL.LastIndexOf(".")) + "_" + (intI - 1).ToString() + fileExtension;
            }
            return returnValue;
        }

        private string xmlSplit(string xmlStr)
        {
            string returnValue = string.Empty;
            if (!string.IsNullOrEmpty(xmlStr))
            {
                XmlDataDocument xmlDoc = new XmlDataDocument();
                xmlDoc.LoadXml(xmlStr);
                returnValue = xmlDoc.DocumentElement.ChildNodes.Item(PageID).InnerText.ToString();
            }
            return returnValue;
        }


        //-----------------------------------------------------------
        //栏目
        //-----------------------------------------------------------
        public object Channel(string Item)
        {
            object returnValue;
            Type_TypeTree TypeTree = new Type_TypeTree();
            TypeTree.Init(ChannelID);

            switch (Item)
            {
                case "ChannelID":
                    returnValue = TypeTree.TypeTree_ID;
                    break;
                case "CName":
                    returnValue = TypeTree.TypeTreeCName;
                    break;
                case "EName":
                    returnValue = TypeTree.TypeTreeEName;
                    break;
                case "Url":
                    returnValue = TypeTree.TypeTreeListURL;
                    break;
                case "Images":
                    returnValue = TypeTree.TypeTreeImages;
                    break;
                case "Explain":
                    returnValue = TypeTree.TypeTreeExplain;
                    break;
                case "ParentID":
                    returnValue = TypeTree.TypeTreeParentID;
                    break;
                default:
                    returnValue = TypeTree.Channels(ChannelID, Item);
                    break;
            }


            Type_TypeTree ParentTypeTree = new Type_TypeTree();
            ParentTypeTree.Init(TypeTree.TypeTreeParentID);

            switch (Item)
            {
                case "ParentChannelID":
                    returnValue = ParentTypeTree.TypeTree_ID;
                    break;
                case "ParentCName":
                    returnValue = ParentTypeTree.TypeTreeCName;
                    break;
                case "ParentEName":
                    returnValue = ParentTypeTree.TypeTreeEName;
                    break;
                case "ParentUrl":
                    returnValue = ParentTypeTree.TypeTreeListURL;
                    break;
                case "ParentImages":
                    returnValue = ParentTypeTree.TypeTreeImages;
                    break;
                case "ParentExplain":
                    returnValue = ParentTypeTree.TypeTreeExplain;
                    break;
                case "ParentParentID":
                    returnValue = ParentTypeTree.TypeTreeParentID;
                    break;
            }

            //  ChannelID = TypeTreeOld_ID
            return returnValue;
        }

        //-----------------------------------------------------------
        //回复
        //-----------------------------------------------------------
        public object Re(string Item)
        {
            object returnValue = new object();
            ContentRemark _ContentRemark = new ContentRemark();
            _ContentRemark.Init(ReID);
            switch (Item)
            {
                case "ReID":
                    returnValue = _ContentRemark.Remark_ID;
                    break;
                case "Name":
                    returnValue = _ContentRemark.Remark_Name;
                    break;
                case "Content":
                    returnValue = _ContentRemark.Remark;
                    break;
                case "ContentID":
                    returnValue = _ContentRemark.Content_ID;
                    break;
                case "Status":
                    returnValue = _ContentRemark.Status;
                    break;
                case "SubmitDate":
                    returnValue = _ContentRemark.Remark_Date;
                    break;
                case "Author":
                    returnValue = _ContentRemark.Author;
                    break;
                case "UserID":
                    returnValue = _ContentRemark.User_ID;
                    break;
            }

            //  ChannelID = TypeTreeOld_ID
            return returnValue;
        }


        public object Location()
        {
            object returnValue;

            Childs Contents = new Childs();
            string GetPChannelID;
            GCMSClassLib.Content.Type_TypeTree TypeTree = new GCMSClassLib.Content.Type_TypeTree();
            GetPChannelID = TypeTree.IDParentTypeTree(int.Parse(GetChannel));

            string[] lines;
            lines = GetPChannelID.Split(',');

            long i;
            for (i = 0; i <= (lines.Length - 1); i++)
            {
                Child chair = new Child();
                chair.ContentID = int.Parse(lines[i]);
                Contents.Contents.Add(chair, lines[i], null, null);
            }
            returnValue = GetPChannelID;
            return returnValue;
        }

        public Collection Remark(int intTop)
        {
            Collection returnValue;


            SqlDataReader myReader;
            string sql;
            Childs Contents = new Childs();

            sql = "SELECT Top " + intTop.ToString() + " * FROM Content_ContentRemark WHERE Content_ID in (" + _ContentID + ") and Status = 4 order by Remark_Date";
            returnValue = LoadCurrentList(sql, "Remark_ID").Contents;
        
            return returnValue;
        }

        public Collection Channels()
        {
            Collection returnValue;

            
            
            Childs Contents = new Childs();
            string strListLastID = string.Empty;
            Type_TypeTree _Type_TypeTree = new Type_TypeTree();
            _Type_TypeTree.Init(ChannelID);

            if (String.IsNullOrEmpty(Order))
            {
                Order = "AtTop desc ,OrderNum desc";
            }

            string FieldsName = "Content_Content";
            string sql = string.Empty;
            if (_Type_TypeTree.TypeTree_Type == 2)
            {
                Content_FieldsName _Content_FieldsName = new Content_FieldsName();
                _Content_FieldsName.Init(_Type_TypeTree.TypeTree_ContentFields);
                FieldsName = "ContentUser_" + _Content_FieldsName.FieldsBase_Name;
                sql = string.Format("SELECT Top {0} * From {1} Where TypeTree_ID in ({2})  and Status=4 {3} order by {4}", ListTop, FieldsName, ChannelID, UserWhere, Order);          
            }
            else if (_Type_TypeTree.TypeTree_Type == 0 && _Type_TypeTree.TypeTree_ContentFields != 0)
            {
                int contentfiledid = _Type_TypeTree.TypeTree_ContentFields;
                Content_FieldsName _Content_FieldsName = new Content_FieldsName();
                _Content_FieldsName.Init(_Type_TypeTree.TypeTree_ContentFields);
                FieldsName = "ContentUser_" + _Content_FieldsName.FieldsBase_Name;
                sql = string.Format("SELECT Top {0} Content_Content.*,{1}.* From Content_Content RIGHT OUTER JOIN {1} ON Content_Content.Content_Id = {1}.Content_ID Where Content_Content.TypeTree_ID in ({2})  and Content_Content.Status=4 {3} order by {4}", ListTop, FieldsName, ChannelID, UserWhere, Order);          
            }

            if (!String.IsNullOrEmpty(ListLastID))
            {
                strListLastID = " and OrderNum < " + ListLastID;
            }

            //sql = "SELECT Top " & ListTop & " Content_ID,Name,Url,OrderNum FROM Content_Content WHERE TypeTree_ID =" & ChannelID.ToString() & strListLastID & " and status = 4 or Content_ID in (select Content_ID from Content_Commend WHERE  TypeTree_ID = " & ChannelID.ToString() & ") order by AtTop desc ,OrderNum desc"
            //sql = "SELECT Top " + ListTop + " Content_ID,Url,OrderNum," + _userwherecolname + " FROM " + FieldsName + " WHERE ( Content_ID in (select Content_ID from Content_Commend WHERE  TypeTree_ID = " + ChannelID.ToString() + ") or TypeTree_ID =" + ChannelID.ToString() + " ) " + strListLastID + " and status = 4 " + UserWhere + " order by " + Order;
           
            returnValue = LoadCurrentList(sql,"Content_ID").Contents;

            return returnValue;
        }


        public Collection Relative(int intTop)//#未完成代码#
        {
            Collection returnValue;

            string GetChannelID;
            GCMSClassLib.Content.Type_TypeTree TypeTree = new GCMSClassLib.Content.Type_TypeTree();
            GetChannelID = TypeTree.IDSonTypeTree(int.Parse(GetChannel));

            SqlDataReader myReader;
            string sql;
            Childs Contents = new Childs();

            if (String.IsNullOrEmpty(Order))
            {
                Order = "AtTop desc ,OrderNum desc";
            }

            sql = "SELECT Top " + intTop.ToString() + " Content_Contact.Other_ID FROM Content_Contact,Content_Content WHERE Content_Contact.Content_ID = " + ContentID + " and Content_Contact.Content_ID = Content_Content.Content_ID and Content_Content.Status = 4 order by Content_Content.OrderNum desc";
            myReader = Tools.DoSqlReader(sql);
            if (myReader.Read())
            {
                while (myReader.Read())
                {

                    Child chair = new Child();
                    chair.ContentID = myReader.GetInt32(0);
                    Contents.Contents.Add(chair, myReader.GetInt32(0).ToString(), null, null);
                }
            }
            else
            {
                myReader.Close();
                sql = "SELECT Top " + intTop.ToString() + " Content_ID FROM Content_Content WHERE Status = 4 and TypeTree_ID = " + ChannelID + " order by " + Order;
                myReader = Tools.DoSqlReader(sql);
                while (myReader.Read())
                {

                    Child chair = new Child();
                    chair.ContentID = myReader.GetInt32(0);
                    Contents.Contents.Add(chair, myReader.GetInt32(0).ToString(), null, null);
                }
            }
            myReader.Close();

            returnValue = Contents.Contents;

            return returnValue;
        }

        public Collection Son(int intTop)
        {
            Collection returnValue;

            string FieldsName;
            Type_TypeTree _Type_TypeTree = new Type_TypeTree();
            ChannelID = int.Parse(GetChannel);
            _Type_TypeTree.Init(int.Parse(GetChannel));

            string sql = string.Empty;
            Childs Contents = new Childs();

            if (String.IsNullOrEmpty(Order))
            {
                Order = "AtTop desc ,OrderNum desc";
            }

            if (_Type_TypeTree.TypeTree_Type == 2)
            {
                Content_FieldsName _Content_FieldsName = new Content_FieldsName();
                _Content_FieldsName.Init(_Type_TypeTree.TypeTree_ContentFields);
                FieldsName = "ContentUser_" + _Content_FieldsName.FieldsBase_Name;

                sql = "SELECT Top " + intTop.ToString() + " * FROM " + FieldsName + " WHERE Content_PID = " + ContentID + " and Status = 4 order by " + Order;
            }
            else if (_Type_TypeTree.TypeTree_Type == 0 && _Type_TypeTree.TypeTree_ContentFields != 0)
            {
                int contentfiledid = _Type_TypeTree.TypeTree_ContentFields;
                Content_FieldsName _Content_FieldsName = new Content_FieldsName();
                _Content_FieldsName.Init(_Type_TypeTree.TypeTree_ContentFields);
                FieldsName = "ContentUser_" + _Content_FieldsName.FieldsBase_Name;
                sql = string.Format("SELECT Top {0} Content_Content.*,{1}.* From Content_Content RIGHT OUTER JOIN {1} ON Content_Content.Content_Id = {1}.Content_ID Where Content_Content.Content_PID={2}  and Content_Content.Status=4 {3} order by {4}", intTop, FieldsName, ContentID, UserWhere, Order);          
            }



            returnValue = LoadCurrentList(sql, "Content_ID").Contents;

            return returnValue;
        }

        public Collection Top(int intTop)
        {
            Console.WriteLine("开始执行Top函数,参数为",intTop.ToString());
            Collection returnValue;

            Type_TypeTree _Type_TypeTree = new Type_TypeTree();
            _Type_TypeTree.Init(int.Parse(GetChannel));//ChannelID

            string GetChannelID = _Type_TypeTree.IDSonTypeTree(int.Parse(GetChannel));
            
            
            string Orderby = " AtTop desc , OrderNum desc";
            string isNews = " and Head_news = \'1\' ";

            if (!String.IsNullOrEmpty(Order))
            {
                Orderby = Order;
            }

            string FieldsName = "Content_Content";
            string sql = string.Empty;
            if (_Type_TypeTree.TypeTree_Type == 2  )
            {
                Content_FieldsName _Content_FieldsName = new Content_FieldsName();
                int contentfiledid = _Type_TypeTree.TypeTree_ContentFields == 0 ? _Type_TypeTree.TypeTree_TypeFields : _Type_TypeTree.TypeTree_ContentFields ;
                _Content_FieldsName.Init(contentfiledid);
                FieldsName = "ContentUser_" + _Content_FieldsName.FieldsBase_Name;
                isNews = " ";
                 sql = string.Format("SELECT Top {0} * From {1} Where TypeTree_ID in ({2}) {3} and Status= 4 {4} order by {5}", intTop, FieldsName, GetChannelID, isNews, UserWhere, Orderby);          
            }
            else if(_Type_TypeTree.TypeTree_Type == 0&& _Type_TypeTree.TypeTree_ContentFields != 0 )
            {
                int contentfiledid = _Type_TypeTree.TypeTree_ContentFields;
                Content_FieldsName _Content_FieldsName = new Content_FieldsName();
                _Content_FieldsName.Init(contentfiledid);
                FieldsName = "ContentUser_" + _Content_FieldsName.FieldsBase_Name;
                isNews = " ";
                sql = string.Format("SELECT Top {0} Content_Content.*,{1}.* From Content_Content RIGHT OUTER JOIN {1} ON Content_Content.Content_Id = {1}.Content_ID Where Content_Content.TypeTree_ID in ({2}) {3} and Content_Content.Status= 4 {4} order by {5}", intTop, FieldsName, GetChannelID, isNews, UserWhere, Orderby);          
            }
            //sql = "SELECT Top " + intTop.ToString() + "  " + FieldsName + ".Content_ID ,Url ," + FieldsName + ".TypeTree_ID,"+_userwherecolname+" FROM " + FieldsName + " WHERE (TypeTree_ID in (" + GetChannelID + ") or " + FieldsName + ".content_ID in (select distinct Content_ID from Content_Commend where TypeTree_ID in (" + GetChannelID + "))) " + isNews + " and Status = 4 "+ UserWhere+ " order by " + Orderby;
            //sql = "SELECT Top " + intTop.ToString() + "  * FROM " + FieldsName + " WHERE (TypeTree_ID in (" + GetChannelID + ") or " + FieldsName + ".content_ID in (select distinct Content_ID from Content_Commend where TypeTree_ID in (" + GetChannelID + "))) " + isNews + " and Status = 4 " + UserWhere + " order by " + Orderby;
            
            
            
            returnValue = LoadCurrentList(sql,"Content_ID").Contents;//载入列表

            return returnValue;
            Console.WriteLine("Top函数执行完毕");
        }

        public Collection Pic(int intTop)
        {
            Collection returnValue;

            string GetChannelID;
            GCMSClassLib.Content.Type_TypeTree TypeTree = new GCMSClassLib.Content.Type_TypeTree();
            GetChannelID = TypeTree.IDSonTypeTree(int.Parse(GetChannel));
         
            if (String.IsNullOrEmpty(Order))
            {
                Order = "AtTop desc ,OrderNum desc";
            }

            //sql = "SELECT Top " & intTop.ToString() & " Content_ID,Name ,Url FROM Content_Content WHERE TypeTree_ID in (" & GetChannelID & ") and Picture_news = '1' and Status = 4 order by OrderNum desc"
            //sql = "SELECT Top " & intTop.ToString() & " Content_Content.Content_ID,Name ,Url ,Content_Content.TypeTree_ID FROM Content_Content,Content_Commend WHERE (Content_Content.TypeTree_ID in (" & GetChannelID & ") or Content_Commend.TypeTree_ID in (" & GetChannelID & ")) and Content_Content.Content_ID = Content_Commend.Content_ID and Picture_news = '1' and Status = 4 order by OrderNum desc"
            string sql = "SELECT Top " + intTop.ToString() + " * FROM Content_Content WHERE (TypeTree_ID in (" + GetChannelID + ") or Content_Content.content_ID in (select distinct Content_ID from Content_Commend where TypeTree_ID in (" + GetChannelID + "))) and Picture_news = \'1\' and Status = 4 order by " + Order;

            returnValue = LoadCurrentList(sql, "Content_ID").Contents;

            return returnValue;
        }

        //取主栏目的子栏目

        public Collection GetChannels(string ChannelID) //子栏目
        {
            Collection returnValue;

            //SqlDataReader myReader;
            string sql;
            //ChannelChilds Channels = new ChannelChilds();

            sql = "SELECT  * FROM Content_Type_TypeTree WHERE TypeTree_ParentID = " + ChannelID + " and TypeTree_Issuance in(1,3,5) order by TypeTree_OrderNum";
            //myReader = Tools.DoSqlReader(sql);

            //while (myReader.Read())
            //{
            //    Child chair = new Child();
            //    chair.ChannelID = myReader.GetInt32(0);
            //    Channels.Contents.Add(chair, myReader.GetInt32(0).ToString(), null, null);
            //}
            //myReader.Close();
            returnValue = LoadCurrentList(sql, "TypeTree_ID").Contents;
            return returnValue;
        }


        public string Feedback() //子栏目
        {
            string returnValue;
            returnValue = "<form action=\"/GCMS/Content/Tools_Feedback.aspx\" method=\"post\" name=\"Form1\" id=\"Form1\" > ";
            returnValue = returnValue + "<TABLE style=\"WIDTH: 100%\">";

            SqlDataReader myReader;
            string sql;
            Childs Contents = new Childs();
            string ToolsPut = string.Empty;

            sql = "SELECT Property_ID,Property_Name,Property_InputType,Property_Alias,Property_InputOptions FROM  Content_Schema  WHERE TypeTree_ID =" + ChannelID + "order by Property_ID";
            myReader = Tools.DoSqlReader(sql);

            while (myReader.Read())
            {

                switch (myReader.GetString(2))
                {
                    case "TEXT":
                        ToolsPut = "<input type=\'text\' size=\'30\' class=\'inputtext\' name=" + myReader.GetString(1) + "><br/>";
                        break;
                    case "IMAGE":
                        ToolsPut = "<textarea name=" + myReader.GetString(1) + " rows=\'6\' cols=\'30\'></textarea>";
                        break;
                    case "TEXTAREA":
                        ToolsPut = "<textarea name=" + myReader.GetString(1) + " rows=\'6\' cols=\'30\'></textarea>";
                        break;
                    case "SELECT":
                        string[] split;
                        char sSplit = ',';
                        string opss = myReader.GetString(4);



                        opss = opss.Replace('\n', sSplit);
                        split = opss.Split(sSplit);
                        ToolsPut = "<select size=\'1\' name=\'" + myReader.GetString(1) + "\' class=\'inputtext\'>";
                        string s;

                        foreach (string tempLoopVar_s in split)
                        {
                            s = tempLoopVar_s;
                            ToolsPut = ToolsPut + "<option value=" + s.ToString() + ">" + s.ToString() + "</option>";
                        }
                        ToolsPut = ToolsPut + "<select>";
                        break;

                    case "LABEL":
                        ToolsPut = myReader.GetString(4);
                        break;
                }

                returnValue = returnValue + "<TR valign=\'top\'><TD>" + myReader.GetString(3) + ("锛?/TD><TD>" + ToolsPut) + "</TD></TR>";

            }
            myReader.Close();

            returnValue = returnValue + "</table>";
            returnValue = returnValue + " <br/><input type=\"hidden\" name=\"ChannelID\" value=" + ChannelID + ">&nbsp;<input type=\"submit\" name=\"Submit\" value=\"鎻愪氦\">";
            returnValue = returnValue + "</form>";
            return returnValue;
        }

        public void GetChannelID(int sID) //子栏目Over
        {
            ChannelID = sID;
            //return null;
        }

        public void GetContentID(int sID) //子栏目Over
        {
            ContentID = sID;
            //return null;
        }

        public void GetReID(int sID) //子栏目Over
        {
            ReID = sID;
            //return null;
        }


        public void GetChannelOver() //子栏目Over
        {
            ContentID = _ContentID;
            ChannelID = _ChannelID;
            ReID = _ReID;
            _userwhere = string.Empty;//Change By Galen 2008.9.21
            //return null;
        }

       
       
        [Guid("C23A58D7-3DC4-4524-A5D9-EFD741136830")]
        [ComVisible(true)]
        [ClassInterface(ClassInterfaceType.AutoDispatch)]
        public class Child
        {

            public string Name;
            private string newName;

            public string Url;
            private string newUrl;

            public int ContentID;
            private int new_ID;

            public int ChannelID;
            private int newTypeTree_ID;

            public int ReID;
            private int Remark_ID;

            public void zh()
            {
                Name = newName;
                Url = newUrl;
                ContentID = new_ID;
                ChannelID = newTypeTree_ID;
                ReID = Remark_ID;
                //return null;
            }
        }

    }

    [Guid("C33A58D7-3D14-4524-A4D9-EFD731136830")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class Childs
    {

        public Collection Contents = new Collection();
        private Collection NewContents = new Collection();

        public Childs()
        {
            Contents = NewContents;
        }
    }

    [Guid("C23A58A1-3DC4-4524-A4D9-CFD831136830")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.AutoDispatch)]
    public class ChannelChilds
    {

        public Collection Contents = new Collection();
        private Collection NewContents = new Collection();

        public ChannelChilds()
        {
            Contents = NewContents;
        }
    }

    
}

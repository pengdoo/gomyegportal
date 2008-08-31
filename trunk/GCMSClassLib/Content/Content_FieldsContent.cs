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
//       2   2008-8-22 修改数据操作方法，封装SQL到存储过程中
//------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.SqlClient ;
using System.Collections;
using System.Data.Common;
using GCMSClassLib.Public_Cls;
using Gomye.CommonClassLib.Data;

namespace GCMSClassLib.Content
{
    /// <summary>
    /// Master 的摘要说明。
    /// </summary>
    public class Content_FieldsContent
    {
        public Content_FieldsContent()
        {
        }

        #region 属性部分
        private int m_Fields_ID;
        public int Fields_ID
        {
            get { return m_Fields_ID; }
            set { m_Fields_ID = value; }
        }
        private int m_FieldsName_ID;
        public int FieldsName_ID
        {
            get { return m_FieldsName_ID; }
            set { m_FieldsName_ID = value; }
        }
        private String m_Property_Name;
        public String Property_Name
        {
            get { return m_Property_Name; }
            set { m_Property_Name = value; }
        }
        private String m_Property_Alias;
        public String Property_Alias
        {
            get { return m_Property_Alias; }
            set { m_Property_Alias = value; }
        }
        private String m_Property_InputType;
        public String Property_InputType
        {
            get { return m_Property_InputType; }
            set { m_Property_InputType = value; }
        }
        private String m_Property_InputOptions;
        public String Property_InputOptions
        {
            get { return m_Property_InputOptions; }
            set { m_Property_InputOptions = value; }
        }
        private int m_Property_Order;
        public int Property_Order
        {
            get { return m_Property_Order; }
            set { m_Property_Order = value; }
        }
        #endregion 属性部分

        #region 数据操作部分
        /// <summary>
        /// 根据当前属性，建立新数据
        /// </summary>
        /// <returns></returns>
        public bool Create()
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@FieldsName_ID", SqlDbType.Int),
                    new SqlParameter("@Property_Name", SqlDbType.VarChar,50),
                    new SqlParameter("@Property_Alias", SqlDbType.VarChar,50),
                    new SqlParameter("@Property_InputType", SqlDbType.VarChar,50),
                    new SqlParameter("@Property_InputOptions", SqlDbType.Text),
                    new SqlParameter("@Property_Order", SqlDbType.Int),
                    new SqlParameter("@Fields_ID", SqlDbType.Int)
            };

            parameters[0].Value = this.FieldsName_ID;

            if (this.Property_Name != null)
                parameters[1].Value = this.Property_Name;
            else
                parameters[1].Value = DBNull.Value;


            if (this.Property_Alias != null)
                parameters[2].Value = this.Property_Alias;
            else
                parameters[2].Value = DBNull.Value;


            if (this.Property_InputType != null)
                parameters[3].Value = this.Property_InputType;
            else
                parameters[3].Value = DBNull.Value;


            if (this.Property_InputOptions != null)
                parameters[4].Value = this.Property_InputOptions;
            else
                parameters[4].Value = DBNull.Value;

            parameters[5].Value = this.Property_Order;
            parameters[6].Direction = ParameterDirection.Output;
            SqlHelper.RunProcedure(SqlHelper.LocalSqlServer, "p_Content_FieldsContent_ADD", parameters, out rowsAffected);

            if (rowsAffected>0)
            {
                this.Fields_ID = (int)parameters[6].Value;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除指定数据
        /// </summary>
        /// <param name="Fields_ID"></param>
        /// <returns></returns>
        public bool Delete(int Fields_ID)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@Fields_ID", SqlDbType.Int)
            };
            parameters[0].Value = Fields_ID;
            SqlHelper.RunProcedure(SqlHelper.LocalSqlServer, "p_Content_FieldsContent_Delete", parameters, out rowsAffected);

            if (rowsAffected == 1)
            {
                return true;
            }
            else
            {
                return false;
            }



        }

        public bool Update(string Fields_ID, string Property_Name, string Property_Alias, string Property_InputType,string Property_InputOptions)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@Fields_ID", SqlDbType.Int),
                    new SqlParameter("@Property_Name", SqlDbType.VarChar,50),
                    new SqlParameter("@Property_Alias", SqlDbType.VarChar,50),
                    new SqlParameter("@Property_InputType", SqlDbType.VarChar,50),
                    new SqlParameter("@Property_InputOptions", SqlDbType.Text)
            };
            parameters[0].Value = Fields_ID;


            if (Property_Name != null)
                parameters[1].Value = Property_Name;
            else
                parameters[1].Value = DBNull.Value;


            if (Property_Alias != null)
                parameters[2].Value = Property_Alias;
            else
                parameters[2].Value = DBNull.Value;


            if (Property_InputType != null)
                parameters[3].Value = Property_InputType;
            else
                parameters[3].Value = DBNull.Value;


            if (Property_InputOptions != null)
                parameters[4].Value =Property_InputOptions;
            else
                parameters[4].Value = DBNull.Value;

          

            SqlHelper.RunProcedure(SqlHelper.LocalSqlServer, "p_Content_FieldsContent_S_Update", parameters, out rowsAffected);
            return rowsAffected > 0;
        }

        /// <summary>
        /// 获取指定数据的属性
        /// </summary>
        /// <param name="Fields_ID"></param>
        /// <returns></returns>
        public bool Init(int Fields_ID)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@Fields_ID", SqlDbType.Int)};
            parameters[0].Value = Fields_ID;
            DataSet ds = SqlHelper.RunProcedure(SqlHelper.LocalSqlServer, "p_Content_FieldsContent_GetModel", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow r = ds.Tables[0].Rows[0];
                GetModel(r);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 输入字段名返回名称
        /// </summary>
        /// <param name="FieldsName_ID"></param>
        /// <param name="Property_Name"></param>
        /// <returns></returns>
        public string InitName(int FieldsName_ID, string Property_Name)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@FieldsName_ID", SqlDbType.Int),
                     new SqlParameter("@Property_Name", SqlDbType.NVarChar,100)};
            parameters[0].Value = FieldsName_ID;
            parameters[1].Value = Property_Name;
            DataSet ds = SqlHelper.RunProcedure(SqlHelper.LocalSqlServer, "p_Content_FieldsContent_S_GetModelByProperty", parameters, "ds");
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow r = ds.Tables[0].Rows[0];
                GetModel(r);
            }
            return this.Property_Alias;
        }

        ///<summary>
        ///按条件检查数据库中是否已经存在
        ///</summary>
        public bool IsExist(int Fields_ID)
        {
            int rowsAffected;
            SqlParameter[] parameters = {
                    new SqlParameter("@Fields_ID", SqlDbType.Int)};
            parameters[0].Value = Fields_ID;
            return SqlHelper.RunProcedure(SqlHelper.LocalSqlServer, "p_Content_FieldsContent_Exists", parameters, out rowsAffected) == 1;
        }

        public System.Collections.ArrayList SelectAll()
        {
            SqlParameter[] parameters ={ };
            DataSet ds = SqlHelper.RunProcedure(SqlHelper.LocalSqlServer, "p_Content_FieldsContent_GetList", parameters, "ds");
            System.Collections.ArrayList list = new System.Collections.ArrayList();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Content_FieldsContent model = new Content_FieldsContent();
                    model.Fields_ID = SqlHelper.GetInt(r["Fields_ID"]);
                    model.FieldsName_ID = SqlHelper.GetInt(r["FieldsName_ID"]);
                    model.Property_Name = SqlHelper.GetString(r["Property_Name"]);
                    model.Property_Alias = SqlHelper.GetString(r["Property_Alias"]);
                    model.Property_InputType = SqlHelper.GetString(r["Property_InputType"]);
                    model.Property_InputOptions = SqlHelper.GetString(r["Property_InputOptions"]);
                    model.Property_Order = SqlHelper.GetInt(r["Property_Order"]);
                    list.Add(model);
                }
            }
            return list;
        }

        public int OrderNumInit(int Fields_ID)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@Fields_ID", SqlDbType.Int)};
            parameters[0].Value = Fields_ID;
            DataSet ds = SqlHelper.RunProcedure(SqlHelper.LocalSqlServer, "p_Content_FieldsContent_GetModel", parameters, "ds");
            int res = 0;
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow r = ds.Tables[0].Rows[0];
                res = this.Property_Order = SqlHelper.GetInt(r["Property_Order"]);
            }    
            return res;
        }


        #endregion 数据操作部分

        private void GetModel(DataRow r)
        {
            this.Fields_ID = SqlHelper.GetInt(r["Fields_ID"]);
            this.FieldsName_ID = SqlHelper.GetInt(r["FieldsName_ID"]);
            this.Property_Name = SqlHelper.GetString(r["Property_Name"]);
            this.Property_Alias = SqlHelper.GetString(r["Property_Alias"]);
            this.Property_InputType = SqlHelper.GetString(r["Property_InputType"]);
            this.Property_InputOptions = SqlHelper.GetString(r["Property_InputOptions"]);
            this.Property_Order = SqlHelper.GetInt(r["Property_Order"]);
        }
      
    }
}


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentScore.Models.Attribute
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class UniqueAttribute : ValidationAttribute
    {
        /// <summary>
        /// 字段数据唯一,没生效
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override Boolean IsValid(Object value)
        {
            //校验数据库是否存在当前Key
            return true;
        }
    }
}

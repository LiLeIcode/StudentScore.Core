using System;
using System.Collections.Generic;
using System.Text;

namespace StudentScore.Models
{
    public class MessageModel<T>
    {
        public int status { get; set; } = 200;
        public bool success { get; set; } = false;
        public string msg { get; set; } = "服务器异常";
        public T response { get; set; }
    }
}

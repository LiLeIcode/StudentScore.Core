# StudentScore.Core
学生成绩管理

1、[StudentScoreSqlServerCore.mdf](https://github.com/LiLeIcode/StudentScore.Core/blob/master/StudentScore.Core/StudentScoreSqlServerCore.mdf)是数据库文件，我的sqlserver版本是2014，打开数据库附加该文件即可

2、数据库连接字符串在StudentScore.Models中的StudentScoreContext.cs中更改

2020-9-25

更改数据库，删除StudentInfo的两个ID的外键，StudentScoreSqlServerCore.mdf文件已更换

调整api/StudentInfo/addStudent接口

给api/StudentInfo/AllStudentInfo增加了MemoryCache缓存


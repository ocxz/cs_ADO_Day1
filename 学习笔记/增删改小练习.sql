-- sql语句脚本练习 练习范围：新建库，建表，插入数据

-- 一个dbTest4数据库，配置主文件和日志文件的相关属性

use master
select * from sysdatabases   -- 查看已存在的数据库

create database dbTest4     -- 新建且配置库
on primary
(
	name='dbTest4',  
	filename='E:\SqlServerData\dbTest4.mdf',
	size=4mb,
	maxsize=10mb,
	filegrowth=1mb
)
log on
(
	name='dbTest4_log.ldf',
	filename='E:\SqlServerData\dbTest4_log.ldf',
	size=1mb,
	maxsize=5mb,
	filegrowth=10%
)


-- 新建一个班级（班级id，班级name）插入四条数据

use dbTest4
create table ClassInfo
(
	ClassId int not null primary key identity(1,1),
	ClassName nvarchar(10) not null unique
)

insert ClassInfo values ('青龙'),('白虎'),('玄武'),('朱雀')
select * from ClassInfo


-- 新建一个学生表（学生id，学生name，学生性别，班级id（与班级构成一对多关系） 插入四条数据

use dbTest4
create table StudentInfo
(
	StuId int not null primary key identity(1,1),
	StuName nvarchar(10) not null,
	StuGender bit default(0),
	ClassId int not null,
	foreign key (ClassId) references ClassInfo(ClassId)
)

insert StudentInfo(StuName,StuGender,ClassId) values
('张三',0,1),
('李四',0,1),
('张珊珊',1,4),
('王五',0,2),
('王语嫣',1,2),
('段誉',0,3)
select * from StudentInfo


-- 新建一个课程表（课程id，课程名）

use dbTest4
create table CourseInfo
(
	courseId int not null primary key identity(1,1),
	courseName nvarchar(10) not null unique
)

insert CourseInfo(courseName) values
('语文'),
('数学'),
('英语'),
('物理'),
('化学'),
('生物')
select * from CourseInfo

-- 新建一个成绩表（编号，学生id，课程id，分数）  约束：成绩和学生 多对1  成绩和课程 多对1

use dbTest4
create table GradeInfo
(
	Grade decimal(3,1) not null check(grade>=0 and grade<=100),
	StuId int not null,
	CourseId int not null,
	GradeId int not null primary key identity(1,1),
	
	foreign key (StuId) references StudentInfo(StuId),   -- 成绩和学生 多对一关系 外键约束
	foreign key (CourseId) references CourseInfo(CourseId)  -- 成绩和课程 多对1关系 外键约束
)

insert GradeInfo(StuId,CourseId,Grade) values
(1,1,95),
(1,2,82),
(1,3,68),
(1,4,83),
(2,1,63),
(2,2,82),
(2,3,78),
(2,4,96.5)

 insert StudentInfo values('刘诗诗',1,2)
 
select stu.StuId as sid,stu.StuName as name,stu.StuGender as gender,stu.ClassId as cid
from StudentInfo as stu

-- 指定查多少行
select top 2 * from StudentInfo

-- 指定查百分之多少行
select top 30 percent * from StudentInfo

-- order by 进行排序查询 先排课，再排成绩
select * from GradeInfo order by CourseId desc, Grade desc

-- distinct消除查询结果中的重复行
select distinct Stuid from GradeInfo

-- where 条件查询
select * from StudentInfo where StuId<>5

-- between..and...

select * from StudentInfo where StuId between 3 and 6

-- 取考试分数在60-80间的成绩
select * from GradeInfo where Grade between 60 and 80

-- 取班级编号为1-3的学生信息
select * from StudentInfo where ClassId between 1 and 3

-- 取班级编号为1或者2或者3的学生信息
select * from StudentInfo where ClassId in (1,2,3)

-- 取2班和三班编号为3-6的学生
select * from StudentInfo where ClassId in (2,3) and StuId  not between 3 and 6 

select * from StudentInfo where not ClassId in (2,3) and StuId  not between 1 and 2

select * from StudentInfo where StuGender=1

-- 模糊查询 查询名字中包含张的学生信息
select * from StudentInfo where StuName like '%张%'

insert StudentInfo values ('三不张',0,2)

-- 模糊查询 查询名字中姓王的学生信息
select * from StudentInfo where StuName like '王%'

-- 模糊查询 查询名字为两位，且姓王的学生信息
select * from StudentInfo where StuName like '王_'

update StudentInfo set StuPhone='18370810801' where StuId=1

update StudentInfo set StuPhone='13870800801' where StuId=2

update StudentInfo set StuPhone='15170990542' where StuId=3

update StudentInfo set StuPhone='18350510769' where StuId=4

update StudentInfo set StuPhone='15178628035' where StuId=5

update StudentInfo set StuPhone='13969893863' where StuId=6

update StudentInfo set StuPhone='13787876326' where StuId=7

update StudentInfo set StuPhone='13070708565' where StuId=8

-- 查询不以15-19开头的电话的学生信息
select * from StudentInfo where StuPhone like '1[^5-9]%'


update StudentInfo set StuEmail='2252779530@qq.com' where StuId=1

update StudentInfo set StuEmail='26586@163.com' where StuId=2

update StudentInfo set StuEmail='201314@网易.com' where StuId=3

update StudentInfo set StuEmail='520520@gmail.com' where StuId=4

update StudentInfo set StuEmail='20868569@qq.com' where StuId=5

update StudentInfo set StuEmail='1385687256@qq.com' where StuId=6

update StudentInfo set StuEmail='3698639652@qq.com' where StuId=7

update StudentInfo set StuEmail='5685623@163.com' where StuId=8

-- 取使用qq邮箱的学生
select * from StudentInfo where StuEmail like '%@qq.com'

select * from StudentInfo where StuId like '[56]'


update StudentInfo set StuPhone=null where StuId like '[12]'

-- 取手机号为空的学生信息
select * from StudentInfo where StuPhone is null

-- 连接查询：需要从多张表中查询信息时使用连接查询
-- 查询学生姓名及所在班级名称

select si.ClassId,si.StuId,si.StuName,ci.ClassName from StudentInfo as si
inner join ClassInfo as ci on si.ClassId=ci.ClassId

select si.ClassId,ci.ClassName,si.StuId,si.StuName from StudentInfo as si 
inner join ClassInfo as ci on si.ClassId=ci.ClassId

-- 查询青龙班所有学生的信息

select si.*,ci.ClassName from StudentInfo as si inner join ClassInfo as ci on si.ClassId=ci.ClassId where ci.ClassName='青龙'

select * from StudentInfo

select * from ClassInfo
insert ClassInfo(ClassName) values ('黄龙')

-- 左外连接，左表数据能匹配到右表数据，但是左表有些特殊数据，右表没有
-- 如查询班级和学生信息，但是黄龙班暂无学生信息
select * from ClassInfo as ci
left join StudentInfo as si on ci.ClassId=si.ClassId

-- 右外连接，左表数据能匹配到右表数据，但是右表有些特殊数据，左表没有
select * from StudentInfo as si
right join ClassInfo as ci  on si.ClassId=ci.ClassId

select * from StudentInfo as si
full join ClassInfo as ci  on si.ClassId=ci.ClassId

--学生姓名、科目名称、分数

select * from GradeInfo

--  要有：学生表、科目表、分数表
--  只用参加了考试的学生成绩，用内连接，如果需要未考试学生的成绩也可以外连接
-- 中间人：分数表中有学生id、分数表中有科目id，分数表就是中间表
select si.StuName,ci.courseName,gi.Grade from GradeInfo as gi
inner join StudentInfo as si on gi.StuId=si.StuId
inner join CourseInfo as ci on gi.CourseId=ci.courseId

-- 班级名称 学生姓名、科目名称、分数
-- 要有：学生表、科目表、班级表、分数表
-- 使用内连接
-- 关系：分数表中可以找到学生和科目、学生表中可以找到班级
select ci.ClassName,si.StuId,si.StuName,course.courseName,Grade from GradeInfo as gi
inner join StudentInfo as si on gi.StuId=si.StuId
inner join CourseInfo as course on gi.CourseId=course.courseId
inner join ClassInfo as ci on ci.ClassId=si.ClassId 
order by si.StuId asc,Grade desc


-- 聚合函数
select * from StudentInfo

-- 聚合函数 count 查看学生表中有多少个人
select COUNT(*) as 学生人数 from StudentInfo

-- 聚合函数 count 查看学生表中二班人数
select COUNT(*) as 二班人数 from StudentInfo where ClassId=2

-- 聚合函数 count 查看学生表有电话号码的人数  遇到null，不统计
select COUNT(StuPhone) as 有手机号的人数 from StudentInfo


select * from GradeInfo
-- 聚合函数 max 查看学生id为1的最高成绩
select MAX(Grade) as 最高成绩 from GradeInfo where StuId=1

-- 聚合函数 max 查看学生id为1的最低分
select MIN(grade) as 最低成绩 from GradeInfo where StuId=1

-- 求语文成绩的平均值 需要科目表、成绩表、平均聚合函数
select * from GradeInfo
select * from CourseInfo

select AVG(grade) 语文成绩平均值 from GradeInfo as gi
inner join CourseInfo as ci on gi.CourseId=ci.courseId
where ci.courseName='语文'


select AVG(grade) from GradeInfo as gi
inner join CourseInfo as ci on gi.CourseId=ci.courseId
where ci.courseName = '语文'

-- over() 显示成绩表所有数据、统计平均成绩、总成绩、最高成绩、最低成绩且显示
select GradeInfo.*, 
Avg(grade) over() as 平均成绩,
SUM(grade) over() as 总成绩,
MAX(Grade) over() as 最高成绩,
MIN(Grade) over() as 最低成绩
from GradeInfo

-- 统计男女人数
select StuGender,COUNT(*) from StudentInfo
group by StuGender

-- 统计各科成绩的平均分
select ci.courseName,
AVG(Grade) as 平均分,
SUM(Grade) as 总分,
MAX(grade) as 最高分,
MIN(grade) as 最低分
from GradeInfo as gi
inner join CourseInfo as ci on gi.CourseId=ci.courseId
group by ci.courseName

-- 统计各班各科成绩的平均分、总分、最高分、最低分

-- 先给班级分组、再给各科目分组
-- 需要用到的表：班级表、学生表、课程表、成绩表

select ci.ClassName,course.courseName,
AVG(gi.Grade) as 平均分,
SUM(gi.Grade) as 总分,
MAX(gi.grade) as 最高分,
MIN(gi.grade) as 最低分
from GradeInfo as gi
inner join StudentInfo as si on gi.StuId=si.StuId
inner join CourseInfo as course on gi.CourseId=course.courseId
inner join ClassInfo as ci on si.ClassId=ci.ClassId
group by course.courseName,ci.ClassName


select * from StudentInfo
update StudentInfo set ClassId=2 where StuName='李四'


select ci.courseName,
SUM(grade),
AVG(Grade)
from GradeInfo as gi
inner join CourseInfo as ci on gi.CourseId=ci.courseId
group by ci.courseName



select CourseId,
AVg(Grade) 平均成绩,
Sum(grade) 总成绩
from GradeInfo
group by CourseId

select * from GradeInfo

select * from StudentInfo

insert StudentInfo values('张三丰',0,null,null,1)

select * from GradeInfo

-- 5 2班 9 1班
insert GradeInfo values
(93,5,1),
(86,5,2),
(75,5,3),
(63,5,4),
(69,9,1),
(76,9,2),
(86,9,3),
(71,9,4)

-- 统计学生编号大于2的各班级的男女生人数
-- 问题解决：拿编号大于2的学生数据、分组（逻辑分组，先分班级，后邮箱类型）

select * from StudentInfo

select ClassId,StuGender,
COUNT(StuGender) 男女人数
from StudentInfo
where StuId>2
group by StuGender,ClassId

-- 统计学生编号大于2的各班级的人数大于3的各性别人数信息
-- 理解：学生编号大于3、班级分组、性别分组、取出性别人数大于1的

select top 50 percent ClassId,StuGender,
COUNT(StuGender) as 男女人数
from StudentInfo
where StuId>2
group by StuGender,ClassId having COUNT(StuGender)>0

select * from StudentInfo

-- 联合查询
select ClassId from ClassInfo
intersect  --交
select StuId from StudentInfo

-- 快速备份 表不存在

select * into test1 from StudentInfo

select * from test1

-- 备份空表 表不存在
select * into test2 from StudentInfo where 1=2
select * from test2

-- 向一个存在的表中插入数据
insert into test1 
select StuName,StuGender,StuPhone,StuEmail,ClassId from StudentInfo

select * from test1


-- 类型转换
-- CAST()
select CAST(89.000000 as decimal(5,1))
select CAST(1 as CHAR(1)) + '1'
select CONVERT(decimal(4,1),89.00000000);

select ASCII('1')

select CHAR(69)

select  left('我的世界开始下雪',2)

select  right('我的世界开始下雪',3)

select SUBSTRING('我的世界开始下雪',3,2)

select len('我的世界开始下雪')

select upper('My World Is Snowing')

select GETDATE()

select DATEPART("dayofyear",GETDATE())

-- 函数总结：以”2019-8-1"的格式显示日期
select GETDATE()   --  2019-08-01 16:39:08.900

select YEAR(getDate()) -- 2019

select MONTH(getDate())  -- 8

select DAY(getDate())   --1

--字符串拼接
select RTRIM(str(year(getDate())))+STR(month(getdate()))+STR(day(getdate()))

-- 作业
-- 查询科目名称、平均分
-- 查询班级名称、平均分
-- 查询班级名称、科目名称、平均分

-- 第一题：两个表，成绩表、科目表，对科目名称进行分组

select ci.courseName,
CAST(AVG(grade) as decimal(3,1)) as 平均分
from GradeInfo as gi
inner join CourseInfo as ci on gi.CourseId=ci.courseId
group by ci.courseName

--第二题：三个表、成绩表、学生表-->班级表，对班级名进行分组

select ci.ClassName,
Convert(decimal(3,1),Avg(grade)) as 平均分
from GradeInfo as gi
inner join StudentInfo as si on gi.StuId=si.StuId
inner join ClassInfo as ci on si.ClassId=ci.ClassId
group by ci.ClassName

-- 第三题：四个表，成绩表、科目表、学生表-->班级表，先对班级进行分组、再对科目进行分组
select ci.ClassName,course.courseName,
cast(avg(grade) is decimal(3,1)) as 平均成绩
from GradeInfo as gi
inner join CourseInfo as course on gi.CourseId=course.courseId
inner join StudentInfo as si on gi.StuId=si.StuId
inner join ClassInfo as ci on si.ClassId=ci.ClassId
group by course.courseName,ci.ClassName
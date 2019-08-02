-- sql���ű���ϰ ��ϰ��Χ���½��⣬������������

-- һ��dbTest4���ݿ⣬�������ļ�����־�ļ����������

use master
select * from sysdatabases   -- �鿴�Ѵ��ڵ����ݿ�

create database dbTest4     -- �½������ÿ�
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


-- �½�һ���༶���༶id���༶name��������������

use dbTest4
create table ClassInfo
(
	ClassId int not null primary key identity(1,1),
	ClassName nvarchar(10) not null unique
)

insert ClassInfo values ('����'),('�׻�'),('����'),('��ȸ')
select * from ClassInfo


-- �½�һ��ѧ����ѧ��id��ѧ��name��ѧ���Ա𣬰༶id����༶����һ�Զ��ϵ�� ������������

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
('����',0,1),
('����',0,1),
('��ɺɺ',1,4),
('����',0,2),
('������',1,2),
('����',0,3)
select * from StudentInfo


-- �½�һ���γ̱��γ�id���γ�����

use dbTest4
create table CourseInfo
(
	courseId int not null primary key identity(1,1),
	courseName nvarchar(10) not null unique
)

insert CourseInfo(courseName) values
('����'),
('��ѧ'),
('Ӣ��'),
('����'),
('��ѧ'),
('����')
select * from CourseInfo

-- �½�һ���ɼ�����ţ�ѧ��id���γ�id��������  Լ�����ɼ���ѧ�� ���1  �ɼ��Ϳγ� ���1

use dbTest4
create table GradeInfo
(
	Grade decimal(3,1) not null check(grade>=0 and grade<=100),
	StuId int not null,
	CourseId int not null,
	GradeId int not null primary key identity(1,1),
	
	foreign key (StuId) references StudentInfo(StuId),   -- �ɼ���ѧ�� ���һ��ϵ ���Լ��
	foreign key (CourseId) references CourseInfo(CourseId)  -- �ɼ��Ϳγ� ���1��ϵ ���Լ��
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

 insert StudentInfo values('��ʫʫ',1,2)
 
select stu.StuId as sid,stu.StuName as name,stu.StuGender as gender,stu.ClassId as cid
from StudentInfo as stu

-- ָ���������
select top 2 * from StudentInfo

-- ָ����ٷ�֮������
select top 30 percent * from StudentInfo

-- order by ���������ѯ ���ſΣ����ųɼ�
select * from GradeInfo order by CourseId desc, Grade desc

-- distinct������ѯ����е��ظ���
select distinct Stuid from GradeInfo

-- where ������ѯ
select * from StudentInfo where StuId<>5

-- between..and...

select * from StudentInfo where StuId between 3 and 6

-- ȡ���Է�����60-80��ĳɼ�
select * from GradeInfo where Grade between 60 and 80

-- ȡ�༶���Ϊ1-3��ѧ����Ϣ
select * from StudentInfo where ClassId between 1 and 3

-- ȡ�༶���Ϊ1����2����3��ѧ����Ϣ
select * from StudentInfo where ClassId in (1,2,3)

-- ȡ2���������Ϊ3-6��ѧ��
select * from StudentInfo where ClassId in (2,3) and StuId  not between 3 and 6 

select * from StudentInfo where not ClassId in (2,3) and StuId  not between 1 and 2

select * from StudentInfo where StuGender=1

-- ģ����ѯ ��ѯ�����а����ŵ�ѧ����Ϣ
select * from StudentInfo where StuName like '%��%'

insert StudentInfo values ('������',0,2)

-- ģ����ѯ ��ѯ������������ѧ����Ϣ
select * from StudentInfo where StuName like '��%'

-- ģ����ѯ ��ѯ����Ϊ��λ����������ѧ����Ϣ
select * from StudentInfo where StuName like '��_'

update StudentInfo set StuPhone='18370810801' where StuId=1

update StudentInfo set StuPhone='13870800801' where StuId=2

update StudentInfo set StuPhone='15170990542' where StuId=3

update StudentInfo set StuPhone='18350510769' where StuId=4

update StudentInfo set StuPhone='15178628035' where StuId=5

update StudentInfo set StuPhone='13969893863' where StuId=6

update StudentInfo set StuPhone='13787876326' where StuId=7

update StudentInfo set StuPhone='13070708565' where StuId=8

-- ��ѯ����15-19��ͷ�ĵ绰��ѧ����Ϣ
select * from StudentInfo where StuPhone like '1[^5-9]%'


update StudentInfo set StuEmail='2252779530@qq.com' where StuId=1

update StudentInfo set StuEmail='26586@163.com' where StuId=2

update StudentInfo set StuEmail='201314@����.com' where StuId=3

update StudentInfo set StuEmail='520520@gmail.com' where StuId=4

update StudentInfo set StuEmail='20868569@qq.com' where StuId=5

update StudentInfo set StuEmail='1385687256@qq.com' where StuId=6

update StudentInfo set StuEmail='3698639652@qq.com' where StuId=7

update StudentInfo set StuEmail='5685623@163.com' where StuId=8

-- ȡʹ��qq�����ѧ��
select * from StudentInfo where StuEmail like '%@qq.com'

select * from StudentInfo where StuId like '[56]'


update StudentInfo set StuPhone=null where StuId like '[12]'

-- ȡ�ֻ���Ϊ�յ�ѧ����Ϣ
select * from StudentInfo where StuPhone is null

-- ���Ӳ�ѯ����Ҫ�Ӷ��ű��в�ѯ��Ϣʱʹ�����Ӳ�ѯ
-- ��ѯѧ�����������ڰ༶����

select si.ClassId,si.StuId,si.StuName,ci.ClassName from StudentInfo as si
inner join ClassInfo as ci on si.ClassId=ci.ClassId

select si.ClassId,ci.ClassName,si.StuId,si.StuName from StudentInfo as si 
inner join ClassInfo as ci on si.ClassId=ci.ClassId

-- ��ѯ����������ѧ������Ϣ

select si.*,ci.ClassName from StudentInfo as si inner join ClassInfo as ci on si.ClassId=ci.ClassId where ci.ClassName='����'

select * from StudentInfo

select * from ClassInfo
insert ClassInfo(ClassName) values ('����')

-- �������ӣ����������ƥ�䵽�ұ����ݣ����������Щ�������ݣ��ұ�û��
-- ���ѯ�༶��ѧ����Ϣ�����ǻ���������ѧ����Ϣ
select * from ClassInfo as ci
left join StudentInfo as si on ci.ClassId=si.ClassId

-- �������ӣ����������ƥ�䵽�ұ����ݣ������ұ���Щ�������ݣ����û��
select * from StudentInfo as si
right join ClassInfo as ci  on si.ClassId=ci.ClassId

select * from StudentInfo as si
full join ClassInfo as ci  on si.ClassId=ci.ClassId

--ѧ����������Ŀ���ơ�����

select * from GradeInfo

--  Ҫ�У�ѧ������Ŀ��������
--  ֻ�òμ��˿��Ե�ѧ���ɼ����������ӣ������Ҫδ����ѧ���ĳɼ�Ҳ����������
-- �м��ˣ�����������ѧ��id�����������п�Ŀid������������м��
select si.StuName,ci.courseName,gi.Grade from GradeInfo as gi
inner join StudentInfo as si on gi.StuId=si.StuId
inner join CourseInfo as ci on gi.CourseId=ci.courseId

-- �༶���� ѧ����������Ŀ���ơ�����
-- Ҫ�У�ѧ������Ŀ���༶��������
-- ʹ��������
-- ��ϵ���������п����ҵ�ѧ���Ϳ�Ŀ��ѧ�����п����ҵ��༶
select ci.ClassName,si.StuId,si.StuName,course.courseName,Grade from GradeInfo as gi
inner join StudentInfo as si on gi.StuId=si.StuId
inner join CourseInfo as course on gi.CourseId=course.courseId
inner join ClassInfo as ci on ci.ClassId=si.ClassId 
order by si.StuId asc,Grade desc


-- �ۺϺ���
select * from StudentInfo

-- �ۺϺ��� count �鿴ѧ�������ж��ٸ���
select COUNT(*) as ѧ������ from StudentInfo

-- �ۺϺ��� count �鿴ѧ�����ж�������
select COUNT(*) as �������� from StudentInfo where ClassId=2

-- �ۺϺ��� count �鿴ѧ�����е绰���������  ����null����ͳ��
select COUNT(StuPhone) as ���ֻ��ŵ����� from StudentInfo


select * from GradeInfo
-- �ۺϺ��� max �鿴ѧ��idΪ1����߳ɼ�
select MAX(Grade) as ��߳ɼ� from GradeInfo where StuId=1

-- �ۺϺ��� max �鿴ѧ��idΪ1����ͷ�
select MIN(grade) as ��ͳɼ� from GradeInfo where StuId=1

-- �����ĳɼ���ƽ��ֵ ��Ҫ��Ŀ���ɼ���ƽ���ۺϺ���
select * from GradeInfo
select * from CourseInfo

select AVG(grade) ���ĳɼ�ƽ��ֵ from GradeInfo as gi
inner join CourseInfo as ci on gi.CourseId=ci.courseId
where ci.courseName='����'


select AVG(grade) from GradeInfo as gi
inner join CourseInfo as ci on gi.CourseId=ci.courseId
where ci.courseName = '����'

-- over() ��ʾ�ɼ����������ݡ�ͳ��ƽ���ɼ����ܳɼ�����߳ɼ�����ͳɼ�����ʾ
select GradeInfo.*, 
Avg(grade) over() as ƽ���ɼ�,
SUM(grade) over() as �ܳɼ�,
MAX(Grade) over() as ��߳ɼ�,
MIN(Grade) over() as ��ͳɼ�
from GradeInfo

-- ͳ����Ů����
select StuGender,COUNT(*) from StudentInfo
group by StuGender

-- ͳ�Ƹ��Ƴɼ���ƽ����
select ci.courseName,
AVG(Grade) as ƽ����,
SUM(Grade) as �ܷ�,
MAX(grade) as ��߷�,
MIN(grade) as ��ͷ�
from GradeInfo as gi
inner join CourseInfo as ci on gi.CourseId=ci.courseId
group by ci.courseName

-- ͳ�Ƹ�����Ƴɼ���ƽ���֡��ܷ֡���߷֡���ͷ�

-- �ȸ��༶���顢�ٸ�����Ŀ����
-- ��Ҫ�õ��ı��༶��ѧ�����γ̱��ɼ���

select ci.ClassName,course.courseName,
AVG(gi.Grade) as ƽ����,
SUM(gi.Grade) as �ܷ�,
MAX(gi.grade) as ��߷�,
MIN(gi.grade) as ��ͷ�
from GradeInfo as gi
inner join StudentInfo as si on gi.StuId=si.StuId
inner join CourseInfo as course on gi.CourseId=course.courseId
inner join ClassInfo as ci on si.ClassId=ci.ClassId
group by course.courseName,ci.ClassName


select * from StudentInfo
update StudentInfo set ClassId=2 where StuName='����'


select ci.courseName,
SUM(grade),
AVG(Grade)
from GradeInfo as gi
inner join CourseInfo as ci on gi.CourseId=ci.courseId
group by ci.courseName



select CourseId,
AVg(Grade) ƽ���ɼ�,
Sum(grade) �ܳɼ�
from GradeInfo
group by CourseId

select * from GradeInfo

select * from StudentInfo

insert StudentInfo values('������',0,null,null,1)

select * from GradeInfo

-- 5 2�� 9 1��
insert GradeInfo values
(93,5,1),
(86,5,2),
(75,5,3),
(63,5,4),
(69,9,1),
(76,9,2),
(86,9,3),
(71,9,4)

-- ͳ��ѧ����Ŵ���2�ĸ��༶����Ů������
-- ���������ñ�Ŵ���2��ѧ�����ݡ����飨�߼����飬�ȷְ༶�����������ͣ�

select * from StudentInfo

select ClassId,StuGender,
COUNT(StuGender) ��Ů����
from StudentInfo
where StuId>2
group by StuGender,ClassId

-- ͳ��ѧ����Ŵ���2�ĸ��༶����������3�ĸ��Ա�������Ϣ
-- ��⣺ѧ����Ŵ���3���༶���顢�Ա���顢ȡ���Ա���������1��

select top 50 percent ClassId,StuGender,
COUNT(StuGender) as ��Ů����
from StudentInfo
where StuId>2
group by StuGender,ClassId having COUNT(StuGender)>0

select * from StudentInfo

-- ���ϲ�ѯ
select ClassId from ClassInfo
intersect  --��
select StuId from StudentInfo

-- ���ٱ��� ������

select * into test1 from StudentInfo

select * from test1

-- ���ݿձ� ������
select * into test2 from StudentInfo where 1=2
select * from test2

-- ��һ�����ڵı��в�������
insert into test1 
select StuName,StuGender,StuPhone,StuEmail,ClassId from StudentInfo

select * from test1


-- ����ת��
-- CAST()
select CAST(89.000000 as decimal(5,1))
select CAST(1 as CHAR(1)) + '1'
select CONVERT(decimal(4,1),89.00000000);

select ASCII('1')

select CHAR(69)

select  left('�ҵ����翪ʼ��ѩ',2)

select  right('�ҵ����翪ʼ��ѩ',3)

select SUBSTRING('�ҵ����翪ʼ��ѩ',3,2)

select len('�ҵ����翪ʼ��ѩ')

select upper('My World Is Snowing')

select GETDATE()

select DATEPART("dayofyear",GETDATE())

-- �����ܽ᣺�ԡ�2019-8-1"�ĸ�ʽ��ʾ����
select GETDATE()   --  2019-08-01 16:39:08.900

select YEAR(getDate()) -- 2019

select MONTH(getDate())  -- 8

select DAY(getDate())   --1

--�ַ���ƴ��
select RTRIM(str(year(getDate())))+STR(month(getdate()))+STR(day(getdate()))

-- ��ҵ
-- ��ѯ��Ŀ���ơ�ƽ����
-- ��ѯ�༶���ơ�ƽ����
-- ��ѯ�༶���ơ���Ŀ���ơ�ƽ����

-- ��һ�⣺�������ɼ�����Ŀ���Կ�Ŀ���ƽ��з���

select ci.courseName,
CAST(AVG(grade) as decimal(3,1)) as ƽ����
from GradeInfo as gi
inner join CourseInfo as ci on gi.CourseId=ci.courseId
group by ci.courseName

--�ڶ��⣺�������ɼ���ѧ����-->�༶���԰༶�����з���

select ci.ClassName,
Convert(decimal(3,1),Avg(grade)) as ƽ����
from GradeInfo as gi
inner join StudentInfo as si on gi.StuId=si.StuId
inner join ClassInfo as ci on si.ClassId=ci.ClassId
group by ci.ClassName

-- �����⣺�ĸ����ɼ�����Ŀ��ѧ����-->�༶���ȶ԰༶���з��顢�ٶԿ�Ŀ���з���
select ci.ClassName,course.courseName,
cast(avg(grade) is decimal(3,1)) as ƽ���ɼ�
from GradeInfo as gi
inner join CourseInfo as course on gi.CourseId=course.courseId
inner join StudentInfo as si on gi.StuId=si.StuId
inner join ClassInfo as ci on si.ClassId=ci.ClassId
group by course.courseName,ci.ClassName
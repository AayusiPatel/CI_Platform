create database EmployeeDb
use EmployeeDb

create table Department
(
dept_id int NOT NULL Primary Key,
dept_name varchar(50) NOT NULL
)

create table Employee
(
emp_id int NOT NULL Primary Key,
dept_id int NOT NULL,
mngr_id int NULL,
emp_name varchar(50) NOT NULL,
salary int NOT NULL
)


insert into Department values(1001, 'FINANCE')
insert into Department values(2001, 'AUDIT')
insert into Department values(3001, 'MARKETING')
insert into Department values(4001, 'PRODUCTION')
insert into Department values(5001, 'TECHNICAL')

select * from Department


insert into Employee values(68319, 1001, NULL, 'KAYLING', 6000)
insert into Employee values(66928, 3001, 68319, 'BLAZE', 2750)
insert into Employee values(67832, 1001, 68319, 'CLARE', 2550)
insert into Employee values(65646, 2001, 68319, 'JONAS', 2967)
insert into Employee values(67858, 2001, 65646, 'SCARLET', 3100)
insert into Employee values(69062, 2001, 65646, 'FRANK', 3100)
insert into Employee values(63679, 2001, 69062, 'SANDRINE', 900)
insert into Employee values(64989, 3001, 66928, 'ADELYN', 1700)
insert into Employee values(65271, 3001, 66928, 'WADE', 1350)
insert into Employee values(66564, 3001, 66928, 'MADDEN', 1350)
insert into Employee values(68454, 3001, 66928, 'TUCKER', 1600)
insert into Employee values(68736, 2001, 67858, 'ADNRES', 1200)
insert into Employee values(69000, 3001, 66928, 'JULIUS', 1050)
insert into Employee values(69324, 1001, 67832, 'MARKER', 1400)
insert into Employee values(69325, 5001, 67832, 'JHON', 5400)


select * from Employee

select * from Department




SELECT   Department.dept_name , count(Employee.emp_id) AS Number_of_people
From Employee
RIGHT JOIN Department
ON Employee.dept_id=Department.dept_id
GROUP BY (Department.dept_name);




SELECT   Department.dept_name , SUM(Employee.salary) AS Salary
From Employee
RIGHT JOIN Department
ON Employee.dept_id=Department.dept_id
GROUP BY (Department.dept_name);




SELECT count(Employee.emp_id) AS Number_of_people , Department.dept_name
From Employee
RIGHT JOIN Department
ON Employee.dept_id=Department.dept_id 
GROUP BY (Department.dept_name)
HAVING count(Employee.emp_id)<3;




SELECT dept_name FROM department
  WHERE dept_id IN
  (
    SELECT dept_id
      FROM Employee
      GROUP BY dept_id
      HAVING COUNT(*) < 3
  );




  SELECT e.emp_name,
		e.salary ,
       e.dept_id,
	   Department.dept_name
FROM Employee e
RIGHT JOIN Department
ON e.dept_id=Department.dept_id
WHERE e.salary IN
    (SELECT max(salary)
     FROM Employee
     GROUP BY dept_id) ;

	 use EmployeeDb


	 SELECT max(Employee.salary)  AS Salary , Department.dept_name 
From Employee
RIGHT JOIN Department
ON Employee.dept_id=Department.dept_id

GROUP BY (Department.dept_name)
HAVING  Employee.salary=max(Employee.salary);




SELECT  Department.dept_name , Employee.emp_name 
From Employee
RIGHT JOIN Department
ON Employee.dept_id=Department.dept_id 

GROUP BY (Department.dept_name) , (Employee.emp_name)
HAVING Employee.salary=max(Employee.salary)

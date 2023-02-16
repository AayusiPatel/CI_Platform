select * from Customer
select * from Orders
select * from Salesman

SELECT salesman.name AS "Salesman",
customer.cust_name, customer.city 
FROM salesman,customer 
WHERE salesman.city=customer.city;

SELECT  a.order_no,a.purch_amt,
b.cust_name,b.city 
FROM orders a
 JOIN customer b 
ON a.customer_id=b.customer_id 
WHERE a.purch_amt BETWEEN 500 AND 2000;

SELECT a.cust_name AS "Customer Name", 
a.city, b.name AS "Salesman", b.commision
FROM customer a 
RIGHT JOIN salesman b 
ON a.salesman_id=b.salesman_id;

SELECT a.cust_name AS "Customer Name", 
a.city, b.name AS "Salesman", b.commision
FROM customer a 
INNER JOIN salesman b 
ON a.salesman_id=b.salesman_id 
WHERE b.commision>.12;


SELECT a.cust_name AS "Customer Name", 
a.city, b.name AS "Salesman", b.city,b.commision  
FROM customer a  
RIGHT JOIN salesman b  
ON a.salesman_id=b.salesman_id 
WHERE b.commision>.12 
AND a.city<>b.city;

SELECT a.order_no,a.order_date,a.purch_amt,
b.cust_name AS "Customer Name", b.grade, 
c.name AS "Salesman", c.commision 
FROM orders a 
INNER JOIN customer b 
ON a.customer_id=b.customer_id 
INNER JOIN salesman c 
ON a.salesman_id=c.salesman_id;

SELECT * 
FROM orders 
NATURAL JOIN Customer

NATURAL JOIN salesman;


SELECT b.customer_id,b.cust_name,b.city AS Customer_City, b.grade,a.order_no,a.purch_amt,a.purch_amt,a.order_date,c.Salesman_id,c.name AS Salesman_name,c.city AS Salesman_City,c.commision
FROM orders a 
INNER JOIN customer b 
ON a.customer_id=b.customer_id 
INNER JOIN salesman c 
ON a.salesman_id=c.salesman_id;

SELECT a.cust_name,a.city,a.grade, 
b.name AS "Salesman",b.city 
FROM customer a 
LEFT JOIN salesman b 
ON a.salesman_id=b.salesman_id 
order by a.customer_id;

SELECT a.cust_name,a.city,a.grade, 
b.name AS "Salesman", b.city 
FROM customer a 
LEFT OUTER JOIN salesman b 
ON a.salesman_id=b.salesman_id 
WHERE a.grade<300 
ORDER BY a.customer_id;


SELECT a.cust_name,a.city, b.order_no,
b.order_date,b.purch_amt AS "Order Amount" 
FROM customer a 
LEFT OUTER JOIN orders b 
ON a.customer_id=b.customer_id 
order by b.order_date;

SELECT a.cust_name,a.city, b.order_no,
b.order_date,b.purch_amt AS "Order Amount", 
c.name,c.commision 
FROM customer a 
LEFT OUTER JOIN orders b 
ON a.customer_id=b.customer_id 
LEFT OUTER JOIN salesman c 
ON c.salesman_id=b.salesman_id;


SELECT a.cust_name,a.city,a.grade, 
b.name AS "Salesman", b.city 
FROM customer a 
RIGHT OUTER JOIN salesman b 
ON b.salesman_id=a.salesman_id 
ORDER BY b.salesman_id;

SELECT b.name AS "Salesman", b.city,
a.cust_name,a.city,a.grade
FROM salesman b 
LEFT OUTER JOIN customer a
ON a.salesman_id=b.salesman_id
ORDER BY b.salesman_id;

SELECT  
b.name AS "Salesman", a.cust_name,a.city,a.grade,
c.order_no, c.order_date, c.purch_amt 
FROM salesman b 
LEFT OUTER JOIN  customer a
ON b.salesman_id=a.salesman_id 
RIGHT OUTER JOIN orders c 
ON c.customer_id=a.customer_id;

SELECT  
b.name AS "Salesman", a.cust_name,a.city,a.grade,
c.order_no, c.order_date, c.purch_amt 
FROM customer a 
RIGHT OUTER JOIN salesman b 
ON b.salesman_id=a.salesman_id 
LEFT OUTER JOIN orders c 
ON c.customer_id=a.customer_id 
WHERE c.purch_amt>=2000 
AND a.grade IS NOT NULL;

SELECT a.cust_name,a.city, b.order_no,
b.order_date,b.purch_amt AS "Order Amount" 
FROM customer a 
FULL OUTER JOIN orders b 
ON a.customer_id=b.customer_id 
WHERE a.grade IS NOT NULL;

SELECT * 
FROM salesman a 
CROSS JOIN customer b;

SELECT * 
FROM salesman a 
CROSS JOIN customer b 
WHERE a.city IS NOT NULL;

SELECT * 
FROM salesman a 
CROSS JOIN  customer b 
WHERE a.city IS NOT NULL 
AND b.grade IS NOT NULL;

SELECT * 
FROM salesman a 
CROSS JOIN customer b 
WHERE a.city IS NOT NULL 
AND b.grade IS NOT NULL 
AND  a.city<>b.city;
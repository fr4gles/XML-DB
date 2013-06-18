Create table TestTable 
(
	CUSTOMER_NUMBER int NOT NULL,
	NAME nvarchar(50) NOT NULL,
	STREET nvarchar(50) NOT NULL,
	CITY nvarchar(50) NOT NULL,
	POST_CODE nvarchar(50) NULL,
	COUNTRY nvarchar(50) NOT NULL,
	PHONE nvarchar(50) NOT NULL,
	id int NOT NULL IDENTITY,
	PRIMARY KEY ( id )
);


insert into TestTable (CUSTOMER_NUMBER, NAME, STREET, CITY, POST_CODE, COUNTRY, PHONE)  values ('0', 'Michał Franczyk', 'al. Wiartu', 'Stumilowy Las', '0-07', 'Polska', '931-321-412');
insert into TestTable (CUSTOMER_NUMBER, NAME, STREET, CITY, POST_CODE, COUNTRY, PHONE)  values ('007', 'Zbyszko z Bogdańca', 'Ziemianka', 'Starachowice', '-', 'Polska', '777-777-777');
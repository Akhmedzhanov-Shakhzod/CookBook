
--create database CookBook;

use CookBook;

create table dishes_type (
code_dishes_type int IDENTITY(1,1) primary key,
name_dishes_type varchar(20) not null
);

create table dishes (
code_dishes int IDENTITY(1,1) primary key,
name_dishes varchar(20) not null,
code_dishes_type int not null,
price_dishes real not null,
foreign key(code_dishes_type) references dishes_type(code_dishes_type)
);

create table units (
code_unit int IDENTITY(1,1) primary key,
name_unit varchar(20) not null
);

create table product (
code_product int IDENTITY(1,1) primary key,
name_product varchar(35) not null,
price_product real not null,
code_unit int not null,
foreign key (code_unit) references units(code_unit)
);

create table dishes_formula (
code_formula int IDENTITY(1,1) primary key,
code_dishes int not null,
code_ingredient int not null,
count_ingredient float not null,
unit_ingredient int not null,
foreign key (unit_ingredient) references units(code_unit),
foreign key(code_dishes) references dishes(code_dishes),
foreign key(code_ingredient) references product(code_product)
);

create table orders (
code_order int IDENTITY(1,1) primary key,
code_dishes int not null,
number_order int not null,
count_order int not null,
price_order real not null,
date_order date not null,
foreign key (code_dishes) references dishes(code_dishes),
);


create table descriptions(
code_description int IDENTITY(1,1) primary key,
code_dishes int not null,
descriptions text not null
foreign key (code_dishes) references dishes(code_dishes)
);

/*
-- units
insert into units(name_unit) values('киллограмм'),
	('литр'),
	('пачка'),
	('штук'),
	('пучок'),
	('Бутылка'),
	('Долька'),
	('Упаковка'),
	('стакан'),
	('ст. ложки'),
	('по вкусу');

-- type of dishes
insert into dishes_type(name_dishes_type) values('первые'),
	('вторые'),
	('холодные'),
	('дисерты'),
	('прочие');

-- dishes 2
insert into dishes(name_dishes,code_dishes_type,price_dishes) values('плов',2,200),
	('лагман',2,230),
	('гуляж',2,190),
	('биштекс',2,190),
	('куурдак',2,190),
	('буженина',2,85),
	('поджарка мясная',2,85),
	('курица в сыре',2,80),
	('мясо по француский',2,90);

-- dishes 1
insert into dishes(name_dishes,code_dishes_type,price_dishes) values('бульон куриный',1,70),
	('борж',1,110),
	('суп рассольник',1,100),
	('суп с фрикадельками',1,90),
	('суп с лапшой',1,110),
	('чечевичный суп',1,85);


-- dishes 3
insert into dishes(name_dishes,code_dishes_type,price_dishes) values('окрошка',3,170),
	('му-му',3,130),
	('тёма с дёсой',3,90),
	('ассорти рыбное',3,145),
	('чародейка',3,80),
	('рыбка моя',3,70);

-- dishes 4
insert into dishes(name_dishes,code_dishes_type,price_dishes) values('манго ласси',4,300),
	('ласси',4,250),
	('крем карамель',4,250),
	('лучи с мороженым',4,450),
	('нарам гарам джамун',4,250),
	('красный бархат',4,480);

-- dishes 5
insert into dishes(name_dishes,code_dishes_type,price_dishes) values('самсы',5,120);

-- product
insert into product (name_product,price_product,code_unit) values('Баклажаны',248,1),
	('Капуста Голландская',39,1),
	('Тыква',28,1),
	('Кабачки',388,1),
	('Брокколи',195,1),
	('Капуста Пекинская',68,1),
	('Капуста цветная',89,1),
	('Капуста свежая',65,1),
	('Капуста краснокочанная',55,1),
	('Помидоры красные местные',88,1),
	('Огурцы тепличные Рава',148,1),
	('Помидоры Черри',330,1),
	('Перец Ласточка',140,1),
	('Чеснок местный',500,1),
	('Чеснок',180,1),
	('Чеснок чищеный',70,1),
	('Перец красный, Калифорния',225,1),
	('Свекла',35,1),
	('Редиска',110,5),
	('Морковь мытая',38,1),
	('Морковь немытая',25,1),
	('Морковь желтая',65,1),
	('морковь крупный салатный',20,1),
	('Редька',30,1),
	('Лук репчатый',15,1),
	('Лук репчатый отборный',17,1),
	('Лук красный',50,1),
	('кукуруза св',28,4),
	('Молоко',52,2),
	('Рис лазер первый сорт',136,1),
	('Рис шлифованный дробленый',35,1),
	('Рис Лидер',40,1),
	('Рис Камолино',40,1),
	('Чечевица',78,1),
	('Картофель',25,1),
	('Морковь',78,1),
	('Масло растительное',180,2),
	('Вода',25,2),
	('Соль',35,1),
	('Перец зеленый (полугорький)',170,1),
	('Перец сладкий красный',89,1),
	('Перец сладкий зеленый',89,4),
	('Лимон',180,1),
	('Маслины',121,6),
	('Зеленый лук оптом',165,1);


-- formula of dishes
insert into dishes_formula(code_dishes,code_ingredient,unit_ingredient,count_ingredient) values(15,34,9,1),
	(15,35,4,3),
	(15,25,1,1),
	(15,37,10,3),
	(15,38,2,2.5),
	(15,39,11,0),
	(15,42,11,0),
	(15,43,11,0),
	(15,44,11,0),
	(15,45,11,0);

-- descriptions for dishes
insert into descriptions(code_dishes,descriptions) values
	(15,'Этот суп из чечевицы действительно самый простой – кроме чечевицы в него кладется только зажарка и зелень. Всё! Тем не менее, такой суп из чечевицы получается очень вкусным.');

-- example order
insert into orders(code_dishes,number_order,count_order,price_order,date_order) values
	(15,1,1,(select price_dishes from dishes where code_dishes = 15),(DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), DAY(GETDATE()))));
*/

-- select info about dishes given types
create or alter procedure DishesInfo
@dishes_type nvarchar(20) = 'первые'
as
Select dishes.name_dishes,dishes_type.name_dishes_type,dishes.price_dishes,descriptions.descriptions 
from dishes
inner join dishes_type on dishes.code_dishes_type = dishes_type.code_dishes_type
left join descriptions on dishes.code_dishes = descriptions.code_dishes
where dishes.code_dishes_type = (select dishes_type.code_dishes_type from dishes_type where dishes_type.name_dishes_type = @dishes_type)
order by dishes.name_dishes 
go

-- select cost price dishes
create or alter procedure priceDish
@codeDish int
as
	return (select sum(count_ingredient*price_product) 
	from dishes_formula
	inner join product
	on dishes_formula.code_ingredient = product.code_product
	where dishes_formula.code_dishes = @codeDish
	);

declare @p float
exec @p = priceDish 265
select @p
go
--select * from orders
--select * from dishes
-- procedure for countdown used products
create or alter procedure allPriceUsedProducts
@date1 date ,
@date2 date 
as
declare @sum float = 0;
declare @firstID int = (select min(code_order) from orders where date_order between @date1 and @date2);
declare @lastID int = (select max(code_order) from orders where date_order between @date1 and @date2);
while @firstID <= @lastId
begin
	if ((select code_order from orders where code_order = @firstID) = @firstID)
	begin
		declare @dishCode int = (select code_dishes from orders where code_order = @firstID);
		declare @priceDish float;
		exec @priceDish = priceDish @dishCode;
		declare @countDish int = (select count_order from orders where code_order = @firstID);
		set @sum = @sum + @priceDish*@countDish;
	end
	set @firstID = @firstID + 1;
end
Select @sum as Summ

exec allPriceUsedProducts '2022-05-22','2022-05-22'
go

--select * from orders
-- select used prodcuts
create or alter procedure usedProductInOrder
@numberOrder int
as
begin
select product.name_product,dishes_formula.count_ingredient*orders.count_order as _Count,product.price_product as _Price,(dishes_formula.count_ingredient*product.price_product*orders.count_order) as _Cost
from product
inner join dishes_formula
on dishes_formula.code_ingredient = product.code_product
inner join orders
on orders.code_dishes = dishes_formula.code_dishes
where orders.number_order = @numberOrder 
--group by product.name_product,dishes_formula.count_ingredient*orders.count_order,product.price_product,(dishes_formula.count_ingredient*product.price_product*orders.count_order)
order by product.name_product
end
go

exec usedProductInOrder 1
exec usedProductInOrder 2
go
--select * from orders

-- check exist order via number of order
create or alter procedure checkExist
@numberID int
as
if ((select count(number_order) from orders where number_order = @numberID) >= 1)
begin
	select 'True'
end
else
begin
	select 'False'
end

exec checkExist 1
go


--
--
--
--

-- triger for uniq data for dishes
create or alter trigger UniqData
on dishes
for insert 
as
BEGIN    
	Declare @name varchar(25);
	Set @name = (Select name_dishes from inserted);
	IF ((select count(name_dishes) from dishes where name_dishes = @name)>=2)
		BEGIN
			ROLLBACK TRAN
			select 'It is not allowed to insert unique data'
		END
END	
go

-- triger for uniq data for types of dishes
create or alter trigger UniqData1
on dishes_type
for insert 
as
BEGIN
	Declare @name varchar(25);
	Set @name = (Select name_dishes_type from inserted);
	IF ((select count(name_dishes_type) from dishes_type where name_dishes_type = @name)>=2)		
		BEGIN
				ROLLBACK TRAN
				select 'It is not allowed to insert unique data'
		END
END	
go

-- triger for uniq data for units
create or alter trigger UniqData2
on units
for insert 
as
BEGIN     
	Declare @name varchar(25);
	Set @name = (Select name_unit from inserted);
	IF ((select count(name_unit) from units where name_unit = @name)>=2)		
		BEGIN
			ROLLBACK TRAN
			select 'It is not allowed to insert unique data'
		END
END	
go

-- triger for uniq data for products
create or alter trigger UniqData3
on product
for insert 
as
BEGIN     
	Declare @name varchar(25);
	Set @name = (Select name_product from inserted);
	IF ((select count(name_product) from product where name_product = @name)>=2)		
		BEGIN
			ROLLBACK TRAN
			select 'It is not allowed to insert unique data'
		END
END	
go

-- triger for uniq data for descriptions
create or alter trigger UniqData4
on descriptions
instead of insert 
as
BEGIN     
	Declare @code int;
	Set @code = (Select code_dishes from inserted);
	IF exists(select code_dishes from descriptions where code_dishes = @code)		
		BEGIN
			update descriptions set descriptions = (select descriptions from inserted where code_dishes = @code) where code_dishes = @code;
			--select 'It is not allowed to insert unique data'
		END
	else
		begin
			insert into descriptions(code_dishes,descriptions)
			select code_dishes,descriptions from inserted
		end
END	
go

/*
/********************************************************/
Select name_unit from units where code_unit = (select code_unit from product where name_product = 'мясо')  
use [CookBook]
select name_unit from units where code_unit = 1


/*********************************************************/

select code_unit from product where name_product = 'Картофель'

select name_unit from units where code_unit = 1
*/


select * from dishes



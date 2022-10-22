INSERT INTO [dbo].[Product]
           ([Description]
           ,[Price]
           ,[Unit])
     VALUES
           ('Polo',80,'UND'),
		   ('Camisa',90,'UND'),
		   ('Pantalon',150,'UND'),
		   ('Saco',200,'UND'),
		   ('Corbata',40,'UND')

GO

INSERT INTO [Order] ([Description]) values
	('Orden de venta para Manuel'),
	('Orden de venta para Jose'),
	('Orden de venta para Luis'),
	('Orden de venta para Monica'),
	('Orden de venta para Erick')

GO
INSERT INTO OrderItem (OrderId,ProductId) values
	(1,1),
	(1,2),
	(1,3),
	(2,1),
	(2,4),
	(2,5),
	(2,2),
	(3,1),
	(3,3),
	(3,5),
	(3,2),
	(4,2),
	(4,3),
	(4,5),
	(4,1)
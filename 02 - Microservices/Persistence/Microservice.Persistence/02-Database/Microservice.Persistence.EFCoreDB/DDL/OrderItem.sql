CREATE TABLE [dbo].[OrderItem]
(
	OrderItemId INT NOT NULL IDENTITY PRIMARY KEY,
	OrderId INT NOT NULL,
	ProductId INT NOT NULL,

	CONSTRAINT FK_OrdernItemOrden FOREIGN KEY (OrderId) REFERENCES [Order](OrderId),
	CONSTRAINT FK_OrdernItemProducto FOREIGN KEY (ProductId) REFERENCES Product(ProductId),
)

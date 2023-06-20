SELECT * FROM Cliente

--INSERT INTO Cliente VALUES('Administrador',' del Sistema', 'admin@gmail.com', 30, '9999999',1)

SELECT * FROM Venta
SELECT * FROM VentaDetalle

GO
CREATE PROC uspListarVentas(@FechaInicio DATE, @FechaFin DATE, @Pagina INT, @Filas INT)
AS
BEGIN
	SELECT 
		V.Id VentaId,
		CONCAT(C.Nombres,' ',C.Apellidos) Cliente,
		CAST(V.FechaVenta AS nvarchar(13)) FechaVenta,
		V.NroDocumento NroFactura,
		(SELECT COUNT(ID) FROM VentaDetalle WHERE IdVenta = V.Id) CantidadProductos,
		V.SubTotal,
		V.Impuestos,
		V.Total
	FROM
	Venta V (NOLOCK)
	INNER JOIN Cliente C ON C.Id = V.IdCliente
	WHERE CAST(V.FechaVenta AS DATE) BETWEEN @FechaInicio AND @FechaFin
	ORDER BY V.NroDocumento
	OFFSET @Pagina ROWS FETCH NEXT @Filas ROWS ONLY;

END;
GO

EXEC uspListarVentas '2023-01-01','2023-06-19',0,2
GO
CREATE OR ALTER PROC uspDashboard (@FechaInicio DATE, @FechaFin DATE)
AS
BEGIN
	SELECT 
		COUNT(V.ID) CantidadVentas,
		(SELECT COUNT(ID) FROM Cliente WHERE Estado = 1) CantidadClientes,
		(SELECT COUNT(ID) FROM Producto WHERE Estado = 1) CantidadProductos,
		(SELECT COUNT(ID) FROM Categoria WHERE Estado = 1) CantidadCategorias,
		SUM(V.Total) SumaTotal,
		AVG(V.Total) PromedioVenta
	FROM Venta V (NOLOCK)
	WHERE CAST(V.FechaVenta AS DATE) BETWEEN @FechaInicio AND @FechaFin

END
GO
EXEC uspDashboard '2023-01-01','2023-06-19'
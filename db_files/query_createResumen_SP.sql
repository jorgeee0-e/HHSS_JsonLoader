CREATE PROC SP_Resument_DTEs
as
begin 
select 
[id_resumen],[codigoGeneracion],[totalNoSuj],[totalExenta],[totalGravada],[subTotalVentas],[descuNoSuj],[descuExenta],[descuGravada],[porcentajeDescuento],[totalDescu],[ivaPerci1],[ivaRete1],[reteRenta],[montoTotalOperacion],
[totalNoGravado],[totalPagar],[totalLetras],[saldoFavor]
FROM [dbo].[ResumenDTE]
end

	CREATE PROCEDURE SP_guardar_resumen_DTEs (
    @codigoGeneracion VARCHAR(36),
    @totalNoSuj DECIMAL(18, 2),
    @totalExenta DECIMAL(18, 2),
    @totalGravada DECIMAL(18, 2),
    @subTotalVentas DECIMAL(18, 2),
    @descuNoSuj DECIMAL(18, 2),
    @descuExenta DECIMAL(18, 2),
    @descuGravada DECIMAL(18, 2),
    @porcentajeDescuento DECIMAL(18, 2),
    @totalDescu DECIMAL(18, 2),
    @ivaPerci1 DECIMAL(18, 2),
    @ivaRete1 DECIMAL(18, 2),
    @reteRenta DECIMAL(18, 2),
    @montoTotalOperacion DECIMAL(18, 2),
    @totalNoGravado DECIMAL(18, 2),
    @totalPagar DECIMAL(18, 2),
    @totalLetras VARCHAR(255),
    @saldoFavor DECIMAL(18, 2)
)
AS 
BEGIN
    INSERT INTO ResumenDTE (
        codigoGeneracion,
        totalNoSuj,
        totalExenta,
        totalGravada,
        subTotalVentas,
        descuNoSuj,
        descuExenta,
        descuGravada,
        porcentajeDescuento,
        totalDescu,
        ivaPerci1,
        ivaRete1,
        reteRenta,
        montoTotalOperacion,
        totalNoGravado,
        totalPagar,
        totalLetras,
        saldoFavor
    )
    VALUES (
        @codigoGeneracion,
        @totalNoSuj,
        @totalExenta,
        @totalGravada,
        @subTotalVentas,
        @descuNoSuj,
        @descuExenta,
        @descuGravada,
        @porcentajeDescuento,
        @totalDescu,
        @ivaPerci1,
        @ivaRete1,
        @reteRenta,
        @montoTotalOperacion,
        @totalNoGravado,
        @totalPagar,
        @totalLetras,
        @saldoFavor
    );
END;
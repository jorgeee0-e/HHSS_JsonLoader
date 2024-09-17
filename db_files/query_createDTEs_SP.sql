CREATE PROC SP_DTEs
as
begin 
select 
[codigoGeneracion],[nit_emisor],[nit_receptor],[version_],[tipoDte],[numeroControl],[fecEmi],[horaEmi]
FROM [dbo].[DTEs]
end

CREATE PROCEDURE SP_guardar_DTEs (
    @codigoGeneracion varchar(36),
	@nit_emisor varchar(14),
	@nit_receptor varchar(14),
	@version_ INT,
	@tipoDte varchar(2),
	@numeroControl varchar(31),
	@fecEmi DATE,
	@horaEmi TIME
)
AS 
BEGIN
    INSERT INTO 
	DTEs (
        codigoGeneracion,
		nit_emisor,
		nit_receptor,
		version_,
		tipoDte,
		numeroControl,
		fecEmi,
		horaEmi
    )
    VALUES (
        @codigoGeneracion,
		@nit_emisor,
		@nit_receptor,
		@version_ ,
		@tipoDte,
		@numeroControl,
		@fecEmi,
		@horaEmi
    );
END;
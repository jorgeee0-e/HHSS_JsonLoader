CREATE PROC SP_Receptores
as
begin 
select 
[nit],[nombre],[codActividad],[departamento],[municipio],[complemento],[correo]
FROM [dbo].[Receptores]
end

CREATE PROCEDURE SP_guardar_Receptores(
		@nit varchar(14),
		@nombre varchar(max),
		@codActividad VARCHAR(5),
		@departamento varchar(max),
		@municipio varchar(max),
		@complemento varchar(max),
		@correo varchar(max)
)
AS 
BEGIN
    INSERT INTO 
	Receptores (
        nit,
		nombre,
		codActividad,
		departamento,
		municipio,
		complemento,
		correo
    )
    VALUES (
        @nit,
		@nombre,
		@codActividad,
		@departamento,
		@municipio,
		@complemento,
		@correo
    );
END;